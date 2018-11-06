﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.capfloor
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
	/// Calibration result for Ibor caplet/floorlet volatilities.
	/// <para>
	/// This stores caplet volatilities <seealso cref="IborCapletFloorletVolatilities"/> and chi square value {@code chiSquare}. 
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class IborCapletFloorletVolatilityCalibrationResult implements org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class IborCapletFloorletVolatilityCalibrationResult : ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final IborCapletFloorletVolatilities volatilities;
		private readonly IborCapletFloorletVolatilities volatilities;
	  /// <summary>
	  /// The chi-square value.
	  /// <para>
	  /// The chi square is 0 if the volatilities are computed by root-finding. 
	  /// The chi square is generally non-zero if the volatilities are computed by least square method.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final double chiSquare;
	  private readonly double chiSquare;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance of least square result. 
	  /// </summary>
	  /// <param name="volatilities">  the caplet volatilities </param>
	  /// <param name="chiSquare">  the chi-square value </param>
	  /// <returns> the instance </returns>
	  public static IborCapletFloorletVolatilityCalibrationResult ofLeastSquare(IborCapletFloorletVolatilities volatilities, double chiSquare)
	  {

		return new IborCapletFloorletVolatilityCalibrationResult(volatilities, chiSquare);
	  }

	  /// <summary>
	  /// Obtains an instance of root-finding result. 
	  /// </summary>
	  /// <param name="volatilities">  the caplet volatilities </param>
	  /// <returns> the instance </returns>
	  public static IborCapletFloorletVolatilityCalibrationResult ofRootFind(IborCapletFloorletVolatilities volatilities)
	  {

		return new IborCapletFloorletVolatilityCalibrationResult(volatilities, 0d);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code IborCapletFloorletVolatilityCalibrationResult}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static IborCapletFloorletVolatilityCalibrationResult.Meta meta()
	  {
		return IborCapletFloorletVolatilityCalibrationResult.Meta.INSTANCE;
	  }

	  static IborCapletFloorletVolatilityCalibrationResult()
	  {
		MetaBean.register(IborCapletFloorletVolatilityCalibrationResult.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private IborCapletFloorletVolatilityCalibrationResult(IborCapletFloorletVolatilities volatilities, double chiSquare)
	  {
		JodaBeanUtils.notNull(volatilities, "volatilities");
		JodaBeanUtils.notNull(chiSquare, "chiSquare");
		this.volatilities = volatilities;
		this.chiSquare = chiSquare;
	  }

	  public override IborCapletFloorletVolatilityCalibrationResult.Meta metaBean()
	  {
		return IborCapletFloorletVolatilityCalibrationResult.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the caplet volatilities. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public IborCapletFloorletVolatilities Volatilities
	  {
		  get
		  {
			return volatilities;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the chi-square value.
	  /// <para>
	  /// The chi square is 0 if the volatilities are computed by root-finding.
	  /// The chi square is generally non-zero if the volatilities are computed by least square method.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public double ChiSquare
	  {
		  get
		  {
			return chiSquare;
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
		  IborCapletFloorletVolatilityCalibrationResult other = (IborCapletFloorletVolatilityCalibrationResult) obj;
		  return JodaBeanUtils.equal(volatilities, other.volatilities) && JodaBeanUtils.equal(chiSquare, other.chiSquare);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(volatilities);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(chiSquare);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("IborCapletFloorletVolatilityCalibrationResult{");
		buf.Append("volatilities").Append('=').Append(volatilities).Append(',').Append(' ');
		buf.Append("chiSquare").Append('=').Append(JodaBeanUtils.ToString(chiSquare));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code IborCapletFloorletVolatilityCalibrationResult}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  volatilities_Renamed = DirectMetaProperty.ofImmutable(this, "volatilities", typeof(IborCapletFloorletVolatilityCalibrationResult), typeof(IborCapletFloorletVolatilities));
			  chiSquare_Renamed = DirectMetaProperty.ofImmutable(this, "chiSquare", typeof(IborCapletFloorletVolatilityCalibrationResult), Double.TYPE);
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "volatilities", "chiSquare");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code volatilities} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<IborCapletFloorletVolatilities> volatilities_Renamed;
		/// <summary>
		/// The meta-property for the {@code chiSquare} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> chiSquare_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "volatilities", "chiSquare");
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
			case -625639549: // volatilities
			  return volatilities_Renamed;
			case -797918495: // chiSquare
			  return chiSquare_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends IborCapletFloorletVolatilityCalibrationResult> builder()
		public override BeanBuilder<IborCapletFloorletVolatilityCalibrationResult> builder()
		{
		  return new IborCapletFloorletVolatilityCalibrationResult.Builder();
		}

		public override Type beanType()
		{
		  return typeof(IborCapletFloorletVolatilityCalibrationResult);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code volatilities} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<IborCapletFloorletVolatilities> volatilities()
		{
		  return volatilities_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code chiSquare} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> chiSquare()
		{
		  return chiSquare_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -625639549: // volatilities
			  return ((IborCapletFloorletVolatilityCalibrationResult) bean).Volatilities;
			case -797918495: // chiSquare
			  return ((IborCapletFloorletVolatilityCalibrationResult) bean).ChiSquare;
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
	  /// The bean-builder for {@code IborCapletFloorletVolatilityCalibrationResult}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<IborCapletFloorletVolatilityCalibrationResult>
	  {

		internal IborCapletFloorletVolatilities volatilities;
		internal double chiSquare;

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
			case -625639549: // volatilities
			  return volatilities;
			case -797918495: // chiSquare
			  return chiSquare;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case -625639549: // volatilities
			  this.volatilities = (IborCapletFloorletVolatilities) newValue;
			  break;
			case -797918495: // chiSquare
			  this.chiSquare = (double?) newValue.Value;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override IborCapletFloorletVolatilityCalibrationResult build()
		{
		  return new IborCapletFloorletVolatilityCalibrationResult(volatilities, chiSquare);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(96);
		  buf.Append("IborCapletFloorletVolatilityCalibrationResult.Builder{");
		  buf.Append("volatilities").Append('=').Append(JodaBeanUtils.ToString(volatilities)).Append(',').Append(' ');
		  buf.Append("chiSquare").Append('=').Append(JodaBeanUtils.ToString(chiSquare));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}