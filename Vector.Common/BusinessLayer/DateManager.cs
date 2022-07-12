

namespace Vector.Common.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Date Manager represents Date time related functionalities.
    /// </summary>
    public static class DateManager
    {
        #region Constants

        private const string TimeFormat = "yyyyMMddHHmmssffff";
        private const string FormatedTime = "{0} {1}";
        private const string Format = "g";

        #endregion

        /// <summary>
        /// Gets Time With Zone
        /// </summary>
        public static string GetTimeWithZone
        {
            get
            {
                TimeZone zone = TimeZone.CurrentTimeZone;

                //// Demonstrate ToLocalTime and ToUniversalTime.
                DateTime local = zone.ToLocalTime(DateTime.Now);

                return local.ToString() + " " + GetTimeZoneAbbrivation(zone.StandardName.ToString());
            }
        }

        /// <summary>
        /// Gets Last Date Of Last Month
        /// </summary>
        public static DateTime GetLastDateOfLastMonth
        {
            get
            {
                DateTime curDate = DateTime.Now;
                DateTime startDate = curDate.AddMonths(-1).AddDays(1 - curDate.Day);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                return endDate;
            }
        }

        #region Date

        #region Date Formating and Get Methods

        /// <summary>
        /// Date Range
        /// </summary>
        /// <param name="fromDate">from Date</param>
        /// <param name="toDate">to Date</param>
        /// <returns>returns date range</returns>
        public static IEnumerable<string> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1).Select(d => fromDate.AddDays(d).ToString("MM/dd/yyyy", CultureInfo.CurrentCulture));
        }



        /// <summary>
        /// Date Range with YYYY/MM/dd
        /// </summary>
        /// <param name="fromDate">from Date</param>
        /// <param name="toDate">to Date</param>
        /// <returns>returns date range</returns>
        public static IEnumerable<string> DateRangeformat(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1).Select(d => fromDate.AddDays(d).ToString("yyyy/MM/dd", CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// ListofMonths
        /// </summary>
        /// <param name="fromMonth"></param>
        /// <param name="fromYear"></param>
        /// <param name="toMonth"></param>
        /// <param name="toYear"></param>
        /// <returns></returns>
        public static List<Monthsdata> ListofMonths(string fromMonth, string fromYear, string toMonth, string toYear)
        {
            try
            {
                List<Monthsdata> listMonthsData = new List<Monthsdata>();

                int mosSt = Convert.ToInt32(fromMonth);
                int mosEn = Convert.ToInt32(toMonth);
                int yearSt = Convert.ToInt32(fromYear);
                int yearEn = Convert.ToInt32(toYear);
                string monthName = string.Empty;
                int monthNumber = 0;
                //if (mosSt != 1 && mosEn != 12 && fromYear != toYear)
                //{
                while (mosSt <= mosEn || yearSt < yearEn)
                {
                    Monthsdata monthsData = new Monthsdata();
                    var temp = new DateTime(yearSt, mosSt, 1);
                    monthName = temp.ToString("MMMM", CultureInfo.InvariantCulture).Substring(0, 3);
                    monthNumber = GetMonthNumber(temp.ToString("MMMM", CultureInfo.InvariantCulture));
                    monthsData.MonthName = monthName;
                    monthsData.MonthNumber = monthNumber;
                    monthsData.Year = yearSt;
                    listMonthsData.Add(monthsData);
                    if (yearSt == yearEn && mosSt == mosEn)
                        break;
                    mosSt++;
                    if (mosSt < 13) continue;
                    yearSt++;
                    mosSt = 1;
                }
                return listMonthsData;
                //}
                //return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<Monthsdata> MonthsBetween(DateTime startDate, DateTime endDate)
        {
            List<Monthsdata> listMonthsData = new List<Monthsdata>();
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                listMonthsData.Add(new Monthsdata() { MonthName = dateTimeFormat.GetMonthName(iterator.Month).Substring(0, 3), MonthNumber = iterator.Month, Year = iterator.Year });
                iterator = iterator.AddMonths(1);
            }
            return listMonthsData;
        }
        public class Monthsdata
        {
            public string MonthName { get; set; }
            public int MonthNumber { get; set; }
            public int Year { get; set; }
        }
        public class QuarterData
        {
            public string QuarterName { get; set; }
            public int QuarterYear { get; set; }
        }


        public static List<QuarterData> ListofQuartes(string dt1, string dt2)
        {
            List<QuarterData> listQuarterData = new List<QuarterData>();
            DateTime fromDate = Convert.ToDateTime(dt1);
            DateTime toDate = Convert.ToDateTime(dt2);

            int fromYearData = fromDate.Year;
            int toYearData = toDate.Year;

            int getNumberOfYears = toYearData - fromYearData;
            int actualYears = getNumberOfYears + 1;
            for (int i = 1; i <= actualYears; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    QuarterData qd = new QuarterData();
                    qd.QuarterName = "Q" + j;
                    qd.QuarterYear = fromYearData;
                    listQuarterData.Add(qd);
                }
                fromYearData++;
            }
            return listQuarterData;
        }

        /// <summary>
        /// Get Month Name based in Value, Getting Format like January -> Jan
        /// </summary>
        /// <param name="monthNum">month number</param>
        /// <returns>month name</returns>
        public static string GetMonthNameForGraphs(int monthNum)
        {
            if (monthNum < 1 || monthNum > 12)
            {
                throw new ArgumentOutOfRangeException("monthNum");
            }

            DateTime date = new DateTime(1, monthNum, 1);
            return date.ToString("MMMM", CultureInfo.CurrentCulture).Substring(0, 3);
        }

        /// <summary>
        /// Get Year Name For Graphs
        /// </summary>
        /// <param name="year">Year value</param>
        /// <returns>year name</returns>
        public static string GetYearNameForGraphs(int year)
        {
            return Convert.ToString(year, CultureInfo.CurrentCulture).Substring(2, 2);
        }

        /// <summary>
        /// generate File Name of Excel file with time stamp
        /// in the format Title_TimeStamp
        /// </summary>
        /// <param name="title">title value</param>
        /// <returns>updated title</returns>
        public static string GenerateTitleWithTimestamp(string title = "")
        {
            string value = title.Replace(" ", "_");
            StringBuilder fileName = new StringBuilder();
            fileName.Append(value);
            fileName.Append(VectorConstants.Underscore);
            fileName.Append(GetTimestamp(DateTime.Now));
            return fileName.ToString();
        }

        /// <summary>
        /// Gets the date time value from a string value.
        /// </summary>
        /// <param name="source">The string value.</param>
        /// <returns>Date time value or zero for error.</returns>
        public static DateTime GetDate(object source)
        {
            return GetDate(source, new DateTime(1901, 1, 1, 0, 0, 0));
        }

        /// <summary>
        /// Gets the date time value from a string value.
        /// </summary>
        /// <param name="month">month value</param>
        /// <param name="year">year value</param>
        /// <returns>date time</returns>
        public static DateTime GetDate(int month, int year)
        {
            return GetDate(new DateTime(year, month, 1, 0, 0, 0));
        }

        /// <summary>
        /// Gets the date time value from a string value.
        /// </summary>
        /// <param name="source">source value</param>
        /// <param name="defaultVal">default value</param>
        /// <returns>date time</returns>
        public static DateTime GetDate(object source, DateTime defaultVal)
        {
            DateTime val = defaultVal;
            if (DateTime.TryParse(source.ToString().Trim(), out val))
            {
                if (val == DateTime.MinValue)
                {
                    val = Convert.ToDateTime("01/01/1901 12:00:00", CultureInfo.CurrentCulture);
                }
            }

            return val;
        }

        /// <summary>
        /// Gets the date time value from a string value.
        /// </summary>
        /// <param name="date">The string value.</param>
        /// <returns>Date time value.</returns>
        public static string GetDateToShortString(DateTime date)
        {
            return date.ToString("MM/dd/yyyy", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Gets the date to short string.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Date time string value.</returns>
        public static string GetDateToShortString(string date)
        {
            return GetDate(date).ToString("MM/dd/yyyy", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Get min date
        /// </summary>
        /// <param name="dateValue">date Value</param>
        /// <returns>Date Time</returns>
        public static DateTime GetDateOrMinValue(string dateValue)
        {
            if (IsParseableDate(dateValue))
            {
                return DateTime.Parse(dateValue, CultureInfo.CurrentCulture);
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// get time stamp for rename file
        /// </summary>
        /// <param name="value">date Value</param>
        /// <returns>time stamp value</returns>
        public static string GetTimestamp(this DateTime value)
        {
            return value.ToString(TimeFormat, CultureInfo.CurrentCulture);
        }

        public static string GetDateTimeInCustomFormat(string dateTimeString, string existingFormat = TimeFormat, string newformat = TimeFormat)
        {
            return DateTime.ParseExact(dateTimeString, existingFormat, System.Globalization.CultureInfo.InvariantCulture).ToString(newformat);
        }

        /// <summary>
        /// Get the Minimum Date in the List if Dates
        /// </summary>
        /// <param name="listDates">list Dates</param>
        /// <returns>Date time</returns>
        public static DateTime GetMinDate(Collection<DateTime> listDates)
        {
            return listDates.Min();
        }

        /// <summary>
        /// get the Max Date in the given date List
        /// </summary>
        /// <param name="listDates">list Dates</param>
        /// <returns>Date Time</returns>
        public static DateTime GetMaxDate(Collection<DateTime> listDates)
        {
            return listDates.Max();
        }

        /// <summary>
        /// get the number of months between the From Date and ToDate
        /// </summary>
        /// <param name="from">from date</param>
        /// <param name="to">to date</param>
        /// <returns>month number</returns>
        public static int GetMonths(DateTime from, DateTime to)
        {
            if (from > to)
            {
                var temp = from;
                from = to;
                to = temp;
            }

            var months = VectorConstants.Zero;

            while (from <= to)
            {
                from = from.AddMonths(VectorConstants.One);
                months++;
            }

            return months - VectorConstants.One;
        }

        /// <summary>
        /// Get Months list
        /// </summary>
        /// <param name="fromMonth">from month</param>
        /// <param name="fromYear">from year</param>
        /// <param name="toMonth">to month</param>
        /// <param name="toYear">to year</param>
        /// <returns>month numbers</returns>
        public static int GetMonths(int fromMonth, int fromYear, int toMonth, int toYear)
        {
            DateTime from = GetDate(fromMonth, fromYear);
            DateTime to = GetLastDateOfMonth(toMonth, toYear);

            if (from > to)
            {
                var temp = from;
                from = to;
                to = temp;
            }

            var months = VectorConstants.Zero;

            while (from <= to)
            {
                from = from.AddMonths(VectorConstants.One);
                months++;
            }

            return months - VectorConstants.One;
        }

        /// <summary>
        /// Get number 
        /// </summary>
        /// <param name="from">from date</param>
        /// <param name="to">to date</param>
        /// <returns>days in a month</returns>
        public static int GetNumberOfDays(DateTime from, DateTime to)
        {
            TimeSpan ts = to - from;
            return ts.Days + VectorConstants.One;
        }

        /// <summary>
        /// Method to calculate total number of days between given dates
        /// </summary>
        /// <param name="fromMonth">from month</param>
        /// <param name="fromYear">from year</param>
        /// <param name="toMonth">to month</param>
        /// <param name="toYear">to year</param>
        /// <returns>number of days in last month</returns>
        public static int GetNumberOfDays(int fromMonth, int fromYear, int toMonth, int toYear)
        {
            DateTime sdate = Convert.ToDateTime(fromMonth + VectorConstants.StartingDay + fromYear, CultureInfo.CurrentCulture);
            DateTime edate = GetLastDateOfMonth(toMonth, toYear);
            TimeSpan ts = edate - sdate;
            int days = ts.Days;
            return days;
        }

        /// <summary>
        /// Get number 
        /// </summary>
        /// <param name="from">from date</param>
        /// <param name="to">to date</param>
        /// <returns>number of days in period</returns>
        public static int GetNumberOfDays(string from, string to)
        {
            DateTime fromDate = GetDate(from);
            DateTime toDate = GetDate(to);
            TimeSpan ts = toDate - fromDate;

            if (StringManager.IsEqual(Convert.ToString(ts.Days, CultureInfo.CurrentCulture), Convert.ToString(VectorConstants.MinusOne, CultureInfo.CurrentCulture)))
            {
                return ts.Days;
            }
            else
            {
                return ts.Days + VectorConstants.One;
            }
        }

        /// <summary>
        /// Get Last Day Of Month
        /// </summary>
        /// <param name="month">Month name</param>
        /// <param name="year">year value</param>
        /// <returns>last day in given month</returns>
        public static int GetLastDayOfMonth(int month, int year)
        {
            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);
            return lastDay.Day;
        }

        /// <summary>
        /// Get Last Date Of Month
        /// </summary>
        /// <param name="month">month name</param>
        /// <param name="year">year value</param>
        /// <returns>last date in given month</returns>
        public static DateTime GetLastDateOfMonth(int month, int year)
        {
            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);
            return lastDay;
        }

        /// <summary>
        /// Get First Date Of Next Month
        /// </summary>
        /// <param name="startDate">start Date</param>
        /// <returns>Date Time</returns>
        public static DateTime GetFirstDateOfNextMonth(DateTime startDate)
        {
            if (startDate.Month == 12)
            {
                startDate = new DateTime(startDate.Year + 1, 1, 1);
            }
            else
            {
                startDate = new DateTime(startDate.Year, startDate.Month + 1, 1);
            }

            return startDate;
        }

        /// <summary>
        /// Get First Date Of Current Month
        /// </summary>
        /// <param name="startDate">start Date</param>
        /// <returns>Date Time</returns>
        public static DateTime GetFirstDateOfCurrentMonth(DateTime startDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            return startDate;
        }

        /// <summary>
        /// get month name by selected month value.
        /// </summary>
        /// <param name="month">month name</param>
        /// <returns>month number</returns>
        public static int GetMonthNumber(string month)
        {
            int mName = 0;
            switch (month)
            {
                case "January":
                    mName = 1;
                    break;
                case "February":
                    mName = 2;
                    break;
                case "March":
                    mName = 3;
                    break;
                case "April":
                    mName = 4;
                    break;
                case "May":
                    mName = 5;
                    break;
                case "June":
                    mName = 6;
                    break;
                case "July":
                    mName = 7;
                    break;
                case "August":
                    mName = 8;
                    break;
                case "September":
                    mName = 9;
                    break;
                case "October":
                    mName = 10;
                    break;
                case "November":
                    mName = 11;
                    break;
                case "December":
                    mName = 12;
                    break;
            }

            return mName;
        }

        /// <summary>
        /// Get Month Number With Month Value Name
        /// </summary>
        /// <param name="value">date value</param>
        /// <param name="format">value format</param>
        /// <returns>month number</returns>
        public static int GetMonthNumberWithMonthValueName(string value, string format = "MMM")
        {
            return DateTime.ParseExact(value, format, CultureInfo.CurrentCulture).Month;
        }

        /// <summary>
        ///  is date valid
        /// </summary>
        /// <param name="datestring">date string</param>
        /// <returns>boolean value</returns>
        public static bool IsParseableDate(string datestring)
        {
            if (string.IsNullOrEmpty(datestring))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Assign first and current date of year 
        /// </summary>
        /// <param name="txtFromDate">text box</param>
        /// <param name="txtToDate">to date control</param>
        public static void AssignFirstAndCurrentDateOfYear(TextBox txtFromDate, TextBox txtToDate)
        {
            DateTime firstDayOfYear = new DateTime(Convert.ToInt32(DateTime.Now.Year), VectorConstants.One, VectorConstants.One);
            txtFromDate.Text = firstDayOfYear.ToShortDateString();
            txtToDate.Text = DateTime.Now.Date.ToShortDateString();
        }

        /// <summary>
        /// Assign Last 30 days from Current Date
        /// </summary>
        /// <param name="txtFromDate">from date</param>
        /// <param name="txtToDate">to date</param>
        public static void AssignLast30DaysFromToDate(TextBox txtFromDate, TextBox txtToDate)
        {
            DateTime firstDayOfYear = new DateTime(Convert.ToInt32(DateTime.Now.Year), VectorConstants.One, VectorConstants.One);
            txtFromDate.Text = DateTime.Now.Date.AddDays(-30).ToShortDateString();
            txtToDate.Text = DateTime.Now.Date.ToShortDateString();
        }

        /// <summary>
        /// Assign first and last date of year 
        /// </summary>
        /// <param name="txtFromDate">from date</param>
        /// <param name="txtToDate">to date</param>
        public static void AssignFirstAndLastDateOfYear(TextBox txtFromDate, TextBox txtToDate)
        {
            DateTime firstDayOfYear = new DateTime(Convert.ToInt32(DateTime.Now.Year), VectorConstants.One, VectorConstants.One);
            DateTime lastDayOfYear = new DateTime(Convert.ToInt32(DateTime.Now.Year), VectorConstants.Twelve, VectorConstants.ThirtyOne);
            txtFromDate.Text = firstDayOfYear.ToShortDateString();
            txtToDate.Text = DateTime.Now.Date.ToShortDateString();
        }

        /// <summary>
        /// Get Month Name based in Value
        /// </summary>
        /// <param name="monthNum">month number</param>
        /// <returns>month name</returns>
        public static string GetMonthName(int monthNum)
        {
            if (monthNum < 1 || monthNum > 12)
            {
                throw new ArgumentOutOfRangeException("monthNum");
            }

            DateTime date = new DateTime(1, monthNum, 1);
            return date.ToString("MMMM", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Get Month Number With Month Value Name
        /// </summary>
        /// <param name="month">month name</param>
        /// <returns>month value</returns>
        public static int GetMonthNumberWithMonthValueName(string month)
        {
            return DateTime.ParseExact(month, "MMM", CultureInfo.CurrentCulture).Month;
        }

        /// <summary>
        /// Get number of days in a month
        /// </summary>
        /// <param name="year">year value</param>
        /// <param name="month">month value</param>
        /// <returns>number of days in a month</returns>
        public static int NoOfDaysInAMonth(int year, int month)
        {
            return Convert.ToInt32(Convert.ToString(DateTime.DaysInMonth(year, month), CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Get Time Zone Abbreviation
        /// </summary>
        /// <param name="zoneName">zone Name</param>
        /// <returns>zone format</returns>
        public static string GetTimeZoneAbbrivation(string zoneName)
        {
            string zone = string.Empty;
            switch (zoneName)
            {
                case "India Standard Time":
                    zone = "IST";
                    break;
                case "Central Standard Time":
                    zone = "CST";
                    break;
                case "Pacific Standard Time":
                    zone = "PST";
                    break;
                case "Eastern Standard Time":
                    zone = "EST";
                    break;
            }

            return zone;
        }

        public static string GetYearStartDate(string year)
        {
            if (!string.IsNullOrEmpty(year))
                return ("01/01/" + year);
            else
                return string.Empty;
        }

        public static string GetYearEndDate(string year)
        {
            if (!string.IsNullOrEmpty(year))
                return ("12/31/" + year);
            else
                return string.Empty;
        }

        #endregion

        #region Date Validations

        /// <summary>
        /// Is Valid Periods
        /// </summary>
        /// <param name="periodTuple">period Topple</param>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date</param>
        /// <returns>zero or one</returns>
        public static int IsValidPeriods(Collection<Tuple<DateTime, DateTime>> periodTuple, DateTime? fromDate = null, DateTime? toDate = null)
        {
            ////Step 1 : First from date and last to dates should be same 
            ////Step 2 : Dates should not over lap

            ////Step 1 : First from date and last to dates should be same 
            if (!IsFirstAndLastDateSame(periodTuple, fromDate, toDate))
            {
                return VectorConstants.One;
            }

            ////Step 2 : Dates should not over lap
            if (Overlap(periodTuple))
            {
                return VectorConstants.Two; ////use constant manager 
            }

            return VectorConstants.Zero;
        }

        /// <summary>
        /// Is First And Last Date Same
        /// </summary>
        /// <param name="ranges">date ranges</param>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date</param>
        /// <returns>boolean value</returns>
        public static bool IsFirstAndLastDateSame(Collection<Tuple<DateTime, DateTime>> ranges, DateTime? fromDate = null, DateTime? toDate = null)
        {
            if (ranges.Count > VectorConstants.Zero)
            {
                if (ranges[VectorConstants.Zero].Item1.Equals(fromDate) && ranges[ranges.Count - VectorConstants.One].Item2.Equals(toDate))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Over lap
        /// </summary>
        /// <param name="ranges">date ranges</param>
        /// <returns>boolean value</returns>
        public static bool Overlap(Collection<Tuple<DateTime, DateTime>> ranges)
        {
            for (int i = VectorConstants.Zero; i < ranges.Count; i++)
            {
                for (int j = i + VectorConstants.One; j < ranges.Count; j++)
                {
                    if (ranges[i].Item1 <= ranges[j].Item2 && ranges[i].Item2 >= ranges[j].Item1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Is Dates Over lap
        /// </summary>
        /// <param name="periodTuple">period Tuple</param>
        /// <returns>boolean value</returns>
        public static bool IsDatesOverlap(Collection<Tuple<DateTime, DateTime>> periodTuple)
        {
            if (Overlap(periodTuple))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Is Period From Less Than To Date
        /// </summary>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date</param>
        /// <returns>boolean value</returns>
        public static bool IsPeriodFromLessThanToDate(DateTime fromDate, DateTime toDate)
        {
            if (DateTime.Compare(fromDate, toDate) < VectorConstants.Zero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Is Period From Less Than To Date
        /// </summary>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date</param>
        /// <returns>boolean value</returns>
        public static bool IsDateExistsBetweenDates(DateTime fromDate, DateTime toDate)
        {
            DateTime dateTocheck = DateTime.Now;
            if (dateTocheck >= fromDate && dateTocheck <= toDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidateMaxLengthOfPeriods(DateTime fromDate, DateTime toDate, int maxDaysLength = 366, int minDaysLength = 31)
        {
            int gpp = DateManager.GetNumberOfDays(fromDate, toDate);

            if (gpp <= maxDaysLength)
                return true;
            else
                return false;
        }

        public static string ValidatePeriod(DateTime fromDate, DateTime toDate, int maxDaysinPeriod, string periodTitle = "")
        {
            if (IsPeriodFromLessThanToDate(fromDate, toDate))
            {
                if (ValidateMaxLengthOfPeriods(fromDate, toDate, maxDaysinPeriod))
                {
                    return string.Empty;
                }
                else
                {
                    return "Number of days should not more then " + maxDaysinPeriod.ToString() + " for period " + periodTitle;
                }
            }
            else
            {
                return "Form date must be less then to date for period " + periodTitle;
            }
        }

        public static string ValidatePeriodYear(string fromYear, string toYear, int maxYearsinPeriod = 3, string periodTitle = "P1")
        {
            int fYear = Convert.ToInt32(fromYear);
            int tYear = Convert.ToInt32(toYear);
            if (fYear < tYear) // 2013-2014
            {
                int yearDiff = tYear - fYear;
                if (yearDiff + 1 <= maxYearsinPeriod)
                {
                    return string.Empty;
                }
                else
                {
                    return "Year difference should not more then " + maxYearsinPeriod + " for period " + periodTitle;
                }
            }
            else
            {
                return "From year should be less than to year for period " + periodTitle;
            }
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Converts the string to24 hour.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>time value</returns>
        public static int ConvertStringTo24Hour(string time)
        {
            string formatedTime = string.Format(CultureInfo.CurrentCulture, FormatedTime, DateTime.Now.ToShortDateString(), time);
            DateTime dt = DateTime.ParseExact(formatedTime, Format, CultureInfo.CurrentCulture);
            return StringManager.GetNumberValue(dt.ToString("HHmm", CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Converts the string to12 hour.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>date time</returns>
        public static string ConvertStringTo12Hour(string time)
        {
            string paddedTime = time.PadLeft(VectorConstants.Four, '0');
            string formatedTime = string.Format(CultureInfo.CurrentCulture, "{0} {1}:{2}", DateTime.Now.ToShortDateString(), paddedTime.Substring(VectorConstants.Zero, VectorConstants.Two), paddedTime.Substring(VectorConstants.Two, VectorConstants.Two));
            DateTime dt = DateTime.Parse(formatedTime, CultureInfo.CurrentCulture);
            return dt.ToString("t", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Get Month Number From Date Time 
        /// </summary>
        /// <param name="date"> Date Time</param>
        /// <returns> Month Number Form Date Time</returns>
        public static string GetMonth(DateTime date)
        {
            return (date.Month).ToString();
        }

        /// <summary>
        /// Get Year form Date
        /// </summary>
        /// <param name="date">Date Time</param>
        /// <returns>Year for Date Time</returns>
        public static string GetYear(DateTime date)
        {
            return (date.Year).ToString();
        }

        #endregion

        #endregion

        #region WeekDays

        /// <summary>
        /// Get Number of Tuesdays /Thurs days etc from the given from and to dates
        /// </summary>
        /// <param name="fromDate">From date eg: "07/01/2014"</param>
        /// <param name="toDate">To date eg: "07/31/2014"</param>
        /// <param name="weekDay">Day of the week to check eg: "Tue" (First Three letters of the Week)</param>
        /// <returns>count number of occurrences eg: 3</returns>
        public static int GetNumberOfWeekDays(DateTime fromDate, DateTime toDate, string weekDay)
        {
            TimeSpan timeSpan = toDate - fromDate;                       // Total duration
            int count = (int)Math.Floor(timeSpan.TotalDays / 7);   // Number of whole weeks
            int remainder = (int)(timeSpan.TotalDays % 7);         // Number of remaining days


            int sinceLastDay = (int)(toDate.DayOfWeek - GetWeekDay(weekDay));   // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]
            if (remainder >= sinceLastDay) count++;


            return count;
        }

        /// <summary>
        /// Get number of times week day occurrences bi-weekly
        /// </summary>
        /// <param name="fromDate">From date eg: "07/01/2014"</param>
        /// <param name="toDate">To date eg: "07/31/2014"</param>
        /// <param name="weekDay">Day of the week to check eg: "Tue" (First Three letters of the Week)</param>
        /// <returns>count number of occurrences eg: 3</returns>
        public static int GetNumberOfBiWeekDays(DateTime fromDate, DateTime toDate, string weekDay)
        {
            //number of occurrences bi-weekly
            int result = VectorConstants.Zero;

            //Get Time span
            TimeSpan numberOfBiWeekly = (toDate - fromDate);

            //Get Remaining days left 
            int remainder = (int)(numberOfBiWeekly.TotalDays % VectorConstants.Fourteen);

            //Get Total Time a week occurs
            int totalbiWeekLyDays = (int)Math.Ceiling((numberOfBiWeekly.TotalDays) / VectorConstants.Fourteen);


            //If the Remainder is Zero then no occurrences else remove 1 from the total
            if (remainder > VectorConstants.Zero)
                totalbiWeekLyDays--;

            //Copy from Date
            DateTime from = fromDate;

            //loop through each 14 days and get 1 week day for each loop
            for (int i = VectorConstants.Zero; i < totalbiWeekLyDays; i++)
            {
                int totalWkDays = GetNumberOfWeekDays(from, from.AddDays(VectorConstants.Fourteen), weekDay);
                if (totalWkDays > VectorConstants.Zero)
                    result++;

                from = from.AddDays(VectorConstants.Fourteen);
            }

            // based on remainder get if the week day exists
            if (remainder > VectorConstants.Zero)
            {
                int totalWkDays = GetNumberOfWeekDays(from, from.AddDays(remainder), weekDay);
                if (totalWkDays > VectorConstants.Zero)
                    result++;
            }

            //return number of occurrences
            return result;
        }

        /// <summary>
        /// Get Week Day Based on the text "Sun" / Mon etc
        /// </summary>
        /// <param name="weekDay">First three leters of the week day eg: "Sun" </param>
        /// <returns> returns DayOfWeek Monday</returns>
        private static DayOfWeek GetWeekDay(string weekDay)
        {
            if (StringManager.IsEqual(weekDay, "Mon"))
                return DayOfWeek.Monday;
            else if (StringManager.IsEqual(weekDay, "Tue"))
                return DayOfWeek.Tuesday;
            else if (StringManager.IsEqual(weekDay, "Wed"))
                return DayOfWeek.Wednesday;
            else if (StringManager.IsEqual(weekDay, "Thu"))
                return DayOfWeek.Thursday;
            else if (StringManager.IsEqual(weekDay, "Fri"))
                return DayOfWeek.Friday;
            else if (StringManager.IsEqual(weekDay, "Sat"))
                return DayOfWeek.Saturday;
            else
                return DayOfWeek.Sunday;
        }

        #endregion



        #region Getting Years

        /// <summary>
        ///  Getting Years List
        /// </summary>
        /// <param name="YearCount"></param>
        /// <param name="Type"></param>
        /// <param name="Count"></param>
        /// <param name="FeatureId"></param>
        /// <returns></returns>
        public static object ListofYears(int yearCount, string type, int Count, string FeatureId, bool withCurrentyear, bool last3years)
        {
            string result = string.Empty;
            try
            {
                List<int> listofyears = new List<int>();
                int currentyear = DateTime.Now.Year;
                if (StringManager.IsEqual(type.ToUpper(), VectorConstants.pastyear.ToUpper()))
                {
                    if (withCurrentyear && !last3years)
                    {
                        for (int i = currentyear - Convert.ToInt32(yearCount); i <= currentyear; i++)
                        {
                            listofyears.Add(i);
                        }
                    }
                    else if (last3years)
                    {
                        for (int i = currentyear - 2; i <= currentyear; i++)
                        {
                            listofyears.Add(i);
                        }
                    }
                    else
                    {
                        for (int i = currentyear - Convert.ToInt32(yearCount); i < currentyear; i++)
                        {
                            listofyears.Add(i);
                        }
                    }
                }
                else if (StringManager.IsEqual(type.ToUpper(), VectorConstants.Pastandfutureyear.ToUpper()))
                {
                    for (int i = currentyear - yearCount; i <= currentyear + Count; i++)
                    {
                        listofyears.Add(i);
                    }

                }
                else if (StringManager.IsEqual(type.ToUpper(), VectorConstants.futureyear.ToUpper()))
                {
                    for (int i = currentyear; i <= currentyear + Count; i++)
                    {
                        listofyears.Add(i);
                    }
                }
                return listofyears.OrderByDescending(p => p).ToList();

            }
            catch (Exception ex)
            {
                return result;
            }
        }
        #endregion;
    }
}