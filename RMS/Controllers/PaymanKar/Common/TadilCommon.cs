using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Common;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Controllers.PaymanKar.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using System.Globalization;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.PaymanKar.Common;

public class TadilCommon
{
    public async Task<ResultGetTadilIndex> GetTadil(GetTadilIndexDto request, ApplicationDbContext _context)
    {
        Guid SVId = request.SVId;
        long Year = request.Year;
        Guid CompanyId = request.CompanyId;

        clsBaravordUser? BU = _context.BaravordUsers.FirstOrDefault(x => x.ID == SVId);
        if (BU != null)
        {
            Guid BUParent = new Guid();
            string strProjectName = "";
            if (BU != null)
            {
                clsBaravordUser? BaravordUserParent = _context.BaravordUsers.FirstOrDefault(x => x.ID == BU.ParentId);
                BUParent = BU.ParentId ?? new Guid();
                if (BaravordUserParent != null)
                {
                    strProjectName = BaravordUserParent.Name;
                }
            }

            clsZaribPishnahadiPaymanKar? zaribPishnahadiPaymanKar = _context.ZaribPishnahadiPaymanKars.FirstOrDefault(x => x.BarAvordUserId == BUParent && x.Year == Year && x.CompanyId == CompanyId);

            DateTime? DTPGHDate = new DateTime();
            string strPGHDate = "1404/05/24";
            if (zaribPishnahadiPaymanKar != null)
            {
                DTPGHDate = zaribPishnahadiPaymanKar.InsertDateTime;
                if (DTPGHDate != null)
                {
                    strPGHDate = SolarDate.ToPersian(DTPGHDate.Value);
                }
            }

            string strFromDate = BU.FromDate ?? "";
            string strToDate = BU.ToDate ?? "";

            long AllDay = GetDaysBetween(strFromDate, strToDate);
            string strTitle = BU.Name;

            //یافتن برآورد قبلی
            long intNum = BU.Num - 1;
            clsBaravordUser? BUPrev = _context.BaravordUsers.FirstOrDefault(x => x.Num == intNum && x.ParentId == BU.ParentId);


            List<int> years = DateHelper.GetYearsInRange(strFromDate, strToDate);

            string[] strNoeFB = BU.NoeFBs.Split(',');

            GetFosoulForBAAndSVInputDto requestFosoulForBAAndSVInput = new GetFosoulForBAAndSVInputDto
            {
                BarAvordUserId = SVId,
                Year = BU.Year
            };

            List<resultTadilIndexDto> lstresultTadilIndexDtos = new List<resultTadilIndexDto>();

            ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();
            foreach (var NoeFB in strNoeFB)
            {
                NoeFehrestBaha NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB);

                string strNoeFBName = "";
                switch (NoeFBId)
                {
                    case NoeFehrestBaha.RahDari:
                        {
                            strNoeFBName = "راهداری";
                            break;
                        }
                    case NoeFehrestBaha.Abnie:
                        {
                            strNoeFBName = "ابنیه";
                            break;
                        }
                    case NoeFehrestBaha.RahoBand:
                        {
                            strNoeFBName = "";
                            break;
                        }
                    case NoeFehrestBaha.Barghi:
                        {
                            strNoeFBName = "";
                            break;
                        }
                    case NoeFehrestBaha.Mekaniki:
                        {
                            strNoeFBName = "";
                            break;
                        }
                    case NoeFehrestBaha.MareMat:
                        {
                            strNoeFBName = "";
                            break;
                        }
                    default:
                        break;
                }
                resultTadilIndexDto resultTadilIndex = new resultTadilIndexDto
                {
                    NoeFB = NoeFBId,
                    NoeFBName = strNoeFBName,
                };

                //فصل 01 را برای بدست آوردن دوره مبنای پیمان ارسال میکنیم
                List<CourseInfo> coursesForMabnaIndex = await (
                                    from t in _context.TadilIndexFosouls
                                    join f in _context.Fosouls
                                        on t.FosoulId equals f.Id
                                    where f.NoeFB == (NoeFehrestBaha)int.Parse(NoeFB)
                                          && f.Code == "01"
                                          && years.Contains(f.Year)
                                    select new CourseInfo
                                    {
                                        CourseType = t.CourseType,
                                        CourseCode = t.CourseCode,
                                        IndexValue = t.IndexValue,
                                        Year = f.Year
                                    })
                                    .ToListAsync();

                TadilIndexData tadilIndexData = new TadilIndexData();
                CourseInfo? pghPreviousCourse =
                    tadilIndexData.GetPGHPreviousCourse(DTPGHDate??new DateTime(), coursesForMabnaIndex);

                string strMabnaIndex = "";
                //List<ResultItem> resultForMabnaIndex = TadilIndexData.Calculate(
                //strFromDate,
                //strToDate,
                //strPGHDate,
                //coursesForMabnaIndex);
                if (pghPreviousCourse != null)
                {
                    if (pghPreviousCourse.CourseType == 1)
                    {
                        switch (pghPreviousCourse.CourseCode)
                        {
                            case 1:
                                {
                                    strMabnaIndex = " بهار ";
                                    break;
                                }
                            case 2:
                                {
                                    strMabnaIndex = " تابستان ";
                                    break;
                                }
                            case 3:
                                {
                                    strMabnaIndex = " پاییز ";
                                    break;
                                }
                            case 4:
                                {
                                    strMabnaIndex = " زمستان ";
                                    break;
                                }
                            default:
                                break;
                        }

                    }
                    else if (pghPreviousCourse.CourseType == 2)
                    {
                        switch (pghPreviousCourse.CourseCode)
                        {
                            case 1:
                                {
                                    strMabnaIndex = " فروردین ";
                                    break;
                                }
                            case 2:
                                {
                                    strMabnaIndex = " اردیبهشت ";
                                    break;
                                }
                            case 3:
                                {
                                    strMabnaIndex = " خرداد ";
                                    break;
                                }
                            case 4:
                                {
                                    strMabnaIndex = " تیر ";
                                    break;
                                }
                            case 5:
                                {
                                    strMabnaIndex = " مرداد ";
                                    break;
                                }
                            case 6:
                                {
                                    strMabnaIndex = " شهریور ";
                                    break;
                                }
                            case 7:
                                {
                                    strMabnaIndex = " مهر ";
                                    break;
                                }
                            case 8:
                                {
                                    strMabnaIndex = " آبان ";
                                    break;
                                }
                            case 9:
                                {
                                    strMabnaIndex = " آذر ";
                                    break;
                                }
                            case 10:
                                {
                                    strMabnaIndex = " دی ";
                                    break;
                                }
                            case 11:
                                {
                                    strMabnaIndex = " بهمن ";
                                    break;
                                }
                            case 12:
                                {
                                    strMabnaIndex = " اسفند ";
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    strMabnaIndex += Year;
                }

                resultTadilIndex.MabnaIndexTitle = strMabnaIndex;

                requestFosoulForBAAndSVInput.NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB);

                ResultGetFosoulForSooratVaziatDto ResultSV = await showBarAvord.fnGetFosoulForSooratVaziat(requestFosoulForBAAndSVInput, BU, _context);
                List<GetFosoulOutputDto> Fosoul = ResultSV.Fosoul;

                List<GetFosoulOutputDto> FosoulPrev = new List<GetFosoulOutputDto>();

                ResultGetFosoulForSooratVaziatDto ResultSVPrev = new ResultGetFosoulForSooratVaziatDto();
                if (BUPrev != null)
                {
                    GetFosoulForBAAndSVInputDto requestFosoulForBAAndSVInputPrev = new GetFosoulForBAAndSVInputDto
                    {
                        BarAvordUserId = BUPrev.ID,
                        Year = BUPrev.Year,
                        NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB)
                    };

                    ResultSVPrev = await showBarAvord.fnGetFosoulForSooratVaziat(requestFosoulForBAAndSVInputPrev, BUPrev, _context);
                    FosoulPrev = ResultSVPrev.Fosoul;
                }

                var allFosoul = Fosoul
                .UnionBy(FosoulPrev, x => x.Code)
                .ToList();


                List<FaslTadilIndexCourseDto> lstFaslTadilIndexCourse = new List<FaslTadilIndexCourseDto>();
                List<FaslTadilIndexCourseDto> lstFaslTadilIndexCoursePrev = new List<FaslTadilIndexCourseDto>();
                foreach (var Fasl in allFosoul)
                {
                    GetFosoulOutputDto? currentFasl = Fosoul.FirstOrDefault(x => x.Code == Fasl.Code);

                    FaslTadilIndexCourseDto faslTadilIndexCourse = new FaslTadilIndexCourseDto();
                    if (currentFasl != null)
                    {
                        if (currentFasl.JameFaslWithZaribAll != 0 || currentFasl.JameFaslStarWithZarib != 0)
                        {
                            faslTadilIndexCourse.BahayeKolInZarib = currentFasl.JameFaslWithZaribAll;
                            faslTadilIndexCourse.FaslCode = currentFasl.Code;

                            List<CourseInfo> courses;

                            if (currentFasl.Code == "99")
                            {
                                NoeFehrestBaha currentNoeFB = (NoeFehrestBaha)int.Parse(NoeFB);

                                var currentIndexes = await _context.TadilIndexNoeFBs
                                    .Where(x =>
                                        x.NoeFB == currentNoeFB &&
                                        years.Contains(x.Year))
                                    .ToListAsync();

                                var abnieIndexes = await _context.TadilIndexNoeFBs
                                    .Where(x =>
                                        x.NoeFB == NoeFehrestBaha.Abnie &&
                                        years.Contains(x.Year))
                                    .ToListAsync();

                                courses = currentIndexes
                                    .Select(x =>
                                    {
                                        var abnie = abnieIndexes.FirstOrDefault(a =>
                                            a.Year == x.Year &&
                                            a.CourseType == x.CourseType &&
                                            a.CourseCode == x.CourseCode);

                                        decimal finalIndexValue = x.IndexValue;

                                        if (abnie != null)
                                        {
                                            finalIndexValue =
                                                (x.IndexValue + abnie.IndexValue) / 2m;
                                        }

                                        return new CourseInfo
                                        {
                                            Year = x.Year,
                                            CourseType = x.CourseType,
                                            CourseCode = x.CourseCode,
                                            IndexValue = finalIndexValue
                                        };
                                    })
                                    .ToList();
                            }
                            else
                            {
                                courses = await (
                                    from t in _context.TadilIndexFosouls
                                    join f in _context.Fosouls
                                        on t.FosoulId equals f.Id
                                    where f.NoeFB == (NoeFehrestBaha)int.Parse(NoeFB)
                                          && f.Code == currentFasl.Code
                                         // && years.Contains(f.Year)
                                    select new CourseInfo
                                    {
                                        CourseType = t.CourseType,
                                        CourseCode = t.CourseCode,
                                        IndexValue = t.IndexValue,
                                        Year = f.Year
                                    })
                                    .ToListAsync();
                            }

                            List<ResultItem> result = tadilIndexData.Calculate(
                            strFromDate,
                            strToDate,
                            //"1404/05/24",
                            strPGHDate,
                            courses);

                            result= result.OrderBy(x => x.Year).ThenBy(x => x.CourseCode).ToList();

                            if (result.Count != 0)
                            {

                                faslTadilIndexCourse.lstTadilIndex = result;
                            }


                            lstFaslTadilIndexCourse.Add(faslTadilIndexCourse);
                        }
                    }

                    GetFosoulOutputDto? currentFaslPrev = FosoulPrev.FirstOrDefault(x => x.Code == Fasl.Code);

                    if (currentFaslPrev != null)
                    {
                        if (currentFaslPrev.JameFaslWithZaribAll != 0)
                        {
                            faslTadilIndexCourse.BahayeKolInZaribPrev = currentFaslPrev.JameFaslWithZaribAll;

                        }
                    }

                    decimal Deff = 0;
                    Deff = faslTadilIndexCourse.BahayeKolInZaribPrev - faslTadilIndexCourse.BahayeKolInZarib;
                    faslTadilIndexCourse.Deff = Deff;

                    foreach (var item in faslTadilIndexCourse.lstTadilIndex ?? new List<ResultItem>())
                    {
                        item.Nesbat = AllDay == 0
                            ? 0
                            : (decimal)item.Days / AllDay;

                        item.MablaghKarkardDoreh =
                            item.Nesbat * faslTadilIndexCourse.Deff;

                        if (item.PGHIndexValue.HasValue &&
                            item.PGHIndexValue.Value != 0)
                        {
                            item.ZaribTadil =
                                ((item.IndexValue / item.PGHIndexValue.Value) - 1) * 0.95m;
                        }

                        item.MablaghTadil =
                            item.MablaghKarkardDoreh * item.ZaribTadil;

                        item.Nesbat = Math.Round(item.Nesbat, 6);

                        item.ZaribTadil = Math.Round(item.ZaribTadil, 3);

                        item.MablaghTadil = Math.Round(item.MablaghTadil, 0);
                    }


                }
                resultTadilIndex.lstFaslTadilIndex = lstFaslTadilIndexCourse;

                decimal dSumBahayeKolInZarib = lstFaslTadilIndexCourse.Sum(x => x.BahayeKolInZarib);
                resultTadilIndex.SumBahayeKolInZarib = Math.Round(dSumBahayeKolInZarib, 0);


                decimal dSumBahayeKolInZaribPrev = lstFaslTadilIndexCourse.Sum(x => x.BahayeKolInZaribPrev);
                resultTadilIndex.SumBahayeKolInZaribPrev = Math.Round(dSumBahayeKolInZaribPrev, 0);

                decimal dSumDeff = lstFaslTadilIndexCourse.Sum(x => x.Deff);
                resultTadilIndex.SumDeff = Math.Round(dSumDeff, 0);


                //resultTadilIndex.SumNesbat =
                //    lstFaslTadilIndexCourse
                //        .SelectMany(x => x.lstTadilIndex ?? new List<ResultItem>())
                //        .Sum(x => x.Nesbat);

                decimal dSumMablaghKarkardDoreh = lstFaslTadilIndexCourse
                        .SelectMany(x => x.lstTadilIndex ?? new List<ResultItem>())
                        .Sum(x => x.MablaghKarkardDoreh);

                resultTadilIndex.SumMablaghKarkardDoreh = Math.Round(dSumMablaghKarkardDoreh, 0);
                //lstFaslTadilIndexCourse
                //    .SelectMany(x => x.lstTadilIndex ?? new List<ResultItem>())
                //    .Sum(x => x.MablaghKarkardDoreh);

                decimal dSumMablaghTadil = lstFaslTadilIndexCourse
                        .SelectMany(x => x.lstTadilIndex ?? new List<ResultItem>())
                        .Sum(x => x.MablaghTadil);

                resultTadilIndex.SumMablaghTadil = Math.Round(dSumMablaghTadil, 0);


                //resultTadilIndex.AvgZaribTadil =
                //    lstFaslTadilIndexCourse
                //        .SelectMany(x => x.lstTadilIndex ?? new List<ResultItem>())
                //        .Average(x => x.ZaribTadil);

                lstresultTadilIndexDtos.Add(resultTadilIndex);
            }

            var Result = new ResultGetTadilIndex
            {
                ProjectName = strProjectName,
                Title = strTitle,
                FromDate = strFromDate,
                ToDate = strToDate,
                Year = Year,
                AllDay = AllDay,
                lstresultTadilIndexDtos = lstresultTadilIndexDtos,
            };

            return Result;
        }
        else
            return new ResultGetTadilIndex();
    }

