﻿using System;
using System.Text;

/*
 * Copyright (C) 2015 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.data.scenario
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;

	/// <summary>
	/// A scenario perturbation that returns its input unchanged and has a scenario count of one.
	/// </summary>
	/// @param <T>  the type of handled by the perturbation </param>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light") final class NoOpScenarioPerturbation<T> implements ScenarioPerturbation<T>, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	internal sealed class NoOpScenarioPerturbation<T> : ScenarioPerturbation<T>, ImmutableBean
	{

	  /// <summary>
	  /// The single shared instance of this class. </summary>
	  internal static readonly ScenarioPerturbation<object> INSTANCE = new NoOpScenarioPerturbation<object>();

	  //-------------------------------------------------------------------------
	  public MarketDataBox<T> applyTo(MarketDataBox<T> marketData, ReferenceData refData)
	  {
		return marketData;
	  }

	  public int ScenarioCount
	  {
		  get
		  {
			// A box with one scenario can be used for any number of scenarios
			return 1;
		  }
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public Class<T> getMarketDataType()
	  public Type<T> MarketDataType
	  {
		  get
		  {
			return (Type<T>) typeof(object);
		  }
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code NoOpScenarioPerturbation}.
	  /// </summary>
	  private static readonly MetaBean META_BEAN = LightMetaBean.of(typeof(NoOpScenarioPerturbation), MethodHandles.lookup());

	  /// <summary>
	  /// The meta-bean for {@code NoOpScenarioPerturbation}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static MetaBean meta()
	  {
		return META_BEAN;
	  }

	  static NoOpScenarioPerturbation()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  private NoOpScenarioPerturbation()
	  {
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public org.joda.beans.TypedMetaBean<NoOpScenarioPerturbation<T>> metaBean()
	  public override TypedMetaBean<NoOpScenarioPerturbation<T>> metaBean()
	  {
		return (TypedMetaBean<NoOpScenarioPerturbation<T>>) META_BEAN;
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
		  return true;
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(32);
		buf.Append("NoOpScenarioPerturbation{");
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}