﻿using System;
using System.Collections.Generic;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.measure.security
{

	using ImmutableMap = com.google.common.collect.ImmutableMap;
	using ImmutableSet = com.google.common.collect.ImmutableSet;
	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using Currency = com.opengamma.strata.basics.currency.Currency;
	using Measure = com.opengamma.strata.calc.Measure;
	using CalculationFunction = com.opengamma.strata.calc.runner.CalculationFunction;
	using CalculationParameters = com.opengamma.strata.calc.runner.CalculationParameters;
	using FunctionRequirements = com.opengamma.strata.calc.runner.FunctionRequirements;
	using FailureReason = com.opengamma.strata.collect.result.FailureReason;
	using Result = com.opengamma.strata.collect.result.Result;
	using ScenarioArray = com.opengamma.strata.data.scenario.ScenarioArray;
	using ScenarioMarketData = com.opengamma.strata.data.scenario.ScenarioMarketData;
	using QuoteId = com.opengamma.strata.market.observable.QuoteId;
	using Security = com.opengamma.strata.product.Security;
	using SecurityPosition = com.opengamma.strata.product.SecurityPosition;

	/// <summary>
	/// Perform calculations on a single {@code SecurityPosition} for each of a set of scenarios.
	/// <para>
	/// The supported built-in measures are:
	/// <ul>
	///   <li><seealso cref="Measures#PRESENT_VALUE Present value"/>
	/// </ul>
	/// </para>
	/// </summary>
	public class SecurityPositionCalculationFunction : CalculationFunction<SecurityPosition>
	{

	  /// <summary>
	  /// The calculations by measure.
	  /// </summary>
	  private static readonly ImmutableMap<Measure, SingleMeasureCalculation> CALCULATORS = ImmutableMap.builder<Measure, SingleMeasureCalculation>().put(Measures.PRESENT_VALUE, SecurityMeasureCalculations.presentValue).build();

	  private static readonly ImmutableSet<Measure> MEASURES = CALCULATORS.Keys;

	  /// <summary>
	  /// Creates an instance.
	  /// </summary>
	  public SecurityPositionCalculationFunction()
	  {
	  }

	  //-------------------------------------------------------------------------
	  public virtual Type<SecurityPosition> targetType()
	  {
		return typeof(SecurityPosition);
	  }

	  public virtual ISet<Measure> supportedMeasures()
	  {
		return MEASURES;
	  }

	  public override Optional<string> identifier(SecurityPosition target)
	  {
		return target.Info.Id.map(id => id.ToString());
	  }

	  public virtual Currency naturalCurrency(SecurityPosition position, ReferenceData refData)
	  {
		Security security = refData.getValue(position.SecurityId);
		return security.Currency;
	  }

	  //-------------------------------------------------------------------------
	  public virtual FunctionRequirements requirements(SecurityPosition position, ISet<Measure> measures, CalculationParameters parameters, ReferenceData refData)
	  {

		Security security = refData.getValue(position.SecurityId);
		QuoteId id = QuoteId.of(position.SecurityId.StandardId);

		return FunctionRequirements.builder().valueRequirements(ImmutableSet.of(id)).outputCurrencies(security.Currency).build();
	  }

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Map<com.opengamma.strata.calc.Measure, com.opengamma.strata.collect.result.Result<?>> calculate(com.opengamma.strata.product.SecurityPosition position, java.util.Set<com.opengamma.strata.calc.Measure> measures, com.opengamma.strata.calc.runner.CalculationParameters parameters, com.opengamma.strata.data.scenario.ScenarioMarketData scenarioMarketData, com.opengamma.strata.basics.ReferenceData refData)
	  public virtual IDictionary<Measure, Result<object>> calculate(SecurityPosition position, ISet<Measure> measures, CalculationParameters parameters, ScenarioMarketData scenarioMarketData, ReferenceData refData)
	  {

		// resolve security
		Security security = refData.getValue(position.SecurityId);

		// loop around measures, calculating all scenarios for one measure
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: java.util.Map<com.opengamma.strata.calc.Measure, com.opengamma.strata.collect.result.Result<?>> results = new java.util.HashMap<>();
		IDictionary<Measure, Result<object>> results = new Dictionary<Measure, Result<object>>();
		foreach (Measure measure in measures)
		{
		  results[measure] = calculate(measure, position, security, scenarioMarketData);
		}
		return results;
	  }

	  // calculate one measure
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: private com.opengamma.strata.collect.result.Result<?> calculate(com.opengamma.strata.calc.Measure measure, com.opengamma.strata.product.SecurityPosition position, com.opengamma.strata.product.Security security, com.opengamma.strata.data.scenario.ScenarioMarketData scenarioMarketData)
	  private Result<object> calculate(Measure measure, SecurityPosition position, Security security, ScenarioMarketData scenarioMarketData)
	  {

		SingleMeasureCalculation calculator = CALCULATORS.get(measure);
		if (calculator == null)
		{
		  return Result.failure(FailureReason.UNSUPPORTED, "Unsupported measure for SecurityPosition: {}", measure);
		}
		return Result.of(() => calculator(security, position.Quantity, scenarioMarketData));
	  }

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @FunctionalInterface interface SingleMeasureCalculation
	  delegate ScenarioArray<object> SingleMeasureCalculation(Security security, double quantity, ScenarioMarketData marketData);

	}

}