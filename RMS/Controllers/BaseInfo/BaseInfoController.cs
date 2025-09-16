using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BaseInfo
{
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
            clsConditionContext CC = _context.ConditionContexts.Where(x => x.Id == request.ConditionContextId).FirstOrDefault();
            if (CC != null)
            {
                return new JsonResult(CC);
            }
            else
                return null;
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
        public JsonResult SetViewBag([FromBody] SetViewBagInputDto request)
        {
            Guid BAId = request.BAId;
            int Year = request.Year;
            NoeFehrestBaha NoeFB = request.NoeFB;

            TempData["BAId"] = BAId;
            TempData["Year"] = Year;
            TempData["NoeFB"] = NoeFB;
            return new JsonResult("OK");
        }

        [HttpPost]
        public JsonResult SetViewBagEmpty()
        {
            TempData["BAId"] = "";
            TempData["Year"] = "";
            TempData["NoeFB"] = "";
            TempData["UserName"] = "";
            return new JsonResult("OK");
        }

        [HttpGet]
        public JsonResult GetViewBag()
        {
            string BAId = TempData["BAId"].ToString();
            string Year = TempData["Year"].ToString();
            string NoeFB = TempData["NoeFB"].ToString();
            TempData["BAId"] = BAId;
            TempData["Year"] = Year;
            TempData["NoeFB"] = NoeFB;
            DataTable Dt = new DataTable();
            Dt.Columns.Add("BAId");
            Dt.Columns.Add("Year");
            Dt.Columns.Add("NoeFB");

            DataRow Dr = Dt.NewRow();
            Dr["BAId"] = BAId;
            Dr["Year"] = Year;
            Dr["NoeFB"] = NoeFB;
            Dt.Rows.Add(Dr);
            DataSet Ds = new DataSet();
            Ds.Tables.Add(Dt);
            return new JsonResult(Ds.GetXml());
        }

        [HttpPost]
        public JsonResult GetFosoul([FromBody] GetFosoulInputDto request)
        {
            List<GetFosoulOutputDto> Fosoul = _context.Fosouls.Include(x => x.NoeFosoul).Where(x => x.NoeFosoul.Id == request.NoeFaslId)
                .OrderBy(x => x.order)
                .Select(x => new GetFosoulOutputDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    FaslName = x.Name,
                    Order = x.order,
                }).ToList();


            var RizMetre = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == request.BarAvordUserId)
                .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
                {
                    Id = riz.ID,
                    Sharh = riz.Sharh,
                    Shomareh = riz.Shomareh,
                    Arz = riz.Arz,
                    Des = riz.Des,
                    Ertefa = riz.Ertefa,
                    Tedad = riz.Tedad,
                    Tool = riz.Tool,
                    Vazn = riz.Vazn,
                    FBId = riz.FBId,
                    MeghdarJoz = riz.MeghdarJoz
                }).OrderBy(x => x.Shomareh).ToList();


            foreach (var fasl in Fosoul)
            {
                // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
                var fbItems = _context.FBs
                    .Where(f => f.BarAvordId == request.BarAvordUserId)
                    .GroupBy(f => f.Shomareh)
                    .Select(g => g.FirstOrDefault())
                    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

                // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
                var userBarAvordOutPut = _context.FehrestBahas
                    .Where(fehrest => fehrest.Sal == request.Year && fehrest.Shomareh.StartsWith(fasl.Code))
                    .ToList() // انتقال به حافظه
                    .GroupJoin(fbItems,
                        fehrest => fehrest.Shomareh,
                        fb => fb.Shomareh,
                        (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
                    .OrderBy(x => x.fehrest.Shomareh)
                    .Select(x => new ViewUserBarAvordOutPutDto
                    {
                        ItemFbShomareh = x.fehrest.Shomareh,
                        Sharh = x.fehrest.Sharh,
                        BahayeVahed = x.fehrest.BahayeVahed,
                        Vahed = x.fehrest.Vahed,
                        FBId = x.fbItem != null ? x.fbItem.ID : null,
                        RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                    }).ToList();


                decimal JameFasl = 0;
                foreach (var item in userBarAvordOutPut)
                {
                    decimal Meghdar = 0;
                    foreach (var RM in RizMetre)
                    {
                        if (item.FBId == RM.FBId)
                        {
                            item.RizMetre.Add(RM);
                            Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                        }
                    }
                    item.Meghdar = Meghdar;
                    decimal dBahayeKol = Meghdar * (item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed));
                    item.BahayeKol = dBahayeKol;
                    JameFasl += dBahayeKol;
                }

                fasl.JameFasl = JameFasl;
            }
            return new JsonResult(Fosoul);
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

}