    private static readonly PersianCalendar pc = new PersianCalendar();

    public static int GetDaysBetween(string fromDate, string toDate)
    {
        var from = ParsePersianDate(fromDate);
        var to = ParsePersianDate(toDate);

        // اختلاف روزها + 1 (برای احتساب روز اول و آخر)
        return (to - from).Days + 1;
    }

    private static DateTime ParsePersianDate(string date)
    {
        var parts =  date.Split('/');//"1404/12/10".Split('/');//

        int year = int.Parse(parts[0]);
        int month = int.Parse(parts[1]);
        int day = int.Parse(parts[2]);

        return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
    }
    ////////////////////


    public static class DateHelper
    {
        public static string ToEnglishDigits(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace('۰', '0')
                .Replace('۱', '1')
                .Replace('۲', '2')
                .Replace('۳', '3')
                .Replace('۴', '4')
                .Replace('۵', '5')
                .Replace('۶', '6')
                .Replace('۷', '7')
                .Replace('۸', '8')
                .Replace('۹', '9');
        }

        public static List<int> GetYearsInRange(string fromDate, string toDate)
        {
            fromDate = ToEnglishDigits(fromDate);
            toDate = ToEnglishDigits(toDate);

            int fromYear = int.Parse(fromDate.Substring(0, 4));
            int toYear = int.Parse(toDate.Substring(0, 4));

            return Enumerable.Range(fromYear, toYear - fromYear + 1).ToList();
        }
    }
}