using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using static Stimulsoft.Report.Export.StiBidirectionalConvert;
using static Stimulsoft.Report.StiRecentConnections;

namespace RMS.Controllers.Operation;

public class RizMetreUserFromShowBarAvordController(ApplicationDbContext _context) : ControllerBase
{
    private readonly ApplicationDbContext context = _context;

    [HttpPost]
    public async Task<JsonResult> GetCurrentRizMetreUsersForShowBarAvord([FromBody] GetCurrentRizMetreUsersForShowBarAvordInputDto request)
    {
        Guid FBId = Guid.Parse(request.FBId);
        NoeFehrestBaha NoeFB = request.NoeFB;
        long Year = request.Year;

        string FBShomareh = request.ItemFBShomareh;
        clsFehrestBahaItemsReplace? fehrestBahaItemsReplace = await _context.FehrestBahaItemsReplaces.FirstOrDefaultAsync(x => x.FBShomarehNew == FBShomareh && x.NoeFB == NoeFB && x.Year == Year);
        if (fehrestBahaItemsReplace != null)
        {
            FBShomareh = fehrestBahaItemsReplace.FBShomarehMain;
        }
        List<clsItemsFields> lstItemsFields = context.ItemsFieldses.Where(x => x.NoeFB == NoeFB && x.ItemShomareh.Trim() == FBShomareh).ToList();
        List<clsRizMetreUsers>? rizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId).OrderBy(x => x.Shomareh).ToList();

