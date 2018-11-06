﻿using System;
using System.Text;

/*
 * Copyright (C) 2018 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	/// <summary>
	/// The traded price of a security-based trade.
	/// <para>
	/// When a trade in a security occurs there is an agreed price and trade date.
	/// This class combines these two pieces of information.
	/// </para>
	/// <para>
	/// Once the trade has occurred, end of day processing typically aggregates the trades into positions.
	/// As a position combines multiple trades at different prices, the information in this class does not apply.
	/// 
	/// <h4>Price</h4>
	/// Strata uses <i>decimal prices</i> in the trade model, pricers and market data.
	/// See the individual security types for more details.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light") public final class TradedPrice implements org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class TradedPrice : ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final java.time.LocalDate tradeDate;
		private readonly LocalDate tradeDate;
	  /// <summary>
	  /// The price at which the trade was agreed.
	  /// <para>
	  /// See the security type for more details on the meaning of the price.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double price;
	  private readonly double price;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance from the trade date and price.
	  /// </summary>
	  /// <param name="tradeDate">  the trade date </param>
	  /// <param name="price">  the price at which the trade was agreed </param>
	  /// <returns> the settlement information </returns>
	  public static TradedPrice of(LocalDate tradeDate, double price)
	  {
		return new TradedPrice(tradeDate, price);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code TradedPrice}.
	  /// </summary>
	  private static readonly TypedMetaBean<TradedPrice> META_BEAN = LightMetaBean.of(typeof(TradedPrice), MethodHandles.lookup(), new string[] {"tradeDate", "price"}, new object[0]);

	  /// <summary>
	  /// The meta-bean for {@code TradedPrice}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static TypedMetaBean<TradedPrice> meta()
	  {
		return META_BEAN;
	  }

	  static TradedPrice()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private TradedPrice(LocalDate tradeDate, double price)
	  {
		JodaBeanUtils.notNull(tradeDate, "tradeDate");
		this.tradeDate = tradeDate;
		this.price = price;
	  }

	  public override TypedMetaBean<TradedPrice> metaBean()
	  {
		return META_BEAN;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the trade date.
	  /// <para>
	  /// The date that the trade occurred.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public LocalDate TradeDate
	  {
		  get
		  {
			return tradeDate;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the price at which the trade was agreed.
	  /// <para>
	  /// See the security type for more details on the meaning of the price.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property </returns>
	  public double Price
	  {
		  get
		  {
			return price;
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
		  TradedPrice other = (TradedPrice) obj;
		  return JodaBeanUtils.equal(tradeDate, other.tradeDate) && JodaBeanUtils.equal(price, other.price);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(tradeDate);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(price);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("TradedPrice{");
		buf.Append("tradeDate").Append('=').Append(tradeDate).Append(',').Append(' ');
		buf.Append("price").Append('=').Append(JodaBeanUtils.ToString(price));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}