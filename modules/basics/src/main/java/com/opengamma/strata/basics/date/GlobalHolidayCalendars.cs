﻿using System;
using System.Collections.Generic;
using System.IO;

/*
 * Copyright (C) 2014 - present by OpenGamma Inc. and the OpenGamma group of companies
 *
 * Please see distribution for license.
 */
namespace com.opengamma.strata.basics.date
{


	using Files = com.google.common.io.Files;

	/// <summary>
	/// Implementation of some common global holiday calendars.
	/// <para>
	/// The data provided here has been identified through direct research and is not
	/// derived from a vendor of holiday calendar data.
	/// This data may or may not be sufficient for your production needs.
	/// </para>
	/// </summary>
	internal sealed class GlobalHolidayCalendars
	{
	  // WARNING!!
	  // If you change this file, you must run the main method to update the binary file
	  // which is used at runtime (for performance reasons)

	  /// <summary>
	  /// Where to store the file. </summary>
	  private static readonly File DATA_FILE = new File("src/main/resources/com/opengamma/strata/basics/date/GlobalHolidayCalendars.bin");

	  //-------------------------------------------------------------------------
	  /// <summary>
	  /// Used to generate a binary holiday data file.
	  /// </summary>
	  /// <param name="args"> ignored </param>
	  /// <exception cref="IOException"> if an IO error occurs </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void main(String[] args) throws java.io.IOException
	  public static void Main(string[] args)
	  {
		Files.createParentDirs(DATA_FILE);
		ImmutableHolidayCalendar[] calendars = new ImmutableHolidayCalendar[] {generateLondon(), generateParis(), generateFrankfurt(), generateZurich(), generateEuropeanTarget(), generateUsGovtSecurities(), generateUsNewYork(), generateNewYorkFed(), generateNewYorkStockExchange(), generateTokyo(), generateSydney(), generateBrazil(), generateMontreal(), generateToronto(), generatePrague(), generateCopenhagen(), generateBudapest(), generateMexicoCity(), generateOslo(), generateAuckland(), generateWellington(), generateNewZealand(), generateWarsaw(), generateStockholm(), generateJohannesburg()};
		using (FileStream fos = new FileStream(DATA_FILE, FileMode.Create, FileAccess.Write))
		{
		  using (DataOutputStream @out = new DataOutputStream(fos))
		  {
			@out.writeByte('H');
			@out.writeByte('C');
			@out.writeByte('a');
			@out.writeByte('l');
			@out.writeShort(calendars.Length);
			foreach (ImmutableHolidayCalendar cal in calendars)
			{
			  cal.writeExternal(@out);
			}
		  }
		}
	  }

	  /// <summary>
	  /// Restricted constructor.
	  /// </summary>
	  private GlobalHolidayCalendars()
	  {
	  }