        var result = new
        {
            rizMetreUsers,
            lstItemsFields
        };
        return new JsonResult(result);
    }

    [HttpPost]
    public async Task<JsonResult> ConfirmRizMetreUsersFromShowBarAvord([FromBody] RizMetreFromShowBarAvordInputDto Request)
    {
        string Sharh = Request.Sharh;
        string TajmieItem = Request.TajmieItem;
        decimal? Tedad = Request.Tedad;
        decimal? Tool = Request.Tool;
        decimal? Arz = Request.Arz;
        decimal? Ertefa = Request.Ertefa;
        decimal? Vazn = Request.Vazn;
        string Des = Request.Des;
        string FBShomareh = Request.Shomareh;
        int Year = Request.Year;
        Guid BarAvordId = Request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = Request.NoeFBId;
        string Code = Request.Shomareh.Substring(0, 2);


        if (TajmieItem.Trim() != "")
        {
            clsTajmieFosoulItems? item = await _context.TajmieFosoulItemses.Include(x => x.TajmieFosoul).FirstOrDefaultAsync(x => x.ItemCode.Trim() == TajmieItem.Trim() && x.TajmieFosoul.NoeFBId == NoeFBId);

            if (item == null)
                return new JsonResult("NOK");

        }


        DateTime Now = DateTime.Now;


        List<clsFehrestBahaItemsReplace> lstfehrestBahaItemsReplace = await _context.FehrestBahaItemsReplaces.Where(x => x.FBShomarehNew == FBShomareh && x.NoeFB == NoeFBId && x.Year == Year).ToListAsync();

        clsFehrestBahaItemsReplace? fehrestBahaItemsReplace = lstfehrestBahaItemsReplace.Where(x => x.FBShomarehNew == FBShomareh && x.NoeFB == NoeFBId && x.Year == Year).FirstOrDefault();
        if (fehrestBahaItemsReplace != null)
        {
            FBShomareh = fehrestBahaItemsReplace.FBShomarehMain;
        }
        //try
        //{
        clsFB? FB = await _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.Shomareh == FBShomareh).FirstOrDefaultAsync();

        decimal dBahayeVahed = 0;
        decimal ItemZarib = 1;


        clsFehrestBaha? fehrestBaha = await _context.FehrestBahas.Where(x => x.Sal == Year && x.NoeFB == NoeFBId && x.Shomareh == FBShomareh).FirstOrDefaultAsync();
        if (fehrestBaha != null)
        {
            dBahayeVahed = decimal.Parse((fehrestBaha.BahayeVahed != null && fehrestBaha.BahayeVahed != "") ? fehrestBaha.BahayeVahed : "0");
        }

        Guid FBId = new Guid();
        if (FB == null)
        {
            clsFB newFB = new clsFB();
            newFB.BarAvordId = BarAvordId;
            newFB.Shomareh = FBShomareh;
            newFB.BahayeVahedZarib = 0;
            newFB.NoeFBId = NoeFBId;
            _context.FBs.Add(newFB);
            await _context.SaveChangesAsync();
            FBId = newFB.ID;
        }
        else
        {
            FBId = FB.ID;
            if (FB.BahayeVahedNew != 0)
            {
                dBahayeVahed = FB.BahayeVahedNew;
                ItemZarib = FB.ItemZarib == null ? 1 : FB.ItemZarib.Value;
            }
        }

        clsRizMetreUsers? RizMetreUser = await context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefaultAsync();
        long Shomareh = 0;
        if (RizMetreUser != null)
            Shomareh = RizMetreUser.Shomareh + 1;
        else
            Shomareh = 1;

        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.ID = Guid.NewGuid();
        RizMetre.Shomareh = Shomareh;
        RizMetre.ShomarehNew = Shomareh.ToString();
        RizMetre.TajmieItem = TajmieItem.Trim();
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

        if (dMeghdarJoz != null)
        {
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
                NoeFBId = NoeFBId,
                //BarAvordHamlId = gBarAvordHamlId,
                Shomareh = Shomareh,
                MeghdarJoz = dMeghdarJoz,
                LevelNumber = 1,
            };
            await HamlCommon.SaveHaml(requestSaveHaml, context);

            await context.SaveChangesAsync();

            decimal? dSumMeghdarJoz = await _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FBId == FBId).SumAsync(x => x.MeghdarJoz);
            decimal? dBahayeItem = dSumMeghdarJoz * dBahayeVahed * ItemZarib;

            return new JsonResult("OK_" + dMeghdarJoz + "_" + dSumMeghdarJoz + "_" + dBahayeItem + "_" + FBId);
        }
        else
            return new JsonResult("NOOK_");
    }

    public async Task<JsonResult> GetJameCurrentFasl([FromBody] JameCurrentFaslDto Request)
    {
        int Year = Request.Year;
        Guid BarAvordId = Request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = Request.NoeFBId;
        string FBShomareh = Request.Shomareh;
        string Code = Request.Code;

        //clsFB? FB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.Shomareh == FBShomareh).FirstOrDefault();

        //Guid FBId = new Guid();
        //if (FB != null)
        //{
        //    decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == FB.ID).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);
        //decimal SumMeghdarJoz = 0;
        /////
        //////
        //////محاسبه کل فصل
        ///
        //decimal SumMeghdarFasl = 0;

        List<clsFehrestBahaItemsReplace> lstfehrestBahaItemsReplace = await _context.FehrestBahaItemsReplaces.Where(x => x.FBShomarehNew.Substring(0, 2) == FBShomareh.Substring(0, 2) && x.NoeFB == NoeFBId && x.Year == Year).ToListAsync();

        var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId && x.FB.NoeFBId == NoeFBId)
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
        //var fbItems = _context.FBs
        //    .Where(f => f.BarAvordId == BarAvordId && f.NoeFBId == NoeFBId)
        //    .GroupBy(f => f.Shomareh)
        //    .Select(g => g.FirstOrDefault())
        //    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        //var fbItems = await (from fb in _context.FBs
        //                     join fehrestbaha in _context.FehrestBahas
        //                     on fb.Shomareh.Substring(0, 6) equals fehrestbaha.Shomareh.Trim()
        //                     where fb.BarAvordId == BarAvordId && fb.NoeFBId == NoeFBId
        //                     select new FBForGetUserBarAvordDto
        //                     {
        //                         ID = fb.ID,
        //                         BahayeVahedSharh = fb.BahayeVahedSharh,
        //                         Shomareh = fb.Shomareh.Substring(0, 6),
        //                         BahayeVahed = fehrestbaha == null ? "0" : fehrestbaha.BahayeVahed != null ? (fb.BahayeVahedZarib * decimal.Parse(fehrestbaha.BahayeVahed.Trim() == "" ? "0" :
        //                                                      fehrestbaha.BahayeVahed)).ToString() : fehrestbaha.BahayeVahed,
        //                         Vahed = fehrestbaha.Vahed,
        //                         BahayeVahedZarib = fb.BahayeVahedZarib,
        //                         BahayeVahedNew = fb.BahayeVahedNew,
        //                         ItemZarib = fb.ItemZarib
        //                     })
        //        .GroupBy(f => f.Shomareh)
        //        .Select(g => g.FirstOrDefault())
        //        .ToListAsync();



        var fbItems = await (
   from fb in _context.FBs

   join fehrestbaha in _context.FehrestBahas
       on fb.Shomareh.Substring(0, 6)
       equals fehrestbaha.Shomareh.Trim()
       into fehrestGroup

   from fehrestbaha in fehrestGroup
       .Where(x =>
           x.NoeFB == NoeFBId &&
           x.Sal == Year)
       .DefaultIfEmpty()

   where fb.BarAvordId == BarAvordId
         && fb.NoeFBId == NoeFBId

   select new FBForGetUserBarAvordDto
   {
       ID = fb.ID,

       BahayeVahedSharh = fb.BahayeVahedSharh,

       // شماره واقعی FB حفظ شود
       Shomareh = fb.Shomareh,

       BahayeVahed =
           fehrestbaha == null
               ? "0"
               : fehrestbaha.BahayeVahed != null
                   ? fehrestbaha.BahayeVahed
                   : "0",

       Vahed = fehrestbaha != null
           ? fehrestbaha.Vahed
           : null,

       //BahayeVahedZarib = fb.BahayeVahedZarib,

       BahayeVahedNew = fb.BahayeVahedNew,

       ItemZarib = fb.ItemZarib
   }
)
.GroupBy(x => x.Shomareh)
.Select(g => g.First())
.ToListAsync();




        //foreach (var item in fbItems)
        //{
        //    foreach (var fbItemsReplace in lstfehrestBahaItemsReplace)
        //    {
        //        if (item.Shomareh == fbItemsReplace.FBShomarehMain)
        //        {
        //            item.Shomareh = fbItemsReplace.FBShomarehNew;
        //        }
        //    }
        //}


        var replaceDictionary = lstfehrestBahaItemsReplace
           .GroupBy(x => x.FBShomarehMain)
           .ToDictionary(
               x => x.Key,
               x => x.Last().FBShomarehNew
           );

        foreach (var item in fbItems)
        {
            if (replaceDictionary.TryGetValue(item.Shomareh, out var newShomareh))
            {
                item.Shomareh = newShomareh;
            }
        }

        var lstFehrestBahas = await (
             from fb in _context.FehrestBahas.AsNoTracking()
             join replace in _context.FehrestBahaItemsReplaces.AsNoTracking()
             on fb.Shomareh equals replace.FBShomarehMain into replaces
             from replace in replaces.DefaultIfEmpty()
             where fb.Sal == Year
                 && fb.NoeFB == NoeFBId
             select new
             {
                 Id = fb.Id,
                 BahayeVahed = fb.BahayeVahed,
                 Vahed = fb.Vahed,
                 Sharh = fb.Sharh,

                 ShomarehMain = fb.Shomareh,

                 Shomareh = replace != null && replace.FBShomarehNew != null
                 ? replace.FBShomarehNew
                 : fb.Shomareh,

                 Sal = fb.Sal,
                 NoeFB = fb.NoeFB
             }).ToListAsync();


        // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        List<ViewUserBarAvordOutPutDto> userBarAvordOutPut = lstFehrestBahas //_context.FehrestBahas
                                                                             .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId && fehrest.Shomareh.StartsWith(Code))
                                                                             .Select(x => new ViewUserBarAvordOutPutDto
                                                                             {
                                                                                 ItemFbShomareh = x.Shomareh,
                                                                                 ItemFbShomarehMain = x.ShomarehMain,
                                                                                 Sharh = x.Sharh,
                                                                                 Vahed = x.Vahed,
                                                                                 BahayeVahed = x.BahayeVahed,

                                                                             }).ToList() // انتقال به حافظه
            .GroupJoin(fbItems,
                fehrest => fehrest.ItemFbShomareh,
                fb => fb.Shomareh,
                (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
            .OrderBy(x => x.fehrest.ItemFbShomareh)
            .Select(x => new ViewUserBarAvordOutPutDto
            {
                ItemFbShomareh = x.fehrest.ItemFbShomareh,
                ItemFbShomarehMain = x.fehrest.ItemFbShomarehMain,
                Sharh = x.fehrest.Sharh,
                BahayeVahed = x.fehrest.BahayeVahed,
                Vahed = x.fehrest.Vahed,
                FBId = x.fbItem != null ? x.fbItem.ID : null,
                BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
                ItemZarib = x.fbItem != null ? x.fbItem.ItemZarib : 0,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
            }).ToList();

        List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFiltered = userBarAvordOutPut.Where(x => x.FBId != null).ToList();

        var fehrestShomarehs = userBarAvordOutPutFiltered.Select(x => x.ItemFbShomareh).ToList();



        var barAvordOnlyInFbItems = fbItems
            .Where(fb => !fehrestShomarehs.Contains(fb.Shomareh))
            .Select(fb => new ViewUserBarAvordOutPutDto
            {
                ItemFbShomareh = fb.Shomareh,
                ItemFbShomarehMain = fb.Shomareh.Substring(0, 6),
                Sharh = fb.BahayeVahedSharh,
                BahayeVahed = fb.BahayeVahed,
                Vahed = fb.Vahed,
                FBId = fb.ID,
                ItemZarib = fb.ItemZarib,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
                ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
            }).ToList();


        userBarAvordOutPutFiltered = barAvordOnlyInFbItems
            .Concat(userBarAvordOutPutFiltered)
            .OrderBy(x => x.ItemFbShomareh)
            .ToList();

        List<string> lstItemFbShomarehs = userBarAvordOutPut.Select(x => x.ItemFbShomareh).Distinct().ToList();

        List<clsBahayeVahedItemsRelated> lstBVItemRelated = _context.BahayeVahedItemsRelateds.Where(x => lstItemFbShomarehs.Contains(x.ItemShomarehRelated.ToLower().Trim()) && x.NoeFB == NoeFBId && x.Year == Year).ToList();


        //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordId);
        //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordId);

        //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
        //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

        ////محاسبه ضرایب
        RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
        {
            BarAvordId = BarAvordId,
            NoeFBId = NoeFBId,
            Year = Year
        };
        ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
        decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
        decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

        ///
        ///

        List<long> FaslIds = _context.ZarayebFosouls.Include(x => x.Fosoul).Where(x => x.Fosoul.NoeFB == NoeFBId && x.Fosoul.Year == Year).Select(x => x.FaslId).ToList();

        var FaslZarib = (from FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                         join ZarayebFosoul in _context.ZarayebFosouls
                         on FehrestBahaItemsReplace.FaslId equals ZarayebFosoul.FaslId
                         where FaslIds.Contains(FehrestBahaItemsReplace.FaslId)
                         select new
                         {
                             FBShomareh = FehrestBahaItemsReplace.FBShomarehMain,
                             ZarayebFosoul.ZaribBalaSari,
                             ZarayebFosoul.ZaribManteghe
                         }).ToList();

        decimal JameFaslInZarib = 0;

        decimal JameFasl = 0;

        decimal JameFaslWithoutBahaVahed = 0;//جمع آیتم های بدون بهای واحد
        decimal JameFaslWithoutBahaVahedWithZarib = 0;//جمع آیتم های بدون بهای واحد در ضریب

        decimal AllJameFaslStar = 0;
        decimal AllJameFaslStarWithZarib = 0;
        foreach (var item in userBarAvordOutPutFiltered)
        {
            decimal Meghdar = 0;

            List<ViewUserBarAvordOutPutRizMetreDto> lstCurrentRM = RizMetreAll.Where(x => x.FBId == item.FBId).ToList();

            foreach (var RM in lstCurrentRM)
            {
                if (item.FBId == RM.FBId)
                {
                    item.RizMetre.Add(RM);
                    Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                }
            }


            if (Meghdar != 0)
            {
                decimal dBVahed = 0;// item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);

                decimal dBahayeKol = 0;


                clsBahayeVahedItemsRelated? BVItemsRelated = lstBVItemRelated.FirstOrDefault(x => x.ItemShomarehRelated.Trim() == item.ItemFbShomareh.Trim());
                if (BVItemsRelated != null)
                {
                    var barAvordInFehrest1 = userBarAvordOutPutFiltered.FirstOrDefault(x => x.ItemFbShomareh.Trim() == BVItemsRelated.ItemShomareh.Trim());
                    if (barAvordInFehrest1 != null)
                    {
                        long BahayeVahed1 = barAvordInFehrest1.BahayeVahedNew != 0 ? barAvordInFehrest1.BahayeVahedNew : barAvordInFehrest1.BahayeVahed == null ? 0 : long.Parse(barAvordInFehrest1.BahayeVahed);
                        item.BahayeVahed = (BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                    }
                }

                item.Meghdar = Meghdar;
                decimal ItemZarib = (item.ItemZarib == null || item.ItemZarib == 0) ? 1 : item.ItemZarib.Value;
                ///در صورتی که آیتم بهای واحدش تغییر یابد
                if (item.BahayeVahedNew != 0)
                {
                    dBVahed = item.BahayeVahedNew;
                    dBahayeKol = Meghdar * dBVahed * ItemZarib;
                    item.BahayeKol = dBahayeKol;
                    JameFaslWithoutBahaVahed += dBahayeKol;

                    if (zaribBalaSari == 0 && zaribBalaSari == 0)
                    {

                    }
                    else
                    {
                        if (FaslZarib.Select(x => x.FBShomareh).Contains(item.ItemFbShomarehMain))
                        {
                            var currentFaslZarib = FaslZarib.Where(x => x.FBShomareh == item.ItemFbShomarehMain).First();

                            decimal newZaribBalaSari = currentFaslZarib.ZaribBalaSari == null ? zaribBalaSari : currentFaslZarib.ZaribBalaSari.Value;
                            decimal newZaribManteghe = currentFaslZarib.ZaribManteghe == null ? zaribManteghe : currentFaslZarib.ZaribManteghe.Value;

                            //JameFaslInZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                            JameFaslWithoutBahaVahedWithZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                        }
                        else
                        //JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                        {
                            if (Code != "99")
                            {

                                JameFaslWithoutBahaVahedWithZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                            }
                            else
                                JameFaslWithoutBahaVahedWithZarib += dBahayeKol;
                        }
                    }
                }
                else
                {
                    if (item.BahayeVahed == null || item.BahayeVahed == "")
                    {
                        dBVahed = 0;
                    }
                    else
                    {
                        dBVahed = decimal.Parse(item.BahayeVahed);
                        dBahayeKol = Meghdar * dBVahed * ItemZarib;
                        item.BahayeKol = dBahayeKol;
                        JameFasl += dBahayeKol;

                        if (zaribBalaSari == 0 && zaribBalaSari == 0)
                        {

                        }
                        else
                        {
                            if (FaslZarib.Select(x => x.FBShomareh).Contains(item.ItemFbShomarehMain))
                            {

                                var currentFaslZarib = FaslZarib.Where(x => x.FBShomareh == item.ItemFbShomarehMain).First();

                                decimal newZaribBalaSari = currentFaslZarib.ZaribBalaSari == null ? zaribBalaSari : currentFaslZarib.ZaribBalaSari.Value;
                                decimal newZaribManteghe = currentFaslZarib.ZaribManteghe == null ? zaribManteghe : currentFaslZarib.ZaribManteghe.Value;

                                JameFaslInZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                            }
                            else
                            {
                                if (Code != "99")
                                {

                                    JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                                }
                                else
                                    JameFaslInZarib += dBahayeKol;
                            }
                        }
                    }
                }
            }
            //}
        }

        List<clsItemFBStar> lstItemFBStars = await _context.ItemFBStars.Where(x => x.BaravordId == BarAvordId && x.NoeFBId == NoeFBId && x.Shomareh.Substring(0, 2) == Code).ToListAsync();
        foreach (var itemFBStarCurrent in lstItemFBStars)
        {
            decimal ItemZaribStar = itemFBStarCurrent.ItemZaribStar == null ? 1 : itemFBStarCurrent.ItemZaribStar.Value;

            decimal JameItemFBStar = 0;
            //clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.Trim() == itemFBStarCurrent.Shomareh);

            //BahayeVahedStar = 0;
            //if (itemFBStar != null)
            //{
            decimal BahayeVahedStar = itemFBStarCurrent.BahayeVahed * ItemZaribStar;
            //}
            JameItemFBStar = context.RizMetreStars.Include(x => x.ItemFBStar)
                 .Where(x => x.ItemFBStar.BaravordId == BarAvordId && x.ItemFBStar.NoeFBId == NoeFBId && x.ItemFBStar.Shomareh.Trim() == itemFBStarCurrent.Shomareh)
                 .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

            AllJameFaslStar += JameItemFBStar * BahayeVahedStar;
            bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;
            decimal ZaribStar2 = zaribBalaSari;
            ///خرید تجهیزات
            if (blnKharidTajhizat)
            {
                zaribManteghe = 1;
                ZaribStar2 = 1.14m;
            }
            AllJameFaslStarWithZarib += (JameItemFBStar * BahayeVahedStar * zaribManteghe * ZaribStar2);
        }

        ////////
        ////////محاسبه کل فصل
        /////

        //CalculateFaslDto CalculateFasl = new CalculateFaslDto
        //{

        //    BarAvordId = BarAvordId,
        //    Code = Code,
        //    FBId = FBId,
        //    NoeFBId = NoeFBId,
        //    Year = Year
        //};
        //ResultCalculateFaslDto result = RizMetreCommon.fnCalculateFasl(CalculateFasl, _context);

        var result1 = new
        {
            //dMeghdarJoz,
            //SumMeghdarJoz,
            JameFasl,
            JameFaslInZarib,
            JameFalsKol = JameFasl + JameFaslWithoutBahaVahed + AllJameFaslStar,
            JameFaslWithoutBahaVahed = JameFaslWithoutBahaVahed + AllJameFaslStar,
            JameFaslWithoutBahaVahedWithZarib = JameFaslWithoutBahaVahedWithZarib + AllJameFaslStarWithZarib,
            JameFalsKolWithoutBahaVahedWithZarib = JameFaslInZarib + JameFaslWithoutBahaVahedWithZarib + AllJameFaslStarWithZarib,
            //FBId
        };
        return new JsonResult(result1);
        //}
        //else
        //    return null;

    }

    public async Task<JsonResult> UpdateRizMetreUsersFrmShowBarAvord([FromBody] UpdateRizMetreUsersInputDto request)
    {
        Guid Id = request.Id;
        string Sharh = request.Sharh;
        string? TajmieItem = request.TajmieItem;
        decimal? Tedad = request.Tedad;
        decimal? Tool = request.Tool;
        decimal? Arz = request.Arz;
        decimal? Ertefa = request.Ertefa;
        decimal? Vazn = request.Vazn;
        string Des = request.Des == null ? "" : request.Des;
        int LevelNumber = request.LevelNumber;

        int Year = request.Year;
        Guid BarAvordId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        string Code = request.Code;


        if (TajmieItem != null)
        {
            if (TajmieItem.Trim() != "")
            {
                clsTajmieFosoulItems? item = await _context.TajmieFosoulItemses.Include(x => x.TajmieFosoul).FirstOrDefaultAsync(x => x.ItemCode.Trim() == TajmieItem.Trim() && x.TajmieFosoul.NoeFBId == NoeFBId);

                if (item == null)
                    return new JsonResult("NOK");
            }
        }

        clsRizMetreUsers? entity = await context.RizMetreUserses.FirstOrDefaultAsync(x => x.ID == Id);
        if (entity == null)
            return new JsonResult("NOK");

        entity.Sharh = Sharh;
        entity.TajmieItem = TajmieItem;
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

        decimal dBahayeVahed = 0;

        clsFB? currentFb = await context.FBs.FirstOrDefaultAsync(x => x.ID == entity.FBId);
        if (currentFb != null)
        {

            clsFehrestBaha? fehrestBaha = await _context.FehrestBahas.Where(x => x.Sal == Year && x.NoeFB == NoeFBId && x.Shomareh == currentFb.Shomareh).FirstOrDefaultAsync();
            if (fehrestBaha != null)
            {
                dBahayeVahed = decimal.Parse((fehrestBaha.BahayeVahed != null && fehrestBaha.BahayeVahed != "") ? fehrestBaha.BahayeVahed : "0");
            }

            if (currentFb.BahayeVahedNew != 0)
            {
                dBahayeVahed = currentFb.BahayeVahedNew;
            }


            string currentShomareh = currentFb.Shomareh;
            List<clsBarAvordHaml> lstBarAvordHaml = await context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.FBShomareh == currentShomareh).ToListAsync();
            if (lstBarAvordHaml.Count != 0)
            {
                List<Guid> lstBarAvordHamlIds = lstBarAvordHaml.Select(x => x.ID).ToList();
                List<clsBarAvordHamlRizMetre> lstBAHRM =
                    context.BarAvordHamlRizMetres.Where(x => lstBarAvordHamlIds.Contains(x.BarAvordHamlId)).ToList();
                List<Guid> lstRizMetreIds = lstBAHRM.Select(x => x.RizMetreId).ToList();
                if (lstBAHRM.Count != 0)
                {
                    clsRizMetreUsers? RizMetreForHaml = await context.RizMetreUserses.FirstOrDefaultAsync(x => lstRizMetreIds.Contains(x.ID) && x.Shomareh == entity.Shomareh);
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

        await context.SaveChangesAsync();

        //decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == entity.FBId).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);


        /////
        //////
        //////محاسبه کل فصل
        ///
        //decimal SumMeghdarFasl = 0;
        //var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId)
        //        .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
        //        {
        //            Id = riz.ID,
        //            Sharh = riz.Sharh,
        //            Shomareh = riz.Shomareh,
        //            Arz = riz.Arz,
        //            Des = riz.Des,
        //            Ertefa = riz.Ertefa,
        //            Tedad = riz.Tedad,
        //            Tool = riz.Tool,
        //            Vazn = riz.Vazn,
        //            FBId = riz.FBId,
        //            MeghdarJoz = riz.MeghdarJoz
        //        }).OrderBy(x => x.Shomareh).ToList();


        //// اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        //var fbItems = _context.FBs
        //    .Where(f => f.BarAvordId == BarAvordId)
        //    .GroupBy(f => f.Shomareh)
        //    .Select(g => g.FirstOrDefault())
        //    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        //// سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        //var userBarAvordOutPut = _context.FehrestBahas
        //    .Where(fehrest => fehrest.Sal == Year && fehrest.Shomareh.StartsWith(Code))
        //    .ToList() // انتقال به حافظه
        //    .GroupJoin(fbItems,
        //        fehrest => fehrest.Shomareh,
        //        fb => fb.Shomareh,
        //        (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
        //    .OrderBy(x => x.fehrest.Shomareh)
        //    .Select(x => new ViewUserBarAvordOutPutDto
        //    {
        //        ItemFbShomareh = x.fehrest.Shomareh,
        //        Sharh = x.fehrest.Sharh,
        //        BahayeVahed = x.fehrest.BahayeVahed,
        //        Vahed = x.fehrest.Vahed,
        //        FBId = x.fbItem != null ? x.fbItem.ID : null,
        //        RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
        //    }).ToList();


        //decimal JameFasl = 0;
        //foreach (var item in userBarAvordOutPut)
        //{
        //    decimal Meghdar = 0;
        //    foreach (var RM in RizMetreAll)
        //    {
        //        if (item.FBId == RM.FBId)
        //        {
        //            item.RizMetre.Add(RM);
        //            Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
        //        }
        //    }
        //    item.Meghdar = Meghdar;
        //    decimal dBahayeKol = Meghdar * (item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed));
        //    item.BahayeKol = dBahayeKol;
        //    JameFasl += dBahayeKol;
        //}


        //return new JsonResult("OK_" + dMeghdarJoz + "_" + SumMeghdarJoz + "_" + JameFasl);



        //var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId)
        //.Select(riz => new ViewUserBarAvordOutPutRizMetreDto
        //{
        //    Id = riz.ID,
        //    Sharh = riz.Sharh,
        //    Shomareh = riz.Shomareh,
        //    Arz = riz.Arz,
        //    Des = riz.Des,
        //    Ertefa = riz.Ertefa,
        //    Tedad = riz.Tedad,
        //    Tool = riz.Tool,
        //    Vazn = riz.Vazn,
        //    FBId = riz.FBId,
        //    MeghdarJoz = riz.MeghdarJoz
        //}).OrderBy(x => x.Shomareh).ToList();

        //decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == entity.FBId).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

        ////clsBaravordUser? UB = _context.BaravordUsers.Where(x => x.ID == BarAvordId).FirstOrDefault();

        //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordId);
        //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordId);

        //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
        //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

        //decimal JameFaslInZarib = 0;

        //// اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        //var fbItems = _context.FBs
        //    .Where(f => f.BarAvordId == BarAvordId)
        //    .GroupBy(f => f.Shomareh)
        //    .Select(g => g.FirstOrDefault())
        //    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        //// سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        //var userBarAvordOutPut = _context.FehrestBahas
        //    .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId && fehrest.Shomareh.StartsWith(Code))
        //    .ToList() // انتقال به حافظه
        //    .GroupJoin(fbItems,
        //        fehrest => fehrest.Shomareh,
        //        fb => fb.Shomareh,
        //        (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
        //    .OrderBy(x => x.fehrest.Shomareh)
        //    .Select(x => new ViewUserBarAvordOutPutDto
        //    {
        //        ItemFbShomareh = x.fehrest.Shomareh,
        //        Sharh = x.fehrest.Sharh,
        //        BahayeVahed = x.fehrest.BahayeVahed,
        //        Vahed = x.fehrest.Vahed,
        //        FBId = x.fbItem != null ? x.fbItem.ID : null,
        //        RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
        //    }).ToList();


        //decimal JameFasl = 0;
        //foreach (var item in userBarAvordOutPut)
        //{
        //    decimal Meghdar = 0;
        //    foreach (var RM in RizMetreAll)
        //    {
        //        if (item.FBId == RM.FBId)
        //        {
        //            item.RizMetre.Add(RM);
        //            Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
        //        }
        //    }
        //    decimal dBVahed = item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
        //    item.Meghdar = Meghdar;
        //    decimal dBahayeKol = Meghdar * dBVahed;
        //    item.BahayeKol = dBahayeKol;
        //    JameFasl += dBahayeKol;
        //    JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
        //}

        ////////
        ////////محاسبه کل فصل
        /////

        //CalculateFaslDto CalculateFasl = new CalculateFaslDto
        //{

        //    BarAvordId = BarAvordId,
        //    Code = Code,
        //    FBId = entity.FBId,
        //    NoeFBId = NoeFBId,
        //    Year = Year
        //};
        //ResultCalculateFaslDto result = RizMetreCommon.fnCalculateFasl(CalculateFasl, _context);

        decimal dSumMeghdarJoz = await context.RizMetreUserses.Where(x => x.FBId == entity.FBId).SumAsync(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);
        decimal? dBahayeItem = dSumMeghdarJoz * dBahayeVahed;


        return new JsonResult("OK_" + dMeghdarJoz + "_" + dSumMeghdarJoz + "_" + dBahayeItem);// + "_" + result.JameFasl + "_" + result.JameFaslInZarib);
    }

    public async Task<ActionResult> DeleteRizMetre([FromBody] DeleteRizMetreInputFromShowBarAvordDto request)
    {
        try
        {
            NoeFehrestBaha NoeFBId = request.NoeFBId;
            Guid BarAvordUserId = request.BarAvordUserId;
            int Year = request.Year;
            clsRizMetreUsers? entity = context.RizMetreUserses.Find(request.Id);
            if (entity != null)
            {
                context.RizMetreUserses.Remove(entity);


                clsFB? FB = context.FBs.FirstOrDefault(x => x.ID == entity.FBId);
                decimal dBahayeVahed = 0;

                if (FB != null)
                {
                    clsFehrestBaha? fehrestBaha = await _context.FehrestBahas.Where(x => x.Sal == Year && x.NoeFB == NoeFBId && x.Shomareh == FB.Shomareh).FirstOrDefaultAsync();
                    if (fehrestBaha != null)
                    {
                        dBahayeVahed = decimal.Parse((fehrestBaha.BahayeVahed != null && fehrestBaha.BahayeVahed != "") ? fehrestBaha.BahayeVahed : "0");
                    }

                    if (FB.BahayeVahedNew != 0)
                    {
                        dBahayeVahed = FB.BahayeVahedNew;
                    }
                    ///////////
                    //حذف حمل//
                    ///////////
                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                    {
                        RIzMetreId = entity.ID,
                        BarAvordId = BarAvordUserId,
                        FBShomareh = FB.Shomareh,
                        RMShomareh = entity.Shomareh,
                        NoeFBId = NoeFBId
                    };
                    await HamlCommon.DeleteHaml(deleteHaml, context);
                    /////////////
                    //////////////
                    //////////////
                    ///

                    await context.SaveChangesAsync();

                    var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordUserId)
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

                    decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == entity.FBId).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

                    string Code = FB.Shomareh.Substring(0, 2);

                    //clsBaravordUser? UB = _context.BaravordUsers.Where(x => x.ID == BarAvordUserId).FirstOrDefault();

                    //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordUserId);
                    //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordUserId);

                    //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
                    //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

                    ////محاسبه ضرایب
                    RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
                    {
                        BarAvordId = BarAvordUserId,
                        NoeFBId = NoeFBId,
                        Year = Year
                    };
                    ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
                    decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
                    decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

                    ///
                    ///

                    decimal JameFaslInZarib = 0;

                    // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
                    var fbItems = _context.FBs
                        .Where(f => f.BarAvordId == BarAvordUserId)
                        .GroupBy(f => f.Shomareh)
                        .Select(g => g.FirstOrDefault())
                        .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

                    // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
                    var userBarAvordOutPut = _context.FehrestBahas
                        .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId && fehrest.Shomareh.StartsWith(Code))
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
                            BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
                            Vahed = x.fehrest.Vahed,
                            FBId = x.fbItem != null ? x.fbItem.ID : null,
                            RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                        }).ToList();

                    List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFiltered = userBarAvordOutPut.Where(x => x.FBId != null).ToList();

                    decimal JameFasl = 0;
                    foreach (var item in userBarAvordOutPutFiltered)
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
                        decimal dBVahed = item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                        item.Meghdar = Meghdar;
                        decimal dBahayeKol = Meghdar * dBVahed;
                        item.BahayeKol = dBahayeKol;
                        JameFasl += dBahayeKol;
                        JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                    }
                    decimal? dBahayeItem = SumMeghdarJoz * dBahayeVahed;

                    return new JsonResult("OK_" + 0 + "_" + SumMeghdarJoz + "_" + dBahayeItem);// + "_" + JameFasl + "_" + JameFaslInZarib);
                }
                else
                    return new JsonResult("NOK");
            }
            else
                return new JsonResult("NOK");
        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }
}
