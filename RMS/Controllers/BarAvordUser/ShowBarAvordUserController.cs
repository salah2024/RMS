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
        var perfWatch = System.Diagnostics.Stopwatch.StartNew();
        var perfStages = new Dictionary<string, long>();
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


        string chapterCode = request.ShomarehFasl?.Trim() ?? string.Empty;

        // فقط جایگزینی‌هایی را دریافت می‌کنیم که شماره قدیم یا جدیدشان به فصل جاری مربوط است.
        // هر دو حالت لازم‌اند؛ ممکن است یک آیتم از فصل دیگری به این فصل منتقل شده باشد.
        List<clsFehrestBahaItemsReplace> lstFBItemReplaces = await _context.FehrestBahaItemsReplaces
            .AsNoTracking()
            .Where(x => x.Year == Year && x.NoeFB == NoeFBId &&
                (x.FBShomarehMain.StartsWith(chapterCode) ||
                 (x.FBShomarehNew != null && x.FBShomarehNew.StartsWith(chapterCode))))
            .ToListAsync();
        perfStages["replacementQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();

        var replacementSourceNumbers = lstFBItemReplaces
            .Where(x => x.FBShomarehNew != null && x.FBShomarehNew.StartsWith(chapterCode))
            .Select(x => x.FBShomarehMain)
            .Distinct()
            .ToList();

        // فهرست بها از ابتدا در SQL به فصل جاری محدود می‌شود؛
        // آیتم‌های منتقل‌شده به این فصل نیز از طریق شماره جایگزین‌شده لحاظ می‌شوند.
        var lstFehrestBahas = await (
            from fb in _context.FehrestBahas.AsNoTracking()
            join replace in _context.FehrestBahaItemsReplaces
                .AsNoTracking()
                .Where(x => x.Year == Year && x.NoeFB == NoeFBId &&
                    (x.FBShomarehMain.StartsWith(chapterCode) ||
                     (x.FBShomarehNew != null && x.FBShomarehNew.StartsWith(chapterCode))))
            on fb.Shomareh equals replace.FBShomarehMain into replaces
            from replace in replaces.DefaultIfEmpty()
            where fb.Sal == Year
                && fb.NoeFB == NoeFBId
                && (fb.Shomareh.StartsWith(chapterCode) ||
                    (replace != null && replace.FBShomarehNew != null &&
                     replace.FBShomarehNew.StartsWith(chapterCode)))
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
        perfStages["fehrestQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();




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
        perfStages["fbQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();



        // اطلاعات FB بدون Join رشته‌ای از SQL دریافت می‌شود؛ شماره کامل (از جمله پسوندهایی
        // مانند 150603e) حفظ می‌شود و اتصال به فهرست بها با کلید شش‌رقمی در حافظه انجام می‌گیرد.
        var fbItems = await _context.FBs
            .AsNoTracking()
            .Where(fb => fb.BarAvordId == request.BarAvordUserId &&
                fb.NoeFBId == NoeFBId &&
                (fb.Shomareh.StartsWith(chapterCode) || replacementSourceNumbers.Contains(fb.Shomareh)))
            .Select(fb => new FBForGetUserBarAvordDto
            {
                ID = fb.ID,
                BahayeVahedSharh = fb.BahayeVahedSharh,
                Shomareh = fb.Shomareh,
                BahayeVahed = "0",
                Vahed = null,
                BahayeVahedNew = fb.BahayeVahedNew,
                ItemZarib = fb.ItemZarib,
                SumRizMetre = fb.SumRizMetre
            })
            .ToListAsync();
        perfStages["rizMetreQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();

        // رفتار انتخاب یک FB برای هر شماره کامل مانند GroupBy/First قبلی حفظ می‌شود.
        fbItems = fbItems
            .GroupBy(fb => fb.Shomareh)
            .Select(group => group.First())
            .ToList();

        // ریزمتره‌ها را فقط برای FBهای نهایی همین فصل دریافت می‌کنیم.
        // فیلتر بر اساس ID باعث می‌شود شماره‌های جابه‌جاشده بین فصل‌ها نیز درست پوشش داده شوند.
        var chapterFbIds = fbItems.Select(fb => fb.ID).Distinct().ToList();
        var RizMetre = chapterFbIds.Count == 0
            ? new List<ViewUserBarAvordOutPutRizMetreDto>()
            : await _context.RizMetreUserses
                .AsNoTracking()
                .Where(riz => chapterFbIds.Contains(riz.FBId))
                .OrderBy(riz => riz.Shomareh)
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
                .ToListAsync();
        perfStages["itemsFieldsQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();

        // کلید فهرست بها، شماره اصلی شش‌رقمی است؛ پسوند شماره FB در کلید دخالت ندارد.
        var fehrestByMainNumber = lstFehrestBahas
            .GroupBy(item => item.ShomarehMain.Trim())
            .ToDictionary(group => group.Key, group => group.First());

        foreach (var fb in fbItems)
        {
            var mainNumber = fb.Shomareh.Length >= 6
                ? fb.Shomareh[..6]
                : fb.Shomareh;

            if (fehrestByMainNumber.TryGetValue(mainNumber, out var fehrest))
            {
                fb.BahayeVahed = string.IsNullOrWhiteSpace(fehrest.BahayeVahed)
                    ? "0"
                    : fehrest.BahayeVahed.Trim();
                fb.Vahed = fehrest.Vahed;
            }
        }



        var fehrestSharhByMain = lstFehrestBahas
            .GroupBy(x => x.ShomarehMain.Trim())
            .ToDictionary(g => g.Key, g => g.First().Sharh);

        foreach (var fb in fbItems)
        {
            var currentSharh = fb.BahayeVahedSharh?.Trim();
            if (string.IsNullOrEmpty(currentSharh) &&
                fehrestSharhByMain.TryGetValue(fb.Shomareh[..6], out var fehrestSharh))
            {
                fb.BahayeVahedSharh = fehrestSharh;
            }
            else
            {
                fb.BahayeVahedSharh = currentSharh ?? string.Empty;
            }
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
            Meghdar = x.fbItem?.SumRizMetre ?? 0m,
            RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
            ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
        }).ToList();


        var fehrestShomarehs = barAvordInFehrest
            .Select(x => x.ItemFbShomareh)
            .ToHashSet();

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
                Meghdar = fb.SumRizMetre ?? 0m,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
                ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
            }).ToList();

        var userBarAvordOutPut = barAvordInFehrest
            .Concat(barAvordOnlyInFbItems)
            .OrderBy(x => x.ItemFbShomareh)
            .ToList();

        //List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFilter = userBarAvordOutPut.Where(x => x.FBId != null).ToList();


        List<string> lstItemFbShomarehs = userBarAvordOutPut.Select(x => x.ItemFbShomareh).Distinct().ToList();

        List<clsItemsFields> itemsFields = await _context.ItemsFieldses
            .AsNoTracking()
            .Where(x => x.NoeFB == NoeFBId && lstItemFbShomarehs.Contains(x.ItemShomareh))
            .OrderBy(x => x.FieldType)
            .ToListAsync();
        perfStages["relatedItemsQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();

        List<clsBahayeVahedItemsRelated> lstBVItemRelated = await _context.BahayeVahedItemsRelateds
            .AsNoTracking()
            .Where(x => lstItemFbShomarehs.Contains(x.ItemShomarehRelated.ToLower().Trim()) && x.NoeFB == NoeFBId && x.Year == Year)
            .ToListAsync();
        perfStages["starItemsQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();

        var relatedByItem = lstBVItemRelated
            .GroupBy(x => x.ItemShomarehRelated.Trim(), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

        var outputByItemNumber = userBarAvordOutPut
            .GroupBy(x => x.ItemFbShomareh.Trim(), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

        var fieldsByMainNumber = itemsFields
            .GroupBy(x => x.ItemShomareh.Trim())
            .ToDictionary(g => g.Key, g => g.OrderBy(x => x.FieldType).ToList());

        var rizMetreByFbId = RizMetre
            .GroupBy(x => x.FBId)
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var item in userBarAvordOutPut)
        {
            if (item.FBId is Guid fbId &&
                rizMetreByFbId.TryGetValue(fbId, out var rms))
            {
                item.RizMetre.AddRange(rms);
            }

            decimal Meghdar = item.Meghdar;

            if (Meghdar != 0)
            {
                if (relatedByItem.TryGetValue(item.ItemFbShomareh.Trim(), out var BVItemsRelated))
                {
                    outputByItemNumber.TryGetValue(BVItemsRelated.ItemShomareh.Trim(), out var barAvordInFehrest1);
                    if (barAvordInFehrest1 != null)
                    {
                        long BahayeVahed1 = barAvordInFehrest1.BahayeVahedNew != 0 ? barAvordInFehrest1.BahayeVahedNew : (barAvordInFehrest1.BahayeVahed == null || barAvordInFehrest1.BahayeVahed == "") ? 0 : long.Parse(barAvordInFehrest1.BahayeVahed);
                        item.BahayeVahed = Math.Round(BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                    }
                }
            }

            List<ItemsFieldForUserBarAvordOutPutDto> currentItemsFields =
                fieldsByMainNumber.TryGetValue(item.ItemFbShomarehMain, out var matchingFields)
                    ? matchingFields.Select(x => new ItemsFieldForUserBarAvordOutPutDto
                        {
                            Id = x.Id,
                            ItemShomareh = x.ItemShomareh.Trim(),
                            FieldType = x.FieldType,
                            Vahed = x.Vahed,
                            IsEnteringValue = x.IsEnteringValue,
                            EssentialValue = x.EssentialValue
                        }).ToList()
                    : new List<ItemsFieldForUserBarAvordOutPutDto>();

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

        List<ItemFBStarForShowBaravordDto> lstItemFBStars = await
            _context.ItemFBStars.AsNoTracking()
            .Where(x => x.BaravordId == request.BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.StartsWith(chapterCode))
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
            }).ToListAsync();
        perfStages["starRizMetreQuery"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Restart();

        List<Guid> lstItemFBStarIds = lstItemFBStars.Select(x => x.Id).ToList();

        var RizMetreStar = await _context.RizMetreStars.AsNoTracking().Where(x => lstItemFBStarIds.Contains(x.ItemFBStarId))
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
        }).OrderBy(x => x.Shomareh).ToListAsync();


        var rizMetreStarByItemId = RizMetreStar
            .GroupBy(x => x.ItemFBStarId)
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var item in lstItemFBStars)
        {

            decimal ItemZaribStar = (item.ItemZaribStar == null || item.ItemZaribStar == 0) ? 1 : item.ItemZaribStar.Value;
            decimal Meghdar = 0;
            if (rizMetreStarByItemId.TryGetValue(item.Id, out var starRizMetres))
            {
                item.RizMetre.AddRange(starRizMetres);
                Meghdar = starRizMetres.Sum(rm => rm.MeghdarJoz ?? 0m);
            }




            item.Meghdar = Meghdar;
            decimal dBahayeKol = Meghdar * item.BahayeVahed * ItemZaribStar;
            item.BahayeKol = dBahayeKol;
        }

        var result = new
        {
            userBarAvordOutPut,
            lstItemFBStars,
            debug = new
            {
                timingsMs = perfStages,
                fbCount = fbItems.Count,
                rizMetreCount = RizMetre.Count,
                itemCount = userBarAvordOutPut.Count,
                itemFieldCount = itemsFields.Count,
                relatedItemCount = lstBVItemRelated.Count,
                starItemCount = lstItemFBStars.Count,
                starRizMetreCount = RizMetreStar.Count
            }
        };

        return new JsonResult(result);
        perfStages["mappingAndDtoBuild"] = perfWatch.ElapsedMilliseconds;
        perfWatch.Stop();
    }
}
