//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ML.Core
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public class DateTimeHelper
    {
        private DateTime dt = DateTime.Now;

        /// <summary>
        /// 当前日期
        /// </summary>
        /// <returns></returns>
        public static string GetToday()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 当前日期自定义格式
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetToday(string format)
        {
            return DateTime.Now.ToString(format);
        }
        /// <summary>
        /// 当前日期 加添加，减天数 -1、1
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetDate(int i)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(i);
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取上个月
        /// </summary>
        /// <returns></returns>
        public static string GetLastMonth()
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddMonths(-1);
            return dt.ToString("yyyy-MM");
        }

        /// <summary>
        /// 获取指定月份的上个月
        /// </summary>
        /// <param name="current_month"></param>
        /// <returns></returns>
        public static string GetLastMonth(string current_month)
        {
            DateTime dt = DateTime.Parse($"{current_month}-01");
            dt = dt.AddMonths(-1);
            return dt.ToString("yyyy-MM");
        }

        public static string GetNumberWeekDay(DateTime dt)
        {
            int y = dt.Year;
            int m = dt.Month;
            int d = dt.Day;
            if (m < 3)
            {
                m += 12;
                y--;
            }
            if (y % 400 == 0 || y % 100 != 0 && y % 4 == 0)
                d--;
            else
                d += 1;
            int val = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
            return val.ToString();
        }

        public string GetChineseWeekDay(int y, int m, int d)
        {
            string[] weekstr = { "日", "一", "二", "三", "四", "五", "六" };
            if (m < 3)
            {
                m += 12;
                y--;
            }
            if (y % 400 == 0 || y % 100 != 0 && y % 4 == 0)
                d--;
            else
                d += 1;
            return "星期" + weekstr[(d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7];
        }

        /// <summary>
        /// 读取时间戳
        /// </summary>
        /// <returns></returns>
        public long TimeSpan()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// 读取当前日期加减n天的时间戳
        /// </summary>
        /// <returns></returns>
        public long DelayTimeSpan(int i)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(i);
            TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        #region 返回本年有多少天

        /// <summary>返回本年有多少天</summary>
        /// <param name="iYear">年份</param>
        /// <returns>本年的天数</returns>
        public static int GetDaysOfYear(int iYear)
        {
            return IsRuYear(iYear) ? 366 : 365;
        }

        /// <summary>本年有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>本天在当年的天数</returns>
        public static int GetDaysOfYear(DateTime dt)
        {
            return IsRuYear(dt.Year) ? 366 : 365;
        }

        #endregion

        #region 返回本月有多少天
        /// <summary>本月有多少天</summary>
        /// <param name="iYear">年</param>
        /// <param name="Month">月</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(int iYear, int Month)
        {
            var days = 0;
            switch (Month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    days = IsRuYear(iYear) ? 29 : 28;
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }

            return days;
        }


        /// <summary>本月有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(DateTime dt)
        {
            //--------------------------------//
            //--从dt中取得当前的年，月信息  --//
            //--------------------------------//
            int days = 0;
            int year = dt.Year;
            int month = dt.Month;

            //--利用年月信息，得到当前月的天数信息。
            switch (month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    days = IsRuYear(year) ? 29 : 28;
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }
            return days;
        }
        #endregion

        #region 返回当前日期的 （星期名称or星期编号）
        /// <summary>返回当前日期的星期名称</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期名称</returns>
        public static string GetWeekNameOfDay(DateTime dt)
        {
            string week = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;
            }
            return week;
        }


        /// <summary>返回当前日期的星期编号</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期数字编号</returns>
        public static int GetWeekNumberOfDay(DateTime dt)
        {
            int week = 0;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = 1;
                    break;
                case DayOfWeek.Tuesday:
                    week = 2;
                    break;
                case DayOfWeek.Wednesday:
                    week = 3;
                    break;
                case DayOfWeek.Thursday:
                    week = 4;
                    break;
                case DayOfWeek.Friday:
                    week = 5;
                    break;
                case DayOfWeek.Saturday:
                    week = 6;
                    break;
                case DayOfWeek.Sunday:
                    week = 7;
                    break;
            }
            return week;
        }
        #endregion

        #region 获取某一年有多少周
        /// <summary>
        /// 获取某一年有多少周
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>该年周数</returns>
        public static int GetWeekAmount(int year)
        {
            var end = new DateTime(year, 12, 31); //该年最后一天
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //该年星期数
        }
        #endregion

        #region 获取某一日期是该年中的第几周
        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>该日期在该年中的周数</returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 返回年度第几个星期
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="week">一周的开始日期</param>
        /// <returns></returns>
        public static int WeekOfYear(DateTime date, DayOfWeek week)
        {
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(date, CalendarWeekRule.FirstDay, week);
        }
        #endregion

        #region 根据某年的第几周获取这周的起止日期
        /// <summary>
        /// 根据某年的第几周获取这周的起止日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOrder"></param>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public static void WeekRange(int year, int weekOrder, ref DateTime firstDate, ref DateTime lastDate)
        {
            //当年的第一天
            var firstDay = new DateTime(year, 1, 1);

            //当年的第一天是星期几
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);

            //计算当年第一周的起止日期，可能跨年
            int dayDiff = (-1) * firstOfWeek + 1;
            int dayAdd = 7 - firstOfWeek;

            firstDate = firstDay.AddDays(dayDiff).Date;
            lastDate = firstDay.AddDays(dayAdd).Date;

            //如果不是要求计算第一周
            if (weekOrder != 1)
            {
                int addDays = (weekOrder - 1) * 7;
                firstDate = firstDate.AddDays(addDays);
                lastDate = lastDate.AddDays(addDays);
            }
        }

        /// <summary>
        /// 得到一年中的某周的起始日和截止日
        /// 年 nYear
        /// 周数 nNumWeek
        /// 周始 out dtWeekStart
        /// 周终 out dtWeekeEnd
        /// </summary>
        /// <param name="nYear">年份</param>
        /// <param name="nNumWeek">第几周</param>
        /// <param name="dtWeekStart">开始日期</param>
        /// <param name="dtWeekeEnd">结束日期</param>
        public static void GetWeekTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            DateTime dt = new DateTime(nYear, 1, 1);
            dt = dt + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Monday);
            dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1);
        }

        /// <summary>
        /// 得到一年中的某周的起始日和截止日    周一到周五  工作日
        /// </summary>
        /// <param name="nYear">年份</param>
        /// <param name="nNumWeek">第几周</param>
        /// <param name="dtWeekStart">开始日期</param>
        /// <param name="dtWeekeEnd">结束日期</param>
        public static void GetWeekWorkTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            DateTime dt = new DateTime(nYear, 1, 1);
            dt = dt + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Monday);
            dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1).AddDays(-2);
        }
        #endregion

        #region 返回两个日期之间相差的天数
        /// <summary>
        /// 返回2个时间的时间差
        /// </summary>
        public static TimeSpan DiffDateTime(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto - dtfrm;
            return tsDiffer;
        }

        /// <summary>
        /// 返回两个日期之间相差的天数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>天数</returns>
        public static int DiffDays(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto - dtfrm;
            return tsDiffer.Days;
        }


        /// <summary>
        /// 返回两个日期之间相差的小时
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>小时</returns>
        public static int DiffHours(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto - dtfrm;
            return tsDiffer.Hours;
        }

        /// <summary>
        /// 返回两个日期之间相差的分钟
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>分钟</returns>
        public static int DiffMinutes(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto - dtfrm;
            return tsDiffer.Minutes;
        }

        /// <summary>
        /// 返回两个日期之间相差的秒数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>秒数</returns>
        public static int DiffSecond(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto - dtfrm;
            return tsDiffer.Seconds;
        }

        /// <summary>
        /// 返回两个日期之间相差的毫秒数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>毫秒数</returns>
        public static int DiffMillSecond(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto - dtfrm;
            return tsDiffer.Milliseconds;
        }
        #endregion

        #region 判断当前年份是否是闰年
        /// <summary>判断当前年份是否是闰年，私有函数</summary>
        /// <param name="iYear">年份</param>
        /// <returns>是闰年：True ，不是闰年：False</returns>
        private static bool IsRuYear(int iYear)
        {
            //形式参数为年份
            //例如：2003
            int n = iYear;
            return (n % 400 == 0) || (n % 4 == 0 && n % 100 != 0);
        }
        #endregion

        #region 将输入的字符串转化为日期。如果字符串的格式非法，则返回当前日期
        /// <summary>
        /// 将输入的字符串转化为日期。如果字符串的格式非法，则返回当前日期。
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>日期对象</returns>
        public static DateTime ToDate(string strInput)
        {
            DateTime oDateTime;

            try
            {
                oDateTime = DateTime.Parse(strInput);
            }
            catch (Exception)
            {
                oDateTime = DateTime.Today;
            }

            return oDateTime;
        }
        #endregion

        #region 将日期对象转化为格式字符串
        /// <summary>
        /// 将日期对象转化为格式字符串
        /// </summary>
        /// <param name="oDateTime">日期对象</param>
        /// <param name="strFormat">
        /// 格式：
        ///		"SHORTDATE"===短日期
        ///		"LONGDATE"==长日期
        ///		其它====自定义格式
        /// </param>
        /// <returns>日期字符串</returns>
        public static string ToString(DateTime oDateTime, string strFormat)
        {
            string strDate;

            try
            {
                switch (strFormat.ToUpper())
                {
                    case "SHORTDATE":
                        strDate = oDateTime.ToShortDateString();
                        break;
                    case "LONGDATE":
                        strDate = oDateTime.ToLongDateString();
                        break;
                    default:
                        strDate = oDateTime.ToString(strFormat);
                        break;
                }
            }
            catch (Exception)
            {
                strDate = oDateTime.ToShortDateString();
            }

            return strDate;
        }
        #endregion

        #region 时间、时间戳互转
        /// <summary>
        /// DateTime转换为JavaScript时间戳
        /// </summary>
        /// <returns></returns>
        public static long ConvertToJavaScriptUnix(global::System.DateTime? time = null)
        {
            if (time == null)
            {
                time = global::System.DateTime.Now;
            }
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new global::System.DateTime(1970, 1, 1)); // 当地时区
            return (long)(time.Value - startTime).TotalMilliseconds; // 相差毫秒数
        }

        /// <summary>
        /// JavaScript时间戳转换为DateTime
        /// </summary>
        /// <param name="jsTimeStamp">JavaScript时间戳</param>
        /// <returns></returns>
        public static global::System.DateTime JavaScriptUnixConvertToDateTime(long jsTimeStamp)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new global::System.DateTime(1970, 1, 1)); // 当地时区
            return startTime.AddMilliseconds(jsTimeStamp);
        }

        /// <summary>
        /// DateTime转换为时间戳
        /// </summary>
        /// <returns></returns>
        public static long ConvertToUnix(global::System.DateTime? time = null)
        {
            if (time == null)
            {
                time = global::System.DateTime.Now;
            }
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new global::System.DateTime(1970, 1, 1)); // 当地时区
            return (long)(time.Value - startTime).TotalSeconds; // 相差秒数
        }

        /// <summary>
        /// 时间戳转换为DateTime
        /// </summary>
        /// <param name="unixTimeStamp">时间戳</param>
        /// <returns></returns>
        public static global::System.DateTime UnixConvertToDateTime(long unixTimeStamp)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new global::System.DateTime(1970, 1, 1)); // 当地时区
            return startTime.AddSeconds(unixTimeStamp);
        }
        #endregion

        #region P/Invoke 设置本地时间

        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME time);

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        /// <summary>
        /// 设置本地计算机时间
        /// </summary>
        /// <param name="dt">DateTime对象</param>
        public static void SetLocalTime(DateTime dt)
        {
            SYSTEMTIME st;

            st.year = (short)dt.Year;
            st.month = (short)dt.Month;
            st.dayOfWeek = (short)dt.DayOfWeek;
            st.day = (short)dt.Day;
            st.hour = (short)dt.Hour;
            st.minute = (short)dt.Minute;
            st.second = (short)dt.Second;
            st.milliseconds = (short)dt.Millisecond;

            SetLocalTime(ref st);
        }

        #endregion

        #region 获取网络时间
        ///// <summary>
        ///// 获取中国国家授时中心网络服务器时间发布的当前时间
        ///// </summary>
        ///// <returns></returns>
        //public static DateTime GetChineseDateTime()
        //{
        //  DateTime res = DateTime.MinValue;
        //  try
        //  {
        //    string url = "http://www.time.ac.cn/stime.asp";
        //    HttpHelper helper = new HttpHelper();
        //    helper.Encoding = Encoding.Default;
        //    string html = helper.GetHtml(url);
        //    string patDt = @"\d{4}年\d{1,2}月\d{1,2}日";
        //    string patHr = @"hrs\s+=\s+\d{1,2}";
        //    string patMn = @"min\s+=\s+\d{1,2}";
        //    string patSc = @"sec\s+=\s+\d{1,2}";
        //    Regex regDt = new Regex(patDt);
        //    Regex regHr = new Regex(patHr);
        //    Regex regMn = new Regex(patMn);
        //    Regex regSc = new Regex(patSc);

        //    res = DateTime.Parse(regDt.Match(html).Value);
        //    int hr = GetInt(regHr.Match(html).Value, false);
        //    int mn = GetInt(regMn.Match(html).Value, false);
        //    int sc = GetInt(regSc.Match(html).Value, false);
        //    res = res.AddHours(hr).AddMinutes(mn).AddSeconds(sc);
        //  }
        //  catch { }
        //  return res;
        //}

        /// <summary>
        /// 从指定的字符串中获取整数
        /// </summary>
        /// <param name="origin">原始的字符串</param>
        /// <param name="fullMatch">是否完全匹配，若为false，则返回字符串中的第一个整数数字</param>
        /// <returns>整数数字</returns>
        private static int GetInt(string origin, bool fullMatch)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return 0;
            }
            origin = origin.Trim();
            if (!fullMatch)
            {
                string pat = @"-?\d+";
                Regex reg = new Regex(pat);
                origin = reg.Match(origin.Trim()).Value;
            }
            int res = 0;
            int.TryParse(origin, out res);
            return res;
        }
        #endregion

        #region 类实例方法
        public DateTimeHelper()
        {
        }
        public DateTimeHelper(DateTime dateTime)
        {
            dt = dateTime;
        }
        public DateTimeHelper(string dateTime)
        {
            dt = DateTime.Parse(dateTime);
        }
        /// <summary>
        /// 哪天
        /// </summary>
        /// <param name="days">7天前:-7 7天后:+7</param>
        /// <returns></returns>
        public string GetTheDay(int? days)
        {
            int day = days ?? 0;
            return dt.AddDays(day).ToShortDateString();
        }

        /// <summary>
        /// 周日
        /// </summary>
        /// <param name="weeks">上周-1 下周+1 本周0</param>
        /// <returns></returns>
        public string GetSunday(int? weeks)
        {
            int week = weeks ?? 0;
            return dt.AddDays(Convert.ToDouble((0 - Convert.ToInt16(dt.DayOfWeek))) + 7 * week).ToShortDateString();
        }
        /// <summary>
        /// 周六
        /// </summary>
        /// <param name="weeks">上周-1 下周+1 本周0</param>
        /// <returns></returns>
        public string GetSaturday(int? weeks)
        {
            int week = weeks ?? 0;
            return dt.AddDays(Convert.ToDouble((6 - Convert.ToInt16(dt.DayOfWeek))) + 7 * week).ToShortDateString();
        }
        /// <summary>
        /// 月第一天
        /// </summary>
        /// <param name="months">上月-1 本月0 下月1</param>
        /// <returns></returns>
        public string GetFirstDayOfMonth(int? months)
        {
            int month = months ?? 0;
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(month).ToShortDateString();
        }
        /// <summary>
        /// 月最后一天
        /// </summary>
        /// <param name="months">上月0 本月1 下月2</param>
        /// <returns></returns>
        public string GetLastDayOfMonth(int? months)
        {
            int month = months ?? 0;
            return DateTime.Parse(dt.ToString("yyyy-MM-01")).AddMonths(month).AddDays(-1).ToShortDateString();
        }
        /// <summary>
        /// 年度第一天
        /// </summary>
        /// <param name="years">上年度-1 下年度+1</param>
        /// <returns></returns>
        public string GetFirstDayOfYear(int? years)
        {
            int year = years ?? 0;
            return DateTime.Parse(dt.ToString("yyyy-01-01")).AddYears(year).ToShortDateString();
        }
        /// <summary>
        /// 年度最后一天
        /// </summary>
        /// <param name="years">上年度0 本年度1 下年度2</param>
        /// <returns></returns>
        public string GetLastDayOfYear(int? years)
        {
            int year = years ?? 0;
            return DateTime.Parse(dt.ToString("yyyy-01-01")).AddYears(year).AddDays(-1).ToShortDateString();
        }
        /// <summary>
        /// 季度第一天
        /// </summary>
        /// <param name="quarters">上季度-1 下季度+1</param>
        /// <returns></returns>
        public string GetFirstDayOfQuarter(int? quarters)
        {
            int quarter = quarters ?? 0;
            return dt.AddMonths(quarter * 3 - ((dt.Month - 1) % 3)).ToString("yyyy-MM-01");
        }
        /// <summary>
        /// 季度最后一天
        /// </summary>
        /// <param name="quarters">上季度0 本季度1 下季度2</param>
        /// <returns></returns>
        public string GetLastDayOfQuarter(int? quarters)
        {
            int quarter = quarters ?? 0;
            return DateTime.Parse(dt.AddMonths(quarter * 3 - ((dt.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
        }
        /// <summary>
        /// 中文星期
        /// </summary>
        /// <returns></returns>
        public string GetDayOfWeekCN()
        {
            string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return Day[Convert.ToInt16(dt.DayOfWeek)];
        }
        /// <summary>
        /// 获取星期数字形式,周一开始
        /// </summary>
        /// <returns></returns>
        public int GetDayOfWeekNum()
        {
            int day = (Convert.ToInt16(dt.DayOfWeek) == 0) ? 7 : Convert.ToInt16(dt.DayOfWeek);
            return day;
        }
        #endregion

        #region 其他转换静态方法
        /// <summary>
        /// C#的时间到Javascript的时间的转换
        /// </summary>
        /// <param name="TheDate"></param>
        /// <returns></returns>
        public static long ConvertTimeToJS(DateTime TheDate)
        {
            //string time = (System.DateTime.Now.Subtract(Convert.ToDateTime("1970-01-01 8:0:0"))).TotalMilliseconds.ToString();
            //long d = MilliTimeStamp(DateTime.Now);

            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = TheDate.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// PHP的时间转换成C#中的DateTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ConvertPHPToTime(long time)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long t = (time + 8 * 60 * 60) * 10000000 + timeStamp.Ticks;
            DateTime dt = new DateTime(t);
            return dt;
        }

        /// <summary>
        ///  C#中的DateTime转换成PHP的时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ConvertTimeToPHP(DateTime time)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;  //注意这里有时区问题，用now就要减掉8个小时
            return a;
        }

        public static string GetDiffTime(DateTime beginTime, DateTime endTime)
        {
            int i = 0;
            return GetDiffTime(beginTime, endTime, ref i);
        }

        /// <summary>
        /// 计算2个时间差
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static string GetDiffTime(DateTime beginTime, DateTime endTime, ref int mindTime)
        {
            string strResout = string.Empty;
            //获得2时间的时间间隔秒计算
            //TimeSpan span = endTime - beginTime;
            TimeSpan span = endTime.Subtract(beginTime);

            int iTatol = Convert.ToInt32(span.TotalSeconds);
            int iMinutes = 1 * 60;
            int iHours = iMinutes * 60;
            int iDay = iHours * 24;
            int iMonth = iDay * 30;
            int iYear = iMonth * 12;

            //提醒时间,到了返回1,否则返回0
            if (mindTime > iTatol && iTatol > 0)
            {
                mindTime = 1;
            }
            else
            {
                mindTime = 0;
            }

            if (iTatol > iYear)
            {
                strResout += iTatol / iYear + "年";
                iTatol = iTatol % iYear;//剩余
            }
            if (iTatol > iMonth)
            {
                strResout += iTatol / iMonth + "月";
                iTatol = iTatol % iMonth;
            }
            if (iTatol > iDay)
            {
                strResout += iTatol / iDay + "天";
                iTatol = iTatol % iDay;

            }
            if (iTatol > iHours)
            {
                strResout += iTatol / iHours + "小时";
                iTatol = iTatol % iHours;
            }
            if (iTatol > iMinutes)
            {
                strResout += iTatol / iMinutes + "分";
                iTatol = iTatol % iMinutes;
            }
            strResout += iTatol + "秒";

            return strResout;
        }
        #endregion

        #region GMT时间转换
        /// <summary>  
        /// GMT时间转成本地时间  
        /// </summary>  
        /// <param name="gmt">字符串形式的GMT时间</param>  
        /// <returns></returns>  
        public static DateTime GMT2Local(string gmt)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                string pattern = "";
                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd-MMM-yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd-MMM-yyyy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "")
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else
                {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch
            {
            }
            return dt;
        }
        #endregion

        /// <summary>  
        /// 广联达专用：DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 是否到期
        /// </summary>
        /// <param name="expireDate"></param>
        /// <returns></returns>
        public static bool IsExpire(string expireDate)
        {
            int nowTimeSpan = DateTimeHelper.ConvertDateTimeInt(DateTime.Now);
            int oldTimeSpan = DateTimeHelper.ConvertDateTimeInt(DateTime.Parse(expireDate));
            if (nowTimeSpan > oldTimeSpan)
            {
                Console.WriteLine("检测到系统有更新组件，程序需升级，请联系管理员处理.");
                Console.ReadKey();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否需要升级
        /// </summary>
        /// <param name="expireDate"></param>
        /// <returns></returns>
        public static bool IsNeedUpdate(string expireDate)
        {
            int nowTimeSpan = DateTimeHelper.ConvertDateTimeInt(DateTime.Now);
            int oldTimeSpan = DateTimeHelper.ConvertDateTimeInt(DateTime.Parse(expireDate));
            if (nowTimeSpan > oldTimeSpan)
            {
                Console.WriteLine("检测到系统有更新组件，程序需升级，请联系管理员处理.");
                Console.ReadKey();
                return true;
            }
            return false;
        }
    }
}
