﻿/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.measure
{
	using ImmutableMeasure = com.opengamma.strata.calc.ImmutableMeasure;
	using Measure = com.opengamma.strata.calc.Measure;

	/// <summary>
	/// The standard set of measures that can be calculated by Strata.
	/// </summary>
	internal sealed class StandardMeasures
	{

	  // present value, with currency conversion
	  public static readonly Measure PRESENT_VALUE = ImmutableMeasure.of("PresentValue");
	  // explain present value, with no currency conversion
	  public static readonly Measure EXPLAIN_PRESENT_VALUE = ImmutableMeasure.of("ExplainPresentValue", false);

	  // PV01 calibrated sum
	  public static readonly Measure PV01_CALIBRATED_SUM = ImmutableMeasure.of("PV01CalibratedSum");
	  // PV01 calibrated bucketed
	  public static readonly Measure PV01_CALIBRATED_BUCKETED = ImmutableMeasure.of("PV01CalibratedBucketed");
	  // PV01 market quote sum
	  public static readonly Measure PV01_MARKET_QUOTE_SUM = ImmutableMeasure.of("PV01MarketQuoteSum");
	  // PV01 market quote bucketed
	  public static readonly Measure PV01_MARKET_QUOTE_BUCKETED = ImmutableMeasure.of("PV01MarketQuoteBucketed");

	  //-------------------------------------------------------------------------
	  // accrued interest
	  public static readonly Measure ACCRUED_INTEREST = ImmutableMeasure.of("AccruedInterest");
	  // cash flows
	  public static readonly Measure CASH_FLOWS = ImmutableMeasure.of("CashFlows");
	  // currency exposure, with no currency conversion
	  public static readonly Measure CURRENCY_EXPOSURE = ImmutableMeasure.of("CurrencyExposure", false);
	  // current cash
	  public static readonly Measure CURRENT_CASH = ImmutableMeasure.of("CurrentCash");
	  // forward FX rate
	  public static readonly Measure FORWARD_FX_RATE = ImmutableMeasure.of("ForwardFxRate", false);
	  // leg present value
	  public static readonly Measure LEG_PRESENT_VALUE = ImmutableMeasure.of("LegPresentValue");
	  // leg initial notional
	  public static readonly Measure LEG_INITIAL_NOTIONAL = ImmutableMeasure.of("LegInitialNotional");
	  // par rate, which is a decimal rate that does not need currency conversion
	  public static readonly Measure PAR_RATE = ImmutableMeasure.of("ParRate", false);
	  // par spread, which is a decimal rate that does not need currency conversion
	  public static readonly Measure PAR_SPREAD = ImmutableMeasure.of("ParSpread", false);
	  // the resolved target
	  public static readonly Measure RESOLVED_TARGET = ImmutableMeasure.of("ResolvedTarget", false);
	  // unit price, which is treated as a simple decimal number even if it refers to a currency
	  public static readonly Measure UNIT_PRICE = ImmutableMeasure.of("UnitPrice", false);

	  //-------------------------------------------------------------------------
	  // semi-parallel gamma bucketed PV01
	  public static readonly Measure PV01_SEMI_PARALLEL_GAMMA_BUCKETED = ImmutableMeasure.of("PV01SemiParallelGammaBucketed");
	  // single-node gamma bucketed PV01
	  public static readonly Measure PV01_SINGLE_NODE_GAMMA_BUCKETED = ImmutableMeasure.of("PV01SingleNodeGammaBucketed");

	}

}