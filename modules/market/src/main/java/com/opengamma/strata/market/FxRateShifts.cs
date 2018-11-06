﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2017 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.market
{

	using Bean = org.joda.beans.Bean;
	using BeanBuilder = org.joda.beans.BeanBuilder;
	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using MetaProperty = org.joda.beans.MetaProperty;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using DirectMetaBean = org.joda.beans.impl.direct.DirectMetaBean;
	using DirectMetaProperty = org.joda.beans.impl.direct.DirectMetaProperty;
	using DirectMetaPropertyMap = org.joda.beans.impl.direct.DirectMetaPropertyMap;
	using DirectPrivateBeanBuilder = org.joda.beans.impl.direct.DirectPrivateBeanBuilder;
	using Logger = org.slf4j.Logger;
	using LoggerFactory = org.slf4j.LoggerFactory;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using CurrencyPair = com.opengamma.strata.basics.currency.CurrencyPair;
	using FxRate = com.opengamma.strata.basics.currency.FxRate;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using MarketDataBox = com.opengamma.strata.data.scenario.MarketDataBox;
	using ScenarioPerturbation = com.opengamma.strata.data.scenario.ScenarioPerturbation;

	/// <summary>
	/// A perturbation that applies different shifts to an FX rate.
	/// <para>
	/// This class contains shifts, each of which is associated with a scenario and applied to an FX rate based on the shift type.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private", constructorScope = "package") public final class FxRateShifts implements com.opengamma.strata.data.scenario.ScenarioPerturbation<com.opengamma.strata.basics.currency.FxRate>, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class FxRateShifts : ScenarioPerturbation<FxRate>, ImmutableBean
	{

	  /// <summary>
	  /// Logger. 
	  /// </summary>
	  private static readonly Logger log = LoggerFactory.getLogger(typeof(FxRateShifts));

	  /// <summary>
	  /// The type of shift applied to the FX rate.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final ShiftType shiftType;
	  private readonly ShiftType shiftType;

	  /// <summary>
	  /// The shifts to apply to {@code FxRate}.
	  /// <para>
	  /// Each element in the array corresponds to each scenario.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.collect.array.DoubleArray shiftAmount;
	  private readonly DoubleArray shiftAmount;

	  /// <summary>
	  /// The currency pair for which the shifts are applied.
	  /// <para>
	  /// This also defines the direction of the FX rate to be shifted.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.basics.currency.CurrencyPair currencyPair;
	  private readonly CurrencyPair currencyPair;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Creates an instance.
	  /// </summary>
	  /// <param name="shiftType">  the shift type </param>
	  /// <param name="shiftAmount">  the shift amount </param>
	  /// <param name="currencyPair">  the currency pair </param>
	  /// <returns> the instance </returns>
	  public static FxRateShifts of(ShiftType shiftType, DoubleArray shiftAmount, CurrencyPair currencyPair)
	  {
		return new FxRateShifts(shiftType, shiftAmount, currencyPair);
	  }

	  //-------------------------------------------------------------------------
	  public MarketDataBox<FxRate> applyTo(MarketDataBox<FxRate> marketData, ReferenceData refData)
	  {
		log.debug("Applying {} shift to FX rate '{}'", shiftType, marketData.getValue(0).Pair.ToString());
		return marketData.mapWithIndex(ScenarioCount, (fxRate, scenarioIndex) => FxRate.of(currencyPair, shiftType.applyShift(fxRate.fxRate(currencyPair), shiftAmount.get(scenarioIndex))));
	  }

	  public int ScenarioCount
	  {
		  get
		  {
			return shiftAmount.size();
		  }
	  }

	  public Type<FxRate> MarketDataType
	  {
		  get
		  {
			return typeof(FxRate);
		  }
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code FxRateShifts}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static FxRateShifts.Meta meta()
	  {
		return FxRateShifts.Meta.INSTANCE;
	  }

	  static FxRateShifts()
	  {
		MetaBean.register(FxRateShifts.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  /// <summary>
	  /// Creates an instance. </summary>
	  /// <param name="shiftType">  the value of the property, not null </param>
	  /// <param name="shiftAmount">  the value of the property, not null </param>
	  /// <param name="currencyPair">  the value of the property, not null </param>
	  internal FxRateShifts(ShiftType shiftType, DoubleArray shiftAmount, CurrencyPair currencyPair)
	  {
		JodaBeanUtils.notNull(shiftType, "shiftType");
		JodaBeanUtils.notNull(shiftAmount, "shiftAmount");
		JodaBeanUtils.notNull(currencyPair, "currencyPair");
		this.shiftType = shiftType;
		this.shiftAmount = shiftAmount;
		this.currencyPair = currencyPair;
	  }

	  public override FxRateShifts.Meta metaBean()
	  {
		return FxRateShifts.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the type of shift applied to the FX rate. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public ShiftType ShiftType
	  {
		  get
		  {
			return shiftType;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the shifts to apply to {@code FxRate}.
	  /// <para>
	  /// Each element in the array corresponds to each scenario.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public DoubleArray ShiftAmount
	  {
		  get
		  {
			return shiftAmount;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the currency pair for which the shifts are applied.
	  /// <para>
	  /// This also defines the direction of the FX rate to be shifted.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public CurrencyPair CurrencyPair
	  {
		  get
		  {
			return currencyPair;
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
		  FxRateShifts other = (FxRateShifts) obj;
		  return JodaBeanUtils.equal(shiftType, other.shiftType) && JodaBeanUtils.equal(shiftAmount, other.shiftAmount) && JodaBeanUtils.equal(currencyPair, other.currencyPair);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(shiftType);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(shiftAmount);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(currencyPair);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("FxRateShifts{");
		buf.Append("shiftType").Append('=').Append(shiftType).Append(',').Append(' ');
		buf.Append("shiftAmount").Append('=').Append(shiftAmount).Append(',').Append(' ');
		buf.Append("currencyPair").Append('=').Append(JodaBeanUtils.ToString(currencyPair));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code FxRateShifts}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  shiftType_Renamed = DirectMetaProperty.ofImmutable(this, "shiftType", typeof(FxRateShifts), typeof(ShiftType));
			  shiftAmount_Renamed = DirectMetaProperty.ofImmutable(this, "shiftAmount", typeof(FxRateShifts), typeof(DoubleArray));
			  currencyPair_Renamed = DirectMetaProperty.ofImmutable(this, "currencyPair", typeof(FxRateShifts), typeof(CurrencyPair));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "shiftType", "shiftAmount", "currencyPair");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code shiftType} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<ShiftType> shiftType_Renamed;
		/// <summary>
		/// The meta-property for the {@code shiftAmount} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<DoubleArray> shiftAmount_Renamed;
		/// <summary>
		/// The meta-property for the {@code currencyPair} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<CurrencyPair> currencyPair_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "shiftType", "shiftAmount", "currencyPair");
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
			case 893345500: // shiftType
			  return shiftType_Renamed;
			case -1043480710: // shiftAmount
			  return shiftAmount_Renamed;
			case 1005147787: // currencyPair
			  return currencyPair_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends FxRateShifts> builder()
		public override BeanBuilder<FxRateShifts> builder()
		{
		  return new FxRateShifts.Builder();
		}

		public override Type beanType()
		{
		  return typeof(FxRateShifts);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code shiftType} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<ShiftType> shiftType()
		{
		  return shiftType_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code shiftAmount} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<DoubleArray> shiftAmount()
		{
		  return shiftAmount_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code currencyPair} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<CurrencyPair> currencyPair()
		{
		  return currencyPair_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 893345500: // shiftType
			  return ((FxRateShifts) bean).ShiftType;
			case -1043480710: // shiftAmount
			  return ((FxRateShifts) bean).ShiftAmount;
			case 1005147787: // currencyPair
			  return ((FxRateShifts) bean).CurrencyPair;
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
	  /// The bean-builder for {@code FxRateShifts}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<FxRateShifts>
	  {

		internal ShiftType shiftType;
		internal DoubleArray shiftAmount;
		internal CurrencyPair currencyPair;

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
			case 893345500: // shiftType
			  return shiftType;
			case -1043480710: // shiftAmount
			  return shiftAmount;
			case 1005147787: // currencyPair
			  return currencyPair;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 893345500: // shiftType
			  this.shiftType = (ShiftType) newValue;
			  break;
			case -1043480710: // shiftAmount
			  this.shiftAmount = (DoubleArray) newValue;
			  break;
			case 1005147787: // currencyPair
			  this.currencyPair = (CurrencyPair) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override FxRateShifts build()
		{
		  return new FxRateShifts(shiftType, shiftAmount, currencyPair);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("FxRateShifts.Builder{");
		  buf.Append("shiftType").Append('=').Append(JodaBeanUtils.ToString(shiftType)).Append(',').Append(' ');
		  buf.Append("shiftAmount").Append('=').Append(JodaBeanUtils.ToString(shiftAmount)).Append(',').Append(' ');
		  buf.Append("currencyPair").Append('=').Append(JodaBeanUtils.ToString(currencyPair));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}