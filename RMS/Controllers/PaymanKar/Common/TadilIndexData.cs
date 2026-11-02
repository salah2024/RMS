namespace RMS.Controllers.PaymanKar.Common;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class CourseInfo
{
    public int Year { get; set; }
    public int CourseType { get; set; } // 1=Seasonal , 2=Monthly
    public int CourseCode { get; set; }
    public decimal IndexValue { get; set; }
}

public class ResultItem
{
    public int Year { get; set; }          // سال واقعی

    public int IndexYear { get; set; }     // سال شاخص
    public decimal IndexValue { get; set; }

    public int CourseType { get; set; }

    public int CourseCode { get; set; }

    public int Days { get; set; }


    public decimal? PGHIndexValue { get; set; }
    public long? PGHYearValue { get; set; }
    public long? PGHCourseTypeValue { get; set; }
    public long? PGHCourseValue { get; set; }

    public decimal Nesbat { get; set; }

    public decimal MablaghKarkardDoreh { get; set; }

    public decimal ZaribTadil { get; set; }

    public decimal MablaghTadil { get; set; }

}

public class TadilIndexData
{
    private static readonly PersianCalendar pc = new PersianCalendar();


    public CourseInfo? GetPGHPreviousCourse(
     DateTime pghDate,
     List<CourseInfo> courses)
    {
        int pghYear = pc.GetYear(pghDate);
        int pghMonth = pc.GetMonth(pghDate);

        int currentSeason = ((pghMonth - 1) / 3) + 1;

        // سال شاخص قابل استفاده
        int effectiveYear = courses
            .Where(x => x.Year <= pghYear)
            .Select(x => x.Year)
            .DefaultIfEmpty()
            .Max();

        if (effectiveYear == 0)
            return null;

        int prevSeason;
        int prevMonth;
        int targetYear;

        if (effectiveYear < pghYear)
        {
            // کل سال جاری شاخص ندارد
            targetYear = effectiveYear;
            prevSeason = 4;
            prevMonth = 12;
        }
        else
        {
            targetYear = pghYear;

            if (currentSeason == 1)
            {
                targetYear--;
                prevSeason = 4;
                prevMonth = 12;
            }
            else
            {
                prevSeason = currentSeason - 1;
                prevMonth = prevSeason * 3;
            }
        }

        // اول ماهانه
        var monthly = courses.FirstOrDefault(x =>
            x.Year == targetYear &&
            x.CourseType == 2 &&
            x.CourseCode == prevMonth);

        if (monthly != null)
            return monthly;

        // بعد فصلی
        var seasonal = courses.FirstOrDefault(x =>
            x.Year == targetYear &&
            x.CourseType == 1 &&
            x.CourseCode == prevSeason);

        return seasonal;
    }



