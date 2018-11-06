﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.credit
{

	using Bean = org.joda.beans.Bean;
	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using MetaProperty = org.joda.beans.MetaProperty;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using ImmutableDefaults = org.joda.beans.gen.ImmutableDefaults;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using DirectFieldsBeanBuilder = org.joda.beans.impl.direct.DirectFieldsBeanBuilder;
	using DirectMetaBean = org.joda.beans.impl.direct.DirectMetaBean;
	using DirectMetaProperty = org.joda.beans.impl.direct.DirectMetaProperty;
	using DirectMetaPropertyMap = org.joda.beans.impl.direct.DirectMetaPropertyMap;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using Payment = com.opengamma.strata.basics.currency.Payment;

	/// <summary>
	/// A trade in a single-name credit default swap (CDS), resolved for pricing.
	/// <para>
	/// This is the resolved form of <seealso cref="CdsTrade"/> and is the primary input to the pricers.
	/// Applications will typically create a {@code ResolvedCdsTrade} from a {@code CdsTrade}
	/// using <seealso cref="CdsTrade#resolve(ReferenceData)"/>.
	/// </para>
	/// <para>
	/// A {@code ResolvedCdsTrade} is bound to data that changes over time, such as holiday calendars.
	/// If the data changes, such as the addition of a new holiday, the resolved form will not be updated.
	/// Care must be taken when placing the resolved form in a cache or persistence layer.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition public final class ResolvedCdsTrade implements com.opengamma.strata.product.ResolvedTrade, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class ResolvedCdsTrade : ResolvedTrade, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final com.opengamma.strata.product.TradeInfo info;
		private readonly TradeInfo info;
	  /// <summary>
	  /// The resolved CDS product.
	  /// <para>
	  /// The product captures the contracted financial details of the trade.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final ResolvedCds product;
	  private readonly ResolvedCds product;
	  /// <summary>
	  /// The upfront fee of the product.
	  /// <para>
	  /// This specifies a single amount payable by the buyer to the seller
	  /// Thus the sign must be compatible with the product Pay/Receive flag.
	  /// </para>
	  /// <para>
	  /// Some CDSs, especially legacy products, are traded at par and the upfront fee is not paid.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(get = "optional") private final com.opengamma.strata.basics.currency.Payment upfrontFee;
	  private readonly Payment upfrontFee;

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutableDefaults private static void applyDefaults(Builder builder)
	  private static void applyDefaults(Builder builder)
	  {
		builder.info_Renamed = TradeInfo.empty();
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code ResolvedCdsTrade}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static ResolvedCdsTrade.Meta meta()
	  {
		return ResolvedCdsTrade.Meta.INSTANCE;
	  }

	  static ResolvedCdsTrade()
	  {
		MetaBean.register(ResolvedCdsTrade.Meta.INSTANCE);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  /// <summary>
	  /// Returns a builder used to create an instance of the bean. </summary>
	  /// <returns> the builder, not null </returns>
	  public static ResolvedCdsTrade.Builder builder()
	  {
		return new ResolvedCdsTrade.Builder();
	  }

	  private ResolvedCdsTrade(TradeInfo info, ResolvedCds product, Payment upfrontFee)
	  {
		JodaBeanUtils.notNull(info, "info");
		JodaBeanUtils.notNull(product, "product");
		this.info = info;
		this.product = product;
		this.upfrontFee = upfrontFee;
	  }

	  public override ResolvedCdsTrade.Meta metaBean()
	  {
		return ResolvedCdsTrade.Meta.INSTANCE;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the additional trade information, defaulted to an empty instance.
	  /// <para>
	  /// This allows additional information to be attached to the trade.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public TradeInfo Info
	  {
		  get
		  {
			return info;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the resolved CDS product.
	  /// <para>
	  /// The product captures the contracted financial details of the trade.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public ResolvedCds Product
	  {
		  get
		  {
			return product;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the upfront fee of the product.
	  /// <para>
	  /// This specifies a single amount payable by the buyer to the seller
	  /// Thus the sign must be compatible with the product Pay/Receive flag.
	  /// </para>
	  /// <para>
	  /// Some CDSs, especially legacy products, are traded at par and the upfront fee is not paid.
	  /// </para>
	  /// </summary>
	  /// <returns> the optional value of the property, not null </returns>
	  public Optional<Payment> UpfrontFee
	  {
		  get
		  {
			return Optional.ofNullable(upfrontFee);
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
		  ResolvedCdsTrade other = (ResolvedCdsTrade) obj;
		  return JodaBeanUtils.equal(info, other.info) && JodaBeanUtils.equal(product, other.product) && JodaBeanUtils.equal(upfrontFee, other.upfrontFee);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(info);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(product);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(upfrontFee);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(128);
		buf.Append("ResolvedCdsTrade{");
		buf.Append("info").Append('=').Append(info).Append(',').Append(' ');
		buf.Append("product").Append('=').Append(product).Append(',').Append(' ');
		buf.Append("upfrontFee").Append('=').Append(JodaBeanUtils.ToString(upfrontFee));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// The meta-bean for {@code ResolvedCdsTrade}.
	  /// </summary>
	  public sealed class Meta : DirectMetaBean
	  {
		  internal bool InstanceFieldsInitialized = false;

		  internal void InitializeInstanceFields()
		  {
			  info_Renamed = DirectMetaProperty.ofImmutable(this, "info", typeof(ResolvedCdsTrade), typeof(TradeInfo));
			  product_Renamed = DirectMetaProperty.ofImmutable(this, "product", typeof(ResolvedCdsTrade), typeof(ResolvedCds));
			  upfrontFee_Renamed = DirectMetaProperty.ofImmutable(this, "upfrontFee", typeof(ResolvedCdsTrade), typeof(Payment));
			  metaPropertyMap$ = new DirectMetaPropertyMap(this, null, "info", "product", "upfrontFee");
		  }

		/// <summary>
		/// The singleton instance of the meta-bean.
		/// </summary>
		internal static readonly Meta INSTANCE = new Meta();

		/// <summary>
		/// The meta-property for the {@code info} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<TradeInfo> info_Renamed;
		/// <summary>
		/// The meta-property for the {@code product} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<ResolvedCds> product_Renamed;
		/// <summary>
		/// The meta-property for the {@code upfrontFee} property.
		/// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal MetaProperty<Payment> upfrontFee_Renamed;
		/// <summary>
		/// The meta-properties.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private final java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap$ = new org.joda.beans.impl.direct.DirectMetaPropertyMap(this, null, "info", "product", "upfrontFee");
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
			case 3237038: // info
			  return info_Renamed;
			case -309474065: // product
			  return product_Renamed;
			case 963468344: // upfrontFee
			  return upfrontFee_Renamed;
		  }
		  return base.metaPropertyGet(propertyName);
		}

		public override ResolvedCdsTrade.Builder builder()
		{
		  return new ResolvedCdsTrade.Builder();
		}

		public override Type beanType()
		{
		  return typeof(ResolvedCdsTrade);
		}

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<String, org.joda.beans.MetaProperty<?>> metaPropertyMap()
		public override IDictionary<string, MetaProperty<object>> metaPropertyMap()
		{
		  return metaPropertyMap$;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// The meta-property for the {@code info} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<TradeInfo> info()
		{
		  return info_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code product} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<ResolvedCds> product()
		{
		  return product_Renamed;
		}

		/// <summary>
		/// The meta-property for the {@code upfrontFee} property. </summary>
		/// <returns> the meta-property, not null </returns>
		public MetaProperty<Payment> upfrontFee()
		{
		  return upfrontFee_Renamed;
		}

		//-----------------------------------------------------------------------
		protected internal override object propertyGet(Bean bean, string propertyName, bool quiet)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 3237038: // info
			  return ((ResolvedCdsTrade) bean).Info;
			case -309474065: // product
			  return ((ResolvedCdsTrade) bean).Product;
			case 963468344: // upfrontFee
			  return ((ResolvedCdsTrade) bean).upfrontFee;
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
	  /// The bean-builder for {@code ResolvedCdsTrade}.
	  /// </summary>
	  public sealed class Builder : DirectFieldsBeanBuilder<ResolvedCdsTrade>
	  {

//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal TradeInfo info_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal ResolvedCds product_Renamed;
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		internal Payment upfrontFee_Renamed;

		/// <summary>
		/// Restricted constructor.
		/// </summary>
		internal Builder()
		{
		  applyDefaults(this);
		}

		/// <summary>
		/// Restricted copy constructor. </summary>
		/// <param name="beanToCopy">  the bean to copy from, not null </param>
		internal Builder(ResolvedCdsTrade beanToCopy)
		{
		  this.info_Renamed = beanToCopy.Info;
		  this.product_Renamed = beanToCopy.Product;
		  this.upfrontFee_Renamed = beanToCopy.upfrontFee;
		}

		//-----------------------------------------------------------------------
		public override object get(string propertyName)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 3237038: // info
			  return info_Renamed;
			case -309474065: // product
			  return product_Renamed;
			case 963468344: // upfrontFee
			  return upfrontFee_Renamed;
			default:
			  throw new NoSuchElementException("Unknown property: " + propertyName);
		  }
		}

		public override Builder set(string propertyName, object newValue)
		{
		  switch (propertyName.GetHashCode())
		  {
			case 3237038: // info
			  this.info_Renamed = (TradeInfo) newValue;
			  break;
			case -309474065: // product
			  this.product_Renamed = (ResolvedCds) newValue;
			  break;
			case 963468344: // upfrontFee
			  this.upfrontFee_Renamed = (Payment) newValue;
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

		public override ResolvedCdsTrade build()
		{
		  return new ResolvedCdsTrade(info_Renamed, product_Renamed, upfrontFee_Renamed);
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// Sets the additional trade information, defaulted to an empty instance.
		/// <para>
		/// This allows additional information to be attached to the trade.
		/// </para>
		/// </summary>
		/// <param name="info">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder info(TradeInfo info)
		{
		  JodaBeanUtils.notNull(info, "info");
		  this.info_Renamed = info;
		  return this;
		}

		/// <summary>
		/// Sets the resolved CDS product.
		/// <para>
		/// The product captures the contracted financial details of the trade.
		/// </para>
		/// </summary>
		/// <param name="product">  the new value, not null </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder product(ResolvedCds product)
		{
		  JodaBeanUtils.notNull(product, "product");
		  this.product_Renamed = product;
		  return this;
		}

		/// <summary>
		/// Sets the upfront fee of the product.
		/// <para>
		/// This specifies a single amount payable by the buyer to the seller
		/// Thus the sign must be compatible with the product Pay/Receive flag.
		/// </para>
		/// <para>
		/// Some CDSs, especially legacy products, are traded at par and the upfront fee is not paid.
		/// </para>
		/// </summary>
		/// <param name="upfrontFee">  the new value </param>
		/// <returns> this, for chaining, not null </returns>
		public Builder upfrontFee(Payment upfrontFee)
		{
		  this.upfrontFee_Renamed = upfrontFee;
		  return this;
		}

		//-----------------------------------------------------------------------
		public override string ToString()
		{
		  StringBuilder buf = new StringBuilder(128);
		  buf.Append("ResolvedCdsTrade.Builder{");
		  buf.Append("info").Append('=').Append(JodaBeanUtils.ToString(info_Renamed)).Append(',').Append(' ');
		  buf.Append("product").Append('=').Append(JodaBeanUtils.ToString(product_Renamed)).Append(',').Append(' ');
		  buf.Append("upfrontFee").Append('=').Append(JodaBeanUtils.ToString(upfrontFee_Renamed));
		  buf.Append('}');
		  return buf.ToString();
		}

	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}