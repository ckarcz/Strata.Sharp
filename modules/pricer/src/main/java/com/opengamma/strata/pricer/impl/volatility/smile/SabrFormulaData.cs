﻿using System;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.impl.volatility.smile
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using ImmutableValidator = org.joda.beans.gen.ImmutableValidator;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using DoubleArray = com.opengamma.strata.collect.array.DoubleArray;

	/// <summary>
	/// The data bundle for SABR formula.
	/// <para>
	/// The bundle contains the SABR model parameters, alpha, beta, rho and nu, as an array.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light") public final class SabrFormulaData implements SmileModelData, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class SabrFormulaData : SmileModelData, ImmutableBean
	{

	  /// <summary>
	  /// The number of model parameters.
	  /// </summary>
	  private const int NUM_PARAMETERS = 4;

	  /// <summary>
	  /// The model parameters.
	  /// <para>
	  /// This must be an array of length 4.
	  /// The parameters in the array are in the order of alpha, beta, rho and nu.
	  /// The constraints for the parameters are defined in <seealso cref="#isAllowed(int, double)"/>.
	  /// </para>
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.collect.array.DoubleArray parameters;
	  private readonly DoubleArray parameters;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance of the SABR formula data.
	  /// </summary>
	  /// <param name="alpha">  the alpha parameter </param>
	  /// <param name="beta">  the beta parameter </param>
	  /// <param name="rho">  the rho parameter </param>
	  /// <param name="nu">  the nu parameter </param>
	  /// <returns> the instance </returns>
	  public static SabrFormulaData of(double alpha, double beta, double rho, double nu)
	  {
		return new SabrFormulaData(DoubleArray.of(alpha, beta, rho, nu));
	  }

	  /// <summary>
	  /// Obtains an instance of the SABR formula data.
	  /// <para>
	  /// The parameters in the input array should be in the order of alpha, beta, rho and nu.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="parameters">  the parameters </param>
	  /// <returns> the instance </returns>
	  public static SabrFormulaData of(double[] parameters)
	  {
		ArgChecker.notNull(parameters, "parameters");
		ArgChecker.isTrue(parameters.Length == NUM_PARAMETERS, "the number of parameters should be 4");
		return new SabrFormulaData(DoubleArray.copyOf(parameters));
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutableValidator private void validate()
	  private void validate()
	  {
		for (int i = 0; i < NUM_PARAMETERS; ++i)
		{
		  ArgChecker.isTrue(isAllowed(i, parameters.get(i)), "the {}-th parameter is not allowed", i);
		}
	  }

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Gets the alpha parameter.
	  /// </summary>
	  /// <returns> the alpha parameter </returns>
	  public double Alpha
	  {
		  get
		  {
			return parameters.get(0);
		  }
	  }

	  /// <summary>
	  /// Gets the beta parameter.
	  /// </summary>
	  /// <returns> the beta parameter </returns>
	  public double Beta
	  {
		  get
		  {
			return parameters.get(1);
		  }
	  }

	  /// <summary>
	  /// Gets the rho parameter.
	  /// </summary>
	  /// <returns> the rho parameter </returns>
	  public double Rho
	  {
		  get
		  {
			return parameters.get(2);
		  }
	  }

	  /// <summary>
	  /// Obtains the nu parameters.
	  /// </summary>
	  /// <returns> the nu parameter </returns>
	  public double Nu
	  {
		  get
		  {
			return parameters.get(3);
		  }
	  }

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Returns a copy of this instance with alpha replaced.
	  /// </summary>
	  /// <param name="alpha">  the new alpha </param>
	  /// <returns> the new data instance </returns>
	  public SabrFormulaData withAlpha(double alpha)
	  {
		return of(alpha, Beta, Rho, Nu);
	  }

	  /// <summary>
	  /// Returns a copy of this instance with beta replaced.
	  /// </summary>
	  /// <param name="beta">  the new beta </param>
	  /// <returns> the new data instance </returns>
	  public SabrFormulaData withBeta(double beta)
	  {
		return of(Alpha, beta, Rho, Nu);
	  }

	  /// <summary>
	  /// Returns a copy of this instance with rho replaced.
	  /// </summary>
	  /// <param name="rho">  the new rho </param>
	  /// <returns> the new data instance </returns>
	  public SabrFormulaData withRho(double rho)
	  {
		return of(Alpha, Beta, rho, Nu);
	  }

	  /// <summary>
	  /// Returns a copy of this instance with nu replaced.
	  /// </summary>
	  /// <param name="nu">  the new nu </param>
	  /// <returns> the new data instance </returns>
	  public SabrFormulaData withNu(double nu)
	  {
		return of(Alpha, Beta, Rho, nu);
	  }

	  //-------------------------------------------------------------------------
	  public int NumberOfParameters
	  {
		  get
		  {
			return NUM_PARAMETERS;
		  }
	  }

	  public double getParameter(int index)
	  {
		ArgChecker.inRangeExclusive(index, -1, NUM_PARAMETERS, "index");
		return parameters.get(index);
	  }

	  public bool isAllowed(int index, double value)
	  {
		switch (index)
		{
		  case 0:
		  case 1:
		  case 3:
			return value >= 0;
		  case 2:
			return value >= -1 && value <= 1;
		  default:
			throw new System.ArgumentException("index " + index + " outside range");
		}
	  }

	  public SabrFormulaData with(int index, double value)
	  {
		ArgChecker.inRange(index, 0, NUM_PARAMETERS, "index");
		double[] paramsCp = parameters.toArray();
		paramsCp[index] = value;
		return of(paramsCp);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code SabrFormulaData}.
	  /// </summary>
	  private static readonly TypedMetaBean<SabrFormulaData> META_BEAN = LightMetaBean.of(typeof(SabrFormulaData), MethodHandles.lookup(), new string[] {"parameters"}, new object[0]);

	  /// <summary>
	  /// The meta-bean for {@code SabrFormulaData}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static TypedMetaBean<SabrFormulaData> meta()
	  {
		return META_BEAN;
	  }

	  static SabrFormulaData()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private SabrFormulaData(DoubleArray parameters)
	  {
		JodaBeanUtils.notNull(parameters, "parameters");
		this.parameters = parameters;
		validate();
	  }

	  public override TypedMetaBean<SabrFormulaData> metaBean()
	  {
		return META_BEAN;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the model parameters.
	  /// <para>
	  /// This must be an array of length 4.
	  /// The parameters in the array are in the order of alpha, beta, rho and nu.
	  /// The constraints for the parameters are defined in <seealso cref="#isAllowed(int, double)"/>.
	  /// </para>
	  /// </summary>
	  /// <returns> the value of the property, not null </returns>
	  public DoubleArray Parameters
	  {
		  get
		  {
			return parameters;
		  }
	  }

	  //-----------------------------------------------------------------------
	  public override bool Equals(object obj)
	  {
		if (obj == this)
		{
		  return true;
		}
		if (obj != null && obj.GetType() == this.GetType())
		{
		  SabrFormulaData other = (SabrFormulaData) obj;
		  return JodaBeanUtils.equal(parameters, other.parameters);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(parameters);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(64);
		buf.Append("SabrFormulaData{");
		buf.Append("parameters").Append('=').Append(JodaBeanUtils.ToString(parameters));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}