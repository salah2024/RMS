using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Common;

public static class HamlCommon
{
    public static async Task<List<Guid>> SaveHaml(SaveHamlDto request, ApplicationDbContext _context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(_context);

        DateTime now = DateTime.Now;
        Guid barAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha noeFBId = request.NoeFBId;
        long year = request.Year;
        string itemFBShomareh = request.ItemFBShomareh?.Trim()
            ?? throw new ArgumentException("شماره آیتم فهرست بها الزامی است.", nameof(request));
        long shomareh = request.Shomareh;
        decimal? meghdarJoz = request.MeghdarJoz;
        int levelNumber = request.LevelNumber;
        long? conditionContextId = request.ConditionContextId;
        string? baseFBItem = request.BaseFBItem?.Trim();

        // همان منطق قبلی حفظ شده است: شماره جدید در کل برآورد محاسبه می‌شود،
        // نه فقط برای یک نوع فهرست بها یا یک FB.
        long lastShomareh = await _context.RizMetreUserses
            .AsNoTracking()
            .Where(x => x.FB.BarAvordId == barAvordUserId)
            .MaxAsync(x => (long?)x.Shomareh) ?? 0L;
        long newShomareh = lastShomareh + 1;

        // فقط ارتباطات مربوط به آیتم جاری از دیتابیس دریافت می‌شوند.
        // Trim روی ItemFB برای حفظ سازگاری با داده‌های قدیمی دارای فاصله نگه داشته شده است.
        List<clsItemsRelatedToItemHaml> relatedItems = await _context.ItemsRelatedToItemHamls
            .AsNoTracking()
            .Where(x => x.Year == year
                && x.NoeFB == noeFBId
                && x.ItemFB.Trim() == itemFBShomareh)
            .ToListAsync();

        if (relatedItems.Count == 0)
            return new List<Guid>();

        clsBaravordUser? baravordUser = await _context.BaravordUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == barAvordUserId);

        // برای صورت‌وضعیت، مقادیر حمل از برآورد پایه خوانده می‌شوند.
        Dictionary<string, clsBarAvordHaml> baseHamlByItem = new();
        if (baravordUser?.Type == NoeBarAvord.SooratVaziat
            && baravordUser.ParentId.HasValue)
        {
            List<clsBarAvordHaml> baseHamls = await _context.BarAvordHamls
                .AsNoTracking()
                .Where(x => x.BarAvordId == baravordUser.ParentId.Value
                    && x.NoeFBId == noeFBId
                    && x.FBShomareh == itemFBShomareh)
                .ToListAsync();

            baseHamlByItem = baseHamls
                .GroupBy(x => x.FBShomarehHaml)
                .ToDictionary(g => g.Key, g => g.First());
        }

        List<string> positiveHamlItems = relatedItems
            .Where(x => ((x.Zarib1 ?? 1m) * (x.Zarib2 ?? 1m) * (x.Zarib3 ?? 1m)) >= 0)
            .Select(x => x.ItemHamlFB.Trim())
            .Where(x => x.Length > 0)
            .Distinct()
            .ToList();

        List<string> negativeHamlItems = relatedItems
            .Where(x => ((x.Zarib1 ?? 1m) * (x.Zarib2 ?? 1m) * (x.Zarib3 ?? 1m)) < 0)
            .Select(x => x.ItemHamlFB)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToList();

        // رکوردهای حمل موجودِ آیتم جاری یک‌بار خوانده می‌شوند.
        List<clsBarAvordHaml> currentHamls = await _context.BarAvordHamls
            .Where(x => x.BarAvordId == barAvordUserId
                && x.NoeFBId == noeFBId
                && x.FBShomareh == itemFBShomareh)
            .ToListAsync();

        Dictionary<string, clsBarAvordHaml> currentHamlByItem = currentHamls
            .GroupBy(x => x.FBShomarehHaml)
            .ToDictionary(g => g.Key, g => g.First());

