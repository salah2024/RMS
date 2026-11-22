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

        // برای سازگاری با فراخواننده‌های فعلی، SaveChanges در همین متد باقی مانده است.
        // فراخواننده‌ها نیز در حال حاضر پس از این متد SaveChangesAsync اجرا می‌کنند.
        await _context.SaveChangesAsync();

        return rizMetreIds;
    }

    public static async Task<bool> DeleteHaml(DeleteHamlDto request, ApplicationDbContext _context)
    {
        try
        {
            Guid BarAvordId = request.BarAvordId;
            Guid RizMetreId = request.RIzMetreId;
            string FBShomareh = request.FBShomareh;
            long RMShomareh = request.RMShomareh;
            NoeFehrestBaha NoeFBId = request.NoeFBId;
            long? ConditionContextId = request.ConditionContextId;

            //List<Guid> lstBarAvordHamlRizMetreIds = _context.BarAvordHamlRizMetres.Where(x => x.RizMetreId == RizMetreId).Select(x => x.RizMetreId).ToList();

            //List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstBarAvordHamlRizMetreIds.Contains(x.ID)).ToList();

            List<clsBarAvordHaml> lstBarAvordHaml = _context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.FBShomareh == FBShomareh).ToList();
            if (lstBarAvordHaml.Count != 0)
            {
                List<clsBarAvordHamlRizMetre> lstBarAvordHamlRizMetre = _context.BarAvordHamlRizMetres.Where(x => lstBarAvordHaml.Select(x => x.ID).Contains(x.BarAvordHamlId)).ToList();

                List<Guid> lstBarAvordHamlRMIds = lstBarAvordHamlRizMetre.Select(x => x.RizMetreId).ToList();
                List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => x.Shomareh == RMShomareh && lstBarAvordHamlRMIds.Contains(x.ID)).ToList();
                if (lstRizMetre.Count != 0)
                {
                    _context.RizMetreUserses.RemoveRange(lstRizMetre);


                    ///
                    ///بررسی اینکه در صورتی که تیک اضافه بهایی برداشته شود
                    ///بایستی بررسی گردد که آیتم حملی جایگزین آیتم حمل آیتم اصلی شده اشت یا خیر
                    ///
                    if (ConditionContextId != null)
                    {
                        List<clsItemHasConditionBaseItemRemoved> lstItemHasConditionBaseItemRemoved =
                            await _context.ItemHasConditionBaseItemRemoveds.Where(x => x.BarAvordId == BarAvordId && ConditionContextId == ConditionContextId.Value).ToListAsync();

                        if (lstItemHasConditionBaseItemRemoved.Count != 0)
                        {
                            List<Guid> lstRizMetreIds = lstItemHasConditionBaseItemRemoved.Select(x => x.RizMetreId).ToList();

                            List<clsRizMetreUsers> lstRizMetreHaml = await _context.RizMetreUserses.IgnoreQueryFilters().Where(x => lstRizMetreIds.Contains(x.ID) && x.IsDeleted==true).ToListAsync();
                            if (lstRizMetreHaml.Count != 0)
                            {
                                foreach (var RM in lstRizMetreHaml)
                                {
                                    RM.IsDeleted = false;
                                }
                            }

                            _context.ItemHasConditionBaseItemRemoveds.RemoveRange(lstItemHasConditionBaseItemRemoved);
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool UpdateHaml(UpdateHamlDto request, ApplicationDbContext _context)
    {
        try
        {
            int Year = request.Year;
            string FBShomareh = request.FBShomareh;
            decimal? dMeghdarJoz = request.MeghdarJoz;
            Guid BarAvordId = request.BarAvordId;
            long RMShomareh = request.RMShomareh;
            NoeFehrestBaha NoeFB = request.NoeFB;

            List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = _context.ItemsRelatedToItemHamls
                                .Where(x => x.ItemFB.Trim() == FBShomareh && x.Year == Year && x.NoeFB == NoeFB).ToList();

            foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
            {
                string strItemHamlFB = "";
                if (itemsRelatedToItemHaml != null)
                {
                    strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                    decimal? Zarib1 = itemsRelatedToItemHaml.Zarib1;
                    decimal? Zarib2 = itemsRelatedToItemHaml.Zarib2;
                    decimal? Zarib3 = itemsRelatedToItemHaml.Zarib3;
                    decimal? dAllZarib = (Zarib1 != null ? Zarib1.Value : 1) * (Zarib2 != null ? Zarib2.Value : 1) * (Zarib3 != null ? Zarib3.Value : 1);

                    string ItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB;
                    clsFB? FbHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemHamlFB);
                    if (FbHaml != null)
                    {
                        List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == FbHaml.ID
                            && x.ForItem == FBShomareh.Substring(0, 6).Trim() && x.Type == "3" && x.Shomareh == RMShomareh).ToList();

                        foreach (var RMForDelForHaml in lstRMForDelForHaml)
                        {
                            _context.Entry(RMForDelForHaml).CurrentValues.SetValues(new
                            {
                                Vazn = dMeghdarJoz,
                                MeghdarJoz = dAllZarib * dMeghdarJoz
                            });
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
