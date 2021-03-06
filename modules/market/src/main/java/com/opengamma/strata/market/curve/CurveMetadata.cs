﻿using System.Collections.Generic;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.market.curve
{

	using Tenor = com.opengamma.strata.basics.date.Tenor;
	using Messages = com.opengamma.strata.collect.Messages;
	using ParameterMetadata = com.opengamma.strata.market.param.ParameterMetadata;

	/// <summary>
	/// Metadata about a curve and curve parameters.
	/// <para>
	/// Implementations of this interface are used to store metadata about a curve.
	/// For example, a curve may be defined based on financial instruments.
	/// The parameters might represent 1 day, 1 week, 1 month, 3 month and 6 months.
	/// The metadata could be used to describe each parameter in terms of a <seealso cref="Tenor"/>.
	/// </para>
	/// <para>
	/// This metadata can be used by applications to interpret the parameters of the curve.
	/// For example, the scenario framework uses the data when applying perturbations.
	/// </para>
	/// <para>
	/// See <seealso cref="Curves"/> for helper methods that create common curve types.
	/// </para>
	/// </summary>
	public interface CurveMetadata
	{

	  /// <summary>
	  /// Gets the curve name.
	  /// </summary>
	  /// <returns> the curve name </returns>
	  CurveName CurveName {get;}

	  /// <summary>
	  /// Gets the x-value type, providing meaning to the x-values of the curve.
	  /// <para>
	  /// This type provides meaning to the x-values. For example, the x-value might
	  /// represent a year fraction, as represented using <seealso cref="ValueType#YEAR_FRACTION"/>.
	  /// </para>
	  /// <para>
	  /// Note that if the x-value of the curve represents time as a year fraction, the day
	  /// count should be specified in the info map to define how the year fraction is calculated.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <returns> the x-value type </returns>
	  ValueType XValueType {get;}

	  /// <summary>
	  /// Gets the y-value type, providing meaning to the y-values of the curve.
	  /// <para>
	  /// This type provides meaning to the y-values. For example, the y-value might
	  /// represent a zero rate, as represented using <seealso cref="ValueType#ZERO_RATE"/>.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <returns> the y-value type </returns>
	  ValueType YValueType {get;}

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Gets curve information of a specific type.
	  /// <para>
	  /// If the information is not found, an exception is thrown.
	  /// </para>
	  /// <para>
	  /// The most common information is the <seealso cref="CurveInfoType#DAY_COUNT day count"/>
	  /// and <seealso cref="CurveInfoType#JACOBIAN curve calibration information"/>.
	  /// 
	  /// </para>
	  /// </summary>
	  /// @param <T>  the type of the info </param>
	  /// <param name="type">  the type to find </param>
	  /// <returns> the curve information </returns>
	  /// <exception cref="IllegalArgumentException"> if the information is not found </exception>
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java default interface methods:
//	  public default <T> T getInfo(CurveInfoType<T> type)
	//  {
	//	return findInfo(type).orElseThrow(() -> new IllegalArgumentException(Messages.format("Curve info not found for type '{}'", type)));
	//  }

	  /// <summary>
	  /// Finds curve information of a specific type.
	  /// <para>
	  /// If the info is not found, optional empty is returned.
	  /// </para>
	  /// <para>
	  /// The most common information is the <seealso cref="CurveInfoType#DAY_COUNT day count"/>
	  /// and <seealso cref="CurveInfoType#JACOBIAN curve calibration information"/>.
	  /// 
	  /// </para>
	  /// </summary>
	  /// @param <T>  the type of the info </param>
	  /// <param name="type">  the type to find </param>
	  /// <returns> the curve information </returns>
	  Optional<T> findInfo<T>(CurveInfoType<T> type);

	  /// <summary>
	  /// Gets the metadata of the parameter at the specified index.
	  /// <para>
	  /// If there is no specific parameter metadata, an empty instance will be returned.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="parameterIndex">  the zero-based index of the parameter to get </param>
	  /// <returns> the metadata of the parameter </returns>
	  /// <exception cref="IndexOutOfBoundsException"> if the index is invalid </exception>
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java default interface methods:
//	  public default com.opengamma.strata.market.param.ParameterMetadata getParameterMetadata(int parameterIndex)
	//  {
	//	return getParameterMetadata().map(pm -> pm.get(parameterIndex)).orElse(ParameterMetadata.empty());
	//  }

	  /// <summary>
	  /// Gets metadata about each parameter underlying the curve, optional.
	  /// <para>
	  /// If present, the parameter metadata will match the number of parameters on the curve.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <returns> the parameter metadata </returns>
	  Optional<IList<ParameterMetadata>> ParameterMetadata {get;}

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Returns an instance where the specified additional information has been added.
	  /// <para>
	  /// The additional information is stored in the result using {@code Map.put} semantics,
	  /// removing the key if the instance is null.
	  /// 
	  /// </para>
	  /// </summary>
	  /// @param <T>  the type of the info </param>
	  /// <param name="type">  the type to store under </param>
	  /// <param name="value">  the value to store, may be null </param>
	  /// <returns> the new curve metadata </returns>
	  DefaultCurveMetadata withInfo<T>(CurveInfoType<T> type, T value);

	  /// <summary>
	  /// Returns an instance where the parameter metadata has been changed.
	  /// <para>
	  /// The result will contain the specified parameter metadata.
	  /// A null value is accepted and causes the result to have no parameter metadata.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="parameterMetadata">  the new parameter metadata, may be null </param>
	  /// <returns> the new curve metadata </returns>
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: public abstract CurveMetadata withParameterMetadata(java.util.List<? extends com.opengamma.strata.market.param.ParameterMetadata> parameterMetadata);
	  CurveMetadata withParameterMetadata<T1>(IList<T1> parameterMetadata);

	}

}