using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.BarAvordUser;

public class ShowBarAvordUserController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewShowBarAvordUserForm");

        return View();
    }

    public JsonResult GetUserBarAvord([FromBody] ViewUserBarAvordInputDto request)
    {
        var RizMetre = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == request.BarAvordUserId)
            .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
            {
                Id = riz.ID,
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
            }).OrderBy(x => x.Shomareh).ToList();


        var fbItems = (from fb in _context.FBs
                       join fehrestbaha in _context.FehrestBahas
                       on fb.Shomareh.Substring(0, 6) equals fehrestbaha.Shomareh.Trim()
                       where fb.BarAvordId == request.BarAvordUserId && fehrestbaha.Sal == request.Year
                       select new FBForGetUserBarAvordDto
                       {
                           ID = fb.ID,
                           BahayeVahedSharh = fb.BahayeVahedSharh,
                           Shomareh = fb.Shomareh,
                           BahayeVahed = fehrestbaha.BahayeVahed != null ? (fb.BahayeVahedZarib * decimal.Parse(fehrestbaha.BahayeVahed)).ToString() : fehrestbaha.BahayeVahed,
                           Vahed = fehrestbaha.Vahed,
                           BahayeVahedZarib = fb.BahayeVahedZarib,
                           BahayeVahedNew = fb.BahayeVahedNew
                       })
                        .GroupBy(f => f.Shomareh)
                        .Select(g => g.FirstOrDefault())
                        .ToList();

        // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        //var fbItems1 = _context.FBs
        //    .Where(f => f.BarAvordId == request.BarAvordUserId)
        //    .GroupBy(f => f.Shomareh)
        //    .Select(g => g.FirstOrDefault())
        //    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        //        var userBarAvordOutPut = _context.FehrestBahas
        //            .Where(fehrest => fehrest.Sal == request.Year
        //                           && fehrest.Shomareh.StartsWith(request.ShomarehFasl))
        //            .ToList() // انتقال به حافظه
        //            .GroupJoin(fbItems,
        //                fehrest => fehrest.Shomareh,
        //                fb => fb.Shomareh,
        //                (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
        //            .OrderBy(x => x.fehrest.Shomareh)
        //            .Select(x => new ViewUserBarAvordOutPutDto
        //            {
        //                ItemFbShomareh = x.fehrest.Shomareh,
        //                Sharh = x.fehrest.Sharh,
        //                BahayeVahed = x.fehrest.BahayeVahed,
        //                Vahed = x.fehrest.Vahed,
        //                FBId = x.fbItem != null ? x.fbItem.ID : null,
        //                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
        //            })
        //            .ToList();


        //        var userBarAvordOutPut1 = fbItems
        //.GroupJoin(
        //    _context.FehrestBahas.Where(fehrest => fehrest.Sal == request.Year),
        //    fb => fb.Shomareh,
        //    fehrest => fehrest.Shomareh,
        //    (fb, fehrestGroup) => new { fb, fehrest = fehrestGroup.FirstOrDefault() }
        //)
        //.Where(x => x.fb.Shomareh.StartsWith(request.ShomarehFasl)) // فیلتر روی fb
        //.OrderBy(x => x.fb.Shomareh)
        //.Select(x => new ViewUserBarAvordOutPutDto
        //{
        //    ItemFbShomareh = x.fb.Shomareh,
        //    Sharh = x.fehrest?.Sharh??"",
        //    BahayeVahed = x.fehrest?.BahayeVahed ?? "0",
        //    Vahed = x.fehrest?.Vahed??"",
        //    FBId = x.fb.ID,
        //    RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
        //})
        //.ToList();



        var barAvordInFehrest = _context.FehrestBahas
        .Where(f => f.Sal == request.Year && f.Shomareh.StartsWith(request.ShomarehFasl))
        .ToList()
        .GroupJoin(
            fbItems,
            fehrest => fehrest.Shomareh,
            fb => fb.Shomareh,
            (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
        .Select(x => new ViewUserBarAvordOutPutDto
        {
            ItemFbShomareh = x.fehrest.Shomareh,
            Sharh = x.fehrest.Sharh,
            BahayeVahed = x.fehrest.BahayeVahed != null ? x.fbItem != null ? x.fbItem.BahayeVahedZarib != 0 ? (x.fbItem.BahayeVahedZarib * decimal.Parse(x.fehrest.BahayeVahed)).ToString() : x.fehrest.BahayeVahed : x.fehrest.BahayeVahed : "0",
            BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
            Vahed = x.fehrest.Vahed,
            FBId = x.fbItem?.ID,
            RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
            ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
        }).ToList();

        var fehrestShomarehs = barAvordInFehrest.Select(x => x.ItemFbShomareh).ToHashSet();

        var barAvordOnlyInFbItems = fbItems
            .Where(fb => fb.Shomareh.StartsWith(request.ShomarehFasl) && !fehrestShomarehs.Contains(fb.Shomareh))
            .Select(fb => new ViewUserBarAvordOutPutDto
            {
                ItemFbShomareh = fb.Shomareh,
                Sharh = fb.BahayeVahedSharh,
                BahayeVahed = fb.BahayeVahed,
                Vahed = fb.Vahed,
                FBId = fb.ID,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
                ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
            }).ToList();

        var userBarAvordOutPut = barAvordInFehrest
            .Concat(barAvordOnlyInFbItems)
            .OrderBy(x => x.ItemFbShomareh)
            .ToList();

        List<clsItemsFields> itemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == request.NoeFB).OrderBy(x => x.FieldType).ToList();

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

            List<ItemsFieldForUserBarAvordOutPutDto> currentItemsFields = itemsFields
                .Where(x => x.ItemShomareh.Trim() == item.ItemFbShomareh).Select(x => new ItemsFieldForUserBarAvordOutPutDto
                {
                    Id = x.Id,
                    ItemShomareh = x.ItemShomareh.Trim(),
                    FieldType = x.FieldType,
                    Vahed = x.Vahed,
                    IsEnteringValue = x.IsEnteringValue,
                    EssentialValue = x.EssentialValue
                }).ToList();

            item.ItemsFields.AddRange(currentItemsFields);

            item.Meghdar = Meghdar;
            decimal dBahayeKol = Meghdar * (item.BahayeVahedNew != 0 ? item.BahayeVahedNew : (item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed)));
            item.BahayeKol = dBahayeKol;

        }

        List<ItemFBStarForShowBaravordDto> lstItemFBStars = _context.ItemFBStars.Include(x => x.Vahed).Where(x => x.BaravordId == request.BarAvordUserId && x.Shomareh.Substring(0,2)== request.ShomarehFasl.Trim())
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
                RizMetre = new List<ViewBarAvordItemStarOutPutRizMetreDto>()
            }).ToList();

        List<Guid> lstItemFBStarIds = lstItemFBStars.Select(x => x.Id).ToList();

        var RizMetreStar = _context.RizMetreStars.Where(x => lstItemFBStarIds.Contains(x.ItemFBStarId))
    .Select(riz => new ViewBarAvordItemStarOutPutRizMetreDto
    {
        Id = riz.ID,
        Sharh = riz.Sharh,
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
            decimal Meghdar = 0;
            foreach (var RM in RizMetreStar)
            {
                if (item.Id == RM.ItemFBStarId)
                {
                    item.RizMetre.Add(RM);
                    Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                }
            }

            //List<ItemsFieldForUserBarAvordOutPutDto> currentItemsFields = itemsFields
            //    .Where(x => x.ItemShomareh.Trim() == item.Shomareh).Select(x => new ItemsFieldForUserBarAvordOutPutDto
            //    {
            //        Id = x.Id,
            //        ItemShomareh = x.ItemShomareh.Trim(),
            //        FieldType = x.FieldType,
            //        Vahed = x.Vahed,
            //        IsEnteringValue = x.IsEnteringValue,
            //        EssentialValue = x.EssentialValue
            //    }).ToList();

            //item.ItemsFields.AddRange(currentItemsFields);

            item.Meghdar = Meghdar;
            decimal dBahayeKol = Meghdar * item.BahayeVahed;
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
