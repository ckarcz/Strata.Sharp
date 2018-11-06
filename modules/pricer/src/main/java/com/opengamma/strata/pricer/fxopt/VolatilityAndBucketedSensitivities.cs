﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2012 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.fxopt
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

	using DoubleMatrix = com.opengamma.strata.collect.array.DoubleMatrix;

	/// <summary>
	/// Combines information about a volatility and its sensitivities.
	/// <para>
	/// This contains a volatility calculated at a particular x and y and the bucketed sensitivities
	/// of this value to the volatility data points that were used to construct the surface.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class VolatilityAndBucketedSensitivities implements org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class VolatilityAndBucketedSensitivities : ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double volatility;
		private readonly double volatility;
	  /// <summary>
	  /// The sensitivities.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.collect.array.DoubleMatrix sensitivities;
	  private readonly DoubleMatrix sensitivities;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance.
	  /// </summary>
	  /// <param name="volatility">  the volatility </param>
	  /// <param name="sensitivities">  the bucketed sensitivities </param>
	  /// <returns> the volatility and sensitivities </returns>
	  public static VolatilityAndBucketedSensitivities of(double volatility, DoubleMatrix sensitivities)
	  {
		return new VolatilityAndBucketedSensitivities(volatility, sensitivities);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code VolatilityAndBucketedSensitivities}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static VolatilityAndBucketedSensitivities.Meta meta()
	  {
		return VolatilityAndBucketedSensitivities.Meta.INSTANCE;
	  }

	  static VolatilityAndBucketedSensitivities()
	  {
		MetaBean.register(VolatilityAndBucketedSensitivities.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private VolatilityAndBucketedSensitivities(double volatility, DoubleMatrix sensitivities)
	  {
		JodaBeanUtils.notNull(sensitivities, "sensitivities");
		this.volatility = volatility;
		this.sensitivities = sensitivities;
	  }

	  public override VolatilityAndBucketedSensitivities.Meta metaBean()
	  {
		return VolatilityAndBucketedSensitivities.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the volatility. </summary>
	  /// <returns> the value of the property </returns>
	  public double Volatility
	  {
		  get
		  {
			return volatility;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the sensitivities. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public DoubleMatrix Sensitivities
	  {
		  get
		  {
			return sensitivities;
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
		  VolatilityAndBucketedSensitivities other = (VolatilityAndBucketedSensitivities) obj;
		  return JodaBeanUtils.equal(volatility, other.volatility) && JodaBeanUtils.equal(sensitivities, other.sensitivities);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(volatility);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(sensitivities);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("VolatilityAndBucketedSensitivities{");
		buf.Append("volatility").Append('=').Append(volatility).Append(',').Append(' ');
		buf.Append("sensitivities").Append('=').Append(JodaBeanUtils.ToString(sensitivities));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code VolatilityAndBucketedSensitivities}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  volatility_Renamed = DirectMetaProperty.ofImmutable(this, "volatility", typeof(VolatilityAndBucketedSensitivities), Double.TYPE);
			  sensitivities_Renamed = DirectMetaProperty.ofImmutable(this, "sensitivities", typeof(VolatilityAndBucketedSensitivities), typeof(DoubleMatrix));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "volatility", "sensitivities");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code volatility} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> volatility_Renamed;
		/// <summary>
		/// The meta-property for the {@code sensitivities} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<DoubleMatrix> sensitivities_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "volatility", "sensitivities");
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
			case -1917967323: // volatility
			  return volatility_Renamed;
			case 1226228605: // sensitivities
			  return sensitivities_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends VolatilityAndBucketedSensitivities> builder()
		public override BeanBuilder<VolatilityAndBucketedSensitivities> builder()
		{
		  return new VolatilityAndBucketedSensitivities.Builder();
		}

		public override Type beanType()
		{
		  return typeof(VolatilityAndBucketedSensitivities);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code volatility} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> volatility()
		{
		  return volatility_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code sensitivities} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<DoubleMatrix> sensitivities()
		{
		  return sensitivities_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -1917967323: // volatility
			  return ((VolatilityAndBucketedSensitivities) bean).Volatility;
			case 1226228605: // sensitivities
			  return ((VolatilityAndBucketedSensitivities) bean).Sensitivities;
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
	  /// The bean-builder for {@code VolatilityAndBucketedSensitivities}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<VolatilityAndBucketedSensitivities>
	  {

		internal double volatility;
		internal DoubleMatrix sensitivities;

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
			case -1917967323: // volatility
			  return volatility;
			case 1226228605: // sensitivities
			  return sensitivities;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -1917967323: // volatility
			  this.volatility = (double?) newValue.Value;
			  break;
			case 1226228605: // sensitivities
			  this.sensitivities = (DoubleMatrix) newValue;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override VolatilityAndBucketedSensitivities build()
		{
		  return new VolatilityAndBucketedSensitivities(volatility, sensitivities);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(96);
		  buf.Append("VolatilityAndBucketedSensitivities.Builder{");
		  buf.Append("volatility").Append('=').Append(JodaBeanUtils.ToString(volatility)).Append(',').Append(' ');
		  buf.Append("sensitivities").Append('=').Append(JodaBeanUtils.ToString(sensitivities));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}