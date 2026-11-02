using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.MashinAlat.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using System.Data;
using System.Web.WebPages;
using static RMS.Models.Common.EnumForEntity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMS.Controllers.MashinAlat;

public class MashinAlatController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewMashinAlatForm");

        return View();
    }

    [HttpPost]
    public JsonResult GetInfoFBOfOperationAndAnalizBaha([FromBody] GetInfoFBOfOperationAndAnalizBahaDto request)
    {
        //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
        //DataTable DtOperation = clsOperation_ItemsFB.OperationListWithParameter("");

        int Id = 1;
        int Year = request.Year;
        NoeFehrestBaha NoeFB = request.NoeFB;
        Guid BarAvordId = request.BarAvordId;

        List<OperationForGetInfoFBOfOperationAndAnalizBahaDto> lstOperation = _context.Operations.Where(x => x.Year == Year).Select(x => new OperationForGetInfoFBOfOperationAndAnalizBahaDto
        {
            Id = x.Id,
            Year = x.Year,
            LatinName = x.LatinName,
            OperationName = x.OperationName,
            Order = x.Order,
            ParentId = x.ParentId
        }).ToList();

        //DataTable DtOperation = clsConvert.ToDataTable(lstOperation);

        var vId = new SqlParameter("@Id", 1404004);
        var vYear = new SqlParameter("@Year", Year);
        var vNoeFB = new SqlParameter("@NoeFB", NoeFB);
        var vBarAvordUserId = new SqlParameter("@BarAvordId", BarAvordId); //BarAvordId);

        var OperationNameAnalizBahaWithOperation = _context.Set<OperationNameAnalizBahaWithOperationDto>()
            .FromSqlRaw("EXEC OperationNameAnalizBahaWithOperationId @Id,@Year,@NoeFB,@BarAvordId", vId, vYear, vNoeFB, vBarAvordUserId)
            .ToList();
        // DataTable DtInfoFBOfOperationAndAnalizBaha = clsConvert.ToDataTable(OperationNameAnalizBahaWithOperation);// clsOperation_ItemsFB.GetInfoFBOfOperationAndAnalizBaha(1, 1397, 234, 156);

        var GetOperationNameHasValue = _context.Set<GetOperationNameHasValueDto>()
            .FromSqlRaw("EXEC GetOperationNameHasValue @Id,@Year,@NoeFB,@BarAvordId", vId, vYear, vNoeFB, vBarAvordUserId)
            .ToList();
        //DataTable DtOperationName = clsConvert.ToDataTable(GetOperationNameHasValue);

        //DataTable DtOperationName = clsOperation_ItemsFB.GetOperationNameHasValue(1, 1397, 234, 156);
        //DtInfoFBOfOperationAndAnalizBaha.TableName = "tblNiroZaribKarKard";
        //DataSet Ds = new DataSet();
        //Ds.Tables.Add(DtInfoFBOfOperationAndAnalizBaha);

        //DtOperationName.Columns.Add("MaxMeghdarMeghias");
        //DtOperationName.Columns.Add("MaxCodeAmelMoaser");

        decimal fltMaxAmelMoaser = 0;
        string strMaxCodeAmelMoaser = "";
        //DataRow[] DrNiroZaribKarKard = null;

        //for (int i = 0; i < GetOperationNameHasValue.Count;i++)// DtOperationName.Rows.Count; i++)
        //{
        foreach (var OperationNameHasValue in GetOperationNameHasValue)
        {
            fltMaxAmelMoaser = 0;
            List<OperationNameAnalizBahaWithOperationDto> DrNiroZaribKarKard = OperationNameAnalizBahaWithOperation.Where(x => x.OperationExceptOpNodeLatinName == OperationNameHasValue.OperationLatinName).ToList(); //DtOperationName.Rows[i]["OperationLatinName"].ToString().Trim() + "'");
            if (DrNiroZaribKarKard.Count != 0)
            {
                //DataTable DtNiroZaribKarKardNew = DrNiroZaribKarKard.CopyToDataTable();
                //DataRow[] drHasAmelMoaser = DtNiroZaribKarKardNew.Select("IsAmelMoaser=1");
                List<OperationNameAnalizBahaWithOperationDto> drHasAmelMoaser = DrNiroZaribKarKard.Where(x => x.IsAmelMoaser == true).ToList();
                //if (drHasAmelMoaser.Length != 0)
                if (drHasAmelMoaser.Count != 0)
                {
                    //DataTable dtHasAmelMoaser = drHasAmelMoaser.CopyToDataTable();
                    foreach (var HasAmelMoaser in drHasAmelMoaser)
                    {
                        //}
                        //for (int j = 0; j < dtHasAmelMoaser.Rows.Count; j++)
                        //{
                        if (HasAmelMoaser.MeghdarMeghias > fltMaxAmelMoaser)
                        {
                            //}
                            //if (float.Parse(dtHasAmelMoaser.Rows[j]["MeghdarMeghias"].ToString().Trim()) > fltMaxAmelMoaser)
                            //{
                            strMaxCodeAmelMoaser = HasAmelMoaser.CodeAmel;// dtHasAmelMoaser.Rows[j]["CodeAmel"].ToString();
                            fltMaxAmelMoaser = HasAmelMoaser.MeghdarMeghias; //float.Parse(dtHasAmelMoaser.Rows[j]["MeghdarMeghias"].ToString());
                        }
                    }
                }
                else
                {
                    foreach (var HasAmelMoaser in drHasAmelMoaser)
                    {
                        if (HasAmelMoaser.MeghdarMeghias > fltMaxAmelMoaser)
                        {
                            strMaxCodeAmelMoaser = HasAmelMoaser.CodeAmel;// dtHasAmelMoaser.Rows[j]["CodeAmel"].ToString();
                            fltMaxAmelMoaser = HasAmelMoaser.MeghdarMeghias; //float.Parse(dtHasAmelMoaser.Rows[j]["MeghdarMeghias"].ToString());
                        }
                    }

                    //for (int j = 0; j < DtNiroZaribKarKardNew.Rows.Count; j++)
                    //{
                    //    if (float.Parse(DtNiroZaribKarKardNew.Rows[j]["MeghdarMeghias"].ToString().Trim()) > fltMaxAmelMoaser)
                    //    {
                    //        strMaxCodeAmelMoaser = DtNiroZaribKarKardNew.Rows[j]["CodeAmel"].ToString();
                    //        fltMaxAmelMoaser = float.Parse(DtNiroZaribKarKardNew.Rows[j]["MeghdarMeghias"].ToString());
                    //    }
                    //}
                }
            }

            OperationNameHasValue.MaxMeghdarMeghias = fltMaxAmelMoaser.ToString();
            OperationNameHasValue.MaxCodeAmelMoaser = strMaxCodeAmelMoaser;

            //DtOperationName.Rows[i]["MaxMeghdarMeghias"] = fltMaxAmelMoaser;
            //DtOperationName.Rows[i]["MaxCodeAmelMoaser"] = strMaxCodeAmelMoaser;
        }
        //DtOperationName.TableName = "tblOperationName";
        //Ds.Tables.Add(DtOperationName);

        //DtOperation.TableName = "tblOperation";
        //Ds.Tables.Add(DtOperation);

        var result = new
        {
            //DtOperationName,
            //DtOperation,
            //DtInfoFBOfOperationAndAnalizBaha
            GetOperationNameHasValue,
            lstOperation,
            OperationNameAnalizBahaWithOperation
        };
        //return new JsonResult("OK");
        return new JsonResult(result);
    }

    public JsonResult changeAmelMoaser([FromBody] ChangeAmelMoaserDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        string CodeAmel = request.CodeAmel;
        long OperationId = request.OperationId;


        return new JsonResult("OK");
    }

    public JsonResult GetGeneralProjectTiming([FromBody] ChangeAmelMoaserDto request)
    {
        List<clsProjectCalendar> projectCalendars = _context.ProjectCalendars.ToList();

        return new JsonResult(projectCalendars);
    }

    public JsonResult SaveGeneralProjectTiming([FromBody] SaveGeneralProjectTimingDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        string StartDate = request.StartDate;
        int CountDaies = request.CountDaies;
        int CourseCountDaies = request.CourseCountDaies;
        int HolyCourseCountDaies = request.HolyCourseCountDaies;

        clsGeneralProjectTiming GeneralProjectTiming = new clsGeneralProjectTiming
        {
            BaravordUserId = BarAvordId,
            StartDate = DateTime.Parse(StartDate),
            CountDaies = CountDaies,
            CourseCountDaies = CourseCountDaies,
            HolyCourseCountDaies = HolyCourseCountDaies,
        };

        _context.GeneralProjectTimings.Add(GeneralProjectTiming);
        _context.SaveChanges();

        return new JsonResult("OK");
    }

    public JsonResult UpdateGeneralProjectTiming([FromBody] UpdateGeneralProjectTimingDto request)
    {
        Guid Id = request.Id;
        string StartDate = request.StartDate;
        int CountDaies = request.CountDaies;
        int CourseCountDaies = request.CourseCountDaies;
        int HolyCourseCountDaies = request.HolyCourseCountDaies;

        //clsGeneralProjectTiming GeneralProjectTiming = new clsGeneralProjectTiming
        //{
        //    //ProjectId = ProjectId,
        //    StartDate = DateTime.Parse(StartDate),
        //    CountDaies = CountDaies,
        //    CourseCountDaies = CourseCountDaies,
        //    HolyCourseCountDaies = HolyCourseCountDaies,
        //};

        //_context.GeneralProjectTimings.Add(GeneralProjectTiming);
        //_context.SaveChanges();

        return new JsonResult("OK");
    }

    [HttpPost]
    public JsonResult GetStatusOfDateInPeriod([FromBody] GetStatusOfDateInPeriodDto request)
    {

        Guid BarAvordUserId = request.BarAvordUserId;
        string StartDate = request.StartDate;
        int ProjectLen = request.ProjectLen;
        int CourseCountDaies = request.CourseCountDaies;
        int HolyCourseCountDaies = request.HolyCourseCountDaies;


        List<clsGeneralProjectTiming> lstGeneralProjects = _context.GeneralProjectTimings.ToList();//.Where(x => x.ProjectId == ProjectId)
        //DataTable Dt = clsGeneralProjectTiming.ListWithParametr("ProjectId=" + ProjectId);
        //DataTable Dt = clsConvert.ToDataTable(lstGeneralProjects);

        int Year = int.Parse(StartDate.Trim().Split('/')[0]);
        int Mon = int.Parse(StartDate.Trim().Split('/')[1]);
        int Day = int.Parse(StartDate.Trim().Split('/')[2]);
        DateTime dt = SolarDate.ToMiladi(Year, Mon, Day);

        decimal dWeekPeriod = Math.Round(decimal.Parse(CourseCountDaies.ToString()) / 7, 0);
        decimal dWeekNum = 1;
        MypersianDate date = new MypersianDate(dt);
        int WeekNum = date.persianweekNum();
        string WeekName = "";
        decimal dDaies = WeekNum;

        int intModatFaliat = ProjectLen;

        DataTable DtZamanbandiProjeTafsiliTemp = new DataTable();
        DtZamanbandiProjeTafsiliTemp.Columns.Add("Date");
        DtZamanbandiProjeTafsiliTemp.Columns.Add("DayNum");
        DtZamanbandiProjeTafsiliTemp.Columns.Add("DayName");
        DtZamanbandiProjeTafsiliTemp.Columns.Add("NoeTatili");
        DtZamanbandiProjeTafsiliTemp.Columns.Add("Active");

        Guid idGeneralProjectTimingId = new Guid();
        if (lstGeneralProjects.Count != 0)
        {
            idGeneralProjectTimingId = lstGeneralProjects.First().ID;
            List<clsProjectCalendar> lstProjectCalendars = _context.ProjectCalendars.Where(x => x.GeneralProjectTimingId == idGeneralProjectTimingId).ToList(); //Guid.Parse(Dt.Rows[0]["Id"].ToString())).ToList();
                                                                                                                                                                //DataTable DtProjectCalendar = clsProjectCalendar.ListWithParametr("GeneralProjectTimingId=" + Dt.Rows[0]["Id"].ToString());
            DataTable DtProjectCalendar = clsConvert.ToDataTable(lstProjectCalendars);

            for (int i = 0; i < intModatFaliat; i++)
            {
                DataRow DrZamanbandiProjeTafsili = DtZamanbandiProjeTafsiliTemp.NewRow();

                date = new MypersianDate(dt);
                DrZamanbandiProjeTafsili["Date"] = date.simpleDate();
                WeekName = date.persianweek();
                DrZamanbandiProjeTafsili["DayName"] = WeekName;
                WeekNum = date.persianweekNum();
                DrZamanbandiProjeTafsili["DayNum"] = WeekNum;
                DrZamanbandiProjeTafsili["Active"] = true;

                string strTarikh = SolarDate.ToPersianNew(dt);

                if (CourseCountDaies < 15)
                {
                    switch (HolyCourseCountDaies)
                    {
                        case 1:
                            {
                                if (WeekNum == 6)
                                {
                                    DrZamanbandiProjeTafsili["NoeTatili"] = 1;

                                    string[] strDateSplit = strTarikh.Trim().Split('/');
                                    int intTarikhYear = int.Parse(strDateSplit[0]);
                                    int intTarikhM = int.Parse(strDateSplit[1]);
                                    int intTarikhD = int.Parse(strDateSplit[2]);
                                    DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                    if (DrProjectCalendar.Length != 0)
                                    {
                                        if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = true;
                                        }
                                        else
                                        {
                                            intModatFaliat++;
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                        }
                                    }
                                    else
                                    {
                                        intModatFaliat++;
                                        DrZamanbandiProjeTafsili["Active"] = false;
                                    }
                                }
                                else
                                {
                                    DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                                    string[] strDateSplit = strTarikh.Trim().Split('/');
                                    int intTarikhYear = int.Parse(strDateSplit[0]);
                                    int intTarikhM = int.Parse(strDateSplit[1]);
                                    int intTarikhD = int.Parse(strDateSplit[2]);
                                    DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                    if (DrProjectCalendar.Length != 0)
                                    {
                                        if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                            intModatFaliat++;
                                        }
                                        else
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = true;
                                        }
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                if (WeekNum == 6 || WeekNum == 5)
                                {
                                    DrZamanbandiProjeTafsili["NoeTatili"] = 1;

                                    string[] strDateSplit = strTarikh.Trim().Split('/');
                                    int intTarikhYear = int.Parse(strDateSplit[0]);
                                    int intTarikhM = int.Parse(strDateSplit[1]);
                                    int intTarikhD = int.Parse(strDateSplit[2]);
                                    DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                    if (DrProjectCalendar.Length != 0)
                                    {
                                        if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = true;
                                        }
                                        else
                                        {
                                            intModatFaliat++;
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                        }
                                    }
                                    else
                                    {
                                        intModatFaliat++;
                                        DrZamanbandiProjeTafsili["Active"] = false;
                                    }
                                }
                                else
                                {
                                    DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                                    string[] strDateSplit = strTarikh.Trim().Split('/');
                                    int intTarikhYear = int.Parse(strDateSplit[0]);
                                    int intTarikhM = int.Parse(strDateSplit[1]);
                                    int intTarikhD = int.Parse(strDateSplit[2]);
                                    DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                    if (DrProjectCalendar.Length != 0)
                                    {
                                        if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                            intModatFaliat++;
                                        }
                                        else
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = true;
                                        }
                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                if (WeekNum == 6 || WeekNum == 5 || WeekNum == 4)
                                {
                                    DrZamanbandiProjeTafsili["NoeTatili"] = 1;

                                    string[] strDateSplit = strTarikh.Trim().Split('/');
                                    int intTarikhYear = int.Parse(strDateSplit[0]);
                                    int intTarikhM = int.Parse(strDateSplit[1]);
                                    int intTarikhD = int.Parse(strDateSplit[2]);
                                    DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                    if (DrProjectCalendar.Length != 0)
                                    {
                                        if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = true;
                                        }
                                        else
                                        {
                                            intModatFaliat++;
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                        }
                                    }
                                    else
                                    {
                                        intModatFaliat++;
                                        DrZamanbandiProjeTafsili["Active"] = false;
                                    }
                                }
                                else
                                {
                                    DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                                    string[] strDateSplit = strTarikh.Trim().Split('/');
                                    int intTarikhYear = int.Parse(strDateSplit[0]);
                                    int intTarikhM = int.Parse(strDateSplit[1]);
                                    int intTarikhD = int.Parse(strDateSplit[2]);
                                    DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                    if (DrProjectCalendar.Length != 0)
                                    {
                                        if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                            intModatFaliat++;
                                        }
                                        else
                                        {
                                            DrZamanbandiProjeTafsili["Active"] = true;
                                        }
                                    }
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    if ((dWeekNum % dWeekPeriod) == 0)
                    {
                        switch (HolyCourseCountDaies)
                        {
                            case 1:
                                {
                                    if (WeekNum == 6)
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 1;

                                        string[] strDateSplit = strTarikh.Trim().Split('/');
                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                            else
                                            {
                                                intModatFaliat++;
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                            }
                                        }
                                        else
                                        {
                                            intModatFaliat++;
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                        }
                                    }
                                    else
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                                        string[] strDateSplit = strTarikh.Trim().Split('/');
                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                                intModatFaliat++;
                                            }
                                            else
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                        }
                                    }

                                    break;
                                }
                            case 2:
                                {
                                    if (WeekNum == 6 || WeekNum == 5)
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 1;

                                        string[] strDateSplit = strTarikh.Trim().Split('/');
                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                            else
                                            {
                                                intModatFaliat++;
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                            }
                                        }
                                        else
                                        {
                                            intModatFaliat++;
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                        }
                                    }
                                    else
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                                        string[] strDateSplit = strTarikh.Trim().Split('/');
                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                                intModatFaliat++;
                                            }
                                            else
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                        }
                                    }

                                    break;
                                }
                            case 3:
                                {
                                    if (WeekNum == 6 || WeekNum == 5 || WeekNum == 4)
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 1;

                                        string[] strDateSplit = strTarikh.Trim().Split('/');
                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                            else
                                            {
                                                intModatFaliat++;
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                            }
                                        }
                                        else
                                        {
                                            intModatFaliat++;
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                        }
                                    }
                                    else
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                                        string[] strDateSplit = strTarikh.Trim().Split('/');
                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                                intModatFaliat++;
                                            }
                                            else
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                        }
                                    }

                                    break;
                                }
                            case 4:
                                {
                                    if (WeekNum == 6 || WeekNum == 5 || WeekNum == 4 || WeekNum == 3)
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 1;
                                        string[] strDateSplit = strTarikh.Trim().Split('/');

                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                            else
                                            {
                                                intModatFaliat++;
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                            }
                                        }
                                        else
                                        {
                                            intModatFaliat++;
                                            DrZamanbandiProjeTafsili["Active"] = false;
                                        }
                                    }
                                    else
                                    {
                                        DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                                        string[] strDateSplit = strTarikh.Trim().Split('/');
                                        int intTarikhYear = int.Parse(strDateSplit[0]);
                                        int intTarikhM = int.Parse(strDateSplit[1]);
                                        int intTarikhD = int.Parse(strDateSplit[2]);
                                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                                        if (DrProjectCalendar.Length != 0)
                                        {
                                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = false;
                                                intModatFaliat++;
                                            }
                                            else
                                            {
                                                DrZamanbandiProjeTafsili["Active"] = true;
                                            }
                                        }
                                    }

                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    else
                    {
                        DrZamanbandiProjeTafsili["NoeTatili"] = 0;

                        string[] strDateSplit = strTarikh.Trim().Split('/');
                        int intTarikhYear = int.Parse(strDateSplit[0]);
                        int intTarikhM = int.Parse(strDateSplit[1]);
                        int intTarikhD = int.Parse(strDateSplit[2]);
                        DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                        if (DrProjectCalendar.Length != 0)
                        {
                            if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "False")
                            {
                                DrZamanbandiProjeTafsili["Active"] = false;
                                intModatFaliat++;
                            }
                            else
                            {
                                DrZamanbandiProjeTafsili["Active"] = true;
                            }
                        }
                    }

                    if ((dDaies % 7) == 0)
                        dWeekNum++;
                    dDaies++;
                }

                if (date.IsHoly())
                {
                    DrZamanbandiProjeTafsili["NoeTatili"] = 2;
                    string[] strDateSplit = strTarikh.Trim().Split('/');
                    int intTarikhYear = int.Parse(strDateSplit[0]);
                    int intTarikhM = int.Parse(strDateSplit[1]);
                    int intTarikhD = int.Parse(strDateSplit[2]);
                    DataRow[] DrProjectCalendar = DtProjectCalendar.Select("Date='" + intTarikhYear + "/" + intTarikhM.ToString("D2") + "/" + intTarikhD.ToString("D2") + "'");
                    if (DrProjectCalendar.Length != 0)
                    {
                        if (DrProjectCalendar[0]["ActiveDeActive"].ToString().Trim() == "True")
                        {
                            DrZamanbandiProjeTafsili["Active"] = true;
                        }
                        else
                        {
                            intModatFaliat++;
                            DrZamanbandiProjeTafsili["Active"] = false;
                        }
                    }
                    else
                    {
                        intModatFaliat++;
                        DrZamanbandiProjeTafsili["Active"] = false;
                    }

                }

                DtZamanbandiProjeTafsiliTemp.Rows.Add(DrZamanbandiProjeTafsili);

                dt = dt.AddDays(1);
            }
        }

        //DataSet Ds = new DataSet();
        //Ds.Tables.Add(DtZamanbandiProjeTafsiliTemp);
        //DtZamanbandiProjeTafsiliTemp.TableName = "tblZamanbandiProjeTafsiliTemp";


        var list = DtZamanbandiProjeTafsiliTemp.AsEnumerable()
            .Select(row => new ResultZamanbandiProjeTafsiliDto
        {
            Date = row.Field<string>("Date"),
            DayNum = row.Field<string>("DayNum"),
            DayName = row.Field<string>("DayName"),
            NoeTatili = row.Field<string>("NoeTatili"),
            Active = row.Field<string>("Active")
        }).ToList();


        return new JsonResult(list);//.GetXml();
    }


    public JsonResult SaveDayState([FromBody] SaveDayStateDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        string Date = request.Date;
        string State = request.State;
        string ActiveDeActive = request.ActiveDeActive;

        List<clsGeneralProjectTiming> lstGeneralProjectTiming = _context.GeneralProjectTimings.ToList();
        DataTable Dt = clsConvert.ToDataTable(lstGeneralProjectTiming);

        //DataTable Dt = clsGeneralProjectTiming.ListWithParametr("ProjectId=" + ProjectId);
        //DataTable DtProjectCalendar = clsProjectCalendar.ListWithParametr("GeneralProjectTimingId=" + Dt.Rows[0]["Id"].ToString());
        bool activeDeactive = Convert.ToBoolean(ActiveDeActive);
        //var activeDeactiveNum=Convert.ToByte(activeDeactive);

        var state = Convert.ToInt16(State);
        if (state == 1 || state == 2)
        {
            if (activeDeactive)
            {
                clsProjectCalendar ProjectCalendar = new clsProjectCalendar();
                ProjectCalendar.ActiveDeActive = true;
                ProjectCalendar.Date = DateTime.Parse(Date.Trim());
                ProjectCalendar.GeneralProjectTimingId = Guid.Parse(Dt.Rows[0]["Id"].ToString());
                ProjectCalendar.State = state;

                _context.ProjectCalendars.Add(ProjectCalendar);

                //ProjectCalendar.Save();
            }
            else
            {
                //clsProjectCalendar.DeleteWithParameter("GeneralProjectTimingId=" + Dt.Rows[0]["Id"].ToString().Trim()
                //    + " and Date='" + Date.Trim() + "' and State=" + State + " and ActiveDeActive='" + true + "'");
            }
        }
        else if (state == 0)
        {
            if (!activeDeactive)
            {
                clsProjectCalendar ProjectCalendar = new clsProjectCalendar();
                ProjectCalendar.ActiveDeActive = false;
                ProjectCalendar.Date = DateTime.Parse(Date.Trim());
                ProjectCalendar.GeneralProjectTimingId = Guid.Parse(Dt.Rows[0]["Id"].ToString());
                ProjectCalendar.State = state;

                _context.ProjectCalendars.Add(ProjectCalendar);

                // ProjectCalendar.Save();
            }
            else
            {
                //clsProjectCalendar.DeleteWithParameter("GeneralProjectTimingId=" + Dt.Rows[0]["Id"].ToString().Trim()
                //   + " and Date='" + Date.Trim() + "' and State=" + State + " and ActiveDeActive='" + false + "'");
            }
        }

        _context.SaveChanges();
        return new JsonResult("OK");

    }
}
