﻿/*
 * Copyright (C) 2014 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer
{
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertJodaConvert;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertSerialization;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.assertThrows;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static com.opengamma.strata.collect.TestHelper.coverEnum;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static org.testng.Assert.assertEquals;

	using DataProvider = org.testng.annotations.DataProvider;
	using Test = org.testng.annotations.Test;

	/// <summary>
	/// Test <seealso cref="CompoundedRateType"/>.
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public class CompoundedRateTypeTest
	public class CompoundedRateTypeTest
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @DataProvider(name = "name") public static Object[][] data_name()
		public static object[][] data_name()
		{
		return new object[][]
		{
			new object[] {CompoundedRateType.PERIODIC, "Periodic"},
			new object[] {CompoundedRateType.CONTINUOUS, "Continuous"}
		};
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(dataProvider = "name") public void test_toString(CompoundedRateType convention, String name)
	  public virtual void test_toString(CompoundedRateType convention, string name)
	  {
		assertEquals(convention.ToString(), name);
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test(dataProvider = "name") public void test_of_lookup(CompoundedRateType convention, String name)
	  public virtual void test_of_lookup(CompoundedRateType convention, string name)
	  {
		assertEquals(CompoundedRateType.of(name), convention);
	  }

	  public virtual void test_of_lookup_notFound()
	  {
		assertThrows(() => CompoundedRateType.of("Rubbish"), typeof(System.ArgumentException));
	  }

	  public virtual void test_of_lookup_null()
	  {
		assertThrows(() => CompoundedRateType.of(null), typeof(System.ArgumentException));
	  }

	  //-------------------------------------------------------------------------
	  public virtual void coverage()
	  {
		coverEnum(typeof(CompoundedRateType));
	  }

	  public virtual void test_serialization()
	  {
		assertSerialization(CompoundedRateType.CONTINUOUS);
	  }

	  public virtual void test_jodaConvert()
	  {
		assertJodaConvert(typeof(CompoundedRateType), CompoundedRateType.CONTINUOUS);
	  }

	}

}