﻿using System;
using System.Collections.Generic;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.measure.bond
{

	using ImmutableMap = com.google.common.collect.ImmutableMap;
	using ImmutableSet = com.google.common.collect.ImmutableSet;
	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using Resolvable = com.opengamma.strata.basics.Resolvable;
	using Currency = com.opengamma.strata.basics.currency.Currency;
	using Measure = com.opengamma.strata.calc.Measure;
	using CalculationFunction = com.opengamma.strata.calc.runner.CalculationFunction;
	using CalculationParameters = com.opengamma.strata.calc.runner.CalculationParameters;
	using FunctionRequirements = com.opengamma.strata.calc.runner.FunctionRequirements;
	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using FailureReason = com.opengamma.strata.collect.result.FailureReason;
	using Result = com.opengamma.strata.collect.result.Result;
	using ScenarioMarketData = com.opengamma.strata.data.scenario.ScenarioMarketData;
	using SecuritizedProductPortfolioItem = com.opengamma.strata.product.SecuritizedProductPortfolioItem;
	using FixedCouponBond = com.opengamma.strata.product.bond.FixedCouponBond;
	using FixedCouponBondPosition = com.opengamma.strata.product.bond.FixedCouponBondPosition;
	using FixedCouponBondTrade = com.opengamma.strata.product.bond.FixedCouponBondTrade;
	using ResolvedFixedCouponBondTrade = com.opengamma.strata.product.bond.ResolvedFixedCouponBondTrade;

	/// <summary>
	/// Perform calculations on a single {@code FixedCouponBondTrade} or {@code FixedCouponBondPosition}
	/// for each of a set of scenarios.
	/// <para>
	/// This uses the standard discounting calculation method.
	/// An instance of <seealso cref="LegalEntityDiscountingMarketDataLookup"/> must be specified.
	/// The supported built-in measures are:
	/// <ul>
	///   <li><seealso cref="Measures#PRESENT_VALUE Present value"/>
	///   <li><seealso cref="Measures#PV01_CALIBRATED_SUM PV01 calibrated sum"/>
	///   <li><seealso cref="Measures#PV01_CALIBRATED_BUCKETED PV01 calibrated bucketed"/>
	///   <li><seealso cref="Measures#PV01_MARKET_QUOTE_SUM PV01 market quote sum"/>
	///   <li><seealso cref="Measures#PV01_MARKET_QUOTE_BUCKETED PV01 market quote bucketed"/>
	///   <li><seealso cref="Measures#CURRENCY_EXPOSURE Currency exposure"/>
	///   <li><seealso cref="Measures#CURRENT_CASH Current cash"/>
	///   <li><seealso cref="Measures#RESOLVED_TARGET Resolved trade"/>
	/// </ul>
	/// 
	/// <h4>Price</h4>
	/// Strata uses <i>decimal prices</i> for bonds in the trade model, pricers and market data.
	/// For example, a price of 99.32% is represented in Strata by 0.9932.
	/// 
	/// </para>
	/// </summary>
	/// @param <T> the trade or position type </param>
	public class FixedCouponBondTradeCalculationFunction<T> : CalculationFunction<T> where T : com.opengamma.strata.product.SecuritizedProductPortfolioItem<com.opengamma.strata.product.bond.FixedCouponBond>, com.opengamma.strata.basics.Resolvable<com.opengamma.strata.product.bond.ResolvedFixedCouponBondTrade>
	{

	  /// <summary>
	  /// The trade instance
	  /// </summary>
	  public static readonly FixedCouponBondTradeCalculationFunction<FixedCouponBondTrade> TRADE = new FixedCouponBondTradeCalculationFunction<FixedCouponBondTrade>(typeof(FixedCouponBondTrade));
	  /// <summary>
	  /// The position instance
	  /// </summary>
	  public static readonly FixedCouponBondTradeCalculationFunction<FixedCouponBondPosition> POSITION = new FixedCouponBondTradeCalculationFunction<FixedCouponBondPosition>(typeof(FixedCouponBondPosition));

	  /// <summary>
	  /// The calculations by measure.
	  /// </summary>
	  private static readonly ImmutableMap<Measure, SingleMeasureCalculation> CALCULATORS = ImmutableMap.builder<Measure, SingleMeasureCalculation>().put(Measures.PRESENT_VALUE, FixedCouponBondMeasureCalculations.DEFAULT.presentValue).put(Measures.PV01_CALIBRATED_SUM, FixedCouponBondMeasureCalculations.DEFAULT.pv01CalibratedSum).put(Measures.PV01_CALIBRATED_BUCKETED, FixedCouponBondMeasureCalculations.DEFAULT.pv01CalibratedBucketed).put(Measures.PV01_MARKET_QUOTE_SUM, FixedCouponBondMeasureCalculations.DEFAULT.pv01MarketQuoteSum).put(Measures.PV01_MARKET_QUOTE_BUCKETED, FixedCouponBondMeasureCalculations.DEFAULT.pv01MarketQuoteBucketed).put(Measures.CURRENCY_EXPOSURE, FixedCouponBondMeasureCalculations.DEFAULT.currencyExposure).put(Measures.CURRENT_CASH, FixedCouponBondMeasureCalculations.DEFAULT.currentCash).put(Measures.RESOLVED_TARGET, (rt, smd) => rt).build();

	  private static readonly ImmutableSet<Measure> MEASURES = CALCULATORS.Keys;

	  /// <summary>
	  /// The trade or position type.
	  /// </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
	  private readonly Type<T> targetType_Renamed;

	  /// <summary>
	  /// Creates an instance.
	  /// </summary>
	  /// <param name="targetType">  the trade or position type </param>
	  private FixedCouponBondTradeCalculationFunction(Type<T> targetType)
	  {
		this.targetType_Renamed = ArgChecker.notNull(targetType, "targetType");
	  }

	  //-------------------------------------------------------------------------
	  public virtual Type<T> targetType()
	  {
		return targetType_Renamed;
	  }

	  public virtual ISet<Measure> supportedMeasures()
	  {
		return MEASURES;
	  }

	  public override Optional<string> identifier(T target)
	  {
		return target.Info.Id.map(id => id.ToString());
	  }

	  public virtual Currency naturalCurrency(T target, ReferenceData refData)
	  {
		return target.Currency;
	  }

	  //-------------------------------------------------------------------------
	  public virtual FunctionRequirements requirements(T target, ISet<Measure> measures, CalculationParameters parameters, ReferenceData refData)
	  {

		// extract data from product
		FixedCouponBond product = target.Product;

		// use lookup to build requirements
		LegalEntityDiscountingMarketDataLookup bondLookup = parameters.getParameter(typeof(LegalEntityDiscountingMarketDataLookup));
		return bondLookup.requirements(product.SecurityId, product.LegalEntityId, product.Currency);
	  }

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<com.opengamma.strata.calc.Measure, com.opengamma.strata.collect.result.Result<?>> calculate(T target, java.util.Set<com.opengamma.strata.calc.Measure> measures, com.opengamma.strata.calc.runner.CalculationParameters parameters, com.opengamma.strata.data.scenario.ScenarioMarketData scenarioMarketData, com.opengamma.strata.basics.ReferenceData refData)
	  public virtual IDictionary<Measure, Result<object>> calculate(T target, ISet<Measure> measures, CalculationParameters parameters, ScenarioMarketData scenarioMarketData, ReferenceData refData)
	  {

		// resolve the trade once for all measures and all scenarios
		ResolvedFixedCouponBondTrade resolved = target.resolve(refData);

		// use lookup to query market data
		LegalEntityDiscountingMarketDataLookup bondLookup = parameters.getParameter(typeof(LegalEntityDiscountingMarketDataLookup));
		LegalEntityDiscountingScenarioMarketData marketData = bondLookup.marketDataView(scenarioMarketData);

		// loop around measures, calculating all scenarios for one measure
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: java.util.Map<com.opengamma.strata.calc.Measure, com.opengamma.strata.collect.result.Result<?>> results = new java.util.HashMap<>();
		IDictionary<Measure, Result<object>> results = new Dictionary<Measure, Result<object>>();
		foreach (Measure measure in measures)
		{
		  results[measure] = calculate(measure, resolved, marketData);
		}
		return results;
	  }

	  // calculate one measure
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private com.opengamma.strata.collect.result.Result<?> calculate(com.opengamma.strata.calc.Measure measure, com.opengamma.strata.product.bond.ResolvedFixedCouponBondTrade resolved, LegalEntityDiscountingScenarioMarketData marketData)
	  private Result<object> calculate(Measure measure, ResolvedFixedCouponBondTrade resolved, LegalEntityDiscountingScenarioMarketData marketData)
	  {

		SingleMeasureCalculation calculator = CALCULATORS.get(measure);
		if (calculator == null)
		{
		  return Result.failure(FailureReason.UNSUPPORTED, "Unsupported measure for FixedCouponBond: {}", measure);
		}
		return Result.of(() => calculator(resolved, marketData));
	  }

	  //-------------------------------------------------------------------------
	  delegate object SingleMeasureCalculation(ResolvedFixedCouponBondTrade resolved, LegalEntityDiscountingScenarioMarketData marketData);

	}

}