﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.market.param
{

	using Bean = org.joda.beans.Bean;
	using BeanBuilder = org.joda.beans.BeanBuilder;
	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using MetaProperty = org.joda.beans.MetaProperty;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using ImmutablePreBuild = org.joda.beans.gen.ImmutablePreBuild;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using DirectMetaBean = org.joda.beans.impl.direct.DirectMetaBean;
	using DirectMetaProperty = org.joda.beans.impl.direct.DirectMetaProperty;
	using DirectMetaPropertyMap = org.joda.beans.impl.direct.DirectMetaPropertyMap;
	using DirectPrivateBeanBuilder = org.joda.beans.impl.direct.DirectPrivateBeanBuilder;

	using ArgChecker = com.opengamma.strata.collect.ArgChecker;

	/// <summary>
	/// Parameter metadata based on a date and year-month.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class YearMonthDateParameterMetadata implements DatedParameterMetadata, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class YearMonthDateParameterMetadata : DatedParameterMetadata, ImmutableBean
	{

	  /// <summary>
	  /// Formatter for Jan15.
	  /// </summary>
	  private static readonly DateTimeFormatter FORMATTER = DateTimeFormatter.ofPattern("MMMuu", Locale.ENGLISH);

	  /// <summary>
	  /// The date associated with the parameter.
	  /// <para>
	  /// This is the date that is most closely associated with the parameter.
	  /// The actual parameter is typically a year fraction based on a day count.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final java.time.LocalDate date;
	  private readonly LocalDate date;
	  /// <summary>
	  /// The year-month associated with the parameter.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final java.time.YearMonth yearMonth;
	  private readonly YearMonth yearMonth;
	  /// <summary>
	  /// The label that describes the parameter, defaulted to the year-month.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notEmpty", overrideGet = true) private final String label;
	  private readonly string label;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance using the year-month.
	  /// </summary>
	  /// <param name="date">  the date associated with the parameter </param>
	  /// <param name="yearMonth">  the year-month of the curve node </param>
	  /// <returns> the parameter metadata based on the year-month </returns>
	  public static YearMonthDateParameterMetadata of(LocalDate date, YearMonth yearMonth)
	  {
		ArgChecker.notNull(date, "date");
		ArgChecker.notNull(yearMonth, "yearMonth");
		return new YearMonthDateParameterMetadata(date, yearMonth, yearMonth.format(FORMATTER));
	  }

	  /// <summary>
	  /// Obtains an instance using the year-month, specifying the label.
	  /// </summary>
	  /// <param name="date">  the date associated with the parameter </param>
	  /// <param name="yearMonth">  the year-month of the curve node </param>
	  /// <param name="label">  the label to use </param>
	  /// <returns> the parameter metadata based on the year-month </returns>
	  public static YearMonthDateParameterMetadata of(LocalDate date, YearMonth yearMonth, string label)
	  {
		return new YearMonthDateParameterMetadata(date, yearMonth, label);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutablePreBuild private static void preBuild(Builder builder)
	  private static void preBuild(Builder builder)
	  {
		if (string.ReferenceEquals(builder.label, null) && builder.yearMonth != null)
		{
		  builder.label = builder.yearMonth.format(FORMATTER);
		}
	  }

	  /// <summary>
	  /// Gets the identifier, which is the year-month.
	  /// </summary>
	  /// <returns> the year-month </returns>
	  public YearMonth Identifier
	  {
		  get
		  {
			return yearMonth;
		  }
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code YearMonthDateParameterMetadata}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static YearMonthDateParameterMetadata.Meta meta()
	  {
		return YearMonthDateParameterMetadata.Meta.INSTANCE;
	  }

	  static YearMonthDateParameterMetadata()
	  {
		MetaBean.register(YearMonthDateParameterMetadata.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private YearMonthDateParameterMetadata(LocalDate date, YearMonth yearMonth, string label)
	  {
		JodaBeanUtils.notNull(date, "date");
		JodaBeanUtils.notNull(yearMonth, "yearMonth");
		JodaBeanUtils.notEmpty(label, "label");
		this.date = date;
		this.yearMonth = yearMonth;
		this.label = label;
	  }

	  public override YearMonthDateParameterMetadata.Meta metaBean()
	  {
		return YearMonthDateParameterMetadata.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the date associated with the parameter.
	  /// <para>
	  /// This is the date that is most closely associated with the parameter.
	  /// The actual parameter is typically a year fraction based on a day count.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public LocalDate Date
	  {
		  get
		  {
			return date;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the year-month associated with the parameter. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public YearMonth YearMonth
	  {
		  get
		  {
			return yearMonth;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the label that describes the parameter, defaulted to the year-month. </summary>
	  /// <returns> the value of the property, not empty </returns>
	  public string Label
	  {
		  get
		  {
			return label;
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
		  YearMonthDateParameterMetadata other = (YearMonthDateParameterMetadata) obj;
		  return JodaBeanUtils.equal(date, other.date) && JodaBeanUtils.equal(yearMonth, other.yearMonth) && JodaBeanUtils.equal(label, other.label);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(date);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(yearMonth);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(label);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("YearMonthDateParameterMetadata{");
		buf.Append("date").Append('=').Append(date).Append(',').Append(' ');
		buf.Append("yearMonth").Append('=').Append(yearMonth).Append(',').Append(' ');
		buf.Append("label").Append('=').Append(JodaBeanUtils.ToString(label));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code YearMonthDateParameterMetadata}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  date_Renamed = DirectMetaProperty.ofImmutable(this, "date", typeof(YearMonthDateParameterMetadata), typeof(LocalDate));
			  yearMonth_Renamed = DirectMetaProperty.ofImmutable(this, "yearMonth", typeof(YearMonthDateParameterMetadata), typeof(YearMonth));
			  label_Renamed = DirectMetaProperty.ofImmutable(this, "label", typeof(YearMonthDateParameterMetadata), typeof(string));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "date", "yearMonth", "label");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code date} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<LocalDate> date_Renamed;
		/// <summary>
		/// The meta-property for the {@code yearMonth} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<YearMonth> yearMonth_Renamed;
		/// <summary>
		/// The meta-property for the {@code label} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<string> label_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "date", "yearMonth", "label");
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
			case 3076014: // date
			  return date_Renamed;
			case -496678845: // yearMonth
			  return yearMonth_Renamed;
			case 102727412: // label
			  return label_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends YearMonthDateParameterMetadata> builder()
		public override BeanBuilder<YearMonthDateParameterMetadata> builder()
		{
		  return new YearMonthDateParameterMetadata.Builder();
		}

		public override Type beanType()
		{
		  return typeof(YearMonthDateParameterMetadata);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code date} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<LocalDate> date()
		{
		  return date_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code yearMonth} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<YearMonth> yearMonth()
		{
		  return yearMonth_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code label} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<string> label()
		{
		  return label_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 3076014: // date
			  return ((YearMonthDateParameterMetadata) bean).Date;
			case -496678845: // yearMonth
			  return ((YearMonthDateParameterMetadata) bean).YearMonth;
			case 102727412: // label
			  return ((YearMonthDateParameterMetadata) bean).Label;
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
	  /// The bean-builder for {@code YearMonthDateParameterMetadata}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<YearMonthDateParameterMetadata>
	  {

		internal LocalDate date;
		internal YearMonth yearMonth;
		internal string label;

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
			case 3076014: // date
			  return date;
			case -496678845: // yearMonth
			  return yearMonth;
			case 102727412: // label
			  return label;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 3076014: // date
			  this.date = (LocalDate) newValue;
			  break;
			case -496678845: // yearMonth
			  this.yearMonth = (YearMonth) newValue;
			  break;
			case 102727412: // label
			  this.label = (string) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override YearMonthDateParameterMetadata build()
		{
		  preBuild(this);
		  return new YearMonthDateParameterMetadata(date, yearMonth, label);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("YearMonthDateParameterMetadata.Builder{");
		  buf.Append("date").Append('=').Append(JodaBeanUtils.ToString(date)).Append(',').Append(' ');
		  buf.Append("yearMonth").Append('=').Append(JodaBeanUtils.ToString(yearMonth)).Append(',').Append(' ');
		  buf.Append("label").Append('=').Append(JodaBeanUtils.ToString(label));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}