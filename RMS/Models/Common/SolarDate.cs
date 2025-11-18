using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SolarDate
/// </summary>
/// 
namespace RMS.Models.Common
{
    public static class SolarDate
    {
        private static string currentDateTime;
        private static string currentTime;
        private static string currentMonth;
        private static string currentDate;
        private static int yAge;
        private static int mAge;
        private static int dAge;

        /// <summary>
        /// تاریخ و زمان فعلی را به صورت 
        /// یک رشته 19 کاراکتری برمی گرداند
        /// </summary>
        public static string CurrentDateTime
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string year = pc.GetYear(DateTime.Now).ToString();
                string month = pc.GetMonth(DateTime.Now).ToString();
                string day = pc.GetDayOfMonth(DateTime.Now).ToString();
                string hour = pc.GetHour(DateTime.Now).ToString();
                string minute = pc.GetMinute(DateTime.Now).ToString();
                string second = pc.GetSecond(DateTime.Now).ToString();

                if (month.Length == 1)
                    month = "0" + month;
                if (day.Length == 1)
                    day = "0" + day;
                if (hour.Length == 1)
                    hour = "0" + hour;
                if (minute.Length == 1)
                    minute = "0" + minute;
                if (second.Length == 1)
                    second = "0" + second;

                currentDateTime = hour + ":" + minute + ":" + second + " " + year + "/" + month + "/" + day;
                return currentDateTime;

            }
        }

        /// <summary>
        /// زمان فعلی را به صورت 
        /// یک رشته 8 کاراکتری برمی گرداند
        /// </summary>
        public static string CurrentTime
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string hour = pc.GetHour(DateTime.Now).ToString();
                string minute = pc.GetMinute(DateTime.Now).ToString();
                string second = pc.GetSecond(DateTime.Now).ToString();

                if (hour.Length == 1)
                    hour = "0" + hour;
                if (minute.Length == 1)
                    minute = "0" + minute;
                if (second.Length == 1)
                    second = "0" + second;
                currentTime = hour + ":" + minute + ":" + second;
                return currentTime;
            }

        }
        public static string CurrentDay
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                DayOfWeek d = pc.GetDayOfWeek(DateTime.Now);
                if (d == DayOfWeek.Friday)
                    return "جمعه";
                else if (d == DayOfWeek.Monday)
                    return "دوشنبه";
                else if (d == DayOfWeek.Saturday)
                    return "شنبه";
                else if (d == DayOfWeek.Sunday)
                    return "یکشنبه";
                else if (d == DayOfWeek.Thursday)
                    return "پنجشنبه";
                else if (d == DayOfWeek.Tuesday)
                    return "سه شنبه";
                else
                    return "چهارشنبه";
            }

        }
        public static int CurrentDayNum
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                DayOfWeek d = pc.GetDayOfWeek(DateTime.Now);
                if (d == DayOfWeek.Friday)
                    return 7;
                else if (d == DayOfWeek.Monday)
                    return 3;
                else if (d == DayOfWeek.Saturday)
                    return 1;
                else if (d == DayOfWeek.Sunday)
                    return 2;
                else if (d == DayOfWeek.Thursday)
                    return 6;
                else if (d == DayOfWeek.Tuesday)
                    return 4;
                else
                    return 5;
            }

        }
        public static string CurrentMonth
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                int d = pc.GetMonth(DateTime.Now);
                if (d == 1)
                    return "فروردین";
                else if (d == 2)
                    return "اردیبهشت";
                else if (d == 3)
                    return "خرداد";
                else if (d == 4)
                    return "تیر";
                else if (d == 5)
                    return "مرداد";
                else if (d == 6)
                    return "شهریوی";
                else if (d == 7)
                    return "مهر";
                else if (d == 8)
                    return "آبان";
                else if (d == 9)
                    return "آذر";
                else if (d == 10)
                    return "دی";
                else if (d == 11)
                    return "بهمن";
                else if (d == 12)
                    return "اسفند";
                else
                    return "";
            }

        }

        /// <summary>
        /// تاریخ شمسی فعلی را به صورت 
        /// یک رشته 10 کاراکتری برمی گرداند
        /// </summary>
        public static string CurrentDate
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                string year = pc.GetYear(DateTime.Now).ToString();
                string month = pc.GetMonth(DateTime.Now).ToString();
                string day = pc.GetDayOfMonth(DateTime.Now).ToString();
                if (month.Length == 1)
                    month = "0" + month;
                if (day.Length == 1)
                    day = "0" + day;
                currentDate = year + "/" + month + "/" + day;
                return currentDate;
            }
        }
        public static string CurrentYear
        {
            get
            {
                return new PersianCalendar().GetYear(DateTime.Now).ToString();
            }
        }

        /*public static string CalculateAge(string date)
        {
            date = validDate(date);
            if (date != "####")
            {
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(5, 2));
                int day = int.Parse(date.Substring(8, 2));
                DateTime birthDate = GregorianDate(year, month, day, 0, 0, 0, 0);
                SqlCommand cmd = new SqlCommand("AgeCalculate");
                cmd.Parameters.AddWithValue("@START_DATE", birthDate);
                cmd.Parameters.AddWithValue("@END_DATE", DateTime.Now);
                string age = new DatabaseManager().ExecuteScalar(cmd).ToString();
                return int.Parse(age.Substring(0, 4)) + " " + "سال" + " " + int.Parse(age.Substring(5, 2)) + " " + "ماه" + " " + int.Parse(age.Substring(8, 2)) + " " + "روز";
            }
            else
                return "";
        }*/
        /*public static int CalculateAgeYear(string date)
        {
            date = validDate(date);
            if (date != "####")
            {
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(5, 2));
                int day = int.Parse(date.Substring(8, 2));
                DateTime birthDate = GregorianDate(year, month, day, 0, 0, 0, 0);
                SqlCommand cmd = new SqlCommand("AgeCalculate");
                cmd.Parameters.AddWithValue("@START_DATE", birthDate);
                cmd.Parameters.AddWithValue("@END_DATE", DateTime.Now);
                string age = new DatabaseManager().ExecuteScalar(cmd).ToString();
                return int.Parse(age.Substring(0, 4));
            }
            else
                return 0;
        }*/
        /*public static int CalculateAgeMonth(string date)
        {
            date = validDate(date);
            if (date != "####")
            {
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(5, 2));
                int day = int.Parse(date.Substring(8, 2));
                DateTime birthDate = GregorianDate(year, month, day, 0, 0, 0, 0);
                SqlCommand cmd = new SqlCommand("AgeCalculate");
                cmd.Parameters.AddWithValue("@START_DATE", birthDate);
                cmd.Parameters.AddWithValue("@END_DATE", DateTime.Now);
                string age = new DatabaseManager().ExecuteScalar(cmd).ToString();
                return int.Parse(age.Substring(5, 2));
            }
            else
                return 0;
        }*/
        #region
        /// <summary>
        /// بررسی می کند که کاراکتر غیر عددی در رشته نباشد
        /// </summary>
        /// <param name="str"> رشته ورودی روز ،ماه یا سال است</param>
        /// <returns>را برمیگرداند true در صورت عدم وجود کاراکتر نامعتبر مقدار </returns>
        private static bool isNumber(string str)
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
                if (str.ToCharArray()[i] >= '0' && str.ToCharArray()[i] <= '9')
                    count++;
                else
                    return false;

            if (count == 2 || count == 4)
                return true;
            return false;
        }
        private static void MonthLessmonth(int D, int M, int Y, int d, int m, int y)
        {
            PersianCalendar pc = new PersianCalendar();
            mAge = M - 1 + 12 - m;
            yAge = Y - 1 - y;
            if (pc.IsLeapYear(Y) && pc.IsLeapYear(y))
            {
                if (M >= 1 && M <= 6)
                {
                    dAge = D + 31 - d;

                    if (m > 1 && m <= 6)
                        dAge += yAge / 4 + 1;

                    else if (m >= 7 && m <= 11)
                        dAge += yAge / 4;

                    if (m == 12)
                        dAge += yAge / 4 - 1;

                }
                ///////////////////////
                if (M >= 7 && M <= 11)
                {
                    dAge = D + 30 - d;

                    if (m > 7 && m <= 11)
                        dAge += yAge / 4 + 1;

                    if (m == 12)
                        dAge += yAge / 4;
                }

            }
            //////////////////////////////////////////////
            if (!pc.IsLeapYear(Y) && pc.IsLeapYear(y))
            {
                if (M >= 1 && M <= 6)
                {
                    dAge = D + 31 - d;

                    if (m >= 1 && m <= 6)
                        dAge += yAge / 4 + 1;

                    if (m >= 7 && m <= 12)
                        dAge += yAge / 4;

                }
                if (M >= 7 && M <= 11)
                {
                    dAge = D + 30 - d;

                    if (m >= 7 && m <= 11)
                        dAge += yAge / 4 + 1;

                    if (m == 12)
                        dAge += yAge / 4;
                }
            }
            ///////////////////////////////////////////
            if (!pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
            {
                if (M >= 1 && M <= 6)
                {
                    dAge = D + 31 - d;

                    if (m >= 1 && m <= 6)
                        dAge += yAge / 4;

                    else if (m >= 7 && m <= 11)
                        dAge += yAge / 4 - 1;

                    else if (m == 12)
                        dAge += yAge / 4 - 2;

                }
                else if (M >= 7 && M <= 11)
                {
                    dAge = D + 30 - d;

                    if (m >= 7 && m <= 11)
                        dAge += yAge / 4;

                    if (m == 12)
                        dAge += yAge / 4 - 1;

                }
            }
            //////////////////////////////////////////
            if (pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
            {
                if (M >= 1 && M <= 6)
                {
                    dAge = D + 31 - d;

                    if (m >= 1 && m <= 6)
                        dAge += yAge / 4;

                    if (m >= 7 && m <= 11)
                        dAge += yAge / 4 - 1;

                    if (m == 12)
                        dAge += yAge / 4 - 2;
                }
                ///////////////////////
                if (M >= 7 && M <= 11)
                {
                    dAge = D + 30 - d;

                    if (m >= 7 && m <= 11)
                        dAge += yAge / 4;

                    if (m == 12)
                        dAge += yAge / 4 - 1;
                }
            }
        }
        private static void MonthMostmonth(int D, int M, int Y, int d, int m, int y)
        {
            PersianCalendar pc = new PersianCalendar();
            mAge = M - 1 - m;
            yAge = Y - y;
            if (pc.IsLeapYear(Y) && !pc.IsLeapYear(y) || pc.IsLeapYear(Y) && pc.IsLeapYear(y) || !pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
            {
                if (M >= 1 && M <= 6)
                {
                    dAge = D + 31 - d;
                    dAge += yAge / 4;
                }
            }
            if (!pc.IsLeapYear(Y) && pc.IsLeapYear(y))
            {
                if (M >= 1 && M <= 6)
                {
                    dAge = D + 31 - d;
                    dAge += yAge / 4 + 1;
                }
                else if (M >= 7 && M <= 11)
                {
                    dAge = D + 30 - d;
                    dAge += yAge / 4 + 2;
                }
                else if (M == 12)
                {
                    dAge = D + 29 - d;
                    dAge += yAge / 4 + 3;
                }
            }
            if (pc.IsLeapYear(Y) && !pc.IsLeapYear(y) || pc.IsLeapYear(Y) && pc.IsLeapYear(y))
            {
                if (M >= 7 && M <= 12)
                {
                    dAge = D + 30 - d;
                    dAge += yAge / 4 + 1;
                }
            }
            if (!pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
            {
                if (M >= 7 && M <= 11)
                {
                    dAge = D + 30 - d;
                    dAge += yAge / 4 + 1;
                }
                else if (M == 12)
                {
                    dAge = D + 29 - d;
                    dAge += yAge / 4 + 2;
                }
            }
        }
        private static void MonthEqualemonth(int D, int M, int Y, int d, int m, int y)
        {
            PersianCalendar pc = new PersianCalendar();
            mAge = M - 1 + 12 - m;
            yAge = Y - 1 - y;

            if (M >= 1 && M <= 6)
                dAge = D + 31 - d;

            else if (M >= 7 && M <= 11)
                dAge = D + 30 - d;

            else if (M == 12)
            {
                if (pc.IsLeapYear(Y))
                    dAge = D + 30 - d;

                else
                    dAge = D + 29 - d;
            }
            if (pc.IsLeapYear(y) && !pc.IsLeapYear(Y))
                dAge += yAge / 4 + 1;

            if (!pc.IsLeapYear(Y) && !pc.IsLeapYear(y) || pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
                dAge += yAge / 4;

            if (pc.IsLeapYear(Y) && pc.IsLeapYear(y))
                dAge += yAge / 4 + 1;
        }
        /// <summary>
        /// دو تاریخ را با هم مقایسه می کند و اگر پارامتر اول بزرگتر باشد عدد 1 ،پارامتر دوم بزرگتر باشد عدد -1 و اگر باهم برابر باشند 0 را بر می گرداند و در صورتی که فرمت هر کدام از تاریخ ها نادرست باشد عدد غیر از این اعداد را برمیگرداند 
        /// </summary>
        #endregion
        public static int compare(string date1, string date2)
        {
            date1 = validDate(date1);
            date2 = validDate(date2);
            if (date1 == "####" || date2 == "####")
                return -100;

            string year1 = date1.Substring(0, date1.IndexOf('/'));
            string month1 = date1.Substring(date1.IndexOf('/') + 1, date1.LastIndexOf('/') - year1.Length - 1);
            string day1 = date1.Substring(date1.LastIndexOf('/') + 1, date1.Length - year1.Length - month1.Length - 2);

            string year2 = date2.Substring(0, date2.IndexOf('/'));
            string month2 = date2.Substring(date2.IndexOf('/') + 1, date2.LastIndexOf('/') - year2.Length - 1);
            string day2 = date2.Substring(date2.LastIndexOf('/') + 1, date2.Length - year2.Length - month2.Length - 2);
            if (year1.CompareTo(year2) > 0)
                return 1;
            else if (year1.CompareTo(year2) < 0)
                return -1;
            else if (year2 == year1)
            {
                if (month1.CompareTo(month2) > 0)
                    return 1;
                else if (month1.CompareTo(month2) < 0)
                    return -1;
                else if (month1 == month2)
                {
                    if (day1.CompareTo(day2) > 0)
                        return 1;
                    else if (day1.CompareTo(day2) < 0)
                        return -1;
                    else if (day1 == day2)
                    {
                        return 0;
                    }
                }
            }
            return 100;
        }
        public static string validDate(string s)
        {
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                    if (s.ToCharArray()[l] != '/')
                    {
                        s = s.Remove(l, 1);
                        l = 0;
                    }
            if (s.ToCharArray()[s.Length - 1] == '/')
                s = s.Remove(s.Length - 1, 1);
            if (s.ToCharArray()[s.Length - 1] == '/')
                s = s.Remove(s.Length - 1, 1);
            if (s.Length == 4)
                s = s + "/01/01";
            if (s.Length == 2)
                s = s + "/01/01";
            int count = 0;
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] == '/')
                    count++;
            if (count == 1)
            {
                s = s + "/01";
                count++;
            }
            if (s.Length < 6 || count != 2)
                return "####";


            string year = s.Substring(0, s.IndexOf('/'));
            string month = s.Substring(s.IndexOf('/') + 1, s.LastIndexOf('/') - year.Length - 1);
            string day = s.Substring(s.LastIndexOf('/') + 1, s.Length - year.Length - month.Length - 2);

            if (year.Length == 2)
                year = "13" + year;
            else if (year.Length != 4)
                return "####";

            if (month.Length == 1)
                month = "0" + month;
            else if (month.Length != 2)
                return "####";

            if (day.Length == 1)
                day = "0" + day;
            else if (day.Length != 2)
                return "####";
            return year + "/" + month + "/" + day;
        }
        public static string reverseDate(string s)
        {
            if (s.Contains("/"))
                return s.Split('/')[2] + "/" + s.Split('/')[1] + "/" + s.Split('/')[0];
            return s;
        }
        /// <summary>
        /// رشته ای را گرفته و آنرا بررسی می کند در صورتی که این رشته یک تاریخ معتبر باشد مقدار 
        /// OK
        /// و در غیر اینصورت خطای آنرا بر می گرداند
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string validateBirthDate(string s)
        {
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                    if (s.ToCharArray()[l] != '/')
                    {
                        s = s.Remove(l, 1);
                        l = 0;
                    }
            if (s.ToCharArray()[s.Length - 1] == '/')
                s = s.Remove(s.Length - 1, 1);
            if (s.ToCharArray()[s.Length - 1] == '/')
                s = s.Remove(s.Length - 1, 1);
            if (s.Length == 4)
                s = s + "/01/01";
            if (s.Length == 2)
                s = s + "/01/01";
            int count = 0;
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] == '/')
                    count++;
            if (count == 1)
            {
                s = s + "/01";
                count++;
            }
            if (s.Length < 6 || count != 2)
                return "این تاریخ تولد معتبر نمی باشد";
            string year = s.Substring(0, s.IndexOf('/'));
            string month = s.Substring(s.IndexOf('/') + 1, s.LastIndexOf('/') - year.Length - 1);
            string day = s.Substring(s.LastIndexOf('/') + 1, s.Length - year.Length - month.Length - 2);

            if (year.Length == 2)
                year = "13" + year;
            else if (year.Length != 4)
                return "سال تولد معتبر نیست";
            if (int.Parse(year) < 1250)
                return "سال این تاریخ تولد نامعتبر است";
            if (int.Parse(month) > 12 || int.Parse(month) < 1)
                return "ماه این تاریخ تولد نامعتبر است";
            if (month.Length == 1)
                month = "0" + month;
            else if (month.Length != 2)
                return "ماه تاریخ تولد نامعتبر می باشد";

            if (day.Length == 1)
                day = "0" + day;
            if (int.Parse(day) > 31 || int.Parse(day) < 1)
                return "روز تاریخ تولد معتبر نمی باشد";
            if (int.Parse(day) > 30 && int.Parse(month) > 6)
                return "روز تاریخ تولد معتبر نمی باشد";
            else if (day.Length != 2)
                return "روز تاریخ تولد معتبر نمی باشد";
            if (compare(year + "/" + month + "/" + day, CurrentDate) == 1)
                return "تاریخ تولد نباید از تاریخ فعلی بزرگتر باشد";
            if (int.Parse(day) == 30 && int.Parse(month) == 12 && !new PersianCalendar().IsLeapYear(int.Parse(year)))
                return "سال وارد شده کبیسه نیست";
            return "OK";
        }

        public static int Cabiseh_No(int year)
        {
            int Y, Y1, Y2, Y3, Y4, CABISE_NO;
            Y = year - 508 + 1300;
            Y1 = Y / 128;
            Y2 = Y % 128;
            Y3 = Y2 / 33;
            Y4 = Y2 % 33;
            CABISE_NO = Y1 * 31 + Y3 * 8 + Y4 / 4 - Y2 / 127 - Y4 / 32 + 123 - 314;
            return CABISE_NO;

        }


        public static DateTime GregorianDate(int year, int month, int day, int hour, int minute, int second, int milisecond)
        {
            PersianCalendar p = new PersianCalendar();
            return p.ToDateTime(year, month, day, hour, minute, second, milisecond);
        }
        public static string validateDate(string s)
        {
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                    if (s.ToCharArray()[l] != '/')
                        return "این تاریخ معتبر نمی باشد";
            int count = 0;
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] == '/')
                    count++;
            if (s.Length != 10 || count != 2)
                return "این تاریخ معتبر نمی باشد";
            string year = s.Substring(0, s.IndexOf('/'));
            string month = s.Substring(s.IndexOf('/') + 1, s.LastIndexOf('/') - year.Length - 1);
            string day = s.Substring(s.LastIndexOf('/') + 1, s.Length - year.Length - month.Length - 2);

            if (year.Length != 4)
                return "سال معتبر نیست";
            if (int.Parse(year) < 1250)
                return "سال این تاریخ ، نامعتبر است";
            if (int.Parse(month) > 12 || int.Parse(month) < 1 || month.Length != 2)
                return "ماه این تاریخ ، نامعتبر است";
            if (int.Parse(day) > 31 || int.Parse(day) < 1 || day.Length != 2)
                return "روز تاریخ ، معتبر نمی باشد";
            if (int.Parse(day) > 30 && int.Parse(month) > 6)
                return "روز تاریخ ، معتبر نمی باشد";
            if (compare(year + "/" + month + "/" + day, CurrentDate) == 1)
                return "تاریخ ، نباید از تاریخ فعلی بزرگتر باشد";
            if (int.Parse(day) == 30 && int.Parse(month) == 12 && !new PersianCalendar().IsLeapYear(int.Parse(year)))
                return "سال وارد شده کبیسه نیست";
            return "OK";
        }
        public static string validateDate2(string s)
        {
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                    if (s.ToCharArray()[l] != '/')
                        return "این تاریخ معتبر نمی باشد";
            int count = 0;
            for (int l = 0; l < s.Length; l++)
                if (s.ToCharArray()[l] == '/')
                    count++;
            if (s.Length != 10 || count != 2)
                return "این تاریخ معتبر نمی باشد";
            string year = s.Substring(0, s.IndexOf('/'));
            string month = s.Substring(s.IndexOf('/') + 1, s.LastIndexOf('/') - year.Length - 1);
            string day = s.Substring(s.LastIndexOf('/') + 1, s.Length - year.Length - month.Length - 2);

            if (year.Length != 4)
                return "سال معتبر نیست";
            if (int.Parse(year) < 1250)
                return "سال این تاریخ ، نامعتبر است";
            if (int.Parse(month) > 12 || int.Parse(month) < 1 || month.Length != 2)
                return "ماه این تاریخ ، نامعتبر است";
            if (int.Parse(day) > 31 || int.Parse(day) < 1 || day.Length != 2)
                return "روز تاریخ ، معتبر نمی باشد";
            if (int.Parse(day) > 30 && int.Parse(month) > 6)
                return "روز تاریخ ، معتبر نمی باشد";
            if (int.Parse(day) == 30 && int.Parse(month) == 12 && !new PersianCalendar().IsLeapYear(int.Parse(year)))
                return "سال وارد شده کبیسه نیست";
            return "OK";
        }
        public static string validateTime(string s)
        {
            if (s.Length != 5 || s.ToCharArray()[2] != ':')
                return "این زمان معتبر نمی باشد";
            if (s.ToCharArray()[0] > '9' || s.ToCharArray()[0] < '0' || s.ToCharArray()[1] > '9' || s.ToCharArray()[1] < '0' || s.ToCharArray()[3] > '9' || s.ToCharArray()[3] < '0' || s.ToCharArray()[4] > '9' || s.ToCharArray()[4] < '0')
                return "این زمان معتبر نمی باشد";
            string hour = s.Substring(0, s.IndexOf(':'));
            string menute = s.Substring(s.IndexOf(':') + 1, 2);
            if (int.Parse(hour) < 0 || int.Parse(hour) > 23)
                return "ساعت این زمان ، نامعتبر است";
            else if (int.Parse(menute) < 0 || int.Parse(menute) > 59)
                return "دقیقه این زمان ، نامعتبر است";
            return "OK";
        }
        public static string normalize(int number)
        {
            if (number < 10)
                return "0" + number.ToString();
            else
                return number.ToString();
        }

        public static string ToPersian(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            int hour = pc.GetHour(dt);
            int min = pc.GetMinute(dt);

            DateTime PersianDateTime = new DateTime(year, month, day, hour, min, 0);

            return PersianDateTime.ToString("yyyy/MM/dd");
        }

        public static string ToPersianNew(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            int hour = pc.GetHour(dt);
            int min = pc.GetMinute(dt);

            //DateTime PersianDateTime = new DateTime(year, month, day, hour, min, 0);

            return year + "/" + month + "/" + day;
        }

        public static string ConvertMiladiToPersion(DateTime dmiladi)
        {
            PersianCalendar p = new PersianCalendar();
            string Month = p.GetMonth(dmiladi).ToString();
            string NewMonth = Month.Length == 1 ? "0" + Month : Month;
            string Day = p.GetDayOfMonth(dmiladi).ToString();
            string NewDay = Day.Length == 1 ? "0" + Day : Day;
            return p.GetYear(dmiladi).ToString() + "/" + NewMonth + "/" + NewDay;
        }

        public static DateTime ToMiladi(int Year, int Month, int Day)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
        }
    }
}