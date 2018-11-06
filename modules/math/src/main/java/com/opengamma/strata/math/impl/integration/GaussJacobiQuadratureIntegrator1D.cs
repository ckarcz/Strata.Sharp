﻿/*
 * Copyright (C) 2009 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.math.impl.integration
{

	using ArgChecker = com.opengamma.strata.collect.ArgChecker;

	/// <summary>
	/// Gauss-Jacobi quadrature approximates the value of integrals of the form
	/// $$
	/// \begin{align*}
	/// \int_{-1}^{1} (1 - x)^\alpha (1 + x)^\beta f(x) dx
	/// \end{align*}
	/// $$
	/// The weights and abscissas are generated by <seealso cref="GaussJacobiWeightAndAbscissaFunction"/>.
	/// <para>
	/// In this integrator, $\alpha = 0$ and $\beta = 0$, which means that no
	/// adjustment to the function must be performed. However, the function is
	/// scaled in such a way as to allow any values for the
	/// limits of the integrals.
	/// </para>
	/// </summary>
	public class GaussJacobiQuadratureIntegrator1D : GaussianQuadratureIntegrator1D
	{

	  private static readonly GaussJacobiWeightAndAbscissaFunction GENERATOR = new GaussJacobiWeightAndAbscissaFunction(0, 0);
	  private static readonly double?[] LIMITS = new double?[] {-1.0, 1.0};

	  //TODO allow alpha and beta to be set
	  /// <param name="n"> The number of sample points to be used in the integration, not negative or zero </param>
	  public GaussJacobiQuadratureIntegrator1D(int n) : base(n, GENERATOR)
	  {
	  }

	  public override double?[] Limits
	  {
		  get
		  {
			return LIMITS;
		  }
	  }

	  /// <summary>
	  /// {@inheritDoc}
	  /// To evaluate an integral over $[a, b]$, a change of interval must be
	  /// performed:
	  /// $$
	  /// \begin{align*}
	  /// \int_a^b f(x)dx 
	  /// &= \frac{b - a}{2}\int_{-1}^1 f(\frac{b - a}{2} x + \frac{a + b}{2})dx\\
	  /// &\approx \frac{b - a}{2}\sum_{i=1}^n w_i f(\frac{b - a}{2} x + \frac{a + b}{2})
	  /// \end{align*}
	  /// $$
	  /// </summary>
	  public override System.Func<double, double> getIntegralFunction(System.Func<double, double> function, double? lower, double? upper)
	  {
		ArgChecker.notNull(function, "function");
		ArgChecker.notNull(lower, "lower");
		ArgChecker.notNull(upper, "upper");
		double m = (upper - lower) / 2;
		double c = (upper + lower) / 2;
		return (double? x) =>
		{
	return m * function(m * x + c);
		};
	  }

	}

}