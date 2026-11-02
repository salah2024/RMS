using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Tajmie.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Tajmie;

public class BarAvordTajmieController : Controller
{
    private readonly ApplicationDbContext _context;
    public BarAvordTajmieController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewBarAvordTajmieForm");

        return View();

    }





    [HttpPost]
    public async Task<JsonResult> GetBaravordTajmie(
    [FromBody] BarAvordTajmieCalculateValueDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        string FaslCode = request.FaslCode.Trim();
        int Year = request.Year;


        // ============================================================
        // 1. دریافت آیتم‌های تجمیع فصول
        // ============================================================

        List<clsTajmieFosoulItems> lstTFI =
            await _context.TajmieFosoulItemses
                .Include(x => x.TajmieFosoul)
                .Where(x =>
                    x.TajmieFosoul.NoeFBId == NoeFB)
                .ToListAsync();


        // ============================================================
        // 2. دریافت مقادیر ذخیره شده آیتم‌های تجمیعی
        // ============================================================

        var lstBarAvordUserTajmie =
            await _context.BarAvordUserTajmies
                .Where(x =>
                    x.BarAvordUserId == BarAvordId)
                .ToListAsync();


        // ============================================================
        // 3. دریافت فهرست بها
        // ============================================================

        List<clsFehrestBaha> lstFBs =
            await _context.FehrestBahas
                .Where(x =>
                    x.Sal == Year &&
                    x.NoeFB == NoeFB)
                .ToListAsync();


        // ============================================================
        // 4. دریافت ضرایب
        // ============================================================

        RequestGetZarayebDto requestGetZarayeb =
            new RequestGetZarayebDto
            {
                BarAvordId = BarAvordId,
                NoeFBId = NoeFB,
                Year = Year
            };


        ResultGetZarayebDto result1 =
            RizMetreCommon.GetZarayeb(
                requestGetZarayeb,
                _context);


        decimal zaribBalaSari =
            result1.zaribBalaSari ?? 1;

        decimal zaribManteghe =
            result1.zaribManteghe ?? 1;


        // ============================================================
        // 5. دریافت تمام ریزمتره‌های معمولی پروژه
        //
        // مهم:
        // اینجا دیگر FaslCode فیلتر نمی‌شود.
        //
        // چون برای درصد وزنی باید کل پروژه را داشته باشیم.
        // ============================================================

        var lstRM =
            await _context.RizMetreUserses

                .Include(x => x.FB)

                .Where(x =>
                    x.FB.BarAvordId == BarAvordId &&
                    x.FB.NoeFBId == NoeFB &&
                    x.TajmieItem != null &&
                    x.TajmieItem.Length >= 2)

                .Select(x =>
                    new RizMetreAndFBShomarehForBarAvordTajmieDto
                    {
                        MeghdarJoz = x.MeghdarJoz,

                        Shomareh = x.Shomareh,

                        FBShomareh = x.FB.Shomareh,

                        TajmieItem = x.TajmieItem,

                        FBSharh = null,

                        FBVahed = null,

                        BahayeVahed =
                            x.FB.BahayeVahedNew,

                        IsStar = false,

                        ItemZaribStar = null,

                        blnKharidTajhizat = false
                    })

                .ToListAsync();


        // ============================================================
        // 6. دریافت تمام شماره‌های فهرست بها
        // ============================================================

        var lstFBShomareh =
            lstRM

                .Where(x =>
                    !string.IsNullOrWhiteSpace(
                        x.FBShomareh))

                .Select(x =>
                    new FBForBarAvordTajmieDto
                    {
                        Shomareh =
                            x.FBShomareh,

                        BahayeVahed =
                            x.BahayeVahed.ToString()
                    })

                .Distinct()
                .ToList();


        // ============================================================
        // 7. لیست شماره‌های فهرست بها
        // ============================================================

        var fbShomarehList =
            lstFBShomareh

                .Select(x =>
                    x.Shomareh?.Trim())

                .Where(x =>
                    !string.IsNullOrWhiteSpace(x))

                .Distinct()
                .ToList();


        // ============================================================
        // 8. دریافت ضرایب و Replace
        // ============================================================

        var replaceList =
            await (

                from z in _context.ZarayebFosouls

                join r in _context.FehrestBahaItemsReplaces
                    on z.FaslId equals r.FaslId

                where
                    r.Year == Year &&
                    r.NoeFB == NoeFB &&
                    fbShomarehList.Contains(
                        r.FBShomarehMain)

                select new ZaribFaslResultDto
                {
                    FBShomarehMain =
                        r.FBShomarehMain,

                    FBShomarehNew =
                        r.FBShomarehNew,

                    Year =
                        r.Year,

                    NoeFB =
                        r.NoeFB,

                    FaslId =
                        z.FaslId,

                    ZaribBalaSari =
                        z.ZaribBalaSari,

                    ZaribManteghe =
                        z.ZaribManteghe
                })

            .ToListAsync();


        // ============================================================
        // 9. شماره‌هایی که Replace ندارند
        // ============================================================

        var existingFBShomareh =
            replaceList

                .Select(x =>
                    x.FBShomarehMain?.Trim())

                .Where(x =>
                    !string.IsNullOrWhiteSpace(x))

                .ToHashSet();


        var missingItems =
            fbShomarehList

                .Where(x =>
                    !existingFBShomareh.Contains(x))

                .Select(x =>
                    new ZaribFaslResultDto
                    {
                        FBShomarehMain =
                            x,

                        FBShomarehNew =
                            null,

                        Year =
                            Year,

                        NoeFB =
                            NoeFB,

                        FaslId =
                            null,

                        ZaribBalaSari =
                            null,

                        ZaribManteghe =
                            null
                    })

                .ToList();


        // ============================================================
        // 10. ترکیب Replace و موارد بدون Replace
        // ============================================================

        var result =
            replaceList
                .Concat(missingItems)
                .ToList();


        // ============================================================
        // 11. تکمیل ضرایب
        // ============================================================

        foreach (var item in result)
        {
            item.ZaribBalaSari ??=
                zaribBalaSari;

            item.ZaribManteghe ??=
                zaribManteghe;
        }


        // ============================================================
        // 12. Dictionary ضرایب
        // ============================================================

        var zaribDictionary =
            result

                .Where(x =>
                    !string.IsNullOrWhiteSpace(
                        x.FBShomarehMain))

                .GroupBy(x =>
                    x.FBShomarehMain.Trim())

                .ToDictionary(
                    g => g.Key,
                    g => g.First());


        // ============================================================
        // 13. Dictionary فهرست بها
        // ============================================================

        var fbDictionary =
            lstFBs

                .Where(x =>
                    !string.IsNullOrWhiteSpace(
                        x.Shomareh))

                .GroupBy(x =>
                    x.Shomareh.Trim())

                .ToDictionary(
                    g => g.Key,
                    g => g.First());


        // ============================================================
        // 14. تکمیل اطلاعات فهرست بها
        // ============================================================

        foreach (var item in lstRM)
        {
            if (string.IsNullOrWhiteSpace(
                    item.FBShomareh))
                continue;


            var key =
                item.FBShomareh.Trim();


            if (!fbDictionary.TryGetValue(
                    key,
                    out var fb))
                continue;


            item.FBSharh =
                fb.Sharh;

            item.FBVahed =
                fb.Vahed;


            if (item.BahayeVahed != 0)
                continue;


            if (string.IsNullOrWhiteSpace(
                    fb.BahayeVahed))
                continue;


            if (long.TryParse(
                    fb.BahayeVahed,
                    out long bahayeVahed))
            {
                item.BahayeVahed =
                    bahayeVahed;
            }
        }


        // ============================================================
        // 15. دریافت تمام ریزمتره‌های Star پروژه
        // ============================================================

        var lstRMStar =
            await _context.RizMetreStars

                .Include(x =>
                    x.ItemFBStar)

                .ThenInclude(x =>
                    x.Vahed)

                .Where(x =>
                    x.ItemFBStar.BaravordId ==
                        BarAvordId &&

                    x.ItemFBStar.NoeFBId ==
                        NoeFB &&

                    x.TajmieItem != null &&

                    x.TajmieItem.Length >= 2)

                .Select(x =>
                    new RizMetreAndFBShomarehForBarAvordTajmieDto
                    {
                        MeghdarJoz =
                            x.MeghdarJoz,

                        Shomareh =
                            x.Shomareh,

                        FBShomareh =
                            x.ItemFBStar.Shomareh,

                        TajmieItem =
                            x.TajmieItem,

                        FBSharh =
                            x.ItemFBStar.Sharh,

                        FBVahed =
                            x.ItemFBStar.Vahed.Name,

                        ItemZaribStar =
                            x.ItemFBStar.ItemZaribStar,

                        blnKharidTajhizat =
                            x.ItemFBStar.blnKharidTajhizat,

                        IsStar =
                            true,

                        BahayeVahed =
                            Math.Round(

                                x.ItemFBStar.BahayeVahed

                                * (x.ItemFBStar.ItemZaribStar ?? 1)

                                * (
                                    x.ItemFBStar.blnKharidTajhizat == true
                                        ? 1.14m
                                        : (result1.zaribBalaSari ?? 1)
                                          *
                                          (result1.zaribManteghe ?? 1)
                                  ),

                                0,

                                MidpointRounding.AwayFromZero)
                    })

                .ToListAsync();


        // ============================================================
        // 16. ترکیب کل ریزمتره‌های پروژه
        // ============================================================

        var allRM =
            lstRM

                .Concat(lstRMStar)

                .Where(x =>
                    !string.IsNullOrWhiteSpace(
                        x.TajmieItem))

                .ToList();


        // ============================================================
        // 17. محاسبه بهای کل محاسباتی هر آیتم تجمیعی
        //
        // این Dictionary برای آیتم‌هایی استفاده می‌شود
        // که هنوز در tblBarAvordUserTajmie ذخیره نشده‌اند.
        // ============================================================

        var calculatedBahayeKolDictionary =
            allRM

                .GroupBy(x =>
                    x.TajmieItem)

                .ToDictionary(
                    g => g.Key,
                    g =>
                        g.Sum(x =>
                        {
                            // =========================================
                            // STAR
                            // =========================================

                            if (x.IsStar)
                            {
                                return
                                    (x.MeghdarJoz ?? 0)
                                    *
                                    x.BahayeVahed;
                            }


                            // =========================================
                            // معمولی
                            // =========================================

                            var fbKey =
                                x.FBShomareh?.Trim();


                            if (string.IsNullOrWhiteSpace(
                                    fbKey))
                                return 0;


                            fbDictionary.TryGetValue(
                                fbKey,
                                out var fbItem);


                            zaribDictionary.TryGetValue(
                                fbKey,
                                out var zarib);


                            decimal bahayeVahed =
                                0;


                            if (fbItem != null &&
                                !string.IsNullOrWhiteSpace(
                                    fbItem.BahayeVahed))
                            {
                                decimal.TryParse(
                                    fbItem.BahayeVahed,
                                    out bahayeVahed);
                            }


                            decimal zaribBalaSariItem =
                                zarib?.ZaribBalaSari ?? 1;


                            decimal zaribMantegheItem =
                                zarib?.ZaribManteghe ?? 1;


                            decimal bahayeVahedFinal =
                                bahayeVahed
                                * zaribBalaSariItem
                                * zaribMantegheItem;


                            return
                                (x.MeghdarJoz ?? 0)
                                *
                                bahayeVahedFinal;
                        }));


        // ============================================================
        // 18. محاسبه جمع کل واقعی پروژه
        //
        // ذخیره شده:
        //     BahayeKol ذخیره شده
        //
        // ذخیره نشده:
        //     مقدار محاسباتی
        // ============================================================

        decimal totalProjectBahayeKol = 0;


        foreach (var item in lstTFI)
        {
            var savedItem =
                lstBarAvordUserTajmie
                    .FirstOrDefault(x =>
                        x.TajmieFosoulItemId ==
                        item.Id);


            if (savedItem != null)
            {
                totalProjectBahayeKol +=
                    savedItem.BahayeKol;

                continue;
            }


            if (calculatedBahayeKolDictionary.TryGetValue(
                    item.ItemCode,
                    out decimal calculatedBahayeKol))
            {
                totalProjectBahayeKol +=
                    calculatedBahayeKol;
            }
        }


        // ============================================================
        // 19. فقط آیتم‌های فصل جاری
        //
        // برای نمایش جدول فعلی
        // ============================================================

        var currentFaslRM =
            allRM

                .Where(x =>
                    !string.IsNullOrWhiteSpace(
                        x.TajmieItem) &&

                    x.TajmieItem.Length >= 2 &&

                    x.TajmieItem.Substring(0, 2) ==
                        FaslCode)

                .ToList();


        // ============================================================
        // 20. محاسبه آیتم‌های تجمیعی فصل جاری
        // ============================================================

        var lstTajmie =
            currentFaslRM

                .GroupBy(x =>
                    x.TajmieItem)

                .Select(g =>
                {
                    // ====================================================
                    // پیدا کردن آیتم تجمیعی
                    // ====================================================

                    var item =
                        lstTFI
                            .FirstOrDefault(x =>
                                x.ItemCode ==
                                g.Key);


                    if (item == null)
                        return null;


                    // ====================================================
                    // جزئیات آیتم تجمیعی
                    // ====================================================

                    var fbItems =
                        g

                        .GroupBy(x =>
                            new
                            {
                                IsStar =
                                    x.IsStar,

                                FBShomareh =
                                    x.FBShomareh?.Trim(),

                                Shomareh =
                                    x.Shomareh
                            })

                        .Select(fb =>
                        {
                            var first =
                                fb.First();


                            decimal bahayeVahedFinal =
                                0;


                            string fbSharh =
                                first.FBSharh;

                            string fbVahed =
                                first.FBVahed;

                            string fbShomareh =
                                null;


                            // =================================================
                            // STAR
                            // =================================================

                            if (first.IsStar)
                            {
                                fbShomareh =
                                    first.FBShomareh;

                                fbSharh =
                                    first.FBSharh;

                                fbVahed =
                                    first.FBVahed;


                                bahayeVahedFinal =
                                    first.BahayeVahed;
                            }


                            // =================================================
                            // معمولی
                            // =================================================

                            else
                            {
                                fbShomareh =
                                    first.FBShomareh?.Trim();


                                if (!string.IsNullOrWhiteSpace(
                                        fbShomareh))
                                {
                                    fbDictionary.TryGetValue(
                                        fbShomareh,
                                        out var fbItem);


                                    zaribDictionary.TryGetValue(
                                        fbShomareh,
                                        out var zarib);


                                    if (fbItem != null)
                                    {
                                        fbSharh =
                                            fbItem.Sharh;

                                        fbVahed =
                                            fbItem.Vahed;
                                    }


                                    decimal bahayeVahed =
                                        0;


                                    if (fbItem != null &&
                                        !string.IsNullOrWhiteSpace(
                                            fbItem.BahayeVahed))
                                    {
                                        decimal.TryParse(
                                            fbItem.BahayeVahed,
                                            out bahayeVahed);
                                    }


                                    decimal zaribBalaSariItem =
                                        zarib?.ZaribBalaSari ?? 1;


                                    decimal zaribMantegheItem =
                                        zarib?.ZaribManteghe ?? 1;


                                    bahayeVahedFinal =
                                        bahayeVahed
                                        * zaribBalaSariItem
                                        * zaribMantegheItem;
                                }
                            }


                            // =================================================
                            // مقدار
                            // =================================================

                            decimal meghdar =
                                fb.Sum(x =>
                                    x.MeghdarJoz ?? 0);


                            // =================================================
                            // بهای کل ریزآیتم
                            // =================================================

                            decimal bahayeKolInMeghdar =
                                bahayeVahedFinal *
                                meghdar;


                            // =================================================
                            // درصد وزنی ریزآیتم نسبت به کل پروژه
                            // =================================================

                            decimal weightPercent =
                                totalProjectBahayeKol != 0

                                    ? Math.Round(
                                        (
                                            bahayeKolInMeghdar /
                                            totalProjectBahayeKol
                                        ) * 100,

                                        2,

                                        MidpointRounding.AwayFromZero)

                                    : 0;


                            // =================================================
                            // نتیجه
                            // =================================================

                            return new FBShomarehValueDto
                            {
                                FBShomareh =
                                    fbShomareh,

                                FBSharh =
                                    fbSharh,

                                Value =
                                    meghdar,

                                BahayeVahed =
                                    bahayeVahedFinal.ToString(),

                                BahayeKolInMeghdar =
                                    bahayeKolInMeghdar.ToString(),

                                WeightPercent =
                                    weightPercent
                            };

                        })

                        .ToList();


                    // ====================================================
                    // 21. محاسبه بهای کل محاسباتی آیتم تجمیعی
                    // ====================================================

                    decimal bahayehKol =
                        g.Sum(x =>
                        {
                            // =============================================
                            // STAR
                            // =============================================

                            if (x.IsStar)
                            {
                                return
                                    (x.MeghdarJoz ?? 0)
                                    *
                                    x.BahayeVahed;
                            }


                            // =============================================
                            // معمولی
                            // =============================================

                            var fbKey =
                                x.FBShomareh?.Trim();


                            if (string.IsNullOrWhiteSpace(
                                    fbKey))
                                return 0;


                            fbDictionary.TryGetValue(
                                fbKey,
                                out var fbItem);


                            zaribDictionary.TryGetValue(
                                fbKey,
                                out var zarib);


                            decimal bahayeVahed =
                                0;


                            if (fbItem != null &&
                                !string.IsNullOrWhiteSpace(
                                    fbItem.BahayeVahed))
                            {
                                decimal.TryParse(
                                    fbItem.BahayeVahed,
                                    out bahayeVahed);
                            }


                            decimal zaribBalaSariItem =
                                zarib?.ZaribBalaSari ?? 1;


                            decimal zaribMantegheItem =
                                zarib?.ZaribManteghe ?? 1;


                            decimal bahayeVahedFinal =
                                bahayeVahed
                                * zaribBalaSariItem
                                * zaribMantegheItem;


                            return
                                (x.MeghdarJoz ?? 0)
                                *
                                bahayeVahedFinal;
                        });


                    // ====================================================
                    // 22. رکورد ذخیره شده
                    // ====================================================

                    var savedItem =
                        lstBarAvordUserTajmie
                            .FirstOrDefault(x =>
                                x.TajmieFosoulItemId ==
                                item.Id);


                    // ====================================================
                    // 23. مقادیر خروجی
                    // ====================================================

                    decimal meghdar = 0;

                    decimal bahayeVahed = 0;

                    decimal bahayeKol = 0;


                    if (savedItem != null)
                    {
                        // ================================================
                        // مقدار ذخیره شده
                        // ================================================

                        meghdar =
                            savedItem.Meghdar;

                        bahayeVahed =
                            savedItem.BahayeVahed;

                        bahayeKol =
                            savedItem.BahayeKol;
                    }
                    else
                    {
                        // ================================================
                        // مقدار محاسباتی
                        // ================================================

                        bahayeKol =
                            bahayehKol;
                    }


                    // ====================================================
                    // 24. درصد وزنی آیتم تجمیعی
                    // ====================================================

                    decimal itemWeightPercent =
                        totalProjectBahayeKol != 0

                            ? Math.Round(
                                (
                                    bahayeKol /
                                    totalProjectBahayeKol
                                ) * 100,

                                2,

                                MidpointRounding.AwayFromZero)

                            : 0;


                    // ====================================================
                    // 25. نتیجه
                    // ====================================================

                    return
                        new ResultBarAvordTajmieCalculateValueDto
                        {
                            TajmieFosoulId =
                                item.Id,

                            ItemCode =
                                g.Key,

                            Meghdar =
                                meghdar,

                            Value =
                                g.Sum(x =>
                                    x.MeghdarJoz ?? 0),

                            Sharh =
                                item.Sharh,

                            Vahed =
                                item.Vahed,

                            BahayeVahed =
                                bahayeVahed,

                            BahayehKol =
                                bahayeKol,

                            // درصد وزنی آیتم تجمیعی
                            WeightPercent =
                                itemWeightPercent,

                            Items =
                                fbItems
                        };
                })

                .Where(x =>
                    x != null)

                .OrderBy(x =>
                    x.ItemCode)

                .ToList();


        // ============================================================
        // 26. خروجی
        // ============================================================

        return new JsonResult(lstTajmie);
    }

    //[HttpPost]
    //public async Task<JsonResult> GetBaravordTajmie([FromBody] BarAvordTajmieCalculateValueDto request)
    //{

    //    Guid BarAvordId = request.BarAvordId;
    //    NoeFehrestBaha NoeFB = request.NoeFB;
    //    string FaslCode = request.FaslCode.Trim();
    //    int Year = request.Year;


    //    // ============================================================
    //    // 1. دریافت آیتم‌های تجمیع فصول
    //    // ============================================================

    //    List<clsTajmieFosoulItems> lstTFI =
    //        _context.TajmieFosoulItemses
    //            .Include(x => x.TajmieFosoul)
    //            .Where(x => x.TajmieFosoul.NoeFBId == NoeFB)
    //            .ToList();

    //    // ============================================================
    //    // دریافت مقادیر ذخیره شده آیتم‌های تجمیعی
    //    // ============================================================

    //    var lstBarAvordUserTajmie =
    //        await _context.BarAvordUserTajmies
    //            .Where(x =>
    //                x.BarAvordUserId == BarAvordId)
    //            .ToListAsync();

    //    // ============================================================
    //    // 2. دریافت فهرست بها
    //    // ============================================================

    //    List<clsFehrestBaha> lstFBs =
    //        _context.FehrestBahas
    //            .Where(x =>
    //                x.Sal == Year &&
    //                x.NoeFB == NoeFB)
    //            .ToList();


    //    // ============================================================
    //    // 3. دریافت ضرایب
    //    // ============================================================

    //    RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
    //    {
    //        BarAvordId = BarAvordId,
    //        NoeFBId = NoeFB,
    //        Year = Year
    //    };

    //    ResultGetZarayebDto result1 =
    //        RizMetreCommon.GetZarayeb(
    //            requestGetZarayeb,
    //            _context
    //        );

    //    decimal zaribBalaSari =
    //        result1.zaribBalaSari ?? 1;

    //    decimal zaribManteghe =
    //        result1.zaribManteghe ?? 1;


    //    // ============================================================
    //    // 4. دریافت ریزمتره‌های معمولی
    //    // ============================================================

    //    var lstRM = await _context.RizMetreUserses

    //        .Include(x => x.FB)

    //        .Where(x =>
    //            x.FB.BarAvordId == BarAvordId &&
    //            x.FB.NoeFBId == NoeFB &&
    //            x.TajmieItem != null &&
    //            x.TajmieItem.Length >= 2 &&
    //            x.TajmieItem.Substring(0, 2) == FaslCode
    //        )

    //        .Select(x => new RizMetreAndFBShomarehForBarAvordTajmieDto
    //        {
    //            MeghdarJoz = x.MeghdarJoz,

    //            Shomareh = x.Shomareh,

    //            FBShomareh = x.FB.Shomareh,

    //            TajmieItem = x.TajmieItem,

    //            FBSharh = null,

    //            FBVahed = null,

    //            BahayeVahed = x.FB.BahayeVahedNew,

    //            IsStar = false,

    //            ItemZaribStar = null,

    //            blnKharidTajhizat = false
    //        })

    //        .ToListAsync();


    //    // ============================================================
    //    // 5. دریافت شماره‌های فهرست بها به صورت یکتا
    //    // ============================================================

    //    var lstFBShomareh = await _context.RizMetreUserses

    //        .Include(x => x.FB)

    //        .Where(x =>
    //            x.FB.BarAvordId == BarAvordId &&
    //            x.FB.NoeFBId == NoeFB &&
    //            x.TajmieItem != null &&
    //            x.TajmieItem.Length >= 2 &&
    //            x.TajmieItem.Substring(0, 2) == FaslCode
    //        )

    //        .Select(x => new FBForBarAvordTajmieDto
    //        {
    //            Shomareh = x.FB.Shomareh,

    //            BahayeVahed =
    //                x.FB.BahayeVahedNew.ToString()
    //        })

    //        .Distinct()

    //        .ToListAsync();


    //    // ============================================================
    //    // 6. لیست شماره‌های فهرست بها
    //    // ============================================================

    //    var fbShomarehList = lstFBShomareh

    //        .Select(x => x.Shomareh?.Trim())

    //        .Where(x => !string.IsNullOrWhiteSpace(x))

    //        .Distinct()

    //        .ToList();


    //    // ============================================================
    //    // 7. دریافت ضرایب و آیتم‌های جایگزین
    //    // ============================================================

    //    var replaceList = await (

    //        from z in _context.ZarayebFosouls

    //        join r in _context.FehrestBahaItemsReplaces
    //            on z.FaslId equals r.FaslId

    //        where
    //            r.Year == Year &&
    //            r.NoeFB == NoeFB &&
    //            fbShomarehList.Contains(r.FBShomarehMain)

    //        select new ZaribFaslResultDto
    //        {
    //            FBShomarehMain = r.FBShomarehMain,

    //            FBShomarehNew = r.FBShomarehNew,

    //            Year = r.Year,

    //            NoeFB = r.NoeFB,

    //            FaslId = z.FaslId,

    //            ZaribBalaSari = z.ZaribBalaSari,

    //            ZaribManteghe = z.ZaribManteghe
    //        }

    //    ).ToListAsync();


    //    // ============================================================
    //    // 8. شماره‌هایی که Replace ندارند
    //    // ============================================================

    //    var existingFBShomareh = replaceList

    //        .Select(x => x.FBShomarehMain?.Trim())

    //        .Where(x => !string.IsNullOrWhiteSpace(x))

    //        .ToHashSet();


    //    var missingItems = fbShomarehList

    //        .Where(x => !existingFBShomareh.Contains(x))

    //        .Select(x => new ZaribFaslResultDto
    //        {
    //            FBShomarehMain = x,

    //            FBShomarehNew = null,

    //            Year = Year,

    //            NoeFB = NoeFB,

    //            FaslId = null,

    //            ZaribBalaSari = null,

    //            ZaribManteghe = null
    //        })

    //        .ToList();


    //    // ============================================================
    //    // 9. ترکیب Replace و شماره‌های بدون Replace
    //    // ============================================================

    //    var result = replaceList

    //        .Concat(missingItems)

    //        .ToList();


    //    // ============================================================
    //    // 10. تکمیل ضرایب Null
    //    // ============================================================

    //    foreach (var item in result)
    //    {
    //        item.ZaribBalaSari ??= zaribBalaSari;

    //        item.ZaribManteghe ??= zaribManteghe;
    //    }


    //    // ============================================================
    //    // 11. Dictionary ضرایب
    //    // ============================================================

    //    var zaribDictionary = result

    //        .Where(x =>
    //            !string.IsNullOrWhiteSpace(
    //                x.FBShomarehMain))

    //        .GroupBy(x =>
    //            x.FBShomarehMain.Trim())

    //        .ToDictionary(
    //            g => g.Key,
    //            g => g.First()
    //        );


    //    // ============================================================
    //    // 12. Dictionary فهرست بها
    //    // ============================================================

    //    var fbDictionary = lstFBs

    //        .Where(x =>
    //            !string.IsNullOrWhiteSpace(x.Shomareh))

    //        .GroupBy(x =>
    //            x.Shomareh.Trim())

    //        .ToDictionary(
    //            g => g.Key,
    //            g => g.First()
    //        );


    //    // ============================================================
    //    // 13. تکمیل اطلاعات فهرست بها برای ریزمتره معمولی
    //    // ============================================================

    //    foreach (var item in lstRM)
    //    {
    //        if (string.IsNullOrWhiteSpace(item.FBShomareh))
    //            continue;

    //        var key = item.FBShomareh.Trim();

    //        if (!fbDictionary.TryGetValue(
    //                key,
    //                out var fb))
    //        {
    //            continue;
    //        }


    //        // --------------------------------------------
    //        // شرح
    //        // --------------------------------------------

    //        item.FBSharh = fb.Sharh;


    //        // --------------------------------------------
    //        // واحد
    //        // --------------------------------------------

    //        item.FBVahed = fb.Vahed;


    //        // --------------------------------------------
    //        // اگر بهای واحد قبلاً وجود دارد
    //        // نیازی به تکمیل نیست
    //        // --------------------------------------------

    //        if (item.BahayeVahed != 0)
    //            continue;


    //        if (string.IsNullOrWhiteSpace(
    //                fb.BahayeVahed))
    //        {
    //            continue;
    //        }


    //        if (long.TryParse(
    //                fb.BahayeVahed,
    //                out long bahayeVahed))
    //        {
    //            item.BahayeVahed = bahayeVahed;
    //        }
    //    }


    //    // ============================================================
    //    // 14. دریافت ریزمتره‌های Star
    //    // ============================================================
    //    //
    //    // نکته مهم:
    //    //
    //    // در Star:
    //    //
    //    // شرح       ← ItemFBStar.Sharh
    //    // واحد      ← ItemFBStar.Vahed.Name
    //    // بها       ← ItemFBStar.BahayeVahed
    //    //
    //    // بنابراین به FBShomareh وابسته نیستیم.
    //    // ============================================================

    //    var lstRMStar = await _context.RizMetreStars

    //        .Include(x => x.ItemFBStar)
    //            .ThenInclude(x => x.Vahed)

    //        .Where(x =>
    //            x.ItemFBStar.BaravordId == BarAvordId &&
    //            x.ItemFBStar.NoeFBId == NoeFB &&
    //            x.TajmieItem != null &&
    //            x.TajmieItem.Length >= 2 &&
    //            x.TajmieItem.Substring(0, 2) == FaslCode
    //        )

    //        .Select(x => new RizMetreAndFBShomarehForBarAvordTajmieDto
    //        {
    //            MeghdarJoz = x.MeghdarJoz,

    //            Shomareh = x.Shomareh,

    //            // ----------------------------------------
    //            // Star شماره فهرست بها ندارد
    //            // ----------------------------------------

    //            FBShomareh = x.ItemFBStar.Shomareh,

    //            TajmieItem = x.TajmieItem,

    //            // ----------------------------------------
    //            // اطلاعات مستقیماً از ItemFBStar
    //            // ----------------------------------------

    //            FBSharh = x.ItemFBStar.Sharh,

    //            FBVahed = x.ItemFBStar.Vahed.Name,

    //            ItemZaribStar =
    //                x.ItemFBStar.ItemZaribStar,

    //            blnKharidTajhizat =
    //                x.ItemFBStar.blnKharidTajhizat,

    //            IsStar = true,

    //            // ----------------------------------------
    //            // بهای واحد نهایی Star
    //            // ----------------------------------------

    //            BahayeVahed =
    //                Math.Round(
    //                x.ItemFBStar.BahayeVahed
    //                * (x.ItemFBStar.ItemZaribStar ?? 1)
    //                * (
    //                x.ItemFBStar.blnKharidTajhizat == true
    //                        ? 1.14m
    //                        : (result1.zaribBalaSari ?? 1) *
    //                        (result1.zaribManteghe ?? 1)
    //                ),
    //                0,
    //                 MidpointRounding.AwayFromZero
    //            )
    //        })
    //        .ToListAsync();


    //    // ============================================================
    //    // 15. ترکیب ریزمتره معمولی و Star
    //    // ============================================================

    //    var allRM = lstRM

    //        .Concat(lstRMStar)

    //        .Where(x =>
    //            !string.IsNullOrWhiteSpace(
    //                x.TajmieItem))

    //        .ToList();


    //    // ============================================================
    //    // 16. محاسبه تجمیع
    //    // ============================================================

    //    var lstTajmie = allRM

    //        .GroupBy(x => x.TajmieItem)

    //        .Select(g =>
    //        {
    //            // ====================================================
    //            // اطلاعات آیتم تجمیعی
    //            // ====================================================

    //            var item = lstTFI

    //                .FirstOrDefault(x =>
    //                    x.ItemCode == g.Key);

    //            if (item == null)
    //                return null;

    //            // ====================================================
    //            // گروه‌بندی آیتم‌ها
    //            //
    //            // معمولی:
    //            //     بر اساس FBShomareh
    //            //
    //            // Star:
    //            //     بر اساس Shomareh
    //            //
    //            // چون Star فاقد FBShomareh است،
    //            // نباید تمام Starها در یک گروه null قرار بگیرند.
    //            // ====================================================

    //            decimal totalProjectBahayeKol = 0;

    //            var fbItems = g

    //                .GroupBy(x => new
    //                {
    //                    IsStar = x.IsStar,

    //                    FBShomareh =
    //                        x.FBShomareh?.Trim(),

    //                    Shomareh =
    //                        x.Shomareh
    //                })

    //                .Select(fb =>
    //                {
    //                    var first = fb.First();

    //                    decimal bahayeVahedFinal = 0;

    //                    string fbSharh = first.FBSharh;

    //                    string fbVahed = first.FBVahed;

    //                    string fbShomareh = null;


    //                    // =================================================
    //                    // STAR
    //                    // =================================================

    //                    if (first.IsStar)
    //                    {
    //                        fbShomareh = first.FBShomareh;

    //                        fbSharh = first.FBSharh;

    //                        fbVahed = first.FBVahed;

    //                        bahayeVahedFinal =
    //                            first.BahayeVahed;
    //                    }


    //                    // =================================================
    //                    // معمولی
    //                    // =================================================

    //                    else
    //                    {
    //                        fbShomareh =
    //                            first.FBShomareh?.Trim();


    //                        if (!string.IsNullOrWhiteSpace(
    //                                fbShomareh))
    //                        {
    //                            fbDictionary.TryGetValue(
    //                                fbShomareh,
    //                                out var fbItem);


    //                            zaribDictionary.TryGetValue(
    //                                fbShomareh,
    //                                out var zarib);


    //                            // -----------------------------------------
    //                            // شرح و واحد
    //                            // -----------------------------------------

    //                            if (fbItem != null)
    //                            {
    //                                fbSharh =
    //                                    fbItem.Sharh;

    //                                fbVahed =
    //                                    fbItem.Vahed;
    //                            }


    //                            // -----------------------------------------
    //                            // بهای پایه
    //                            // -----------------------------------------

    //                            decimal bahayeVahed = 0;


    //                            if (fbItem != null &&
    //                                !string.IsNullOrWhiteSpace(
    //                                    fbItem.BahayeVahed))
    //                            {
    //                                decimal.TryParse(
    //                                    fbItem.BahayeVahed,
    //                                    out bahayeVahed);
    //                            }


    //                            // -----------------------------------------
    //                            // ضرایب
    //                            // -----------------------------------------

    //                            decimal zaribBalaSariItem =
    //                                zarib?.ZaribBalaSari ?? 1;

    //                            decimal zaribMantegheItem =
    //                                zarib?.ZaribManteghe ?? 1;


    //                            // -----------------------------------------
    //                            // بهای واحد نهایی
    //                            // -----------------------------------------

    //                            bahayeVahedFinal =
    //                                bahayeVahed
    //                                * zaribBalaSariItem
    //                                * zaribMantegheItem;
    //                        }
    //                    }


    //                    // =================================================
    //                    // مقدار
    //                    // =================================================

    //                    decimal meghdar =
    //                        fb.Sum(x =>
    //                            x.MeghdarJoz ?? 0);


    //                    // =================================================
    //                    // بهای کل این ردیف
    //                    // =================================================

    //                    decimal dBahayeKolInMeghdar =
    //                        bahayeVahedFinal * meghdar;


    //                    // =================================================
    //                    // درصد وزنی از کل پروژه
    //                    // =================================================

    //                    decimal weightPercent =
    //                        totalProjectBahayeKol != 0
    //                            ? Math.Round(
    //                                (dBahayeKolInMeghdar /
    //                                 totalProjectBahayeKol) * 100,
    //                                2,
    //                                MidpointRounding.AwayFromZero)
    //                            : 0;


    //                    return new FBShomarehValueDto
    //                    {
    //                        FBShomareh =
    //                            fbShomareh,

    //                        FBSharh =
    //                            fbSharh,

    //                        Value =
    //                            meghdar,

    //                        BahayeVahed =
    //                            bahayeVahedFinal.ToString(),

    //                        BahayeKolInMeghdar =
    //                            dBahayeKolInMeghdar.ToString(),

    //                        WeightPercent =
    //                            weightPercent
    //                    };

    //                })

    //                .ToList();

    //            // ====================================================
    //            // 17. محاسبه بهای کل آیتم تجمیعی
    //            // ====================================================

    //            var bahayehKol = g.Sum(x =>
    //            {
    //                // =================================================
    //                // STAR
    //                // =================================================

    //                if (x.IsStar)
    //                {
    //                    // ---------------------------------------------
    //                    // بهای واحد Star قبلاً محاسبه شده
    //                    // ---------------------------------------------

    //                    return
    //                        (x.MeghdarJoz ?? 0)
    //                        *
    //                        x.BahayeVahed;
    //                }


    //                // =================================================
    //                // معمولی
    //                // =================================================

    //                var fbKey =
    //                    x.FBShomareh?.Trim();


    //                if (string.IsNullOrWhiteSpace(fbKey))
    //                    return 0;


    //                // ---------------------------------------------
    //                // اطلاعات فهرست بها
    //                // ---------------------------------------------

    //                fbDictionary.TryGetValue(
    //                    fbKey,
    //                    out var fbItem);


    //                // ---------------------------------------------
    //                // اطلاعات ضرایب
    //                // ---------------------------------------------

    //                zaribDictionary.TryGetValue(
    //                    fbKey,
    //                    out var zarib);


    //                // ---------------------------------------------
    //                // بهای واحد پایه
    //                // ---------------------------------------------

    //                decimal bahayeVahed = 0;


    //                if (fbItem != null &&
    //                    !string.IsNullOrWhiteSpace(
    //                        fbItem.BahayeVahed))
    //                {
    //                    decimal.TryParse(
    //                        fbItem.BahayeVahed,
    //                        out bahayeVahed);
    //                }


    //                // ---------------------------------------------
    //                // ضرایب
    //                // ---------------------------------------------

    //                decimal zaribBalaSariItem =
    //                    zarib?.ZaribBalaSari ?? 1;


    //                decimal zaribMantegheItem =
    //                    zarib?.ZaribManteghe ?? 1;


    //                // ---------------------------------------------
    //                // بهای واحد نهایی
    //                // ---------------------------------------------

    //                decimal bahayeVahedFinal =

    //                    bahayeVahed

    //                    * zaribBalaSariItem

    //                    * zaribMantegheItem;


    //                // ---------------------------------------------
    //                // بهای کل
    //                // ---------------------------------------------

    //                return
    //                    (x.MeghdarJoz ?? 0)
    //                    *
    //                    bahayeVahedFinal;
    //            });

    //            // ====================================================
    //            // رکورد ذخیره شده برای این آیتم تجمیعی
    //            // ====================================================

    //            var savedItem = lstBarAvordUserTajmie
    //                .FirstOrDefault(x =>
    //                    x.TajmieFosoulItemId == item.Id);


    //            // ====================================================
    //            // مقادیر اولیه
    //            // ====================================================

    //            decimal meghdar = 0;

    //            decimal bahayeVahed = 0;

    //            decimal bahayeKol = 0;


    //            // ====================================================
    //            // اگر رکورد قبلاً ذخیره شده باشد
    //            // مقادیر ذخیره شده را لود کن
    //            // ====================================================

    //            if (savedItem != null)
    //            {
    //                meghdar = savedItem.Meghdar;

    //                bahayeVahed = savedItem.BahayeVahed;

    //                bahayeKol = savedItem.BahayeKol;
    //            }
    //            else
    //            {
    //                // اگر هنوز ذخیره نشده،
    //                // مقدار محاسباتی فعلی استفاده شود

    //                //meghdar = g.Sum(x =>
    //                //    x.MeghdarJoz ?? 0);

    //                //bahayeVahed =
    //                //    meghdar != 0
    //                //        ? bahayehKol / meghdar
    //                //        : 0;

    //                bahayeKol = bahayehKol;
    //            }


    //            // ====================================================
    //            // نتیجه
    //            // ====================================================

    //            return new ResultBarAvordTajmieCalculateValueDto
    //            {
    //                TajmieFosoulId = item.Id,

    //                ItemCode = g.Key,

    //                // مقدار ذخیره شده
    //                Meghdar = meghdar,

    //                // مقدار محاسباتی ریزمتره
    //                Value = g.Sum(x =>
    //                    x.MeghdarJoz ?? 0),

    //                Sharh = item.Sharh,

    //                Vahed = item.Vahed,

    //                // بهای واحد ذخیره شده
    //                BahayeVahed = bahayeVahed,

    //                // بهای کل ذخیره شده
    //                BahayehKol = bahayeKol,

    //                Items = fbItems
    //            };

    //        })
    //        .Where(x => x != null)
    //        .OrderBy(x => x.ItemCode)
    //        .ToList();


    //    // ============================================================
    //    // محاسبه جمع کل پروژه برای درصد وزنی
    //    // ============================================================

    //    // بهای کل محاسباتی آیتم‌های موجود در این بخش
    //    var calculatedProjectBahayeKol =
    //        allRM
    //            .GroupBy(x => x.TajmieItem)
    //            .Sum(g =>
    //            {
    //                return g.Sum(x =>
    //                {
    //                    // -----------------------------------------
    //                    // STAR
    //                    // -----------------------------------------

    //                    if (x.IsStar)
    //                    {
    //                        return
    //                            (x.MeghdarJoz ?? 0)
    //                            * x.BahayeVahed;
    //                    }


    //                    // -----------------------------------------
    //                    // معمولی
    //                    // -----------------------------------------

    //                    var fbKey =
    //                        x.FBShomareh?.Trim();

    //                    if (string.IsNullOrWhiteSpace(fbKey))
    //                        return 0;


    //                    fbDictionary.TryGetValue(
    //                        fbKey,
    //                        out var fbItem);

    //                    zaribDictionary.TryGetValue(
    //                        fbKey,
    //                        out var zarib);


    //                    decimal bahayeVahed = 0;

    //                    if (fbItem != null &&
    //                        !string.IsNullOrWhiteSpace(
    //                            fbItem.BahayeVahed))
    //                    {
    //                        decimal.TryParse(
    //                            fbItem.BahayeVahed,
    //                            out bahayeVahed);
    //                    }


    //                    decimal zaribBalaSariItem =
    //                        zarib?.ZaribBalaSari ?? 1;

    //                    decimal zaribMantegheItem =
    //                        zarib?.ZaribManteghe ?? 1;


    //                    decimal bahayeVahedFinal =
    //                        bahayeVahed
    //                        * zaribBalaSariItem
    //                        * zaribMantegheItem;


    //                    return
    //                        (x.MeghdarJoz ?? 0)
    //                        * bahayeVahedFinal;
    //                });
    //            });

















    //    return new JsonResult(lstTajmie);

    //}




    [HttpPost]
    public async Task<IActionResult> InsertUpdateBarAvordUserTajmie(
    [FromBody] BarAvordUserTajmieDto model)
    {
        try
        {
            var result = await pInsertUpdateBarAvordUserTajmie(model);

            return Json(new
            {
                success = true,
                data = result
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                success = false,
                message = ex.Message
            });
        }
    }


    private async Task<clsBarAvordUserTajmie>
    pInsertUpdateBarAvordUserTajmie(
        BarAvordUserTajmieDto model)
    {
        var entity = await _context.BarAvordUserTajmies
            .FirstOrDefaultAsync(x =>
                x.BarAvordUserId == model.BarAvordUserId &&
                x.TajmieFosoulItemId == model.TajmieFosoulItemId);

        if (entity == null)
        {
            entity = new clsBarAvordUserTajmie
            {
                BarAvordUserId = model.BarAvordUserId,

                TajmieFosoulItemId = model.TajmieFosoulItemId,

                BahayeVahed = model.BahayeVahed,

                Meghdar = model.Meghdar,

                BahayeKol = model.BahayeKol,

                InsertDateTime = DateTime.Now,

                //UserInserter = UserId
            };

            _context.BarAvordUserTajmies.Add(entity);
        }
        else
        {
            entity.BahayeVahed = model.BahayeVahed;

            entity.Meghdar = model.Meghdar;

            entity.BahayeKol = model.BahayeKol;

            entity.InsertDateTime = DateTime.Now;

            //entity.UserInserter = UserId;
        }

        await _context.SaveChangesAsync();

        return entity;
    }
}
