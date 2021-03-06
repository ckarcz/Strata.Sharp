﻿using System;
using System.Collections.Generic;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.sensitivity
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.currency.Currency.USD;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertFalse;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertTrue;


	using Test = org.testng.annotations.Test;

	using ImmutableMap = com.google.common.collect.ImmutableMap;
	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using Currency = com.opengamma.strata.basics.currency.Currency;
	using CurrencyAmount = com.opengamma.strata.basics.currency.CurrencyAmount;
	using IborIndex = com.opengamma.strata.basics.index.IborIndex;
	using IborIndices = com.opengamma.strata.basics.index.IborIndices;
	using Index = com.opengamma.strata.basics.index.Index;
	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using DoubleMatrix = com.opengamma.strata.collect.array.DoubleMatrix;
	using Pair = com.opengamma.strata.collect.tuple.Pair;
	using CombinedCurve = com.opengamma.strata.market.curve.CombinedCurve;
	using Curve = com.opengamma.strata.market.curve.Curve;
	using InterpolatedNodalCurve = com.opengamma.strata.market.curve.InterpolatedNodalCurve;
	using LegalEntityGroup = com.opengamma.strata.market.curve.LegalEntityGroup;
	using RepoGroup = com.opengamma.strata.market.curve.RepoGroup;
	using CrossGammaParameterSensitivities = com.opengamma.strata.market.param.CrossGammaParameterSensitivities;
	using CrossGammaParameterSensitivity = com.opengamma.strata.market.param.CrossGammaParameterSensitivity;
	using CurrencyParameterSensitivities = com.opengamma.strata.market.param.CurrencyParameterSensitivities;
	using CurrencyParameterSensitivity = com.opengamma.strata.market.param.CurrencyParameterSensitivity;
	using PointSensitivities = com.opengamma.strata.market.sensitivity.PointSensitivities;
	using ImmutableLegalEntityDiscountingProvider = com.opengamma.strata.pricer.bond.ImmutableLegalEntityDiscountingProvider;
	using RatesProviderDataSets = com.opengamma.strata.pricer.datasets.RatesProviderDataSets;
	using ImmutableRatesProvider = com.opengamma.strata.pricer.rate.ImmutableRatesProvider;
	using RatesProvider = com.opengamma.strata.pricer.rate.RatesProvider;
	using DiscountingSwapProductPricer = com.opengamma.strata.pricer.swap.DiscountingSwapProductPricer;
	using BuySell = com.opengamma.strata.product.common.BuySell;
	using ResolvedSwap = com.opengamma.strata.product.swap.ResolvedSwap;
	using FixedIborSwapConventions = com.opengamma.strata.product.swap.type.FixedIborSwapConventions;

	/// <summary>
	/// Test <seealso cref="CurveGammaCalculator"/> cross-gamma.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class CurveGammaCalculatorCrossGammaTest
	public class CurveGammaCalculatorCrossGammaTest
	{

	  private static readonly ReferenceData REF_DATA = ReferenceData.standard();
	  private const double EPS = 1.0e-6;
	  private const double TOL = 1.0e-14;
	  private static readonly CurveGammaCalculator FORWARD = CurveGammaCalculator.ofForwardDifference(EPS * 0.1);
	  private static readonly CurveGammaCalculator CENTRAL = CurveGammaCalculator.ofCentralDifference(EPS);
	  private static readonly CurveGammaCalculator BACKWARD = CurveGammaCalculator.ofBackwardDifference(EPS * 0.1);

	  public virtual void sensitivity_single_curve()
	  {
		CrossGammaParameterSensitivities forward = FORWARD.calculateCrossGammaIntraCurve(RatesProviderDataSets.SINGLE_USD, this.sensiFn);
		CrossGammaParameterSensitivities central = CENTRAL.calculateCrossGammaIntraCurve(RatesProviderDataSets.SINGLE_USD, this.sensiFn);
		CrossGammaParameterSensitivities backward = BACKWARD.calculateCrossGammaIntraCurve(RatesProviderDataSets.SINGLE_USD, this.sensiFn);
		DoubleArray times = RatesProviderDataSets.TIMES_1;
		foreach (CrossGammaParameterSensitivities sensi in new CrossGammaParameterSensitivities[] {forward, central, backward})
		{
		  CurrencyParameterSensitivities diagonalComputed = sensi.diagonal();
		  assertEquals(sensi.size(), 1);
		  assertEquals(diagonalComputed.size(), 1);
		  DoubleMatrix s = sensi.Sensitivities.get(0).Sensitivity;
		  assertEquals(s.columnCount(), times.size());
		  for (int i = 0; i < times.size(); i++)
		  {
			for (int j = 0; j < times.size(); j++)
			{
			  double expected = 32d * times.get(i) * times.get(j);
			  assertEquals(s.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
			}
		  }
		}
		// no difference for single curve
		CrossGammaParameterSensitivities forwardCross = FORWARD.calculateCrossGammaCrossCurve(RatesProviderDataSets.SINGLE_USD, this.sensiFn);
		assertTrue(forward.equalWithTolerance(forwardCross, TOL));
		CrossGammaParameterSensitivities centralCross = CENTRAL.calculateCrossGammaCrossCurve(RatesProviderDataSets.SINGLE_USD, this.sensiFn);
		assertTrue(central.equalWithTolerance(centralCross, TOL));
		CrossGammaParameterSensitivities backwardCross = BACKWARD.calculateCrossGammaCrossCurve(RatesProviderDataSets.SINGLE_USD, this.sensiFn);
		assertTrue(backward.equalWithTolerance(backwardCross, TOL));
	  }

	  public virtual void sensitivity_intra_multi_curve()
	  {
		CrossGammaParameterSensitivities sensiComputed = CENTRAL.calculateCrossGammaIntraCurve(RatesProviderDataSets.MULTI_CPI_USD, this.sensiFn);
		DoubleArray times1 = RatesProviderDataSets.TIMES_1;
		DoubleArray times2 = RatesProviderDataSets.TIMES_2;
		DoubleArray times3 = RatesProviderDataSets.TIMES_3;
		DoubleArray times4 = RatesProviderDataSets.TIMES_4;
		assertEquals(sensiComputed.size(), 4);
		DoubleMatrix s1 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_DSC_NAME, USD).Sensitivity;
		assertEquals(s1.columnCount(), times1.size());
		for (int i = 0; i < times1.size(); i++)
		{
		  for (int j = 0; j < times1.size(); j++)
		  {
			double expected = 8d * times1.get(i) * times1.get(j);
			assertEquals(s1.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s2 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L3_NAME, USD).Sensitivity;
		assertEquals(s2.columnCount(), times2.size());
		for (int i = 0; i < times2.size(); i++)
		{
		  for (int j = 0; j < times2.size(); j++)
		  {
			double expected = 2d * times2.get(i) * times2.get(j);
			assertEquals(s2.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
		DoubleMatrix s3 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L6_NAME, USD).Sensitivity;
		assertEquals(s3.columnCount(), times3.size());
		for (int i = 0; i < times3.size(); i++)
		{
		  for (int j = 0; j < times3.size(); j++)
		  {
			double expected = 2d * times3.get(i) * times3.get(j);
			assertEquals(s3.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
		DoubleMatrix s4 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_CPI_NAME, USD).Sensitivity;
		assertEquals(s4.columnCount(), times4.size());
		for (int i = 0; i < times4.size(); i++)
		{
		  for (int j = 0; j < times4.size(); j++)
		  {
			double expected = 2d * times4.get(i) * times4.get(j);
			assertEquals(s4.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
	  }

	  public virtual void sensitivity_multi_curve_empty()
	  {
		CrossGammaParameterSensitivities sensiComputed = CENTRAL.calculateCrossGammaIntraCurve(RatesProviderDataSets.MULTI_CPI_USD, this.sensiModFn);
		DoubleArray times2 = RatesProviderDataSets.TIMES_2;
		DoubleArray times3 = RatesProviderDataSets.TIMES_3;
		assertEquals(sensiComputed.size(), 2);
		DoubleMatrix s2 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L3_NAME, USD).Sensitivity;
		assertEquals(s2.columnCount(), times2.size());
		for (int i = 0; i < times2.size(); i++)
		{
		  for (int j = 0; j < times2.size(); j++)
		  {
			double expected = 2d * times2.get(i) * times2.get(j);
			assertEquals(s2.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
		DoubleMatrix s3 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L6_NAME, USD).Sensitivity;
		assertEquals(s3.columnCount(), times3.size());
		for (int i = 0; i < times3.size(); i++)
		{
		  for (int j = 0; j < times3.size(); j++)
		  {
			double expected = 2d * times3.get(i) * times3.get(j);
			assertEquals(s3.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
		Optional<CrossGammaParameterSensitivity> oisSensi = sensiComputed.findSensitivity(RatesProviderDataSets.USD_DSC_NAME, USD);
		assertFalse(oisSensi.Present);
		Optional<CrossGammaParameterSensitivity> priceIndexSensi = sensiComputed.findSensitivity(RatesProviderDataSets.USD_CPI_NAME, USD);
		assertFalse(priceIndexSensi.Present);
	  }

	  public virtual void sensitivity_cross_multi_curve()
	  {
		CrossGammaParameterSensitivities sensiComputed = CENTRAL.calculateCrossGammaCrossCurve(RatesProviderDataSets.MULTI_CPI_USD, this.sensiFn);
		DoubleArray times1 = RatesProviderDataSets.TIMES_1;
		DoubleArray times2 = RatesProviderDataSets.TIMES_2;
		DoubleArray times3 = RatesProviderDataSets.TIMES_3;
		DoubleArray times4 = RatesProviderDataSets.TIMES_4;
		int paramsTotal = times1.size() + times2.size() + times3.size() + times4.size();
		double[] timesTotal = new double[paramsTotal];
		DoubleArray times1Twice = times1.multipliedBy(2d);
		Array.Copy(times4.toArray(), 0, timesTotal, 0, times4.size());
		Array.Copy(times1Twice.toArray(), 0, timesTotal, times4.size(), times1.size());
		Array.Copy(times2.toArray(), 0, timesTotal, times1.size() + times4.size(), times2.size());
		Array.Copy(times3.toArray(), 0, timesTotal, times1.size() + times2.size() + times4.size(), times3.size());

		assertEquals(sensiComputed.size(), 4);
		DoubleMatrix s1 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_DSC_NAME, USD).Sensitivity;
		assertEquals(s1.columnCount(), paramsTotal);
		for (int i = 0; i < times1.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 4d * times1.get(i) * timesTotal[j];
			assertEquals(s1.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s2 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L3_NAME, USD).Sensitivity;
		assertEquals(s2.columnCount(), paramsTotal);
		for (int i = 0; i < times2.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 2d * times2.get(i) * timesTotal[j];
			assertEquals(s2.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s3 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L6_NAME, USD).Sensitivity;
		assertEquals(s3.columnCount(), paramsTotal);
		for (int i = 0; i < times3.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 2d * times3.get(i) * timesTotal[j];
			assertEquals(s3.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s4 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_CPI_NAME, USD).Sensitivity;
		assertEquals(s4.columnCount(), paramsTotal);
		for (int i = 0; i < times4.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 2d * times4.get(i) * timesTotal[j];
			assertEquals(s4.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 20d);
		  }
		}
	  }

	  public virtual void sensitivity_cross_multi_curve_empty()
	  {
		CrossGammaParameterSensitivities sensiComputed = CENTRAL.calculateCrossGammaCrossCurve(RatesProviderDataSets.MULTI_CPI_USD, this.sensiModFn);
		DoubleArray times2 = RatesProviderDataSets.TIMES_2;
		DoubleArray times3 = RatesProviderDataSets.TIMES_3;
		int paramsTotal = times2.size() + times3.size();
		double[] timesTotal = new double[paramsTotal];
		Array.Copy(times2.toArray(), 0, timesTotal, 0, times2.size());
		Array.Copy(times3.toArray(), 0, timesTotal, times2.size(), times3.size());
		assertEquals(sensiComputed.size(), 2);
		DoubleMatrix s2 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L3_NAME, USD).Sensitivity;
		assertEquals(s2.columnCount(), paramsTotal);
		for (int i = 0; i < times2.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 2d * times2.get(i) * timesTotal[j];
			assertEquals(s2.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
		DoubleMatrix s3 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L6_NAME, USD).Sensitivity;
		assertEquals(s3.columnCount(), paramsTotal);
		for (int i = 0; i < times3.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 2d * times3.get(i) * timesTotal[j];
			assertEquals(s3.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
		Optional<CrossGammaParameterSensitivity> oisSensi = sensiComputed.findSensitivity(RatesProviderDataSets.USD_DSC_NAME, USD);
		assertFalse(oisSensi.Present);
		Optional<CrossGammaParameterSensitivity> priceIndexSensi = sensiComputed.findSensitivity(RatesProviderDataSets.USD_CPI_NAME, USD);
		assertFalse(priceIndexSensi.Present);
	  }

	  // test diagonal part against finite difference approximation computed from pv
	  public virtual void swap_exampleTest()
	  {
		LocalDate start = LocalDate.of(2014, 3, 10);
		LocalDate end = LocalDate.of(2021, 3, 10);
		double notional = 1.0e6;
		ResolvedSwap swap = FixedIborSwapConventions.USD_FIXED_6M_LIBOR_3M.toTrade(RatesProviderDataSets.VAL_DATE_2014_01_22, start, end, BuySell.BUY, notional, 0.005).Product.resolve(REF_DATA);
		DiscountingSwapProductPricer pricer = DiscountingSwapProductPricer.DEFAULT;
		System.Func<ImmutableRatesProvider, CurrencyAmount> pvFunction = p => pricer.presentValue(swap, USD, p);
		System.Func<ImmutableRatesProvider, CurrencyParameterSensitivities> sensiFunction = p =>
		{
	  PointSensitivities sensi = pricer.presentValueSensitivity(swap, p).build();
	  return p.parameterSensitivity(sensi);
		};
		CurrencyParameterSensitivities expected = sensitivityDiagonal(RatesProviderDataSets.MULTI_CPI_USD, pvFunction);
		CurrencyParameterSensitivities computed = CENTRAL.calculateCrossGammaIntraCurve(RatesProviderDataSets.MULTI_CPI_USD, sensiFunction).diagonal();
		assertTrue(computed.equalWithTolerance(expected, Math.Sqrt(EPS) * notional));
		CurrencyParameterSensitivities computedFromCross = CENTRAL.calculateCrossGammaCrossCurve(RatesProviderDataSets.MULTI_CPI_USD, sensiFunction).diagonal();
		assertTrue(computed.equalWithTolerance(computedFromCross, TOL));
	  }

	  public virtual void sensitivity_multi_combined_curve()
	  {
		CrossGammaParameterSensitivities sensiCrossComputed = CENTRAL.calculateCrossGammaCrossCurve(RatesProviderDataSets.MULTI_CPI_USD_COMBINED, this.sensiCombinedFn);
		DoubleArray times1 = RatesProviderDataSets.TIMES_1; // ois
		DoubleArray times2 = RatesProviderDataSets.TIMES_2; // l3
		DoubleArray times3 = RatesProviderDataSets.TIMES_3; // l6
		DoubleArray times4 = RatesProviderDataSets.TIMES_4; // cpi
		int paramsTotal = times1.size() + times2.size() + times3.size() + times4.size();
		double[] timesTotal = new double[paramsTotal];
		DoubleArray times1Twice = times1.multipliedBy(2d);
		Array.Copy(times4.toArray(), 0, timesTotal, 0, times4.size());
		Array.Copy(times1Twice.toArray(), 0, timesTotal, times4.size(), times1.size());
		Array.Copy(times2.toArray(), 0, timesTotal, times1.size() + times4.size(), times2.size());
		Array.Copy(times3.toArray(), 0, timesTotal, times1.size() + times2.size() + times4.size(), times3.size());

		assertEquals(sensiCrossComputed.size(), 4);
		DoubleMatrix s1 = sensiCrossComputed.getSensitivity(RatesProviderDataSets.USD_DSC_NAME, USD).Sensitivity;
		assertEquals(s1.columnCount(), paramsTotal);
		for (int i = 0; i < times1.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 4d * times1.get(i) * timesTotal[j];
			assertEquals(s1.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s2 = sensiCrossComputed.getSensitivity(RatesProviderDataSets.USD_L3_NAME, USD).Sensitivity;
		assertEquals(s2.columnCount(), paramsTotal);
		for (int i = 0; i < times2.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 8d * times2.get(i) * timesTotal[j];
			assertEquals(s2.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s3 = sensiCrossComputed.getSensitivity(RatesProviderDataSets.USD_L6_NAME, USD).Sensitivity;
		assertEquals(s3.columnCount(), paramsTotal);
		for (int i = 0; i < times3.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 2d * times3.get(i) * timesTotal[j];
			assertEquals(s3.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s4 = sensiCrossComputed.getSensitivity(RatesProviderDataSets.USD_CPI_NAME, USD).Sensitivity;
		assertEquals(s4.columnCount(), paramsTotal);
		for (int i = 0; i < times4.size(); i++)
		{
		  for (int j = 0; j < paramsTotal; j++)
		  {
			double expected = 2d * times4.get(i) * timesTotal[j];
			assertEquals(s4.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 20d);
		  }
		}

		CrossGammaParameterSensitivities sensiIntraComputed = CENTRAL.calculateCrossGammaIntraCurve(RatesProviderDataSets.MULTI_CPI_USD_COMBINED, this.sensiCombinedFn);
		DoubleMatrix s1Intra = sensiIntraComputed.getSensitivity(RatesProviderDataSets.USD_DSC_NAME, USD).Sensitivity;
		DoubleMatrix s2Intra = sensiIntraComputed.getSensitivity(RatesProviderDataSets.USD_L3_NAME, USD).Sensitivity;
		DoubleMatrix s3Intra = sensiIntraComputed.getSensitivity(RatesProviderDataSets.USD_L6_NAME, USD).Sensitivity;
		DoubleMatrix s4Intra = sensiIntraComputed.getSensitivity(RatesProviderDataSets.USD_CPI_NAME, USD).Sensitivity;
		int offsetOis = times4.size();
		for (int i = 0; i < times1.size(); i++)
		{
		  for (int j = 0; j < times1.size(); j++)
		  {
			assertEquals(s1Intra.get(i, j), s1.get(i, offsetOis + j), TOL);
		  }
		}
		int offset3m = times4.size() + times1.size();
		for (int i = 0; i < times2.size(); i++)
		{
		  for (int j = 0; j < times2.size(); j++)
		  {
			assertEquals(s2Intra.get(i, j), s2.get(i, offset3m + j), TOL);
		  }
		}
		int offset6m = times4.size() + times1.size() + times2.size();
		for (int i = 0; i < times3.size(); i++)
		{
		  for (int j = 0; j < times3.size(); j++)
		  {
			assertEquals(s3Intra.get(i, j), s3.get(i, offset6m + j), TOL);
		  }
		}
		for (int i = 0; i < times4.size(); i++)
		{
		  for (int j = 0; j < times4.size(); j++)
		  {
			assertEquals(s4Intra.get(i, j), s4.get(i, j), TOL);
		  }
		}
	  }

	  //-------------------------------------------------------------------------
	  public virtual void sensitivity_intra_multi_bond_curve()
	  {
		CrossGammaParameterSensitivities sensiComputed = CENTRAL.calculateCrossGammaIntraCurve(RatesProviderDataSets.MULTI_BOND, this.sensiFnBond);
		DoubleArray timesUsRepo = RatesProviderDataSets.TIMES_1;
		DoubleArray timesUsIssuer1 = RatesProviderDataSets.TIMES_3;
		DoubleArray timesUsIssuer2 = RatesProviderDataSets.TIMES_2;
		assertEquals(sensiComputed.size(), 3);
		DoubleMatrix s1 = sensiComputed.getSensitivity(RatesProviderDataSets.US_REPO_CURVE_NAME, USD).Sensitivity;
		assertEquals(s1.columnCount(), timesUsRepo.size());
		for (int i = 0; i < timesUsRepo.size(); i++)
		{
		  for (int j = 0; j < timesUsRepo.size(); j++)
		  {
			double expected = 2d * timesUsRepo.get(i) * timesUsRepo.get(j);
			assertEquals(s1.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s2 = sensiComputed.getSensitivity(RatesProviderDataSets.US_ISSUER_CURVE_1_NAME, USD).Sensitivity;
		assertEquals(s2.columnCount(), timesUsIssuer1.size());
		for (int i = 0; i < timesUsIssuer1.size(); i++)
		{
		  for (int j = 0; j < timesUsIssuer1.size(); j++)
		  {
			double expected = 2d * timesUsIssuer1.get(i) * timesUsIssuer1.get(j);
			assertEquals(s2.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
		DoubleMatrix s3 = sensiComputed.getSensitivity(RatesProviderDataSets.US_ISSUER_CURVE_2_NAME, USD).Sensitivity;
		assertEquals(s3.columnCount(), timesUsIssuer2.size());
		for (int i = 0; i < timesUsIssuer2.size(); i++)
		{
		  for (int j = 0; j < timesUsIssuer2.size(); j++)
		  {
			double expected = 2d * timesUsIssuer2.get(i) * timesUsIssuer2.get(j);
			assertEquals(s3.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS);
		  }
		}
	  }

	  public virtual void sensitivity_multi_combined_bond_curve()
	  {
		CrossGammaParameterSensitivities sensiComputed = CENTRAL.calculateCrossGammaIntraCurve(RatesProviderDataSets.MULTI_BOND_COMBINED, this.sensiCombinedFnBond);
		DoubleArray timesUsL3 = RatesProviderDataSets.TIMES_2;
		DoubleArray timesUsRepo = RatesProviderDataSets.TIMES_1;
		DoubleArray timesUsIssuer1 = RatesProviderDataSets.TIMES_3;
		DoubleArray timesUsIssuer2 = RatesProviderDataSets.TIMES_2;
		assertEquals(sensiComputed.size(), 4);
		DoubleMatrix s1 = sensiComputed.getSensitivity(RatesProviderDataSets.USD_L3_NAME, USD).Sensitivity;
		assertEquals(s1.columnCount(), timesUsL3.size());
		for (int i = 0; i < timesUsL3.size(); i++)
		{
		  for (int j = 0; j < timesUsL3.size(); j++)
		  {
			double expected = 2d * timesUsL3.get(i) * timesUsL3.get(j) * 3d * 3d;
			assertEquals(s1.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s2 = sensiComputed.getSensitivity(RatesProviderDataSets.US_REPO_CURVE_NAME, USD).Sensitivity;
		assertEquals(s2.columnCount(), timesUsRepo.size());
		for (int i = 0; i < timesUsRepo.size(); i++)
		{
		  for (int j = 0; j < timesUsRepo.size(); j++)
		  {
			double expected = 2d * timesUsRepo.get(i) * timesUsRepo.get(j);
			assertEquals(s2.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s3 = sensiComputed.getSensitivity(RatesProviderDataSets.US_ISSUER_CURVE_1_NAME, USD).Sensitivity;
		assertEquals(s3.columnCount(), timesUsIssuer1.size());
		for (int i = 0; i < timesUsIssuer1.size(); i++)
		{
		  for (int j = 0; j < timesUsIssuer1.size(); j++)
		  {
			double expected = 2d * timesUsIssuer1.get(i) * timesUsIssuer1.get(j);
			assertEquals(s3.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 10d);
		  }
		}
		DoubleMatrix s4 = sensiComputed.getSensitivity(RatesProviderDataSets.US_ISSUER_CURVE_2_NAME, USD).Sensitivity;
		assertEquals(s4.columnCount(), timesUsIssuer2.size());
		for (int i = 0; i < timesUsIssuer2.size(); i++)
		{
		  for (int j = 0; j < timesUsIssuer2.size(); j++)
		  {
			double expected = 2d * timesUsIssuer2.get(i) * timesUsIssuer2.get(j);
			assertEquals(s4.get(i, j), expected, Math.Max(Math.Abs(expected), 1d) * EPS * 20d);
		  }
		}
	  }

	  //-------------------------------------------------------------------------
	  private CurrencyParameterSensitivities sensiFn(ImmutableRatesProvider provider)
	  {
		CurrencyParameterSensitivities sensi = CurrencyParameterSensitivities.empty();
		// Currency
		ImmutableMap<Currency, Curve> mapCurrency = provider.DiscountCurves;
		foreach (KeyValuePair<Currency, Curve> entry in mapCurrency.entrySet())
		{
		  InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
		  double sumSqrt = sum(provider);
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(curveInt.Name, USD, DoubleArray.of(curveInt.ParameterCount, i => 2d * sumSqrt * curveInt.XValues.get(i))));
		}
		// Index
		ImmutableMap<Index, Curve> mapIndex = provider.IndexCurves;
		foreach (KeyValuePair<Index, Curve> entry in mapIndex.entrySet())
		{
		  InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
		  double sumSqrt = sum(provider);
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(curveInt.Name, USD, DoubleArray.of(curveInt.ParameterCount, i => 2d * sumSqrt * curveInt.XValues.get(i))));
		}
		return sensi;
	  }

	  // modified sensitivity function - CombinedCurve involved
	  private CurrencyParameterSensitivities sensiCombinedFn(ImmutableRatesProvider provider)
	  {
		CurrencyParameterSensitivities sensi = CurrencyParameterSensitivities.empty();
		double sum = sumCombine(provider);
		// Currency
		ImmutableMap<Currency, Curve> mapCurrency = provider.DiscountCurves;
		foreach (KeyValuePair<Currency, Curve> entry in mapCurrency.entrySet())
		{
		  CombinedCurve curveComb = (CombinedCurve) entry.Value;
		  InterpolatedNodalCurve baseCurveInt = checkInterpolated(curveComb.BaseCurve);
		  InterpolatedNodalCurve spreadCurveInt = checkInterpolated(curveComb.SpreadCurve);
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(baseCurveInt.Name, USD, DoubleArray.of(baseCurveInt.ParameterCount, i => 2d * sum * baseCurveInt.XValues.get(i))));
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(spreadCurveInt.Name, USD, DoubleArray.of(spreadCurveInt.ParameterCount, i => 2d * sum * spreadCurveInt.XValues.get(i))));
		}
		// Index
		ImmutableMap<Index, Curve> mapIndex = provider.IndexCurves;
		foreach (KeyValuePair<Index, Curve> entry in mapIndex.entrySet())
		{
		  if (entry.Value is CombinedCurve)
		  {
			CombinedCurve curveComb = (CombinedCurve) entry.Value;
			InterpolatedNodalCurve baseCurveInt = checkInterpolated(curveComb.BaseCurve);
			InterpolatedNodalCurve spreadCurveInt = checkInterpolated(curveComb.SpreadCurve);
			sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(baseCurveInt.Name, USD, DoubleArray.of(baseCurveInt.ParameterCount, i => 2d * sum * baseCurveInt.XValues.get(i))));
			sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(spreadCurveInt.Name, USD, DoubleArray.of(spreadCurveInt.ParameterCount, i => 2d * sum * spreadCurveInt.XValues.get(i))));
		  }
		  else
		  {
			InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
			sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(curveInt.Name, USD, DoubleArray.of(curveInt.ParameterCount, i => 2d * sum * curveInt.XValues.get(i))));
		  }
		}
		return sensi;
	  }

	  // modified sensitivity function - sensitivities are computed only for ibor index curves
	  private CurrencyParameterSensitivities sensiModFn(ImmutableRatesProvider provider)
	  {
		CurrencyParameterSensitivities sensi = CurrencyParameterSensitivities.empty();
		// Index
		ImmutableMap<Index, Curve> mapIndex = provider.IndexCurves;
		foreach (KeyValuePair<Index, Curve> entry in mapIndex.entrySet())
		{
		  if (entry.Key is IborIndex)
		  {
			InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
			double sumSqrt = sumMod(provider);
			sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(curveInt.Name, USD, DoubleArray.of(curveInt.ParameterCount, i => 2d * sumSqrt * curveInt.XValues.get(i))));
		  }
		}
		return sensi;
	  }

	  private double sum(ImmutableRatesProvider provider)
	  {
		double result = 0.0;
		// Currency
		ImmutableMap<Currency, Curve> mapCurrency = provider.DiscountCurves;
		foreach (KeyValuePair<Currency, Curve> entry in mapCurrency.entrySet())
		{
		  InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
		  result += sumSingle(curveInt);
		}
		// Index
		ImmutableMap<Index, Curve> mapIndex = provider.IndexCurves;
		foreach (KeyValuePair<Index, Curve> entry in mapIndex.entrySet())
		{
		  InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
		  result += sumSingle(curveInt);
		}
		return result;
	  }

	  private double sumCombine(ImmutableRatesProvider provider)
	  {
		double result = 0.0;
		// Currency
		ImmutableMap<Currency, Curve> mapCurrency = provider.DiscountCurves;
		foreach (KeyValuePair<Currency, Curve> entry in mapCurrency.entrySet())
		{
		  if (entry.Value is CombinedCurve)
		  {
			InterpolatedNodalCurve baseCurveInt = checkInterpolated(entry.Value.Split().get(0));
			InterpolatedNodalCurve spreadCurveInt = checkInterpolated(entry.Value.Split().get(1));
			result += 0.25d * sumSingle(baseCurveInt);
			result += sumSingle(spreadCurveInt);
		  }
		  else
		  {
			InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
			result += sumSingle(curveInt);
		  }
		}
		// Index
		ImmutableMap<Index, Curve> mapIndex = provider.IndexCurves;
		foreach (KeyValuePair<Index, Curve> entry in mapIndex.entrySet())
		{
		  if (entry.Value is CombinedCurve)
		  {
			InterpolatedNodalCurve baseCurveInt = checkInterpolated(entry.Value.Split().get(0));
			InterpolatedNodalCurve spreadCurveInt = checkInterpolated(entry.Value.Split().get(1));
			result += 0.25d * sumSingle(baseCurveInt);
			result += sumSingle(spreadCurveInt);
		  }
		  else
		  {
			InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
			result += entry.Key.Equals(IborIndices.USD_LIBOR_3M) ? 0.25d * sumSingle(curveInt) : sumSingle(curveInt);
		  }
		}
		return result;
	  }

	  private double sumMod(ImmutableRatesProvider provider)
	  {
		double result = 0.0;
		// Index
		ImmutableMap<Index, Curve> mapIndex = provider.IndexCurves;
		foreach (KeyValuePair<Index, Curve> entry in mapIndex.entrySet())
		{
		  if (entry.Key is IborIndex)
		  {
			InterpolatedNodalCurve curveInt = checkInterpolated(entry.Value);
			result += sumSingle(curveInt);
		  }
		}
		return result;
	  }

	  // check that the curve is InterpolatedNodalCurve
	  private InterpolatedNodalCurve checkInterpolated(Curve curve)
	  {
		ArgChecker.isTrue(curve is InterpolatedNodalCurve, "Curve should be a InterpolatedNodalCurve");
		return (InterpolatedNodalCurve) curve;
	  }

	  //-------------------------------------------------------------------------
	  // computes diagonal part
	  private CurrencyParameterSensitivities sensitivityDiagonal(RatesProvider provider, System.Func<ImmutableRatesProvider, CurrencyAmount> valueFn)
	  {

		ImmutableRatesProvider immProv = provider.toImmutableRatesProvider();
		CurrencyAmount valueInit = valueFn(immProv);
		CurrencyParameterSensitivities discounting = sensitivity(immProv, immProv.DiscountCurves, (@base, bumped) => @base.toBuilder().discountCurves(bumped).build(), valueFn, valueInit);
		CurrencyParameterSensitivities forward = sensitivity(immProv, immProv.IndexCurves, (@base, bumped) => @base.toBuilder().indexCurves(bumped).build(), valueFn, valueInit);
		return discounting.combinedWith(forward);
	  }

	  // computes the sensitivity with respect to the curves
	  private CurrencyParameterSensitivities sensitivity<T>(ImmutableRatesProvider provider, IDictionary<T, Curve> baseCurves, System.Func<ImmutableRatesProvider, IDictionary<T, Curve>, ImmutableRatesProvider> storeBumpedFn, System.Func<ImmutableRatesProvider, CurrencyAmount> valueFn, CurrencyAmount valueInit)
	  {

		CurrencyParameterSensitivities result = CurrencyParameterSensitivities.empty();
		foreach (KeyValuePair<T, Curve> entry in baseCurves.SetOfKeyValuePairs())
		{
		  Curve curve = entry.Value;
		  DoubleArray sensitivity = DoubleArray.of(curve.ParameterCount, i =>
		  {
		  Curve dscUp = curve.withParameter(i, curve.getParameter(i) + EPS);
		  Curve dscDw = curve.withParameter(i, curve.getParameter(i) - EPS);
		  Dictionary<T, Curve> mapUp = new Dictionary<T, Curve>(baseCurves);
		  Dictionary<T, Curve> mapDw = new Dictionary<T, Curve>(baseCurves);
		  mapUp[entry.Key] = dscUp;
		  mapDw[entry.Key] = dscDw;
		  ImmutableRatesProvider providerUp = storeBumpedFn(provider, mapUp);
		  ImmutableRatesProvider providerDw = storeBumpedFn(provider, mapDw);
		  return (valueFn(providerUp).Amount + valueFn(providerDw).Amount - 2d * valueInit.Amount) / EPS / EPS;
		  });
		  result = result.combinedWith(curve.createParameterSensitivity(valueInit.Currency, sensitivity));
		}
		return result;
	  }

	  //-------------------------------------------------------------------------
	  private CurrencyParameterSensitivities sensiFnBond(ImmutableLegalEntityDiscountingProvider provider)
	  {
		CurrencyParameterSensitivities sensi = CurrencyParameterSensitivities.empty();
		double sum = this.sum(provider);
		// repo curves
		ImmutableMap<Pair<RepoGroup, Currency>, DiscountFactors> mapRepoCurves = provider.RepoCurves;
		foreach (KeyValuePair<Pair<RepoGroup, Currency>, DiscountFactors> entry in mapRepoCurves.entrySet())
		{
		  DiscountFactors discountFactors = entry.Value;
		  InterpolatedNodalCurve curve = (InterpolatedNodalCurve) getCurve(discountFactors);
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(curve.Name, discountFactors.Currency, DoubleArray.of(discountFactors.ParameterCount, i => 2d * curve.XValues.get(i) * sum)));
		}
		// issuer curves
		ImmutableMap<Pair<LegalEntityGroup, Currency>, DiscountFactors> mapIssuerCurves = provider.IssuerCurves;
		foreach (KeyValuePair<Pair<LegalEntityGroup, Currency>, DiscountFactors> entry in mapIssuerCurves.entrySet())
		{
		  DiscountFactors discountFactors = entry.Value;
		  InterpolatedNodalCurve curve = (InterpolatedNodalCurve) getCurve(discountFactors);
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(curve.Name, discountFactors.Currency, DoubleArray.of(discountFactors.ParameterCount, i => 2d * curve.XValues.get(i) * sum)));
		}
		return sensi;
	  }

	  // modified sensitivity function - CombinedCurve involved
	  private CurrencyParameterSensitivities sensiCombinedFnBond(ImmutableLegalEntityDiscountingProvider provider)
	  {
		CurrencyParameterSensitivities sensi = CurrencyParameterSensitivities.empty();
		double sum = sumCombine(provider);
		// repo curves
		ImmutableMap<Pair<RepoGroup, Currency>, DiscountFactors> mapCurrency = provider.RepoCurves;
		foreach (KeyValuePair<Pair<RepoGroup, Currency>, DiscountFactors> entry in mapCurrency.entrySet())
		{
		  CombinedCurve curveComb = (CombinedCurve) getCurve(entry.Value);
		  InterpolatedNodalCurve baseCurveInt = checkInterpolated(curveComb.BaseCurve);
		  InterpolatedNodalCurve spreadCurveInt = checkInterpolated(curveComb.SpreadCurve);
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(baseCurveInt.Name, USD, DoubleArray.of(baseCurveInt.ParameterCount, i => 2d * sum * baseCurveInt.XValues.get(i))));
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(spreadCurveInt.Name, USD, DoubleArray.of(spreadCurveInt.ParameterCount, i => 2d * sum * spreadCurveInt.XValues.get(i))));
		}
		// issuer curves
		ImmutableMap<Pair<LegalEntityGroup, Currency>, DiscountFactors> mapIndex = provider.IssuerCurves;
		foreach (KeyValuePair<Pair<LegalEntityGroup, Currency>, DiscountFactors> entry in mapIndex.entrySet())
		{
		  CombinedCurve curveComb = (CombinedCurve) getCurve(entry.Value);
		  InterpolatedNodalCurve baseCurveInt = checkInterpolated(curveComb.BaseCurve);
		  InterpolatedNodalCurve spreadCurveInt = checkInterpolated(curveComb.SpreadCurve);
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(baseCurveInt.Name, USD, DoubleArray.of(baseCurveInt.ParameterCount, i => 2d * sum * baseCurveInt.XValues.get(i))));
		  sensi = sensi.combinedWith(CurrencyParameterSensitivity.of(spreadCurveInt.Name, USD, DoubleArray.of(spreadCurveInt.ParameterCount, i => 2d * sum * spreadCurveInt.XValues.get(i))));
		}
		return sensi;
	  }

	  private double sumCombine(ImmutableLegalEntityDiscountingProvider provider)
	  {
		double result = 0d;
		// repo curves
		ImmutableMap<Pair<RepoGroup, Currency>, DiscountFactors> mapCurrency = provider.RepoCurves;
		foreach (KeyValuePair<Pair<RepoGroup, Currency>, DiscountFactors> entry in mapCurrency.entrySet())
		{
		  CombinedCurve curve = (CombinedCurve) getCurve(entry.Value);
		  result += sumSingle((InterpolatedNodalCurve) curve.split().get(0));
		  result += sumSingle((InterpolatedNodalCurve) curve.split().get(1));
		}
		// issuer curves
		ImmutableMap<Pair<LegalEntityGroup, Currency>, DiscountFactors> mapIndex = provider.IssuerCurves;
		foreach (KeyValuePair<Pair<LegalEntityGroup, Currency>, DiscountFactors> entry in mapIndex.entrySet())
		{
		  CombinedCurve curve = (CombinedCurve) getCurve(entry.Value);
		  result += sumSingle((InterpolatedNodalCurve) curve.split().get(0));
		  result += sumSingle((InterpolatedNodalCurve) curve.split().get(1));
		}
		return result;
	  }

	  private double sum(ImmutableLegalEntityDiscountingProvider provider)
	  {
		double result = 0d;
		// repo curves
		ImmutableMap<Pair<RepoGroup, Currency>, DiscountFactors> mapIndex = provider.RepoCurves;
		foreach (KeyValuePair<Pair<RepoGroup, Currency>, DiscountFactors> entry in mapIndex.entrySet())
		{
		  InterpolatedNodalCurve curve = (InterpolatedNodalCurve) getCurve(entry.Value);
		  result += sumSingle(curve);
		}
		// issuer curves
		ImmutableMap<Pair<LegalEntityGroup, Currency>, DiscountFactors> mapCurrency = provider.IssuerCurves;
		foreach (KeyValuePair<Pair<LegalEntityGroup, Currency>, DiscountFactors> entry in mapCurrency.entrySet())
		{
		  InterpolatedNodalCurve curve = (InterpolatedNodalCurve) getCurve(entry.Value);
		  result += sumSingle(curve);
		}
		return result;
	  }

	  private double sumSingle(InterpolatedNodalCurve interpolatedNodalCurve)
	  {
		double result = 0.0;
		int nbNodePoints = interpolatedNodalCurve.ParameterCount;
		for (int i = 0; i < nbNodePoints; i++)
		{
		  result += interpolatedNodalCurve.XValues.get(i) * interpolatedNodalCurve.YValues.get(i);
		}
		return result;
	  }

	  private Curve getCurve(DiscountFactors discountFactors)
	  {
		return ((ZeroRateDiscountFactors) discountFactors).Curve;
	  }

	}

}