        // برای ضرایب منفی، همه حمل‌های آیتم پایه به‌صورت یک‌جا دریافت می‌شوند.
        List<clsBarAvordHaml> removedBaseHamls = new();
        if (negativeHamlItems.Count > 0 && !string.IsNullOrWhiteSpace(baseFBItem))
        {
            removedBaseHamls = await _context.BarAvordHamls
                .Where(x => x.BarAvordId == barAvordUserId
                    && x.NoeFBId == noeFBId
                    && x.FBShomareh == baseFBItem
                    && negativeHamlItems.Contains(x.FBShomarehHaml))
                .ToListAsync();
        }

        if (removedBaseHamls.Count > 0)
        {
            List<Guid> removedHamlIds = removedBaseHamls
                .Select(x => x.ID)
                .Distinct()
                .ToList();

            List<Guid> removedRizMetreIds = await _context.BarAvordHamlRizMetres
                .Where(x => removedHamlIds.Contains(x.BarAvordHamlId))
                .Select(x => x.RizMetreId)
                .Distinct()
                .ToListAsync();

            if (removedRizMetreIds.Count > 0)
            {
                if (!conditionContextId.HasValue)
                {
                    throw new InvalidOperationException(
                        "برای حذف ریزمتره آیتم پایه، ConditionContextId الزامی است.");
                }

                List<clsRizMetreUsers> rizMetresToRemove = await _context.RizMetreUserses
                    .Where(x => removedRizMetreIds.Contains(x.ID))
                    .ToListAsync();

                if (rizMetresToRemove.Count > 0)
                {
                    // برای هر ریزمتره یک Entity مستقل ساخته می‌شود.
                    _context.ItemHasConditionBaseItemRemoveds.AddRange(
                        rizMetresToRemove.Select(x => new clsItemHasConditionBaseItemRemoved
                        {
                            BarAvordId = barAvordUserId,
                            ConditionContextId = conditionContextId.Value,
                            RizMetreId = x.ID
                        }));

                    foreach (clsRizMetreUsers rizMetre in rizMetresToRemove)
                        rizMetre.IsDeleted = true;
                }
            }
        }

        // FBهای حمل نیز به‌صورت دسته‌ای دریافت می‌شوند تا در حلقه برای هر حمل
        // یک رفت‌وبرگشت جداگانه به دیتابیس نداشته باشیم.
        Dictionary<string, clsFB> fbByHamlItem = new();
        if (positiveHamlItems.Count > 0)
        {
            List<clsFB> existingFbs = await _context.FBs
                .Where(x => x.BarAvordId == barAvordUserId
                    && x.NoeFBId == noeFBId
                    && positiveHamlItems.Contains(x.Shomareh))
                .ToListAsync();

            fbByHamlItem = existingFbs
                .GroupBy(x => x.Shomareh)
                .ToDictionary(g => g.Key, g => g.First());
        }

        List<Guid> rizMetreIds = new();

