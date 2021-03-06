﻿using System;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.market.curve
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	using ResolvedProduct = com.opengamma.strata.product.ResolvedProduct;
	using ResolvedTrade = com.opengamma.strata.product.ResolvedTrade;
	using Trade = com.opengamma.strata.product.Trade;
	using TradeInfo = com.opengamma.strata.product.TradeInfo;

	/// <summary>
	/// Dummy trade.
	/// Based on a FRA.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light") public final class DummyFraTrade implements com.opengamma.strata.product.Trade, com.opengamma.strata.product.ResolvedTrade, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class DummyFraTrade : Trade, ResolvedTrade, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final java.time.LocalDate date;
		private readonly LocalDate date;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double fixedRate;
	  private readonly double fixedRate;

	  public static DummyFraTrade of(LocalDate date, double fixedRate)
	  {
		return new DummyFraTrade(date, fixedRate);
	  }

	  public TradeInfo Info
	  {
		  get
		  {
			return TradeInfo.empty();
		  }
	  }

	  public Trade withInfo(TradeInfo info)
	  {
		return this;
	  }

	  public ResolvedProduct Product
	  {
		  get
		  {
			throw new System.NotSupportedException();
		  }
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code DummyFraTrade}.
	  /// </summary>
	  private static readonly TypedMetaBean<DummyFraTrade> META_BEAN = LightMetaBean.of(typeof(DummyFraTrade), MethodHandles.lookup(), new string[] {"date", "fixedRate"}, new object[0]);

	  /// <summary>
	  /// The meta-bean for {@code DummyFraTrade}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static TypedMetaBean<DummyFraTrade> meta()
	  {
		return META_BEAN;
	  }

	  static DummyFraTrade()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private DummyFraTrade(LocalDate date, double fixedRate)
	  {
		JodaBeanUtils.notNull(date, "date");
		this.date = date;
		this.fixedRate = fixedRate;
	  }

	  public override TypedMetaBean<DummyFraTrade> metaBean()
	  {
		return META_BEAN;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the date. </summary>
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
	  /// Gets the fixedRate. </summary>
	  /// <returns> the value of the property </returns>
	  public double FixedRate
	  {
		  get
		  {
			return fixedRate;
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
		  DummyFraTrade other = (DummyFraTrade) obj;
		  return JodaBeanUtils.equal(date, other.date) && JodaBeanUtils.equal(fixedRate, other.fixedRate);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(date);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(fixedRate);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("DummyFraTrade{");
		buf.Append("date").Append('=').Append(date).Append(',').Append(' ');
		buf.Append("fixedRate").Append('=').Append(JodaBeanUtils.ToString(fixedRate));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}