﻿using System;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.measure.cms
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	using CalculationTarget = com.opengamma.strata.basics.CalculationTarget;
	using Measure = com.opengamma.strata.calc.Measure;
	using CalculationParameter = com.opengamma.strata.calc.runner.CalculationParameter;
	using SwaptionMarketDataLookup = com.opengamma.strata.measure.swaption.SwaptionMarketDataLookup;
	using CmsTrade = com.opengamma.strata.product.cms.CmsTrade;

	/// <summary>
	/// The additional parameters necessary for pricing CMS using SABR extrapolation replication.
	/// <para>
	/// The volatilities used in pricing are provided using <seealso cref="SwaptionMarketDataLookup"/>.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light") public final class CmsSabrExtrapolationParams implements com.opengamma.strata.calc.runner.CalculationParameter, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class CmsSabrExtrapolationParams : CalculationParameter, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double cutOffStrike;
		private readonly double cutOffStrike;
	  /// <summary>
	  /// The tail thickness parameter.
	  /// <para>
	  /// This must be greater than 0 in order to ensure that the call price converges to 0 for infinite strike.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final double mu;
	  private readonly double mu;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance based on a lookup and market data.
	  /// <para>
	  /// The lookup knows how to obtain the volatilities from the market data.
	  /// This might involve accessing a surface or a cube.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="cutOffStrike">  the cut-off strike </param>
	  /// <param name="mu">  the tail thickness parameter </param>
	  /// <returns> the SABR extrapolation parameters </returns>
	  public static CmsSabrExtrapolationParams of(double cutOffStrike, double mu)
	  {
		return new CmsSabrExtrapolationParams(cutOffStrike, mu);
	  }

	  //-------------------------------------------------------------------------
	  public override Optional<CalculationParameter> filter(CalculationTarget target, Measure measure)
	  {
		return target is CmsTrade ? this : null;
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code CmsSabrExtrapolationParams}.
	  /// </summary>
	  private static readonly TypedMetaBean<CmsSabrExtrapolationParams> META_BEAN = LightMetaBean.of(typeof(CmsSabrExtrapolationParams), MethodHandles.lookup(), new string[] {"cutOffStrike", "mu"}, new object[0]);

	  /// <summary>
	  /// The meta-bean for {@code CmsSabrExtrapolationParams}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static TypedMetaBean<CmsSabrExtrapolationParams> meta()
	  {
		return META_BEAN;
	  }

	  static CmsSabrExtrapolationParams()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private CmsSabrExtrapolationParams(double cutOffStrike, double mu)
	  {
		this.cutOffStrike = cutOffStrike;
		this.mu = mu;
	  }

	  public override TypedMetaBean<CmsSabrExtrapolationParams> metaBean()
	  {
		return META_BEAN;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the cut-off strike.
	  /// <para>
	  /// The smile is extrapolated above that level.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property </returns>
	  public double CutOffStrike
	  {
		  get
		  {
			return cutOffStrike;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the tail thickness parameter.
	  /// <para>
	  /// This must be greater than 0 in order to ensure that the call price converges to 0 for infinite strike.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property </returns>
	  public double Mu
	  {
		  get
		  {
			return mu;
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
		  CmsSabrExtrapolationParams other = (CmsSabrExtrapolationParams) obj;
		  return JodaBeanUtils.equal(cutOffStrike, other.cutOffStrike) && JodaBeanUtils.equal(mu, other.mu);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(cutOffStrike);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(mu);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("CmsSabrExtrapolationParams{");
		buf.Append("cutOffStrike").Append('=').Append(cutOffStrike).Append(',').Append(' ');
		buf.Append("mu").Append('=').Append(JodaBeanUtils.ToString(mu));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}