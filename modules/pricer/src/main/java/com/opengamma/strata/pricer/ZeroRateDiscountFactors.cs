﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer
{

	using Bean = org.joda.beans.Bean;
	using BeanBuilder = org.joda.beans.BeanBuilder;
	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using MetaProperty = org.joda.beans.MetaProperty;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using ImmutableConstructor = org.joda.beans.gen.ImmutableConstructor;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using DirectMetaBean = org.joda.beans.impl.direct.DirectMetaBean;
	using DirectMetaProperty = org.joda.beans.impl.direct.DirectMetaProperty;
	using DirectMetaPropertyMap = org.joda.beans.impl.direct.DirectMetaPropertyMap;
	using DirectPrivateBeanBuilder = org.joda.beans.impl.direct.DirectPrivateBeanBuilder;

	using Currency = com.opengamma.strata.basics.currency.Currency;
	using DayCount = com.opengamma.strata.basics.date.DayCount;
	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using MarketDataName = com.opengamma.strata.data.MarketDataName;
	using ValueType = com.opengamma.strata.market.ValueType;
	using Curve = com.opengamma.strata.market.curve.Curve;
	using CurveInfoType = com.opengamma.strata.market.curve.CurveInfoType;
	using Curves = com.opengamma.strata.market.curve.Curves;
	using InterpolatedNodalCurve = com.opengamma.strata.market.curve.InterpolatedNodalCurve;
	using CurrencyParameterSensitivities = com.opengamma.strata.market.param.CurrencyParameterSensitivities;
	using CurrencyParameterSensitivity = com.opengamma.strata.market.param.CurrencyParameterSensitivity;
	using ParameterMetadata = com.opengamma.strata.market.param.ParameterMetadata;
	using ParameterPerturbation = com.opengamma.strata.market.param.ParameterPerturbation;
	using UnitParameterSensitivity = com.opengamma.strata.market.param.UnitParameterSensitivity;

	/// <summary>
	/// Provides access to discount factors for a currency based on a zero rate continuously compounded curve.
	/// <para>
	/// This provides discount factors for a single currency.
	/// </para>
	/// <para>
	/// This implementation is based on an underlying curve that is stored with maturities
	/// and zero-coupon continuously-compounded rates.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class ZeroRateDiscountFactors implements DiscountFactors, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class ZeroRateDiscountFactors : DiscountFactors, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final com.opengamma.strata.basics.currency.Currency currency;
		private readonly Currency currency;
	  /// <summary>
	  /// The valuation date.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final java.time.LocalDate valuationDate;
	  private readonly LocalDate valuationDate;
	  /// <summary>
	  /// The underlying curve.
	  /// The metadata of the curve must define a day count.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.market.curve.Curve curve;
	  private readonly Curve curve;
	  /// <summary>
	  /// The day count convention of the curve.
	  /// </summary>
	  [NonSerialized]
	  private readonly DayCount dayCount; // cached, not a property

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance based on a zero-rates curve.
	  /// <para>
	  /// The curve is specified by an instance of <seealso cref="Curve"/>, such as <seealso cref="InterpolatedNodalCurve"/>.
	  /// The curve must contain <seealso cref="ValueType#YEAR_FRACTION year fractions"/>
	  /// against <seealso cref="ValueType#ZERO_RATE zero rates"/>, and the day count must be present.
	  /// A suitable metadata instance for the curve can be created by <seealso cref="Curves#zeroRates(String, DayCount)"/>.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="currency">  the currency </param>
	  /// <param name="valuationDate">  the valuation date for which the curve is valid </param>
	  /// <param name="underlyingCurve">  the underlying curve </param>
	  /// <returns> the curve </returns>
	  public static ZeroRateDiscountFactors of(Currency currency, LocalDate valuationDate, Curve underlyingCurve)
	  {
		return new ZeroRateDiscountFactors(currency, valuationDate, underlyingCurve);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutableConstructor private ZeroRateDiscountFactors(com.opengamma.strata.basics.currency.Currency currency, java.time.LocalDate valuationDate, com.opengamma.strata.market.curve.Curve curve)
	  private ZeroRateDiscountFactors(Currency currency, LocalDate valuationDate, Curve curve)
	  {

		ArgChecker.notNull(currency, "currency");
		ArgChecker.notNull(valuationDate, "valuationDate");
		ArgChecker.notNull(curve, "curve");
		curve.Metadata.XValueType.checkEquals(ValueType.YEAR_FRACTION, "Incorrect x-value type for zero-rate discount curve");
		curve.Metadata.YValueType.checkEquals(ValueType.ZERO_RATE, "Incorrect y-value type for zero-rate discount curve");
		DayCount dayCount = curve.Metadata.findInfo(CurveInfoType.DAY_COUNT).orElseThrow(() => new System.ArgumentException("Incorrect curve metadata, missing DayCount"));

		this.currency = currency;
		this.valuationDate = valuationDate;
		this.curve = curve;
		this.dayCount = dayCount;
	  }

	  // ensure standard constructor is invoked
	  private object readResolve()
	  {
		return new ZeroRateDiscountFactors(currency, valuationDate, curve);
	  }

	  //-------------------------------------------------------------------------
	  public Optional<T> findData<T>(MarketDataName<T> name)
	  {
		if (curve.Name.Equals(name))
		{
		  return (name.MarketDataType.cast(curve));
		}
		return null;
	  }

	  public int ParameterCount
	  {
		  get
		  {
			return curve.ParameterCount;
		  }
	  }

	  public double getParameter(int parameterIndex)
	  {
		return curve.getParameter(parameterIndex);
	  }

	  public ParameterMetadata getParameterMetadata(int parameterIndex)
	  {
		return curve.getParameterMetadata(parameterIndex);
	  }

	  public ZeroRateDiscountFactors withParameter(int parameterIndex, double newValue)
	  {
		return withCurve(curve.withParameter(parameterIndex, newValue));
	  }

	  public ZeroRateDiscountFactors withPerturbation(ParameterPerturbation perturbation)
	  {
		return withCurve(curve.withPerturbation(perturbation));
	  }

	  //-------------------------------------------------------------------------
	  public double relativeYearFraction(LocalDate date)
	  {
		return dayCount.relativeYearFraction(valuationDate, date);
	  }

	  public double discountFactor(double yearFraction)
	  {
		// convert zero rate to discount factor
		return Math.Exp(-yearFraction * curve.yValue(yearFraction));
	  }

	  public double discountFactorTimeDerivative(double yearFraction)
	  {
		double zr = curve.yValue(yearFraction);
		return -Math.Exp(-yearFraction * curve.yValue(yearFraction)) * (zr + curve.firstDerivative(yearFraction) * yearFraction);
	  }

	  public double zeroRate(double yearFraction)
	  {
		return curve.yValue(yearFraction);
	  }

	  //-------------------------------------------------------------------------
	  public ZeroRateSensitivity zeroRatePointSensitivity(double yearFraction, Currency sensitivityCurrency)
	  {
		double discountFactor = this.discountFactor(yearFraction);
		return ZeroRateSensitivity.of(currency, yearFraction, sensitivityCurrency, -discountFactor * yearFraction);
	  }

	  public CurrencyParameterSensitivities parameterSensitivity(ZeroRateSensitivity pointSens)
	  {
		double yearFraction = pointSens.YearFraction;
		UnitParameterSensitivity unitSens = curve.yValueParameterSensitivity(yearFraction);
		CurrencyParameterSensitivity curSens = unitSens.multipliedBy(pointSens.Currency, pointSens.Sensitivity);
		return CurrencyParameterSensitivities.of(curSens);
	  }

	  public CurrencyParameterSensitivities createParameterSensitivity(Currency currency, DoubleArray sensitivities)
	  {
		return CurrencyParameterSensitivities.of(curve.createParameterSensitivity(currency, sensitivities));
	  }

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Returns a new instance with a different curve.
	  /// </summary>
	  /// <param name="curve">  the new curve </param>
	  /// <returns> the new instance </returns>
	  public ZeroRateDiscountFactors withCurve(Curve curve)
	  {
		return new ZeroRateDiscountFactors(currency, valuationDate, curve);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code ZeroRateDiscountFactors}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static ZeroRateDiscountFactors.Meta meta()
	  {
		return ZeroRateDiscountFactors.Meta.INSTANCE;
	  }

	  static ZeroRateDiscountFactors()
	  {
		MetaBean.register(ZeroRateDiscountFactors.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  public override ZeroRateDiscountFactors.Meta metaBean()
	  {
		return ZeroRateDiscountFactors.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the currency that the discount factors are for. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public Currency Currency
	  {
		  get
		  {
			return currency;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the valuation date. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public LocalDate ValuationDate
	  {
		  get
		  {
			return valuationDate;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the underlying curve.
	  /// The metadata of the curve must define a day count. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public Curve Curve
	  {
		  get
		  {
			return curve;
		  }
	  }

	  //-----------------------------------------------------------------------
	  public override bool Equals(object obj)
	  {
		if (obj == this)
		{
		  return true;
		}
		if (obj != null && obj.GetType() == this.GetType())
		{
		  ZeroRateDiscountFactors other = (ZeroRateDiscountFactors) obj;
		  return JodaBeanUtils.equal(currency, other.currency) && JodaBeanUtils.equal(valuationDate, other.valuationDate) && JodaBeanUtils.equal(curve, other.curve);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(currency);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(valuationDate);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(curve);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("ZeroRateDiscountFactors{");
		buf.Append("currency").Append('=').Append(currency).Append(',').Append(' ');
		buf.Append("valuationDate").Append('=').Append(valuationDate).Append(',').Append(' ');
		buf.Append("curve").Append('=').Append(JodaBeanUtils.ToString(curve));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code ZeroRateDiscountFactors}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  currency_Renamed = DirectMetaProperty.ofImmutable(this, "currency", typeof(ZeroRateDiscountFactors), typeof(Currency));
			  valuationDate_Renamed = DirectMetaProperty.ofImmutable(this, "valuationDate", typeof(ZeroRateDiscountFactors), typeof(LocalDate));
			  curve_Renamed = DirectMetaProperty.ofImmutable(this, "curve", typeof(ZeroRateDiscountFactors), typeof(Curve));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "currency", "valuationDate", "curve");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code currency} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<Currency> currency_Renamed;
		/// <summary>
		/// The meta-property for the {@code valuationDate} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<LocalDate> valuationDate_Renamed;
		/// <summary>
		/// The meta-property for the {@code curve} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<Curve> curve_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "currency", "valuationDate", "curve");
		internal IDictionary<string, MetaProperty<object>> metaPropertyMap$;

		/// <summary>
		/// Restricted constructor.
		/// </summary>
		internal Meta()
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override protected org.joda.beans.MetaProperty<?> metaPropertyGet(String propertyName)
		protected internal override MetaProperty<object> metaPropertyGet(string propertyName)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 575402001: // currency
			  return currency_Renamed;
			case 113107279: // valuationDate
			  return valuationDate_Renamed;
			case 95027439: // curve
			  return curve_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends ZeroRateDiscountFactors> builder()
		public override BeanBuilder<ZeroRateDiscountFactors> builder()
		{
		  return new ZeroRateDiscountFactors.Builder();
		}

		public override Type beanType()
		{
		  return typeof(ZeroRateDiscountFactors);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code currency} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<Currency> currency()
		{
		  return currency_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code valuationDate} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<LocalDate> valuationDate()
		{
		  return valuationDate_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code curve} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<Curve> curve()
		{
		  return curve_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 575402001: // currency
			  return ((ZeroRateDiscountFactors) bean).Currency;
			case 113107279: // valuationDate
			  return ((ZeroRateDiscountFactors) bean).ValuationDate;
			case 95027439: // curve
			  return ((ZeroRateDiscountFactors) bean).Curve;
		  }
		  return base.propertyGet(bean, propertyName, quiet);
		}

		protected internal override void propertySet(Bean bean, string propertyName, object newValue, bool quiet)
		{
		  metaProperty(propertyName);
		  if (quiet)
		  {
			return;
		  }
		  throw new System.NotSupportedException("Property cannot be written: " + propertyName);
		}

	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The bean-builder for {@code ZeroRateDiscountFactors}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<ZeroRateDiscountFactors>
	  {

		internal Currency currency;
		internal LocalDate valuationDate;
		internal Curve curve;

		/// <summary>
		/// Restricted constructor.
		/// </summary>
		internal Builder()
		{
		}

		//-----------------------------------------------------------------------
		public override object get(string propertyName)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 575402001: // currency
			  return currency;
			case 113107279: // valuationDate
			  return valuationDate;
			case 95027439: // curve
			  return curve;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 575402001: // currency
			  this.currency = (Currency) newValue;
			  break;
			case 113107279: // valuationDate
			  this.valuationDate = (LocalDate) newValue;
			  break;
			case 95027439: // curve
			  this.curve = (Curve) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override ZeroRateDiscountFactors build()
		{
		  return new ZeroRateDiscountFactors(currency, valuationDate, curve);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("ZeroRateDiscountFactors.Builder{");
		  buf.Append("currency").Append('=').Append(JodaBeanUtils.ToString(currency)).Append(',').Append(' ');
		  buf.Append("valuationDate").Append('=').Append(JodaBeanUtils.ToString(valuationDate)).Append(',').Append(' ');
		  buf.Append("curve").Append('=').Append(JodaBeanUtils.ToString(curve));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}