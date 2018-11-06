﻿/*
 * Copyright (C) 2018 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.bond
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertSerialization;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverBeanEquals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverImmutableBean;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;

	using Test = org.testng.annotations.Test;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using StandardId = com.opengamma.strata.basics.StandardId;
	using Currency = com.opengamma.strata.basics.currency.Currency;

	/// <summary>
	/// Test <seealso cref="CapitalIndexedBondPosition"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class CapitalIndexedBondPositionTest
	public class CapitalIndexedBondPositionTest
	{

	  private static readonly ReferenceData REF_DATA = ReferenceData.standard();
	  private static readonly PositionInfo POSITION_INFO = PositionInfo.builder().id(StandardId.of("A", "B")).build();
	  private static readonly PositionInfo POSITION_INFO2 = PositionInfo.builder().id(StandardId.of("A", "C")).build();
	  private const double QUANTITY = 10;
	  private static readonly CapitalIndexedBond PRODUCT = CapitalIndexedBondTest.sut();
	  private static readonly CapitalIndexedBond PRODUCT2 = CapitalIndexedBondTest.sut2();

	  //-------------------------------------------------------------------------
	  public virtual void test_builder_resolved()
	  {
		CapitalIndexedBondPosition test = sut();
		assertEquals(test.Product, PRODUCT);
		assertEquals(test.Info, POSITION_INFO);
		assertEquals(test.LongQuantity, QUANTITY, 0d);
		assertEquals(test.ShortQuantity, 0d, 0d);
		assertEquals(test.Quantity, QUANTITY, 0d);
		assertEquals(test.withInfo(POSITION_INFO).Info, POSITION_INFO);
		assertEquals(test.withQuantity(129).Quantity, 129d, 0d);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_summarize()
	  {
		CapitalIndexedBondPosition trade = sut();
		PortfolioItemSummary expected = PortfolioItemSummary.builder().id(POSITION_INFO.Id.orElse(null)).portfolioItemType(PortfolioItemType.POSITION).productType(ProductType.BOND).currencies(Currency.USD).description("Bond x 10").build();
		assertEquals(trade.summarize(), expected);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_withQuantity()
	  {
		CapitalIndexedBondPosition @base = sut();
		double quantity = 75343d;
		CapitalIndexedBondPosition computed = @base.withQuantity(quantity);
		CapitalIndexedBondPosition expected = CapitalIndexedBondPosition.builder().info(POSITION_INFO).product(PRODUCT).longQuantity(quantity).build();
		assertEquals(computed, expected);
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_resolve()
	  {
		ResolvedCapitalIndexedBondTrade expected = ResolvedCapitalIndexedBondTrade.builder().info(POSITION_INFO).product(PRODUCT.resolve(REF_DATA)).quantity(QUANTITY).build();
		assertEquals(sut().resolve(REF_DATA), expected);
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
	  internal static CapitalIndexedBondPosition sut()
	  {
		return CapitalIndexedBondPosition.builder().info(POSITION_INFO).product(PRODUCT).longQuantity(QUANTITY).build();
	  }

	  internal static CapitalIndexedBondPosition sut2()
	  {
		return CapitalIndexedBondPosition.builder().info(POSITION_INFO2).product(PRODUCT2).longQuantity(100).shortQuantity(50).build();
	  }

	}

}