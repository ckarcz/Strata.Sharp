﻿using System;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.bond
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.currency.Currency.GBP;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.currency.Currency.JPY;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.currency.Currency.USD;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.DayCounts.ACT_ACT_ICMA;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.DayCounts.NL_365;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.HolidayCalendarIds.GBLO;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.HolidayCalendarIds.JPTO;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.HolidayCalendarIds.USNY;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.index.PriceIndices.GB_RPI;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.index.PriceIndices.JP_CPI_EXF;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.index.PriceIndices.US_CPI_U;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.pricer.CompoundedRateType.CONTINUOUS;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.pricer.CompoundedRateType.PERIODIC;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.bond.CapitalIndexedBondYieldConvention.GB_IL_BOND;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.bond.CapitalIndexedBondYieldConvention.GB_IL_FLOAT;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.bond.CapitalIndexedBondYieldConvention.JP_IL_COMPOUND;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.bond.CapitalIndexedBondYieldConvention.JP_IL_SIMPLE;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.bond.CapitalIndexedBondYieldConvention.US_IL_REAL;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.swap.PriceIndexCalculationMethod.INTERPOLATED;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.swap.PriceIndexCalculationMethod.INTERPOLATED_JAPAN;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.swap.PriceIndexCalculationMethod.MONTHLY;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertTrue;


	using Test = org.testng.annotations.Test;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using CurrencyAmount = com.opengamma.strata.basics.currency.CurrencyAmount;
	using MultiCurrencyAmount = com.opengamma.strata.basics.currency.MultiCurrencyAmount;
	using BusinessDayAdjustment = com.opengamma.strata.basics.date.BusinessDayAdjustment;
	using BusinessDayConventions = com.opengamma.strata.basics.date.BusinessDayConventions;
	using DaysAdjustment = com.opengamma.strata.basics.date.DaysAdjustment;
	using Frequency = com.opengamma.strata.basics.schedule.Frequency;
	using PeriodicSchedule = com.opengamma.strata.basics.schedule.PeriodicSchedule;
	using RollConventions = com.opengamma.strata.basics.schedule.RollConventions;
	using Schedule = com.opengamma.strata.basics.schedule.Schedule;
	using StubConvention = com.opengamma.strata.basics.schedule.StubConvention;
	using ValueSchedule = com.opengamma.strata.basics.value.ValueSchedule;
	using LocalDateDoubleTimeSeries = com.opengamma.strata.collect.timeseries.LocalDateDoubleTimeSeries;
	using CurrencyParameterSensitivities = com.opengamma.strata.market.param.CurrencyParameterSensitivities;
	using PointSensitivities = com.opengamma.strata.market.sensitivity.PointSensitivities;
	using ImmutableRatesProvider = com.opengamma.strata.pricer.rate.ImmutableRatesProvider;
	using RateComputationFn = com.opengamma.strata.pricer.rate.RateComputationFn;
	using RatesFiniteDifferenceSensitivityCalculator = com.opengamma.strata.pricer.sensitivity.RatesFiniteDifferenceSensitivityCalculator;
	using LegalEntityId = com.opengamma.strata.product.LegalEntityId;
	using SecurityId = com.opengamma.strata.product.SecurityId;
	using CapitalIndexedBond = com.opengamma.strata.product.bond.CapitalIndexedBond;
	using CapitalIndexedBondPaymentPeriod = com.opengamma.strata.product.bond.CapitalIndexedBondPaymentPeriod;
	using ResolvedCapitalIndexedBond = com.opengamma.strata.product.bond.ResolvedCapitalIndexedBond;
	using RateComputation = com.opengamma.strata.product.rate.RateComputation;
	using InflationRateCalculation = com.opengamma.strata.product.swap.InflationRateCalculation;

	/// <summary>
	/// Test <seealso cref="DiscountingCapitalIndexedBondProductPricer"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class DiscountingCapitalIndexedBondProductPricerTest
	public class DiscountingCapitalIndexedBondProductPricerTest
	{

	  private static readonly ReferenceData REF_DATA = ReferenceData.standard();
	  private const double NOTIONAL = 10_000_000d;
	  private const double START_INDEX = 198.47742;
	  private const double REAL_COUPON_VALUE = 0.01;
	  private static readonly ValueSchedule REAL_COUPON = ValueSchedule.of(REAL_COUPON_VALUE);
	  private static readonly InflationRateCalculation RATE_CALC = InflationRateCalculation.builder().gearing(REAL_COUPON).index(US_CPI_U).lag(Period.ofMonths(3)).indexCalculationMethod(INTERPOLATED).firstIndexValue(START_INDEX).build();
	  private static readonly BusinessDayAdjustment EX_COUPON_ADJ = BusinessDayAdjustment.of(BusinessDayConventions.PRECEDING, USNY);
	  private static readonly DaysAdjustment SETTLE_OFFSET = DaysAdjustment.ofBusinessDays(2, USNY);
	  private static readonly LegalEntityId LEGAL_ENTITY = CapitalIndexedBondCurveDataSet.IssuerId;
	  private static readonly LocalDate START = LocalDate.of(2006, 1, 15);
	  private static readonly LocalDate END = LocalDate.of(2016, 1, 15);
	  private static readonly Frequency FREQUENCY = Frequency.P6M;
	  private static readonly BusinessDayAdjustment BUSINESS_ADJUST = BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, USNY);
	  private static readonly PeriodicSchedule SCHEDULE = PeriodicSchedule.of(START, END, FREQUENCY, BUSINESS_ADJUST, StubConvention.NONE, RollConventions.NONE);
	  private static readonly SecurityId SECURITY_ID = SecurityId.of("OG-Ticker", "BOND1");
	  private static readonly ResolvedCapitalIndexedBond PRODUCT = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NOTIONAL).currency(USD).dayCount(ACT_ACT_ICMA).rateCalculation(RATE_CALC).legalEntityId(LEGAL_ENTITY).yieldConvention(US_IL_REAL).settlementDateOffset(SETTLE_OFFSET).accrualSchedule(SCHEDULE).build().resolve(REF_DATA);
	  private static readonly DaysAdjustment EX_COUPON = DaysAdjustment.ofCalendarDays(-5, EX_COUPON_ADJ);
	  private static readonly ResolvedCapitalIndexedBond PRODUCT_EX_COUPON = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NOTIONAL).currency(USD).dayCount(ACT_ACT_ICMA).rateCalculation(RATE_CALC).legalEntityId(LEGAL_ENTITY).yieldConvention(US_IL_REAL).settlementDateOffset(SETTLE_OFFSET).accrualSchedule(SCHEDULE).exCouponPeriod(EX_COUPON).build().resolve(REF_DATA);
	  // detachment date (for nonzero ex-coupon days) < valuation date < payment date
	  private static readonly LocalDate VALUATION = LocalDate.of(2014, 7, 10);
	  private static readonly LocalDateDoubleTimeSeries TS = CapitalIndexedBondCurveDataSet.getTimeSeries(VALUATION);
	  private static readonly ImmutableRatesProvider RATES_PROVIDER = CapitalIndexedBondCurveDataSet.getRatesProvider(VALUATION, TS);
	  private static readonly LegalEntityDiscountingProvider ISSUER_RATES_PROVIDER = CapitalIndexedBondCurveDataSet.getLegalEntityDiscountingProvider(VALUATION);
	  private static readonly IssuerCurveDiscountFactors ISSUER_DISCOUNT_FACTORS = CapitalIndexedBondCurveDataSet.getIssuerCurveDiscountFactors(VALUATION);
	  // valuation date = payment date
	  private static readonly LocalDate VALUATION_ON_PAY = LocalDate.of(2014, 1, 15);
	  private static readonly LocalDateDoubleTimeSeries TS_ON_PAY = CapitalIndexedBondCurveDataSet.getTimeSeries(VALUATION_ON_PAY);
	  private static readonly ImmutableRatesProvider RATES_PROVIDER_ON_PAY = CapitalIndexedBondCurveDataSet.getRatesProvider(VALUATION_ON_PAY, TS_ON_PAY);

	  private const double Z_SPREAD = 0.015;
	  private const int PERIOD_PER_YEAR = 4;

	  private const double TOL = 1.0e-12;
	  private const double EPS = 1.0e-6;
	  private static readonly DiscountingCapitalIndexedBondProductPricer PRICER = DiscountingCapitalIndexedBondProductPricer.DEFAULT;
	  private static readonly DiscountingCapitalIndexedBondPaymentPeriodPricer PERIOD_PRICER = DiscountingCapitalIndexedBondPaymentPeriodPricer.DEFAULT;
	  private static readonly RatesFiniteDifferenceSensitivityCalculator FD_CAL = new RatesFiniteDifferenceSensitivityCalculator(EPS);

	  //-------------------------------------------------------------------------
	  public virtual void test_getter()
	  {
		assertEquals(PRICER.PeriodPricer, PERIOD_PRICER);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_presentValue()
	  {
		CurrencyAmount computed = PRICER.presentValue(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER);
		double expected = PERIOD_PRICER.presentValue(PRODUCT.NominalPayment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS);
		int size = PRODUCT.PeriodicPayments.size();
		for (int i = 16; i < size; ++i)
		{
		  CapitalIndexedBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  expected += PERIOD_PRICER.presentValue(payment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS);
		}
		assertEquals(computed.Amount, expected, TOL * NOTIONAL);
	  }

	  public virtual void test_presentValue_exCoupon()
	  {
		CurrencyAmount computed = PRICER.presentValue(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER);
		double expected = PERIOD_PRICER.presentValue(PRODUCT_EX_COUPON.NominalPayment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS);
		int size = PRODUCT_EX_COUPON.PeriodicPayments.size();
		for (int i = 17; i < size; ++i)
		{ // in ex-coupon period
		  CapitalIndexedBondPaymentPeriod payment = PRODUCT_EX_COUPON.PeriodicPayments.get(i);
		  expected += PERIOD_PRICER.presentValue(payment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS);
		}
		assertEquals(computed.Amount, expected, TOL * NOTIONAL);
	  }

	  public virtual void test_presentValueWithZSpread()
	  {
		CurrencyAmount computed = PRICER.presentValueWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double expected = PERIOD_PRICER.presentValueWithZSpread(PRODUCT.NominalPayment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		int size = PRODUCT.PeriodicPayments.size();
		for (int i = 16; i < size; ++i)
		{
		  CapitalIndexedBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  expected += PERIOD_PRICER.presentValueWithZSpread(payment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		}
		assertEquals(computed.Amount, expected, TOL * NOTIONAL);
	  }

	  public virtual void test_presentValueWithZSpread_exCoupon()
	  {
		CurrencyAmount computed = PRICER.presentValueWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		double expected = PERIOD_PRICER.presentValueWithZSpread(PRODUCT_EX_COUPON.NominalPayment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS, Z_SPREAD, CONTINUOUS, 0);
		int size = PRODUCT_EX_COUPON.PeriodicPayments.size();
		for (int i = 17; i < size; ++i)
		{ // in ex-coupon period
		  CapitalIndexedBondPaymentPeriod payment = PRODUCT_EX_COUPON.PeriodicPayments.get(i);
		  expected += PERIOD_PRICER.presentValueWithZSpread(payment, RATES_PROVIDER, ISSUER_DISCOUNT_FACTORS, Z_SPREAD, CONTINUOUS, 0);
		}
		assertEquals(computed.Amount, expected, TOL * NOTIONAL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_presentValueSensitivity()
	  {
		PointSensitivities point = PRICER.presentValueSensitivity(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPvSensitivity(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  public virtual void test_presentValueSensitivity_exCoupon()
	  {
		PointSensitivities point = PRICER.presentValueSensitivity(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPvSensitivity(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  public virtual void test_presentValueSensitivityWithZSpread()
	  {
		PointSensitivities point = PRICER.presentValueSensitivityWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, CONTINUOUS, 0).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPvSensitivityWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  public virtual void test_presentValueSensitivityWithZSpread_exCoupon()
	  {
		PointSensitivities point = PRICER.presentValueSensitivityWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPvSensitivityWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_zSpreadFromCurvesAndPv()
	  {
		CurrencyAmount pv = PRICER.presentValueWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double computed = PRICER.zSpreadFromCurvesAndPv(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA, pv, PERIODIC, PERIOD_PER_YEAR);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  public virtual void test_zSpreadFromCurvesAndPv_exCoupon()
	  {
		CurrencyAmount pv = PRICER.presentValueWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		double computed = PRICER.zSpreadFromCurvesAndPv(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA, pv, CONTINUOUS, 0);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_dirtyNominalPriceFromCurves()
	  {
		double computed = PRICER.dirtyNominalPriceFromCurves(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA);
		LocalDate settlement = SETTLE_OFFSET.adjust(VALUATION, REF_DATA);
		double df = ISSUER_RATES_PROVIDER.repoCurveDiscountFactors(SECURITY_ID, LEGAL_ENTITY, USD).discountFactor(settlement);
		double expected = PRICER.presentValue(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, settlement).Amount / NOTIONAL / df;
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void test_dirtyNominalPriceFromCurves_exCoupon()
	  {
		double computed = PRICER.dirtyNominalPriceFromCurves(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA);
		LocalDate settlement = SETTLE_OFFSET.adjust(VALUATION, REF_DATA);
		double df = ISSUER_RATES_PROVIDER.repoCurveDiscountFactors(SECURITY_ID, LEGAL_ENTITY, USD).discountFactor(settlement);
		double expected = PRICER.presentValue(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, settlement).Amount / NOTIONAL / df;
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void test_dirtyNominalPriceFromCurvesWithZSpread()
	  {
		double computed = PRICER.dirtyNominalPriceFromCurvesWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA, Z_SPREAD, CONTINUOUS, 0);
		LocalDate settlement = SETTLE_OFFSET.adjust(VALUATION, REF_DATA);
		double df = ISSUER_RATES_PROVIDER.repoCurveDiscountFactors(SECURITY_ID, LEGAL_ENTITY, USD).discountFactor(settlement);
		double expected = PRICER.presentValueWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, settlement, Z_SPREAD, CONTINUOUS, 0).Amount / NOTIONAL / df;
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void test_dirtyNominalPriceFromCurvesWithZSpread_exCoupon()
	  {
		double computed = PRICER.dirtyNominalPriceFromCurvesWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		LocalDate settlement = SETTLE_OFFSET.adjust(VALUATION, REF_DATA);
		double df = ISSUER_RATES_PROVIDER.repoCurveDiscountFactors(SECURITY_ID, LEGAL_ENTITY, USD).discountFactor(settlement);
		double expected = PRICER.presentValueWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, settlement, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR).Amount / NOTIONAL / df;
		assertEquals(computed, expected, TOL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_dirtyPriceNominalPriceFromCurvesSensitivity()
	  {
		PointSensitivities point = PRICER.dirtyNominalPriceSensitivity(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPriceSensitivity(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  public virtual void test_dirtyPriceNominalPriceFromCurvesSensitivity_exCoupon()
	  {
		PointSensitivities point = PRICER.dirtyNominalPriceSensitivity(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPriceSensitivity(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  public virtual void test_dirtyPriceNominalPriceFromCurvesSensitivityWithZSpread()
	  {
		PointSensitivities point = PRICER.dirtyNominalPriceSensitivityWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPriceSensitivityWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  public virtual void test_dirtyPriceNominalPriceFromCurvesSensitivityWithZSpread_exCoupon()
	  {
		PointSensitivities point = PRICER.dirtyNominalPriceSensitivityWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, REF_DATA, Z_SPREAD, CONTINUOUS, 0).build();
		CurrencyParameterSensitivities computed1 = RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities computed2 = ISSUER_RATES_PROVIDER.parameterSensitivity(point);
		CurrencyParameterSensitivities expected = fdPriceSensitivityWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		assertTrue(expected.equalWithTolerance(computed1.combinedWith(computed2), EPS * NOTIONAL));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_currencyExposure()
	  {
		MultiCurrencyAmount computed = PRICER.currencyExposure(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, VALUATION);
		PointSensitivities point = PRICER.presentValueSensitivity(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER).build();
		MultiCurrencyAmount expected = RATES_PROVIDER.currencyExposure(point).plus(PRICER.presentValue(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER));
		assertEquals(computed.Currencies.size(), 1);
		assertEquals(computed.getAmount(USD).Amount, expected.getAmount(USD).Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_currencyExposure_exCoupon()
	  {
		MultiCurrencyAmount computed = PRICER.currencyExposure(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, VALUATION);
		PointSensitivities point = PRICER.presentValueSensitivity(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER).build();
		MultiCurrencyAmount expected = RATES_PROVIDER.currencyExposure(point).plus(PRICER.presentValue(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER));
		assertEquals(computed.Currencies.size(), 1);
		assertEquals(computed.getAmount(USD).Amount, expected.getAmount(USD).Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_currencyExposureWithZSpread()
	  {
		MultiCurrencyAmount computed = PRICER.currencyExposureWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, VALUATION, Z_SPREAD, CONTINUOUS, 0);
		PointSensitivities point = PRICER.presentValueSensitivityWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, CONTINUOUS, 0).build();
		MultiCurrencyAmount expected = RATES_PROVIDER.currencyExposure(point).plus(PRICER.presentValueWithZSpread(PRODUCT, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, CONTINUOUS, 0));
		assertEquals(computed.Currencies.size(), 1);
		assertEquals(computed.getAmount(USD).Amount, expected.getAmount(USD).Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_currencyExposureWithZSpread_exCoupon()
	  {
		MultiCurrencyAmount computed = PRICER.currencyExposureWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, VALUATION, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		PointSensitivities point = PRICER.presentValueSensitivityWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR).build();
		MultiCurrencyAmount expected = RATES_PROVIDER.currencyExposure(point).plus(PRICER.presentValueWithZSpread(PRODUCT_EX_COUPON, RATES_PROVIDER, ISSUER_RATES_PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR));
		assertEquals(computed.Currencies.size(), 1);
		assertEquals(computed.getAmount(USD).Amount, expected.getAmount(USD).Amount, NOTIONAL * TOL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_currentCash()
	  {
		CurrencyAmount computed = PRICER.currentCash(PRODUCT, RATES_PROVIDER, VALUATION);
		assertEquals(computed.Amount, 0d);
	  }

	  public virtual void test_currentCash_exCoupon()
	  {
		CurrencyAmount computed = PRICER.currentCash(PRODUCT_EX_COUPON, RATES_PROVIDER, VALUATION);
		assertEquals(computed.Amount, 0d);
	  }

	  public virtual void test_currentCash_onPayment()
	  {
		CurrencyAmount computed = PRICER.currentCash(PRODUCT, RATES_PROVIDER_ON_PAY, VALUATION_ON_PAY.minusDays(7));
		double expected = PERIOD_PRICER.forecastValue(PRODUCT.PeriodicPayments.get(15), RATES_PROVIDER_ON_PAY);
		assertEquals(computed.Amount, expected);
	  }

	  public virtual void test_currentCash_onPayment_exCoupon()
	  {
		CurrencyAmount computed = PRICER.currentCash(PRODUCT_EX_COUPON, RATES_PROVIDER_ON_PAY, VALUATION_ON_PAY);
		assertEquals(computed.Amount, 0d);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_dirtyPriceFromStandardYield()
	  {
		double yield = 0.0175;
		LocalDate standardSettle = SETTLE_OFFSET.adjust(VALUATION, REF_DATA);
		double computed = PRICER.dirtyPriceFromStandardYield(PRODUCT, RATES_PROVIDER, standardSettle, yield);
		Schedule sch = SCHEDULE.createSchedule(REF_DATA).toUnadjusted();
		CapitalIndexedBondPaymentPeriod period = PRODUCT.PeriodicPayments.get(16);
		double factorPeriod = ACT_ACT_ICMA.relativeYearFraction(period.UnadjustedStartDate, period.UnadjustedEndDate, sch);
		double factorSpot = ACT_ACT_ICMA.relativeYearFraction(period.UnadjustedStartDate, standardSettle, sch);
		double factorToNext = (factorPeriod - factorSpot) / factorPeriod;
		double dscFactor = 1d / (1d + 0.5 * yield);
		double expected = Math.Pow(dscFactor, 3);
		for (int i = 0; i < 4; ++i)
		{
		  expected += REAL_COUPON_VALUE * Math.Pow(dscFactor, i);
		}
		expected *= Math.Pow(dscFactor, factorToNext);
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void test_modifiedDurationFromStandardYield()
	  {
		double yield = 0.0175;
		LocalDate standardSettle = SETTLE_OFFSET.adjust(VALUATION, REF_DATA);
		double computed = PRICER.modifiedDurationFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield);
		double price = PRICER.dirtyPriceFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield);
		double up = PRICER.dirtyPriceFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield + EPS);
		double dw = PRICER.dirtyPriceFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield - EPS);
		double expected = -0.5 * (up - dw) / price / EPS;
		assertEquals(computed, expected, EPS);

	  }

	  public virtual void test_convexityFromStandardYield()
	  {
		double yield = 0.0175;
		LocalDate standardSettle = SETTLE_OFFSET.adjust(VALUATION, REF_DATA);
		double computed = PRICER.convexityFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield);
		double md = PRICER.modifiedDurationFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield);
		double up = PRICER.modifiedDurationFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield + EPS);
		double dw = PRICER.modifiedDurationFromStandardYield(PRODUCT_EX_COUPON, RATES_PROVIDER, standardSettle, yield - EPS);
		double expected = -0.5 * (up - dw) / EPS + md * md;
		assertEquals(computed, expected, EPS);
		double computed1 = PRICER.convexityFromStandardYield(PRODUCT, RATES_PROVIDER, VALUATION, yield);
		double md1 = PRICER.modifiedDurationFromStandardYield(PRODUCT, RATES_PROVIDER, VALUATION, yield);
		double up1 = PRICER.modifiedDurationFromStandardYield(PRODUCT, RATES_PROVIDER, VALUATION, yield + EPS);
		double dw1 = PRICER.modifiedDurationFromStandardYield(PRODUCT, RATES_PROVIDER, VALUATION, yield - EPS);
		double expected1 = -0.5 * (up1 - dw1) / EPS + md1 * md1;
		assertEquals(computed1, expected1, EPS);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_accruedInterest()
	  {
		LocalDate refDate = LocalDate.of(2014, 6, 10);
		double computed = PRODUCT.accruedInterest(refDate);
		Schedule sch = SCHEDULE.createSchedule(REF_DATA).toUnadjusted();
		CapitalIndexedBondPaymentPeriod period = PRODUCT.PeriodicPayments.get(16);
		double factor = ACT_ACT_ICMA.relativeYearFraction(period.UnadjustedStartDate, refDate, sch);
		assertEquals(computed, factor * REAL_COUPON_VALUE * NOTIONAL * 2d, TOL * REAL_COUPON_VALUE * NOTIONAL);
	  }

	  public virtual void test_accruedInterest_onPayment()
	  {
		CapitalIndexedBondPaymentPeriod period = PRODUCT.PeriodicPayments.get(16);
		LocalDate refDate = period.PaymentDate;
		double computed = PRODUCT.accruedInterest(refDate);
		assertEquals(computed, 0d, TOL * REAL_COUPON_VALUE * NOTIONAL);
	  }

	  public virtual void test_accruedInterest_before()
	  {
		LocalDate refDate = LocalDate.of(2003, 1, 22);
		double computed = PRODUCT.accruedInterest(refDate);
		assertEquals(computed, 0d, TOL * REAL_COUPON_VALUE * NOTIONAL);
	  }

	  public virtual void test_accruedInterest_exCoupon_in()
	  {
		CapitalIndexedBondPaymentPeriod period = PRODUCT_EX_COUPON.PeriodicPayments.get(16);
		LocalDate refDate = period.DetachmentDate;
		double computed = PRODUCT_EX_COUPON.accruedInterest(refDate);
		Schedule sch = SCHEDULE.createSchedule(REF_DATA).toUnadjusted();
		double factor = ACT_ACT_ICMA.relativeYearFraction(period.UnadjustedStartDate, refDate, sch);
		double factorTotal = ACT_ACT_ICMA.relativeYearFraction(period.UnadjustedStartDate, period.UnadjustedEndDate, sch);
		assertEquals(computed, (factor - factorTotal) * REAL_COUPON_VALUE * NOTIONAL * 2d, TOL * REAL_COUPON_VALUE * NOTIONAL);
	  }

	  public virtual void test_accruedInterest_exCoupon_out()
	  {
		LocalDate refDate = LocalDate.of(2014, 6, 10);
		CapitalIndexedBondPaymentPeriod period = PRODUCT_EX_COUPON.PeriodicPayments.get(16);
		double computed = PRODUCT_EX_COUPON.accruedInterest(refDate);
		Schedule sch = SCHEDULE.createSchedule(REF_DATA).toUnadjusted();
		double factor = ACT_ACT_ICMA.relativeYearFraction(period.UnadjustedStartDate, refDate, sch);
		assertEquals(computed, factor * REAL_COUPON_VALUE * NOTIONAL * 2d, TOL * REAL_COUPON_VALUE * NOTIONAL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_cleanRealPrice_dirtyRealPrice()
	  {
		double dirtyRealPrice = 1.055;
		LocalDate refDate = LocalDate.of(2014, 6, 10);
		double cleanRealPrice = PRICER.cleanRealPriceFromDirtyRealPrice(PRODUCT, refDate, dirtyRealPrice);
		double expected = dirtyRealPrice - PRODUCT.accruedInterest(refDate) / NOTIONAL;
		assertEquals(cleanRealPrice, expected, TOL);
		assertEquals(PRICER.dirtyRealPriceFromCleanRealPrice(PRODUCT, refDate, cleanRealPrice), dirtyRealPrice, TOL);
	  }

	  public virtual void test_realPrice_nominalPrice_settleBefore()
	  {
		double realPrice = 1.055;
		LocalDate refDate = LocalDate.of(2014, 6, 10);
		double nominalPrice = PRICER.nominalPriceFromRealPrice(PRODUCT, RATES_PROVIDER_ON_PAY, refDate, realPrice);
		RateComputation obs = RATE_CALC.createRateComputation(refDate);
		double refRate = RateComputationFn.standard().rate(obs, null, null, RATES_PROVIDER_ON_PAY);
		double expected = realPrice * (refRate + 1d);
		assertEquals(nominalPrice, expected, TOL);
		assertEquals(PRICER.realPriceFromNominalPrice(PRODUCT, RATES_PROVIDER_ON_PAY, refDate, nominalPrice), realPrice, TOL);
	  }

	  public virtual void test_realPrice_nominalPrice_settleAfter()
	  {
		double realPrice = 1.055;
		LocalDate refDate = LocalDate.of(2014, 6, 10);
		double nominalPrice = PRICER.nominalPriceFromRealPrice(PRODUCT, RATES_PROVIDER, refDate, realPrice);
		RateComputation obs = RATE_CALC.createRateComputation(VALUATION);
		double refRate = RateComputationFn.standard().rate(obs, null, null, RATES_PROVIDER);
		double expected = realPrice * (refRate + 1d);
		assertEquals(nominalPrice, expected, TOL);
		assertEquals(PRICER.realPriceFromNominalPrice(PRODUCT, RATES_PROVIDER, refDate, nominalPrice), realPrice, TOL);
	  }

	  public virtual void test_cleanNominalPrice_dirtyNominalPrice()
	  {
		double dirtyNominalPrice = 1.055;
		LocalDate refDate = LocalDate.of(2014, 6, 10);
		double cleanNominalPrice = PRICER.cleanNominalPriceFromDirtyNominalPrice(PRODUCT, RATES_PROVIDER, refDate, dirtyNominalPrice);
		RateComputation obs = RATE_CALC.createRateComputation(VALUATION);
		double refRate = RateComputationFn.standard().rate(obs, null, null, RATES_PROVIDER);
		double expected = dirtyNominalPrice - PRODUCT.accruedInterest(refDate) * (refRate + 1d) / NOTIONAL;
		assertEquals(cleanNominalPrice, expected, TOL);
		assertEquals(PRICER.dirtyNominalPriceFromCleanNominalPrice(PRODUCT, RATES_PROVIDER, refDate, cleanNominalPrice), dirtyNominalPrice, TOL);
	  }

	  //-------------------------------------------------------------------------
	  private const double NTNL = 1_000_000d;
	  private static readonly LocalDate VAL_DATE = LocalDate.of(2016, 2, 29);
	  private static readonly ImmutableRatesProvider RATES_PROVS_US = CapitalIndexedBondCurveDataSet.getRatesProvider(VAL_DATE, CapitalIndexedBondCurveDataSet.getTimeSeries(VAL_DATE));
	  private static readonly LegalEntityDiscountingProvider ISSUER_PROVS_US = CapitalIndexedBondCurveDataSet.getLegalEntityDiscountingProvider(VAL_DATE);

	  private const double START_INDEX_US = 218.085;
	  private const double CPN_VALUE_US = 0.0125 * 0.5;
	  private static readonly ValueSchedule CPN_US = ValueSchedule.of(CPN_VALUE_US);
	  private static readonly InflationRateCalculation RATE_CALC_US = InflationRateCalculation.builder().gearing(CPN_US).index(US_CPI_U).lag(Period.ofMonths(3)).indexCalculationMethod(INTERPOLATED).firstIndexValue(START_INDEX_US).build();
	  private static readonly LocalDate START_USD = LocalDate.of(2010, 7, 15);
	  private static readonly LocalDate END_USD = LocalDate.of(2020, 7, 15);
	  private static readonly DaysAdjustment SETTLE_OFFSET_US = DaysAdjustment.ofBusinessDays(1, USNY);
	  private static readonly PeriodicSchedule SCHEDULE_US = PeriodicSchedule.of(START_USD, END_USD, FREQUENCY, BUSINESS_ADJUST, StubConvention.NONE, RollConventions.NONE);
	  private static readonly ResolvedCapitalIndexedBond PRODUCT_US = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NTNL).currency(USD).dayCount(ACT_ACT_ICMA).rateCalculation(RATE_CALC_US).legalEntityId(LEGAL_ENTITY).yieldConvention(US_IL_REAL).settlementDateOffset(SETTLE_OFFSET_US).accrualSchedule(SCHEDULE_US).build().resolve(REF_DATA);
	  private const double YIELD_US = -0.00189;

	  public virtual void test_priceFromRealYield_us()
	  {
		LocalDate standardSettle = PRODUCT_US.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double computed = PRICER.cleanPriceFromRealYield(PRODUCT_US, RATES_PROVS_US, standardSettle, YIELD_US);
		assertEquals(computed, 1.06, 1.e-2);
		double computedSmall = PRICER.cleanPriceFromRealYield(PRODUCT_US, RATES_PROVS_US, standardSettle, 0.0);
		assertEquals(computedSmall, 1.05, 1.e-2);
		double dirtyPrice = PRICER.dirtyPriceFromRealYield(PRODUCT_US, RATES_PROVS_US, standardSettle, YIELD_US);
		double cleanPrice = PRICER.cleanRealPriceFromDirtyRealPrice(PRODUCT_US, standardSettle, dirtyPrice);
		assertEquals(computed, cleanPrice);
		double yieldRe = PRICER.realYieldFromDirtyPrice(PRODUCT_US, RATES_PROVS_US, standardSettle, dirtyPrice);
		assertEquals(yieldRe, YIELD_US, TOL);
	  }

	  public virtual void test_modifiedDuration_convexity_us()
	  {
		double eps = 1.0e-5;
		LocalDate standardSettle = PRODUCT_US.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double mdComputed = PRICER.modifiedDurationFromRealYieldFiniteDifference(PRODUCT_US, RATES_PROVS_US, standardSettle, YIELD_US);
		double cvComputed = PRICER.convexityFromRealYieldFiniteDifference(PRODUCT_US, RATES_PROVS_US, standardSettle, YIELD_US);
		double price = PRICER.cleanPriceFromRealYield(PRODUCT_US, RATES_PROVS_US, standardSettle, YIELD_US);
		double up = PRICER.cleanPriceFromRealYield(PRODUCT_US, RATES_PROVS_US, standardSettle, YIELD_US + eps);
		double dw = PRICER.cleanPriceFromRealYield(PRODUCT_US, RATES_PROVS_US, standardSettle, YIELD_US - eps);
		assertEquals(mdComputed, 0.5 * (dw - up) / eps / price, eps);
		assertEquals(cvComputed, (up + dw - 2d * price) / price / eps / eps, eps);
	  }

	  public virtual void test_realYieldFromCurves_us()
	  {
		LocalDate standardSettle = PRODUCT_US.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double computed = PRICER.realYieldFromCurves(PRODUCT_US, RATES_PROVS_US, ISSUER_PROVS_US, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurves(PRODUCT_US, RATES_PROVS_US, ISSUER_PROVS_US, REF_DATA);
		double dirtyRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_US, RATES_PROVS_US, standardSettle, dirtyNominalPrice);
		double expected = PRICER.realYieldFromDirtyPrice(PRODUCT_US, RATES_PROVS_US, standardSettle, dirtyRealPrice);
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void zSpreadFromCurvesAndCleanPrice_us()
	  {
		LocalDate standardSettle = PRODUCT_US.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurvesWithZSpread(PRODUCT_US, RATES_PROVS_US, ISSUER_PROVS_US, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double cleanRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_US, RATES_PROVS_US, standardSettle, PRICER.cleanNominalPriceFromDirtyNominalPrice(PRODUCT_US, RATES_PROVS_US, standardSettle, dirtyNominalPrice));
		double computed = PRICER.zSpreadFromCurvesAndCleanPrice(PRODUCT_US, RATES_PROVS_US, ISSUER_PROVS_US, REF_DATA, cleanRealPrice, PERIODIC, PERIOD_PER_YEAR);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  public virtual void test_accruedInterest_us()
	  {
		double accPositive = PRODUCT_US.accruedInterest(LocalDate.of(2016, 7, 14));
		assertEquals(accPositive, 6216d, 1.0);
		double accZero = PRODUCT_US.accruedInterest(LocalDate.of(2016, 7, 15));
		assertEquals(accZero, 0d);
	  }

	  //-------------------------------------------------------------------------
	  private static readonly LocalDate VAL_DATE_GB = LocalDate.of(2016, 3, 1);
	  private static readonly ImmutableRatesProvider RATES_PROVS_GB = CapitalIndexedBondCurveDataSet.getRatesProviderGb(VAL_DATE_GB, CapitalIndexedBondCurveDataSet.getTimeSeriesGb(VAL_DATE_GB));
	  private static readonly LegalEntityDiscountingProvider ISSUER_PROVS_GB = CapitalIndexedBondCurveDataSet.getLegalEntityDiscountingProviderGb(VAL_DATE_GB);

	  private const double START_INDEX_GOV = 82.966;
	  private const double CPN_VALUE_GOV = 0.025 * 0.5;
	  private static readonly ValueSchedule CPN_GOV = ValueSchedule.of(CPN_VALUE_GOV);
	  private static readonly InflationRateCalculation RATE_CALC_GOV = InflationRateCalculation.builder().gearing(CPN_GOV).index(GB_RPI).lag(Period.ofMonths(8)).indexCalculationMethod(MONTHLY).firstIndexValue(START_INDEX_GOV).build();
	  private static readonly LocalDate START_GOV = LocalDate.of(1983, 10, 16);
	  private static readonly LocalDate END_GOV = LocalDate.of(2020, 4, 16);
	  private static readonly BusinessDayAdjustment BUSINESS_ADJUST_GOV = BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, GBLO);
	  private static readonly DaysAdjustment SETTLE_OFFSET_GB = DaysAdjustment.ofBusinessDays(1, GBLO);
	  private static readonly PeriodicSchedule SCHEDULE_GOV = PeriodicSchedule.of(START_GOV, END_GOV, FREQUENCY, BUSINESS_ADJUST_GOV, StubConvention.NONE, RollConventions.NONE);
	  private static readonly BusinessDayAdjustment EX_COUPON_ADJ_GOV = BusinessDayAdjustment.of(BusinessDayConventions.PRECEDING, GBLO);
	  private static readonly DaysAdjustment EX_COUPON_GOV = DaysAdjustment.ofCalendarDays(-8, EX_COUPON_ADJ_GOV);
	  private static readonly ResolvedCapitalIndexedBond PRODUCT_GOV = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NTNL).currency(GBP).dayCount(ACT_ACT_ICMA).rateCalculation(RATE_CALC_GOV).legalEntityId(LEGAL_ENTITY).yieldConvention(GB_IL_FLOAT).settlementDateOffset(SETTLE_OFFSET_GB).accrualSchedule(SCHEDULE_GOV).exCouponPeriod(EX_COUPON_GOV).build().resolve(REF_DATA);
	  private const double YIELD_GOV = -0.01532;

	  private const double START_INDEX_GOV_OP = 81.623;
	  private static readonly LocalDate START_GOV_OP = LocalDate.of(1983, 1, 26);
	  private static readonly LocalDate END_GOV_OP = LocalDate.of(2016, 7, 26);
	  private static readonly PeriodicSchedule SCHEDULE_GOV_OP = PeriodicSchedule.of(START_GOV_OP, END_GOV_OP, FREQUENCY, BUSINESS_ADJUST_GOV, StubConvention.NONE, RollConventions.NONE);
	  private static readonly ResolvedCapitalIndexedBond PRODUCT_GOV_OP = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NTNL).currency(GBP).dayCount(ACT_ACT_ICMA).rateCalculation(RATE_CALC_GOV.toBuilder().firstIndexValue(START_INDEX_GOV_OP).build()).legalEntityId(LEGAL_ENTITY).yieldConvention(GB_IL_FLOAT).settlementDateOffset(SETTLE_OFFSET_GB).accrualSchedule(SCHEDULE_GOV_OP).exCouponPeriod(EX_COUPON_GOV).build().resolve(REF_DATA);
	  private const double YIELD_GOV_OP = -0.0244;

	  public virtual void test_priceFromRealYield_ukGov()
	  {
		LocalDate standardSettle = PRODUCT_GOV.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double computed = PRICER.cleanPriceFromRealYield(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, YIELD_GOV);
		assertEquals(computed, 3.60, 1.e-2);
		double computedOnePeriod = PRICER.cleanPriceFromRealYield(PRODUCT_GOV_OP, RATES_PROVS_GB, PRODUCT_GOV_OP.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA), YIELD_GOV_OP);
		assertEquals(computedOnePeriod, 3.21, 4.e-2);
		double dirtyPrice = PRICER.dirtyPriceFromRealYield(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, YIELD_GOV);
		double cleanPrice = PRICER.cleanNominalPriceFromDirtyNominalPrice(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, dirtyPrice);
		assertEquals(computed, cleanPrice);
		double yieldRe = PRICER.realYieldFromDirtyPrice(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, dirtyPrice);
		assertEquals(yieldRe, YIELD_GOV, TOL);
	  }

	  public virtual void test_modifiedDuration_convexity_ukGov()
	  {
		double eps = 1.0e-5;
		LocalDate standardSettle = PRODUCT_GOV.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double mdComputed = PRICER.modifiedDurationFromRealYieldFiniteDifference(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, YIELD_GOV);
		double cvComputed = PRICER.convexityFromRealYieldFiniteDifference(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, YIELD_GOV);
		double price = PRICER.cleanPriceFromRealYield(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, YIELD_GOV);
		double up = PRICER.cleanPriceFromRealYield(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, YIELD_GOV + eps);
		double dw = PRICER.cleanPriceFromRealYield(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, YIELD_GOV - eps);
		assertEquals(mdComputed, 0.5 * (dw - up) / eps / price, eps);
		assertEquals(cvComputed, (up + dw - 2d * price) / price / eps / eps, eps);
	  }

	  public virtual void test_realYieldFromCurves_ukGov()
	  {
		LocalDate standardSettle = PRODUCT_GOV.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double computed = PRICER.realYieldFromCurves(PRODUCT_GOV, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurves(PRODUCT_GOV, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA);
		double expected = PRICER.realYieldFromDirtyPrice(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, dirtyNominalPrice);
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void zSpreadFromCurvesAndCleanPrice_ukGov()
	  {
		LocalDate standardSettle = PRODUCT_GOV.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurvesWithZSpread(PRODUCT_GOV, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double cleanNominalPrice = PRICER.cleanNominalPriceFromDirtyNominalPrice(PRODUCT_GOV, RATES_PROVS_GB, standardSettle, dirtyNominalPrice);
		double computed = PRICER.zSpreadFromCurvesAndCleanPrice(PRODUCT_GOV, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA, cleanNominalPrice, PERIODIC, PERIOD_PER_YEAR);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  public virtual void test_accruedInterest_ukGov()
	  {
		double accPositive = PRODUCT_GOV.accruedInterest(LocalDate.of(2016, 4, 7));
		assertEquals(accPositive, 11885d, 1.0);
		double accNegative = PRODUCT_GOV.accruedInterest(LocalDate.of(2016, 4, 8));
		assertEquals(accNegative, -546.44, 1.0e-2);
		double accZero = PRODUCT_GOV.accruedInterest(LocalDate.of(2016, 4, 16));
		assertEquals(accZero, 0d);
	  }

	  //-------------------------------------------------------------------------
	  private const double START_INDEX_CORP = 216.52;
	  private const double CPN_VALUE_CORP = 0.00625 * 0.5;
	  private static readonly ValueSchedule CPN_CORP = ValueSchedule.of(CPN_VALUE_CORP);
	  private static readonly InflationRateCalculation RATE_CALC_CORP = InflationRateCalculation.builder().gearing(CPN_CORP).index(GB_RPI).lag(Period.ofMonths(3)).indexCalculationMethod(INTERPOLATED).firstIndexValue(START_INDEX_CORP).build();
	  private static readonly LocalDate START_CORP = LocalDate.of(2010, 3, 22);
	  private static readonly LocalDate END_CORP = LocalDate.of(2040, 3, 22);
	  private static readonly BusinessDayAdjustment BUSINESS_ADJUST_CORP = BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, GBLO);
	  private static readonly PeriodicSchedule SCHEDULE_CORP = PeriodicSchedule.of(START_CORP, END_CORP, FREQUENCY, BUSINESS_ADJUST_CORP, StubConvention.NONE, RollConventions.NONE);
	  private static readonly BusinessDayAdjustment EX_COUPON_ADJ_CORP = BusinessDayAdjustment.of(BusinessDayConventions.PRECEDING, GBLO);
	  private static readonly DaysAdjustment EX_COUPON_CORP = DaysAdjustment.ofCalendarDays(-8, EX_COUPON_ADJ_CORP);
	  private static readonly ResolvedCapitalIndexedBond PRODUCT_CORP = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NTNL).currency(GBP).dayCount(ACT_ACT_ICMA).rateCalculation(RATE_CALC_CORP).legalEntityId(LEGAL_ENTITY).yieldConvention(GB_IL_BOND).settlementDateOffset(SETTLE_OFFSET_GB).accrualSchedule(SCHEDULE_CORP).exCouponPeriod(EX_COUPON_CORP).build().resolve(REF_DATA);
	  private const double YIELD_CORP = -0.00842;

	  public virtual void test_priceFromRealYield_ukCorp()
	  {
		LocalDate standardSettle = PRODUCT_CORP.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double computed = PRICER.cleanPriceFromRealYield(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, YIELD_CORP);
		assertEquals(computed, 1.39, 1.e-2);
		double computedOnePeriod = PRICER.cleanPriceFromRealYield(PRODUCT_CORP, RATES_PROVS_GB, LocalDate.of(2039, 12, 1), -0.02842);
		assertEquals(computedOnePeriod, 1.01, 1.e-2);
		double dirtyPrice = PRICER.dirtyPriceFromRealYield(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, YIELD_CORP);
		double cleanPrice = PRICER.cleanRealPriceFromDirtyRealPrice(PRODUCT_CORP, standardSettle, dirtyPrice);
		assertEquals(computed, cleanPrice);
		double yieldRe = PRICER.realYieldFromDirtyPrice(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, dirtyPrice);
		assertEquals(yieldRe, YIELD_CORP, TOL);
	  }

	  public virtual void test_modifiedDuration_convexity_ukCor()
	  {
		double eps = 1.0e-5;
		LocalDate standardSettle = PRODUCT_CORP.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double mdComputed = PRICER.modifiedDurationFromRealYieldFiniteDifference(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, YIELD_CORP);
		double cvComputed = PRICER.convexityFromRealYieldFiniteDifference(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, YIELD_CORP);
		double price = PRICER.cleanPriceFromRealYield(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, YIELD_CORP);
		double up = PRICER.cleanPriceFromRealYield(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, YIELD_CORP + eps);
		double dw = PRICER.cleanPriceFromRealYield(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, YIELD_CORP - eps);
		assertEquals(mdComputed, 0.5 * (dw - up) / eps / price, eps);
		assertEquals(cvComputed, (up + dw - 2d * price) / price / eps / eps, eps);
	  }

	  public virtual void test_realYieldFromCurves_ukCor()
	  {
		LocalDate standardSettle = PRODUCT_CORP.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double computed = PRICER.realYieldFromCurves(PRODUCT_CORP, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurves(PRODUCT_CORP, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA);
		double dirtyRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, dirtyNominalPrice);
		double expected = PRICER.realYieldFromDirtyPrice(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, dirtyRealPrice);
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void zSpreadFromCurvesAndCleanPrice_ukCor()
	  {
		LocalDate standardSettle = PRODUCT_CORP.SettlementDateOffset.adjust(VAL_DATE_GB, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurvesWithZSpread(PRODUCT_CORP, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double cleanRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, PRICER.cleanNominalPriceFromDirtyNominalPrice(PRODUCT_CORP, RATES_PROVS_GB, standardSettle, dirtyNominalPrice));
		double computed = PRICER.zSpreadFromCurvesAndCleanPrice(PRODUCT_CORP, RATES_PROVS_GB, ISSUER_PROVS_GB, REF_DATA, cleanRealPrice, PERIODIC, PERIOD_PER_YEAR);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  public virtual void test_accruedInterest_ukCor()
	  {
		double accPositive = PRODUCT_CORP.accruedInterest(LocalDate.of(2016, 3, 13));
		assertEquals(accPositive, 2971d, 1.0);
		double accNegative = PRODUCT_CORP.accruedInterest(LocalDate.of(2016, 3, 14));
		assertEquals(accNegative, -137.37, 1.0e-2);
		double accZero = PRODUCT_CORP.accruedInterest(LocalDate.of(2016, 3, 22));
		assertEquals(accZero, 0d);
	  }

	  //-------------------------------------------------------------------------
	  private static readonly ImmutableRatesProvider RATES_PROVS_JP = CapitalIndexedBondCurveDataSet.getRatesProviderJp(VAL_DATE, CapitalIndexedBondCurveDataSet.getTimeSeriesJp(VAL_DATE));
	  private static readonly LegalEntityDiscountingProvider ISSUER_PROVS_JP = CapitalIndexedBondCurveDataSet.getLegalEntityDiscountingProviderJp(VAL_DATE);

	  private const double START_INDEX_JPI = 103.2d;
	  private const double CPN_VALUE_JPI = 0.001 * 0.5;
	  private static readonly ValueSchedule CPN_JPI = ValueSchedule.of(CPN_VALUE_JPI);
	  private static readonly InflationRateCalculation RATE_CALC_JPI = InflationRateCalculation.builder().gearing(CPN_JPI).index(JP_CPI_EXF).lag(Period.ofMonths(3)).indexCalculationMethod(INTERPOLATED_JAPAN).firstIndexValue(START_INDEX_JPI).build();
	  private static readonly LocalDate START_JPI = LocalDate.of(2015, 3, 10);
	  private static readonly LocalDate END_JPI = LocalDate.of(2025, 3, 10);
	  private static readonly BusinessDayAdjustment BUSINESS_ADJUST_JPI = BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, JPTO);
	  private static readonly DaysAdjustment SETTLE_OFFSET_JPI = DaysAdjustment.ofBusinessDays(2, JPTO);
	  private static readonly PeriodicSchedule SCHEDULE_JPI = PeriodicSchedule.of(START_JPI, END_JPI, FREQUENCY, BUSINESS_ADJUST_JPI, StubConvention.NONE, RollConventions.NONE);
	  private static readonly ResolvedCapitalIndexedBond PRODUCT_JPI = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NTNL).currency(JPY).dayCount(NL_365).rateCalculation(RATE_CALC_JPI).legalEntityId(LEGAL_ENTITY).yieldConvention(JP_IL_SIMPLE).settlementDateOffset(SETTLE_OFFSET_JPI).accrualSchedule(SCHEDULE_JPI).build().resolve(REF_DATA);
	  private const double YIELD_JPI = -0.00309;

	  public virtual void test_priceFromRealYield_jpi()
	  {
		LocalDate standardSettle = PRODUCT_JPI.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double computed = PRICER.cleanPriceFromRealYield(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, YIELD_JPI);
		assertEquals(computed, 1.04, 1.e-2);
		double dirtyPrice = PRICER.dirtyPriceFromRealYield(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, YIELD_JPI);
		double cleanPrice = PRICER.cleanRealPriceFromDirtyRealPrice(PRODUCT_JPI, standardSettle, dirtyPrice);
		assertEquals(computed, cleanPrice);
		double yieldRe = PRICER.realYieldFromDirtyPrice(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, dirtyPrice);
		assertEquals(yieldRe, YIELD_JPI, TOL);
	  }

	  public virtual void test_modifiedDuration_convexity_jpi()
	  {
		double eps = 1.0e-5;
		LocalDate standardSettle = PRODUCT_JPI.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double mdComputed = PRICER.modifiedDurationFromRealYieldFiniteDifference(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, YIELD_JPI);
		double cvComputed = PRICER.convexityFromRealYieldFiniteDifference(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, YIELD_JPI);
		double price = PRICER.cleanPriceFromRealYield(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, YIELD_JPI);
		double up = PRICER.cleanPriceFromRealYield(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, YIELD_JPI + eps);
		double dw = PRICER.cleanPriceFromRealYield(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, YIELD_JPI - eps);
		assertEquals(mdComputed, 0.5 * (dw - up) / eps / price, eps);
		assertEquals(cvComputed, (up + dw - 2d * price) / price / eps / eps, eps);
	  }

	  public virtual void test_realYieldFromCurves_jpi()
	  {
		LocalDate standardSettle = PRODUCT_JPI.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double computed = PRICER.realYieldFromCurves(PRODUCT_JPI, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurves(PRODUCT_JPI, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA);
		double dirtyRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, dirtyNominalPrice);
		double expected = PRICER.realYieldFromDirtyPrice(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, dirtyRealPrice);
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void zSpreadFromCurvesAndCleanPrice_jpi()
	  {
		LocalDate standardSettle = PRODUCT_JPI.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurvesWithZSpread(PRODUCT_JPI, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double cleanRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, PRICER.cleanNominalPriceFromDirtyNominalPrice(PRODUCT_JPI, RATES_PROVS_JP, standardSettle, dirtyNominalPrice));
		double computed = PRICER.zSpreadFromCurvesAndCleanPrice(PRODUCT_JPI, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA, cleanRealPrice, PERIODIC, PERIOD_PER_YEAR);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  public virtual void test_accruedInterest_jpi()
	  {
		double accPositive = PRODUCT_JPI.accruedInterest(LocalDate.of(2016, 3, 9));
		CapitalIndexedBondPaymentPeriod period = PRODUCT_JPI.PeriodicPayments.get(1);
		double yc = PRODUCT_JPI.DayCount.relativeYearFraction(period.StartDate, period.EndDate);
		double expected = CPN_VALUE_JPI * 2d * yc * NTNL; // accrual of total period based on ACT/365F
		assertEquals(accPositive, expected, TOL * NTNL);
		double accZero = PRODUCT_JPI.accruedInterest(LocalDate.of(2016, 3, 10));
		assertEquals(accZero, 0d);
	  }

	  //-------------------------------------------------------------------------
	  private const double START_INDEX_JPW = 100.0d;
	  private const double CPN_VALUE_JPW = 0.008 * 0.5;
	  private static readonly ValueSchedule CPN_JPW = ValueSchedule.of(CPN_VALUE_JPW);
	  private static readonly InflationRateCalculation RATE_CALC_JPW = InflationRateCalculation.builder().gearing(CPN_JPW).index(JP_CPI_EXF).lag(Period.ofMonths(3)).indexCalculationMethod(INTERPOLATED_JAPAN).firstIndexValue(START_INDEX_JPW).build();
	  private static readonly LocalDate START_JPW = LocalDate.of(2013, 9, 10);
	  private static readonly LocalDate END_JPW = LocalDate.of(2023, 9, 10);
	  private static readonly BusinessDayAdjustment BUSINESS_ADJUST_JPW = BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, JPTO);
	  private static readonly DaysAdjustment SETTLE_OFFSET_JPW = DaysAdjustment.ofBusinessDays(2, JPTO);
	  private static readonly PeriodicSchedule SCHEDULE_JPW = PeriodicSchedule.of(START_JPW, END_JPW, FREQUENCY, BUSINESS_ADJUST_JPW, StubConvention.NONE, RollConventions.NONE);
	  private static readonly ResolvedCapitalIndexedBond PRODUCT_JPW = CapitalIndexedBond.builder().securityId(SECURITY_ID).notional(NTNL).currency(JPY).dayCount(NL_365).rateCalculation(RATE_CALC_JPW).legalEntityId(LEGAL_ENTITY).yieldConvention(JP_IL_COMPOUND).settlementDateOffset(SETTLE_OFFSET_JPW).accrualSchedule(SCHEDULE_JPW).build().resolve(REF_DATA);
	  private const double YIELD_JPW = -0.005;

	  public virtual void test_priceFromRealYield_jpw()
	  {
		LocalDate standardSettle = PRODUCT_JPW.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double computed = PRICER.cleanPriceFromRealYield(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, YIELD_JPW);
		assertEquals(computed, 1.10, 1.e-2);
		double dirtyPrice = PRICER.dirtyPriceFromRealYield(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, YIELD_JPW);
		double cleanPrice = PRICER.cleanRealPriceFromDirtyRealPrice(PRODUCT_JPW, standardSettle, dirtyPrice);
		assertEquals(computed, cleanPrice);
		double yieldRe = PRICER.realYieldFromDirtyPrice(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, dirtyPrice);
		assertEquals(yieldRe, YIELD_JPW, TOL);
	  }

	  public virtual void test_modifiedDuration_convexity_jpw()
	  {
		double eps = 1.0e-5;
		LocalDate standardSettle = PRODUCT_JPW.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double mdComputed = PRICER.modifiedDurationFromRealYieldFiniteDifference(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, YIELD_JPW);
		double cvComputed = PRICER.convexityFromRealYieldFiniteDifference(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, YIELD_JPW);
		double price = PRICER.cleanPriceFromRealYield(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, YIELD_JPW);
		double up = PRICER.cleanPriceFromRealYield(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, YIELD_JPW + eps);
		double dw = PRICER.cleanPriceFromRealYield(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, YIELD_JPW - eps);
		assertEquals(mdComputed, 0.5 * (dw - up) / eps / price, eps);
		assertEquals(cvComputed, (up + dw - 2d * price) / price / eps / eps, eps);
	  }

	  public virtual void test_realYieldFromCurves_jpw()
	  {
		LocalDate standardSettle = PRODUCT_JPW.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double computed = PRICER.realYieldFromCurves(PRODUCT_JPW, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurves(PRODUCT_JPW, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA);
		double dirtyRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, dirtyNominalPrice);
		double expected = PRICER.realYieldFromDirtyPrice(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, dirtyRealPrice);
		assertEquals(computed, expected, TOL);
	  }

	  public virtual void zSpreadFromCurvesAndCleanPrice_jpw()
	  {
		LocalDate standardSettle = PRODUCT_JPW.SettlementDateOffset.adjust(VAL_DATE, REF_DATA);
		double dirtyNominalPrice = PRICER.dirtyNominalPriceFromCurvesWithZSpread(PRODUCT_JPW, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double cleanRealPrice = PRICER.realPriceFromNominalPrice(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, PRICER.cleanNominalPriceFromDirtyNominalPrice(PRODUCT_JPW, RATES_PROVS_JP, standardSettle, dirtyNominalPrice));
		double computed = PRICER.zSpreadFromCurvesAndCleanPrice(PRODUCT_JPW, RATES_PROVS_JP, ISSUER_PROVS_JP, REF_DATA, cleanRealPrice, PERIODIC, PERIOD_PER_YEAR);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  public virtual void test_accruedInterest_jpw()
	  {
		double accPositive = PRODUCT_JPW.accruedInterest(LocalDate.of(2016, 3, 9));
		CapitalIndexedBondPaymentPeriod period = PRODUCT_JPW.PeriodicPayments.get(4);
		double yc = PRODUCT_JPW.DayCount.relativeYearFraction(period.StartDate, period.EndDate);
		double expected = CPN_VALUE_JPW * 2d * yc * NTNL; // accrual of total period based on ACT/365F
		assertEquals(accPositive, expected, NTNL * NOTIONAL);
		double accZero = PRODUCT_JPW.accruedInterest(LocalDate.of(2016, 3, 10));
		assertEquals(accZero, 0d);
	  }

	  //-------------------------------------------------------------------------
	  // computes sensitivity with finite difference approximation
	  private CurrencyParameterSensitivities fdPvSensitivity(ResolvedCapitalIndexedBond product, ImmutableRatesProvider ratesProvider, LegalEntityDiscountingProvider issuerRatesProvider)
	  {

		CurrencyParameterSensitivities sensi1 = FD_CAL.sensitivity(issuerRatesProvider, p => PRICER.presentValue(product, ratesProvider, p));
		CurrencyParameterSensitivities sensi2 = FD_CAL.sensitivity(ratesProvider, p => PRICER.presentValue(product, p, issuerRatesProvider));
		return sensi1.combinedWith(sensi2);
	  }

	  // computes sensitivity with finite difference approximation
	  private CurrencyParameterSensitivities fdPvSensitivityWithZSpread(ResolvedCapitalIndexedBond product, ImmutableRatesProvider ratesProvider, LegalEntityDiscountingProvider issuerRatesProvider, double zSpread, CompoundedRateType compoundedRateType, int periodsPerYear)
	  {

		CurrencyParameterSensitivities sensi1 = FD_CAL.sensitivity(issuerRatesProvider, p => PRICER.presentValueWithZSpread(product, ratesProvider, p, zSpread, compoundedRateType, periodsPerYear));
		CurrencyParameterSensitivities sensi2 = FD_CAL.sensitivity(ratesProvider, p => PRICER.presentValueWithZSpread(product, p, issuerRatesProvider, zSpread, compoundedRateType, periodsPerYear));
		return sensi1.combinedWith(sensi2);
	  }

	  // computes sensitivity with finite difference approximation
	  private CurrencyParameterSensitivities fdPriceSensitivity(ResolvedCapitalIndexedBond bond, ImmutableRatesProvider ratesProvider, LegalEntityDiscountingProvider issuerRatesProvider)
	  {

		CurrencyParameterSensitivities sensi1 = FD_CAL.sensitivity(issuerRatesProvider, p => CurrencyAmount.of(USD, PRICER.dirtyNominalPriceFromCurves(bond, ratesProvider, p, REF_DATA)));
		CurrencyParameterSensitivities sensi2 = FD_CAL.sensitivity(ratesProvider, p => CurrencyAmount.of(USD, PRICER.dirtyNominalPriceFromCurves(bond, p, issuerRatesProvider, REF_DATA)));
		return sensi1.combinedWith(sensi2);
	  }

	  // computes sensitivity with finite difference approximation
	  private CurrencyParameterSensitivities fdPriceSensitivityWithZSpread(ResolvedCapitalIndexedBond bond, ImmutableRatesProvider ratesProvider, LegalEntityDiscountingProvider issuerRatesProvider, double zSpread, CompoundedRateType compoundedRateType, int periodsPerYear)
	  {

		CurrencyParameterSensitivities sensi1 = FD_CAL.sensitivity(issuerRatesProvider, p => CurrencyAmount.of(USD, PRICER.dirtyNominalPriceFromCurvesWithZSpread(bond, ratesProvider, p, REF_DATA, zSpread, compoundedRateType, periodsPerYear)));
		CurrencyParameterSensitivities sensi2 = FD_CAL.sensitivity(ratesProvider, p => CurrencyAmount.of(USD, PRICER.dirtyNominalPriceFromCurvesWithZSpread(bond, p, issuerRatesProvider, REF_DATA, zSpread, compoundedRateType, periodsPerYear)));
		return sensi1.combinedWith(sensi2);
	  }

	}

}