        foreach (clsItemsRelatedToItemHaml relatedItem in relatedItems)
        {
            string hamlItemFB = relatedItem.ItemHamlFB.Trim();
            decimal? zarib1 = relatedItem.Zarib1;
            decimal? zarib2 = relatedItem.Zarib2;
            decimal? zarib3 = relatedItem.Zarib3;

            decimal allZarib = (zarib1 ?? 1m) * (zarib2 ?? 1m) * (zarib3 ?? 1m);

            // ضرایب منفی در بخش بالا پردازش شده‌اند و ریزمتره حمل جدید ندارند.
            if (allZarib < 0)
                continue;

            if (!currentHamlByItem.TryGetValue(hamlItemFB, out clsBarAvordHaml? currentHaml))
            {
                decimal? baseValue = null;
                decimal? baseValueBase = null;

                if (baravordUser?.Type == NoeBarAvord.SooratVaziat
                    && baseHamlByItem.TryGetValue(hamlItemFB, out clsBarAvordHaml? baseHaml))
                {
                    baseValue = baseHaml.Value;
                    baseValueBase = baseHaml.ValueBase;
                }

                currentHaml = new clsBarAvordHaml
                {
                    ID = Guid.NewGuid(),
                    BarAvordId = barAvordUserId,
                    FBShomareh = itemFBShomareh,
                    FBShomarehHaml = hamlItemFB,
                    NoeFBId = noeFBId,
                    Value = baseValue,
                    ValueBase = baseValueBase
                };

                _context.BarAvordHamls.Add(currentHaml);
                currentHamlByItem.Add(hamlItemFB, currentHaml);
            }

            if (!fbByHamlItem.TryGetValue(hamlItemFB, out clsFB? fbHaml))
            {
                fbHaml = new clsFB
                {
                    BarAvordId = barAvordUserId,
                    InsertDateTime = now,
                    Shomareh = hamlItemFB,
                    NoeFBId = noeFBId
                };

                _context.FBs.Add(fbHaml);
                fbByHamlItem.Add(hamlItemFB, fbHaml);
            }

            // مقدار Tedad از حمل متعلق به همین آیتم جاری خوانده می‌شود؛
            // جست‌وجوی قبلی بدون FBShomareh می‌توانست مقدار آیتم دیگری را برگرداند.
            clsRizMetreUsers rizMetre = new()
            {
                Shomareh = shomareh,
                ShomarehNew = newShomareh.ToString(),
                Sharh = " حمل ",
                Tedad = currentHaml.Value,
                Tool = zarib1,
                Arz = zarib2,
                Ertefa = zarib3,
                Des = " آیتم - " + itemFBShomareh,
                FBId = fbHaml.ID,
                OperationsOfHamlId = 1,
                Type = "3",
                ForItem = hamlItemFB,
                UseItem = "",
                LevelNumber = levelNumber,
                Vazn = meghdarJoz,
                MeghdarJoz = allZarib * meghdarJoz
            };

            _context.RizMetreUserses.Add(rizMetre);

            _context.BarAvordHamlRizMetres.Add(new clsBarAvordHamlRizMetre
            {
                BarAvordHamlId = currentHaml.ID,
                RizMetreId = rizMetre.ID
            });

            rizMetreIds.Add(rizMetre.ID);
            newShomareh++;
        }

