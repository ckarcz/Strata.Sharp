﻿/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.bond
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.currency.Currency.GBP;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.currency.Currency.USD;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.DayCounts.ACT_365F;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverBeanEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverImmutableBean;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.date;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;

	using Test = org.testng.annotations.Test;

	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using CurveMetadata = com.opengamma.strata.market.curve.CurveMetadata;
	using CurveName = com.opengamma.strata.market.curve.CurveName;
	using Curves = com.opengamma.strata.market.curve.Curves;
	using InterpolatedNodalCurve = com.opengamma.strata.market.curve.InterpolatedNodalCurve;
	using RepoGroup = com.opengamma.strata.market.curve.RepoGroup;
	using CurveInterpolator = com.opengamma.strata.market.curve.interpolator.CurveInterpolator;
	using CurveInterpolators = com.opengamma.strata.market.curve.interpolator.CurveInterpolators;
	using CurrencyParameterSensitivities = com.opengamma.strata.market.param.CurrencyParameterSensitivities;

	/// <summary>
	/// Test <seealso cref="RepoCurveDiscountFactors"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class RepoCurveDiscountFactorsTest
	public class RepoCurveDiscountFactorsTest
	{

	  private static readonly LocalDate DATE = date(2015, 6, 4);
	  private static readonly LocalDate DATE_AFTER = date(2015, 7, 30);
	  private static readonly CurveInterpolator INTERPOLATOR = CurveInterpolators.LINEAR;
	  private static readonly CurveName NAME = CurveName.of("TestCurve");
	  private static readonly CurveMetadata METADATA = Curves.zeroRates(NAME, ACT_365F);
	  private static readonly InterpolatedNodalCurve CURVE = InterpolatedNodalCurve.of(METADATA, DoubleArray.of(0, 10), DoubleArray.of(1, 2), INTERPOLATOR);
	  private static readonly DiscountFactors DSC_FACTORS = ZeroRateDiscountFactors.of(GBP, DATE, CURVE);
	  private static readonly RepoGroup GROUP = RepoGroup.of("ISSUER1 BND 5Y");

	  public virtual void test_of()
	  {
		RepoCurveDiscountFactors test = RepoCurveDiscountFactors.of(DSC_FACTORS, GROUP);
		assertEquals(test.RepoGroup, GROUP);
		assertEquals(test.Currency, GBP);
		assertEquals(test.ValuationDate, DATE);
		assertEquals(test.discountFactor(DATE_AFTER), DSC_FACTORS.discountFactor(DATE_AFTER));
	  }

	  public virtual void test_zeroRatePointSensitivity()
	  {
		RepoCurveDiscountFactors @base = RepoCurveDiscountFactors.of(DSC_FACTORS, GROUP);
		RepoCurveZeroRateSensitivity expected = RepoCurveZeroRateSensitivity.of(DSC_FACTORS.zeroRatePointSensitivity(DATE_AFTER), GROUP);
		RepoCurveZeroRateSensitivity computed = @base.zeroRatePointSensitivity(DATE_AFTER);
		assertEquals(computed, expected);
	  }

	  public virtual void test_zeroRatePointSensitivity_USD()
	  {
		RepoCurveDiscountFactors @base = RepoCurveDiscountFactors.of(DSC_FACTORS, GROUP);
		RepoCurveZeroRateSensitivity expected = RepoCurveZeroRateSensitivity.of(DSC_FACTORS.zeroRatePointSensitivity(DATE_AFTER, USD), GROUP);
		RepoCurveZeroRateSensitivity computed = @base.zeroRatePointSensitivity(DATE_AFTER, USD);
		assertEquals(computed, expected);
	  }

	  public virtual void test_parameterSensitivity()
	  {
		RepoCurveDiscountFactors @base = RepoCurveDiscountFactors.of(DSC_FACTORS, GROUP);
		RepoCurveZeroRateSensitivity sensi = @base.zeroRatePointSensitivity(DATE_AFTER, USD);
		CurrencyParameterSensitivities computed = @base.parameterSensitivity(sensi);
		CurrencyParameterSensitivities expected = DSC_FACTORS.parameterSensitivity(DSC_FACTORS.zeroRatePointSensitivity(DATE_AFTER, USD));
		assertEquals(computed, expected);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void coverage()
	  {
		RepoCurveDiscountFactors test1 = RepoCurveDiscountFactors.of(DSC_FACTORS, GROUP);
		coverImmutableBean(test1);
		RepoCurveDiscountFactors test2 = RepoCurveDiscountFactors.of(ZeroRateDiscountFactors.of(USD, DATE, CURVE), RepoGroup.of("ISSUER2"));
		coverBeanEquals(test1, test2);
	  }

	}

}