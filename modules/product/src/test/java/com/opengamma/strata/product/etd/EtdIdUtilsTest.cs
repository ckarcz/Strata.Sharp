﻿/*
 * Copyright (C) 2017 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.etd
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverPrivateConstructor;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.product.etd.EtdVariant.MONTHLY;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;

	using Test = org.testng.annotations.Test;

	using StandardId = com.opengamma.strata.basics.StandardId;
	using ExchangeIds = com.opengamma.strata.product.common.ExchangeIds;
	using PutCall = com.opengamma.strata.product.common.PutCall;

	/// <summary>
	/// Test <seealso cref="EtdIdUtils"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class EtdIdUtilsTest
	public class EtdIdUtilsTest
	{

	  private static readonly EtdContractCode OGBS = EtdContractCode.of("OGBS");
	  private static readonly EtdContractCode FGBS = EtdContractCode.of("FGBS");

	  public virtual void test_contractSpecId_future()
	  {
		EtdContractSpecId test = EtdIdUtils.contractSpecId(EtdType.FUTURE, ExchangeIds.ECAG, FGBS);
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "F-ECAG-FGBS"));
	  }

	  public virtual void test_contractSpecId_option()
	  {
		EtdContractSpecId test = EtdIdUtils.contractSpecId(EtdType.OPTION, ExchangeIds.ECAG, OGBS);
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-OGBS"));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_futureId_monthly()
	  {
		SecurityId test = EtdIdUtils.futureId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), MONTHLY);
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "F-ECAG-FGBS-201706"));
	  }

	  public virtual void test_futureId_weekly()
	  {
		SecurityId test = EtdIdUtils.futureId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofWeekly(2));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "F-ECAG-FGBS-201706W2"));
	  }

	  public virtual void test_futureId_daily()
	  {
		SecurityId test = EtdIdUtils.futureId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofDaily(2));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "F-ECAG-FGBS-20170602"));
	  }

	  public virtual void test_futureId_flex()
	  {
		SecurityId test = EtdIdUtils.futureId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofFlexFuture(26, EtdSettlementType.DERIVATIVE));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "F-ECAG-FGBS-20170626D"));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_optionId_monthly()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), MONTHLY, 0, PutCall.PUT, 12.34);
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-201706-P12.34"));
	  }

	  public virtual void test_optionId_weekly()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofWeekly(3), 0, PutCall.CALL, -1.45);
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-201706W3-CM1.45"));
	  }

	  public virtual void test_optionId_daily9_version()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofDaily(9), 3, PutCall.PUT, 12.34);
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-20170609-V3-P12.34"));
	  }

	  public virtual void test_optionId_daily21_version()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofDaily(21), 11, PutCall.PUT, 12.34);
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-20170621-V11-P12.34"));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_optionIdUnderlying_monthly()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), MONTHLY, 0, PutCall.PUT, 12.34, YearMonth.of(2017, 9));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-201706-P12.34-U201709"));
	  }

	  public virtual void test_optionIdUnderlying_monthlySameMonth()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), MONTHLY, 0, PutCall.PUT, 12.34, YearMonth.of(2017, 6));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-201706-P12.34"));
	  }

	  public virtual void test_optionIdUnderlying_weekly()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofWeekly(3), 0, PutCall.CALL, -1.45, YearMonth.of(2017, 9));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-201706W3-CM1.45-U201709"));
	  }

	  public virtual void test_optionIdUnderlying_daily9_version()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofDaily(9), 3, PutCall.PUT, 12.34, YearMonth.of(2017, 9));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-20170609-V3-P12.34-U201709"));
	  }

	  public virtual void test_optionIdUnderlying_daily21_version()
	  {
		SecurityId test = EtdIdUtils.optionId(ExchangeIds.ECAG, FGBS, YearMonth.of(2017, 6), EtdVariant.ofDaily(21), 11, PutCall.PUT, 12.34, YearMonth.of(2017, 9));
		assertEquals(test.StandardId, StandardId.of("OG-ETD", "O-ECAG-FGBS-20170621-V11-P12.34-U201709"));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void test_coverage()
	  {
		coverPrivateConstructor(typeof(EtdIdUtils));
	  }

	}

}