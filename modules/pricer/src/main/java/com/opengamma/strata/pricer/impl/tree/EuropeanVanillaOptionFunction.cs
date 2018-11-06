﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.impl.tree
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

	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using PutCall = com.opengamma.strata.product.common.PutCall;

	/// <summary>
	/// European vanilla option function.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class EuropeanVanillaOptionFunction implements OptionFunction, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class EuropeanVanillaOptionFunction : OptionFunction, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double strike;
		private readonly double strike;
	  /// <summary>
	  /// The time to expiry.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(overrideGet = true) private final double timeToExpiry;
	  private readonly double timeToExpiry;
	  /// <summary>
	  /// The sign.
	  /// <para>
	  /// The sign is +1 for call and -1 for put.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double sign;
	  private readonly double sign;

	  /// <summary>
	  /// The number of time steps.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(overrideGet = true) private final int numberOfSteps;
	  private readonly int numberOfSteps;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance.
	  /// </summary>
	  /// <param name="strike">  the strike </param>
	  /// <param name="timeToExpiry">  the time to expiry </param>
	  /// <param name="putCall">  put or call </param>
	  /// <param name="numberOfSteps">  the number of time steps </param>
	  /// <returns> the instance </returns>
	  public static EuropeanVanillaOptionFunction of(double strike, double timeToExpiry, PutCall putCall, int numberOfSteps)
	  {
		double sign = putCall.Call ? 1d : -1d;
		ArgChecker.isTrue(numberOfSteps > 0, "the number of steps should be positive");
		return new EuropeanVanillaOptionFunction(strike, timeToExpiry, sign, numberOfSteps);
	  }

	  //-------------------------------------------------------------------------
	  public DoubleArray getPayoffAtExpiryTrinomial(DoubleArray stateValue)
	  {
		int nNodes = stateValue.size();
		double[] values = new double[nNodes];
		for (int i = 0; i < nNodes; ++i)
		{
		  values[i] = Math.Max(sign * (stateValue.get(i) - strike), 0d);
		}
		return DoubleArray.ofUnsafe(values);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code EuropeanVanillaOptionFunction}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static EuropeanVanillaOptionFunction.Meta meta()
	  {
		return EuropeanVanillaOptionFunction.Meta.INSTANCE;
	  }

	  static EuropeanVanillaOptionFunction()
	  {
		MetaBean.register(EuropeanVanillaOptionFunction.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private EuropeanVanillaOptionFunction(double strike, double timeToExpiry, double sign, int numberOfSteps)
	  {
		this.strike = strike;
		this.timeToExpiry = timeToExpiry;
		this.sign = sign;
		this.numberOfSteps = numberOfSteps;
	  }

	  public override EuropeanVanillaOptionFunction.Meta metaBean()
	  {
		return EuropeanVanillaOptionFunction.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the strike value. </summary>
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
	  /// Gets the time to expiry. </summary>
	  /// <returns> the value of the property </returns>
	  public double TimeToExpiry
	  {
		  get
		  {
			return timeToExpiry;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the sign.
	  /// <para>
	  /// The sign is +1 for call and -1 for put.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property </returns>
	  public double Sign
	  {
		  get
		  {
			return sign;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the number of time steps. </summary>
	  /// <returns> the value of the property </returns>
	  public int NumberOfSteps
	  {
		  get
		  {
			return numberOfSteps;
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
		  EuropeanVanillaOptionFunction other = (EuropeanVanillaOptionFunction) obj;
		  return JodaBeanUtils.equal(strike, other.strike) && JodaBeanUtils.equal(timeToExpiry, other.timeToExpiry) && JodaBeanUtils.equal(sign, other.sign) && (numberOfSteps == other.numberOfSteps);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(strike);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(timeToExpiry);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(sign);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(numberOfSteps);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(160);
		buf.Append("EuropeanVanillaOptionFunction{");
		buf.Append("strike").Append('=').Append(strike).Append(',').Append(' ');
		buf.Append("timeToExpiry").Append('=').Append(timeToExpiry).Append(',').Append(' ');
		buf.Append("sign").Append('=').Append(sign).Append(',').Append(' ');
		buf.Append("numberOfSteps").Append('=').Append(JodaBeanUtils.ToString(numberOfSteps));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code EuropeanVanillaOptionFunction}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  strike_Renamed = DirectMetaProperty.ofImmutable(this, "strike", typeof(EuropeanVanillaOptionFunction), Double.TYPE);
			  timeToExpiry_Renamed = DirectMetaProperty.ofImmutable(this, "timeToExpiry", typeof(EuropeanVanillaOptionFunction), Double.TYPE);
			  sign_Renamed = DirectMetaProperty.ofImmutable(this, "sign", typeof(EuropeanVanillaOptionFunction), Double.TYPE);
			  numberOfSteps_Renamed = DirectMetaProperty.ofImmutable(this, "numberOfSteps", typeof(EuropeanVanillaOptionFunction), Integer.TYPE);
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "strike", "timeToExpiry", "sign", "numberOfSteps");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code strike} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> strike_Renamed;
		/// <summary>
		/// The meta-property for the {@code timeToExpiry} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> timeToExpiry_Renamed;
		/// <summary>
		/// The meta-property for the {@code sign} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> sign_Renamed;
		/// <summary>
		/// The meta-property for the {@code numberOfSteps} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<int> numberOfSteps_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "strike", "timeToExpiry", "sign", "numberOfSteps");
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
			case -891985998: // strike
			  return strike_Renamed;
			case -1831499397: // timeToExpiry
			  return timeToExpiry_Renamed;
			case 3530173: // sign
			  return sign_Renamed;
			case -1323103225: // numberOfSteps
			  return numberOfSteps_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends EuropeanVanillaOptionFunction> builder()
		public override BeanBuilder<EuropeanVanillaOptionFunction> builder()
		{
		  return new EuropeanVanillaOptionFunction.Builder();
		}

		public override Type beanType()
		{
		  return typeof(EuropeanVanillaOptionFunction);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code strike} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> strike()
		{
		  return strike_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code timeToExpiry} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> timeToExpiry()
		{
		  return timeToExpiry_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code sign} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> sign()
		{
		  return sign_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code numberOfSteps} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<int> numberOfSteps()
		{
		  return numberOfSteps_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -891985998: // strike
			  return ((EuropeanVanillaOptionFunction) bean).Strike;
			case -1831499397: // timeToExpiry
			  return ((EuropeanVanillaOptionFunction) bean).TimeToExpiry;
			case 3530173: // sign
			  return ((EuropeanVanillaOptionFunction) bean).Sign;
			case -1323103225: // numberOfSteps
			  return ((EuropeanVanillaOptionFunction) bean).NumberOfSteps;
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
	  /// The bean-builder for {@code EuropeanVanillaOptionFunction}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<EuropeanVanillaOptionFunction>
	  {

		internal double strike;
		internal double timeToExpiry;
		internal double sign;
		internal int numberOfSteps;

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
			case -891985998: // strike
			  return strike;
			case -1831499397: // timeToExpiry
			  return timeToExpiry;
			case 3530173: // sign
			  return sign;
			case -1323103225: // numberOfSteps
			  return numberOfSteps;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -891985998: // strike
			  this.strike = (double?) newValue.Value;
			  break;
			case -1831499397: // timeToExpiry
			  this.timeToExpiry = (double?) newValue.Value;
			  break;
			case 3530173: // sign
			  this.sign = (double?) newValue.Value;
			  break;
			case -1323103225: // numberOfSteps
			  this.numberOfSteps = (int?) newValue.Value;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override EuropeanVanillaOptionFunction build()
		{
		  return new EuropeanVanillaOptionFunction(strike, timeToExpiry, sign, numberOfSteps);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(160);
		  buf.Append("EuropeanVanillaOptionFunction.Builder{");
		  buf.Append("strike").Append('=').Append(JodaBeanUtils.ToString(strike)).Append(',').Append(' ');
		  buf.Append("timeToExpiry").Append('=').Append(JodaBeanUtils.ToString(timeToExpiry)).Append(',').Append(' ');
		  buf.Append("sign").Append('=').Append(JodaBeanUtils.ToString(sign)).Append(',').Append(' ');
		  buf.Append("numberOfSteps").Append('=').Append(JodaBeanUtils.ToString(numberOfSteps));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}