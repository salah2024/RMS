using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Common;

public class BarAvordUserCommon
{
    public async Task<bool> PrePareToConvert(PrePareToConvertDto request, ApplicationDbContext _context)
    {
        //try
        //{
            Guid BarAvordUserId = request.BarAvordUserId;
            int Year = request.Year;

            clsZaribPishnahadiPaymanKar zaribPishnahadiPaymanKar = new clsZaribPishnahadiPaymanKar
            {
                BarAvordUserId = BarAvordUserId,
                Year = Year,
                CompanyId = Guid.Parse("7D9BD20C-6FD3-458C-0B05-08DE745B0EBA"),
                InsertDateTime = DateTime.Now,
                UserInserter = new Guid(),
            };
            _context.ZaribPishnahadiPaymanKars.Add(zaribPishnahadiPaymanKar);


            string[] NoeFBIds = request.NoeFBs.Split(",");
            foreach (var NoeFB in NoeFBIds)
            {
                int intNoeFB = int.Parse(NoeFB);
                NoeFehrestBaha NoeFBId = (NoeFehrestBaha)intNoeFB;

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

                RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
                {
                    BarAvordId = BarAvordUserId,
                    NoeFBId = NoeFBId,
                    Year = Year
                };
                ResultGetZarayebDto result1 = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);


                List<clsFehrestBahaItemsReplace> lstFBItemReplaces = await _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToListAsync();

                var fbItems = await _context.FBs
                    .Where(f => f.BarAvordId == BarAvordUserId && f.NoeFBId == NoeFBId)
                    .GroupBy(f => f.Shomareh)
                    .Select(g => g.FirstOrDefault())
                    .ToListAsync(); // انتقال به حافظه، جلوگیری از خطای EF


                foreach (var item in fbItems)
                {
                    foreach (var fbItemReplace in lstFBItemReplaces)
                    {
                        if (item.Shomareh == fbItemReplace.FBShomarehMain)
                        {
                            item.Shomareh = fbItemReplace.FBShomarehNew;
                        }
                    }
                }


                decimal zaribBalaSari = result1.zaribBalaSari != null ? result1.zaribBalaSari.Value : 1;
                decimal zaribManteghe = result1.zaribManteghe != null ? result1.zaribManteghe.Value : 1;

                decimal AllZarib = zaribManteghe * zaribBalaSari;

                var lstFehrestBahas = await (from FehrestBaha in _context.FehrestBahas
                                             join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                                             on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
                                             from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
                                             where FehrestBaha.Sal == Year && FehrestBaha.NoeFB == NoeFBId
                                             select new
                                             {
                                                 Id = FehrestBaha.Id,
                                                 BahayeVahed = FehrestBaha.BahayeVahed,
                                                 Vahed = FehrestBaha.Vahed,
                                                 Sharh = FehrestBaha.Sharh,
                                                 ShomarehMain = FehrestBaha.Shomareh,
                                                 Shomareh = FehrestBahaItemsReplace != null && FehrestBahaItemsReplace.FBShomarehNew != null
                                                                           ? FehrestBahaItemsReplace.FBShomarehNew
                                                                           : FehrestBaha.Shomareh,
                                                 Sal = FehrestBaha.Sal,
                                                 NoeFB = FehrestBaha.NoeFB,
                                             }).ToListAsync();
                clsTajhizKargahDarsad? TajhizKargahDarsad = await _context.TajhizKargahDarsads.FirstOrDefaultAsync(x => x.BarAvordUserId == request.BarAvordUserId && x.NoeFB == NoeFBId);



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
                            RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                        }).ToList();

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
                    if (userBarAvordOutPutFiltered.Count != 0)
                    {
                        foreach (var item in userBarAvordOutPutFiltered)
                        {
                            decimal currentJameFasl = 0;
                            decimal currentJameFaslInZarib = 0;

                            decimal Meghdar = 0;
                            foreach (var RM in RizMetre)
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
                                item.Meghdar = Meghdar;

                                ///در صورتی که آیتم بهای واحدش تغییر یابد
                                if (item.BahayeVahedNew != 0)
                                {
                                    dBVahed = item.BahayeVahedNew;
                                    dBahayeKol = Meghdar * dBVahed;

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
                                            currentJameFaslInZarib = AllZarib * dBahayeKol;
                                            AllJameFaslWithoutBahaVahedWithZarib += currentJameFaslInZarib;
                                        }
                                    }
                                    else
                                    {
                                        AllJameFaslWithoutBahaVahedWithZarib += dBahayeKol; //currentJameFaslInZarib;// AllJameFaslWithoutBahaVahed;
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

                                        dBahayeKol = Meghdar * dBVahed;
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
                                                currentJameFaslInZarib = AllZarib * dBahayeKol;
                                                dJameFaslWithZarib += currentJameFaslInZarib;
                                            }
                                        }
                                        else
                                        {
                                            dJameFaslWithZarib += dBahayeKol; //currentJameFaslInZarib;// JameFasl;
                                        }
                                    }

                                    fasl.JameFasl += currentJameFasl;

                                    fasl.JameFaslWithZarib += currentJameFaslInZarib;
                                }
                            }
                            else
                            {
                                if (fasl.Code == "99")
                                {
                                    if (TajhizKargahDarsad != null)
                                    {
                                        AllJameFaslWithoutBahaVahedWithZarib += TajhizKargahDarsad.Meghdar;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (fasl.Code == "99")
                        {
                            if (TajhizKargahDarsad != null)
                            {
                                AllJameFaslWithoutBahaVahed += TajhizKargahDarsad.Meghdar;
                                AllJameFaslWithoutBahaVahedWithZarib += TajhizKargahDarsad.Meghdar;
                            }
                        }
                    }

                    decimal AllJameFaslStar = 0;
                    decimal AllJameFaslStarWithZarib = 0;

                    List<clsItemFBStar> lstItemFBStars = await _context.ItemFBStars.Where(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.Substring(0, 2) == fasl.Code).ToListAsync();
                    foreach (var itemFBStarCurrent in lstItemFBStars)
                    {
                        decimal JameItemFBStar = 0;
                        //clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.Trim() == itemFBStarCurrent.Shomareh);

                        //BahayeVahedStar = 0;
                        //if (itemFBStar != null)
                        //{
                        decimal BahayeVahedStar = itemFBStarCurrent.BahayeVahed;
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
                        AllJameFaslStarWithZarib += (JameItemFBStar * BahayeVahedStar * zaribManteghe * ZaribStar2);
                    }

                    decimal dJameFaslAll = AllJameFaslStar + JameFasl + AllJameFaslWithoutBahaVahed;
                    decimal dJameFaslWithZaribAll = AllJameFaslStarWithZarib + dJameFaslWithZarib + AllJameFaslWithoutBahaVahedWithZarib;

                    fasl.JameFaslStar = AllJameFaslStar + AllJameFaslWithoutBahaVahed;
                    fasl.JameFaslStarWithZarib = AllJameFaslStarWithZarib + AllJameFaslWithoutBahaVahedWithZarib;
                    fasl.JameFaslAll = dJameFaslAll;// AllJameFaslStar + JameFasl + AllJameFaslWithoutBahaVahed;
                    fasl.JameFaslWithZaribAll = dJameFaslWithZaribAll;// AllJameFaslStarWithZarib + dJameFaslWithZarib + AllJameFaslWithoutBahaVahedWithZarib;

                    if (dJameFaslAll != 0)
                    {
                        clsZaribPishnahadiPaymanKarFosoul zaribPishnahadiPaymanKarFosoul = new clsZaribPishnahadiPaymanKarFosoul
                        {
                            ID=Guid.NewGuid(),
                            BaravordFasl = dJameFaslAll,
                            BaravordFaslBaZarib = dJameFaslWithZaribAll,
                            Code = fasl.Code,
                            Title = fasl.FaslName,
                            ZaribPishnahadiPaymanKarId = zaribPishnahadiPaymanKar.ID,
                            InsertDateTime = DateTime.Now,
                            UserInserter = new Guid(),
                            NoeFBId=NoeFBId,
                        };
                        _context.ZaribPishnahadiPaymanKarFosouls.Add(zaribPishnahadiPaymanKarFosoul);

                }
            }
            }

            return true;
        //}
        //catch (Exception)
        //{
        //    return false;
        //}
    }
}
