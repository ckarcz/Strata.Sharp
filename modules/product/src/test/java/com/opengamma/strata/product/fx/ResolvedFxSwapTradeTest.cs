﻿/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.fx
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertSerialization;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverBeanEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverImmutableBean;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.date;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;

	using Test = org.testng.annotations.Test;

	/// <summary>
	/// Test <seealso cref="ResolvedFxSwapTrade"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class ResolvedFxSwapTradeTest
	public class ResolvedFxSwapTradeTest
	{

	  private static readonly ResolvedFxSwap SWAP1 = ResolvedFxSwapTest.sut();
	  private static readonly ResolvedFxSwap SWAP2 = ResolvedFxSwapTest.sut2();
	  private static readonly TradeInfo TRADE_INFO = TradeInfo.of(date(2015, 1, 15));

	  //-------------------------------------------------------------------------
	  public virtual void test_builder()
	  {
		ResolvedFxSwapTrade test = ResolvedFxSwapTrade.builder().info(TRADE_INFO).product(SWAP1).build();
		assertEquals(test.Info, TRADE_INFO);
		assertEquals(test.Product, SWAP1);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void coverage()
	  {
		coverImmutableBean(sut());
		coverBeanEquals(sut(), sut2());
	  }

	  public virtual void test_serialization()
	  {
		assertSerialization(sut());
	  }

	  //-------------------------------------------------------------------------
	  internal static ResolvedFxSwapTrade sut()
	  {
		return ResolvedFxSwapTrade.builder().info(TRADE_INFO).product(SWAP1).build();
	  }

	  internal static ResolvedFxSwapTrade sut2()
	  {
		return ResolvedFxSwapTrade.builder().product(SWAP2).build();
	  }

	}

}