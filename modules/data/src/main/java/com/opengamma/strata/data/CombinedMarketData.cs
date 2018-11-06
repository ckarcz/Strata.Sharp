﻿using System;
using System.Collections.Generic;
using System.Text;

/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.data
{

	using ImmutableBean = org.joda.beans.ImmutableBean;
	using JodaBeanUtils = org.joda.beans.JodaBeanUtils;
	using MetaBean = org.joda.beans.MetaBean;
	using TypedMetaBean = org.joda.beans.TypedMetaBean;
	using BeanDefinition = org.joda.beans.gen.BeanDefinition;
	using ImmutableValidator = org.joda.beans.gen.ImmutableValidator;
	using PropertyDefinition = org.joda.beans.gen.PropertyDefinition;
	using LightMetaBean = org.joda.beans.impl.light.LightMetaBean;

	using ImmutableSet = com.google.common.collect.ImmutableSet;
	using LocalDateDoubleTimeSeries = com.opengamma.strata.collect.timeseries.LocalDateDoubleTimeSeries;

	/// <summary>
	/// A set of market data which combines the data from two other <seealso cref="MarketData"/> instances.
	/// <para>
	/// When an item of data is requested the underlying sets of market data are checked in order.
	/// If the item is present in the first set of data it is returned. If the item is not found
	/// it is looked up in the second set of data.
	/// </para>
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeanDefinition(style = "light", constructorScope = "package") final class CombinedMarketData implements MarketData, org.joda.beans.ImmutableBean, java.io.Serializable
	[Serializable]
	internal sealed class CombinedMarketData : MarketData, ImmutableBean
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final MarketData underlying1;
		private readonly MarketData underlying1;
	  /// <summary>
	  /// The second set of market data.
	  /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PropertyDefinition(validate = "notNull") private final MarketData underlying2;
	  private readonly MarketData underlying2;

	  //-------------------------------------------------------------------------
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ImmutableValidator private void validate()
	  private void validate()
	  {
		if (!underlying1.ValuationDate.Equals(underlying2.ValuationDate))
		{
		  throw new System.ArgumentException("Unable to combine market data instances with different valuation dates");
		}
	  }

	  //-------------------------------------------------------------------------
	  public LocalDate ValuationDate
	  {
		  get
		  {
			return underlying1.ValuationDate;
		  }
	  }

	  public override bool containsValue<T1>(MarketDataId<T1> id)
	  {
		return underlying1.containsValue(id) || underlying2.containsValue(id);
	  }

	  public override T getValue<T>(MarketDataId<T> id)
	  {
		Optional<T> value1 = underlying1.findValue(id);
		return value1.Present ? value1.get() : underlying2.getValue(id);
	  }

	  public Optional<T> findValue<T>(MarketDataId<T> id)
	  {
		Optional<T> value1 = underlying1.findValue(id);
		return value1.Present ? value1 : underlying2.findValue(id);
	  }

//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: @Override public java.util.Set<MarketDataId<?>> getIds()
	  public ISet<MarketDataId<object>> Ids
	  {
		  get
		  {
	//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
	//ORIGINAL LINE: return com.google.common.collect.ImmutableSet.builder<MarketDataId<?>>().addAll(underlying1.getIds()).addAll(underlying2.getIds()).build();
			return ImmutableSet.builder<MarketDataId<object>>().addAll(underlying1.Ids).addAll(underlying2.Ids).build();
		  }
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override @SuppressWarnings("unchecked") public <T> java.util.Set<MarketDataId<T>> findIds(MarketDataName<T> name)
	  public ISet<MarketDataId<T>> findIds<T>(MarketDataName<T> name)
	  {
		return ImmutableSet.builder<MarketDataId<T>>().addAll(underlying1.findIds(name)).addAll(underlying2.findIds(name)).build();
	  }

	  public ISet<ObservableId> TimeSeriesIds
	  {
		  get
		  {
			return ImmutableSet.builder<ObservableId>().addAll(underlying1.TimeSeriesIds).addAll(underlying2.TimeSeriesIds).build();
		  }
	  }

	  public LocalDateDoubleTimeSeries getTimeSeries(ObservableId id)
	  {
		LocalDateDoubleTimeSeries timeSeries = underlying1.getTimeSeries(id);
		return !timeSeries.Empty ? timeSeries : underlying2.getTimeSeries(id);
	  }

	  //------------------------- AUTOGENERATED START -------------------------
	  /// <summary>
	  /// The meta-bean for {@code CombinedMarketData}.
	  /// </summary>
	  private static readonly TypedMetaBean<CombinedMarketData> META_BEAN = LightMetaBean.of(typeof(CombinedMarketData), MethodHandles.lookup(), new string[] {"underlying1", "underlying2"}, new object[0]);

	  /// <summary>
	  /// The meta-bean for {@code CombinedMarketData}. </summary>
	  /// <returns> the meta-bean, not null </returns>
	  public static TypedMetaBean<CombinedMarketData> meta()
	  {
		return META_BEAN;
	  }

	  static CombinedMarketData()
	  {
		MetaBean.register(META_BEAN);
	  }

	  /// <summary>
	  /// The serialization version id.
	  /// </summary>
	  private const long serialVersionUID = 1L;

	  /// <summary>
	  /// Creates an instance. </summary>
	  /// <param name="underlying1">  the value of the property, not null </param>
	  /// <param name="underlying2">  the value of the property, not null </param>
	  internal CombinedMarketData(MarketData underlying1, MarketData underlying2)
	  {
		JodaBeanUtils.notNull(underlying1, "underlying1");
		JodaBeanUtils.notNull(underlying2, "underlying2");
		this.underlying1 = underlying1;
		this.underlying2 = underlying2;
		validate();
	  }

	  public override TypedMetaBean<CombinedMarketData> metaBean()
	  {
		return META_BEAN;
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the first set of market data. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public MarketData Underlying1
	  {
		  get
		  {
			return underlying1;
		  }
	  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the second set of market data. </summary>
	  /// <returns> the value of the property, not null </returns>
	  public MarketData Underlying2
	  {
		  get
		  {
			return underlying2;
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
		  CombinedMarketData other = (CombinedMarketData) obj;
		  return JodaBeanUtils.equal(underlying1, other.underlying1) && JodaBeanUtils.equal(underlying2, other.underlying2);
		}
		return false;
	  }

	  public override int GetHashCode()
	  {
		int hash = this.GetType().GetHashCode();
		hash = hash * 31 + JodaBeanUtils.GetHashCode(underlying1);
		hash = hash * 31 + JodaBeanUtils.GetHashCode(underlying2);
		return hash;
	  }

	  public override string ToString()
	  {
		StringBuilder buf = new StringBuilder(96);
		buf.Append("CombinedMarketData{");
		buf.Append("underlying1").Append('=').Append(underlying1).Append(',').Append(' ');
		buf.Append("underlying2").Append('=').Append(JodaBeanUtils.ToString(underlying2));
		buf.Append('}');
		return buf.ToString();
	  }

	  //-------------------------- AUTOGENERATED END --------------------------
	}

}