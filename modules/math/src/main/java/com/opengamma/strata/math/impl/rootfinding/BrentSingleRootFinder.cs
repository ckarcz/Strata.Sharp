﻿using System;

/*
 * Copyright (C) 2009 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.math.impl.rootfinding
{

	/// <summary>
	/// Root finder.
	/// </summary>
	public class BrentSingleRootFinder : RealSingleRootFinder
	{

	  private const int MAX_ITER = 100;
	  private const double ZERO = 1e-16;
	  private readonly double _accuracy;

	  /// <summary>
	  /// Creates an instance.
	  /// Sets the accuracy to 10<sup>-15</sup>
	  /// </summary>
	  public BrentSingleRootFinder() : this(1e-15)
	  {
	  }

	  /// <summary>
	  /// Creates an instance. </summary>
	  /// <param name="accuracy"> The accuracy of the root </param>
	  public BrentSingleRootFinder(double accuracy)
	  {
		_accuracy = accuracy;
	  }

	  //-------------------------------------------------------------------------
	  public override double? getRoot(System.Func<double, double> function, double? xLower, double? xUpper)
	  {
		checkInputs(function, xLower, xUpper);
		if (xLower.Equals(xUpper))
		{
		  return xLower;
		}
		double x1 = xLower.Value;
		double x2 = xUpper.Value;
		double x3 = xUpper.Value;
		double delta = 0;
		double oldDelta = 0;
		double f1 = function(x1);
		double f2 = function(x2);
		double f3 = f2;
		double r1, r2, r3, r4, eps, xMid, min1, min2;
		for (int i = 0; i < MAX_ITER; i++)
		{
		  if (f2 > 0 && f3 > 0 || f2 < 0 && f3 < 0)
		  {
			x3 = x1;
			f3 = f1;
			delta = x2 - x1;
			oldDelta = delta;
		  }
		  if (Math.Abs(f3) < Math.Abs(f2))
		  {
			x1 = x2;
			x2 = x3;
			x3 = x1;
			f1 = f2;
			f2 = f3;
			f3 = f1;
		  }
		  eps = 2 * ZERO * Math.Abs(x2) + 0.5 * _accuracy;
		  xMid = (x3 - x2) / 2;
		  if (Math.Abs(xMid) <= eps)
		  {
			return x2;
		  }
		  if (Math.Abs(oldDelta) >= eps && Math.Abs(f1) > Math.Abs(f2))
		  {
			r4 = f2 / f1;
			if (Math.Abs(x1 - x3) < ZERO)
			{
			  r1 = 2 * xMid * r4;
			  r2 = 1 - r4;
			}
			else
			{
			  r2 = f1 / f3;
			  r3 = f2 / f3;
			  r1 = r4 * (2 * xMid * r2 * (r2 - r3) - (x2 - x1) * (r3 - 1));
			  r2 = (r2 - 1) * (r3 - 1) * (r4 - 1);
			}
			if (r1 > 0)
			{
			  r2 *= -1;
			}
			r1 = Math.Abs(r1);
			min1 = 3 * xMid * r2 - Math.Abs(eps * r2);
			min2 = Math.Abs(oldDelta * r2);
			if (2 * r1 < (min1 < min2 ? min1 : min2))
			{
			  oldDelta = delta;
			  delta = r1 / r2;
			}
			else
			{
			  delta = xMid;
			  oldDelta = delta;
			}
		  }
		  else
		  {
			delta = xMid;
			oldDelta = delta;
		  }
		  x1 = x2;
		  if (Math.Abs(delta) > eps)
		  {
			x2 += delta;
		  }
		  else
		  {
			x2 += Math.copySign(eps, xMid);
		  }
		  f1 = function(x1);
		  f2 = function(x2);
		  f3 = function(x3);
		}
		throw new MathException("Could not converge to root in " + MAX_ITER + " attempts");
	  }

	}

}