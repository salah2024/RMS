using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Tajmie.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Tajmie;

public class TajmieCommon
{
    private readonly ApplicationDbContext _context;

    public TajmieCommon(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<TajmieReportDto> TajmieFosoulAndSub(
        long Year,
        Guid BarAvordId,
        NoeFehrestBaha? noeFBId)
    {
        // ============================================================
        // 1. دریافت فصل‌ها
        // ============================================================

        var lstFosoul = await _context.TajmieFosouls
            .Where(x =>
                x.NoeFBId == noeFBId)
            .Select(x => new
            {
                x.Id,
                x.FaslCode,
                x.Title
            })
            .OrderBy(x => x.FaslCode)
            .ToListAsync();


        // ============================================================
        // 2. دریافت آیتم‌های تجمیعی
        // ============================================================

        var lstTFI = await _context.TajmieFosoulItemses
            .Where(x =>
                x.TajmieFosoul.NoeFBId == noeFBId)
            .ToListAsync();


        // ============================================================
        // 3. دریافت مقادیر ذخیره شده
        // ============================================================

        var lstSavedTajmie =
            await _context.BarAvordUserTajmies
                .Where(x =>
                    x.BarAvordUserId == BarAvordId)
                .ToListAsync();


        // ============================================================
        // 4. دریافت فهرست بها
        // ============================================================

        var lstFB = await _context.FehrestBahas
            .Where(x =>
                x.Sal == Year &&
                x.NoeFB == noeFBId)
            .ToListAsync();


        // ============================================================
        // 5. Dictionary فهرست بها
        // ============================================================

        var fbDictionary = lstFB
            .Where(x =>
                !string.IsNullOrWhiteSpace(x.Shomareh))
            .GroupBy(x =>
                x.Shomareh.Trim())
            .ToDictionary(
                g => g.Key,
                g => g.First());


        // ============================================================
        // 6. ضرایب
        // ============================================================

        RequestGetZarayebDto requestGetZarayeb =
            new RequestGetZarayebDto
            {
                BarAvordId = BarAvordId,
                NoeFBId = (NoeFehrestBaha)noeFBId,
                Year = (int)Year
            };


        ResultGetZarayebDto resultZarayeb =
            RizMetreCommon.GetZarayeb(
                requestGetZarayeb,
                _context);


        decimal zaribBalaSari =
            resultZarayeb.zaribBalaSari ?? 1;


        decimal zaribManteghe =
            resultZarayeb.zaribManteghe ?? 1;


        // ============================================================
        // 7. ریزمتره‌های معمولی
        // ============================================================

        var lstRM = await _context.RizMetreUserses
            .Include(x => x.FB)
            .Where(x =>
                x.FB.BarAvordId == BarAvordId &&
                x.FB.NoeFBId == noeFBId &&
                x.TajmieItem != null)
            .Select(x => new
            {
                x.TajmieItem,
                x.MeghdarJoz,

                FBShomareh =
                    x.FB.Shomareh,

                BahayeVahed =
                    x.FB.BahayeVahedNew
            })
            .ToListAsync();


        // ============================================================
        // 8. ریزمتره‌های Star
        // ============================================================

        var lstRMStar = await _context.RizMetreStars
            .Include(x => x.ItemFBStar)
            .Where(x =>
                x.ItemFBStar.BaravordId == BarAvordId &&
                x.ItemFBStar.NoeFBId == noeFBId &&
                x.TajmieItem != null)
            .Select(x => new
            {
                x.TajmieItem,

                x.MeghdarJoz,

                FBShomareh =
                    x.ItemFBStar.Shomareh,

                BahayeVahed =
                    x.ItemFBStar.BahayeVahed,

                ItemZaribStar =
                    x.ItemFBStar.ItemZaribStar,

                blnKharidTajhizat =
                    x.ItemFBStar.blnKharidTajhizat
            })
            .ToListAsync();


        // ============================================================
        // 9. محاسبات آیتم‌های تجمیعی
        // ============================================================

        var calculatedBahayeKolDictionary =
            new Dictionary<long, decimal>();


        var calculatedMeghdarDictionary =
            new Dictionary<long, decimal>();


        // ============================================================
        // اطلاعات سطح سوم
        // ============================================================

        var detailDictionary =
            new Dictionary<long,
                Dictionary<string, TajmieReportDetailDto>>();


        // ============================================================
        // 10. ریزمتره‌های معمولی
        // ============================================================

        foreach (var rm in lstRM)
        {
            if (string.IsNullOrWhiteSpace(rm.TajmieItem))
                continue;


            string tajmieItemCode =
                rm.TajmieItem.Trim();


            var tajmieItem =
                lstTFI.FirstOrDefault(x =>
                    x.ItemCode == tajmieItemCode);


            if (tajmieItem == null)
                continue;


            string fbKey =
                rm.FBShomareh?.Trim();


            if (string.IsNullOrWhiteSpace(fbKey))
                continue;


            fbDictionary.TryGetValue(
                fbKey,
                out var fbItem);


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


            // --------------------------------------------------------
            // بهای واحد نهایی
            // --------------------------------------------------------

            decimal bahayeVahedFinal =
                bahayeVahed *
                zaribBalaSari *
                zaribManteghe;


            decimal meghdar =
                rm.MeghdarJoz ?? 0;


            decimal bahayeKol =
                meghdar *
                bahayeVahedFinal;


            // --------------------------------------------------------
            // جمع آیتم تجمیعی
            // --------------------------------------------------------

            if (calculatedBahayeKolDictionary
                .ContainsKey(tajmieItem.Id))
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] += bahayeKol;

                calculatedMeghdarDictionary[
                    tajmieItem.Id] += meghdar;
            }
            else
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] = bahayeKol;

