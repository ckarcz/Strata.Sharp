﻿using System;
using System.Collections.Generic;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.market.curve.node
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.basics.date.Tenor.TENOR_10Y;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertSerialization;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertThrows;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverBeanEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverImmutableBean;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.date;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.common.BuySell.BUY;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertFalse;


	using Test = org.testng.annotations.Test;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using StandardId = com.opengamma.strata.basics.StandardId;
	using Tenor = com.opengamma.strata.basics.date.Tenor;
	using ImmutableMarketData = com.opengamma.strata.data.ImmutableMarketData;
	using MarketData = com.opengamma.strata.data.MarketData;
	using MarketDataNotFoundException = com.opengamma.strata.data.MarketDataNotFoundException;
	using ObservableId = com.opengamma.strata.data.ObservableId;
	using QuoteId = com.opengamma.strata.market.observable.QuoteId;
	using DatedParameterMetadata = com.opengamma.strata.market.param.DatedParameterMetadata;
	using ParameterMetadata = com.opengamma.strata.market.param.ParameterMetadata;
	using TenorDateParameterMetadata = com.opengamma.strata.market.param.TenorDateParameterMetadata;
	using SwapTrade = com.opengamma.strata.product.swap.SwapTrade;
	using FixedIborSwapConventions = com.opengamma.strata.product.swap.type.FixedIborSwapConventions;
	using FixedIborSwapTemplate = com.opengamma.strata.product.swap.type.FixedIborSwapTemplate;

	/// <summary>
	/// Test <seealso cref="FixedIborSwapCurveNode"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class FixedIborSwapCurveNodeTest
	public class FixedIborSwapCurveNodeTest
	{

	  private static readonly ReferenceData REF_DATA = ReferenceData.standard();
	  private static readonly LocalDate VAL_DATE = date(2015, 6, 30);

	  private static readonly FixedIborSwapTemplate TEMPLATE = FixedIborSwapTemplate.of(TENOR_10Y, FixedIborSwapConventions.USD_FIXED_6M_LIBOR_3M);
	  private static readonly QuoteId QUOTE_ID = QuoteId.of(StandardId.of("OG-Ticker", "Deposit1"));
	  private const double SPREAD = 0.0015;
	  private const string LABEL = "Label";
	  private const string LABEL_AUTO = "10Y";

	  private const double TOLERANCE_DF = 1.0E-10;

	  public virtual void test_builder()
	  {
		FixedIborSwapCurveNode test = FixedIborSwapCurveNode.builder().label(LABEL).template(TEMPLATE).rateId(QUOTE_ID).additionalSpread(SPREAD).build();
		assertEquals(test.Label, LABEL);
		assertEquals(test.RateId, QUOTE_ID);
		assertEquals(test.AdditionalSpread, SPREAD);
		assertEquals(test.Template, TEMPLATE);
		assertEquals(test.Date, CurveNodeDate.END);
	  }

	  public virtual void test_of_noSpread()
	  {
		FixedIborSwapCurveNode test = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID);
		assertEquals(test.Label, LABEL_AUTO);
		assertEquals(test.RateId, QUOTE_ID);
		assertEquals(test.AdditionalSpread, 0.0d);
		assertEquals(test.Template, TEMPLATE);
	  }

	  public virtual void test_of_withSpread()
	  {
		FixedIborSwapCurveNode test = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		assertEquals(test.Label, LABEL_AUTO);
		assertEquals(test.RateId, QUOTE_ID);
		assertEquals(test.AdditionalSpread, SPREAD);
		assertEquals(test.Template, TEMPLATE);
	  }

	  public virtual void test_of_withSpreadAndLabel()
	  {
		FixedIborSwapCurveNode test = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD, LABEL);
		assertEquals(test.Label, LABEL);
		assertEquals(test.RateId, QUOTE_ID);
		assertEquals(test.AdditionalSpread, SPREAD);
		assertEquals(test.Template, TEMPLATE);
	  }

	  public virtual void test_requirements()
	  {
		FixedIborSwapCurveNode test = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		ISet<ObservableId> set = test.requirements();
		IEnumerator<ObservableId> itr = set.GetEnumerator();
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
		assertEquals(itr.next(), QUOTE_ID);
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
		assertFalse(itr.hasNext());
	  }

	  public virtual void test_trade()
	  {
		FixedIborSwapCurveNode node = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		double rate = 0.125;
		double quantity = -1234.56;
		MarketData marketData = ImmutableMarketData.builder(VAL_DATE).addValue(QUOTE_ID, rate).build();
		SwapTrade trade = node.trade(quantity, marketData, REF_DATA);
		SwapTrade expected = TEMPLATE.createTrade(VAL_DATE, BUY, -quantity, rate + SPREAD, REF_DATA);
		assertEquals(trade, expected);
	  }

	  public virtual void test_trade_noMarketData()
	  {
		FixedIborSwapCurveNode node = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		MarketData marketData = MarketData.empty(VAL_DATE);
		assertThrows(() => node.trade(1d, marketData, REF_DATA), typeof(MarketDataNotFoundException));
	  }

	  public virtual void test_initialGuess()
	  {
		FixedIborSwapCurveNode node = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		double rate = 0.035;
		MarketData marketData = ImmutableMarketData.builder(VAL_DATE).addValue(QUOTE_ID, rate).build();
		assertEquals(node.initialGuess(marketData, ValueType.ZERO_RATE), rate);
		assertEquals(node.initialGuess(marketData, ValueType.FORWARD_RATE), rate);
		double df = Math.Exp(-TENOR_10Y.get(ChronoUnit.YEARS) * rate);
		assertEquals(node.initialGuess(marketData, ValueType.DISCOUNT_FACTOR), df, TOLERANCE_DF);
		assertEquals(node.initialGuess(marketData, ValueType.PRICE_INDEX), 0d);
	  }

	  public virtual void test_metadata_end()
	  {
		FixedIborSwapCurveNode node = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		LocalDate valuationDate = LocalDate.of(2015, 1, 22);
		ParameterMetadata metadata = node.metadata(valuationDate, REF_DATA);
		// 2015-01-22 is Thursday, start is 2015-01-26, but 2025-01-26 is Sunday, so end is 2025-01-27
		assertEquals(((TenorDateParameterMetadata) metadata).Date, LocalDate.of(2025, 1, 27));
		assertEquals(((TenorDateParameterMetadata) metadata).Tenor, Tenor.TENOR_10Y);
	  }

	  public virtual void test_metadata_fixed()
	  {
		FixedIborSwapCurveNode node = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD, LABEL).withDate(CurveNodeDate.of(VAL_DATE));
		LocalDate valuationDate = LocalDate.of(2015, 1, 22);
		DatedParameterMetadata metadata = node.metadata(valuationDate, REF_DATA);
		assertEquals(metadata.Date, VAL_DATE);
		assertEquals(metadata.Label, node.Label);
	  }

	  public virtual void test_metadata_last_fixing()
	  {
		FixedIborSwapCurveNode node = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD, LABEL).withDate(CurveNodeDate.LAST_FIXING);
		LocalDate valuationDate = LocalDate.of(2015, 1, 22);
		LocalDate fixingExpected = LocalDate.of(2024, 10, 24);
		DatedParameterMetadata metadata = node.metadata(valuationDate, REF_DATA);
		assertEquals(metadata.Date, fixingExpected);
		assertEquals(metadata.Label, node.Label);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void coverage()
	  {
		FixedIborSwapCurveNode test = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		coverImmutableBean(test);
		FixedIborSwapCurveNode test2 = FixedIborSwapCurveNode.of(FixedIborSwapTemplate.of(TENOR_10Y, FixedIborSwapConventions.USD_FIXED_1Y_LIBOR_3M), QuoteId.of(StandardId.of("OG-Ticker", "Deposit2")));
		coverBeanEquals(test, test2);
	  }

	  public virtual void test_serialization()
	  {
		FixedIborSwapCurveNode test = FixedIborSwapCurveNode.of(TEMPLATE, QUOTE_ID, SPREAD);
		assertSerialization(test);
	  }

	}

}