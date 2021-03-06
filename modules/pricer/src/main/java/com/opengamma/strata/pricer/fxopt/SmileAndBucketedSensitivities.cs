﻿using System;
using System.Text;

/*
 * Copyright (C) 2012 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.pricer.fxopt
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	using DoubleMatrix = com.opengamma.strata.collect.array.DoubleMatrix;

	/// <summary>
	/// Combines information about a volatility smile expressed in delta form and its sensitivities.
	/// <para>
	/// This contains a volatility smile expressed in delta form and the bucketed sensitivities
	/// of the smile to the data points that were used to construct it.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light") public final class SmileAndBucketedSensitivities implements org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	public sealed class SmileAndBucketedSensitivities : ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition private final SmileDeltaParameters smile;
		private readonly SmileDeltaParameters smile;
	  /// <summary>
	  /// The sensitivities.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final com.opengamma.strata.collect.array.DoubleMatrix sensitivities;
	  private readonly DoubleMatrix sensitivities;

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Obtains an instance.
	  /// </summary>
	  /// <param name="smile">  the smile </param>
	  /// <param name="sensitivities">  the bucketed sensitivities </param>
	  /// <returns> the volatility and sensitivities </returns>
	  public static SmileAndBucketedSensitivities of(SmileDeltaParameters smile, DoubleMatrix sensitivities)
	  {
		return new SmileAndBucketedSensitivities(smile, sensitivities);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code SmileAndBucketedSensitivities}.
	  /// </summary>
	  private static readonly TypedMetaBean<SmileAndBucketedSensitivities> META_BEAN = LightMetaBean.of(typeof(SmileAndBucketedSensitivities), MethodHandles.lookup(), new string[] {"smile", "sensitivities"}, new object[0]);

	  /// <summary>
	  /// The meta-bean for {@code SmileAndBucketedSensitivities}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static TypedMetaBean<SmileAndBucketedSensitivities> meta()
	  {
		return META_BEAN;
	  }

	  static SmileAndBucketedSensitivities()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private SmileAndBucketedSensitivities(SmileDeltaParameters smile, DoubleMatrix sensitivities)
	  {
		JodaBeanUtils.notNull(sensitivities, "sensitivities");
		this.smile = smile;
		this.sensitivities = sensitivities;
	  }

	  public override TypedMetaBean<SmileAndBucketedSensitivities> metaBean()
	  {
		return META_BEAN;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the smile. </summary>
	  /// <returns> the value of the property </returns>
	  public SmileDeltaParameters Smile
	  {
		  get
		  {
			return smile;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the sensitivities. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public DoubleMatrix Sensitivities
	  {
		  get
		  {
			return sensitivities;
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
		  SmileAndBucketedSensitivities other = (SmileAndBucketedSensitivities) obj;
		  return JodaBeanUtils.equal(smile, other.smile) && JodaBeanUtils.equal(sensitivities, other.sensitivities);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(smile);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(sensitivities);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("SmileAndBucketedSensitivities{");
		buf.Append("smile").Append('=').Append(smile).Append(',').Append(' ');
		buf.Append("sensitivities").Append('=').Append(JodaBeanUtils.ToString(sensitivities));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}