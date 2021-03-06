﻿/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.bond
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.currency.Currency.EUR;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.DayCounts.ACT_365F;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.HolidayCalendarIds.JPTO;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.HolidayCalendarIds.SAT_SUN;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertThrows;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.date;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.pricer.CompoundedRateType.CONTINUOUS;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.pricer.CompoundedRateType.PERIODIC;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertTrue;

	using Test = org.testng.annotations.Test;

	using ImmutableMap = com.google.common.collect.ImmutableMap;
	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using Currency = com.opengamma.strata.basics.currency.Currency;
	using CurrencyAmount = com.opengamma.strata.basics.currency.CurrencyAmount;
	using BusinessDayAdjustment = com.opengamma.strata.basics.date.BusinessDayAdjustment;
	using BusinessDayConventions = com.opengamma.strata.basics.date.BusinessDayConventions;
	using DayCount = com.opengamma.strata.basics.date.DayCount;
	using DayCounts = com.opengamma.strata.basics.date.DayCounts;
	using DaysAdjustment = com.opengamma.strata.basics.date.DaysAdjustment;
	using HolidayCalendarId = com.opengamma.strata.basics.date.HolidayCalendarId;
	using HolidayCalendarIds = com.opengamma.strata.basics.date.HolidayCalendarIds;
	using Frequency = com.opengamma.strata.basics.schedule.Frequency;
	using PeriodicSchedule = com.opengamma.strata.basics.schedule.PeriodicSchedule;
	using StubConvention = com.opengamma.strata.basics.schedule.StubConvention;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using Pair = com.opengamma.strata.collect.tuple.Pair;
	using CurveMetadata = com.opengamma.strata.market.curve.CurveMetadata;
	using CurveName = com.opengamma.strata.market.curve.CurveName;
	using Curves = com.opengamma.strata.market.curve.Curves;
	using InterpolatedNodalCurve = com.opengamma.strata.market.curve.InterpolatedNodalCurve;
	using LegalEntityGroup = com.opengamma.strata.market.curve.LegalEntityGroup;
	using RepoGroup = com.opengamma.strata.market.curve.RepoGroup;
	using CurveInterpolator = com.opengamma.strata.market.curve.interpolator.CurveInterpolator;
	using CurveInterpolators = com.opengamma.strata.market.curve.interpolator.CurveInterpolators;
	using CurrencyParameterSensitivities = com.opengamma.strata.market.param.CurrencyParameterSensitivities;
	using PointSensitivityBuilder = com.opengamma.strata.market.sensitivity.PointSensitivityBuilder;
	using RatesFiniteDifferenceSensitivityCalculator = com.opengamma.strata.pricer.sensitivity.RatesFiniteDifferenceSensitivityCalculator;
	using LegalEntityId = com.opengamma.strata.product.LegalEntityId;
	using SecurityId = com.opengamma.strata.product.SecurityId;
	using FixedCouponBond = com.opengamma.strata.product.bond.FixedCouponBond;
	using FixedCouponBondPaymentPeriod = com.opengamma.strata.product.bond.FixedCouponBondPaymentPeriod;
	using FixedCouponBondYieldConvention = com.opengamma.strata.product.bond.FixedCouponBondYieldConvention;
	using ResolvedFixedCouponBond = com.opengamma.strata.product.bond.ResolvedFixedCouponBond;

	/// <summary>
	/// Test <seealso cref="DiscountingFixedCouponBondProductPricer"/>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class DiscountingFixedCouponBondProductPricerTest
	public class DiscountingFixedCouponBondProductPricerTest
	{

	  private static readonly ReferenceData REF_DATA = ReferenceData.standard();

	  // fixed coupon bond
	  private static readonly SecurityId SECURITY_ID = SecurityId.of("OG-Ticker", "GOVT1-BOND1");
	  private static readonly LegalEntityId ISSUER_ID = LegalEntityId.of("OG-Ticker", "GOVT1");
	  private static readonly LocalDate VAL_DATE = date(2016, 4, 25);
	  private const FixedCouponBondYieldConvention YIELD_CONVENTION = FixedCouponBondYieldConvention.DE_BONDS;
	  private const double NOTIONAL = 1.0e7;
	  private const double FIXED_RATE = 0.015;
	  private static readonly HolidayCalendarId EUR_CALENDAR = HolidayCalendarIds.EUTA;
	  private static readonly DaysAdjustment DATE_OFFSET = DaysAdjustment.ofBusinessDays(3, EUR_CALENDAR);
	  private static readonly DayCount DAY_COUNT = DayCounts.ACT_365F;
	  private static readonly LocalDate START_DATE = LocalDate.of(2015, 4, 12);
	  private static readonly LocalDate END_DATE = LocalDate.of(2025, 4, 12);
	  private static readonly BusinessDayAdjustment BUSINESS_ADJUST = BusinessDayAdjustment.of(BusinessDayConventions.MODIFIED_FOLLOWING, EUR_CALENDAR);
	  private static readonly PeriodicSchedule PERIOD_SCHEDULE = PeriodicSchedule.of(START_DATE, END_DATE, Frequency.P6M, BUSINESS_ADJUST, StubConvention.SHORT_INITIAL, false);
	  private static readonly DaysAdjustment EX_COUPON = DaysAdjustment.ofBusinessDays(-5, EUR_CALENDAR, BUSINESS_ADJUST);
	  /// <summary>
	  /// nonzero ex-coupon period </summary>
	  private static readonly ResolvedFixedCouponBond PRODUCT = FixedCouponBond.builder().securityId(SECURITY_ID).dayCount(DAY_COUNT).fixedRate(FIXED_RATE).legalEntityId(ISSUER_ID).currency(EUR).notional(NOTIONAL).accrualSchedule(PERIOD_SCHEDULE).settlementDateOffset(DATE_OFFSET).yieldConvention(YIELD_CONVENTION).exCouponPeriod(EX_COUPON).build().resolve(REF_DATA);
	  /// <summary>
	  /// no ex-coupon period </summary>
	  private static readonly ResolvedFixedCouponBond PRODUCT_NO_EXCOUPON = FixedCouponBond.builder().securityId(SECURITY_ID).dayCount(DAY_COUNT).fixedRate(FIXED_RATE).legalEntityId(ISSUER_ID).currency(EUR).notional(NOTIONAL).accrualSchedule(PERIOD_SCHEDULE).settlementDateOffset(DATE_OFFSET).yieldConvention(YIELD_CONVENTION).build().resolve(REF_DATA);

	  // rates provider
	  private static readonly CurveInterpolator INTERPOLATOR = CurveInterpolators.LINEAR;
	  private static readonly CurveName NAME_REPO = CurveName.of("TestRepoCurve");
	  private static readonly CurveMetadata METADATA_REPO = Curves.zeroRates(NAME_REPO, ACT_365F);
	  private static readonly InterpolatedNodalCurve CURVE_REPO = InterpolatedNodalCurve.of(METADATA_REPO, DoubleArray.of(0.1, 2.0, 10.0), DoubleArray.of(0.05, 0.06, 0.09), INTERPOLATOR);
	  private static readonly DiscountFactors DSC_FACTORS_REPO = ZeroRateDiscountFactors.of(EUR, VAL_DATE, CURVE_REPO);
	  private static readonly RepoGroup GROUP_REPO = RepoGroup.of("GOVT1 BOND1");
	  private static readonly CurveName NAME_ISSUER = CurveName.of("TestIssuerCurve");
	  private static readonly CurveMetadata METADATA_ISSUER = Curves.zeroRates(NAME_ISSUER, ACT_365F);
	  private static readonly InterpolatedNodalCurve CURVE_ISSUER = InterpolatedNodalCurve.of(METADATA_ISSUER, DoubleArray.of(0.2, 9.0, 15.0), DoubleArray.of(0.03, 0.05, 0.13), INTERPOLATOR);
	  private static readonly DiscountFactors DSC_FACTORS_ISSUER = ZeroRateDiscountFactors.of(EUR, VAL_DATE, CURVE_ISSUER);
	  private static readonly LegalEntityGroup GROUP_ISSUER = LegalEntityGroup.of("GOVT1");
	  private static readonly LegalEntityDiscountingProvider PROVIDER = ImmutableLegalEntityDiscountingProvider.builder().issuerCurves(ImmutableMap.of(Pair.of(GROUP_ISSUER, EUR), DSC_FACTORS_ISSUER)).issuerCurveGroups(ImmutableMap.of(ISSUER_ID, GROUP_ISSUER)).repoCurves(ImmutableMap.of(Pair.of(GROUP_REPO, EUR), DSC_FACTORS_REPO)).repoCurveSecurityGroups(ImmutableMap.of(SECURITY_ID, GROUP_REPO)).valuationDate(VAL_DATE).build();

	  private const double Z_SPREAD = 0.035;
	  private const int PERIOD_PER_YEAR = 4;
	  private const double TOL = 1.0e-12;
	  private const double EPS = 1.0e-6;

	  // pricers
	  private static readonly DiscountingFixedCouponBondProductPricer PRICER = DiscountingFixedCouponBondProductPricer.DEFAULT;
	  private static readonly DiscountingPaymentPricer PRICER_NOMINAL = DiscountingPaymentPricer.DEFAULT;
	  private static readonly DiscountingFixedCouponBondPaymentPeriodPricer PRICER_COUPON = DiscountingFixedCouponBondPaymentPeriodPricer.DEFAULT;
	  private static readonly RatesFiniteDifferenceSensitivityCalculator FD_CAL = new RatesFiniteDifferenceSensitivityCalculator(EPS);

	  //-------------------------------------------------------------------------
	  public virtual void test_presentValue()
	  {
		CurrencyAmount computed = PRICER.presentValue(PRODUCT, PROVIDER);
		CurrencyAmount expected = PRICER_NOMINAL.presentValue(PRODUCT.NominalPayment, DSC_FACTORS_ISSUER);
		int size = PRODUCT.PeriodicPayments.size();
		double pvCupon = 0d;
		for (int i = 2; i < size; ++i)
		{
		  FixedCouponBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  pvCupon += PRICER_COUPON.presentValue(payment, IssuerCurveDiscountFactors.of(DSC_FACTORS_ISSUER, GROUP_ISSUER));
		}
		expected = expected.plus(pvCupon);
		assertEquals(computed.Currency, EUR);
		assertEquals(computed.Amount, expected.Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_presentValueWithZSpread_continuous()
	  {
		CurrencyAmount computed = PRICER.presentValueWithZSpread(PRODUCT, PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		CurrencyAmount expected = PRICER_NOMINAL.presentValueWithSpread(PRODUCT.NominalPayment, DSC_FACTORS_ISSUER, Z_SPREAD, CONTINUOUS, 0);
		int size = PRODUCT.PeriodicPayments.size();
		double pvcCupon = 0d;
		for (int i = 2; i < size; ++i)
		{
		  FixedCouponBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  pvcCupon += PRICER_COUPON.presentValueWithSpread(payment, IssuerCurveDiscountFactors.of(DSC_FACTORS_ISSUER, GROUP_ISSUER), Z_SPREAD, CONTINUOUS, 0);
		}
		expected = expected.plus(pvcCupon);
		assertEquals(computed.Currency, EUR);
		assertEquals(computed.Amount, expected.Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_presentValueWithZSpread_periodic()
	  {
		CurrencyAmount computed = PRICER.presentValueWithZSpread(PRODUCT, PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		CurrencyAmount expected = PRICER_NOMINAL.presentValueWithSpread(PRODUCT.NominalPayment, DSC_FACTORS_ISSUER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		int size = PRODUCT.PeriodicPayments.size();
		double pvcCupon = 0d;
		for (int i = 2; i < size; ++i)
		{
		  FixedCouponBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  pvcCupon += PRICER_COUPON.presentValueWithSpread(payment, IssuerCurveDiscountFactors.of(DSC_FACTORS_ISSUER, GROUP_ISSUER), Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		}
		expected = expected.plus(pvcCupon);
		assertEquals(computed.Currency, EUR);
		assertEquals(computed.Amount, expected.Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_presentValue_noExcoupon()
	  {
		CurrencyAmount computed = PRICER.presentValue(PRODUCT_NO_EXCOUPON, PROVIDER);
		CurrencyAmount expected = PRICER_NOMINAL.presentValue(PRODUCT.NominalPayment, DSC_FACTORS_ISSUER);
		int size = PRODUCT.PeriodicPayments.size();
		double pvcCupon = 0d;
		for (int i = 2; i < size; ++i)
		{
		  FixedCouponBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  pvcCupon += PRICER_COUPON.presentValue(payment, IssuerCurveDiscountFactors.of(DSC_FACTORS_ISSUER, GROUP_ISSUER));
		}
		expected = expected.plus(pvcCupon);
		assertEquals(computed.Currency, EUR);
		assertEquals(computed.Amount, expected.Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_presentValueWithZSpread_continuous_noExcoupon()
	  {
		CurrencyAmount computed = PRICER.presentValueWithZSpread(PRODUCT_NO_EXCOUPON, PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		CurrencyAmount expected = PRICER_NOMINAL.presentValueWithSpread(PRODUCT.NominalPayment, DSC_FACTORS_ISSUER, Z_SPREAD, CONTINUOUS, 0);
		int size = PRODUCT.PeriodicPayments.size();
		double pvcCupon = 0d;
		for (int i = 2; i < size; ++i)
		{
		  FixedCouponBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  pvcCupon += PRICER_COUPON.presentValueWithSpread(payment, IssuerCurveDiscountFactors.of(DSC_FACTORS_ISSUER, GROUP_ISSUER), Z_SPREAD, CONTINUOUS, 0);
		}
		expected = expected.plus(pvcCupon);
		assertEquals(computed.Currency, EUR);
		assertEquals(computed.Amount, expected.Amount, NOTIONAL * TOL);
	  }

	  public virtual void test_presentValueWithZSpread_periodic_noExcoupon()
	  {
		CurrencyAmount computed = PRICER.presentValueWithZSpread(PRODUCT_NO_EXCOUPON, PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		CurrencyAmount expected = PRICER_NOMINAL.presentValueWithSpread(PRODUCT.NominalPayment, DSC_FACTORS_ISSUER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		int size = PRODUCT.PeriodicPayments.size();
		double pvcCupon = 0d;
		for (int i = 2; i < size; ++i)
		{
		  FixedCouponBondPaymentPeriod payment = PRODUCT.PeriodicPayments.get(i);
		  pvcCupon += PRICER_COUPON.presentValueWithSpread(payment, IssuerCurveDiscountFactors.of(DSC_FACTORS_ISSUER, GROUP_ISSUER), Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		}
		expected = expected.plus(pvcCupon);
		assertEquals(computed.Currency, EUR);
		assertEquals(computed.Amount, expected.Amount, NOTIONAL * TOL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_dirtyPriceFromCurves()
	  {
		double computed = PRICER.dirtyPriceFromCurves(PRODUCT, PROVIDER, REF_DATA);
		CurrencyAmount pv = PRICER.presentValue(PRODUCT, PROVIDER);
		LocalDate settlement = DATE_OFFSET.adjust(VAL_DATE, REF_DATA);
		double df = DSC_FACTORS_REPO.discountFactor(settlement);
		assertEquals(computed, pv.Amount / df / NOTIONAL);
	  }

	  public virtual void test_dirtyPriceFromCurvesWithZSpread_continuous()
	  {
		double computed = PRICER.dirtyPriceFromCurvesWithZSpread(PRODUCT, PROVIDER, REF_DATA, Z_SPREAD, CONTINUOUS, 0);
		CurrencyAmount pv = PRICER.presentValueWithZSpread(PRODUCT, PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		LocalDate settlement = DATE_OFFSET.adjust(VAL_DATE, REF_DATA);
		double df = DSC_FACTORS_REPO.discountFactor(settlement);
		assertEquals(computed, pv.Amount / df / NOTIONAL);
	  }

	  public virtual void test_dirtyPriceFromCurvesWithZSpread_periodic()
	  {
		double computed = PRICER.dirtyPriceFromCurvesWithZSpread(PRODUCT, PROVIDER, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		CurrencyAmount pv = PRICER.presentValueWithZSpread(PRODUCT, PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		LocalDate settlement = DATE_OFFSET.adjust(VAL_DATE, REF_DATA);
		double df = DSC_FACTORS_REPO.discountFactor(settlement);
		assertEquals(computed, pv.Amount / df / NOTIONAL);
	  }

	  public virtual void test_dirtyPriceFromCleanPrice_cleanPriceFromDirtyPrice()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromCurves(PRODUCT, PROVIDER, REF_DATA);
		LocalDate settlement = DATE_OFFSET.adjust(VAL_DATE, REF_DATA);
		double cleanPrice = PRICER.cleanPriceFromDirtyPrice(PRODUCT, settlement, dirtyPrice);
		double accruedInterest = PRICER.accruedInterest(PRODUCT, settlement);
		assertEquals(cleanPrice, dirtyPrice - accruedInterest / NOTIONAL, NOTIONAL * TOL);
		double dirtyPriceRe = PRICER.dirtyPriceFromCleanPrice(PRODUCT, settlement, cleanPrice);
		assertEquals(dirtyPriceRe, dirtyPrice, TOL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_zSpreadFromCurvesAndPV_continuous()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromCurvesWithZSpread(PRODUCT, PROVIDER, REF_DATA, Z_SPREAD, CONTINUOUS, 0);
		double computed = PRICER.zSpreadFromCurvesAndDirtyPrice(PRODUCT, PROVIDER, REF_DATA, dirtyPrice, CONTINUOUS, 0);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  public virtual void test_zSpreadFromCurvesAndPV_periodic()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromCurvesWithZSpread(PRODUCT, PROVIDER, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		double computed = PRICER.zSpreadFromCurvesAndDirtyPrice(PRODUCT, PROVIDER, REF_DATA, dirtyPrice, PERIODIC, PERIOD_PER_YEAR);
		assertEquals(computed, Z_SPREAD, TOL);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_presentValueSensitivity()
	  {
		PointSensitivityBuilder point = PRICER.presentValueSensitivity(PRODUCT, PROVIDER);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => PRICER.presentValue(PRODUCT, p));
		assertTrue(computed.equalWithTolerance(expected, 30d * NOTIONAL * EPS));
	  }

	  public virtual void test_presentValueSensitivityWithZSpread_continuous()
	  {
		PointSensitivityBuilder point = PRICER.presentValueSensitivityWithZSpread(PRODUCT, PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => PRICER.presentValueWithZSpread(PRODUCT, p, Z_SPREAD, CONTINUOUS, 0));
		assertTrue(computed.equalWithTolerance(expected, 20d * NOTIONAL * EPS));
	  }

	  public virtual void test_presentValueSensitivityWithZSpread_periodic()
	  {
		PointSensitivityBuilder point = PRICER.presentValueSensitivityWithZSpread(PRODUCT, PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => PRICER.presentValueWithZSpread(PRODUCT, p, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR));
		assertTrue(computed.equalWithTolerance(expected, 20d * NOTIONAL * EPS));
	  }

	  public virtual void test_presentValueProductSensitivity_noExcoupon()
	  {
		PointSensitivityBuilder point = PRICER.presentValueSensitivity(PRODUCT_NO_EXCOUPON, PROVIDER);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => PRICER.presentValue(PRODUCT_NO_EXCOUPON, p));
		assertTrue(computed.equalWithTolerance(expected, 30d * NOTIONAL * EPS));
	  }

	  public virtual void test_presentValueSensitivityWithZSpread_continuous_noExcoupon()
	  {
		PointSensitivityBuilder point = PRICER.presentValueSensitivityWithZSpread(PRODUCT_NO_EXCOUPON, PROVIDER, Z_SPREAD, CONTINUOUS, 0);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => PRICER.presentValueWithZSpread(PRODUCT_NO_EXCOUPON, p, Z_SPREAD, CONTINUOUS, 0));
		assertTrue(computed.equalWithTolerance(expected, 20d * NOTIONAL * EPS));
	  }

	  public virtual void test_presentValueSensitivityWithZSpread_periodic_noExcoupon()
	  {
		PointSensitivityBuilder point = PRICER.presentValueSensitivityWithZSpread(PRODUCT_NO_EXCOUPON, PROVIDER, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => PRICER.presentValueWithZSpread(PRODUCT_NO_EXCOUPON, p, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR));
		assertTrue(computed.equalWithTolerance(expected, 20d * NOTIONAL * EPS));
	  }

	  public virtual void test_dirtyPriceSensitivity()
	  {
		PointSensitivityBuilder point = PRICER.dirtyPriceSensitivity(PRODUCT, PROVIDER, REF_DATA);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => CurrencyAmount.of(EUR, PRICER.dirtyPriceFromCurves(PRODUCT, p, REF_DATA)));
		assertTrue(computed.equalWithTolerance(expected, NOTIONAL * EPS));
	  }

	  public virtual void test_dirtyPriceSensitivityWithZspread_continuous()
	  {
		PointSensitivityBuilder point = PRICER.dirtyPriceSensitivityWithZspread(PRODUCT, PROVIDER, REF_DATA, Z_SPREAD, CONTINUOUS, 0);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => CurrencyAmount.of(EUR, PRICER.dirtyPriceFromCurvesWithZSpread(PRODUCT, p, REF_DATA, Z_SPREAD, CONTINUOUS, 0)));
		assertTrue(computed.equalWithTolerance(expected, NOTIONAL * EPS));
	  }

	  public virtual void test_dirtyPriceSensitivityWithZspread_periodic()
	  {
		PointSensitivityBuilder point = PRICER.dirtyPriceSensitivityWithZspread(PRODUCT, PROVIDER, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR);
		CurrencyParameterSensitivities computed = PROVIDER.parameterSensitivity(point.build());
		CurrencyParameterSensitivities expected = FD_CAL.sensitivity(PROVIDER, p => CurrencyAmount.of(EUR, PRICER.dirtyPriceFromCurvesWithZSpread(PRODUCT, p, REF_DATA, Z_SPREAD, PERIODIC, PERIOD_PER_YEAR)));
		assertTrue(computed.equalWithTolerance(expected, NOTIONAL * EPS));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_accruedInterest()
	  {
		// settle before start
		LocalDate settleDate1 = START_DATE.minusDays(5);
		double accruedInterest1 = PRICER.accruedInterest(PRODUCT, settleDate1);
		assertEquals(accruedInterest1, 0d);
		// settle between endDate and endDate -lag
		LocalDate settleDate2 = date(2015, 10, 8);
		double accruedInterest2 = PRICER.accruedInterest(PRODUCT, settleDate2);
		assertEquals(accruedInterest2, -4.0 / 365.0 * FIXED_RATE * NOTIONAL, EPS);
		// normal
		LocalDate settleDate3 = date(2015, 4, 18); // not adjusted
		ResolvedFixedCouponBond product = FixedCouponBond.builder().securityId(SECURITY_ID).dayCount(DAY_COUNT).fixedRate(FIXED_RATE).legalEntityId(ISSUER_ID).currency(EUR).notional(NOTIONAL).accrualSchedule(PERIOD_SCHEDULE).settlementDateOffset(DATE_OFFSET).yieldConvention(YIELD_CONVENTION).exCouponPeriod(DaysAdjustment.NONE).build().resolve(REF_DATA);
		double accruedInterest3 = PRICER.accruedInterest(product, settleDate3);
		assertEquals(accruedInterest3, 6.0 / 365.0 * FIXED_RATE * NOTIONAL, EPS);
	  }

	  //-------------------------------------------------------------------------
	  /* US Street convention */
	  private static readonly LocalDate START_US = date(2006, 11, 15);
	  private static readonly LocalDate END_US = START_US.plusYears(10);
	  private static readonly PeriodicSchedule SCHEDULE_US = PeriodicSchedule.of(START_US, END_US, Frequency.P6M, BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, SAT_SUN), StubConvention.SHORT_INITIAL, false);
	  private static readonly ResolvedFixedCouponBond PRODUCT_US = FixedCouponBond.builder().securityId(SECURITY_ID).dayCount(DayCounts.ACT_ACT_ICMA).fixedRate(0.04625).legalEntityId(ISSUER_ID).currency(Currency.USD).notional(100).accrualSchedule(SCHEDULE_US).settlementDateOffset(DaysAdjustment.ofBusinessDays(3, SAT_SUN)).yieldConvention(FixedCouponBondYieldConvention.US_STREET).exCouponPeriod(DaysAdjustment.NONE).build().resolve(REF_DATA);
	  private static readonly ResolvedFixedCouponBond PRODUCT_US_0 = PRODUCT_US.toBuilder().fixedRate(0.0d).build();
	  private static readonly LocalDate VALUATION_US = date(2011, 8, 18);
	  private static readonly LocalDate SETTLEMENT_US = PRODUCT_US.SettlementDateOffset.adjust(VALUATION_US, REF_DATA);
	  private static readonly LocalDate VALUATION_LAST_US = date(2016, 6, 3);
	  private static readonly LocalDate SETTLEMENT_LAST_US = PRODUCT_US.SettlementDateOffset.adjust(VALUATION_LAST_US, REF_DATA);
	  private const double YIELD_US = 0.04;

	  public virtual void dirtyPriceFromYieldUS()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US);
		assertEquals(dirtyPrice, 1.0417352500524246, TOL); // 2.x.
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_US, SETTLEMENT_US, dirtyPrice);
		assertEquals(yield, YIELD_US, TOL);
	  }

	  // Check price from yield when coupon is 0.
	  public virtual void dirtyPriceFromYieldUS0()
	  {
		double dirtyPrice0 = PRICER.dirtyPriceFromYield(PRODUCT_US_0, SETTLEMENT_US, 0.0d);
		assertEquals(dirtyPrice0, 1.0d, TOL);
		double dirtyPrice = PRICER.dirtyPriceFromYield(PRODUCT_US_0, SETTLEMENT_US, YIELD_US);
		assertEquals(dirtyPrice, 0.8129655023939295, TOL); // Previous run
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_US_0, SETTLEMENT_US, dirtyPrice);
		assertEquals(yield, YIELD_US, TOL);
	  }

	  public virtual void dirtyPriceFromYieldUSLastPeriod()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US);
		assertEquals(dirtyPrice, 1.005635683760684, TOL); // 2.x.
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_US, SETTLEMENT_LAST_US, dirtyPrice);
		assertEquals(yield, YIELD_US, TOL);
	  }

	  public virtual void modifiedDurationFromYieldUS()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void modifiedDurationFromYieldUSLastPeriod()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldUS()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldUSLastPeriod()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void macaulayDurationFromYieldUS()
	  {
		double duration = PRICER.macaulayDurationFromYield(PRODUCT_US, SETTLEMENT_US, YIELD_US);
		assertEquals(duration, 4.6575232098896215, TOL); // 2.x.
	  }

	  public virtual void macaulayDurationFromYieldUSLastPeriod()
	  {
		double duration = PRICER.macaulayDurationFromYield(PRODUCT_US, SETTLEMENT_LAST_US, YIELD_US);
		assertEquals(duration, 0.43478260869565216, TOL); // 2.x.
	  }

	  /* UK BUMP/DMO convention */
	  private static readonly LocalDate START_UK = date(2002, 9, 7);
	  private static readonly LocalDate END_UK = START_UK.plusYears(12);
	  private static readonly PeriodicSchedule SCHEDULE_UK = PeriodicSchedule.of(START_UK, END_UK, Frequency.P6M, BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, SAT_SUN), StubConvention.SHORT_INITIAL, false);
	  private static readonly ResolvedFixedCouponBond PRODUCT_UK = FixedCouponBond.builder().securityId(SECURITY_ID).dayCount(DayCounts.ACT_ACT_ICMA).fixedRate(0.05).legalEntityId(ISSUER_ID).currency(Currency.GBP).notional(100).accrualSchedule(SCHEDULE_UK).settlementDateOffset(DaysAdjustment.ofBusinessDays(1, SAT_SUN)).yieldConvention(FixedCouponBondYieldConvention.GB_BUMP_DMO).exCouponPeriod(DaysAdjustment.ofCalendarDays(-7, BusinessDayAdjustment.of(BusinessDayConventions.PRECEDING, SAT_SUN))).build().resolve(REF_DATA);
	  private static readonly LocalDate VALUATION_UK = date(2011, 9, 2);
	  private static readonly LocalDate SETTLEMENT_UK = PRODUCT_UK.SettlementDateOffset.adjust(VALUATION_UK, REF_DATA);
	  private static readonly LocalDate VALUATION_LAST_UK = date(2014, 6, 3);
	  private static readonly LocalDate SETTLEMENT_LAST_UK = PRODUCT_UK.SettlementDateOffset.adjust(VALUATION_LAST_UK, REF_DATA);
	  private const double YIELD_UK = 0.04;

	  public virtual void dirtyPriceFromYieldUK()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK);
		assertEquals(dirtyPrice, 1.0277859038905428, TOL); // 2.x.
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_UK, SETTLEMENT_UK, dirtyPrice);
		assertEquals(yield, YIELD_UK, TOL);
	  }

	  public virtual void dirtyPriceFromYieldUKLastPeriod()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK);
		assertEquals(dirtyPrice, 1.0145736043763598, TOL); // 2.x.
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_UK, SETTLEMENT_LAST_UK, dirtyPrice);
		assertEquals(yield, YIELD_UK, TOL);
	  }

	  public virtual void modifiedDurationFromYieldUK()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void modifiedDurationFromYieldUKLastPeriod()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldUK()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldUKLastPeriod()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void macaulayDurationFromYieldUK()
	  {
		double duration = PRICER.macaulayDurationFromYield(PRODUCT_UK, SETTLEMENT_UK, YIELD_UK);
		assertEquals(duration, 2.8312260658609163, TOL); // 2.x.
	  }

	  public virtual void macaulayDurationFromYieldUKLastPeriod()
	  {
		double duration = PRICER.macaulayDurationFromYield(PRODUCT_UK, SETTLEMENT_LAST_UK, YIELD_UK);
		assertEquals(duration, 0.25815217391304346, TOL); // 2.x.
	  }

	  /* German bond convention */
	  private static readonly LocalDate START_GER = date(2002, 9, 7);
	  private static readonly LocalDate END_GER = START_GER.plusYears(12);
	  private static readonly PeriodicSchedule SCHEDULE_GER = PeriodicSchedule.of(START_GER, END_GER, Frequency.P12M, BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, SAT_SUN), StubConvention.SHORT_INITIAL, false);
	  private static readonly ResolvedFixedCouponBond PRODUCT_GER = FixedCouponBond.builder().securityId(SECURITY_ID).dayCount(DayCounts.ACT_ACT_ICMA).fixedRate(0.05).legalEntityId(ISSUER_ID).currency(Currency.EUR).notional(100).accrualSchedule(SCHEDULE_GER).settlementDateOffset(DaysAdjustment.ofBusinessDays(3, SAT_SUN)).yieldConvention(FixedCouponBondYieldConvention.DE_BONDS).exCouponPeriod(DaysAdjustment.NONE).build().resolve(REF_DATA);
	  private static readonly LocalDate VALUATION_GER = date(2011, 9, 2);
	  private static readonly LocalDate SETTLEMENT_GER = PRODUCT_GER.SettlementDateOffset.adjust(VALUATION_GER, REF_DATA);
	  private static readonly LocalDate VALUATION_LAST_GER = date(2014, 6, 3);
	  private static readonly LocalDate SETTLEMENT_LAST_GER = PRODUCT_GER.SettlementDateOffset.adjust(VALUATION_LAST_GER, REF_DATA);
	  private const double YIELD_GER = 0.04;

	  public virtual void dirtyPriceFromYieldGerman()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER);
		assertEquals(dirtyPrice, 1.027750910332271, TOL); // 2.x.
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_GER, SETTLEMENT_GER, dirtyPrice);
		assertEquals(yield, YIELD_GER, TOL);
	  }

	  public virtual void dirtyPriceFromYieldGermanLastPeriod()
	  {
		double dirtyPrice = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER);
		assertEquals(dirtyPrice, 1.039406595790844, TOL); // 2.x.
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_GER, SETTLEMENT_LAST_GER, dirtyPrice);
		assertEquals(yield, YIELD_GER, TOL);
	  }

	  public virtual void modifiedDurationFromYieldGER()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void modifiedDurationFromYieldGERLastPeriod()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldGER()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldGERLastPeriod()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void macaulayDurationFromYieldGER()
	  {
		double duration = PRICER.macaulayDurationFromYield(PRODUCT_GER, SETTLEMENT_GER, YIELD_GER);
		assertEquals(duration, 2.861462874541554, TOL); // 2.x.
	  }

	  public virtual void macaulayDurationFromYieldGERLastPeriod()
	  {
		double duration = PRICER.macaulayDurationFromYield(PRODUCT_GER, SETTLEMENT_LAST_GER, YIELD_GER);
		assertEquals(duration, 0.26231286613148186, TOL); // 2.x.
	  }

	  /* Japan simple convention */
	  private static readonly LocalDate START_JP = date(2015, 9, 20);
	  private static readonly LocalDate END_JP = START_JP.plusYears(10);
	  private static readonly PeriodicSchedule SCHEDULE_JP = PeriodicSchedule.of(START_JP, END_JP, Frequency.P6M, BusinessDayAdjustment.of(BusinessDayConventions.FOLLOWING, JPTO), StubConvention.SHORT_INITIAL, false);
	  private const double RATE_JP = 0.004;
	  private static readonly ResolvedFixedCouponBond PRODUCT_JP = FixedCouponBond.builder().securityId(SECURITY_ID).dayCount(DayCounts.NL_365).fixedRate(RATE_JP).legalEntityId(ISSUER_ID).currency(Currency.JPY).notional(100).accrualSchedule(SCHEDULE_JP).settlementDateOffset(DaysAdjustment.ofBusinessDays(3, JPTO)).yieldConvention(FixedCouponBondYieldConvention.JP_SIMPLE).exCouponPeriod(DaysAdjustment.NONE).build().resolve(REF_DATA);
	  private static readonly LocalDate VALUATION_JP = date(2015, 9, 24);
	  private static readonly LocalDate SETTLEMENT_JP = PRODUCT_JP.SettlementDateOffset.adjust(VALUATION_JP, REF_DATA);
	  private static readonly LocalDate VALUATION_LAST_JP = date(2025, 6, 3);
	  private static readonly LocalDate SETTLEMENT_LAST_JP = PRODUCT_JP.SettlementDateOffset.adjust(VALUATION_LAST_JP, REF_DATA);
	  private static readonly LocalDate VALUATION_ENDED_JP = date(2026, 8, 3);
	  private static readonly LocalDate SETTLEMENT_ENDED_JP = PRODUCT_JP.SettlementDateOffset.adjust(VALUATION_ENDED_JP, REF_DATA);
	  private const double YIELD_JP = 0.00321;

	  public virtual void dirtyPriceFromYieldJP()
	  {
		double computed = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP);
		double maturity = DayCounts.NL_365.relativeYearFraction(SETTLEMENT_JP, END_JP);
		double expected = PRICER.dirtyPriceFromCleanPrice(PRODUCT_JP, SETTLEMENT_JP, (1d + RATE_JP * maturity) / (1d + YIELD_JP * maturity));
		assertEquals(computed, expected, TOL);
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_JP, SETTLEMENT_JP, computed);
		assertEquals(yield, YIELD_JP, TOL);
	  }

	  public virtual void dirtyPriceFromYieldJPLastPeriod()
	  {
		double computed = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP);
		double maturity = DayCounts.NL_365.relativeYearFraction(SETTLEMENT_LAST_JP, END_JP);
		double expected = PRICER.dirtyPriceFromCleanPrice(PRODUCT_JP, SETTLEMENT_LAST_JP, (1d + RATE_JP * maturity) / (1d + YIELD_JP * maturity));
		assertEquals(computed, expected, TOL);
		double yield = PRICER.yieldFromDirtyPrice(PRODUCT_JP, SETTLEMENT_LAST_JP, computed);
		assertEquals(yield, YIELD_JP, TOL);
	  }

	  public virtual void dirtyPriceFromYieldJPEnded()
	  {
		double computed = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_ENDED_JP, YIELD_JP);
		assertEquals(computed, 0d, TOL);
	  }

	  public virtual void modifiedDurationFromYielddJP()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void modifiedDurationFromYieldJPLastPeriod()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP);
		double price = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP);
		double priceUp = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP + EPS);
		double priceDw = PRICER.dirtyPriceFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP - EPS);
		double expected = 0.5 * (priceDw - priceUp) / price / EPS;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void modifiedDurationFromYielddJPEnded()
	  {
		double computed = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_ENDED_JP, YIELD_JP);
		assertEquals(computed, 0d, EPS);
	  }

	  public virtual void convexityFromYieldJP()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldJPLastPeriod()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP);
		double duration = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP);
		double durationUp = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP + EPS);
		double durationDw = PRICER.modifiedDurationFromYield(PRODUCT_JP, SETTLEMENT_LAST_JP, YIELD_JP - EPS);
		double expected = 0.5 * (durationDw - durationUp) / EPS + duration * duration;
		assertEquals(computed, expected, EPS);
	  }

	  public virtual void convexityFromYieldJPEnded()
	  {
		double computed = PRICER.convexityFromYield(PRODUCT_JP, SETTLEMENT_ENDED_JP, YIELD_JP);
		assertEquals(computed, 0d, EPS);
	  }

	  public virtual void macaulayDurationFromYieldYieldJP()
	  {
		assertThrows(() => PRICER.macaulayDurationFromYield(PRODUCT_JP, SETTLEMENT_JP, YIELD_JP), typeof(System.NotSupportedException), "The convention JP_SIMPLE is not supported.");
	  }

	}

}