        // ذخیره‌سازی به فراخواننده واگذار می‌شود تا تمام تغییرات این عملیات
        // همراه با ریزمتره اصلی، در یک SaveChanges و یک واحد کاری ذخیره شوند.
        return rizMetreIds;
    }

    public static async Task<bool> DeleteHaml(DeleteHamlDto request, ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);

        List<clsBarAvordHaml> itemHamls = await context.BarAvordHamls
            .Where(x => x.BarAvordId == request.BarAvordId
                && x.NoeFBId == request.NoeFBId
                && x.FBShomareh == request.FBShomareh)
            .ToListAsync();

        if (itemHamls.Count == 0)
            return true;

        List<Guid> itemHamlIds = itemHamls.Select(x => x.ID).ToList();

        // فقط ریزمتره‌های حملی حذف می‌شوند که به همین آیتم و همین شماره ریزمتره وصل‌اند.
        List<clsBarAvordHamlRizMetre> links = await context.BarAvordHamlRizMetres
            .Where(x => itemHamlIds.Contains(x.BarAvordHamlId))
            .ToListAsync();

        List<Guid> hamlRizMetreIds = links.Select(x => x.RizMetreId).Distinct().ToList();

        List<clsRizMetreUsers> hamlRizMetres = hamlRizMetreIds.Count == 0
            ? new List<clsRizMetreUsers>()
            : await context.RizMetreUserses
                .Where(x => hamlRizMetreIds.Contains(x.ID)
                    && x.Shomareh == request.RMShomareh)
                .ToListAsync();

        if (hamlRizMetres.Count > 0)
        {
            List<Guid> deletedHamlRizMetreIds = hamlRizMetres.Select(x => x.ID).ToList();

            // ابتدا ارتباط‌ها حذف می‌شوند تا رکورد یتیم در جدول واسط باقی نماند.
            List<clsBarAvordHamlRizMetre> linksToDelete = links
                .Where(x => deletedHamlRizMetreIds.Contains(x.RizMetreId))
                .ToList();

            context.BarAvordHamlRizMetres.RemoveRange(linksToDelete);
            context.RizMetreUserses.RemoveRange(hamlRizMetres);
        }

        // در صورت حذف آیتم شرطی، ریزمتره‌های آیتم پایه که قبلاً غیرفعال شده بودند برگردانده می‌شوند.
        if (request.ConditionContextId.HasValue)
        {
            List<clsItemHasConditionBaseItemRemoved> removedBaseItems =
                await context.ItemHasConditionBaseItemRemoveds
                    .Where(x => x.BarAvordId == request.BarAvordId
                        && x.ConditionContextId == request.ConditionContextId.Value)
                    .ToListAsync();

            if (removedBaseItems.Count > 0)
            {
                List<Guid> baseRizMetreIds = removedBaseItems
                    .Select(x => x.RizMetreId)
                    .Distinct()
                    .ToList();

                List<clsRizMetreUsers> baseRizMetres = await context.RizMetreUserses
                    .IgnoreQueryFilters()
                    .Where(x => baseRizMetreIds.Contains(x.ID) && x.IsDeleted == true)
                    .ToListAsync();

                foreach (clsRizMetreUsers baseRizMetre in baseRizMetres)
                    baseRizMetre.IsDeleted = false;

                context.ItemHasConditionBaseItemRemoveds.RemoveRange(removedBaseItems);
            }
        }

        return true;
    }

    public static async Task<bool> UpdateHaml(UpdateHamlDto request, ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(context);

        string itemFBShomareh = request.FBShomareh.Trim();

        List<clsItemsRelatedToItemHaml> relatedItems = await context.ItemsRelatedToItemHamls
            .AsNoTracking()
            .Where(x => x.Year == request.Year
                && x.NoeFB == request.NoeFB
                && x.ItemFB.Trim() == itemFBShomareh)
            .ToListAsync();

        if (relatedItems.Count == 0)
            return true;

        List<clsBarAvordHaml> itemHamls = await context.BarAvordHamls
            .Where(x => x.BarAvordId == request.BarAvordId
                && x.NoeFBId == request.NoeFB
                && x.FBShomareh == itemFBShomareh)
            .ToListAsync();

        if (itemHamls.Count == 0)
            return true;

        List<Guid> itemHamlIds = itemHamls.Select(x => x.ID).ToList();

        List<clsBarAvordHamlRizMetre> links = await context.BarAvordHamlRizMetres
            .Where(x => itemHamlIds.Contains(x.BarAvordHamlId))
            .ToListAsync();

        if (links.Count == 0)
            return true;

        List<Guid> hamlRizMetreIds = links.Select(x => x.RizMetreId).Distinct().ToList();

        List<clsRizMetreUsers> hamlRizMetres = await context.RizMetreUserses
            .Where(x => hamlRizMetreIds.Contains(x.ID)
                && x.Shomareh == request.RMShomareh)
            .ToListAsync();

        // ضرایب حمل از جدول ارتباطات خوانده می‌شوند و دست‌نخورده می‌مانند.
        // فقط مقدار پایه‌ای که از ریزمتره آیتم اصلی می‌آید به‌روزرسانی می‌شود.
        Dictionary<string, decimal> coefficientsByHamlItem = relatedItems
            .Where(x => (x.Zarib1 ?? 1m) * (x.Zarib2 ?? 1m) * (x.Zarib3 ?? 1m) >= 0)
            .GroupBy(x => x.ItemHamlFB.Trim())
            .ToDictionary(
                group => group.Key,
                group => group.First().Zarib1.GetValueOrDefault(1m)
                    * group.First().Zarib2.GetValueOrDefault(1m)
                    * group.First().Zarib3.GetValueOrDefault(1m));

        foreach (clsRizMetreUsers hamlRizMetre in hamlRizMetres)
        {
            if (!coefficientsByHamlItem.TryGetValue(hamlRizMetre.ForItem?.Trim() ?? string.Empty, out decimal coefficient))
                continue;

            hamlRizMetre.Vazn = request.MeghdarJoz;
            hamlRizMetre.MeghdarJoz = coefficient * request.MeghdarJoz;
        }

        return true;
    }
}
