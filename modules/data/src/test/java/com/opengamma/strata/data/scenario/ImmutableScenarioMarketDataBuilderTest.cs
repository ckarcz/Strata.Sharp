﻿using System.Collections.Generic;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.data.scenario
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertThrowsIllegalArg;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.date;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.assertj.core.api.Assertions.assertThat;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;


	using Test = org.testng.annotations.Test;

	using ImmutableList = com.google.common.collect.ImmutableList;
	using ImmutableMap = com.google.common.collect.ImmutableMap;
	using ImmutableSet = com.google.common.collect.ImmutableSet;
	using StandardId = com.opengamma.strata.basics.StandardId;
	using Currency = com.opengamma.strata.basics.currency.Currency;
	using FxRate = com.opengamma.strata.basics.currency.FxRate;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;
	using LocalDateDoubleTimeSeries = com.opengamma.strata.collect.timeseries.LocalDateDoubleTimeSeries;

	/// <summary>
	/// Test <seealso cref="ImmutableScenarioMarketDataBuilder"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class ImmutableScenarioMarketDataBuilderTest
	public class ImmutableScenarioMarketDataBuilderTest
	{

	  private static readonly LocalDate VAL_DATE = LocalDate.of(2011, 3, 8);

	  private static readonly TestId TEST_ID1 = new TestId("1");
	  private static readonly TestId TEST_ID2 = new TestId("2");
	  private static readonly TestId TEST_ID3 = new TestId("3");

	  //-------------------------------------------------------------------------
	  public virtual void addNothing()
	  {
		ImmutableScenarioMarketData marketData = ImmutableScenarioMarketData.builder(VAL_DATE).build();
		assertEquals(marketData.ScenarioCount, 1);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_values()
	  {
		FxRateId eurGbpId = FxRateId.of(Currency.EUR, Currency.GBP);
		FxRateId eurUsdId = FxRateId.of(Currency.EUR, Currency.USD);
		FxRate eurGbpRate1 = FxRate.of(Currency.EUR, Currency.GBP, 0.85);
		FxRate eurGbpRate2 = FxRate.of(Currency.EUR, Currency.GBP, 0.8);
		FxRate eurUsdRate2 = FxRate.of(Currency.EUR, Currency.USD, 1.1);
		IDictionary<FxRateId, FxRate> values1 = ImmutableMap.of(eurGbpId, eurGbpRate1);
		IDictionary<FxRateId, FxRate> values2 = ImmutableMap.of(eurGbpId, eurGbpRate2, eurUsdId, eurUsdRate2);

		ImmutableScenarioMarketData marketData = ImmutableScenarioMarketData.builder(VAL_DATE).values(values1).values(values2).build();
		assertEquals(marketData.ScenarioCount, 1);
		assertEquals(marketData.Ids, ImmutableSet.of(eurGbpId, eurUsdId));
		assertEquals(marketData.getValue(eurGbpId), MarketDataBox.ofSingleValue(eurGbpRate2));
		assertEquals(marketData.getValue(eurUsdId), MarketDataBox.ofSingleValue(eurUsdRate2));
	  }

	  public virtual void test_addSingleAndList()
	  {
		FxRateId eurGbpId = FxRateId.of(Currency.EUR, Currency.GBP);
		FxRateId eurUsdId = FxRateId.of(Currency.EUR, Currency.USD);
		FxRate eurGbpRate = FxRate.of(Currency.EUR, Currency.GBP, 0.8);
		FxRate eurUsdRate1 = FxRate.of(Currency.EUR, Currency.USD, 1.1);
		FxRate eurUsdRate2 = FxRate.of(Currency.EUR, Currency.USD, 1.2);

		ImmutableScenarioMarketData marketData = ImmutableScenarioMarketData.builder(VAL_DATE).addValue(eurGbpId, eurGbpRate).addScenarioValue(eurUsdId, ImmutableList.of(eurUsdRate1, eurUsdRate2)).build();
		assertEquals(marketData.ScenarioCount, 2);
		assertEquals(marketData.Ids, ImmutableSet.of(eurGbpId, eurUsdId));
		assertEquals(marketData.getValue(eurGbpId), MarketDataBox.ofSingleValue(eurGbpRate));
		assertEquals(marketData.getValue(eurUsdId), MarketDataBox.ofScenarioValues(eurUsdRate1, eurUsdRate2));
	  }

	  public virtual void test_addSingleAndBox()
	  {
		FxRateId eurGbpId = FxRateId.of(Currency.EUR, Currency.GBP);
		FxRateId eurUsdId = FxRateId.of(Currency.EUR, Currency.USD);
		FxRate eurGbpRate = FxRate.of(Currency.EUR, Currency.GBP, 0.8);
		FxRate eurUsdRate1 = FxRate.of(Currency.EUR, Currency.USD, 1.1);
		FxRate eurUsdRate2 = FxRate.of(Currency.EUR, Currency.USD, 1.2);

		ImmutableScenarioMarketData marketData = ImmutableScenarioMarketData.builder(VAL_DATE).addValue(eurGbpId, eurGbpRate).addBox(eurUsdId, MarketDataBox.ofScenarioValues(eurUsdRate1, eurUsdRate2)).build();
		assertEquals(marketData.ScenarioCount, 2);
		assertEquals(marketData.Ids, ImmutableSet.of(eurGbpId, eurUsdId));
		assertEquals(marketData.getValue(eurGbpId), MarketDataBox.ofSingleValue(eurGbpRate));
		assertEquals(marketData.getValue(eurUsdId), MarketDataBox.ofScenarioValues(eurUsdRate1, eurUsdRate2));
	  }

	  public virtual void test_addBadScenarioCount()
	  {
		FxRateId eurGbpId = FxRateId.of(Currency.EUR, Currency.GBP);
		FxRateId eurUsdId = FxRateId.of(Currency.EUR, Currency.USD);
		FxRate eurGbpRate1 = FxRate.of(Currency.EUR, Currency.GBP, 0.8);
		FxRate eurGbpRate2 = FxRate.of(Currency.EUR, Currency.GBP, 0.9);
		FxRate eurGbpRate3 = FxRate.of(Currency.EUR, Currency.GBP, 0.95);
		FxRate eurUsdRate1 = FxRate.of(Currency.EUR, Currency.USD, 1.1);
		FxRate eurUsdRate2 = FxRate.of(Currency.EUR, Currency.USD, 1.2);

		ImmutableScenarioMarketDataBuilder builder = ImmutableScenarioMarketData.builder(VAL_DATE).addBox(eurGbpId, MarketDataBox.ofScenarioValues(eurGbpRate1, eurGbpRate2, eurGbpRate3));
		assertThrowsIllegalArg(() => builder.addBox(eurUsdId, MarketDataBox.ofScenarioValues(eurUsdRate1, eurUsdRate2)));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_addValueMap()
	  {
		FxRateId eurGbpId = FxRateId.of(Currency.EUR, Currency.GBP);
		FxRateId eurUsdId = FxRateId.of(Currency.EUR, Currency.USD);
		FxRate eurGbpRate = FxRate.of(Currency.EUR, Currency.GBP, 0.8);
		FxRate eurUsdRate = FxRate.of(Currency.EUR, Currency.USD, 1.1);
		IDictionary<FxRateId, FxRate> values = ImmutableMap.of(eurGbpId, eurGbpRate, eurUsdId, eurUsdRate);

		ImmutableScenarioMarketData marketData = ImmutableScenarioMarketData.builder(VAL_DATE).addValueMap(values).build();
		assertEquals(marketData.ScenarioCount, 1);
		assertEquals(marketData.Ids, ImmutableSet.of(eurGbpId, eurUsdId));
		assertEquals(marketData.getValue(eurGbpId), MarketDataBox.ofSingleValue(eurGbpRate));
		assertEquals(marketData.getValue(eurUsdId), MarketDataBox.ofSingleValue(eurUsdRate));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_addScenarioValueMap()
	  {
		FxRateId eurGbpId = FxRateId.of(Currency.EUR, Currency.GBP);
		FxRateId eurUsdId = FxRateId.of(Currency.EUR, Currency.USD);
		FxRateScenarioArray eurGbpRates = FxRateScenarioArray.of(Currency.EUR, Currency.GBP, DoubleArray.of(0.79, 0.8, 0.81));
		FxRateScenarioArray eurUsdRates = FxRateScenarioArray.of(Currency.EUR, Currency.USD, DoubleArray.of(1.09, 1.1, 1.11));
		IDictionary<FxRateId, FxRateScenarioArray> values = ImmutableMap.of(eurGbpId, eurGbpRates, eurUsdId, eurUsdRates);

		ImmutableScenarioMarketData marketData = ImmutableScenarioMarketData.builder(VAL_DATE).addScenarioValueMap(values).build();
		assertEquals(marketData.ScenarioCount, 3);
		assertEquals(marketData.Ids, ImmutableSet.of(eurGbpId, eurUsdId));
		assertEquals(marketData.getValue(eurGbpId), MarketDataBox.ofScenarioValue(eurGbpRates));
		assertEquals(marketData.getValue(eurUsdId), MarketDataBox.ofScenarioValue(eurUsdRates));
	  }

	  public virtual void test_combinedWith()
	  {
		LocalDateDoubleTimeSeries timeSeries1 = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 1).put(date(2011, 3, 9), 2).put(date(2011, 3, 10), 3).build();

		LocalDateDoubleTimeSeries timeSeries2 = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 10).put(date(2011, 3, 9), 20).put(date(2011, 3, 10), 30).build();

		LocalDateDoubleTimeSeries timeSeries2a = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 1000).put(date(2011, 3, 9), 2000).put(date(2011, 3, 10), 3000).build();

		LocalDateDoubleTimeSeries timeSeries3 = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 100).put(date(2011, 3, 9), 200).put(date(2011, 3, 10), 300).build();

		ImmutableScenarioMarketData marketData1 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addTimeSeries(TEST_ID1, timeSeries1).addTimeSeries(TEST_ID2, timeSeries2).addBox(TEST_ID1, MarketDataBox.ofScenarioValues(1.0, 1.1, 1.2)).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(2.0, 2.1, 2.2)).build();

		ImmutableScenarioMarketData marketData2 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 10)).addTimeSeries(TEST_ID2, timeSeries2a).addTimeSeries(TEST_ID3, timeSeries3).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(21.0, 21.1, 21.2)).addBox(TEST_ID3, MarketDataBox.ofScenarioValues(3.0, 3.1, 3.2)).build();

		// marketData1 values should be in the combined data when the same ID is present in both
		ImmutableScenarioMarketData expected = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addTimeSeries(TEST_ID1, timeSeries1).addTimeSeries(TEST_ID2, timeSeries2).addTimeSeries(TEST_ID3, timeSeries3).addBox(TEST_ID1, MarketDataBox.ofScenarioValues(1.0, 1.1, 1.2)).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(2.0, 2.1, 2.2)).addBox(TEST_ID3, MarketDataBox.ofScenarioValues(3.0, 3.1, 3.2)).build();

		ScenarioMarketData combined = marketData1.combinedWith(marketData2);
		assertThat(combined).isEqualTo(expected);
	  }

	  public virtual void test_combinedWithIncompatibleScenarioCount()
	  {
		ImmutableScenarioMarketData marketData1 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID1, MarketDataBox.ofScenarioValues(1.0, 1.1, 1.2)).build();

		ImmutableScenarioMarketData marketData2 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(1.0, 1.1)).build();

		assertThrowsIllegalArg(() => marketData1.combinedWith(marketData2), ".* same number of scenarios .* 3 and 2");
	  }

	  public virtual void test_combinedWithReceiverHasOneScenario()
	  {
		ImmutableScenarioMarketData marketData1 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID1, MarketDataBox.ofSingleValue(1.0)).build();

		ImmutableScenarioMarketData marketData2 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(1.0, 1.1)).build();

		ImmutableScenarioMarketData expected = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID1, MarketDataBox.ofSingleValue(1.0)).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(1.0, 1.1)).build();

		assertThat(marketData1.combinedWith(marketData2)).isEqualTo(expected);
	  }

	  public virtual void test_combinedWithOtherHasOneScenario()
	  {
		ImmutableScenarioMarketData marketData1 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(1.0, 1.1)).build();

		ImmutableScenarioMarketData marketData2 = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID1, MarketDataBox.ofSingleValue(1.0)).build();

		ImmutableScenarioMarketData expected = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addBox(TEST_ID1, MarketDataBox.ofSingleValue(1.0)).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(1.0, 1.1)).build();

		assertThat(marketData1.combinedWith(marketData2)).isEqualTo(expected);
	  }

	  /// <summary>
	  /// Tests the combinedWith method when the other set of market data is not an instance of ImmutableScenarioMarketData
	  /// </summary>
	  public virtual void test_combinedWithDifferentImpl()
	  {
		LocalDateDoubleTimeSeries timeSeries1 = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 1).put(date(2011, 3, 9), 2).put(date(2011, 3, 10), 3).build();

		LocalDateDoubleTimeSeries timeSeries2 = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 10).put(date(2011, 3, 9), 20).put(date(2011, 3, 10), 30).build();

		LocalDateDoubleTimeSeries timeSeries2a = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 1000).put(date(2011, 3, 9), 2000).put(date(2011, 3, 10), 3000).build();

		LocalDateDoubleTimeSeries timeSeries3 = LocalDateDoubleTimeSeries.builder().put(date(2011, 3, 8), 100).put(date(2011, 3, 9), 200).put(date(2011, 3, 10), 300).build();

		MarketData marketData = ImmutableMarketData.builder(LocalDate.of(2011, 3, 8)).addTimeSeries(TEST_ID1, timeSeries1).addTimeSeries(TEST_ID2, timeSeries2).addValue(TEST_ID1, 1.1).addValue(TEST_ID2, 1.2).build();

		RepeatedScenarioMarketData repeatedScenarioMarketData = RepeatedScenarioMarketData.of(3, marketData);

		ImmutableScenarioMarketData immutableScenarioMarketData = ImmutableScenarioMarketData.builder(LocalDate.of(2011, 3, 8)).addTimeSeries(TEST_ID2, timeSeries2a).addTimeSeries(TEST_ID3, timeSeries3).addBox(TEST_ID2, MarketDataBox.ofScenarioValues(2.0, 2.1, 2.2)).addBox(TEST_ID3, MarketDataBox.ofScenarioValues(3.0, 3.1, 3.2)).build();

		ScenarioMarketData combinedData = immutableScenarioMarketData.combinedWith(repeatedScenarioMarketData);
		assertThat(combinedData.ScenarioCount).isEqualTo(3);
		assertThat(combinedData.getValue(TEST_ID1).getValue(0)).isEqualTo(1.1);
		assertThat(combinedData.getValue(TEST_ID1).getValue(2)).isEqualTo(1.1);
		assertThat(combinedData.getValue(TEST_ID1).getValue(3)).isEqualTo(1.1);
		assertThat(combinedData.getValue(TEST_ID2)).isEqualTo(MarketDataBox.ofScenarioValues(2.0, 2.1, 2.2));
		assertThat(combinedData.getValue(TEST_ID3)).isEqualTo(MarketDataBox.ofScenarioValues(3.0, 3.1, 3.2));
		assertThat(combinedData.getTimeSeries(TEST_ID1)).isEqualTo(timeSeries1);
		assertThat(combinedData.getTimeSeries(TEST_ID2)).isEqualTo(timeSeries2a);
		assertThat(combinedData.getTimeSeries(TEST_ID3)).isEqualTo(timeSeries3);
	  }

	  //-------------------------------------------------------------------------
	  private sealed class TestId : ObservableId
	  {

		internal readonly string id;

		internal TestId(string id)
		{
		  this.id = id;
		}

		public StandardId StandardId
		{
			get
			{
			  throw new System.NotSupportedException("getStandardId not implemented");
			}
		}

		public FieldName FieldName
		{
			get
			{
			  throw new System.NotSupportedException("getFieldName not implemented");
			}
		}

		public ObservableSource ObservableSource
		{
			get
			{
			  throw new System.NotSupportedException("getObservableSource not implemented");
			}
		}

		public ObservableId withObservableSource(ObservableSource obsSource)
		{
		  throw new System.NotSupportedException("withObservableSource not implemented");
		}

		public override bool Equals(object o)
		{
		  if (this == o)
		  {
			return true;
		  }
		  if (o == null || this.GetType() != o.GetType())
		  {
			return false;
		  }
		  TestId testId = (TestId) o;
		  return Objects.Equals(id, testId.id);
		}

		public override int GetHashCode()
		{
		  return Objects.hash(id);
		}
	  }
	}

}