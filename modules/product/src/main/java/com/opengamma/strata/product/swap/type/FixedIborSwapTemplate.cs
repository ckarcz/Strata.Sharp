﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.swap.type
{

	using Bean = org.joda.beans.Bean;
	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using MetaProperty = org.joda.beans.MetaProperty;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using ImmutableValidator = org.joda.beans.gen.ImmutableValidator;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using DirectFieldsBeanBuilder = org.joda.beans.impl.direct.DirectFieldsBeanBuilder;
	using DirectMetaBean = org.joda.beans.impl.direct.DirectMetaBean;
	using DirectMetaProperty = org.joda.beans.impl.direct.DirectMetaProperty;
	using DirectMetaPropertyMap = org.joda.beans.impl.direct.DirectMetaPropertyMap;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using ReferenceDataNotFoundException = com.opengamma.strata.basics.ReferenceDataNotFoundException;
	using Tenor = com.opengamma.strata.basics.date.Tenor;
	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using BuySell = com.opengamma.strata.product.common.BuySell;

	/// <summary>
	/// A template for creating Fixed-Ibor swap trades.
	/// <para>
	/// This defines almost all the data necessary to create a Fixed-Ibor single currency <seealso cref="SwapTrade"/>.
	/// The trade date, notional and fixed rate are required to complete the template and create the trade.
	/// As such, it is often possible to get a market price for a trade based on the template.
	/// The market price is typically quoted as a bid/ask on the fixed rate.
	/// </para>
	/// <para>
	/// The template references four dates.
	/// <ul>
	/// <li>Trade date, the date that the trade is agreed
	/// <li>Spot date, the base for date calculations, typically 2 business days after the trade date
	/// <li>Start date, the date on which accrual starts
	/// <li>End date, the date on which accrual ends
	/// </ul>
	/// Some of these dates are specified by the convention embedded within this template.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition public final class FixedIborSwapTemplate implements com.opengamma.strata.product.TradeTemplate, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class FixedIborSwapTemplate : TradeTemplate, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final java.time.Period periodToStart;
		private readonly Period periodToStart;
	  /// <summary>
	  /// The tenor of the swap.
	  /// <para>
	  /// This is the period from the first accrual date to the last accrual date.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.basics.date.Tenor tenor;
	  private readonly Tenor tenor;
	  /// <summary>
	  /// The market convention of the swap.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final FixedIborSwapConvention convention;
	  private readonly FixedIborSwapConvention convention;

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutableValidator private void validate()
	  private void validate()
	  {
		ArgChecker.isFalse(periodToStart.Negative, "Period to start must not be negative");
	  }

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains a template based on the specified tenor and convention.
	  /// <para>
	  /// The swap will start on the spot date.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="tenor">  the tenor of the swap </param>
	  /// <param name="convention">  the market convention </param>
	  /// <returns> the template </returns>
	  public static FixedIborSwapTemplate of(Tenor tenor, FixedIborSwapConvention convention)
	  {
		return of(Period.ZERO, tenor, convention);
	  }

	  /// <summary>
	  /// Creates a template based on the specified period, tenor and convention.
	  /// <para>
	  /// The period from the spot date to the start date is specified.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="periodToStart">  the period between the spot date and the start date </param>
	  /// <param name="tenor">  the tenor of the swap </param>
	  /// <param name="convention">  the market convention </param>
	  /// <returns> the template </returns>
	  public static FixedIborSwapTemplate of(Period periodToStart, Tenor tenor, FixedIborSwapConvention convention)
	  {
		return FixedIborSwapTemplate.builder().periodToStart(periodToStart).tenor(tenor).convention(convention).build();
	  }

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Creates a trade based on this template.
	  /// <para>
	  /// This returns a trade based on the specified trade date.
	  /// </para>
	  /// <para>
	  /// The notional is unsigned, with buy/sell determining the direction of the trade.
	  /// If buying the swap, the floating rate is received from the counterparty, with the fixed rate being paid.
	  /// If selling the swap, the floating rate is paid to the counterparty, with the fixed rate being received.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="tradeDate">  the date of the trade </param>
	  /// <param name="buySell">  the buy/sell flag </param>
	  /// <param name="notional">  the notional amount, in the payment currency of the template </param>
	  /// <param name="fixedRate">  the fixed rate, typically derived from the market </param>
	  /// <param name="refData">  the reference data, used to resolve the trade dates </param>
	  /// <returns> the trade </returns>
	  /// <exception cref="ReferenceDataNotFoundException"> if an identifier cannot be resolved in the reference data </exception>
	  public SwapTrade createTrade(LocalDate tradeDate, BuySell buySell, double notional, double fixedRate, ReferenceData refData)
	  {

		return convention.createTrade(tradeDate, periodToStart, tenor, buySell, notional, fixedRate, refData);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code FixedIborSwapTemplate}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static FixedIborSwapTemplate.Meta meta()
	  {
		return FixedIborSwapTemplate.Meta.INSTANCE;
	  }

	  static FixedIborSwapTemplate()
	  {
		MetaBean.register(FixedIborSwapTemplate.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  /// <summary>
	  /// Returns a builder used to create an instance of the bean. </summary>
	  /// <returns> the builder, not null </returns>
	  public static FixedIborSwapTemplate.Builder builder()
	  {
		return new FixedIborSwapTemplate.Builder();
	  }

	  private FixedIborSwapTemplate(Period periodToStart, Tenor tenor, FixedIborSwapConvention convention)
	  {
		JodaBeanUtils.notNull(periodToStart, "periodToStart");
		JodaBeanUtils.notNull(tenor, "tenor");
		JodaBeanUtils.notNull(convention, "convention");
		this.periodToStart = periodToStart;
		this.tenor = tenor;
		this.convention = convention;
		validate();
	  }

	  public override FixedIborSwapTemplate.Meta metaBean()
	  {
		return FixedIborSwapTemplate.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the period between the spot value date and the start date.
	  /// <para>
	  /// This is often zero, but can be greater if the swap if <i>forward starting</i>.
	  /// This must not be negative.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public Period PeriodToStart
	  {
		  get
		  {
			return periodToStart;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the tenor of the swap.
	  /// <para>
	  /// This is the period from the first accrual date to the last accrual date.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public Tenor Tenor
	  {
		  get
		  {
			return tenor;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the market convention of the swap. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public FixedIborSwapConvention Convention
	  {
		  get
		  {
			return convention;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Returns a builder that allows this bean to be mutated. </summary>
	  /// <returns> the mutable builder, not null </returns>
	  public Builder toBuilder()
	  {
		return new Builder(this);
	  }

	  public override bool Equals(object obj)
	  {
		if (obj == this)
		{
		  return true;
		}
		if (obj != null && obj.GetType() == this.GetType())
		{
		  FixedIborSwapTemplate other = (FixedIborSwapTemplate) obj;
		  return JodaBeanUtils.equal(periodToStart, other.periodToStart) && JodaBeanUtils.equal(tenor, other.tenor) && JodaBeanUtils.equal(convention, other.convention);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(periodToStart);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(tenor);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(convention);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("FixedIborSwapTemplate{");
		buf.Append("periodToStart").Append('=').Append(periodToStart).Append(',').Append(' ');
		buf.Append("tenor").Append('=').Append(tenor).Append(',').Append(' ');
		buf.Append("convention").Append('=').Append(JodaBeanUtils.ToString(convention));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code FixedIborSwapTemplate}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  periodToStart_Renamed = DirectMetaProperty.ofImmutable(this, "periodToStart", typeof(FixedIborSwapTemplate), typeof(Period));
			  tenor_Renamed = DirectMetaProperty.ofImmutable(this, "tenor", typeof(FixedIborSwapTemplate), typeof(Tenor));
			  convention_Renamed = DirectMetaProperty.ofImmutable(this, "convention", typeof(FixedIborSwapTemplate), typeof(FixedIborSwapConvention));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "periodToStart", "tenor", "convention");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code periodToStart} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<Period> periodToStart_Renamed;
		/// <summary>
		/// The meta-property for the {@code tenor} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<Tenor> tenor_Renamed;
		/// <summary>
		/// The meta-property for the {@code convention} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<FixedIborSwapConvention> convention_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "periodToStart", "tenor", "convention");
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
			case -574688858: // periodToStart
			  return periodToStart_Renamed;
			case 110246592: // tenor
			  return tenor_Renamed;
			case 2039569265: // convention
			  return convention_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

		public override FixedIborSwapTemplate.Builder builder()
		{
		  return new FixedIborSwapTemplate.Builder();
		}

		public override Type beanType()
		{
		  return typeof(FixedIborSwapTemplate);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code periodToStart} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<Period> periodToStart()
		{
		  return periodToStart_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code tenor} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<Tenor> tenor()
		{
		  return tenor_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code convention} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<FixedIborSwapConvention> convention()
		{
		  return convention_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -574688858: // periodToStart
			  return ((FixedIborSwapTemplate) bean).PeriodToStart;
			case 110246592: // tenor
			  return ((FixedIborSwapTemplate) bean).Tenor;
			case 2039569265: // convention
			  return ((FixedIborSwapTemplate) bean).Convention;
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
	  /// The bean-builder for {@code FixedIborSwapTemplate}.
	  /// </summary>
	  public sealed class Builder : DirectFieldsBeanBuilder<FixedIborSwapTemplate>
	  {

//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal Period periodToStart_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal Tenor tenor_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal FixedIborSwapConvention convention_Renamed;

		/// <summary>
		/// Restricted constructor.
		/// </summary>
		internal Builder()
		{
		}

		/// <summary>
		/// Restricted copy constructor. </summary>
		/// <param name="beanToCopy">  the bean to copy from, not null </param>
		internal Builder(FixedIborSwapTemplate beanToCopy)
		{
		  this.periodToStart_Renamed = beanToCopy.PeriodToStart;
		  this.tenor_Renamed = beanToCopy.Tenor;
		  this.convention_Renamed = beanToCopy.Convention;
		}

		//-----------------------------------------------------------------------
		public override object get(string propertyName)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -574688858: // periodToStart
			  return periodToStart_Renamed;
			case 110246592: // tenor
			  return tenor_Renamed;
			case 2039569265: // convention
			  return convention_Renamed;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -574688858: // periodToStart
			  this.periodToStart_Renamed = (Period) newValue;
			  break;
			case 110246592: // tenor
			  this.tenor_Renamed = (Tenor) newValue;
			  break;
			case 2039569265: // convention
			  this.convention_Renamed = (FixedIborSwapConvention) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override Builder set<T1>(MetaProperty<T1> property, object value)
		{
		  base.set(property, value);
		  return this;
		}

		public override FixedIborSwapTemplate build()
		{
		  return new FixedIborSwapTemplate(periodToStart_Renamed, tenor_Renamed, convention_Renamed);
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// Sets the period between the spot value date and the start date.
		/// <para>
		/// This is often zero, but can be greater if the swap if <i>forward starting</i>.
		/// This must not be negative.
		/// </para>
		/// </summary>
		/// <param name="periodToStart">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder periodToStart(Period periodToStart)
		{
		  JodaBeanUtils.notNull(periodToStart, "periodToStart");
		  this.periodToStart_Renamed = periodToStart;
		  return this;
		}

		/// <summary>
		/// Sets the tenor of the swap.
		/// <para>
		/// This is the period from the first accrual date to the last accrual date.
		/// </para>
		/// </summary>
		/// <param name="tenor">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder tenor(Tenor tenor)
		{
		  JodaBeanUtils.notNull(tenor, "tenor");
		  this.tenor_Renamed = tenor;
		  return this;
		}

		/// <summary>
		/// Sets the market convention of the swap. </summary>
		/// <param name="convention">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder convention(FixedIborSwapConvention convention)
		{
		  JodaBeanUtils.notNull(convention, "convention");
		  this.convention_Renamed = convention;
		  return this;
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("FixedIborSwapTemplate.Builder{");
		  buf.Append("periodToStart").Append('=').Append(JodaBeanUtils.ToString(periodToStart_Renamed)).Append(',').Append(' ');
		  buf.Append("tenor").Append('=').Append(JodaBeanUtils.ToString(tenor_Renamed)).Append(',').Append(' ');
		  buf.Append("convention").Append('=').Append(JodaBeanUtils.ToString(convention_Renamed));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}