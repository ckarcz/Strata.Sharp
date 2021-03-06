﻿/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.collect.function
{
	/// <summary>
	/// A function of two arguments - {@code int} and {@code double}.
	/// <para>
	/// This takes two arguments and returns a {@code double} result.
	/// </para>
	/// </summary>
	public delegate double IntDoubleToDoubleFunction(int intValue, double doubleValue);

}