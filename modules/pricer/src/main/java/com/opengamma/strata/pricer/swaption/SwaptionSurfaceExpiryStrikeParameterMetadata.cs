﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.swaption
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

	using Pair = com.opengamma.strata.collect.tuple.Pair;
	using ParameterMetadata = com.opengamma.strata.market.param.ParameterMetadata;

	/// <summary>
	/// Surface node metadata for a surface node for swaptions with a specific time to expiry and strike.
	/// <para>
	/// This typically represents a node of swaption volatility surface parameterized by expiry and strike.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class SwaptionSurfaceExpiryStrikeParameterMetadata implements com.opengamma.strata.market.param.ParameterMetadata, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class SwaptionSurfaceExpiryStrikeParameterMetadata : ParameterMetadata, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double yearFraction;
		private readonly double yearFraction;
	  /// <summary>
	  /// The strike of the surface node.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double strike;
	  private readonly double strike;
	  /// <summary>
	  /// The label that describes the node.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notEmpty", overrideGet = true) private final String label;
	  private readonly string label;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Creates node metadata using swap convention, year fraction and strike.
	  /// </summary>
	  /// <param name="yearFraction">  the year fraction </param>
	  /// <param name="strike">  the strike </param>
	  /// <returns> node metadata  </returns>
	  public static SwaptionSurfaceExpiryStrikeParameterMetadata of(double yearFraction, double strike)
	  {

		string label = Pair.of(yearFraction, strike).ToString();
		return new SwaptionSurfaceExpiryStrikeParameterMetadata(yearFraction, strike, label);
	  }

	  /// <summary>
	  /// Creates node using swap convention, year fraction, strike and label.
	  /// </summary>
	  /// <param name="yearFraction">  the year fraction </param>
	  /// <param name="strike">  the simple moneyness </param>
	  /// <param name="label">  the label to use </param>
	  /// <returns> the metadata </returns>
	  public static SwaptionSurfaceExpiryStrikeParameterMetadata of(double yearFraction, double strike, string label)
	  {

		return new SwaptionSurfaceExpiryStrikeParameterMetadata(yearFraction, strike, label);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutablePreBuild private static void preBuild(Builder builder)
	  private static void preBuild(Builder builder)
	  {
		if (string.ReferenceEquals(builder.label, null))
		{
		  builder.label = Pair.of(builder.yearFraction, builder.strike).ToString();
		}
	  }

	  public Pair<double, double> Identifier
	  {
		  get
		  {
			return Pair.of(yearFraction, strike);
		  }
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code SwaptionSurfaceExpiryStrikeParameterMetadata}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static SwaptionSurfaceExpiryStrikeParameterMetadata.Meta meta()
	  {
		return SwaptionSurfaceExpiryStrikeParameterMetadata.Meta.INSTANCE;
	  }

	  static SwaptionSurfaceExpiryStrikeParameterMetadata()
	  {
		MetaBean.register(SwaptionSurfaceExpiryStrikeParameterMetadata.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private SwaptionSurfaceExpiryStrikeParameterMetadata(double yearFraction, double strike, string label)
	  {
		JodaBeanUtils.notEmpty(label, "label");
		this.yearFraction = yearFraction;
		this.strike = strike;
		this.label = label;
	  }

	  public override SwaptionSurfaceExpiryStrikeParameterMetadata.Meta metaBean()
	  {
		return SwaptionSurfaceExpiryStrikeParameterMetadata.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the year fraction of the surface node.
	  /// <para>
	  /// This is the time to expiry that the node on the surface is defined as.
	  /// There is not necessarily a direct relationship with a date from an underlying instrument.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property </returns>
	  public double YearFraction
	  {
		  get
		  {
			return yearFraction;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the strike of the surface node. </summary>
	  /// <returns> the value of the property </returns>
	  public double Strike
	  {
		  get
		  {
			return strike;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the label that describes the node. </summary>
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
		  SwaptionSurfaceExpiryStrikeParameterMetadata other = (SwaptionSurfaceExpiryStrikeParameterMetadata) obj;
		  return JodaBeanUtils.equal(yearFraction, other.yearFraction) && JodaBeanUtils.equal(strike, other.strike) && JodaBeanUtils.equal(label, other.label);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(yearFraction);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(strike);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(label);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("SwaptionSurfaceExpiryStrikeParameterMetadata{");
		buf.Append("yearFraction").Append('=').Append(yearFraction).Append(',').Append(' ');
		buf.Append("strike").Append('=').Append(strike).Append(',').Append(' ');
		buf.Append("label").Append('=').Append(JodaBeanUtils.ToString(label));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code SwaptionSurfaceExpiryStrikeParameterMetadata}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  yearFraction_Renamed = DirectMetaProperty.ofImmutable(this, "yearFraction", typeof(SwaptionSurfaceExpiryStrikeParameterMetadata), Double.TYPE);
			  strike_Renamed = DirectMetaProperty.ofImmutable(this, "strike", typeof(SwaptionSurfaceExpiryStrikeParameterMetadata), Double.TYPE);
			  label_Renamed = DirectMetaProperty.ofImmutable(this, "label", typeof(SwaptionSurfaceExpiryStrikeParameterMetadata), typeof(string));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "yearFraction", "strike", "label");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code yearFraction} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> yearFraction_Renamed;
		/// <summary>
		/// The meta-property for the {@code strike} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> strike_Renamed;
		/// <summary>
		/// The meta-property for the {@code label} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<string> label_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "yearFraction", "strike", "label");
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
			case -1731780257: // yearFraction
			  return yearFraction_Renamed;
			case -891985998: // strike
			  return strike_Renamed;
			case 102727412: // label
			  return label_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends SwaptionSurfaceExpiryStrikeParameterMetadata> builder()
		public override BeanBuilder<SwaptionSurfaceExpiryStrikeParameterMetadata> builder()
		{
		  return new SwaptionSurfaceExpiryStrikeParameterMetadata.Builder();
		}

		public override Type beanType()
		{
		  return typeof(SwaptionSurfaceExpiryStrikeParameterMetadata);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code yearFraction} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> yearFraction()
		{
		  return yearFraction_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code strike} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> strike()
		{
		  return strike_Renamed;
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
			case -1731780257: // yearFraction
			  return ((SwaptionSurfaceExpiryStrikeParameterMetadata) bean).YearFraction;
			case -891985998: // strike
			  return ((SwaptionSurfaceExpiryStrikeParameterMetadata) bean).Strike;
			case 102727412: // label
			  return ((SwaptionSurfaceExpiryStrikeParameterMetadata) bean).Label;
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
	  /// The bean-builder for {@code SwaptionSurfaceExpiryStrikeParameterMetadata}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<SwaptionSurfaceExpiryStrikeParameterMetadata>
	  {

		internal double yearFraction;
		internal double strike;
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
			case -1731780257: // yearFraction
			  return yearFraction;
			case -891985998: // strike
			  return strike;
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
			case -1731780257: // yearFraction
			  this.yearFraction = (double?) newValue.Value;
			  break;
			case -891985998: // strike
			  this.strike = (double?) newValue.Value;
			  break;
			case 102727412: // label
			  this.label = (string) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override SwaptionSurfaceExpiryStrikeParameterMetadata build()
		{
		  preBuild(this);
		  return new SwaptionSurfaceExpiryStrikeParameterMetadata(yearFraction, strike, label);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("SwaptionSurfaceExpiryStrikeParameterMetadata.Builder{");
		  buf.Append("yearFraction").Append('=').Append(JodaBeanUtils.ToString(yearFraction)).Append(',').Append(' ');
		  buf.Append("strike").Append('=').Append(JodaBeanUtils.ToString(strike)).Append(',').Append(' ');
		  buf.Append("label").Append('=').Append(JodaBeanUtils.ToString(label));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}