	  //-------------------------------------------------------------------------
	  // generate GBLO
	  // common law (including before 1871) good friday and christmas day (unadjusted for weekends)
	  // from 1871 easter monday, whit monday, first Mon in Aug and boxing day
	  // from 1965 to 1970, first in Aug moved to Mon after last Sat in Aug
	  // from 1971, whitsun moved to last Mon in May, last Mon in Aug
	  // from 1974, added new year
	  // from 1978, added first Mon in May
	  // see Hansard for specific details
	  // 1965, Whitsun, Last Mon Aug - http://hansard.millbanksystems.com/commons/1964/mar/04/staggered-holidays
	  // 1966, Whitsun May - http://hansard.millbanksystems.com/commons/1964/mar/04/staggered-holidays
	  // 1966, 29th Aug - http://hansard.millbanksystems.com/written_answers/1965/nov/25/august-bank-holiday
	  // 1967, 29th May, 28th Aug - http://hansard.millbanksystems.com/written_answers/1965/jun/03/bank-holidays-1967-and-1968
	  // 1968, 3rd Jun, 2nd Sep - http://hansard.millbanksystems.com/written_answers/1965/jun/03/bank-holidays-1967-and-1968
	  // 1969, 26th May, 1st Sep - http://hansard.millbanksystems.com/written_answers/1967/mar/21/bank-holidays-1969-dates
	  // 1970, 25th May, 31st Aug - http://hansard.millbanksystems.com/written_answers/1967/jul/28/bank-holidays
	  internal static ImmutableHolidayCalendar generateLondon()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  if (year >= 1974)
		  {
			holidays.Add(bumpToMon(first(year, 1)));
		  }
		  // easter
		  holidays.Add(easter(year).minusDays(2));
		  holidays.Add(easter(year).plusDays(1));
		  // early May
		  if (year == 1995)
		  {
			// ve day
			holidays.Add(date(1995, 5, 8));
		  }
		  else if (year >= 1978)
		  {
			holidays.Add(first(year, 5).with(firstInMonth(MONDAY)));
		  }
		  // spring
		  if (year == 2002)
		  {
			// golden jubilee
			holidays.Add(date(2002, 6, 3));
			holidays.Add(date(2002, 6, 4));
		  }
		  else if (year == 2012)
		  {
			// diamond jubilee
			holidays.Add(date(2012, 6, 4));
			holidays.Add(date(2012, 6, 5));
		  }
		  else if (year == 1967 || year == 1970)
		  {
			holidays.Add(first(year, 5).with(lastInMonth(MONDAY)));
		  }
		  else if (year < 1971)
		  {
			// whitsun
			holidays.Add(easter(year).plusDays(50));
		  }
		  else
		  {
			holidays.Add(first(year, 5).with(lastInMonth(MONDAY)));
		  }
		  // summer
		  if (year < 1965)
		  {
			holidays.Add(first(year, 8).with(firstInMonth(MONDAY)));
		  }
		  else if (year < 1971)
		  {
			holidays.Add(first(year, 8).with(lastInMonth(SATURDAY)).plusDays(2));
		  }
		  else
		  {
			holidays.Add(first(year, 8).with(lastInMonth(MONDAY)));
		  }
		  // christmas
		  holidays.Add(christmasBumpedSatSun(year));
		  holidays.Add(boxingDayBumpedSatSun(year));
		}
		holidays.Add(date(2011, 4, 29)); // royal wedding
		holidays.Add(date(1999, 12, 31)); // millennium
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.GBLO, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate FRPA
	  // data sources
	  // http://www.legifrance.gouv.fr/affichCodeArticle.do?idArticle=LEGIARTI000006902611&cidTexte=LEGITEXT000006072050
	  // http://jollyday.sourceforge.net/data/fr.html
	  // Euronext holidays only New Year, Good Friday, Easter Monday, Labour Day, Christmas Day, Boxing Day
	  // New Years Eve is holiday for cash markets and derivatives in 2015
	  // https://www.euronext.com/en/holidays-and-hours
	  // https://www.euronext.com/en/trading/nyse-euronext-trading-calendar/archives
	  // evidence suggests that Monday is holiday when Tuesday is, and Friday is holiday when Thursday is
	  internal static ImmutableHolidayCalendar generateParis()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  holidays.Add(date(year, 1, 1)); // new year
		  holidays.Add(easter(year).minusDays(2)); // good friday
		  holidays.Add(easter(year).plusDays(1)); // easter monday
		  holidays.Add(date(year, 5, 1)); // labour day
		  holidays.Add(date(year, 5, 8)); // victory in europe
		  holidays.Add(easter(year).plusDays(39)); // ascension day
		  if (year <= 2004 || year >= 2008)
		  {
			holidays.Add(easter(year).plusDays(50)); // whit monday
		  }
		  holidays.Add(date(year, 7, 14)); // bastille
		  holidays.Add(date(year, 8, 15)); // assumption of mary
		  holidays.Add(date(year, 11, 1)); // all saints
		  holidays.Add(date(year, 11, 11)); // armistice day
		  holidays.Add(date(year, 12, 25)); // christmas day
		  holidays.Add(date(year, 12, 26)); // saint stephen
		}
		holidays.Add(date(1999, 12, 31)); // millennium
		applyBridging(holidays);
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.FRPA, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate DEFR
	  // data sources
	  // https://www.feiertagskalender.ch/index.php?geo=3122&klasse=3&jahr=2017&hl=en
	  // http://jollyday.sourceforge.net/data/de.html
	  internal static ImmutableHolidayCalendar generateFrankfurt()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  holidays.Add(date(year, 1, 1)); // new year
		  holidays.Add(easter(year).minusDays(2)); // good friday
		  holidays.Add(easter(year).plusDays(1)); // easter monday
		  holidays.Add(date(year, 5, 1)); // labour day
		  holidays.Add(easter(year).plusDays(39)); // ascension day
		  holidays.Add(easter(year).plusDays(50)); // whit monday
		  holidays.Add(easter(year).plusDays(60)); // corpus christi
		  if (year >= 2000)
		  {
			holidays.Add(date(year, 10, 3)); // german unity
		  }
		  if (year <= 1994)
		  {
			// Wed before the Sunday that is 2 weeks before first advent, which is 4th Sunday before Christmas
			holidays.Add(date(year, 12, 25).with(previous(SUNDAY)).minusWeeks(6).minusDays(4)); // repentance
		  }
		  holidays.Add(date(year, 12, 25)); // christmas day
		  holidays.Add(date(year, 12, 26)); // saint stephen
		  holidays.Add(date(year, 12, 31)); // new year
		}
		holidays.Add(date(2017, 10, 31)); // reformation day
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.DEFR, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate CHZU
	  // data sources
	  // http://jollyday.sourceforge.net/data/ch.html
	  // https://github.com/lballabio/quantlib/blob/master/QuantLib/ql/time/calendars/switzerland.cpp
	  // http://www.six-swiss-exchange.com/funds/trading/trading_and_settlement_calendar_en.html
	  // http://www.six-swiss-exchange.com/swx_messages/online/swx7299e.pdf
	  internal static ImmutableHolidayCalendar generateZurich()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  holidays.Add(date(year, 1, 1)); // new year
		  holidays.Add(date(year, 1, 2)); // saint berchtoldstag
		  holidays.Add(easter(year).minusDays(2)); // good friday
		  holidays.Add(easter(year).plusDays(1)); // easter monday
		  holidays.Add(date(year, 5, 1)); // labour day
		  holidays.Add(easter(year).plusDays(39)); // ascension day
		  holidays.Add(easter(year).plusDays(50)); // whit monday
		  holidays.Add(date(year, 8, 1)); // national day
		  holidays.Add(date(year, 12, 25)); // christmas day
		  holidays.Add(date(year, 12, 26)); // saint stephen
		}
		holidays.Add(date(1999, 12, 31)); // millennium
		holidays.Add(date(2000, 1, 3)); // millennium
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.CHZU, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate EUTA
	  // 1997 - 1998 (testing phase), Jan 1, christmas day
	  // https://www.ecb.europa.eu/pub/pdf/other/tagien.pdf
	  // in 1999, Jan 1, christmas day, Dec 26, Dec 31
	  // http://www.ecb.europa.eu/press/pr/date/1999/html/pr990715_1.en.html
	  // http://www.ecb.europa.eu/press/pr/date/1999/html/pr990331.en.html
	  // in 2000, Jan 1, good friday, easter monday, May 1, christmas day, Dec 26
	  // http://www.ecb.europa.eu/press/pr/date/1999/html/pr990715_1.en.html
	  // in 2001, Jan 1, good friday, easter monday, May 1, christmas day, Dec 26, Dec 31
	  // http://www.ecb.europa.eu/press/pr/date/2000/html/pr000525_2.en.html
	  // from 2002, Jan 1, good friday, easter monday, May 1, christmas day, Dec 26
	  // http://www.ecb.europa.eu/press/pr/date/2000/html/pr001214_4.en.html
	  internal static ImmutableHolidayCalendar generateEuropeanTarget()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1997; year <= 2099; year++)
		{
		  if (year >= 2000)
		  {
			holidays.Add(date(year, 1, 1));
			holidays.Add(easter(year).minusDays(2));
			holidays.Add(easter(year).plusDays(1));
			holidays.Add(date(year, 5, 1));
			holidays.Add(date(year, 12, 25));
			holidays.Add(date(year, 12, 26));
		  }
		  else
		  { // 1997 to 1999
			holidays.Add(date(year, 1, 1));
			holidays.Add(date(year, 12, 25));
		  }
		  if (year == 1999 || year == 2001)
		  {
			holidays.Add(date(year, 12, 31));
		  }
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.EUTA, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // common US holidays
	  private static void usCommon(IList<LocalDate> holidays, int year, bool bumpBack, bool columbusVeteran, int mlkStartYear)
	  {
		// new year, adjusted if Sunday
		holidays.Add(bumpSunToMon(date(year, 1, 1)));
		// martin luther king
		if (year >= mlkStartYear)
		{
		  holidays.Add(date(year, 1, 1).with(dayOfWeekInMonth(3, MONDAY)));
		}
		// washington
		if (year < 1971)
		{
		  holidays.Add(bumpSunToMon(date(year, 2, 22)));
		}
		else
		{
		  holidays.Add(date(year, 2, 1).with(dayOfWeekInMonth(3, MONDAY)));
		}
		// memorial
		if (year < 1971)
		{
		  holidays.Add(bumpSunToMon(date(year, 5, 30)));
		}
		else
		{
		  holidays.Add(date(year, 5, 1).with(lastInMonth(MONDAY)));
		}
		// labor day
		holidays.Add(date(year, 9, 1).with(firstInMonth(MONDAY)));
		// columbus day
		if (columbusVeteran)
		{
		  if (year < 1971)
		  {
			holidays.Add(bumpSunToMon(date(year, 10, 12)));
		  }
		  else
		  {
			holidays.Add(date(year, 10, 1).with(dayOfWeekInMonth(2, MONDAY)));
		  }
		}
		// veterans day
		if (columbusVeteran)
		{
		  if (year >= 1971 && year < 1978)
		  {
			holidays.Add(date(year, 10, 1).with(dayOfWeekInMonth(4, MONDAY)));
		  }
		  else
		  {
			holidays.Add(bumpSunToMon(date(year, 11, 11)));
		  }
		}
		// thanksgiving
		holidays.Add(date(year, 11, 1).with(dayOfWeekInMonth(4, THURSDAY)));
		// independence day & christmas day
		if (bumpBack)
		{
		  holidays.Add(bumpToFriOrMon(date(year, 7, 4)));
		  holidays.Add(bumpToFriOrMon(date(year, 12, 25)));
		}
		else
		{
		  holidays.Add(bumpSunToMon(date(year, 7, 4)));
		  holidays.Add(bumpSunToMon(date(year, 12, 25)));
		}
	  }

	  // generate USGS
	  // http://www.sifma.org/services/holiday-schedule/
	  internal static ImmutableHolidayCalendar generateUsGovtSecurities()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  usCommon(holidays, year, true, true, 1986);
		  // good friday, in 1999/2007 only a partial holiday
		  holidays.Add(easter(year).minusDays(2));
		  // hurricane sandy
		  if (year == 2012)
		  {
			holidays.Add(date(year, 10, 30));
		  }
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.USGS, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate USNY
	  // http://www.cs.ny.gov/attendance_leave/2012_legal_holidays.cfm
	  // http://www.cs.ny.gov/attendance_leave/2013_legal_holidays.cfm
	  // etc
	  // ignore election day and lincoln day
	  internal static ImmutableHolidayCalendar generateUsNewYork()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  usCommon(holidays, year, false, true, 1986);
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.USNY, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate NYFD
	  // http://www.ny.frb.org/aboutthefed/holiday_schedule.html
	  internal static ImmutableHolidayCalendar generateNewYorkFed()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  usCommon(holidays, year, false, true, 1986);
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.NYFD, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate NYSE
	  // https://www.nyse.com/markets/hours-calendars
	  // http://www1.nyse.com/pdfs/closings.pdf
	  internal static ImmutableHolidayCalendar generateNewYorkStockExchange()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  usCommon(holidays, year, true, false, 1998);
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		}
		// Lincoln day 1896-1953
		// Columbus day 1909-1953
		// Veterans day 1934-1953
		for (int i = 1950; i <= 1953; i++)
		{
		  holidays.Add(date(i, 2, 12));
		  holidays.Add(date(i, 10, 12));
		  holidays.Add(date(i, 11, 11));
		}
		// election day, Tue after first Monday of November
		for (int i = 1950; i <= 1968; i++)
		{
		  holidays.Add(date(i, 11, 1).with(TemporalAdjusters.nextOrSame(MONDAY)).plusDays(1));
		}
		holidays.Add(date(1972, 11, 7));
		holidays.Add(date(1976, 11, 2));
		holidays.Add(date(1980, 11, 4));
		// special days
		holidays.Add(date(1955, 12, 24)); // Christmas Eve
		holidays.Add(date(1956, 12, 24)); // Christmas Eve
		holidays.Add(date(1958, 12, 26)); // Day after Christmas
		holidays.Add(date(1961, 5, 29)); // Decoration day
		holidays.Add(date(1963, 11, 25)); // Death of John F Kennedy
		holidays.Add(date(1965, 12, 24)); // Christmas Eve
		holidays.Add(date(1968, 2, 12)); // Lincoln birthday
		holidays.Add(date(1968, 4, 9)); // Death of Martin Luther King
		holidays.Add(date(1968, 6, 12)); // Paperwork crisis
		holidays.Add(date(1968, 6, 19)); // Paperwork crisis
		holidays.Add(date(1968, 6, 26)); // Paperwork crisis
		holidays.Add(date(1968, 7, 3)); // Paperwork crisis
		holidays.Add(date(1968, 7, 5)); // Day after independence
		holidays.Add(date(1968, 7, 10)); // Paperwork crisis
		holidays.Add(date(1968, 7, 17)); // Paperwork crisis
		holidays.Add(date(1968, 7, 24)); // Paperwork crisis
		holidays.Add(date(1968, 7, 31)); // Paperwork crisis
		holidays.Add(date(1968, 8, 7)); // Paperwork crisis
		holidays.Add(date(1968, 8, 13)); // Paperwork crisis
		holidays.Add(date(1968, 8, 21)); // Paperwork crisis
		holidays.Add(date(1968, 8, 28)); // Paperwork crisis
		holidays.Add(date(1968, 9, 4)); // Paperwork crisis
		holidays.Add(date(1968, 9, 11)); // Paperwork crisis
		holidays.Add(date(1968, 9, 18)); // Paperwork crisis
		holidays.Add(date(1968, 9, 25)); // Paperwork crisis
		holidays.Add(date(1968, 10, 2)); // Paperwork crisis
		holidays.Add(date(1968, 10, 9)); // Paperwork crisis
		holidays.Add(date(1968, 10, 16)); // Paperwork crisis
		holidays.Add(date(1968, 10, 23)); // Paperwork crisis
		holidays.Add(date(1968, 10, 30)); // Paperwork crisis
		holidays.Add(date(1968, 11, 6)); // Paperwork crisis
		holidays.Add(date(1968, 11, 13)); // Paperwork crisis
		holidays.Add(date(1968, 11, 20)); // Paperwork crisis
		holidays.Add(date(1968, 11, 27)); // Paperwork crisis
		holidays.Add(date(1968, 12, 4)); // Paperwork crisis
		holidays.Add(date(1968, 12, 11)); // Paperwork crisis
		holidays.Add(date(1968, 12, 18)); // Paperwork crisis
		holidays.Add(date(1968, 12, 25)); // Paperwork crisis
		holidays.Add(date(1968, 12, 31)); // Paperwork crisis
		holidays.Add(date(1969, 2, 10)); // Snow
		holidays.Add(date(1969, 3, 31)); // Death of Dwight Eisenhower
		holidays.Add(date(1969, 7, 21)); // Lunar exploration
		holidays.Add(date(1972, 12, 28)); // Death of Harry Truman
		holidays.Add(date(1973, 1, 25)); // Death of Lyndon Johnson
		holidays.Add(date(1977, 7, 14)); // Blackout
		holidays.Add(date(1985, 9, 27)); // Hurricane Gloria
		holidays.Add(date(1994, 4, 27)); // Death of Richard Nixon
		holidays.Add(date(2001, 9, 11)); // 9/11 attack
		holidays.Add(date(2001, 9, 12)); // 9/11 attack
		holidays.Add(date(2001, 9, 13)); // 9/11 attack
		holidays.Add(date(2001, 9, 14)); // 9/11 attack
		holidays.Add(date(2004, 6, 11)); // Death of Ronald Reagan
		holidays.Add(date(2007, 1, 2)); // Death of Gerald Ford
		holidays.Add(date(2012, 10, 30)); // Hurricane Sandy
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.NYSE, holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate JPTO
	  // data sources
	  // https://www.boj.or.jp/en/about/outline/holi.htm/
	  // http://web.archive.org/web/20110513190217/http://www.boj.or.jp/en/about/outline/holi.htm/
	  // http://web.archive.org/web/20130502031733/http://www.boj.or.jp/en/about/outline/holi.htm
	  // http://www8.cao.go.jp/chosei/shukujitsu/gaiyou.html (law)
	  // http://www.nao.ac.jp/faq/a0301.html (equinox)
	  // http://eco.mtk.nao.ac.jp/koyomi/faq/holiday.html.en
	  internal static ImmutableHolidayCalendar generateTokyo()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  holidays.Add(date(year, 1, 2));
		  holidays.Add(date(year, 1, 3));
		  // coming of age
		  if (year >= 2000)
		  {
			holidays.Add(date(year, 1, 1).with(dayOfWeekInMonth(2, MONDAY)));
		  }
		  else
		  {
			holidays.Add(bumpSunToMon(date(year, 1, 15)));
		  }
		  // national foundation
		  if (year >= 1967)
		  {
			holidays.Add(bumpSunToMon(date(year, 2, 11)));
		  }
		  // vernal equinox (from 1948), 20th or 21st (predictions/facts 2000 to 2030)
		  if (year == 2000 || year == 2001 || year == 2004 || year == 2005 || year == 2008 || year == 2009 || year == 2012 || year == 2013 || year == 2016 || year == 2017 || year == 2020 || year == 2021 || year == 2024 || year == 2025 || year == 2026 || year == 2028 || year == 2029 || year == 2030)
		  {
			holidays.Add(bumpSunToMon(date(year, 3, 20)));
		  }
		  else
		  {
			holidays.Add(bumpSunToMon(date(year, 3, 21)));
		  }
		  // showa (from 2007 onwards), greenery (from 1989 to 2006), emperor (before 1989)
		  // http://news.bbc.co.uk/1/hi/world/asia-pacific/4543461.stm
		  holidays.Add(bumpSunToMon(date(year, 4, 29)));
		  // constitution (from 1948)
		  // greenery (from 2007 onwards), holiday between two other holidays before that (from 1985)
		  // children (from 1948)
		  if (year >= 1985)
		  {
			holidays.Add(bumpSunToMon(date(year, 5, 3)));
			holidays.Add(bumpSunToMon(date(year, 5, 4)));
			holidays.Add(bumpSunToMon(date(year, 5, 5)));
			if (year >= 2007 && (date(year, 5, 3).DayOfWeek == SUNDAY || date(year, 5, 4).DayOfWeek == SUNDAY))
			{
			  holidays.Add(date(year, 5, 6));
			}
		  }
		  else
		  {
			holidays.Add(bumpSunToMon(date(year, 5, 3)));
			holidays.Add(bumpSunToMon(date(year, 5, 5)));
		  }
		  // marine
		  if (year >= 2003)
		  {
			holidays.Add(date(year, 7, 1).with(dayOfWeekInMonth(3, MONDAY)));
		  }
		  else if (year >= 1996)
		  {
			holidays.Add(bumpSunToMon(date(year, 7, 20)));
		  }
		  // mountain
		  if (year >= 2016)
		  {
			holidays.Add(bumpSunToMon(date(year, 8, 11)));
		  }
		  // aged
		  if (year >= 2003)
		  {
			holidays.Add(date(year, 9, 1).with(dayOfWeekInMonth(3, MONDAY)));
		  }
		  else if (year >= 1966)
		  {
			holidays.Add(bumpSunToMon(date(year, 9, 15)));
		  }
		  // autumn equinox (from 1948), 22nd or 23rd (predictions/facts 2000 to 2030)
		  if (year == 2012 || year == 2016 || year == 2020 || year == 2024 || year == 2028)
		  {
			holidays.Add(bumpSunToMon(date(year, 9, 22)));
		  }
		  else
		  {
			holidays.Add(bumpSunToMon(date(year, 9, 23)));
		  }
		  citizensDay(holidays, date(year, 9, 20), date(year, 9, 22));
		  citizensDay(holidays, date(year, 9, 21), date(year, 9, 23));
		  // health-sports
		  if (year >= 2000)
		  {
			holidays.Add(date(year, 10, 1).with(dayOfWeekInMonth(2, MONDAY)));
		  }
		  else if (year >= 1966)
		  {
			holidays.Add(bumpSunToMon(date(year, 10, 10)));
		  }
		  // culture (from 1948)
		  holidays.Add(bumpSunToMon(date(year, 11, 3)));
		  // labor (from 1948)
		  holidays.Add(bumpSunToMon(date(year, 11, 23)));
		  // emperor (current emporer)
		  if (year >= 1990)
		  {
			holidays.Add(bumpSunToMon(date(year, 12, 23)));
		  }
		  // new years eve - bank of Japan, but not national holiday
		  holidays.Add(bumpSunToMon(date(year, 12, 31)));
		}
		holidays.Add(date(1959, 4, 10)); // marriage akihito
		holidays.Add(date(1989, 2, 24)); // funeral showa
		holidays.Add(date(1990, 11, 12)); // enthrone akihito
		holidays.Add(date(1993, 6, 9)); // marriage naruhito
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarIds.JPTO, holidays, SATURDAY, SUNDAY);
	  }

	  // extra day between two other holidays, appears to exclude weekends
	  private static void citizensDay(IList<LocalDate> holidays, LocalDate date1, LocalDate date2)
	  {
		if (holidays.Contains(date1) && holidays.Contains(date2))
		{
		  if (date1.DayOfWeek == MONDAY || date1.DayOfWeek == TUESDAY || date1.DayOfWeek == WEDNESDAY)
		  {
			holidays.Add(date1.plusDays(1));
		  }
		}
	  }

	  //-------------------------------------------------------------------------
	  // generate CAMO
	  // data sources
	  // https://www.cnt.gouv.qc.ca/en/leaves-and-absences/statutory-holidays/index.html
	  // https://www.canada.ca/en/revenue-agency/services/tax/public-holidays.html
	  // http://www.statutoryholidayscanada.com/
	  internal static ImmutableHolidayCalendar generateMontreal()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(bumpToMon(date(year, 1, 1)));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // patriots
		  holidays.Add(date(year, 5, 25).with(TemporalAdjusters.previous(MONDAY)));
		  // fete nationale quebec
		  holidays.Add(bumpToMon(date(year, 6, 24)));
		  // canada
		  holidays.Add(bumpToMon(date(year, 7, 1)));
		  // labour
		  holidays.Add(first(year, 9).with(dayOfWeekInMonth(1, MONDAY)));
		  // thanksgiving
		  holidays.Add(first(year, 10).with(dayOfWeekInMonth(2, MONDAY)));
		  // christmas
		  holidays.Add(christmasBumpedSatSun(year));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("CAMO"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate CATO
	  // data sources
	  // http://www.labour.gov.on.ca/english/es/pubs/guide/publicholidays.php
	  // http://www.cra-arc.gc.ca/tx/hldys/menu-eng.html
	  // http://www.tmxmoney.com/en/investor_tools/market_hours.html
	  // http://www.statutoryholidayscanada.com/
	  // http://www.osc.gov.on.ca/en/SecuritiesLaw_csa_20151209_13-315_sra-closed-dates.htm
	  internal static ImmutableHolidayCalendar generateToronto()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year (public)
		  holidays.Add(bumpToMon(date(year, 1, 1)));
		  // family (public)
		  if (year >= 2008)
		  {
			holidays.Add(first(year, 2).with(dayOfWeekInMonth(3, MONDAY)));
		  }
		  // good friday (public)
		  holidays.Add(easter(year).minusDays(2));
		  // victoria (public)
		  holidays.Add(date(year, 5, 25).with(TemporalAdjusters.previous(MONDAY)));
		  // canada (public)
		  holidays.Add(bumpToMon(date(year, 7, 1)));
		  // civic
		  holidays.Add(first(year, 8).with(dayOfWeekInMonth(1, MONDAY)));
		  // labour (public)
		  holidays.Add(first(year, 9).with(dayOfWeekInMonth(1, MONDAY)));
		  // thanksgiving (public)
		  holidays.Add(first(year, 10).with(dayOfWeekInMonth(2, MONDAY)));
		  // remembrance
		  holidays.Add(bumpToMon(date(year, 11, 11)));
		  // christmas (public)
		  holidays.Add(christmasBumpedSatSun(year));
		  // boxing (public)
		  holidays.Add(boxingDayBumpedSatSun(year));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("CATO"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate DKCO
	  // data sources
	  // http://www.finansraadet.dk/Bankkunde/Pages/bankhelligdage.aspx
	  // web archive history of those pages
	  internal static ImmutableHolidayCalendar generateCopenhagen()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  // maundy thursday
		  holidays.Add(easter(year).minusDays(3));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // easter monday
		  holidays.Add(easter(year).plusDays(1));
		  // prayer day (Friday)
		  holidays.Add(easter(year).plusDays(26));
		  // ascension (Thursday)
		  holidays.Add(easter(year).plusDays(39));
		  // ascension + 1 (Friday)
		  holidays.Add(easter(year).plusDays(40));
		  // whit monday
		  holidays.Add(easter(year).plusDays(50));
		  // constitution
		  holidays.Add(date(year, 6, 5));
		  // christmas eve
		  holidays.Add(date(year, 12, 24));
		  // christmas
		  holidays.Add(date(year, 12, 25));
		  // boxing
		  holidays.Add(date(year, 12, 26));
		  // new years eve
		  holidays.Add(date(year, 12, 31));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("DKCO"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate NOOS
	  // data sources
	  // http://www.oslobors.no/ob_eng/Oslo-Boers/About-Oslo-Boers/Opening-hours
	  // http://www.oslobors.no/Oslo-Boers/Om-Oslo-Boers/AApningstider
	  // web archive history of those pages
	  internal static ImmutableHolidayCalendar generateOslo()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  // maundy thursday
		  holidays.Add(easter(year).minusDays(3));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // easter monday
		  holidays.Add(easter(year).plusDays(1));
		  // labour
		  holidays.Add(date(year, 5, 1));
		  // constitution
		  holidays.Add(date(year, 5, 17));
		  // ascension
		  holidays.Add(easter(year).plusDays(39));
		  // whit monday
		  holidays.Add(easter(year).plusDays(50));
		  // christmas eve
		  holidays.Add(date(year, 12, 24));
		  // christmas
		  holidays.Add(date(year, 12, 25));
		  // boxing
		  holidays.Add(date(year, 12, 26));
		  // new years eve
		  holidays.Add(date(year, 12, 31));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("NOOS"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate NZAU
	  // https://www.nzfma.org/Site/practices_standards/market_conventions.aspx
	  internal static ImmutableHolidayCalendar generateAuckland()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  newZealand(holidays, year);
		  // auckland anniversary day
		  holidays.Add(date(year, 1, 29).minusDays(3).with(nextOrSame(MONDAY)));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("NZAU"), holidays, SATURDAY, SUNDAY);
	  }

	  // generate NZWE
	  // https://www.nzfma.org/Site/practices_standards/market_conventions.aspx
	  internal static ImmutableHolidayCalendar generateWellington()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  newZealand(holidays, year);
		  // wellington anniversary day
		  holidays.Add(date(year, 1, 22).minusDays(3).with(nextOrSame(MONDAY)));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("NZWE"), holidays, SATURDAY, SUNDAY);
	  }

	  // generate NZBD
	  // https://www.nzfma.org/Site/practices_standards/market_conventions.aspx
	  internal static ImmutableHolidayCalendar generateNewZealand()
	  {
		// artificial non-ISDA definition named after BRBD for Brazil
		// this is needed as NZD-BBR index is published on both Wellington and Auckland anniversary days
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  newZealand(holidays, year);
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("NZBD"), holidays, SATURDAY, SUNDAY);
	  }

	  private static void newZealand(IList<LocalDate> holidays, int year)
	  {
		// new year and day after
		LocalDate newYear = bumpToMon(date(year, 1, 1));
		holidays.Add(newYear);
		holidays.Add(bumpToMon(newYear.plusDays(1)));
		// waitangi day
		// https://www.employment.govt.nz/leave-and-holidays/public-holidays/public-holidays-and-anniversary-dates/
		if (year >= 2014)
		{
		  holidays.Add(bumpToMon(date(year, 2, 6)));
		}
		else
		{
		  holidays.Add(date(year, 2, 6));
		}
		// good friday
		holidays.Add(easter(year).minusDays(2));
		// easter monday
		holidays.Add(easter(year).plusDays(1));
		// anzac day
		// https://www.employment.govt.nz/leave-and-holidays/public-holidays/public-holidays-and-anniversary-dates/
		if (year >= 2014)
		{
		  holidays.Add(bumpToMon(date(year, 4, 25)));
		}
		else
		{
		  holidays.Add(date(year, 4, 25));
		}
		// queens birthday
		holidays.Add(first(year, 6).with(firstInMonth(MONDAY)));
		// labour day
		holidays.Add(first(year, 10).with(dayOfWeekInMonth(4, MONDAY)));
		// christmas
		holidays.Add(christmasBumpedSatSun(year));
		holidays.Add(boxingDayBumpedSatSun(year));
	  }

	  //-------------------------------------------------------------------------
	  // generate PLWA
	  // data sources#
	  // http://isap.sejm.gov.pl/DetailsServlet?id=WDU19510040028 and linked pages
	  // https://www.gpw.pl/dni_bez_sesji_en
	  // http://jollyday.sourceforge.net/data/pl.html
	  internal static ImmutableHolidayCalendar generateWarsaw()
	  {
		// holiday law dates from 1951, but don't know situation before then, so ignore 1951 date
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  // epiphany
		  if (year < 1961 || year >= 2011)
		  {
			holidays.Add(date(year, 1, 6));
		  }
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // easter monday
		  holidays.Add(easter(year).plusDays(1));
		  // state
		  holidays.Add(date(year, 5, 1));
		  // constitution
		  if (year >= 1990)
		  {
			holidays.Add(date(year, 5, 3));
		  }
		  // rebirth/national
		  if (year < 1990)
		  {
			holidays.Add(date(year, 7, 22));
		  }
		  // corpus christi
		  holidays.Add(easter(year).plusDays(60));
		  // assumption
		  if (year < 1961 || year >= 1989)
		  {
			holidays.Add(date(year, 8, 15));
		  }
		  // all saints
		  holidays.Add(date(year, 11, 1));
		  // independence
		  if (year >= 1990)
		  {
			holidays.Add(date(year, 11, 11));
		  }
		  // christmas (exchange)
		  holidays.Add(date(year, 12, 24));
		  // christmas
		  holidays.Add(date(year, 12, 25));
		  // boxing
		  holidays.Add(date(year, 12, 26));
		  // new years eve (exchange, rule based on sample data)
		  LocalDate nyeve = date(year, 12, 31);
		  if (nyeve.DayOfWeek == MONDAY || nyeve.DayOfWeek == THURSDAY || nyeve.DayOfWeek == FRIDAY)
		  {
			holidays.Add(nyeve);
		  }
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("PLWA"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // generate SEST
	  // data sources - history of dates that STIBOR fixing occurred
	  // http://www.riksbank.se/en/Interest-and-exchange-rates/search-interest-rates-exchange-rates/?g5-SEDP1MSTIBOR=on&from=2016-01-01&to=2016-10-05&f=Day&cAverage=Average&s=Comma#search
	  internal static ImmutableHolidayCalendar generateStockholm()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  // epiphany
		  holidays.Add(date(year, 1, 6));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // easter monday
		  holidays.Add(easter(year).plusDays(1));
		  // labour
		  holidays.Add(date(year, 5, 1));
		  // ascension
		  holidays.Add(easter(year).plusDays(39));
		  // midsummer friday
		  holidays.Add(date(year, 6, 19).with(nextOrSame(FRIDAY)));
		  // national
		  if (year > 2005)
		  {
			holidays.Add(date(year, 6, 6));
		  }
		  // christmas
		  holidays.Add(date(year, 12, 24));
		  // christmas
		  holidays.Add(date(year, 12, 25));
		  // boxing
		  holidays.Add(date(year, 12, 26));
		  // new years eve (fixings, rule based on sample data)
		  holidays.Add(date(year, 12, 31));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("SEST"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // http://www.rba.gov.au/schedules-events/bank-holidays/bank-holidays-2016.html
	  // http://www.rba.gov.au/schedules-events/bank-holidays/bank-holidays-2017.html
	  // web archive history of those pages
	  internal static ImmutableHolidayCalendar generateSydney()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(bumpToMon(date(year, 1, 1)));
		  // australia day
		  holidays.Add(bumpToMon(date(year, 1, 26)));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // easter monday
		  holidays.Add(easter(year).plusDays(1));
		  // anzac day
		  holidays.Add(date(year, 4, 25));
		  // queen's birthday
		  holidays.Add(first(year, 6).with(dayOfWeekInMonth(2, MONDAY)));
		  // bank holiday
		  holidays.Add(first(year, 8).with(dayOfWeekInMonth(1, MONDAY)));
		  // labour day
		  holidays.Add(first(year, 10).with(dayOfWeekInMonth(1, MONDAY)));
		  // christmas
		  holidays.Add(christmasBumpedSatSun(year));
		  // boxing
		  holidays.Add(boxingDayBumpedSatSun(year));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("AUSY"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // http://www.gov.za/about-sa/public-holidays
	  // http://www.gov.za/sites/www.gov.za/files/Act36of1994.pdf
	  // http://www.gov.za/sites/www.gov.za/files/Act48of1995.pdf
	  // 27th Dec when Tue http://www.gov.za/sites/www.gov.za/files/34881_proc72.pdf
	  internal static ImmutableHolidayCalendar generateJohannesburg()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // from 1995 (act of 7 Dec 1994)
		  // older act from 1952 not implemented here
		  // new year
		  holidays.Add(bumpSunToMon(date(year, 1, 1)));
		  // human rights day
		  holidays.Add(bumpSunToMon(date(year, 3, 21)));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // family day (easter monday)
		  holidays.Add(easter(year).plusDays(1));
		  // freedom day
		  holidays.Add(bumpSunToMon(date(year, 4, 27)));
		  // workers day
		  holidays.Add(bumpSunToMon(date(year, 5, 1)));
		  // youth day
		  holidays.Add(bumpSunToMon(date(year, 6, 16)));
		  // womens day
		  holidays.Add(bumpSunToMon(date(year, 8, 9)));
		  // heritage day
		  holidays.Add(bumpSunToMon(date(year, 9, 24)));
		  // reconcilliation
		  holidays.Add(bumpSunToMon(date(year, 12, 16)));
		  // christmas
		  holidays.Add(christmasBumpedSun(year));
		  // goodwill
		  holidays.Add(boxingDayBumpedSun(year));
		}
		// mostly election days
		// http://www.gov.za/sites/www.gov.za/files/40125_proc%2045.pdf
		holidays.Add(date(2016, 8, 3));
		// http://www.gov.za/sites/www.gov.za/files/37376_proc13.pdf
		holidays.Add(date(2014, 5, 7));
		// http://www.gov.za/sites/www.gov.za/files/34127_proc27.pdf
		holidays.Add(date(2011, 5, 18));
		// http://www.gov.za/sites/www.gov.za/files/32039_17.pdf
		holidays.Add(date(2009, 4, 22));
		// http://www.gov.za/sites/www.gov.za/files/30900_7.pdf (moved human rights day)
		holidays.Add(date(2008, 5, 2));
		// http://www.gov.za/sites/www.gov.za/files/28442_0.pdf
		holidays.Add(date(2006, 3, 1));
		// http://www.gov.za/sites/www.gov.za/files/26075.pdf
		holidays.Add(date(2004, 4, 14));
		// http://www.gov.za/sites/www.gov.za/files/20032_0.pdf
		holidays.Add(date(1999, 12, 31));
		holidays.Add(date(2000, 1, 1));
		holidays.Add(date(2000, 1, 2));
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("ZAJO"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // http://www.magyarkozlony.hu/dokumentumok/b0d596a3e6ce15a2350a9e138c058a78dd8622d0/megtekintes (article 148)
	  // http://www.mfa.gov.hu/NR/rdonlyres/18C1949E-D740-45E0-923A-BDFC81EC44C8/0/ListofHolidays2016.pdf
	  // http://jollyday.sourceforge.net/data/hu.html
	  // https://englishhungary.wordpress.com/2012/01/15/bridge-days/
	  // http://www.ucmsgroup.hu/newsletter/public-holiday-and-related-work-schedule-changes-in-2015/
	  // http://www.ucmsgroup.hu/newsletter/public-holiday-and-related-work-schedule-changes-in-2014/
	  internal static ImmutableHolidayCalendar generateBudapest()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		ISet<LocalDate> workDays = new HashSet<LocalDate>(500);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  addDateWithHungarianBridging(date(year, 1, 1), -1, 1, holidays, workDays);
		  // national day
		  addDateWithHungarianBridging(date(year, 3, 15), -2, 1, holidays, workDays);
		  if (year >= 2017)
		  {
			// good friday
			holidays.Add(easter(year).minusDays(2));
		  }
		  // easter monday
		  holidays.Add(easter(year).plusDays(1));
		  // labour day
		  addDateWithHungarianBridging(date(year, 5, 1), 0, 1, holidays, workDays);
		  // pentecost monday
		  holidays.Add(easter(year).plusDays(50));
		  // state foundation day
		  addDateWithHungarianBridging(date(year, 8, 20), 0, -2, holidays, workDays);
		  // national day
		  addDateWithHungarianBridging(date(year, 10, 23), 0, -1, holidays, workDays);
		  // all saints day
		  addDateWithHungarianBridging(date(year, 11, 1), -3, 1, holidays, workDays);
		  // christmas
		  holidays.Add(date(year, 12, 25));
		  holidays.Add(date(year, 12, 26));
		  if (date(year, 12, 25).DayOfWeek == TUESDAY)
		  {
			holidays.Add(date(year, 12, 24));
			workDays.Add(date(year, 12, 15));
		  }
		  else if (date(year, 12, 25).DayOfWeek == WEDNESDAY)
		  {
			holidays.Add(date(year, 12, 24));
			holidays.Add(date(year, 12, 27));
			workDays.Add(date(year, 12, 7));
			workDays.Add(date(year, 12, 21));
		  }
		  else if (date(year, 12, 25).DayOfWeek == THURSDAY)
		  {
			holidays.Add(date(year, 12, 24));
		  }
		  else if (date(year, 12, 25).DayOfWeek == FRIDAY)
		  {
			holidays.Add(date(year, 12, 24));
			workDays.Add(date(year, 12, 12));
		  }
		}
		// some Saturdays are work days
		addHungarianSaturdays(holidays, workDays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("HUBU"), holidays, SUNDAY, SUNDAY);
	  }

	  // an attempt to divine the official rules from the data available
	  private static void addDateWithHungarianBridging(LocalDate date, int relativeWeeksTue, int relativeWeeksThu, IList<LocalDate> holidays, ISet<LocalDate> workDays)
	  {

		DayOfWeek dow = date.DayOfWeek;
		switch (dow)
		{
		  case MONDAY:
		  case WEDNESDAY:
		  case FRIDAY:
			holidays.Add(date);
			return;
		  case TUESDAY:
			holidays.Add(date.minusDays(1));
			holidays.Add(date);
			workDays.Add(date.plusDays(4).plusWeeks(relativeWeeksTue)); // a Saturday is now a workday
			return;
		  case THURSDAY:
			holidays.Add(date.plusDays(1));
			holidays.Add(date);
			workDays.Add(date.plusDays(2).plusWeeks(relativeWeeksThu)); // a Saturday is now a workday
			return;
		  case SATURDAY:
		  case SUNDAY:
		  default:
			return;
		}
	  }

	  private static void addHungarianSaturdays(IList<LocalDate> holidays, ISet<LocalDate> workDays)
	  {
		// remove all saturdays and sundays
		removeSatSun(holidays);
		// add all saturdays
		LocalDate endDate = LocalDate.of(2099, 12, 31);
		LocalDate date = LocalDate.of(1950, 1, 7);
		while (date.isBefore(endDate))
		{
		  if (!workDays.Contains(date))
		  {
			holidays.Add(date);
		  }
		  date = date.plusDays(7);
		}
	  }

	  //-------------------------------------------------------------------------
	  // generate MXMC
	  // dates of published fixings - https://twitter.com/Banxico
	  // http://www.banxico.org.mx/SieInternet/consultarDirectorioInternetAction.do?accion=consultarCuadro&idCuadro=CF111&locale=en
	  // http://www.gob.mx/cms/uploads/attachment/file/161094/calendario_vacaciones2016.pdf
	  internal static ImmutableHolidayCalendar generateMexicoCity()
	  {
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  // constitution
		  holidays.Add(first(year, 2).with(firstInMonth(MONDAY)));
		  // president
		  holidays.Add(first(year, 3).with(firstInMonth(MONDAY)).plusWeeks(2));
		  // maundy thursday
		  holidays.Add(easter(year).minusDays(3));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // labour
		  holidays.Add(date(year, 5, 1));
		  // independence
		  holidays.Add(date(year, 9, 16));
		  // dead
		  holidays.Add(date(year, 11, 2));
		  // revolution
		  holidays.Add(first(year, 11).with(firstInMonth(MONDAY)).plusWeeks(2));
		  // guadalupe
		  holidays.Add(date(year, 12, 12));
		  // christmas
		  holidays.Add(date(year, 12, 25));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("MXMC"), holidays, SATURDAY, SUNDAY);
	  }

	  // generate BRBD
	  // a holiday in this calendar is only declared if there is a holiday in Sao Paulo, Rio de Janeiro and Brasilia
	  // http://www.planalto.gov.br/ccivil_03/leis/l0662.htm
	  // http://www.planalto.gov.br/ccivil_03/Leis/L6802.htm
	  // http://www.planalto.gov.br/ccivil_03/leis/2002/L10607.htm
	  internal static ImmutableHolidayCalendar generateBrazil()
	  {
		// base law is from 1949, reworded in 2002
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  // carnival
		  holidays.Add(easter(year).minusDays(48));
		  holidays.Add(easter(year).minusDays(47));
		  // tiradentes
		  holidays.Add(date(year, 4, 21));
		  // good friday
		  holidays.Add(easter(year).minusDays(2));
		  // labour
		  holidays.Add(date(year, 5, 1));
		  // corpus christi
		  holidays.Add(easter(year).plusDays(60));
		  // independence
		  holidays.Add(date(year, 9, 7));
		  // aparedica
		  if (year >= 1980)
		  {
			holidays.Add(date(year, 10, 12));
		  }
		  // dead
		  holidays.Add(date(year, 11, 2));
		  // republic
		  holidays.Add(date(year, 11, 15));
		  // christmas
		  holidays.Add(date(year, 12, 25));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("BRBD"), holidays, SATURDAY, SUNDAY);
	  }

	  // generate CZPR
	  // https://www.cnb.cz/en/public/media_service/schedules/media_svatky.html
	  internal static ImmutableHolidayCalendar generatePrague()
	  {
		// dates are fixed - no moving Sunday to Monday or similar
		IList<LocalDate> holidays = new List<LocalDate>(2000);
		for (int year = 1950; year <= 2099; year++)
		{
		  // new year
		  holidays.Add(date(year, 1, 1));
		  // good friday
		  if (year > 2015)
		  {
			holidays.Add(easter(year).minusDays(2));
		  }
		  // easter monday
		  holidays.Add(easter(year).plusDays(1));
		  // may day
		  holidays.Add(date(year, 5, 1));
		  // liberation from fascism
		  holidays.Add(date(year, 5, 8));
		  // cyril and methodius
		  holidays.Add(date(year, 7, 5));
		  // jan hus
		  holidays.Add(date(year, 7, 6));
		  // statehood
		  holidays.Add(date(year, 9, 28));
		  // republic
		  holidays.Add(date(year, 10, 28));
		  // freedom and democracy
		  holidays.Add(date(year, 11, 17));
		  // christmas eve
		  holidays.Add(date(year, 12, 24));
		  // christmas
		  holidays.Add(date(year, 12, 25));
		  // boxing
		  holidays.Add(date(year, 12, 26));
		}
		removeSatSun(holidays);
		return ImmutableHolidayCalendar.of(HolidayCalendarId.of("CZPR"), holidays, SATURDAY, SUNDAY);
	  }

	  //-------------------------------------------------------------------------
	  // date
	  private static LocalDate date(int year, int month, int day)
	  {
		return LocalDate.of(year, month, day);
	  }

	  // bump to following Monday
	  private static LocalDate bumpToMon(LocalDate date)
	  {
		if (date.DayOfWeek == SATURDAY)
		{
		  return date.plusDays(2);
		}
		else if (date.DayOfWeek == SUNDAY)
		{
		  return date.plusDays(1);
		}
		return date;
	  }

	  // bump Sunday to following Monday
	  private static LocalDate bumpSunToMon(LocalDate date)
	  {
		if (date.DayOfWeek == SUNDAY)
		{
		  return date.plusDays(1);
		}
		return date;
	  }

	  // bump to Saturday to Friday and Sunday to Monday
	  private static LocalDate bumpToFriOrMon(LocalDate date)
	  {
		if (date.DayOfWeek == SATURDAY)
		{
		  return date.minusDays(1);
		}
		else if (date.DayOfWeek == SUNDAY)
		{
		  return date.plusDays(1);
		}
		return date;
	  }

	  // christmas
	  private static LocalDate christmasBumpedSatSun(int year)
	  {
		LocalDate @base = LocalDate.of(year, 12, 25);
		if (@base.DayOfWeek == SATURDAY || @base.DayOfWeek == SUNDAY)
		{
		  return LocalDate.of(year, 12, 27);
		}
		return @base;
	  }

	  // christmas (if Christmas is Sunday, moved to Monday)
	  private static LocalDate christmasBumpedSun(int year)
	  {
		LocalDate @base = LocalDate.of(year, 12, 25);
		if (@base.DayOfWeek == SUNDAY)
		{
		  return LocalDate.of(year, 12, 26);
		}
		return @base;
	  }

	  // boxing day
	  private static LocalDate boxingDayBumpedSatSun(int year)
	  {
		LocalDate @base = LocalDate.of(year, 12, 26);
		if (@base.DayOfWeek == SATURDAY || @base.DayOfWeek == SUNDAY)
		{
		  return LocalDate.of(year, 12, 28);
		}
		return @base;
	  }

	  // boxing day (if Christmas is Sunday, boxing day moved from Monday to Tuesday)
	  private static LocalDate boxingDayBumpedSun(int year)
	  {
		LocalDate @base = LocalDate.of(year, 12, 26);
		if (@base.DayOfWeek == MONDAY)
		{
		  return LocalDate.of(year, 12, 27);
		}
		return @base;
	  }

	  // first of a month
	  private static LocalDate first(int year, int month)
	  {
		return LocalDate.of(year, month, 1);
	  }

	  // remove any holidays covered by Sat/Sun
	  private static void removeSatSun(IList<LocalDate> holidays)
	  {
		holidays.removeIf(date => date.DayOfWeek == SATURDAY || date.DayOfWeek == SUNDAY);
	  }

	  // apply bridging (Mon/Fri are holidays if Tue/Thu are)
	  private static void applyBridging(IList<LocalDate> holidays)
	  {
//JAVA TO C# CONVERTER TODO TASK: Most Java stream collectors are not converted by Java to C# Converter:
		ISet<LocalDate> additional1 = holidays.Where(date => date.DayOfWeek == TUESDAY && !MonthDay.from(date).Equals(MonthDay.of(1, 1))).Select(date => date.minusDays(1)).collect(toSet());
//JAVA TO C# CONVERTER TODO TASK: Most Java stream collectors are not converted by Java to C# Converter:
		ISet<LocalDate> additional2 = holidays.Where(date => date.DayOfWeek == THURSDAY && !MonthDay.from(date).Equals(MonthDay.of(12, 26))).Select(date => date.plusDays(1)).collect(toSet());
		((IList<LocalDate>)holidays).AddRange(additional1);
		((IList<LocalDate>)holidays).AddRange(additional2);
	  }

	  // calculate easter day by Delambre
	  internal static LocalDate easter(int year)
	  {
		int a = year % 19;
		int b = year / 100;
		int c = year % 100;
		int d = b / 4;
		int e = b % 4;
		int f = (b + 8) / 25;
		int g = (b - f + 1) / 3;
		int h = (19 * a + b - d - g + 15) % 30;
		int i = c / 4;
		int k = c % 4;
		int l = (32 + 2 * e + 2 * i - h - k) % 7;
		int m = (a + 11 * h + 22 * l) / 451;
		int month = (h + l - 7 * m + 114) / 31;
		int day = ((h + l - 7 * m + 114) % 31) + 1;
		return LocalDate.of(year, month, day);
	  }

	}

}