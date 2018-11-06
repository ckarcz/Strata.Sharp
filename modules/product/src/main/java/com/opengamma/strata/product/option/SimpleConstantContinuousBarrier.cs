﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.option
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
	/// Continuous barrier with constant barrier level.
	/// <para>
	/// This defines a simple continuous barrier with a constant barrier level.
	/// It is assumed that the barrier event period agrees with the lifetime of the option,
	/// thus observation start date and end date are not specified in this class.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(builderScope = "private") public final class SimpleConstantContinuousBarrier implements Barrier, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class SimpleConstantContinuousBarrier : Barrier, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final BarrierType barrierType;
		private readonly BarrierType barrierType;
	  /// <summary>
	  /// The knock type.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final KnockType knockType;
	  private readonly KnockType knockType;
	  /// <summary>
	  /// The barrier level.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double barrierLevel;
	  private readonly double barrierLevel;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains the continuous barrier with constant barrier level.
	  /// </summary>
	  /// <param name="barrierType">  the barrier type </param>
	  /// <param name="knockType">  the knock type </param>
	  /// <param name="barrierLevel">  the barrier level </param>
	  /// <returns> the instance </returns>
	  public static SimpleConstantContinuousBarrier of(BarrierType barrierType, KnockType knockType, double barrierLevel)
	  {
		return new SimpleConstantContinuousBarrier(barrierType, knockType, barrierLevel);
	  }

	  //-------------------------------------------------------------------------
	  public double getBarrierLevel(LocalDate date)
	  {
		return barrierLevel;
	  }

	  public SimpleConstantContinuousBarrier inverseKnockType()
	  {
		KnockType inverse = knockType.Equals(KnockType.KNOCK_IN) ? KnockType.KNOCK_OUT : KnockType.KNOCK_IN;
		return of(barrierType, inverse, barrierLevel);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code SimpleConstantContinuousBarrier}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static SimpleConstantContinuousBarrier.Meta meta()
	  {
		return SimpleConstantContinuousBarrier.Meta.INSTANCE;
	  }

	  static SimpleConstantContinuousBarrier()
	  {
		MetaBean.register(SimpleConstantContinuousBarrier.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private SimpleConstantContinuousBarrier(BarrierType barrierType, KnockType knockType, double barrierLevel)
	  {
		JodaBeanUtils.notNull(barrierType, "barrierType");
		JodaBeanUtils.notNull(knockType, "knockType");
		this.barrierType = barrierType;
		this.knockType = knockType;
		this.barrierLevel = barrierLevel;
	  }

	  public override SimpleConstantContinuousBarrier.Meta metaBean()
	  {
		return SimpleConstantContinuousBarrier.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the barrier type. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public BarrierType BarrierType
	  {
		  get
		  {
			return barrierType;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the knock type. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public KnockType KnockType
	  {
		  get
		  {
			return knockType;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the barrier level. </summary>
	  /// <returns> the value of the property </returns>
	  public double BarrierLevel
	  {
		  get
		  {
			return barrierLevel;
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
		  SimpleConstantContinuousBarrier other = (SimpleConstantContinuousBarrier) obj;
		  return JodaBeanUtils.equal(barrierType, other.barrierType) && JodaBeanUtils.equal(knockType, other.knockType) && JodaBeanUtils.equal(barrierLevel, other.barrierLevel);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(barrierType);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(knockType);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(barrierLevel);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("SimpleConstantContinuousBarrier{");
		buf.Append("barrierType").Append('=').Append(barrierType).Append(',').Append(' ');
		buf.Append("knockType").Append('=').Append(knockType).Append(',').Append(' ');
		buf.Append("barrierLevel").Append('=').Append(JodaBeanUtils.ToString(barrierLevel));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code SimpleConstantContinuousBarrier}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  barrierType_Renamed = DirectMetaProperty.ofImmutable(this, "barrierType", typeof(SimpleConstantContinuousBarrier), typeof(BarrierType));
			  knockType_Renamed = DirectMetaProperty.ofImmutable(this, "knockType", typeof(SimpleConstantContinuousBarrier), typeof(KnockType));
			  barrierLevel_Renamed = DirectMetaProperty.ofImmutable(this, "barrierLevel", typeof(SimpleConstantContinuousBarrier), Double.TYPE);
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "barrierType", "knockType", "barrierLevel");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code barrierType} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<BarrierType> barrierType_Renamed;
		/// <summary>
		/// The meta-property for the {@code knockType} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<KnockType> knockType_Renamed;
		/// <summary>
		/// The meta-property for the {@code barrierLevel} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<double> barrierLevel_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "barrierType", "knockType", "barrierLevel");
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
			case 1029043089: // barrierType
			  return barrierType_Renamed;
			case 975895086: // knockType
			  return knockType_Renamed;
			case 1827586573: // barrierLevel
			  return barrierLevel_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public org.joda.beans.BeanBuilder<? extends SimpleConstantContinuousBarrier> builder()
		public override BeanBuilder<SimpleConstantContinuousBarrier> builder()
		{
		  return new SimpleConstantContinuousBarrier.Builder();
		}

		public override Type beanType()
		{
		  return typeof(SimpleConstantContinuousBarrier);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code barrierType} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<BarrierType> barrierType()
		{
		  return barrierType_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code knockType} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<KnockType> knockType()
		{
		  return knockType_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code barrierLevel} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<double> barrierLevel()
		{
		  return barrierLevel_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 1029043089: // barrierType
			  return ((SimpleConstantContinuousBarrier) bean).BarrierType;
			case 975895086: // knockType
			  return ((SimpleConstantContinuousBarrier) bean).KnockType;
			case 1827586573: // barrierLevel
			  return ((SimpleConstantContinuousBarrier) bean).BarrierLevel;
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
	  /// The bean-builder for {@code SimpleConstantContinuousBarrier}.
	  /// </summary>
	  private sealed class Builder : DirectPrivateBeanBuilder<SimpleConstantContinuousBarrier>
	  {

		internal BarrierType barrierType;
		internal KnockType knockType;
		internal double barrierLevel;

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
			case 1029043089: // barrierType
			  return barrierType;
			case 975895086: // knockType
			  return knockType;
			case 1827586573: // barrierLevel
			  return barrierLevel;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 1029043089: // barrierType
			  this.barrierType = (BarrierType) newValue;
			  break;
			case 975895086: // knockType
			  this.knockType = (KnockType) newValue;
			  break;
			case 1827586573: // barrierLevel
			  this.barrierLevel = (double?) newValue.Value;
			  break;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		  return this;
		}

		public override SimpleConstantContinuousBarrier build()
		{
		  return new SimpleConstantContinuousBarrier(barrierType, knockType, barrierLevel);
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("SimpleConstantContinuousBarrier.Builder{");
		  buf.Append("barrierType").Append('=').Append(JodaBeanUtils.ToString(barrierType)).Append(',').Append(' ');
		  buf.Append("knockType").Append('=').Append(JodaBeanUtils.ToString(knockType)).Append(',').Append(' ');
		  buf.Append("barrierLevel").Append('=').Append(JodaBeanUtils.ToString(barrierLevel));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}