                calculatedMeghdarDictionary[
                    tajmieItem.Id] = meghdar;
            }


            // --------------------------------------------------------
            // سطح سوم
            // --------------------------------------------------------

            if (!detailDictionary.ContainsKey(
                tajmieItem.Id))
            {
                detailDictionary[
                    tajmieItem.Id] =
                    new Dictionary<string,
                        TajmieReportDetailDto>();
            }


            if (detailDictionary[
                    tajmieItem.Id]
                .TryGetValue(
                    fbKey,
                    out var detail))
            {
                detail.Meghdar += meghdar;
                detail.BahayeKol += bahayeKol;
            }
            else
            {
                detailDictionary[
                    tajmieItem.Id][fbKey] =
                    new TajmieReportDetailDto
                    {
                        FBShomareh = fbKey,

                        Sharh =
                            fbItem?.Sharh ?? "",

                        Meghdar =
                            meghdar,

                        BahayeVahed =
                            bahayeVahedFinal,

                        BahayeKol =
                            bahayeKol
                    };
            }
        }


        // ============================================================
        // 11. ریزمتره‌های Star
        // ============================================================

        foreach (var rm in lstRMStar)
        {
            if (string.IsNullOrWhiteSpace(
                rm.TajmieItem))
                continue;


            string tajmieItemCode =
                rm.TajmieItem.Trim();


            var tajmieItem =
                lstTFI.FirstOrDefault(x =>
                    x.ItemCode == tajmieItemCode);


            if (tajmieItem == null)
                continue;


            string fbKey =
                rm.FBShomareh?.Trim();


            if (string.IsNullOrWhiteSpace(fbKey))
                continue;


            // --------------------------------------------------------
            // ضرایب Star
            // --------------------------------------------------------

            decimal zaribStar =
                rm.ItemZaribStar ?? 1;


            decimal zaribKharid =
                rm.blnKharidTajhizat == true
                    ? 1.14m
                    : zaribBalaSari *
                      zaribManteghe;


            // --------------------------------------------------------
            // بهای واحد
            // --------------------------------------------------------

            decimal bahayeVahedFinal =
                rm.BahayeVahed *
                zaribStar *
                zaribKharid;


            decimal meghdar =
                rm.MeghdarJoz ?? 0;


            decimal bahayeKol =
                meghdar *
                bahayeVahedFinal;


            // --------------------------------------------------------
            // جمع آیتم
            // --------------------------------------------------------

            if (calculatedBahayeKolDictionary
                .ContainsKey(tajmieItem.Id))
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] += bahayeKol;

                calculatedMeghdarDictionary[
                    tajmieItem.Id] += meghdar;
            }
            else
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] = bahayeKol;

                calculatedMeghdarDictionary[
                    tajmieItem.Id] = meghdar;
            }


            // --------------------------------------------------------
            // سطح سوم
            // --------------------------------------------------------

            if (!detailDictionary.ContainsKey(
                tajmieItem.Id))
            {
                detailDictionary[
                    tajmieItem.Id] =
                    new Dictionary<string,
                        TajmieReportDetailDto>();
            }


            if (detailDictionary[
                    tajmieItem.Id]
                .TryGetValue(
                    fbKey,
                    out var detail))
            {
                detail.Meghdar += meghdar;
                detail.BahayeKol += bahayeKol;
            }
            else
            {
                detailDictionary[
                    tajmieItem.Id][fbKey] =
                    new TajmieReportDetailDto
                    {
                        FBShomareh =
                            fbKey,

                        Sharh = "",

                        Meghdar =
                            meghdar,

                        BahayeVahed =
                            bahayeVahedFinal,

                        BahayeKol =
                            bahayeKol
                    };
            }
        }


        // ============================================================
        // 12. ساخت ساختار سه سطحی
        // ============================================================

        var resultFosoul =
            new List<TajmieReportFaslDto>();


        foreach (var fosoul in lstFosoul)
        {
            var faslDto =
                new TajmieReportFaslDto
                {
                    Id = fosoul.Id,

                    FaslCode =
                        fosoul.FaslCode,

                    Title =
                        fosoul.Title
                };


            var items =
                lstTFI
                    .Where(x =>
                        x.TajmieFosoulId ==
                        fosoul.Id)
                    .OrderBy(x =>
                        x.ItemCode)
                    .ToList();


            foreach (var item in items)
            {
                // ----------------------------------------------------
                // مقدار محاسبه شده
                // ----------------------------------------------------

                decimal calculatedBahayeKol =
                    calculatedBahayeKolDictionary
                        .TryGetValue(
                            item.Id,
                            out var calculatedPrice)
                        ? calculatedPrice
                        : 0;


                decimal calculatedMeghdar =
                    calculatedMeghdarDictionary
                        .TryGetValue(
                            item.Id,
                            out var calculatedQty)
                        ? calculatedQty
                        : 0;


                // ----------------------------------------------------
                // مقدار ذخیره شده
                // ----------------------------------------------------

                var savedItem =
                    lstSavedTajmie
                        .FirstOrDefault(x =>
                            x.TajmieFosoulItemId ==
                            item.Id);


                decimal meghdar;
                decimal bahayeVahed;
                decimal bahayeKol;


                if (savedItem != null)
                {
                    meghdar =
                        savedItem.Meghdar;

                    bahayeVahed =
                        savedItem.BahayeVahed;

                    bahayeKol =
                        savedItem.BahayeKol;
                }
                else
                {
                    meghdar =
                        calculatedMeghdar;

                    bahayeKol =
                        calculatedBahayeKol;

                    bahayeVahed =
                        meghdar != 0
                            ? bahayeKol / meghdar
                            : 0;
                }


                // ----------------------------------------------------
                // سطح سوم
                // ----------------------------------------------------

                var details =
                    detailDictionary.TryGetValue(
                        item.Id,
                        out var itemDetails)
                        ? itemDetails
                            .Values
                            .OrderBy(x =>
                                x.FBShomareh)
                            .ToList()
                        : new List<TajmieReportDetailDto>();


                // ----------------------------------------------------
                // آیتم سطح دوم
                // ----------------------------------------------------

                var itemDto =
                    new TajmieReportItemDto
                    {
                        Id =
                            item.Id,

                        ItemCode =
                            item.ItemCode ?? "",

                        Sharh =
                            item.Sharh ?? "",

                        Vahed =
                            item.Vahed ?? "",

                        Meghdar =
                            meghdar,

                        BahayeVahed =
                            bahayeVahed,

                        BahayeKol =
                            bahayeKol,

                        Details =
                            details
                    };


                faslDto.Items.Add(
                    itemDto);


                // ----------------------------------------------------
                // جمع فصل
                // ----------------------------------------------------

                faslDto.BahayeKol +=
                    bahayeKol;
            }


            resultFosoul.Add(
                faslDto);
        }


        // ============================================================
        // 13. جمع کل
        // ============================================================

        decimal totalAllFosoul =
            resultFosoul.Sum(x =>
                x.BahayeKol);


        // ============================================================
        // 14. درصد وزنی
        // ============================================================

        foreach (var fasl in resultFosoul)
        {
            fasl.WeightPercent =
                totalAllFosoul != 0
                    ? Math.Round(
                        (fasl.BahayeKol /
                         totalAllFosoul) * 100,
                        2,
                        MidpointRounding.AwayFromZero)
                    : 0;
        }


        // ============================================================
        // 15. خروجی نهایی
        // ============================================================

        return new TajmieReportDto
        {
            Fosoul =
                resultFosoul,

            TotalBahayeKol =
                totalAllFosoul,

            TotalWeightPercent =
                totalAllFosoul != 0
                    ? 100
                    : 0
        };
    }
}