    public List<ResultItem> Calculate(
    string fromDate,
    string toDate,
    string PGHDate,
    List<CourseInfo> courses)
    {
        var from = ParsePersianDate(fromDate);
        var to = ParsePersianDate(toDate);
        var pgh = ParsePersianDate(PGHDate);

        var pghPreviousCourse =
            GetPGHPreviousCourse(pgh, courses);

        var monthDays = GetMonthDaysInRange(from, to);

        var result = new List<ResultItem>();

        // سال‌های موجود در شاخص‌ها
        var availableYears = courses
            .Select(x => x.Year)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        // همه سال‌هایی که در بازه تاریخ وجود دارند
        List<int> yearsInRange = monthDays.Keys
            .Select(x => x.year)
            .Distinct()
            .ToList();

        yearsInRange.Reverse();

        foreach (var year in yearsInRange)
        {
            int effectiveYear = 0;
            //availableYears.Contains(year)
            //    ? year
            //    : availableYears
            //        .Where(y => y < year)
            //        .DefaultIfEmpty()
            //        .Max();

            bool blnIsCurrentYearEffective = false;
            if (availableYears.Contains(year))
            {
                effectiveYear = year;
                blnIsCurrentYearEffective = true;
            }
            else
            {
                effectiveYear = availableYears
                    .Where(y => y < year)
                    .DefaultIfEmpty()
                    .Max();
            }

            if (effectiveYear == 0)
                continue;

            List<CourseInfo> yearCourses = courses
                .Where(x => x.Year == effectiveYear).OrderByDescending(x => x.CourseCode)
                .ToList();

            List<CourseInfo> lstyearCourseForRepeat = new List<CourseInfo>();

            if (!blnIsCurrentYearEffective)
            {
                lstyearCourseForRepeat.Add(yearCourses.First());
            }
            else
                lstyearCourseForRepeat.AddRange(yearCourses);

            foreach (var course in lstyearCourseForRepeat)
            {
                int days = 0;
                int CurrentCourseCode = 1;
                if (course.CourseType == 2) // ماهانه
                {
                    monthDays.TryGetValue(
                        (year, course.CourseCode),
                        out days);

                    if (!blnIsCurrentYearEffective)
                    {
                        CurrentCourseCode = course.CourseCode + 1;
                        if (CurrentCourseCode > 12)
                        {
                            CurrentCourseCode = 1;
                        }
                    }
                    else
                    {
                        CurrentCourseCode = course.CourseCode;

                    }


                    //if (days == 0)
                    //{
                    //    var newCourse = BuildCourseFromMonthDays(
                    //        year,
                    //        course.CourseType,
                    //        monthDays);

                    //    if (newCourse != null)
                    //    {
                    //        course.CourseCode = newCourse.CourseCode;

                    //        monthDays.TryGetValue(
                    //            (year, course.CourseCode),
                    //            out days);
                    //    }
                    //}

                }
                else if (course.CourseType == 1) // فصلی
                {
                    int startMonth = 0;
                    int endMonth = 0;

                    if (!blnIsCurrentYearEffective)
                    {
                        CurrentCourseCode = course.CourseCode + 1;

                        startMonth = (course.CourseCode - 1) * 3 + 1;
                        endMonth = startMonth + 2;
                        for (int month = startMonth; month <= endMonth; month++)
                        {
                            if (monthDays.TryGetValue((effectiveYear, month), out int d))
                            {
                                days += d;
                            }
                        }

                        if (CurrentCourseCode > 4)
                        {
                            CurrentCourseCode = 1;
                        }
                    }
                    else
                    {
                        CurrentCourseCode = course.CourseCode;

                        var newCourse = BuildCourseFromMonthDays(
                            year,
                            course.CourseType,
                            monthDays);

                        course.CourseCode = newCourse.CourseCode;

                        startMonth = (course.CourseCode - 1) * 3 + 1;
                        endMonth = startMonth + 2;

                        for (int month = startMonth; month <= endMonth; month++)
                        {
                            if (monthDays.TryGetValue((year, month), out int d))
                                days += d;
                        }
                    }


                    if (days == 0)
                    {
                        var newCourse = BuildCourseFromMonthDays(
                            year,
                            course.CourseType,
                            monthDays);

                        if (newCourse != null)
                        {
                            course.CourseCode = newCourse.CourseCode;

                            startMonth = (course.CourseCode - 1) * 3 + 1;
                            endMonth = startMonth + 2;

                            for (int month = startMonth; month <= endMonth; month++)
                            {
                                if (monthDays.TryGetValue((year, month), out int d))
                                    days += d;
                            }
                        }
                    }


                }

                if (days > 0)
                {
                    result.Add(new ResultItem
                    {
                        Year = year,
                        CourseType = course.CourseType,
                        CourseCode = CurrentCourseCode, //course.CourseCode,
                        Days = days,
                        IndexValue = course.IndexValue,
                        IndexYear = effectiveYear,

                        PGHIndexValue = pghPreviousCourse?.IndexValue,
                        PGHYearValue = pghPreviousCourse?.Year,
                        PGHCourseValue = pghPreviousCourse?.CourseCode,
                        PGHCourseTypeValue = pghPreviousCourse?.CourseType,
                    });
                }
            }
        }

        return result;
    }


    private CourseInfo? BuildCourseFromMonthDays(
    int year,
    int courseType,
    Dictionary<(int year, int month), int> monthDays)
    {
        if (courseType == 2) // ماهانه
        {
            var month = monthDays.Keys
                .Where(x => x.year == year)
                .Select(x => x.month)
                .OrderBy(x => x)
                .FirstOrDefault();

            if (month == 0)
                return null;

            return new CourseInfo
            {
                Year = year,
                CourseType = 2,
                CourseCode = month
            };
        }
        else // فصلی
        {
            var month = monthDays.Keys
                .Where(x => x.year == year)
                .Select(x => x.month)
                .OrderBy(x => x)
                .FirstOrDefault();

            if (month == 0)
                return null;

            int season = ((month - 1) / 3) + 1;

            return new CourseInfo
            {
                Year = year,
                CourseType = 1,
                CourseCode = season
            };
        }
    }



    private static Dictionary<(int year, int month), int> GetMonthDaysInRange(
        DateTime from,
        DateTime to)
    {
        var result = new Dictionary<(int, int), int>();

        DateTime current = from;

        while (current <= to)
        {
            int year = pc.GetYear(current);
            int month = pc.GetMonth(current);

            int daysInMonth = pc.GetDaysInMonth(year, month);

            DateTime monthEnd = pc.ToDateTime(
                year,
                month,
                daysInMonth,
                0, 0, 0, 0);

            if (monthEnd > to)
                monthEnd = to;

            int days =
                (monthEnd.Date - current.Date).Days + 1;

            if (result.ContainsKey((year, month)))
                result[(year, month)] += days;
            else
                result[(year, month)] = days;

            current = monthEnd.AddDays(1);
        }

        return result;
    }

    private static DateTime ParsePersianDate(string date)
    {
        var parts = date.Split('/');

        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        return pc.ToDateTime(
            year,
            month,
            day,
            0, 0, 0, 0);
    }
}