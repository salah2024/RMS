using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Common;

public class AmalyateKhakiCommon
{
    public async Task<bool> SaveEzafeBahaAKh(SaveEzafeBahaAKhDto request, ApplicationDbContext _context)
    {
        long Year = request.Year;
        NoeFehrestBaha NoeFBId = request.NoeFBId;

        clsNoeKhakBardariEzafeBaha? NoeKhB_EB = await _context.NoeKhakBardariEzafeBahas.FirstOrDefaultAsync(x => x.Id == request.NoeKhakBardariEzafeBahaId);
        string strCurrentShomareh = "";
        string? strCondition = "";
        if (NoeKhB_EB != null)
        {
            strCurrentShomareh = NoeKhB_EB.FBItemShomareh;
            strCondition = NoeKhB_EB.Condition;
        }
        else
        {
            return false;
        }
        DateTime Now = DateTime.Now;
        Guid BarAvordUserId = request.BarAvordUserId;

        long Shomareh = 1;
        clsRizMetreUsers? rizMetreUser = await _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefaultAsync(x => x.FB.BarAvordId == BarAvordUserId);
        if (rizMetreUser != null)
        {
            Shomareh = rizMetreUser.Shomareh + 1;
        }

        List<long> noeKhB = await _context.NoeKhakBardari_NoeKhakBardariEzafeBahas
              .Where(x => x.NoeKhakBardariEzafeBahaId == request.NoeKhakBardariEzafeBahaId).Select(x => x.NoeKhakBardariId).ToListAsync();


        List<AmalyateKhakiInfoForBarAvordDetailsInsertedDto> lstAKhForBD =
           await _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Include(x => x.AmalyateKhakiInfoForBarAvordDetails)
                .ThenInclude(x => x.lstAmalyateKhakiInfoForBarAvordDetailsMore)
                .Include(x => x.AmalyateKhakiInfoForBarAvordDetails).ThenInclude(x => x.NoeKhakBardari)
             .Where(x => x.AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId == request.AmalyateKhakiInfoForBarAvordId
                         && noeKhB.Contains(x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardariId))
             .Select(x => new AmalyateKhakiInfoForBarAvordDetailsInsertedDto
             {
                 RizMetreId = x.RizMetreUserId,
                 //AmalyateKhakiInfoForBarAvordDetailId = x.AmalyateKhakiInfoForBarAvordDetailsId,
                 //AmalyateKhakiInfoForBarAvordId = x.AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId,
                 NoeKhakBardariId = x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardariId,
                 NoeKhakBardariName = x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardari.Title,
                 FBShomareh = x.AmalyateKhakiInfoForBarAvordDetails.NoeKhakBardari.FBItemShomareh,
                 lstAmalyateKhakiInfoForBarAvordDetailsMore = x.AmalyateKhakiInfoForBarAvordDetails.lstAmalyateKhakiInfoForBarAvordDetailsMore.ToList()
             }).ToListAsync();



        clsAmalyateKhakiInfoForBarAvordEzafeBaha amalyateKhakiInfoForBarAvordEzafeBaha = new clsAmalyateKhakiInfoForBarAvordEzafeBaha
        {
            AmalyateKhakiInfoForBarAvordId = request.AmalyateKhakiInfoForBarAvordId,
            NoeKhakBardariEzafeBahaId = NoeKhB_EB.Id,
            InsertDateTime = Now
        };
        _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Add(amalyateKhakiInfoForBarAvordEzafeBaha);

        clsFB? FB = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strCurrentShomareh && x.NoeFBId == NoeFBId);
        Guid gFBId = new Guid();
        if (FB != null)
        {
            gFBId = FB.ID;
        }
        else
        {
            clsFB newFB = new clsFB
            {
                BarAvordId = BarAvordUserId,
                InsertDateTime = Now,
                Shomareh = strCurrentShomareh,
                NoeFBId = NoeFBId
            };
            _context.FBs.Add(newFB);
            gFBId = newFB.ID;
            FB = newFB;
        }

        decimal dSumAllRizMetre = 0;

        List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = await _context.ItemsRelatedToItemHamls.Where(x => x.NoeFB == NoeFBId && x.Year == Year).ToListAsync();

