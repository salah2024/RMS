using System;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Models.Common.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using RMS.Controllers.BarAvordUser.Common;

namespace RMS.Controllers.BaseInfo;

public class BaseInfoController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    // GET: BaseInfo
    public ActionResult Index()
    {
        var baseInfo = _context.BaseInfos.OrderBy(f => f.Priority).ToList();
        return View(baseInfo);
    }

    // GET: BaseInfo/Details/5
    public ActionResult Details(long id)
    {
        var baseInfo = _context.BaseInfos.Where(x => x.Id == id).ToList();
        return View(baseInfo);
    }

    public ActionResult GetConditionDes([FromBody] GetConditionDesInputDto request)
    {
        clsConditionContext? CC = _context.ConditionContexts.Where(x => x.Id == request.ConditionContextId).FirstOrDefault();
        if (CC != null)
        {
            return new JsonResult(CC);
        }
        else
            return new JsonResult(null);
    }

    //[HttpPost]
    //public JsonResult GetBaseInfoNoeFB(string LatinName)
    //{
    //    var lstbaseInfo = (from BIT in _context.BaseInfoTypes
    //                       join
    //                              BI in _context.BaseInfos on BIT.Id equals BI.TypeId
    //                       where BIT.LatinName == LatinName
    //                       select new
    //                       {
    //                           BI.Name,
    //                           BI.Id,
    //                           BI.Priority
    //                       }).OrderBy(x => x.Priority).ToList();
    //    return new JsonResult(lstbaseInfo);
    //}


    // GET: BaseInfo/Create
    //public ActionResult Create()
    //{
    //    clsBaseInfo baseInfo = new clsBaseInfo();
    //    var BaseInfoType = baseInfo.BaseInfoTypeItems.FirstOrDefault();
    //    baseInfo.Priority = GetLastPriority(Guid.Parse(BaseInfoType.Value));
    //    return View(baseInfo);
    //}

    [HttpPost]
    public async Task<JsonResult> SetViewBag([FromBody] SetViewBagInputDto request)
    {
        Guid BAId = request.BAId;
        string strProjectTitle = "";
        int BAType = request.BAType;
        if (BAType == 3)
        {
            clsBaravordUser CurrentBarAvord = await _context.BaravordUsers.FirstAsync(x => x.ID == BAId);
            clsBaravordUser? ParentBarAvord = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == CurrentBarAvord.ParentId);
            if (ParentBarAvord != null)
            {
                strProjectTitle = ParentBarAvord.Name;
            }
        }
        else if (BAType == 2)
        {
            clsBaravordUser CurrentBarAvord = await _context.BaravordUsers.FirstAsync(x => x.ID == BAId);
            strProjectTitle = CurrentBarAvord.Name;
        }

        int Year = request.Year;
        //NoeFehrestBaha NoeFB = request.NoeFB;

        TempData["BAId"] = BAId;
        TempData["BAType"] = BAType;
        TempData["Year"] = Year;
        //TempData["NoeFB"] = NoeFB;
        return new JsonResult("OK_" + strProjectTitle);
    }

    [HttpPost]
    public JsonResult SetViewBagEmpty()
    {
        TempData["BAId"] = "";
        TempData["Year"] = "";
        TempData["NoeFB"] = "";
        TempData["UserName"] = "";
        TempData["BAType"] = "";
        return new JsonResult("OK");
    }

    [HttpGet]
    public JsonResult GetViewBag()
    {
        try
        {
            if (TempData.Count != 0)
            {
                if (TempData["BAId"] != null)
                {
                    string BAId = TempData["BAId"].ToString();
                    string BAType = TempData["BAType"].ToString();
                    string Year = TempData["Year"].ToString();
                    //string NoeFB = TempData["NoeFB"].ToString();
                    TempData["BAId"] = BAId;
                    TempData["BAType"] = BAType;
                    TempData["Year"] = Year;
                    //TempData["NoeFB"] = NoeFB;
                    //DataTable Dt = new DataTable();
                    //Dt.Columns.Add("BAId");
                    //Dt.Columns.Add("Year");
                    //Dt.Columns.Add("NoeFB");

                    //DataRow Dr = Dt.NewRow();
                    //Dr["BAId"] = BAId;
                    //Dr["Year"] = Year;
                    //Dr["NoeFB"] = NoeFB;
                    //Dt.Rows.Add(Dr);
                    //DataSet Ds = new DataSet();
                    //Ds.Tables.Add(Dt);

                    ViewBagDataDto viewBag = new ViewBagDataDto
                    {
                        BAId = BAId == null ? "" : BAId,
                        BAType = BAType == null ? "" : BAType,
                        Year = Year == null ? "" : Year,
                    };

                    if (BAId != null)
                    {
                        Guid BaravordId = Guid.Parse(BAId);
                        clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == BaravordId);

                        if (baravordUser != null)
                        {
                            viewBag.BarAvordName = baravordUser.Name;
                            viewBag.BAType = baravordUser.Type.ToString();
                            string NoeFBs = baravordUser.NoeFBs;
                            if (NoeFBs != "")
                            {
                                string Title = baravordUser.Name;
                                if (baravordUser.Type == NoeBarAvord.BarAvord)
                                {

                                }
                                else if (baravordUser.Type == NoeBarAvord.Gharardad || baravordUser.Type == NoeBarAvord.SooratVaziat)
                                {
                                    clsBaravordUser? ParentBaravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == baravordUser.ParentId);

                                    if (ParentBaravordUser != null)
                                    {

                                        Title = ParentBaravordUser.Name + " - " + Title;
                                    }
                                }
                                List<NoeFBDto> lstNoeFBs = new List<NoeFBDto>();
                                string[] strNoeFBs = NoeFBs.Split(",");
                                foreach (var item in strNoeFBs)
                                {
                                    NoeFBDto noeFB = new NoeFBDto();
                                    NoeFehrestBaha NoeFBItem = (NoeFehrestBaha)int.Parse(item);
                                    switch (NoeFBItem)
                                    {
                                        case NoeFehrestBaha.RahDari:
                                            {
                                                noeFB.Id = 22;
                                                noeFB.Name = "راهداری";
                                                break;
                                            }
                                        case NoeFehrestBaha.Abnie:
                                            {
                                                noeFB.Id = 11;
                                                noeFB.Name = "ابنیه";
                                                break;
                                            }
                                        case NoeFehrestBaha.RahoBand:
                                            {
                                                noeFB.Id = 21;
                                                noeFB.Name = "راه، راه‌آھن و باند فرودگاه";
                                                break;
                                            }
                                        case NoeFehrestBaha.Barghi:
                                            {
                                                noeFB.Id = 13;
                                                noeFB.Name = "تاسیسات برقی";
                                                break;
                                            }
                                        case NoeFehrestBaha.Mekaniki:
                                            {
                                                noeFB.Id = 12;
                                                noeFB.Name = "تاسیسات مکانیکی";
                                                break;
                                            }
                                        case NoeFehrestBaha.MareMat:
                                            {
                                                noeFB.Id = 16;
                                                noeFB.Name = "مرمت بناهای تاریخی";
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    lstNoeFBs.Add(noeFB);
                                }

                                var result = new
                                {
                                    viewBag,
                                    lstNoeFBs,
                                    Title
                                };
                                return new JsonResult(result);
                            }
                            else
                                return new JsonResult("NOK");
                        }
                        else
                            return new JsonResult("NOK");
                    }
                    else
                        return new JsonResult("NOK");
                }
                else
                    return new JsonResult("NOK");
            }
            else
                return new JsonResult("NOK");

        }
        catch (Exception e)
        {
            // ریدایرکت به داشبورد
            return new JsonResult("NOK");
        }
    }

    [HttpPost]
    public async Task<JsonResult> GetFosoulForBarAvord([FromBody] GetFosoulForBAAndSVInputDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;


        //List<GetFosoulOutputDto> Fosoul = await showBarAvord.AllFosoulBarAvord(request, BaravordUser, _context);

        //List<clsVahed> lstVaheds = await _context.Vaheds.ToListAsync();

        //var result = new ResultGetFosoulForBarAvordDto
        //{
        //    lstVaheds = lstVaheds,
        //    Fosoul = Fosoul,
        //};

        ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

        ResultGetFosoulForBarAvordDto Result = await showBarAvord.fnGetFosoulForBarAvord(request, _context);

        return new JsonResult(Result);
    }

    [HttpPost]
    public async Task<JsonResult> GetFosoulForSooratVaziat([FromBody] GetFosoulForBAAndSVInputDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;
        string Code = request.Code;

        //List<GetFosoulOutputDto> Fosoul = await showBarAvord.AllFosoulBarAvord(request, BaravordUser, _context);

        //List<clsVahed> lstVaheds = await _context.Vaheds.ToListAsync();

        //var result = new ResultGetFosoulForBarAvordDto
        //{
        //    lstVaheds = lstVaheds,
        //    Fosoul = Fosoul,
        //};

        ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

        clsBaravordUser? BaravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (BaravordUser == null)
        {
            return new JsonResult(null);
        }
        else
        {

            ResultGetFosoulForSooratVaziatDto Result = await showBarAvord.fnGetFosoulForSooratVaziat(request, BaravordUser, _context);
            if (Code != "")
            {
                GetFosoulOutputDto? Fasl = Result.Fosoul.FirstOrDefault(x => x.Code == Code);
                if (Fasl!=null) 
                    Result.CurrentFasl = Fasl;
            }
            return new JsonResult(Result);
        }

    }


    [HttpPost]
    public async Task<JsonResult> GetSumAllFBForSooratVaziat([FromBody] GetFosoulForBAAndSVInputDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        //NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

        clsBaravordUser? BaravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (BaravordUser == null)
        {
            return new JsonResult(null);
        }
        else
        {
            string[] NoeFBs = BaravordUser.NoeFBs.Split(",");

            decimal dJamAllFB = 0;
            foreach (var NoeFB in NoeFBs)
            {
                NoeFehrestBaha NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB);
                request.NoeFBId = NoeFBId;
                ResultGetFosoulForSooratVaziatDto Result = await showBarAvord.fnGetFosoulForSooratVaziat(request, BaravordUser, _context);

                dJamAllFB += Result.SumFosoulForSooratVaziat.dJameFaslWithTajhizWithZarib;
            }


            return new JsonResult(dJamAllFB);
        }
    }

    [HttpPost]
    public async Task<JsonResult> GetSumAllFosoulForSooratVaziat([FromBody] GetFosoulForBAAndSVInputDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

        clsBaravordUser? BaravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (BaravordUser == null)
        {
            return new JsonResult(null);
        }
        else
        {

            decimal dJamAllFB = 0;

            ResultGetFosoulForSooratVaziatDto Result = await showBarAvord.fnGetFosoulForSooratVaziat(request, BaravordUser, _context);

            dJamAllFB += Result.SumFosoulForSooratVaziat.dJameFaslWithTajhizWithZarib;

            return new JsonResult(dJamAllFB);
        }
    }


    [HttpPost]
    public JsonResult GetDescriptionForFasl([FromBody] GetDescriptionInputForFaslDto request)
    {
        GetFosoulOutputForFaslDto? Description = _context.Fosouls.Where(x => x.Id == request.Id)
            .Select(x => new GetFosoulOutputForFaslDto
            {
                Title = x.Name,
                Description = x.Description,
            }).FirstOrDefault();

        return new JsonResult(Description);
    }

    [HttpGet]
    public JsonResult GetUserName()
    {
        try
        {
            string UserName = TempData["UserName"].ToString();
            TempData["UserName"] = UserName;
            DataTable Dt = new DataTable();
            Dt.Columns.Add("UserName");

            DataRow Dr = Dt.NewRow();
            Dr["UserName"] = UserName;
            Dt.Rows.Add(Dr);
            DataSet Ds = new DataSet();
            Dt.TableName = "tblUserName";
            Ds.Tables.Add(Dt);
            return new JsonResult(Ds.GetXml());
        }
        catch (Exception)
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add("UserName");

            DataRow Dr = Dt.NewRow();
            Dr["UserName"] = "";
            Dt.Rows.Add(Dr);
            DataSet Ds = new DataSet();
            Dt.TableName = "tblUserName";
            Ds.Tables.Add(Dt);
            return new JsonResult(Ds.GetXml());
        }
    }

    //[HttpPost]
    //public int GetLastPriority(Guid typeId)
    //{
    //    clsBaseInfo baseInfo = _context.BaseInfos.Where(f => f.TypeId == typeId).OrderByDescending(u => u.Priority).FirstOrDefault();
    //    int intpriority = 0;
    //    if (baseInfo == null)
    //        intpriority = 1;
    //    else
    //        intpriority = baseInfo._Priority + 1;
    //    return intpriority;
    //}

    // POST: BaseInfo/Create
    [HttpPost]
    public ActionResult Create(clsBaseInfo baseInfo)
    {
        try
        {
            // TODO: Add insert logic here
            _context.BaseInfos.Add(baseInfo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    // GET: BaseInfo/Edit/5
    public ActionResult Edit(long id)
    {
        clsBaseInfo? baseInfo = _context.BaseInfos.Where(x => x.Id == id).FirstOrDefault();
        return View(baseInfo);
    }

    // POST: BaseInfo/Edit/5
    [HttpPost]
    public ActionResult Edit(clsBaseInfo baseInfo)
    {
        try
        {
            // TODO: Add update logic here
            clsBaseInfo? tblBaseInfo = _context.BaseInfos.Where(x => x.Id == baseInfo.Id).FirstOrDefault();
            if (tblBaseInfo != null)
            {
                _context.Entry(tblBaseInfo).CurrentValues.SetValues(baseInfo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baseInfo);
        }
        catch
        {
            return View();
        }
    }

    // GET: BaseInfo/Delete/5
    public ActionResult Delete(long id)
    {
        clsBaseInfo? baseInfo = _context.BaseInfos.Find(id);
        return View(baseInfo);
    }

    // POST: BaseInfo/Delete/5
    [HttpPost]
    public ActionResult Delete(long id, clsBaseInfo BaseInfo)
    {
        try
        {
            // TODO: Add delete logic here

            var baseInfo = _context.BaseInfos.Where(x => x.Id == id).FirstOrDefault();
            if (baseInfo != null)
            {
                _context.BaseInfos.Remove(baseInfo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }
}
