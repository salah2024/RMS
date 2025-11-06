using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation;

public class RizMetreUserFromShowBarAvordController(ApplicationDbContext _context) : ControllerBase
{
    private readonly ApplicationDbContext context = _context;

    [HttpPost]
    public JsonResult GetCurrentRizMetreUsersForShowBarAvord([FromBody] GetCurrentRizMetreUsersForShowBarAvordInputDto request)
    {
        Guid FBId = Guid.Parse(request.FBId);
        List<clsItemsFields> lstItemsFields = context.ItemsFieldses.Where(x => x.ItemShomareh.Trim() == request.ItemFBShomareh).ToList();
        List<clsRizMetreUsers>? rizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId).OrderBy(x => x.Shomareh).ToList();

        var result = new
        {
            rizMetreUsers,
            lstItemsFields
        };
        return new JsonResult(result);
    }

    [HttpPost]
    public JsonResult ConfirmRizMetreUsersFromShowBarAvord([FromBody] RizMetreFromShowBarAvordInputDto Request)
    {
        string Sharh = Request.Sharh;
        decimal? Tedad = Request.Tedad;
        decimal? Tool = Request.Tool;
        decimal? Arz = Request.Arz;
        decimal? Ertefa = Request.Ertefa;
        decimal? Vazn = Request.Vazn;
        string Des = Request.Des;
        string FBShomareh = Request.Shomareh;
        int Year = Request.Year;
        Guid BarAvordId = Request.BarAvordUserId;
        string Code = Request.Shomareh.Substring(0, 2);
        DateTime Now = DateTime.Now;

        //try
        //{
        clsFB? FB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == FBShomareh).FirstOrDefault();
        clsBaravordUser? UB = _context.BaravordUsers.Where(x => x.ID == Request.BarAvordUserId).FirstOrDefault();

        Guid FBId = new Guid();
        if (FB == null)
        {
            clsFB newFB = new clsFB();
            newFB.BarAvordId = BarAvordId;
            newFB.Shomareh = FBShomareh;
            newFB.BahayeVahedZarib = 0;
            _context.FBs.Add(newFB);
            _context.SaveChanges();
            FBId = newFB.ID;
        }
        else
            FBId = FB.ID;

        clsRizMetreUsers? RizMetreUser = context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh = 0;
        if (RizMetreUser != null)
            Shomareh = RizMetreUser.Shomareh + 1;
        else
            Shomareh = 1;

        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.ID = Guid.NewGuid();
        RizMetre.Shomareh = Shomareh;
        RizMetre.ShomarehNew = Shomareh.ToString();
        RizMetre.Sharh = Sharh.Trim();
        RizMetre.Tedad = Tedad;
        RizMetre.Tool = Tool;
        RizMetre.Arz = Arz;
        RizMetre.Ertefa = Ertefa;
        RizMetre.Vazn = Vazn;
        RizMetre.Des = Des.Trim();
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "1";
        RizMetre.ForItem = null;
        RizMetre.UseItem = "";
        RizMetre.LevelNumber = 1;
        RizMetre.InsertDateTime = Now;


        ///محاسبه مقدار جزء
        decimal? dMeghdarJoz = null;
        if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
            dMeghdarJoz = null;
        else
            dMeghdarJoz = (Tedad == null ? 1 : Tedad) * (Tool == null ? 1 : Tool) *
            (Arz == null ? 1 : Arz) * (Ertefa == null ? 1 : Ertefa) * (Vazn == null ? 1 : Vazn);
        RizMetre.MeghdarJoz = dMeghdarJoz;

        context.RizMetreUserses.Add(RizMetre);


        /////////////
        ///درج حمل///
        /////////////
        ///


        ///درج ریز متره
        SaveHamlDto requestSaveHaml = new SaveHamlDto
        {
            BarAvordUserId = Request.BarAvordUserId,
            Year = Year,
            ItemFBShomareh = FBShomareh,
            //BarAvordHamlId = gBarAvordHamlId,
            Shomareh = Shomareh,
            MeghdarJoz = dMeghdarJoz,
            LevelNumber = 1
        };
        HamlCommon.SaveHaml(requestSaveHaml, context);

        context.SaveChanges();

        decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == FBId).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);
        /////
        //////
        //////محاسبه کل فصل
        ///
        decimal SumMeghdarFasl = 0;
        var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId)
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


        // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        var fbItems = _context.FBs
            .Where(f => f.BarAvordId == BarAvordId)
            .GroupBy(f => f.Shomareh)
            .Select(g => g.FirstOrDefault())
            .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        var userBarAvordOutPut = _context.FehrestBahas
            .Where(fehrest => fehrest.Sal == UB.Year && fehrest.Shomareh.StartsWith(Code))
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
            foreach (var RM in RizMetreAll)
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


        return new JsonResult("OK_" + FBId + "_" + dMeghdarJoz + "_" + SumMeghdarJoz + "_" + JameFasl);
    }

    public JsonResult UpdateRizMetreUsersFrmShowBarAvord([FromBody] UpdateRizMetreUsersInputDto request)
    {
        Guid Id = request.Id;
        string Sharh = request.Sharh;
        decimal? Tedad = request.Tedad;
        decimal? Tool = request.Tool;
        decimal? Arz = request.Arz;
        decimal? Ertefa = request.Ertefa;
        decimal? Vazn = request.Vazn;
        string Des = request.Des;
        int LevelNumber = request.LevelNumber;

        int Year = request.Year;
        Guid BarAvordId = request.BarAvordUserId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        string Code = request.Code;

        clsRizMetreUsers? entity = context.RizMetreUserses.FirstOrDefault(x => x.ID == Id);
        if (entity == null)
            return new JsonResult("NOK");

        entity.Sharh = Sharh;
        entity.Tedad = Tedad;
        entity.Tool = Tool;
        entity.Arz = Arz;
        entity.Ertefa = Ertefa;
        entity.Vazn = Vazn;
        entity.Des = Des;
        entity.LevelNumber = request.LevelNumber;

        decimal dMeghdarJoz = 0;
        if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
            dMeghdarJoz = 0;
        else
            dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
        entity.MeghdarJoz = dMeghdarJoz;


        /////////////
        ///ویرایش حمل///
        /////////////
        ///

        clsFB? currentFb = context.FBs.FirstOrDefault(x => x.ID == entity.FBId);
        if (currentFb != null)
        {
            string currentShomareh = currentFb.Shomareh;
            List<clsBarAvordHaml> lstBarAvordHaml = context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh == currentShomareh).ToList();
            if (lstBarAvordHaml.Count != 0)
            {
                List<Guid> lstBarAvordHamlIds = lstBarAvordHaml.Select(x => x.ID).ToList();
                List<clsBarAvordHamlRizMetre> lstBAHRM =
                    context.BarAvordHamlRizMetres.Where(x => lstBarAvordHamlIds.Contains(x.BarAvordHamlId)).ToList();
                List<Guid> lstRizMetreIds = lstBAHRM.Select(x => x.RizMetreId).ToList();
                if (lstBAHRM.Count != 0)
                {
                    clsRizMetreUsers? RizMetreForHaml = context.RizMetreUserses.FirstOrDefault(x => lstRizMetreIds.Contains(x.ID) && x.Shomareh == entity.Shomareh);
                    if (RizMetreForHaml != null)
                    {
                        //    RizMetreForHaml.Tedad = Tedad;
                        //    RizMetreForHaml.Tool = Tool;
                        //    RizMetreForHaml.Arz = Arz;
                        //    RizMetreForHaml.Ertefa = Ertefa;
                        decimal? dMeghdarJozHaml = null;
                        if (RizMetreForHaml.Tedad == null && RizMetreForHaml.Tool == null && RizMetreForHaml.Arz == null && RizMetreForHaml.Ertefa == null)
                            dMeghdarJozHaml = null;
                        else
                            dMeghdarJozHaml = (RizMetreForHaml.Tedad == null ? 1 : RizMetreForHaml.Tedad.Value) * (RizMetreForHaml.Tool == null ? 1 : RizMetreForHaml.Tool.Value) *
                            (RizMetreForHaml.Arz == null ? 1 : RizMetreForHaml.Arz.Value) * (RizMetreForHaml.Ertefa == null ? 1 : RizMetreForHaml.Ertefa.Value)
                            * (RizMetreForHaml.Vazn == null ? 1 : RizMetreForHaml.Vazn.Value);

                        RizMetreForHaml.Vazn = dMeghdarJoz;
                        RizMetreForHaml.MeghdarJoz = (dMeghdarJozHaml == null ? 1 : dMeghdarJozHaml) * dMeghdarJoz;
                    }
                }
            }
        }

        context.SaveChanges();

        decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == entity.FBId).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);


        /////
        //////
        //////محاسبه کل فصل
        ///
        decimal SumMeghdarFasl = 0;
        var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId)
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


        // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        var fbItems = _context.FBs
            .Where(f => f.BarAvordId == BarAvordId)
            .GroupBy(f => f.Shomareh)
            .Select(g => g.FirstOrDefault())
            .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        var userBarAvordOutPut = _context.FehrestBahas
            .Where(fehrest => fehrest.Sal == Year && fehrest.Shomareh.StartsWith(Code))
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
            foreach (var RM in RizMetreAll)
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


        return new JsonResult("OK_" + dMeghdarJoz + "_" + SumMeghdarJoz + "_" + JameFasl);
    }

    public ActionResult DeleteRizMetre([FromBody] DeleteRizMetreInputFromShowBarAvordDto request)
    {
        try
        {
            Guid BarAvordUserId = request.BarAvordUserId;
            clsRizMetreUsers? entity = context.RizMetreUserses.Find(request.Id);
            if (entity != null)
            {
                context.RizMetreUserses.Remove(entity);

                ///////////
                //حذف حمل//
                ///////////
                clsFB? FB = context.FBs.FirstOrDefault(x => x.ID == entity.FBId);

                if (FB != null)
                {
                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                    {
                        BarAvordId = BarAvordUserId,
                        FBShomareh = FB.Shomareh,
                        RMShomareh = entity.Shomareh,
                    };
                    HamlCommon.DeleteHaml(deleteHaml, context);
                }
                /////////////
                //////////////
                //////////////
                ///
            }
            context.SaveChanges();
            return new JsonResult("OK");

        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }
}
