﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.calc
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

	using Currency = com.opengamma.strata.basics.currency.Currency;

	/// <summary>
	/// Provides access to the column name and measure in the grid of results.
	/// <para>
	/// <seealso cref="CalculationRunner"/> provides the ability to calculate a grid of results
	/// for a given set targets and columns. This defines the columns in the results.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class ColumnHeader implements org.joda.beans.ImmutableBean
	public sealed class ColumnHeader : ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final ColumnName name;
		private readonly ColumnName name;
	  /// <summary>
	  /// The measure that was calculated.
	  /// <para>
	  /// This defines the calculation that was performed, such as 'PresentValue' or 'ParRate'.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final Measure measure;
	  private readonly Measure measure;
	  /// <summary>
	  /// The currency of the result.
	  /// <para>
	  /// If the measure can be <seealso cref="Measure#isCurrencyConvertible() automatically converted"/>
	  /// to a different currency, and a specific <seealso cref="ReportingCurrency"/> was specified,
	  /// then the currency will be stored here.
	  /// If the reporting currency is "natural", or the result has no currency, then this will be empty.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(get = "optional") private final com.opengamma.strata.basics.currency.Currency currency;
	  private readonly Currency currency;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance from the name and measure.
	  /// </summary>
	  /// <param name="name">  the name </param>
	  /// <param name="measure">  the measure </param>
	  /// <returns> a column with the specified measure </returns>
	  public static ColumnHeader of(ColumnName name, Measure measure)
	  {
		return new ColumnHeader(name, measure, null);
	  }

	  /// <summary>
	  /// Obtains an instance from the name, measure and currency.
	  /// </summary>
	  /// <param name="name">  the name </param>
	  /// <param name="measure">  the measure </param>
	  /// <param name="currency">  the currency </param>
	  /// <returns> a column with the specified measure </returns>
	  public static ColumnHeader of(ColumnName name, Measure measure, Currency currency)
	  {
		return new ColumnHeader(name, measure, currency);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code ColumnHeader}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static ColumnHeader.Meta meta()
	  {
		return ColumnHeader.Meta.INSTANCE;
	  }

	  static ColumnHeader()
	  {
		MetaBean.register(ColumnHeader.Meta.INSTANCE);
	  }

	  private ColumnHeader(ColumnName name, Measure measure, Currency currency)
	  {
		JodaBeanUtils.notNull(name, "name");
		JodaBeanUtils.notNull(measure, "measure");
		this.name = name;
		this.measure = measure;
		this.currency = currency;
	  }

	  public override ColumnHeader.Meta metaBean()
	  {
		return ColumnHeader.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the column name.
	  /// <para>
	  /// This is the name of the column, and should be unique in a list of columns.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public ColumnName Name
	  {
		  get
		  {
			return name;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the measure that was calculated.
	  /// <para>
	  /// This defines the calculation that was performed, such as 'PresentValue' or 'ParRate'.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public Measure Measure
	  {
		  get
		  {
			return measure;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the currency of the result.
	  /// <para>
	  /// If the measure can be <seealso cref="Measure#isCurrencyConvertible() automatically converted"/>
	  /// to a different currency, and a specific <seealso cref="ReportingCurrency"/> was specified,
	  /// then the currency will be stored here.
	  /// If the reporting currency is "natural", or the result has no currency, then this will be empty.
	  /// </para>
	  /// </summary>
	  /// <returns> the optional value of the property, not null </returns>
	  public Optional<Currency> Currency
	  {
		  get
		  {
			return Optional.ofNullable(currency);
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
		  ColumnHeader other = (ColumnHeader) obj;
		  return JodaBeanUtils.equal(name, other.name) && JodaBeanUtils.equal(measure, other.measure) && JodaBeanUtils.equal(currency, other.currency);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(name);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(measure);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(currency);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("ColumnHeader{");
		buf.Append("name").Append('=').Append(name).Append(',').Append(' ');
		buf.Append("measure").Append('=').Append(measure).Append(',').Append(' ');
		buf.Append("currency").Append('=').Append(JodaBeanUtils.ToString(currency));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code ColumnHeader}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  name_Renamed = DirectMetaProperty.ofImmutable(this, "name", typeof(ColumnHeader), typeof(ColumnName));
			  measure_Renamed = DirectMetaProperty.ofImmutable(this, "measure", typeof(ColumnHeader), typeof(Measure));
			  currency_Renamed = DirectMetaProperty.ofImmutable(this, "currency", typeof(ColumnHeader), typeof(Currency));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "name", "measure", "currency");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code name} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<ColumnName> name_Renamed;
		/// <summary>
		/// The meta-property for the {@code measure} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<Measure> measure_Renamed;
		/// <summary>
		/// The meta-property for the {@code currency} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<Currency> currency_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "name", "measure", "currency");
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
			case 3373707: // name
			  return name_Renamed;
			case 938321246: // measure
			  return measure_Renamed;
			case 575402001: // currency
			  return currency_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends ColumnHeader> builder()
		public override BeanBuilder<ColumnHeader> builder()
		{
		  return new ColumnHeader.Builder();
		}

		public override Type beanType()
		{
		  return typeof(ColumnHeader);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code name} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<ColumnName> name()
		{
		  return name_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code measure} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<Measure> measure()
		{
		  return measure_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code currency} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<Currency> currency()
		{
		  return currency_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 3373707: // name
			  return ((ColumnHeader) bean).Name;
			case 938321246: // measure
			  return ((ColumnHeader) bean).Measure;
			case 575402001: // currency
			  return ((ColumnHeader) bean).currency;
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
	  /// The bean-builder for {@code ColumnHeader}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<ColumnHeader>
	  {

		internal ColumnName name;
		internal Measure measure;
		internal Currency currency;

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
			case 3373707: // name
			  return name;
			case 938321246: // measure
			  return measure;
			case 575402001: // currency
			  return currency;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 3373707: // name
			  this.name = (ColumnName) newValue;
			  break;
			case 938321246: // measure
			  this.measure = (Measure) newValue;
			  break;
			case 575402001: // currency
			  this.currency = (Currency) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override ColumnHeader build()
		{
		  return new ColumnHeader(name, measure, currency);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("ColumnHeader.Builder{");
		  buf.Append("name").Append('=').Append(JodaBeanUtils.ToString(name)).Append(',').Append(' ');
		  buf.Append("measure").Append('=').Append(JodaBeanUtils.ToString(measure)).Append(',').Append(' ');
		  buf.Append("currency").Append('=').Append(JodaBeanUtils.ToString(currency));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}