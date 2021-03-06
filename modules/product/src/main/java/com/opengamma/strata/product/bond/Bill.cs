﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2018 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.bond
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
	using Resolvable = com.opengamma.strata.basics.Resolvable;
	using AdjustablePayment = com.opengamma.strata.basics.currency.AdjustablePayment;
	using Currency = com.opengamma.strata.basics.currency.Currency;
	using DayCount = com.opengamma.strata.basics.date.DayCount;
	using DaysAdjustment = com.opengamma.strata.basics.date.DaysAdjustment;
	using ArgChecker = com.opengamma.strata.collect.ArgChecker;

	/// <summary>
	/// A bill.
	/// <para>
	/// A bill is a financial instrument that represents a unique fixed payment.
	/// 
	/// <h4>Price and yield</h4>
	/// Strata uses <i>decimal</i> yields and prices for bills in the trade model, pricers and market data.
	/// For example, a price of 99.32% is represented in Strata by 0.9932 and a yield of 1.32% is represented by 0.0132.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(constructorScope = "package") public final class Bill implements com.opengamma.strata.product.SecuritizedProduct, com.opengamma.strata.basics.Resolvable<ResolvedBill>, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class Bill : SecuritizedProduct, Resolvable<ResolvedBill>, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final com.opengamma.strata.product.SecurityId securityId;
		private readonly SecurityId securityId;
	  /// <summary>
	  /// The adjustable notional payment of the bill notional, the amount must be positive.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.basics.currency.AdjustablePayment notional;
	  private readonly AdjustablePayment notional;
	  /// <summary>
	  /// The day count convention applicable.
	  /// <para>
	  /// The conversion from dates to a numerical value is made based on this day count.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.basics.date.DayCount dayCount;
	  private readonly DayCount dayCount;
	  /// <summary>
	  /// Yield convention.
	  /// <para>
	  /// The convention defines how to convert from yield to price and inversely.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final BillYieldConvention yieldConvention;
	  private readonly BillYieldConvention yieldConvention;
	  /// <summary>
	  /// The legal entity identifier.
	  /// <para>
	  /// This identifier is used for the legal entity that issues the bill.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.product.LegalEntityId legalEntityId;
	  private readonly LegalEntityId legalEntityId;
	  /// <summary>
	  /// The number of days between valuation date and settlement date.
	  /// <para>
	  /// It is usually one business day for US and UK bills and two days for Euroland government bills.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.basics.date.DaysAdjustment settlementDateOffset;
	  private readonly DaysAdjustment settlementDateOffset;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutableValidator private void validate()
	  private void validate()
	  {
		ArgChecker.isTrue(settlementDateOffset.Days >= 0, "The settlement date offset must be non-negative");
		ArgChecker.isTrue(notional.Amount > 0, "Notional must be strictly positive");
	  }

	  public Currency Currency
	  {
		  get
		  {
			return notional.Currency;
		  }
	  }

	  /// <summary>
	  /// Computes the price from the yield at a given settlement date.
	  /// </summary>
	  /// <param name="yield">  the yield </param>
	  /// <param name="settlementDate">  the settlement date </param>
	  /// <returns> the price </returns>
	  public double priceFromYield(double yield, LocalDate settlementDate)
	  {
		double accrualFactor = dayCount.relativeYearFraction(settlementDate, notional.Date.Unadjusted);
		return yieldConvention.priceFromYield(yield, accrualFactor);
	  }

	  /// <summary>
	  /// Computes the yield from the price at a given settlement date.
	  /// </summary>
	  /// <param name="price">  the price </param>
	  /// <param name="settlementDate">  the settlement date </param>
	  /// <returns> the yield </returns>
	  public double yieldFromPrice(double price, LocalDate settlementDate)
	  {
		double accrualFactor = dayCount.relativeYearFraction(settlementDate, notional.Date.Unadjusted);
		return yieldConvention.yieldFromPrice(price, accrualFactor);
	  }

	  //-------------------------------------------------------------------------
	  public ResolvedBill resolve(ReferenceData refData)
	  {
		return ResolvedBill.builder().securityId(securityId).notional(notional.resolve(refData)).dayCount(dayCount).yieldConvention(yieldConvention).legalEntityId(legalEntityId).settlementDateOffset(settlementDateOffset).build();
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code Bill}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static Bill.Meta meta()
	  {
		return Bill.Meta.INSTANCE;
	  }

	  static Bill()
	  {
		MetaBean.register(Bill.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  /// <summary>
	  /// Returns a builder used to create an instance of the bean. </summary>
	  /// <returns> the builder, not null </returns>
	  public static Bill.Builder builder()
	  {
		return new Bill.Builder();
	  }

	  /// <summary>
	  /// Creates an instance. </summary>
	  /// <param name="securityId">  the value of the property, not null </param>
	  /// <param name="notional">  the value of the property, not null </param>
	  /// <param name="dayCount">  the value of the property, not null </param>
	  /// <param name="yieldConvention">  the value of the property, not null </param>
	  /// <param name="legalEntityId">  the value of the property, not null </param>
	  /// <param name="settlementDateOffset">  the value of the property, not null </param>
	  internal Bill(SecurityId securityId, AdjustablePayment notional, DayCount dayCount, BillYieldConvention yieldConvention, LegalEntityId legalEntityId, DaysAdjustment settlementDateOffset)
	  {
		JodaBeanUtils.notNull(securityId, "securityId");
		JodaBeanUtils.notNull(notional, "notional");
		JodaBeanUtils.notNull(dayCount, "dayCount");
		JodaBeanUtils.notNull(yieldConvention, "yieldConvention");
		JodaBeanUtils.notNull(legalEntityId, "legalEntityId");
		JodaBeanUtils.notNull(settlementDateOffset, "settlementDateOffset");
		this.securityId = securityId;
		this.notional = notional;
		this.dayCount = dayCount;
		this.yieldConvention = yieldConvention;
		this.legalEntityId = legalEntityId;
		this.settlementDateOffset = settlementDateOffset;
		validate();
	  }

	  public override Bill.Meta metaBean()
	  {
		return Bill.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the security identifier.
	  /// <para>
	  /// This identifier uniquely identifies the security within the system.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public SecurityId SecurityId
	  {
		  get
		  {
			return securityId;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the adjustable notional payment of the bill notional, the amount must be positive. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public AdjustablePayment Notional
	  {
		  get
		  {
			return notional;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the day count convention applicable.
	  /// <para>
	  /// The conversion from dates to a numerical value is made based on this day count.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public DayCount DayCount
	  {
		  get
		  {
			return dayCount;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets yield convention.
	  /// <para>
	  /// The convention defines how to convert from yield to price and inversely.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public BillYieldConvention YieldConvention
	  {
		  get
		  {
			return yieldConvention;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the legal entity identifier.
	  /// <para>
	  /// This identifier is used for the legal entity that issues the bill.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public LegalEntityId LegalEntityId
	  {
		  get
		  {
			return legalEntityId;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the number of days between valuation date and settlement date.
	  /// <para>
	  /// It is usually one business day for US and UK bills and two days for Euroland government bills.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public DaysAdjustment SettlementDateOffset
	  {
		  get
		  {
			return settlementDateOffset;
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
		  Bill other = (Bill) obj;
		  return JodaBeanUtils.equal(securityId, other.securityId) && JodaBeanUtils.equal(notional, other.notional) && JodaBeanUtils.equal(dayCount, other.dayCount) && JodaBeanUtils.equal(yieldConvention, other.yieldConvention) && JodaBeanUtils.equal(legalEntityId, other.legalEntityId) && JodaBeanUtils.equal(settlementDateOffset, other.settlementDateOffset);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(securityId);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(notional);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(dayCount);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(yieldConvention);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(legalEntityId);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(settlementDateOffset);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(224);
		buf.Append("Bill{");
		buf.Append("securityId").Append('=').Append(securityId).Append(',').Append(' ');
		buf.Append("notional").Append('=').Append(notional).Append(',').Append(' ');
		buf.Append("dayCount").Append('=').Append(dayCount).Append(',').Append(' ');
		buf.Append("yieldConvention").Append('=').Append(yieldConvention).Append(',').Append(' ');
		buf.Append("legalEntityId").Append('=').Append(legalEntityId).Append(',').Append(' ');
		buf.Append("settlementDateOffset").Append('=').Append(JodaBeanUtils.ToString(settlementDateOffset));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code Bill}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  securityId_Renamed = DirectMetaProperty.ofImmutable(this, "securityId", typeof(Bill), typeof(SecurityId));
			  notional_Renamed = DirectMetaProperty.ofImmutable(this, "notional", typeof(Bill), typeof(AdjustablePayment));
			  dayCount_Renamed = DirectMetaProperty.ofImmutable(this, "dayCount", typeof(Bill), typeof(DayCount));
			  yieldConvention_Renamed = DirectMetaProperty.ofImmutable(this, "yieldConvention", typeof(Bill), typeof(BillYieldConvention));
			  legalEntityId_Renamed = DirectMetaProperty.ofImmutable(this, "legalEntityId", typeof(Bill), typeof(LegalEntityId));
			  settlementDateOffset_Renamed = DirectMetaProperty.ofImmutable(this, "settlementDateOffset", typeof(Bill), typeof(DaysAdjustment));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "securityId", "notional", "dayCount", "yieldConvention", "legalEntityId", "settlementDateOffset");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code securityId} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<SecurityId> securityId_Renamed;
		/// <summary>
		/// The meta-property for the {@code notional} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<AdjustablePayment> notional_Renamed;
		/// <summary>
		/// The meta-property for the {@code dayCount} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<DayCount> dayCount_Renamed;
		/// <summary>
		/// The meta-property for the {@code yieldConvention} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<BillYieldConvention> yieldConvention_Renamed;
		/// <summary>
		/// The meta-property for the {@code legalEntityId} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<LegalEntityId> legalEntityId_Renamed;
		/// <summary>
		/// The meta-property for the {@code settlementDateOffset} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<DaysAdjustment> settlementDateOffset_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "securityId", "notional", "dayCount", "yieldConvention", "legalEntityId", "settlementDateOffset");
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
			case 1574023291: // securityId
			  return securityId_Renamed;
			case 1585636160: // notional
			  return notional_Renamed;
			case 1905311443: // dayCount
			  return dayCount_Renamed;
			case -1895216418: // yieldConvention
			  return yieldConvention_Renamed;
			case 866287159: // legalEntityId
			  return legalEntityId_Renamed;
			case 135924714: // settlementDateOffset
			  return settlementDateOffset_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

		public override Bill.Builder builder()
		{
		  return new Bill.Builder();
		}

		public override Type beanType()
		{
		  return typeof(Bill);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code securityId} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<SecurityId> securityId()
		{
		  return securityId_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code notional} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<AdjustablePayment> notional()
		{
		  return notional_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code dayCount} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<DayCount> dayCount()
		{
		  return dayCount_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code yieldConvention} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<BillYieldConvention> yieldConvention()
		{
		  return yieldConvention_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code legalEntityId} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<LegalEntityId> legalEntityId()
		{
		  return legalEntityId_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code settlementDateOffset} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<DaysAdjustment> settlementDateOffset()
		{
		  return settlementDateOffset_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 1574023291: // securityId
			  return ((Bill) bean).SecurityId;
			case 1585636160: // notional
			  return ((Bill) bean).Notional;
			case 1905311443: // dayCount
			  return ((Bill) bean).DayCount;
			case -1895216418: // yieldConvention
			  return ((Bill) bean).YieldConvention;
			case 866287159: // legalEntityId
			  return ((Bill) bean).LegalEntityId;
			case 135924714: // settlementDateOffset
			  return ((Bill) bean).SettlementDateOffset;
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
	  /// The bean-builder for {@code Bill}.
	  /// </summary>
	  public sealed class Builder : DirectFieldsBeanBuilder<Bill>
	  {

//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal SecurityId securityId_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal AdjustablePayment notional_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal DayCount dayCount_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal BillYieldConvention yieldConvention_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal LegalEntityId legalEntityId_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal DaysAdjustment settlementDateOffset_Renamed;

		/// <summary>
		/// Restricted constructor.
		/// </summary>
		internal Builder()
		{
		}

		/// <summary>
		/// Restricted copy constructor. </summary>
		/// <param name="beanToCopy">  the bean to copy from, not null </param>
		internal Builder(Bill beanToCopy)
		{
		  this.securityId_Renamed = beanToCopy.SecurityId;
		  this.notional_Renamed = beanToCopy.Notional;
		  this.dayCount_Renamed = beanToCopy.DayCount;
		  this.yieldConvention_Renamed = beanToCopy.YieldConvention;
		  this.legalEntityId_Renamed = beanToCopy.LegalEntityId;
		  this.settlementDateOffset_Renamed = beanToCopy.SettlementDateOffset;
		}

		//-----------------------------------------------------------------------
		public override object get(string propertyName)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 1574023291: // securityId
			  return securityId_Renamed;
			case 1585636160: // notional
			  return notional_Renamed;
			case 1905311443: // dayCount
			  return dayCount_Renamed;
			case -1895216418: // yieldConvention
			  return yieldConvention_Renamed;
			case 866287159: // legalEntityId
			  return legalEntityId_Renamed;
			case 135924714: // settlementDateOffset
			  return settlementDateOffset_Renamed;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 1574023291: // securityId
			  this.securityId_Renamed = (SecurityId) newValue;
			  break;
			case 1585636160: // notional
			  this.notional_Renamed = (AdjustablePayment) newValue;
			  break;
			case 1905311443: // dayCount
			  this.dayCount_Renamed = (DayCount) newValue;
			  break;
			case -1895216418: // yieldConvention
			  this.yieldConvention_Renamed = (BillYieldConvention) newValue;
			  break;
			case 866287159: // legalEntityId
			  this.legalEntityId_Renamed = (LegalEntityId) newValue;
			  break;
			case 135924714: // settlementDateOffset
			  this.settlementDateOffset_Renamed = (DaysAdjustment) newValue;
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

		public override Bill build()
		{
		  return new Bill(securityId_Renamed, notional_Renamed, dayCount_Renamed, yieldConvention_Renamed, legalEntityId_Renamed, settlementDateOffset_Renamed);
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// Sets the security identifier.
		/// <para>
		/// This identifier uniquely identifies the security within the system.
		/// </para>
		/// </summary>
		/// <param name="securityId">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder securityId(SecurityId securityId)
		{
		  JodaBeanUtils.notNull(securityId, "securityId");
		  this.securityId_Renamed = securityId;
		  return this;
		}

		/// <summary>
		/// Sets the adjustable notional payment of the bill notional, the amount must be positive. </summary>
		/// <param name="notional">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder notional(AdjustablePayment notional)
		{
		  JodaBeanUtils.notNull(notional, "notional");
		  this.notional_Renamed = notional;
		  return this;
		}

		/// <summary>
		/// Sets the day count convention applicable.
		/// <para>
		/// The conversion from dates to a numerical value is made based on this day count.
		/// </para>
		/// </summary>
		/// <param name="dayCount">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder dayCount(DayCount dayCount)
		{
		  JodaBeanUtils.notNull(dayCount, "dayCount");
		  this.dayCount_Renamed = dayCount;
		  return this;
		}

		/// <summary>
		/// Sets yield convention.
		/// <para>
		/// The convention defines how to convert from yield to price and inversely.
		/// </para>
		/// </summary>
		/// <param name="yieldConvention">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder yieldConvention(BillYieldConvention yieldConvention)
		{
		  JodaBeanUtils.notNull(yieldConvention, "yieldConvention");
		  this.yieldConvention_Renamed = yieldConvention;
		  return this;
		}

		/// <summary>
		/// Sets the legal entity identifier.
		/// <para>
		/// This identifier is used for the legal entity that issues the bill.
		/// </para>
		/// </summary>
		/// <param name="legalEntityId">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder legalEntityId(LegalEntityId legalEntityId)
		{
		  JodaBeanUtils.notNull(legalEntityId, "legalEntityId");
		  this.legalEntityId_Renamed = legalEntityId;
		  return this;
		}

		/// <summary>
		/// Sets the number of days between valuation date and settlement date.
		/// <para>
		/// It is usually one business day for US and UK bills and two days for Euroland government bills.
		/// </para>
		/// </summary>
		/// <param name="settlementDateOffset">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder settlementDateOffset(DaysAdjustment settlementDateOffset)
		{
		  JodaBeanUtils.notNull(settlementDateOffset, "settlementDateOffset");
		  this.settlementDateOffset_Renamed = settlementDateOffset;
		  return this;
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(224);
		  buf.Append("Bill.Builder{");
		  buf.Append("securityId").Append('=').Append(JodaBeanUtils.ToString(securityId_Renamed)).Append(',').Append(' ');
		  buf.Append("notional").Append('=').Append(JodaBeanUtils.ToString(notional_Renamed)).Append(',').Append(' ');
		  buf.Append("dayCount").Append('=').Append(JodaBeanUtils.ToString(dayCount_Renamed)).Append(',').Append(' ');
		  buf.Append("yieldConvention").Append('=').Append(JodaBeanUtils.ToString(yieldConvention_Renamed)).Append(',').Append(' ');
		  buf.Append("legalEntityId").Append('=').Append(JodaBeanUtils.ToString(legalEntityId_Renamed)).Append(',').Append(' ');
		  buf.Append("settlementDateOffset").Append('=').Append(JodaBeanUtils.ToString(settlementDateOffset_Renamed));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}