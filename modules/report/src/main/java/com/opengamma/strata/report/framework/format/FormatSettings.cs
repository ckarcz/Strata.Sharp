﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.report.framework.format
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

	/// <summary>
	/// Contains formatting settings for a specific type.
	/// </summary>
	/// @param <T> the type of value to which the settings apply </param>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class FormatSettings<T> implements org.joda.beans.ImmutableBean
	public sealed class FormatSettings<T> : ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final FormatCategory category;
		private readonly FormatCategory category;
	  /// <summary>
	  /// The formatter to use to convert this type into a string.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final ValueFormatter<T> formatter;
	  private readonly ValueFormatter<T> formatter;

	  /// <summary>
	  /// Obtains settings from category and formatter.
	  /// </summary>
	  /// @param <T>  the type of the value </param>
	  /// <param name="category">  the category of the type </param>
	  /// <param name="formatter">  the formatter the use for the type </param>
	  /// <returns> the format settings </returns>
	  public static FormatSettings<T> of<T>(FormatCategory category, ValueFormatter<T> formatter)
	  {
		return new FormatSettings<T>(category, formatter);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code FormatSettings}. </summary>
	  /// <returns> the meta-bean, not null </returns>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("rawtypes") public static FormatSettings.Meta meta()
	  public static FormatSettings.Meta meta()
	  {
		return FormatSettings.Meta.INSTANCE;
	  }

	  /// <summary>
	  /// The meta-bean for {@code FormatSettings}. </summary>
	  /// @param <R>  the bean's generic type </param>
	  /// <param name="cls">  the bean's generic type </param>
	  /// <returns> the meta-bean, not null </returns>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") public static <R> FormatSettings.Meta<R> metaFormatSettings(Class<R> cls)
	  public static FormatSettings.Meta<R> metaFormatSettings<R>(Type<R> cls)
	  {
		return FormatSettings.Meta.INSTANCE;
	  }

	  static FormatSettings()
	  {
		MetaBean.register(FormatSettings.Meta.INSTANCE);
	  }

	  private FormatSettings(FormatCategory category, ValueFormatter<T> formatter)
	  {
		JodaBeanUtils.notNull(category, "category");
		JodaBeanUtils.notNull(formatter, "formatter");
		this.category = category;
		this.formatter = formatter;
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public FormatSettings.Meta<T> metaBean()
	  public override FormatSettings.Meta<T> metaBean()
	  {
		return FormatSettings.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the category of this type. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public FormatCategory Category
	  {
		  get
		  {
			return category;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the formatter to use to convert this type into a string. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public ValueFormatter<T> Formatter
	  {
		  get
		  {
			return formatter;
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
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: FormatSettings<?> other = (FormatSettings<?>) obj;
		  FormatSettings<object> other = (FormatSettings<object>) obj;
		  return JodaBeanUtils.equal(category, other.category) && JodaBeanUtils.equal(formatter, other.formatter);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(category);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(formatter);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("FormatSettings{");
		buf.Append("category").Append('=').Append(category).Append(',').Append(' ');
		buf.Append("formatter").Append('=').Append(JodaBeanUtils.ToString(formatter));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code FormatSettings}. </summary>
	  /// @param <T>  the type </param>
	  public sealed class Meta<T> : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  category_Renamed = DirectMetaProperty.ofImmutable(this, "category", typeof(FormatSettings), typeof(FormatCategory));
			  formatter_Renamed = DirectMetaProperty.ofImmutable(this, "formatter", typeof(FormatSettings), (Type) typeof(ValueFormatter));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "category", "formatter");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("rawtypes") static final Meta INSTANCE = new Meta();
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code category} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<FormatCategory> category_Renamed;
		/// <summary>
		/// The meta-property for the {@code formatter} property.
		/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings({"unchecked", "rawtypes" }) private final org.joda.beans.MetaProperty<ValueFormatter<T>> formatter = org.joda.beans.impl.direct.DirectMetaProperty.ofImmutable(this, "formatter", FormatSettings.class, (Class) ValueFormatter.class);
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<ValueFormatter<T>> formatter_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "category", "formatter");
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
			case 50511102: // category
			  return category_Renamed;
			case 1811591370: // formatter
			  return formatter_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends FormatSettings<T>> builder()
		public override BeanBuilder<FormatSettings<T>> builder()
		{
		  return new FormatSettings.Builder<>();
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings({"unchecked", "rawtypes" }) @Override public Class beanType()
		public override Type beanType()
		{
		  return (Type) typeof(FormatSettings);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code category} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<FormatCategory> category()
		{
		  return category_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code formatter} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<ValueFormatter<T>> formatter()
		{
		  return formatter_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 50511102: // category
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: return ((FormatSettings<?>) bean).getCategory();
			  return ((FormatSettings<object>) bean).Category;
			case 1811591370: // formatter
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: return ((FormatSettings<?>) bean).getFormatter();
			  return ((FormatSettings<object>) bean).Formatter;
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
	  /// The bean-builder for {@code FormatSettings}. </summary>
	  /// @param <T>  the type </param>
	  private sealed class Builder<T> : DirectPrivateBeanBuilder<FormatSettings<T>>
	  {

		internal FormatCategory category;
		internal ValueFormatter<T> formatter;

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
			case 50511102: // category
			  return category;
			case 1811591370: // formatter
			  return formatter;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public Builder<T> set(String propertyName, Object newValue)
		public override Builder<T> set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 50511102: // category
			  this.category = (FormatCategory) newValue;
			  break;
			case 1811591370: // formatter
			  this.formatter = (ValueFormatter<T>) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override FormatSettings<T> build()
		{
		  return new FormatSettings<T>(category, formatter);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(96);
		  buf.Append("FormatSettings.Builder{");
		  buf.Append("category").Append('=').Append(JodaBeanUtils.ToString(category)).Append(',').Append(' ');
		  buf.Append("formatter").Append('=').Append(JodaBeanUtils.ToString(formatter));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}