        foreach (var item in lstAKhForBD)
        {
            decimal? MeghdarJoz = 0;
            clsAmalyateKhakiInfoForBarAvordDetailsMore? AmalyateKhakiInfoForBarAvordDetailsMore = null;
            switch (strCondition)
            {
                case "z*m":
                    {
                        AmalyateKhakiInfoForBarAvordDetailsMore =
                                            item.lstAmalyateKhakiInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
                        if (AmalyateKhakiInfoForBarAvordDetailsMore != null)
                        {
                            MeghdarJoz = AmalyateKhakiInfoForBarAvordDetailsMore.Value;
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh++;
                            RizMetre.Sharh = item.NoeKhakBardariName + " - " + "حمل به دپو";
                            RizMetre.Tedad = null;
                            RizMetre.Tool = null;
                            RizMetre.Arz = null;
                            RizMetre.Ertefa = null;
                            RizMetre.Vazn = null;
                            RizMetre.Des = "";
                            RizMetre.FBId = gFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "2";
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            RizMetre.MeghdarJoz = MeghdarJoz;
                            _context.RizMetreUserses.Add(RizMetre);

                            dSumAllRizMetre += MeghdarJoz ?? 0;

                            clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);


                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///

                            string FBShomareh = item.FBShomareh.Trim();
                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                            {
                                BarAvordUserId = BarAvordUserId,
                                Year = Year,
                                ItemFBShomareh = FBShomareh,
                                NoeFBId = NoeFBId,
                                Shomareh = RizMetre.Shomareh,
                                MeghdarJoz = MeghdarJoz,
                                LevelNumber = 1,
                            };
                            await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            List<Guid> lstRMHaml = await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID
                                };

                            foreach (var RMHaml in lstRMHaml)
                            {
                                AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre.RizMetreId = RMHaml;
                                _context.AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre);
                            }

                        }
                        break;
                    }
                case "(z+y)*m":
                    {
                        //decimal? d1 = 0;
                        //decimal? d2 = 0;
                        //AmalyateKhakiInfoForBarAvordDetailsMore =
                        //             item.lstAmalyateKhakiInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
                        //if (AmalyateKhakiInfoForBarAvordDetailsMore != null)
                        //{
                        //    MeghdarJoz = AmalyateKhakiInfoForBarAvordDetailsMore.Value;
                        //    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        //    RizMetre.Shomareh = Shomareh++;
                        //    RizMetre.Sharh = item.NoeKhakBardariName + " - " + "حمل به دپو";
                        //    RizMetre.Tedad = null;
                        //    RizMetre.Tool = null;
                        //    RizMetre.Arz = null;
                        //    RizMetre.Ertefa = null;
                        //    RizMetre.Vazn = null;
                        //    RizMetre.Des = "";
                        //    RizMetre.FBId = gFBId;
                        //    RizMetre.OperationsOfHamlId = 1;
                        //    RizMetre.Type = "2";
                        //    RizMetre.ForItem = "";
                        //    RizMetre.UseItem = "";
                        //    RizMetre.MeghdarJoz = MeghdarJoz;
                        //    _context.RizMetreUserses.Add(RizMetre);

                        //    clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                        //        = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                        //        {
                        //            AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                        //            RizMetreUserId = RizMetre.ID
                        //        };
                        //    _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);
                        //}


                        AmalyateKhakiInfoForBarAvordDetailsMore =
                                     item.lstAmalyateKhakiInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "reusehajm");
                        if (AmalyateKhakiInfoForBarAvordDetailsMore != null)
                        {
                            MeghdarJoz = AmalyateKhakiInfoForBarAvordDetailsMore.Value;
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh++;
                            RizMetre.Sharh = item.NoeKhakBardariName + " - " + "حمل خاکریزی";
                            RizMetre.Tedad = null;
                            RizMetre.Tool = null;
                            RizMetre.Arz = null;
                            RizMetre.Ertefa = null;
                            RizMetre.Vazn = null;
                            RizMetre.Des = "";
                            RizMetre.FBId = gFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "2";
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";
                            RizMetre.MeghdarJoz = MeghdarJoz;
                            _context.RizMetreUserses.Add(RizMetre);

                            dSumAllRizMetre += MeghdarJoz ?? 0;

                            clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);

                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///

                            string FBShomareh = item.FBShomareh.Trim();
                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                            {
                                BarAvordUserId = BarAvordUserId,
                                Year = Year,
                                ItemFBShomareh = FBShomareh,
                                NoeFBId = NoeFBId,
                                Shomareh = RizMetre.Shomareh,
                                MeghdarJoz = MeghdarJoz,
                                LevelNumber = 1,
                            };
                            List<Guid> lstRMHaml = await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID
                                };

                            foreach (var RMHaml in lstRMHaml)
                            {
                                AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre.RizMetreId = RMHaml;
                                _context.AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre);
                            }

                            await _context.SaveChangesAsync();

                        }
                        //if (d1 != 0 || d2 != 0)
                        //{
                        //    MeghdarJoz = (d1 == 0 ? 1 : d1) * (d2 == 0 ? 1 : d2);
                        //    break;
                        //}

                        //if (d1 != 0 && d2 != 0)
                        //{
                        //    MeghdarJoz = d1 + d2;
                        //}
                        break;
                    }
                case "x*y":
                    {


                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh++;
                        RizMetre.Sharh = item.NoeKhakBardariName;
                        RizMetre.Tedad = null;
                        RizMetre.Tool = null;
                        RizMetre.Arz = null;
                        RizMetre.Ertefa = null;
                        RizMetre.Vazn = null;
                        RizMetre.Des = "";
                        RizMetre.FBId = gFBId;
                        RizMetre.OperationsOfHamlId = 1;
                        RizMetre.Type = "2";
                        RizMetre.ForItem = "";
                        RizMetre.UseItem = "";
                        RizMetre.MeghdarJoz = null;
                        _context.RizMetreUserses.Add(RizMetre);

                        clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                            = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                            {
                                AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                                RizMetreUserId = RizMetre.ID
                            };
                        _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);

                        //Start///////////////////////
                        ////////////حمل///////////////
                        //////////////////////////////
                        ///آیتم های حمل درج میگردد
                        ///

                        string FBShomareh = item.FBShomareh.Trim();
                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                        {
                            BarAvordUserId = BarAvordUserId,
                            Year = Year,
                            ItemFBShomareh = FBShomareh,
                            NoeFBId = NoeFBId,
                            Shomareh = RizMetre.Shomareh,
                            MeghdarJoz = null,
                            LevelNumber = 1,
                        };
                        await HamlCommon.SaveHaml(requestSaveHaml, _context);

                        List<Guid> lstRMHaml = await HamlCommon.SaveHaml(requestSaveHaml, _context);

                        clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                            = new clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                            {
                                AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID
                            };

                        foreach (var RMHaml in lstRMHaml)
                        {
                            AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre.RizMetreId = RMHaml;
                            _context.AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre);
                        }
                        break;
                    }
            }



            //await _context.SaveChangesAsync();

            ///آیتم های حمل درج میگردد
            //bool blnHasHaml = false;
            //clsItemsRelatedToItemHaml? itemsRelatedToItemHaml = lstRelatedToItemHaml.FirstOrDefault(x => x.ItemFB.Trim() == FBShomareh);
            //string strItemHamlFB = "";
            //if (itemsRelatedToItemHaml != null)
            //{
            //    strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
            //    blnHasHaml = true;
            //}

            //if (blnHasHaml)
            //{
            //    clsFB? FBHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strItemHamlFB && x.NoeFBId == NoeFBId);
            //    Guid gFBIdHaml = new Guid();
            //    if (FBHaml != null)
            //    {
            //        gFBIdHaml = FBHaml.ID;
            //    }
            //    else
            //    {
            //        clsFB newFBHaml = new clsFB
            //        {
            //            BarAvordId = BarAvordUserId,
            //            InsertDateTime = Now,
            //            Shomareh = strItemHamlFB,
            //            NoeFBId = NoeFBId
            //        };
            //        _context.FBs.Add(newFBHaml);
            //        gFBIdHaml = newFBHaml.ID;
            //    }

            //    ///
            //    ///درج ریز متره
            //    ///

            //    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            //    RizMetre.Shomareh = Shomareh++;
            //    RizMetre.Sharh = " - حمل " + item.NoeKhakBardariName;
            //    RizMetre.Tedad = null;
            //    RizMetre.Tool = null;
            //    RizMetre.Arz = null;
            //    RizMetre.Ertefa = null;
            //    RizMetre.Vazn = null;
            //    RizMetre.Des = "";
            //    RizMetre.FBId = gFBIdHaml;
            //    RizMetre.OperationsOfHamlId = 1;
            //    RizMetre.Type = "3";
            //    RizMetre.ForItem = "";
            //    RizMetre.UseItem = "";
            //    RizMetre.MeghdarJoz = MeghdarJoz;
            //    _context.RizMetreUserses.Add(RizMetre);

            //    clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
            //        = new clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
            //        {
            //            AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
            //            RizMetreId = RizMetre.ID
            //        };
            //    _context.AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre);
            //}
        }

        /////////////////////////////////////
        //////دریافت جمع ریزمتره های جاری///
        /////////////////////////////////////
        decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(gFBId, _context);

        decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
               ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
               : null;

        FB.SumRizMetre = dSumRizMetreCurrentFBAll;

        return true;
    }

    public async Task<bool> DeleteAmalyateKhakiInfoForBarAvord(DeleteKhakBardariInfoForBarAvordDto request, clsAmalyateKhakiInfoForBarAvord? currentAmalyateKhakiInfoForBarAvord, ApplicationDbContext _context)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        Guid KMKhakBardariId = request.KMKhakBardariId;
        long Year = request.Year;
        int Num = request.KMNum;


        try
        {


            /////
            /////دریافت اضافه بهاهای درج شده
            /////
            if (currentAmalyateKhakiInfoForBarAvord != null)
            {
                List<clsAmalyateKhakiInfoForBarAvordDetails> lstAmalyateKhakiInfoForBarAvordDetails = _context.AmalyateKhakiInfoForBarAvordDetailses.Where(x => x.AmalyateKhakiInfoForBarAvordId == KMKhakBardariId).ToList();

                List<Guid> lstAmalyateKhakiInfoForBarAvordDetailIds = lstAmalyateKhakiInfoForBarAvordDetails.Select(x => x.ID).ToList();

                List<clsAmalyateKhakiInfoForBarAvordDetailsRizMetre> lstAmalyateKhakiInfoForBarAvordDetailsRizMetres
                    = _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Where(x => lstAmalyateKhakiInfoForBarAvordDetailIds.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();

                if (lstAmalyateKhakiInfoForBarAvordDetails.Count != 0)
                {
                    _context.AmalyateKhakiInfoForBarAvordDetailses.RemoveRange(lstAmalyateKhakiInfoForBarAvordDetails);
                    _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.RemoveRange(lstAmalyateKhakiInfoForBarAvordDetailsRizMetres);

                    List<clsRizMetreUsers> lstRizMetreAmalyateKhakiInfoForBarAvordDetailsRizMetres =
                        _context.RizMetreUserses.Where(x => lstAmalyateKhakiInfoForBarAvordDetailsRizMetres.Select(x => x.RizMetreUserId).Contains(x.ID)).ToList();
                    _context.RizMetreUserses.RemoveRange(lstRizMetreAmalyateKhakiInfoForBarAvordDetailsRizMetres);

                }

                List<clsAmalyateKhakiInfoForBarAvordMore> lstAmalyateKhakiInfoForBarAvordMore = _context.AmalyateKhakiInfoForBarAvordMores.Where(x => x.AmalyateKhakiInfoForBarAvordId == KMKhakBardariId).ToList();
                if (lstAmalyateKhakiInfoForBarAvordMore.Count != 0)
                {
                    _context.AmalyateKhakiInfoForBarAvordMores.RemoveRange(lstAmalyateKhakiInfoForBarAvordMore);
                }

                List<clsAmalyateKhakiInfoForBarAvordDetailsMore> lstAmalyateKhakiInfoForBarAvordDetailsMores
                    = _context.AmalyateKhakiInfoForBarAvordDetailsMores.Where(x => lstAmalyateKhakiInfoForBarAvordDetailIds.Contains(x.AmalyateKhakiInfoForBarAvordDetailsId)).ToList();

                _context.AmalyateKhakiInfoForBarAvordDetailsMores.RemoveRange(lstAmalyateKhakiInfoForBarAvordDetailsMores);

                List<clsAmalyateKhakiInfoForBarAvordEzafeBaha> lstAKhForBEB =
                        _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Where(x => x.AmalyateKhakiInfoForBarAvordId == currentAmalyateKhakiInfoForBarAvord.ID).ToList();

                ///حذف اضافه بهاهای قبلی
                _context.AmalyateKhakiInfoForBarAvordEzafeBahas.RemoveRange(lstAKhForBEB);

                List<Guid> lstlstAKhForBEBIds = _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Select(x => x.ID).ToList();

                List<clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre> lstAmalyateKhakiInfoForBarAvordEzafeBahaRizMetres =
                        _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Where(x => lstlstAKhForBEBIds.Contains(x.AmalyateKhakiInfoForBarAvordEzafeBahaId)).ToList();

                List<clsRizMetreUsers> lstRizMetreAmalyateKhakiInfoForBarAvordEzafeBaha = _context.RizMetreUserses.Where(x => lstAmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Select(x => x.RizMetreUserId).Contains(x.ID)).ToList();
                _context.RizMetreUserses.RemoveRange(lstRizMetreAmalyateKhakiInfoForBarAvordEzafeBaha);


                ///حذف حمل های درج شده
                List<clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre> lstAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres =
                    _context.AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.Where(x => lstAKhForBEB.Select(x => x.ID).Contains(x.AmalyateKhakiInfoForBarAvordEzafeBahaId)).ToList();
                _context.AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.RemoveRange(lstAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres);

                List<clsRizMetreUsers> lstRizMetreHaml = _context.RizMetreUserses.Where(x => lstAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.Select(x => x.RizMetreId).Contains(x.ID)).ToList();
                _context.RizMetreUserses.RemoveRange(lstRizMetreHaml);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
