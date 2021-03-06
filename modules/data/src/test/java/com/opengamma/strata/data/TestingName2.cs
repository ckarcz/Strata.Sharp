﻿using System;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.data
{
	/// <summary>
	/// MarketDataName implementation used in tests.
	/// </summary>
	public class TestingName2 : MarketDataName<string>
	{

	  private readonly string name;

	  public TestingName2(string name)
	  {
		this.name = name;
	  }

	  public override string Name
	  {
		  get
		  {
			return name;
		  }
	  }

	  public override Type<string> MarketDataType
	  {
		  get
		  {
			return typeof(string);
		  }
	  }

	}

}