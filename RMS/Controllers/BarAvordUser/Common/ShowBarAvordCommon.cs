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


        clsBaravordUser? BaravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (BaravordUser == null)
        {
            return new ResultGetFosoulForBarAvordDto();
        }
        else
        {
            List<GetFosoulOutputDto> Fosoul = await AllFosoulBarAvord(request, BaravordUser, _context);

            List<clsVahed> lstVaheds = await _context.Vaheds.ToListAsync();

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
        clsTajhizKargahDarsad? TajhizKargahDarsad = _context.TajhizKargahDarsads.FirstOrDefault(x => x.BarAvordUserId == BarAvordUserId && x.NoeFB == request.NoeFBId);
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

        List<clsVahed> lstVaheds = await _context.Vaheds.ToListAsync();

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
                clsBaravordUser? baravordUserMain = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BaravordUser.ParentId);
                if (baravordUserMain != null)
                {
                    //یافتن ضرایب پیشنهادی پیمانکار برای هر فصل
                    lstZaribPishnahadiPaymanKarFosoul =
                          _context.ZaribPishnahadiPaymanKarFosouls.Include(x => x.ZaribPishnahadiPaymanKar).Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == baravordUserMain.ID).ToList();
                }
            }
        }
        ////////////////////////
        ////////////////////////
        ////////////////////////

        List<GetFosoulOutputDto> Fosoul = await _context.Fosouls.Where(x => x.NoeFB == NoeFBId)
            .OrderBy(x => x.order)
            .Select(x => new GetFosoulOutputDto
            {
                Id = x.Id,
                Code = x.Code,
                FaslName = x.Name,
                Order = x.order,
                FaslAsli = x.FaslAsli == null ? true : x.FaslAsli
            }).ToListAsync();


        var RizMetre = await _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordUserId && x.FB.NoeFBId == NoeFBId)
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
            }).OrderBy(x => x.Shomareh).ToListAsync();



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


        List<clsFehrestBahaItemsReplace> lstFBItemReplaces = await _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToListAsync();

        //var fbItems = await _context.FBs
        //    .Where(f => f.BarAvordId == BarAvordUserId && f.NoeFBId == NoeFBId)
        //    .GroupBy(f => f.Shomareh)
        //    .Select(g => g.FirstOrDefault())
        //    .ToListAsync(); // انتقال به حافظه، جلوگیری از خطای EF



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

        BahayeVahedSharh = fb.BahayeVahedSharh,

        // شماره واقعی FB حفظ شود
        Shomareh = fb.Shomareh,

        BahayeVahed =
            fehrestbaha == null
                ? "0"
                : fehrestbaha.BahayeVahed != null
                    ?  fehrestbaha.BahayeVahed.Trim():"0",

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

        List<clsFehrestBahaItemsReplace> lstFehrestBahaItemsReplaces = await _context.FehrestBahaItemsReplaces.Where(x => x.NoeFB == NoeFBId && x.Year == Year).ToListAsync();
        List<clsZarayebFosoul> lstZarayebFosouls = await _context.ZarayebFosouls.ToListAsync();
        foreach (var fasl in Fosoul)
        {
            // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
            // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
            List<ViewUserBarAvordOutPutDto> userBarAvordOutPut = lstFehrestBahas//_context.FehrestBahas
                .Where(fehrest => fehrest.Sal == request.Year && fehrest.NoeFB == NoeFBId && fehrest.Shomareh.StartsWith(fasl.Code))
                .ToList() // انتقال به حافظه
                .GroupJoin(fbItems,
                    fehrest => fehrest.Shomareh,
                    fb => fb.Shomareh.Substring(0, 6),
                    (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
                .OrderBy(x => x.fehrest.Shomareh)
                .Select(x => new ViewUserBarAvordOutPutDto
                {
                    ItemFbShomareh = x.fehrest.Shomareh,
                    ItemFbShomarehMain = x.fehrest.ShomarehMain,
                    Sharh = x.fehrest.Sharh,
                    BahayeVahed = x.fehrest.BahayeVahed,
                    Vahed = x.fehrest.Vahed,
                    FBId = x.fbItem != null ? x.fbItem.ID : null,
                    BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
                    ItemZarib = x.fbItem != null ? x.fbItem.ItemZarib : null,
                    RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                }).ToList();

            //ضرایب مربوط به فصول را در سایر ضرایب ضرب میکنیم
            decimal? ZaribFasl = null;
            if (NoeBAType != NoeBarAvord.BarAvord)
                ZaribFasl = lstZaribPishnahadiPaymanKarFosoul.FirstOrDefault(x => x.Code.Trim() == fasl.Code.Trim())?.ZaribJoz;

            decimal dCurrentZarib = AllZarib * (ZaribFasl == null ? 1 : ZaribFasl.Value);

            //میزان اعشار را در چهار رقم رند میکنیم
            //عدد 5 و بالاتر رو به بالا رند میشود و کوچکتر از 5 رو به پایین رند میشود

            // dCurrentZarib = Math.Round(dCurrentZarib, 4);

            bool blnFaslAsli = fasl.FaslAsli == null ? true : fasl.FaslAsli.Value;

            List<clsFehrestBahaItemsReplace> lstExceptionItems = new List<clsFehrestBahaItemsReplace>();
            clsZarayebFosoul ZarayebFasl = new clsZarayebFosoul();
            if (!blnFaslAsli)
            {
                lstExceptionItems = lstFehrestBahaItemsReplaces.Where(x => x.FaslId == fasl.Id).ToList();
                ZarayebFasl = lstZarayebFosouls.First(x => x.FaslId == fasl.Id);
            }

            decimal JameFasl = 0;
            decimal dJameFaslWithZarib = 0;

            decimal AllJameFaslWithoutBahaVahed = 0;//جمع آیتم های بدون بهای واحد
            decimal AllJameFaslWithoutBahaVahedWithZarib = 0;//جمع آیتم های بدون بهای واحد در ضریب

            List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFiltered = userBarAvordOutPut.Where(x => x.FBId != null).ToList();


            var fehrestShomarehs = userBarAvordOutPutFiltered.Select(x => x.ItemFbShomareh).ToList();

            var barAvordOnlyInFbItems = fbItems
                .Where(fb => fb.Shomareh.StartsWith(fasl.Code) && !fehrestShomarehs.Contains(fb.Shomareh))
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

            userBarAvordOutPutFiltered = userBarAvordOutPutFiltered
            .Concat(barAvordOnlyInFbItems).DistinctBy(x => x.FBId)
            .OrderBy(x => x.ItemFbShomareh)
            .ToList();

            List<string> lstItemFbShomarehs = userBarAvordOutPutFiltered.Select(x => x.ItemFbShomareh).Distinct().ToList();


            List<clsBahayeVahedItemsRelated> lstBVItemRelated = _context.BahayeVahedItemsRelateds.Where(x => lstItemFbShomarehs.Contains(x.ItemShomarehRelated.ToLower().Trim()) && x.NoeFB == NoeFBId && x.Year == Year).ToList();


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


                var rizMetres = RizMetre
                    .Where(x => x.FBId == item.FBId)
                    .ToList();

                item.RizMetre.AddRange(rizMetres);

                Meghdar += rizMetres.Sum(x => x.MeghdarJoz ?? 0);

                

                if (Meghdar != 0)
                {

                    decimal dBVahed = 0;// item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                    decimal dBahayeKol = 0;


                    clsBahayeVahedItemsRelated? BVItemsRelated = lstBVItemRelated.FirstOrDefault(x => x.ItemShomarehRelated.Trim().ToLower() == item.ItemFbShomareh.Trim().ToLower());
                    if (BVItemsRelated != null)
                    {
                        var barAvordInFehrest1 = userBarAvordOutPutFiltered.FirstOrDefault(x => x.ItemFbShomareh.Trim() == BVItemsRelated.ItemShomareh.Trim());
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


            List<clsItemFBStar> lstItemFBStars = await _context.ItemFBStars.Where(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.Substring(0, 2) == fasl.Code).ToListAsync();
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
                JameItemFBStar = _context.RizMetreStars.Include(x => x.ItemFBStar)
                     .Where(x => x.ItemFBStar.BaravordId == BarAvordUserId && x.ItemFBStar.NoeFBId == NoeFBId && x.ItemFBStar.Shomareh.Trim() == itemFBStarCurrent.Shomareh)
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
