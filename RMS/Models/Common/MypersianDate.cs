using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace RMS.Models.Common;

public class MypersianDate
{
    private string month_name;
    private string week_name;
    private string ayyam_name;
    private string ayyam_name2;
    private string ayyam_name3;
    DateTime mydate;
    PersianCalendar mycal = new PersianCalendar();
    HijriCalendar hijrical = new HijriCalendar();

    public MypersianDate(DateTime dt)
    {
        mydate = dt;
    }

    public string persianweek()
    {
        switch (mycal.GetDayOfWeek(mydate).ToString().ToLower())
        {
            case "saturday": week_name = "شنبه"; break;
            case "sunday": week_name = "یکشنبه"; break;
            case "monday": week_name = "دوشنبه"; break;
            case "tuesday": week_name = "سه شنبه"; break;
            case "wednesday": week_name = "چهارشنبه"; break;
            case "thursday": week_name = "پنج شنبه"; break;
            case "friday": week_name = "جمعه"; break;
        }
        return week_name;
    }

    public int persianweekNum()
    {
        int weekNum = 0;
        switch (mycal.GetDayOfWeek(mydate).ToString().ToLower())
        {
            case "saturday": weekNum = 0; break;
            case "sunday": weekNum = 1; break;
            case "monday": weekNum = 2; break;
            case "tuesday": weekNum = 3; break;
            case "wednesday": weekNum = 4; break;
            case "thursday": weekNum = 5; break;
            case "friday": weekNum = 6; break;
        }
        return weekNum;
    }
    private string ayyam()
    {
        switch (mycal.GetMonth(mydate))
        {
            case 1:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 1: ayyam_name = "سال نو بر شما مبارک باد - تعطیل"; break;
                    case 2: ayyam_name = "سال نو بر شما مبارک باد - تعطیل"; break;
                    case 3: ayyam_name = "سال نو بر شما مبارک باد - تعطیل"; break;
                    case 4: ayyam_name = "سال نو بر شما مبارک باد - تعطیل"; break;
                    case 12: ayyam_name = "روز جمهوری اسلامی ایران -- تعطیل"; break;
                    case 13: ayyam_name = "روز طبیعت -- تعطیل"; break;
                }
                break;
            case 2:
                break;
            case 3:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 14: ayyam_name = "رحلت حضرت امام خمینی -- تعطیل"; break;
                    case 15: ayyam_name = "قیام خونین 15 خرداد -- تعطیل"; break;
                }
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 22: ayyam_name = "پیروزی انقلاب و سقوط شاهنشاهی -- تعطیل"; break;
                }
                break;
            case 12:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 29: ayyam_name = "روز ملی شدن صنعت نفت ایران -- تعطیل"; break;
                }
                break;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        switch (hijrical.GetMonth(mydate))
        {
            case 1:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 9: ayyam_name2 = "تاسوعای حسینی -- تعطیل"; break;
                    case 10: ayyam_name2 = "عاشورای حسینی -- تعطیل"; break;
                }
                break;
            case 2:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 20: ayyam_name2 = "اربعین حسینی -- تعطیل"; break;
                    case 28: ayyam_name2 = "رحلت حضرت رسول اکرم ص - شهادت حضرت امام حسن مجتبی ع -- تعطیل"; break;
                    case 29: ayyam_name2 = "شهادت حضرت امام رضا ع - تعطیل"; break;
                }
                break;
            case 3:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 17: ayyam_name2 = "میلاد حضرت رسول اکرم و روز اخلاق و مهرورزی -- میلاد امام جعفر صادق -- تعطیل"; break;
                }
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 3: ayyam_name2 = "شهادت حضرت فاطمه زهرا س -- تعطیل"; break;
                }
                break;
            case 7:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 27: ayyam_name2 = "مبعث رسول اکرم ص -- تعطیل"; break;
                }
                break;
            case 8:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 15: ayyam_name2 = "ولادت حضرت قائم عج روز جهانی مستضعفان -- تعطیل"; break;
                }
                break;
            case 9:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 21: ayyam_name2 = "شهادت حضرت علی علیه السلام -- تعطیل"; break;
                }
                break;
            case 10:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 1: ayyam_name2 = "عید سعید فطر -- تعطیل"; break;
                    case 2: ayyam_name2 = "عید سعید فطر -- تعطیل"; break;
                    case 25: ayyam_name2 = "شهادت امام جعفر صادق ع -- تعطیل"; break;
                }
                break;
            case 11:
                break;
            case 12:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 10: ayyam_name2 = "عید سعید قربان -- تعطیل "; break;
                }
                break;

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        return ayyam_name3 = ayyam_name2 + " - " + ayyam_name;
    }

    public bool IsHoly()
    {
        bool blnCheck = false;
        switch (mycal.GetMonth(mydate))
        {
            case 1:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 1: blnCheck=true; break;
                    case 2: blnCheck = true; break;
                    case 3: blnCheck = true; break;
                    case 4: blnCheck = true; break;
                    case 12: blnCheck = true; break;
                    case 13: blnCheck = true; break;
                }
                break;
            case 2:
                break;
            case 3:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 14: blnCheck = true; break;
                    case 15: blnCheck = true; break;
                }
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 22: blnCheck = true; break;
                }
                break;
            case 12:
                switch (mycal.GetDayOfMonth(mydate))
                {
                    case 29: blnCheck = true; break;
                }
                break;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        switch (hijrical.GetMonth(mydate))
        {
            case 1:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 9: blnCheck = true; break;
                    case 10: blnCheck = true; break;
                }
                break;
            case 2:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 20: blnCheck = true; break;
                    case 28: blnCheck = true; break;
                    case 29: blnCheck = true; break;
                }
                break;
            case 3:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 17: blnCheck = true; break;
                }
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 3: blnCheck = true; break;
                }
                break;
            case 7:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 27: blnCheck = true; break;
                }
                break;
            case 8:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 15: blnCheck = true; break;
                }
                break;
            case 9:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 21: blnCheck = true; break;
                }
                break;
            case 10:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 1: blnCheck = true; break;
                    case 2: blnCheck = true; break;
                    case 25: blnCheck = true; break;
                }
                break;
            case 11:
                break;
            case 12:
                switch (hijrical.GetDayOfMonth(mydate))
                {
                    case 10: blnCheck = true; break;
                }
                break;

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        return blnCheck;
    }


    /// <summary>
    /// ساعت سیستم بصورت اصلاح شده
    /// </summary>
    /// <returns></returns>
    public string time()
    {
        return DateTime.Now.Second.ToString() + " : " + DateTime.Now.Minute.ToString() + " : " + DateTime.Now.Hour.ToString();
        //throw new System.NotImplementedException();
    }
    /// <summary>
    /// کل تقویم شمسی در یک رشته
    /// </summary>
    /// <returns></returns>
    public string taghvim()
    {
        return ayyam();
    }
    /// <summary>
    /// تاریخ شمسی همراه با مصادف ها
    /// </summary>
    /// <returns></returns>
    public string CompletePrsDate()
    {
        string str;
        str = persianweek();
        return str;
        //throw new System.NotImplementedException();
    }

    /// <summary>
    /// تاریخ شمسی ساده برای دخیره در دیتابیس
    /// </summary>
    /// <returns></returns>
    public string simpleDate()
    {
        string strYear = mycal.GetYear(mydate).ToString();
        string strMonth = (mycal.GetMonth(mydate).ToString().Length == 1) ? ("0" + mycal.GetMonth(mydate)) : (mycal.GetMonth(mydate).ToString());
        string strDay = (mycal.GetDayOfMonth(mydate).ToString().Length == 1) ? ("0" + mycal.GetDayOfMonth(mydate)) : (mycal.GetDayOfMonth(mydate).ToString());
        return strYear + "/" + strMonth + "/" + strDay;
    }
    public string GetTimeNow()
    {
        return DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
    }
    public string bebDate()
    {
        string strYear = mycal.GetYear(mydate).ToString();
        string strMonth = (mycal.GetMonth(mydate).ToString().Length == 1) ? ("0" + mycal.GetMonth(mydate)) : (mycal.GetMonth(mydate).ToString());
        string strDay = (mycal.GetDayOfMonth(mydate).ToString().Length == 1) ? ("0" + mycal.GetDayOfMonth(mydate)) : (mycal.GetDayOfMonth(mydate).ToString());
        return strYear.Substring(2, 2) + strMonth + strDay;
    }

}
