﻿/*
 * Copyright (C) 2016 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.product.swap.type
{

	using FromString = org.joda.convert.FromString;
	using ToString = org.joda.convert.ToString;

	using ReferenceData = com.opengamma.strata.basics.ReferenceData;
	using ReferenceDataNotFoundException = com.opengamma.strata.basics.ReferenceDataNotFoundException;
	using Tenor = com.opengamma.strata.basics.date.Tenor;
	using ArgChecker = com.opengamma.strata.collect.ArgChecker;
	using ExtendedEnum = com.opengamma.strata.collect.named.ExtendedEnum;
	using Named = com.opengamma.strata.collect.named.Named;
	using BuySell = com.opengamma.strata.product.common.BuySell;

	/// <summary>
	/// A market convention for Overnight-Ibor swap trades.
	/// <para>
	/// This defines the market convention for a Overnight-Ibor single currency swap.
	/// In USD, this is often known as an <i>Fed Fund Swap</i>.
	/// The convention is formed by combining two swap leg conventions in the same currency.
	/// </para>
	/// <para>
	/// To manually create a convention, see <seealso cref="ImmutableOvernightIborSwapConvention"/>.
	/// To register a specific convention, see {@code OvernightIborSwapConvention.ini}.
	/// </para>
	/// </summary>
	public interface OvernightIborSwapConvention : SingleCurrencySwapConvention, Named
	{

	  /// <summary>
	  /// Obtains an instance from the specified unique name.
	  /// </summary>
	  /// <param name="uniqueName">  the unique name </param>
	  /// <returns> the convention </returns>
	  /// <exception cref="IllegalArgumentException"> if the name is not known </exception>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @FromString public static OvernightIborSwapConvention of(String uniqueName)
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java static interface methods:
//	  public static OvernightIborSwapConvention of(String uniqueName)
	//  {
	//	ArgChecker.notNull(uniqueName, "uniqueName");
	//	return extendedEnum().lookup(uniqueName);
	//  }

	  /// <summary>
	  /// Gets the extended enum helper.
	  /// <para>
	  /// This helper allows instances of the convention to be looked up.
	  /// It also provides the complete set of available instances.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <returns> the extended enum helper </returns>
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java static interface methods:
//	  public static com.opengamma.strata.collect.named.ExtendedEnum<OvernightIborSwapConvention> extendedEnum()
	//  {
	//	return OvernightIborSwapConventions.ENUM_LOOKUP;
	//  }

	  //-----------------------------------------------------------------------
	  /// <summary>
	  /// Gets the market convention of the overnight leg.
	  /// </summary>
	  /// <returns> the overnight leg convention </returns>
	  OvernightRateSwapLegConvention OvernightLeg {get;}

	  /// <summary>
	  /// Gets the market convention of the Ibor leg.
	  /// </summary>
	  /// <returns> the Ibor leg convention </returns>
	  IborRateSwapLegConvention IborLeg {get;}

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Creates a spot-starting trade based on this convention.
	  /// <para>
	  /// This returns a trade based on the specified tenor. For example, a tenor
	  /// of 5 years creates a swap starting on the spot date and maturing 5 years later.
	  /// </para>
	  /// <para>
	  /// The notional is unsigned, with buy/sell determining the direction of the trade.
	  /// If buying the swap, the Ibor rate is received from the counterparty, with the overnight and spread being paid.
	  /// If selling the swap, the Ibor rate is paid to the counterparty, with the overnight and spread being received.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="tradeDate">  the date of the trade </param>
	  /// <param name="tenor">  the tenor of the swap </param>
	  /// <param name="buySell">  the buy/sell flag </param>
	  /// <param name="notional">  the notional amount </param>
	  /// <param name="spread">  the spread of added the overnight rates, typically derived from the market </param>
	  /// <param name="refData">  the reference data, used to resolve the trade dates </param>
	  /// <returns> the trade </returns>
	  /// <exception cref="ReferenceDataNotFoundException"> if an identifier cannot be resolved in the reference data </exception>
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java default interface methods:
//	  public default com.opengamma.strata.product.swap.SwapTrade createTrade(java.time.LocalDate tradeDate, com.opengamma.strata.basics.date.Tenor tenor, com.opengamma.strata.product.common.BuySell buySell, double notional, double spread, com.opengamma.strata.basics.ReferenceData refData)
	//  {
	//
	//	// override for Javadoc
	//	return SingleCurrencySwapConvention.this.createTrade(tradeDate, tenor, buySell, notional, spread, refData);
	//  }

	  /// <summary>
	  /// Creates a forward-starting trade based on this convention.
	  /// <para>
	  /// This returns a trade based on the specified period and tenor. For example, a period of
	  /// 3 months and a tenor of 5 years creates a swap starting three months after the spot date
	  /// and maturing 5 years later.
	  /// </para>
	  /// <para>
	  /// The notional is unsigned, with buy/sell determining the direction of the trade.
	  /// If buying the swap, the Ibor rate is received from the counterparty, with the overnight and spread being paid.
	  /// If selling the swap, the Ibor rate is paid to the counterparty, with the overnight and spread being received.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="tradeDate">  the date of the trade </param>
	  /// <param name="periodToStart">  the period between the spot date and the start date </param>
	  /// <param name="tenor">  the tenor of the swap </param>
	  /// <param name="buySell">  the buy/sell flag </param>
	  /// <param name="notional">  the notional amount </param>
	  /// <param name="spread">  the spread of added the overnight rates, typically derived from the market </param>
	  /// <param name="refData">  the reference data, used to resolve the trade dates </param>
	  /// <returns> the trade </returns>
	  /// <exception cref="ReferenceDataNotFoundException"> if an identifier cannot be resolved in the reference data </exception>
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java default interface methods:
//	  public default com.opengamma.strata.product.swap.SwapTrade createTrade(java.time.LocalDate tradeDate, java.time.Period periodToStart, com.opengamma.strata.basics.date.Tenor tenor, com.opengamma.strata.product.common.BuySell buySell, double notional, double spread, com.opengamma.strata.basics.ReferenceData refData)
	//  {
	//
	//	// override for Javadoc
	//	return SingleCurrencySwapConvention.this.createTrade(tradeDate, periodToStart, tenor, buySell, notional, spread, refData);
	//  }

	  /// <summary>
	  /// Creates a trade based on this convention.
	  /// <para>
	  /// This returns a trade based on the specified dates.
	  /// </para>
	  /// <para>
	  /// The notional is unsigned, with buy/sell determining the direction of the trade.
	  /// If buying the swap, the Ibor rate is received from the counterparty, with the overnight and spread being paid.
	  /// If selling the swap, the Ibor rate is paid to the counterparty, with the overnight and spread being received.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="tradeDate">  the date of the trade </param>
	  /// <param name="startDate">  the start date </param>
	  /// <param name="endDate">  the end date </param>
	  /// <param name="buySell">  the buy/sell flag </param>
	  /// <param name="notional">  the notional amount </param>
	  /// <param name="spread">  the spread of added the overnight rates, typically derived from the market </param>
	  /// <returns> the trade </returns>
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java default interface methods:
//	  public default com.opengamma.strata.product.swap.SwapTrade toTrade(java.time.LocalDate tradeDate, java.time.LocalDate startDate, java.time.LocalDate endDate, com.opengamma.strata.product.common.BuySell buySell, double notional, double spread)
	//  {
	//
	//	// override for Javadoc
	//	return SingleCurrencySwapConvention.this.toTrade(tradeDate, startDate, endDate, buySell, notional, spread);
	//  }

	  /// <summary>
	  /// Creates a trade based on this convention.
	  /// <para>
	  /// This returns a trade based on the specified dates.
	  /// </para>
	  /// <para>
	  /// The notional is unsigned, with buy/sell determining the direction of the trade.
	  /// If buying the swap, the Ibor rate is received from the counterparty, with the overnight and spread being paid.
	  /// If selling the swap, the Ibor rate is paid to the counterparty, with the overnight and spread being received.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <param name="tradeInfo">  additional information about the trade </param>
	  /// <param name="startDate">  the start date </param>
	  /// <param name="endDate">  the end date </param>
	  /// <param name="buySell">  the buy/sell flag </param>
	  /// <param name="notional">  the notional amount </param>
	  /// <param name="spread">  the spread of added the overnight rates, typically derived from the market </param>
	  /// <returns> the trade </returns>
	  SwapTrade toTrade(TradeInfo tradeInfo, LocalDate startDate, LocalDate endDate, BuySell buySell, double notional, double spread);

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Gets the name that uniquely identifies this convention.
	  /// <para>
	  /// This name is used in serialization and can be parsed using <seealso cref="#of(String)"/>.
	  /// 
	  /// </para>
	  /// </summary>
	  /// <returns> the unique name </returns>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @ToString @Override public abstract String getName();
	  string Name {get;}

	}

}