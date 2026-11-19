using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using System.Web.Mvc;
using static RMS.Models.Common.EnumForEntity;
using static Stimulsoft.Report.StiRecentConnections;

namespace RMS.Controllers.BarAvordUser.Common;

public class ShowBarAvordCommon
{

    public async Task<ResultGetFosoulForBarAvordDto> fnGetFosoulForBarAvord(GetFosoulForBAAndSVInputDto request, ApplicationDbContext _context)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;


        clsBaravordUser? BaravordUser = await _context.BaravordUsers.AsNoTracking().FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (BaravordUser == null)
        {
            return new ResultGetFosoulForBarAvordDto();
        }
        else
        {
            List<GetFosoulOutputDto> Fosoul = await AllFosoulBarAvord(request, BaravordUser, _context);

            List<clsVahed> lstVaheds = await _context.Vaheds.AsNoTracking().ToListAsync();

            var result = new ResultGetFosoulForBarAvordDto
            {
                lstVaheds = lstVaheds,
                Fosoul = Fosoul,
            };

            return result;
        }

    }

    public async Task<ResultGetFosoulForSooratVaziatDto> fnGetFosoulForSooratVaziat(GetFosoulForBAAndSVInputDto request, clsBaravordUser BaravordUser, ApplicationDbContext _context)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;


        List<GetFosoulOutputDto> Fosoul = await AllFosoulBarAvord(request, BaravordUser, _context);

        decimal dJameFaslStar = 0;
        decimal dJameFaslStarWithZarib = 0;
        decimal dJameFasl = 0;
        decimal dJameFaslWithZarib = 0;
        decimal dJameFaslAll = 0;
        decimal dJameFaslWithZaribAll = 0;

        decimal dJameFaslTajhiz = 0;
        decimal dJameFaslTajhizWithZarib = 0;

        bool blnCheckUsePercent = false;
        //در صورتی که تجهیزکارگاه بصورت درصدی حساب شود
        clsTajhizKargahDarsad? TajhizKargahDarsad = await _context.TajhizKargahDarsads.AsNoTracking().FirstOrDefaultAsync(x => x.BarAvordUserId == BarAvordUserId && x.NoeFB == request.NoeFBId);
        if (TajhizKargahDarsad != null)
        {
            blnCheckUsePercent = true;
        }


        foreach (var fasl in Fosoul)
        {
            if (blnCheckUsePercent)
            {
                if (fasl.Code != "99")
                {
                    dJameFaslStar += fasl.JameFaslStar;
                    dJameFaslStarWithZarib += fasl.JameFaslStarWithZarib;
                    dJameFasl += fasl.JameFasl;
                    dJameFaslWithZarib += fasl.JameFaslWithZarib;
                }
            }
            else
            {
                if (fasl.Code != "99")
                {
                    dJameFaslStar += fasl.JameFaslStar;
                    dJameFaslStarWithZarib += fasl.JameFaslStarWithZarib;
                    dJameFasl += fasl.JameFasl;
                    dJameFaslWithZarib += fasl.JameFaslWithZarib;
                }
                else
                {
                    dJameFaslTajhiz += fasl.JameFaslAll;
                    dJameFaslTajhizWithZarib += fasl.JameFaslWithZaribAll;
                }
            }
        }

        decimal dJameFaslWithZaribAll1 = decimal.Parse(dJameFaslWithZarib.ToString("n0")) + decimal.Parse(dJameFaslStarWithZarib.ToString("n0"));

        decimal JameTajhiz = 0;

        if (TajhizKargahDarsad != null)
        {
            JameTajhiz = (dJameFaslWithZarib + dJameFaslStarWithZarib) * (TajhizKargahDarsad.Darsad / 100);
        }

        GetSumFosoulForSooratVaziatDto SumFosoulForSooratVaziat = new GetSumFosoulForSooratVaziatDto
        {
            dJameFaslStar = dJameFaslStar,
            dJameFaslStarWithZarib = dJameFaslStarWithZarib,
            dJameFasl = dJameFasl,
            dJameFaslWithZarib = dJameFaslWithZarib,
            dJameFaslAll = decimal.Parse(dJameFasl.ToString("n0")) + decimal.Parse(dJameFaslStar.ToString("n0")),
            dJameFaslWithZaribAll = dJameFaslWithZaribAll1,

            dJameFaslWithTajhizWithZarib = JameTajhiz + decimal.Parse(dJameFaslWithZarib.ToString("n0")) + decimal.Parse(dJameFaslStarWithZarib.ToString("n0")) + decimal.Parse(dJameFaslTajhizWithZarib.ToString("n0")),
            //dJameTajhiz= JameTajhiz
        };

        List<clsVahed> lstVaheds = await _context.Vaheds.AsNoTracking().ToListAsync();

        var result = new ResultGetFosoulForSooratVaziatDto
        {
            lstVaheds = lstVaheds,
            Fosoul = Fosoul,
            SumFosoulForSooratVaziat = SumFosoulForSooratVaziat
        };

        return result;


    }

    public async Task<List<GetFosoulOutputDto>> AllFosoulBarAvord(GetFosoulForBAAndSVInputDto request, clsBaravordUser? BaravordUser, ApplicationDbContext _context)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        ////////////////////////////////
        //در صورتی که صورت وضعیت باشد، بایستی ضریب پیشنهادی پیمانکار را نیز در ضرایب منطقه و بالاسری برای هر فصل پیدا کرده و ضرب کنیم
        ////////////////////////////////
        NoeBarAvord NoeBAType = NoeBarAvord.BarAvord;

        List<clsZaribPishnahadiPaymanKarFosoul> lstZaribPishnahadiPaymanKarFosoul = new List<clsZaribPishnahadiPaymanKarFosoul>();

        if (BaravordUser != null)
        {

            NoeBAType = BaravordUser.Type;

            if (NoeBAType != NoeBarAvord.BarAvord)
            {
                clsBaravordUser? baravordUserMain = await _context.BaravordUsers.AsNoTracking().FirstOrDefaultAsync(x => x.ID == BaravordUser.ParentId);
                if (baravordUserMain != null)
                {
                    //یافتن ضرایب پیشنهادی پیمانکار برای هر فصل
                    lstZaribPishnahadiPaymanKarFosoul =
                          await _context.ZaribPishnahadiPaymanKarFosouls
                            .AsNoTracking()
                            .Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == baravordUserMain.ID)
                            .ToListAsync();
                }
            }
        }
        ////////////////////////
        ////////////////////////
        ////////////////////////

        List<GetFosoulOutputDto> Fosoul = await _context.Fosouls.AsNoTracking().Where(x => x.NoeFB == NoeFBId)
            .OrderBy(x => x.order)
            .Select(x => new GetFosoulOutputDto
            {
                Id = x.Id,
                Code = x.Code,
                FaslName = x.Name,
                Order = x.order,
                FaslAsli = x.FaslAsli == null ? true : x.FaslAsli
            }).ToListAsync();


        // این متد همه فصل‌ها را محاسبه می‌کند؛ جزئیات ریزمتره فقط برای نمایش لازم‌اند.
        // جمع هر FB در تمام محاسبات از FB.SumRizMetre استفاده می‌شود.
        var RizMetre = await _context.RizMetreUserses
            .AsNoTracking()
            .Where(x => x.FB.BarAvordId == BarAvordUserId && x.FB.NoeFBId == NoeFBId)
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
            })
            .OrderBy(x => x.Shomareh)
            .ToListAsync();

        // دسترسی به جزئیات هر FB به‌صورت O(1)، به‌جای فیلترکردن کل لیست برای هر آیتم.
        var rizMetreByFbId = RizMetre
            .GroupBy(x => x.FBId)
            .ToDictionary(g => g.Key, g => g.ToList());



        //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == request.BarAvordUserId);
        //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == request.BarAvordUserId);

        //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
        //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

        RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
        {
            BarAvordId = BarAvordUserId,
            NoeFBId = NoeFBId,
            Year = Year
        };
        ResultGetZarayebDto result1 = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);


        List<clsFehrestBahaItemsReplace> lstFBItemReplaces = await _context.FehrestBahaItemsReplaces
            .AsNoTracking()
            .Where(x => x.Year == Year && x.NoeFB == NoeFBId)
            .ToListAsync();

        //var fbItems = await _context.FBs
        //    .Where(f => f.BarAvordId == BarAvordUserId && f.NoeFBId == NoeFBId)
        //    .GroupBy(f => f.Shomareh)
        //    .Select(g => g.FirstOrDefault())
        //    .ToListAsync(); // انتقال به حافظه، جلوگیری از خطای EF



        // فهرست بها یک بار برای کل بارگذاری فصول دریافت می‌شود.
        var lstFehrestBahas =await (
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

        var fehrestByMainNumber = lstFehrestBahas
            .GroupBy(x => x.ShomarehMain.Trim(), StringComparer.Ordinal)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.Ordinal);

        // دریافت FB بدون Join رشته‌ای؛ شماره کامل و پسوندهایی مانند e حفظ می‌شوند.
        var fbItems = await _context.FBs
            .AsNoTracking()
            .Where(fb => fb.BarAvordId == request.BarAvordUserId && fb.NoeFBId == NoeFBId)
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

        fbItems = fbItems
            .GroupBy(x => x.Shomareh)
            .Select(g => g.First())
            .ToList();

        foreach (var fb in fbItems)
        {
            string number = fb.Shomareh;
            string mainNumber = number.Length >= 6 ? number.Substring(0, 6) : number;
            if (fehrestByMainNumber.TryGetValue(mainNumber, out var fehrest))
            {
                fb.BahayeVahed = fehrest.BahayeVahed?.Trim() ?? "0";
                fb.Vahed = fehrest.Vahed;
            }
        }


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


        decimal zaribBalaSari = result1.zaribBalaSari != null ? result1.zaribBalaSari.Value : 1;
        decimal zaribManteghe = result1.zaribManteghe != null ? result1.zaribManteghe.Value : 1;

        decimal AllZarib = zaribManteghe * zaribBalaSari;



        // همان داده جایگزینی که بالاتر دریافت شده، دوباره از دیتابیس خوانده نمی‌شود.
        List<clsFehrestBahaItemsReplace> lstFehrestBahaItemsReplaces = lstFBItemReplaces;
        var relevantFaslIds = lstFBItemReplaces
            .Where(x => x.FaslId != null)
            .Select(x => x.FaslId)
            .Distinct()
            .ToList();

        List<clsZarayebFosoul> lstZarayebFosouls = relevantFaslIds.Count == 0
            ? new List<clsZarayebFosoul>()
            : await _context.ZarayebFosouls
                .AsNoTracking()
                .Where(x => relevantFaslIds.Contains(x.FaslId))
                .ToListAsync();

        // جدول آیتم‌های مرتبط در محدوده سال و نوع فهرست بها فقط یک‌بار خوانده می‌شود.
        // این روش از Query تکراری هر فصل و نیز ساخت IN بزرگ با احتمال عبور از محدودیت پارامترهای SQL Server جلوگیری می‌کند.
        var allRelatedItems = await _context.BahayeVahedItemsRelateds
            .AsNoTracking()
            .Where(x => x.NoeFB == NoeFBId && x.Year == Year)
            .ToListAsync();

        var relatedByItemNumberGlobal = allRelatedItems
            .GroupBy(x => x.ItemShomarehRelated.Trim(), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

        var zaribPishnahadiByFasl = lstZaribPishnahadiPaymanKarFosoul
            .GroupBy(x => x.Code.Trim(), StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First().ZaribJoz, StringComparer.OrdinalIgnoreCase);

        var exceptionItemsByFaslId = lstFehrestBahaItemsReplaces
            .Where(x => x.FaslId != null)
            .GroupBy(x => x.FaslId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var zarayebByFaslId = lstZarayebFosouls
            .GroupBy(x => x.FaslId)
            .ToDictionary(g => g.Key, g => g.First());
        // ستاره‌ها و جمع ریزمتره‌های آن‌ها یک‌بار برای کل برآورد دریافت می‌شوند.
        var allItemFBStars = await _context.ItemFBStars
            .AsNoTracking()
            .Where(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId)
            .ToListAsync();

        var rizMetreStarSums = await _context.RizMetreStars
            .AsNoTracking()
            .Where(x => x.ItemFBStar.BaravordId == BarAvordUserId &&
                        x.ItemFBStar.NoeFBId == NoeFBId)
            .GroupBy(x => x.ItemFBStar.Shomareh.Trim())
            .Select(g => new
            {
                Shomareh = g.Key,
                SumMeghdar = g.Sum(x => x.MeghdarJoz ?? 0m)
            })
            .ToDictionaryAsync(x => x.Shomareh, x => x.SumMeghdar);

        // داده‌های فهرست بها و اقلام ستاره‌دار را یک بار بر اساس کد فصل دسته‌بندی می‌کنیم.
        static string GetFaslPrefix(string? number) =>
            string.IsNullOrEmpty(number) ? string.Empty :
            number.Length >= 2 ? number.Substring(0, 2) : number;

        var fehrestByFaslCode = lstFehrestBahas
            .GroupBy(x => GetFaslPrefix(x.Shomareh), StringComparer.Ordinal)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);

        // کلید اتصال دقیقاً معادل شش رقم اول شماره FB پس از اعمال جایگزینی است.
        var fbByMainNumber = fbItems
            .GroupBy(x => x.Shomareh.Length >= 6 ? x.Shomareh.Substring(0, 6) : x.Shomareh,
                StringComparer.Ordinal)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.Ordinal);

        var starsByFaslCode = allItemFBStars
            .GroupBy(x => GetFaslPrefix(x.Shomareh), StringComparer.Ordinal)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);

        foreach (var fasl in Fosoul)
        {
            // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
            // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
            List<ViewUserBarAvordOutPutDto> userBarAvordOutPut = new List<ViewUserBarAvordOutPutDto>();

            if (fehrestByFaslCode.TryGetValue(fasl.Code, out var chapterFehrestItems))
            {
                userBarAvordOutPut = chapterFehrestItems
                    .Select(fehrest =>
                    {
                        fbByMainNumber.TryGetValue(fehrest.Shomareh, out var fbItem);
                        return new ViewUserBarAvordOutPutDto
                        {
                            ItemFbShomareh = fehrest.Shomareh,
                            ItemFbShomarehMain = fehrest.ShomarehMain,
                            Sharh = fehrest.Sharh,
                            BahayeVahed = fehrest.BahayeVahed,
                            Vahed = fehrest.Vahed,
                            FBId = fbItem != null ? fbItem.ID : null,
                            BahayeVahedNew = fbItem != null ? fbItem.BahayeVahedNew : 0,
                            ItemZarib = fbItem != null ? fbItem.ItemZarib : null,
                            Meghdar = fbItem?.SumRizMetre ?? 0m,
                            RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                        };
                    })
                    .OrderBy(x => x.ItemFbShomareh)
                    .ToList();
            }

            //ضرایب مربوط به فصول را در سایر ضرایب ضرب میکنیم
            decimal? ZaribFasl = null;
            if (NoeBAType != NoeBarAvord.BarAvord &&
                zaribPishnahadiByFasl.TryGetValue(fasl.Code.Trim(), out var zaribFaslValue))
                ZaribFasl = zaribFaslValue;

            decimal dCurrentZarib = AllZarib * (ZaribFasl == null ? 1 : ZaribFasl.Value);

            //میزان اعشار را در چهار رقم رند میکنیم
            //عدد 5 و بالاتر رو به بالا رند میشود و کوچکتر از 5 رو به پایین رند میشود

            // dCurrentZarib = Math.Round(dCurrentZarib, 4);

            bool blnFaslAsli = fasl.FaslAsli == null ? true : fasl.FaslAsli.Value;

            List<clsFehrestBahaItemsReplace> lstExceptionItems = new List<clsFehrestBahaItemsReplace>();
            clsZarayebFosoul ZarayebFasl = new clsZarayebFosoul();
            if (!blnFaslAsli)
            {
                if (exceptionItemsByFaslId.TryGetValue(fasl.Id, out var exceptionItems))
                    lstExceptionItems = exceptionItems;
                ZarayebFasl = zarayebByFaslId[fasl.Id];
            }

            decimal JameFasl = 0;
            decimal dJameFaslWithZarib = 0;

            decimal AllJameFaslWithoutBahaVahed = 0;//جمع آیتم های بدون بهای واحد
            decimal AllJameFaslWithoutBahaVahedWithZarib = 0;//جمع آیتم های بدون بهای واحد در ضریب

            List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFiltered = userBarAvordOutPut.Where(x => x.FBId != null).ToList();


            var fehrestShomarehs = userBarAvordOutPutFiltered
                .Select(x => x.ItemFbShomareh)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var barAvordOnlyInFbItems = fbItems
                .Where(fb => fb.Shomareh.StartsWith(fasl.Code, StringComparison.Ordinal) &&
                             !fehrestShomarehs.Contains(fb.Shomareh))
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

            userBarAvordOutPutFiltered = userBarAvordOutPutFiltered
            .Concat(barAvordOnlyInFbItems).DistinctBy(x => x.FBId)
            .OrderBy(x => x.ItemFbShomareh)
            .ToList();

            // دیکشنری سراسری از قبل ساخته شده و برای تمام فصل‌ها استفاده می‌شود.
            var relatedByItemNumber = relatedByItemNumberGlobal;

            var outputByItemNumber = userBarAvordOutPutFiltered
                .GroupBy(x => x.ItemFbShomareh.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);


            foreach (var item in userBarAvordOutPutFiltered)
            {
                decimal currentJameFasl = 0;
                decimal currentJameFaslInZarib = 0;

                decimal Meghdar = 0;

                //List<ViewUserBarAvordOutPutRizMetreDto> RizMetreFiltered = RizMetre.Where(x => x.FBId == item.FBId).ToList();
                //foreach (var RM in RizMetreFiltered)
                //{
                //    if (item.FBId == RM.FBId)
                //    {
                //        item.RizMetre.Add(RM);
                //        Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                //    }
                //}


                if (item.FBId is Guid fbId &&
                    rizMetreByFbId.TryGetValue(fbId, out var rizMetres))
                {
                    // جزئیات برای نمایش از ریزمتره‌ها خوانده می‌شود؛ جمع از فیلد تجمیعی FB است.
                    item.RizMetre.AddRange(rizMetres);
                }

                Meghdar = item.Meghdar;

                

                if (Meghdar != 0)
                {

                    decimal dBVahed = 0;// item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                    decimal dBahayeKol = 0;


                    if (relatedByItemNumber.TryGetValue(item.ItemFbShomareh.Trim(), out var BVItemsRelated))
                    {
                        outputByItemNumber.TryGetValue(BVItemsRelated.ItemShomareh.Trim(), out var barAvordInFehrest1);
                        if (barAvordInFehrest1 != null)
                        {
                            long BahayeVahed1 = barAvordInFehrest1.BahayeVahedNew != 0 ? barAvordInFehrest1.BahayeVahedNew : (barAvordInFehrest1.BahayeVahed == null || barAvordInFehrest1.BahayeVahed == "") ? 0 : long.Parse(barAvordInFehrest1.BahayeVahed);
                            item.BahayeVahed = Math.Round(BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                        }
                    }

                    item.Meghdar = Meghdar;
                    decimal ItemZarib = (item.ItemZarib == null || item.ItemZarib == 0) ? 1 : item.ItemZarib.Value;

                    ///در صورتی که آیتم بهای واحدش تغییر یابد
                    if (item.BahayeVahedNew != 0)
                    {
                        dBVahed = item.BahayeVahedNew;
                        dBahayeKol = Meghdar * dBVahed * ItemZarib;

                        currentJameFasl = dBahayeKol;
                        AllJameFaslWithoutBahaVahed += currentJameFasl;

                        if (fasl.Code != "99")
                        {
                            if (lstExceptionItems.Count != 0)
                            {
                                foreach (var ExceptionItem in lstExceptionItems)
                                {
                                    if (item.ItemFbShomarehMain == ExceptionItem.FBShomarehMain)
                                    {
                                        currentJameFaslInZarib = dBahayeKol * (ZarayebFasl.ZaribBalaSari != null ? ZarayebFasl.ZaribBalaSari.Value : 1) * (ZarayebFasl.ZaribManteghe != null ? ZarayebFasl.ZaribManteghe.Value : 1);
                                        AllJameFaslWithoutBahaVahedWithZarib += currentJameFaslInZarib;
                                    }
                                }
                            }
                            else
                            {
                                currentJameFaslInZarib = dCurrentZarib * dBahayeKol;
                                AllJameFaslWithoutBahaVahedWithZarib += currentJameFaslInZarib;
                            }
                        }
                        else
                        {
                            currentJameFaslInZarib = dBahayeKol;
                            AllJameFaslWithoutBahaVahedWithZarib += currentJameFaslInZarib;// AllJameFaslWithoutBahaVahed;
                        }

                        fasl.JameFaslStar += currentJameFasl;

                        fasl.JameFaslStarWithZarib += currentJameFaslInZarib;

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
                            currentJameFasl = dBahayeKol;


                            JameFasl += currentJameFasl;

                            if (fasl.Code != "99")
                            {
                                if (lstExceptionItems.Count != 0)
                                {
                                    foreach (var ExceptionItem in lstExceptionItems)
                                    {
                                        if (item.ItemFbShomarehMain == ExceptionItem.FBShomarehMain)
                                        {
                                            currentJameFaslInZarib = dBahayeKol * (ZarayebFasl.ZaribBalaSari != null ? ZarayebFasl.ZaribBalaSari.Value : 1) * (ZarayebFasl.ZaribManteghe != null ? ZarayebFasl.ZaribManteghe.Value : 1);
                                            dJameFaslWithZarib += currentJameFaslInZarib;
                                        }
                                    }
                                }
                                else
                                {
                                    currentJameFaslInZarib = dCurrentZarib * dBahayeKol;
                                    dJameFaslWithZarib += currentJameFaslInZarib;
                                }
                            }
                            else
                            {
                                currentJameFaslInZarib = dBahayeKol;
                                dJameFaslWithZarib += currentJameFaslInZarib;// JameFasl;
                            }
                        }

                        fasl.JameFasl += currentJameFasl;

                        fasl.JameFaslWithZarib += currentJameFaslInZarib;
                    }
                }
            }


            decimal AllJameFaslStar = 0;
            decimal AllJameFaslStarWithZarib = 0;


            List<clsItemFBStar> lstItemFBStars = starsByFaslCode.TryGetValue(fasl.Code, out var chapterStars)
                ? chapterStars
                : new List<clsItemFBStar>();
            foreach (var itemFBStarCurrent in lstItemFBStars)
            {
                decimal ItemZaribStar = itemFBStarCurrent.ItemZaribStar == null ? 1 : itemFBStarCurrent.ItemZaribStar.Value;

                decimal BahayeVahedStar = itemFBStarCurrent.BahayeVahed * ItemZaribStar;
                decimal JameItemFBStar = rizMetreStarSums.TryGetValue(itemFBStarCurrent.Shomareh.Trim(), out var starSum)
                    ? starSum
                    : 0m;

                AllJameFaslStar += JameItemFBStar * BahayeVahedStar;
                bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;
                decimal ZaribStar2 = zaribBalaSari;
                ///خرید تجهیزات
                if (blnKharidTajhizat)
                {
                    zaribManteghe = 1;
                    ZaribStar2 = 1.14m;
                }

                decimal? AllZaribStar = zaribManteghe * ZaribStar2;
                if (NoeBAType != NoeBarAvord.BarAvord)
                {
                    AllZaribStar *= ZaribFasl == null ? 1 : ZaribFasl;
                }

                AllJameFaslStarWithZarib += (JameItemFBStar * BahayeVahedStar * (AllZaribStar == null ? 1 : AllZaribStar.Value));
            }

            fasl.JameFaslStar = AllJameFaslStar + AllJameFaslWithoutBahaVahed;
            fasl.JameFaslStarWithZarib = AllJameFaslStarWithZarib + AllJameFaslWithoutBahaVahedWithZarib;
            fasl.JameFaslAll = AllJameFaslStar + JameFasl + AllJameFaslWithoutBahaVahed;
            fasl.JameFaslWithZaribAll = AllJameFaslStarWithZarib + dJameFaslWithZarib + AllJameFaslWithoutBahaVahedWithZarib;
        }
        return Fosoul;
    }



}
