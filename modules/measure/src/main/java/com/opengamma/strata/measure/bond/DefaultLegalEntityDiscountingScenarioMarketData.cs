﻿using System;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.measure.bond
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using ImmutableConstructor = org.joda.beans.gen.ImmutableConstructor;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using ScenarioMarketData = com.opengamma.strata.data.scenario.ScenarioMarketData;

	/// <summary>
	/// The default market data for products based on repo and issuer curves, used for calculation across multiple scenarios.
	/// <para>
	/// This uses a <seealso cref="LegalEntityDiscountingMarketDataLookup"/> to provide a view on <seealso cref="ScenarioMarketData"/>.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light") final class DefaultLegalEntityDiscountingScenarioMarketData implements LegalEntityDiscountingScenarioMarketData, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	internal sealed class DefaultLegalEntityDiscountingScenarioMarketData : LegalEntityDiscountingScenarioMarketData, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final LegalEntityDiscountingMarketDataLookup lookup;
		private readonly LegalEntityDiscountingMarketDataLookup lookup;
	  /// <summary>
	  /// The market data.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull", overrideGet = true) private final com.opengamma.strata.data.scenario.ScenarioMarketData marketData;
	  private readonly ScenarioMarketData marketData;
	  /// <summary>
	  /// The cache of single scenario instances.
	  /// </summary>
	  [NonSerialized]
	  private readonly AtomicReferenceArray<LegalEntityDiscountingMarketData> cache; // derived

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance based on a lookup and market data.
	  /// <para>
	  /// The lookup provides the mapping to find the correct repo and issuer curves.
	  /// The curves are in the market data.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="lookup">  the lookup </param>
	  /// <param name="marketData">  the market data </param>
	  /// <returns> the rates market view </returns>
	  internal static DefaultLegalEntityDiscountingScenarioMarketData of(LegalEntityDiscountingMarketDataLookup lookup, ScenarioMarketData marketData)
	  {

		return new DefaultLegalEntityDiscountingScenarioMarketData(lookup, marketData);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutableConstructor private DefaultLegalEntityDiscountingScenarioMarketData(LegalEntityDiscountingMarketDataLookup lookup, com.opengamma.strata.data.scenario.ScenarioMarketData marketData)
	  private DefaultLegalEntityDiscountingScenarioMarketData(LegalEntityDiscountingMarketDataLookup lookup, ScenarioMarketData marketData)
	  {

		this.lookup = ArgChecker.notNull(lookup, "lookup");
		this.marketData = ArgChecker.notNull(marketData, "marketData");
		this.cache = new AtomicReferenceArray<LegalEntityDiscountingMarketData>(marketData.ScenarioCount);
	  }

	  // ensure standard constructor is invoked
	  private object readResolve()
	  {
		return new DefaultLegalEntityDiscountingScenarioMarketData(lookup, marketData);
	  }

	  //-------------------------------------------------------------------------
	  public LegalEntityDiscountingScenarioMarketData withMarketData(ScenarioMarketData marketData)
	  {
		return DefaultLegalEntityDiscountingScenarioMarketData.of(lookup, marketData);
	  }

	  //-------------------------------------------------------------------------
	  public int ScenarioCount
	  {
		  get
		  {
			return marketData.ScenarioCount;
		  }
	  }

	  public LegalEntityDiscountingMarketData scenario(int scenarioIndex)
	  {
		LegalEntityDiscountingMarketData current = cache.get(scenarioIndex);
		if (current != null)
		{
		  return current;
		}
		return cache.updateAndGet(scenarioIndex, v => v != null ? v : lookup.marketDataView(marketData.scenario(scenarioIndex)));
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code DefaultLegalEntityDiscountingScenarioMarketData}.
	  /// </summary>
	  private static readonly TypedMetaBean<DefaultLegalEntityDiscountingScenarioMarketData> META_BEAN = LightMetaBean.of(typeof(DefaultLegalEntityDiscountingScenarioMarketData), MethodHandles.lookup(), new string[] {"lookup", "marketData"}, new object[0]);

	  /// <summary>
	  /// The meta-bean for {@code DefaultLegalEntityDiscountingScenarioMarketData}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static TypedMetaBean<DefaultLegalEntityDiscountingScenarioMarketData> meta()
	  {
		return META_BEAN;
	  }

	  static DefaultLegalEntityDiscountingScenarioMarketData()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  public override TypedMetaBean<DefaultLegalEntityDiscountingScenarioMarketData> metaBean()
	  {
		return META_BEAN;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the lookup. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public LegalEntityDiscountingMarketDataLookup Lookup
	  {
		  get
		  {
			return lookup;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the market data. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public ScenarioMarketData MarketData
	  {
		  get
		  {
			return marketData;
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
		  DefaultLegalEntityDiscountingScenarioMarketData other = (DefaultLegalEntityDiscountingScenarioMarketData) obj;
		  return JodaBeanUtils.equal(lookup, other.lookup) && JodaBeanUtils.equal(marketData, other.marketData);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(lookup);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(marketData);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("DefaultLegalEntityDiscountingScenarioMarketData{");
		buf.Append("lookup").Append('=').Append(lookup).Append(',').Append(' ');
		buf.Append("marketData").Append('=').Append(JodaBeanUtils.ToString(marketData));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}