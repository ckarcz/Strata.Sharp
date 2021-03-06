﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.loader.csv
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverBeanEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverImmutableBean;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverPrivateConstructor;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.assertj.core.api.Assertions.assertThat;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.assertj.core.api.Assertions.offset;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertNotNull;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertTrue;


	using Test = org.testng.annotations.Test;

	using ImmutableList = com.google.common.collect.ImmutableList;
	using Iterables = com.google.common.collect.Iterables;
	using ListMultimap = com.google.common.collect.ListMultimap;
	using Currency = com.opengamma.strata.basics.currency.Currency;
	using DayCounts = com.opengamma.strata.basics.date.DayCounts;
	using IborIndices = com.opengamma.strata.basics.index.IborIndices;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using ResourceLocator = com.opengamma.strata.collect.io.ResourceLocator;
	using ValueType = com.opengamma.strata.market.ValueType;
	using Curve = com.opengamma.strata.market.curve.Curve;
	using CurveGroupName = com.opengamma.strata.market.curve.CurveGroupName;
	using CurveName = com.opengamma.strata.market.curve.CurveName;
	using InterpolatedNodalCurve = com.opengamma.strata.market.curve.InterpolatedNodalCurve;
	using RatesCurveGroup = com.opengamma.strata.market.curve.RatesCurveGroup;
	using CurveExtrapolators = com.opengamma.strata.market.curve.interpolator.CurveExtrapolators;
	using CurveInterpolators = com.opengamma.strata.market.curve.interpolator.CurveInterpolators;
	using ParameterMetadata = com.opengamma.strata.market.param.ParameterMetadata;

	/// <summary>
	/// Test <seealso cref="RatesCurvesCsvLoader"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class RatesCurvesCsvLoaderTest
	public class RatesCurvesCsvLoaderTest
	{

	  private const string GROUPS_1 = "classpath:com/opengamma/strata/loader/csv/groups.csv";
	  private const string SETTINGS_1 = "classpath:com/opengamma/strata/loader/csv/settings.csv";
	  private const string CURVES_1 = "classpath:com/opengamma/strata/loader/csv/curves-1.csv";
	  private const string CURVES_2 = "classpath:com/opengamma/strata/loader/csv/curves-2.csv";
	  private const string CURVES_3 = "classpath:com/opengamma/strata/loader/csv/curves-3.csv";
	  private const string CURVES_1_AND_2 = "classpath:com/opengamma/strata/loader/csv/curves-1-and-2.csv";

	  private const string SETTINGS_INVALID_DAY_COUNT = "classpath:com/opengamma/strata/loader/csv/settings-invalid-day-count.csv";
	  private const string SETTINGS_INVALID_INTERPOLATOR = "classpath:com/opengamma/strata/loader/csv/settings-invalid-interpolator.csv";
	  private const string SETTINGS_INVALID_LEFT_EXTRAPOLATOR = "classpath:com/opengamma/strata/loader/csv/settings-invalid-left-extrapolator.csv";
	  private const string SETTINGS_INVALID_RIGHT_EXTRAPOLATOR = "classpath:com/opengamma/strata/loader/csv/settings-invalid-right-extrapolator.csv";
	  private const string SETTINGS_INVALID_MISSING_COLUMN = "classpath:com/opengamma/strata/loader/csv/settings-invalid-missing-column.csv";
	  private const string SETTINGS_INVALID_VALUE_TYPE = "classpath:com/opengamma/strata/loader/csv/settings-invalid-value-type.csv";
	  private const string SETTINGS_EMPTY = "classpath:com/opengamma/strata/loader/csv/settings-empty.csv";

	  private const string GROUPS_INVALID_CURVE_TYPE = "classpath:com/opengamma/strata/loader/csv/groups-invalid-curve-type.csv";
	  private const string GROUPS_INVALID_REFERENCE_INDEX = "classpath:com/opengamma/strata/loader/csv/groups-invalid-reference-index.csv";
	  private const string CURVES_INVALID_DUPLICATE_POINTS = "classpath:com/opengamma/strata/loader/csv/curves-invalid-duplicate-points.csv";

	  // curve date used in the test data
	  private static readonly LocalDate CURVE_DATE = LocalDate.of(2009, 7, 31);
	  private static readonly LocalDate CURVE_DATE_CURVES_3 = LocalDate.of(2009, 7, 30);

	  // tolerance
	  private const double TOLERANCE = 1.0E-4;

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class) public void test_missing_settings_file()
	  public virtual void test_missing_settings_file()
	  {
		testSettings("classpath:invalid");
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "Header not found: 'Curve Name'") public void test_invalid_settings_missing_column_file()
	  public virtual void test_invalid_settings_missing_column_file()
	  {
		testSettings(SETTINGS_INVALID_MISSING_COLUMN);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "Unknown DayCount value.*") public void test_invalid_settings_day_count_file()
	  public virtual void test_invalid_settings_day_count_file()
	  {
		testSettings(SETTINGS_INVALID_DAY_COUNT);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "CurveInterpolator name not found: Wacky") public void test_invalid_settings_interpolator_file()
	  public virtual void test_invalid_settings_interpolator_file()
	  {
		testSettings(SETTINGS_INVALID_INTERPOLATOR);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "CurveExtrapolator name not found: Polynomial") public void test_invalid_settings_left_extrapolator_file()
	  public virtual void test_invalid_settings_left_extrapolator_file()
	  {
		testSettings(SETTINGS_INVALID_LEFT_EXTRAPOLATOR);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "CurveExtrapolator name not found: Polynomial") public void test_invalid_settings_right_extrapolator_file()
	  public virtual void test_invalid_settings_right_extrapolator_file()
	  {
		testSettings(SETTINGS_INVALID_RIGHT_EXTRAPOLATOR);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "Unsupported Value Type in curve settings: DS") public void test_invalid_settings_value_type_file()
	  public virtual void test_invalid_settings_value_type_file()
	  {
		testSettings(SETTINGS_INVALID_VALUE_TYPE);
	  }

	  private void testSettings(string settingsResource)
	  {
		RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(settingsResource), ImmutableList.of(ResourceLocator.of(CURVES_1)));
	  }

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class) public void test_missing_groups_file()
	  public virtual void test_missing_groups_file()
	  {
		testGroups("classpath:invalid");
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "Unsupported curve type: Inflation") public void test_invalid_groups_curve_type_file()
	  public virtual void test_invalid_groups_curve_type_file()
	  {
		testGroups(GROUPS_INVALID_CURVE_TYPE);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "Index name not found: LIBOR") public void test_invalid_groups_reference_index_file()
	  public virtual void test_invalid_groups_reference_index_file()
	  {
		testGroups(GROUPS_INVALID_REFERENCE_INDEX);
	  }

	  private void testGroups(string groupsResource)
	  {
		RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(groupsResource), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1)));
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "Missing settings for curve: .*") public void test_noSettings()
	  public virtual void test_noSettings()
	  {
		IList<RatesCurveGroup> curveGroups = RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_EMPTY), ImmutableList.of(ResourceLocator.of(CURVES_1)));

		assertEquals(curveGroups.Count, 1);

		RatesCurveGroup curveGroup = Iterables.getOnlyElement(curveGroups);
		assertUsdDisc(curveGroup.findDiscountCurve(Currency.USD).get());
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class, expectedExceptionsMessageRegExp = "Rates curve loader found multiple curves with the same name: .*") public void test_single_curve_multiple_Files()
	  public virtual void test_single_curve_multiple_Files()
	  {
		RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1), ResourceLocator.of(CURVES_1)));
	  }

	  public virtual void test_multiple_curves_single_file()
	  {
		IList<RatesCurveGroup> curveGroups = RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1_AND_2)));

		assertCurves(curveGroups);
	  }

	  public virtual void test_multiple_curves_multiple_files()
	  {
		IList<RatesCurveGroup> curveGroups = RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1), ResourceLocator.of(CURVES_2)));

		assertCurves(curveGroups);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(expectedExceptions = IllegalArgumentException.class) public void test_invalid_curve_duplicate_points()
	  public virtual void test_invalid_curve_duplicate_points()
	  {
		RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_INVALID_DUPLICATE_POINTS)));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_load_all_curves()
	  {
		ListMultimap<LocalDate, RatesCurveGroup> allGroups = RatesCurvesCsvLoader.loadAllDates(ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1), ResourceLocator.of(CURVES_2), ResourceLocator.of(CURVES_3)));

		assertEquals(allGroups.size(), 2);
		assertCurves(allGroups.get(CURVE_DATE));

		IList<RatesCurveGroup> curves3 = allGroups.get(CURVE_DATE_CURVES_3);
		assertEquals(curves3.Count, 1);
		RatesCurveGroup group = curves3[0];

		// All curve points are set to 0 in test data to ensure these are really different curve instances
		Curve usdDisc = group.findDiscountCurve(Currency.USD).get();
		InterpolatedNodalCurve usdDiscNodal = (InterpolatedNodalCurve) usdDisc;
		assertEquals(usdDiscNodal.Metadata.CurveName, CurveName.of("USD-Disc"));
		assertTrue(usdDiscNodal.YValues.equalZeroWithTolerance(0d));

		Curve usd3ml = group.findForwardCurve(IborIndices.USD_LIBOR_3M).get();
		InterpolatedNodalCurve usd3mlNodal = (InterpolatedNodalCurve) usd3ml;
		assertEquals(usd3mlNodal.Metadata.CurveName, CurveName.of("USD-3ML"));
		assertTrue(usd3mlNodal.YValues.equalZeroWithTolerance(0d));
	  }

	  public virtual void test_load_curves_date_filtering()
	  {
		IList<RatesCurveGroup> curves = RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1), ResourceLocator.of(CURVES_2), ResourceLocator.of(CURVES_3)));

		assertCurves(curves);
	  }

	  //-------------------------------------------------------------------------
	  private void assertCurves(IList<RatesCurveGroup> curveGroups)
	  {
		assertNotNull(curveGroups);
		assertEquals(curveGroups.Count, 1);

		RatesCurveGroup curveGroup = curveGroups[0];
		assertEquals(curveGroup.Name, CurveGroupName.of("Default"));
		assertUsdDisc(curveGroup.findDiscountCurve(Currency.USD).get());

		Curve usd3ml = curveGroup.findForwardCurve(IborIndices.USD_LIBOR_3M).get();
		assertUsd3ml(usd3ml);
	  }

	  private void assertUsdDisc(Curve curve)
	  {
		assertTrue(curve is InterpolatedNodalCurve);
		InterpolatedNodalCurve nodalCurve = (InterpolatedNodalCurve) curve;
		assertEquals(nodalCurve.Metadata.CurveName, CurveName.of("USD-Disc"));

		LocalDate valuationDate = LocalDate.of(2009, 7, 31);
		LocalDate[] nodeDates = new LocalDate[] {LocalDate.of(2009, 11, 6), LocalDate.of(2010, 2, 8), LocalDate.of(2010, 8, 6), LocalDate.of(2011, 8, 8), LocalDate.of(2012, 8, 8), LocalDate.of(2014, 8, 6), LocalDate.of(2019, 8, 7)};
		string[] labels = new string[] {"3M", "6M", "1Y", "2Y", "3Y", "5Y", "10Y"};

		for (int i = 0; i < nodalCurve.XValues.size(); i++)
		{
		  LocalDate nodeDate = nodeDates[i];
		  double actualYearFraction = nodalCurve.XValues.get(i);
		  double expectedYearFraction = getYearFraction(valuationDate, nodeDate);
		  assertThat(actualYearFraction).isCloseTo(expectedYearFraction, offset(TOLERANCE));

		  ParameterMetadata nodeMetadata = nodalCurve.Metadata.ParameterMetadata.get().get(i);
		  assertEquals(nodeMetadata.Label, labels[i]);
		}

		DoubleArray expectedYValues = DoubleArray.of(0.001763775, 0.002187884, 0.004437206, 0.011476741, 0.017859057, 0.026257102, 0.035521988);
		assertEquals(nodalCurve.YValues, expectedYValues);
	  }

	  private void assertUsd3ml(Curve curve)
	  {
		assertTrue(curve is InterpolatedNodalCurve);
		InterpolatedNodalCurve nodalCurve = (InterpolatedNodalCurve) curve;
		assertEquals(nodalCurve.Metadata.CurveName, CurveName.of("USD-3ML"));

		LocalDate valuationDate = LocalDate.of(2009, 7, 31);
		LocalDate[] nodeDates = new LocalDate[] {LocalDate.of(2009, 11, 4), LocalDate.of(2010, 8, 4), LocalDate.of(2011, 8, 4), LocalDate.of(2012, 8, 6), LocalDate.of(2014, 8, 5), LocalDate.of(2019, 8, 6)};
		string[] labels = new string[] {"3M", "1Y", "2Y", "3Y", "5Y", "10Y"};

		for (int i = 0; i < nodalCurve.XValues.size(); i++)
		{
		  LocalDate nodeDate = nodeDates[i];
		  double actualYearFraction = nodalCurve.XValues.get(i);
		  double expectedYearFraction = getYearFraction(valuationDate, nodeDate);
		  assertThat(actualYearFraction).isCloseTo(expectedYearFraction, offset(TOLERANCE));

		  ParameterMetadata nodeMetadata = nodalCurve.Metadata.ParameterMetadata.get().get(i);
		  assertEquals(nodeMetadata.Label, labels[i]);
		}

		DoubleArray expectedYValues = DoubleArray.of(0.007596889, 0.008091541, 0.015244398, 0.021598026, 0.029984216, 0.039245812);
		assertEquals(nodalCurve.YValues, expectedYValues);
	  }

	  private double getYearFraction(LocalDate fromDate, LocalDate toDate)
	  {
		return DayCounts.ACT_ACT_ISDA.yearFraction(fromDate, toDate);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_writer_curve_settings()
	  {
		IList<RatesCurveGroup> curveGroups = RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1), ResourceLocator.of(CURVES_2)));
		Appendable underlying = new StringBuilder();
		RatesCurvesCsvLoader.writeCurveSettings(underlying, curveGroups[0]);
		string created = underlying.ToString();
		string expected = "Curve Name,Value Type,Day Count,Interpolator,Left Extrapolator,Right Extrapolator" + Environment.NewLine +
				"USD-Disc,zero,Act/Act ISDA,Linear,Flat,Flat" + Environment.NewLine +
				"USD-3ML,zero,Act/Act ISDA,Linear,Flat,Flat" + Environment.NewLine;
		assertEquals(created, expected);
	  }

	  public virtual void test_writer_curve_nodes()
	  {
		IList<RatesCurveGroup> curveGroups = RatesCurvesCsvLoader.load(CURVE_DATE, ResourceLocator.of(GROUPS_1), ResourceLocator.of(SETTINGS_1), ImmutableList.of(ResourceLocator.of(CURVES_1), ResourceLocator.of(CURVES_2)));
		Appendable underlying = new StringBuilder();
		RatesCurvesCsvLoader.writeCurveNodes(underlying, CURVE_DATE, curveGroups[0]);
		string created = underlying.ToString();
		string expected = "Valuation Date,Curve Name,Date,Value,Label" + Environment.NewLine +
				"2009-07-31,USD-Disc,2009-11-06,0.001763775,3M" + Environment.NewLine +
				"2009-07-31,USD-Disc,2010-02-08,0.002187884,6M" + Environment.NewLine +
				"2009-07-31,USD-Disc,2010-08-06,0.004437206,1Y" + Environment.NewLine +
				"2009-07-31,USD-Disc,2011-08-08,0.011476741,2Y" + Environment.NewLine +
				"2009-07-31,USD-Disc,2012-08-08,0.017859057,3Y" + Environment.NewLine +
				"2009-07-31,USD-Disc,2014-08-06,0.026257102,5Y" + Environment.NewLine +
				"2009-07-31,USD-Disc,2019-08-07,0.035521988,10Y" + Environment.NewLine +
				"2009-07-31,USD-3ML,2009-11-04,0.007596889,3M" + Environment.NewLine +
				"2009-07-31,USD-3ML,2010-08-04,0.008091541,1Y" + Environment.NewLine +
				"2009-07-31,USD-3ML,2011-08-04,0.015244398,2Y" + Environment.NewLine +
				"2009-07-31,USD-3ML,2012-08-06,0.021598026,3Y" + Environment.NewLine +
				"2009-07-31,USD-3ML,2014-08-05,0.029984216,5Y" + Environment.NewLine +
				"2009-07-31,USD-3ML,2019-08-06,0.039245812,10Y" + Environment.NewLine;
		assertEquals(created, expected);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void coverage()
	  {
		coverPrivateConstructor(typeof(RatesCurvesCsvLoader));
		LoadedCurveKey.meta();
		coverImmutableBean(LoadedCurveKey.of(CURVE_DATE, CurveName.of("Test")));
		LoadedCurveNode.meta();
		coverImmutableBean(LoadedCurveNode.of(CURVE_DATE, 1d, "Test"));
		LoadedCurveSettings.meta();
		LoadedCurveSettings settings1 = LoadedCurveSettings.of(CurveName.of("Test"), ValueType.YEAR_FRACTION, ValueType.ZERO_RATE, DayCounts.ACT_365F, CurveInterpolators.LINEAR, CurveExtrapolators.FLAT, CurveExtrapolators.FLAT);
		LoadedCurveSettings settings2 = LoadedCurveSettings.of(CurveName.of("Test2"), ValueType.YEAR_FRACTION, ValueType.DISCOUNT_FACTOR, DayCounts.ACT_ACT_ISDA, CurveInterpolators.LOG_LINEAR, CurveExtrapolators.LINEAR, CurveExtrapolators.LINEAR);
		coverImmutableBean(settings1);
		coverBeanEquals(settings1, settings2);
	  }

	}

}