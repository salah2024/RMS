using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Models.Entity;
using System.Security.Cryptography;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser;

public class ShowBarAvordUserController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewShowBarAvordUserForm");

        return PartialView();
    }



    public async Task<JsonResult> GetUserBarAvord([FromBody] ViewUserBarAvordInputDto request)
    {
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        //var RizMetre = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == request.BarAvordUserId && x.FB.NoeFBId == NoeFBId)
        //    .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
        //    {
        //        Id = riz.ID,
        //        tajmieItem = riz.TajmieItem,
        //        Sharh = riz.Sharh,
        //        Shomareh = riz.Shomareh,
        //        ShomarehNew = riz.ShomarehNew,
        //        Arz = riz.Arz,
        //        Des = riz.Des,
        //        Ertefa = riz.Ertefa,
        //        Tedad = riz.Tedad,
        //        Tool = riz.Tool,
        //        Vazn = riz.Vazn,
        //        FBId = riz.FBId,
        //        MeghdarJoz = riz.MeghdarJoz
        //    }).OrderBy(x => x.Shomareh).ToList();


        //var lstFB = _context.FBs.Where(x => x.BarAvordId == request.BarAvordUserId && x.NoeFBId == NoeFBId).ToList();


        var RizMetre = _context.RizMetreUserses
    .Where(x =>
        x.FB.BarAvordId == request.BarAvordUserId &&
        x.FB.NoeFBId == NoeFBId)
    .OrderBy(x => x.Shomareh)
    .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
    {
        Id = riz.ID,
        tajmieItem = riz.TajmieItem,
        Sharh = riz.Sharh,
        Shomareh = riz.Shomareh,
        ShomarehNew = riz.ShomarehNew,
        Arz = riz.Arz,
        Des = riz.Des,
        Ertefa = riz.Ertefa,
        Tedad = riz.Tedad,
        Tool = riz.Tool,
        Vazn = riz.Vazn,
        FBId = riz.FBId,
        MeghdarJoz = riz.MeghdarJoz
    })
    .AsNoTracking()
    .ToList();

        List<clsFehrestBahaItemsReplace> lstFBItemReplaces = _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToList();

        var lstFehrestBahas = await (
            from fb in _context.FehrestBahas.AsNoTracking()
            join replace in _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).AsNoTracking()
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




        //var lstFehrestBahas = (from FehrestBaha in _context.FehrestBahas
        //                       join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
        //                       on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
        //                       from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
        //                       where FehrestBaha.Sal == Year && FehrestBaha.NoeFB == NoeFBId
        //                       select new
        //                       {
        //                           Id = FehrestBaha.Id,
        //                           BahayeVahed = FehrestBaha.BahayeVahed,
        //                           Vahed = FehrestBaha.Vahed,
        //                           Sharh = FehrestBaha.Sharh,
        //                           ShomarehMain = FehrestBaha.Shomareh,
        //                           Shomareh = FehrestBahaItemsReplace != null && FehrestBahaItemsReplace.FBShomarehNew != null
        //                                                     ? FehrestBahaItemsReplace.FBShomarehNew
        //                                                     : FehrestBaha.Shomareh,
        //                           Sal = FehrestBaha.Sal,
        //                           NoeFB = FehrestBaha.NoeFB,
        //                       }).ToList();



        //var fbItems = await (from fb in _context.FBs
        //                     join fehrestbaha in _context.FehrestBahas
        //                     on fb.Shomareh.Substring(0, 6) equals fehrestbaha.Shomareh.Trim()
        //                     where fb.BarAvordId == request.BarAvordUserId && fb.NoeFBId == NoeFBId
        //                            && fehrestbaha.NoeFB == NoeFBId && fehrestbaha.Sal == request.Year
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
        //                .GroupBy(f => f.Shomareh)
        //                .Select(g => g.FirstOrDefault())
        //                .ToListAsync();



        var fbItems = await (
    from fb in _context.FBs

    join fehrestbaha in _context.FehrestBahas
        on fb.Shomareh.Substring(0, 6)
        equals fehrestbaha.Shomareh.Trim()
        into fehrestGroup

    from fehrestbaha in fehrestGroup
        .Where(x =>
            x.NoeFB == NoeFBId &&
            x.Sal == request.Year)
        .DefaultIfEmpty()

    where fb.BarAvordId == request.BarAvordUserId
          && fb.NoeFBId == NoeFBId

    select new FBForGetUserBarAvordDto
    {
        ID = fb.ID,

        BahayeVahedSharh = fb.BahayeVahedSharh.Trim(),

        // شماره واقعی FB حفظ شود
        Shomareh = fb.Shomareh,

        BahayeVahed =
            fehrestbaha == null
                ? "0"
                : fehrestbaha.BahayeVahed != null
                    ? fehrestbaha.BahayeVahed.Trim()
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



        foreach (var fb in fbItems)
        {
            fb.BahayeVahedSharh = fb.BahayeVahedSharh.Trim() == "" ? lstFehrestBahas.Where(x => x.ShomarehMain.Trim() == fb.Shomareh.Substring(0, 6)).First().Sharh : fb.BahayeVahedSharh.Trim();
        }

        //foreach (var item in fbItems)
        //{
        //    foreach (var fbItemReplace in lstFBItemReplaces)
        //    {
        //        if (item.Shomareh == fbItemReplace.FBShomarehMain)
        //        {
        //            item.Shomareh = fbItemReplace.FBShomarehNew;
        //        }
        //    }
        //}


        var replaceDictionary = lstFBItemReplaces
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



        long Id = request.Id;



        var barAvordInFehrest = lstFehrestBahas
        .Where(f => f.Shomareh.StartsWith(request.ShomarehFasl))
        .ToList()
        .GroupJoin(
            fbItems,
            fehrest => fehrest.Shomareh,
            fb => fb.Shomareh,
            (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
        .Select(x => new ViewUserBarAvordOutPutDto
        {
            ItemFbShomareh = x.fehrest.Shomareh,
            ItemFbShomarehMain = x.fehrest.ShomarehMain,
            Sharh = x.fehrest.Sharh,
            //BahayeVahed = x.fehrest.BahayeVahed != null ? x.fbItem != null ? x.fbItem.BahayeVahedZarib != 0 ?
            //             (x.fbItem.BahayeVahedZarib * decimal.Parse(x.fehrest.BahayeVahed)).ToString() : x.fehrest.BahayeVahed : x.fehrest.BahayeVahed : "0",
            BahayeVahed = x.fehrest.BahayeVahed != null ? x.fehrest.BahayeVahed : "0",
            BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
            ItemZarib = x.fbItem != null ? x.fbItem.ItemZarib : null,
            Vahed = x.fehrest.Vahed,
            FBId = x.fbItem?.ID,
            RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
            ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
        }).ToList();


        var fehrestShomarehs = barAvordInFehrest.Select(x => x.ItemFbShomareh).ToList();

        var barAvordOnlyInFbItems = fbItems
            .Where(fb => fb.Shomareh.StartsWith(request.ShomarehFasl) && !fehrestShomarehs.Contains(fb.Shomareh))
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

        var userBarAvordOutPut = barAvordInFehrest
            .Concat(barAvordOnlyInFbItems)
            .OrderBy(x => x.ItemFbShomareh)
            .ToList();

        //List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFilter = userBarAvordOutPut.Where(x => x.FBId != null).ToList();


        List<string> lstItemFbShomarehs = userBarAvordOutPut.Select(x => x.ItemFbShomareh).Distinct().ToList();

        List<clsItemsFields> itemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFBId && lstItemFbShomarehs.Contains(x.ItemShomareh)).OrderBy(x => x.FieldType).ToList();

        List<clsBahayeVahedItemsRelated> lstBVItemRelated = _context.BahayeVahedItemsRelateds.Where(x => lstItemFbShomarehs.Contains(x.ItemShomarehRelated.ToLower().Trim()) && x.NoeFB == NoeFBId && x.Year == Year).ToList();

        var rizMetreByFbId = RizMetre
            .GroupBy(x => x.FBId)
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var item in userBarAvordOutPut)
        {
            decimal Meghdar = 0;
            if (item.FBId is Guid fbId &&
                rizMetreByFbId.TryGetValue(fbId, out var rms))
            {
                item.RizMetre.AddRange(rms);

                Meghdar = rms.Sum(x => x.MeghdarJoz ?? 0);
            }

            if (Meghdar != 0)
            {
                clsBahayeVahedItemsRelated? BVItemsRelated = lstBVItemRelated.FirstOrDefault(x => x.ItemShomarehRelated.Trim() == item.ItemFbShomareh.Trim());
                if (BVItemsRelated != null)
                {
                    var barAvordInFehrest1 = userBarAvordOutPut.FirstOrDefault(x => x.ItemFbShomareh.Trim() == BVItemsRelated.ItemShomareh.Trim());
                    if (barAvordInFehrest1 != null)
                    {
                        long BahayeVahed1 = barAvordInFehrest1.BahayeVahedNew != 0 ? barAvordInFehrest1.BahayeVahedNew : (barAvordInFehrest1.BahayeVahed == null || barAvordInFehrest1.BahayeVahed == "") ? 0 : long.Parse(barAvordInFehrest1.BahayeVahed);
                        item.BahayeVahed = Math.Round(BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                    }
                }
            }

            List<ItemsFieldForUserBarAvordOutPutDto> currentItemsFields = itemsFields
                    .Where(x => x.ItemShomareh.Trim() == item.ItemFbShomarehMain)
                        .Select(x => new ItemsFieldForUserBarAvordOutPutDto
                        {
                            Id = x.Id,
                            ItemShomareh = x.ItemShomareh.Trim(),
                            FieldType = x.FieldType,
                            Vahed = x.Vahed,
                            IsEnteringValue = x.IsEnteringValue,
                            EssentialValue = x.EssentialValue
                        }).ToList();

            item.ItemsFields.AddRange(currentItemsFields);

            if (Meghdar != 0)
            {
                decimal ItemZarib = (item.ItemZarib == null || item.ItemZarib == 0) ? 1 : item.ItemZarib.Value;

                item.Meghdar = Meghdar;
                decimal dBahayeKol = Meghdar * (item.BahayeVahedNew != 0 ? item.BahayeVahedNew : (item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed)));
                item.BahayeKol = dBahayeKol * ItemZarib;
            }
        }

        //var lstUserBarAvordOutPut7L = userBarAvordOutPut.Where(x => x.ItemFbShomareh.Length == 7).ToList();

        //foreach (var item in lstUserBarAvordOutPut7L)
        //{
        //    string currentItemFB = item.ItemFbShomareh.Substring(0, 6);
        //    var currentItem = userBarAvordOutPut.First(x => x.ItemFbShomareh.Trim() == currentItemFB.Trim());
        //    currentItem.RizMetre.AddRange(item.RizMetre);
        //    currentItem.Meghdar += item.Meghdar;
        //    currentItem.BahayeKol = decimal.Parse(currentItem.BahayeVahed == null ? "0" : currentItem.BahayeVahed) * currentItem.Meghdar;
        //    userBarAvordOutPut.Remove(item);
        //}

        List<ItemFBStarForShowBaravordDto> lstItemFBStars =
            _context.ItemFBStars.Include(x => x.Vahed).Where(x => x.BaravordId == request.BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.Substring(0, 2) == request.ShomarehFasl.Trim())
            .Select(x => new ItemFBStarForShowBaravordDto
            {
                Id = x.ID,
                BaravordId = x.BaravordId,
                BahayeVahed = x.BahayeVahed,
                Sharh = x.Sharh,
                Shomareh = x.Shomareh,
                VahedId = x.VahedId,
                VahedName = x.Vahed.Name,
                blnKharidTajhizat = x.blnKharidTajhizat,
                ItemZaribStar = x.ItemZaribStar,
                RizMetre = new List<ViewBarAvordItemStarOutPutRizMetreDto>()
            }).ToList();

        List<Guid> lstItemFBStarIds = lstItemFBStars.Select(x => x.Id).ToList();

        var RizMetreStar = _context.RizMetreStars.Where(x => lstItemFBStarIds.Contains(x.ItemFBStarId))
        .Select(riz => new ViewBarAvordItemStarOutPutRizMetreDto
        {
            Id = riz.ID,
            Sharh = riz.Sharh,
            TajmieItem = riz.TajmieItem,
            Shomareh = riz.Shomareh,
            ShomarehNew = riz.ShomarehNew,
            Arz = riz.Arz,
            Des = riz.Des,
            Ertefa = riz.Ertefa,
            Tedad = riz.Tedad,
            Tool = riz.Tool,
            Vazn = riz.Vazn,
            MeghdarJoz = riz.MeghdarJoz,
            ItemFBStarId = riz.ItemFBStarId
        }).OrderBy(x => x.Shomareh).ToList();


        foreach (var item in lstItemFBStars)
        {

            decimal ItemZaribStar = (item.ItemZaribStar == null || item.ItemZaribStar == 0) ? 1 : item.ItemZaribStar.Value;
            decimal Meghdar = 0;
            foreach (var RM in RizMetreStar)
            {
                if (item.Id == RM.ItemFBStarId)
                {
                    item.RizMetre.Add(RM);
                    Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                }
            }




            item.Meghdar = Meghdar;
            decimal dBahayeKol = Meghdar * item.BahayeVahed * ItemZaribStar;
            item.BahayeKol = dBahayeKol;
        }

        var result = new
        {
            userBarAvordOutPut,
            lstItemFBStars
        };

        return new JsonResult(result);
    }
}
