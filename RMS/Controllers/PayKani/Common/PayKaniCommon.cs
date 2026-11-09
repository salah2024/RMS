using Microsoft.EntityFrameworkCore;
using RMS.Controllers.PayKani.Dto;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using RMS.Controllers.AmalyateKhaki.Dto;
namespace RMS.Controllers.PayKani.Common;

public class PayKaniCommon
{
    public async Task<bool> SaveEzafeBahaPayKani(SaveEzafeBahaPayKaniDto request, ApplicationDbContext _context)
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


        List<PayKaniInfoForBarAvordDetailsInsertedDto> lstAKhForBD =
           await _context.PayKaniInfoForBarAvordDetailsRizMetres.Include(x => x.PayKaniInfoForBarAvordDetails)
                .ThenInclude(x => x.lstPayKaniInfoForBarAvordDetailsMore)
                .Include(x => x.PayKaniInfoForBarAvordDetails).ThenInclude(x => x.NoeKhakBardari)
             .Where(x => x.PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId == request.PayKaniInfoForBarAvordId
                         && noeKhB.Contains(x.PayKaniInfoForBarAvordDetails.NoeKhakBardariId))
             .Select(x => new PayKaniInfoForBarAvordDetailsInsertedDto
             {
                 RizMetreId = x.RizMetreUserId,
                 //PayKaniInfoForBarAvordDetailId = x.PayKaniInfoForBarAvordDetailsId,
                 //PayKaniInfoForBarAvordId = x.PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId,
                 NoeKhakBardariId = x.PayKaniInfoForBarAvordDetails.NoeKhakBardariId,
                 NoeKhakBardariName = x.PayKaniInfoForBarAvordDetails.NoeKhakBardari.Title,
                 FBShomareh = x.PayKaniInfoForBarAvordDetails.NoeKhakBardari.FBItemShomareh,
                 lstPayKaniInfoForBarAvordDetailsMore = x.PayKaniInfoForBarAvordDetails.lstPayKaniInfoForBarAvordDetailsMore.ToList()
             }).ToListAsync();



        clsPayKaniInfoForBarAvordEzafeBaha PayKaniInfoForBarAvordEzafeBaha = new clsPayKaniInfoForBarAvordEzafeBaha
        {
            PayKaniInfoForBarAvordId = request.PayKaniInfoForBarAvordId,
            NoeKhakBardariEzafeBahaId = NoeKhB_EB.Id,
            InsertDateTime = Now
        };
        _context.PayKaniInfoForBarAvordEzafeBahas.Add(PayKaniInfoForBarAvordEzafeBaha);

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
            clsPayKaniInfoForBarAvordDetailsMore? PayKaniInfoForBarAvordDetailsMore = null;
            switch (strCondition)
            {
                case "z*m":
                    {
                        PayKaniInfoForBarAvordDetailsMore =
                                            item.lstPayKaniInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
                        if (PayKaniInfoForBarAvordDetailsMore != null)
                        {
                            MeghdarJoz = PayKaniInfoForBarAvordDetailsMore.Value;
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh++;
                            RizMetre.Sharh = item.NoeKhakBardariName + " - " + "پخش در محل دپو";
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

                            clsPayKaniInfoForBarAvordEzafeBahaRizMetre PayKaniInfoForBarAvordEzafeBahaRizMetre
                                = new clsPayKaniInfoForBarAvordEzafeBahaRizMetre
                                {
                                    PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaRizMetre);


                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///

                            //string FBShomareh = item.FBShomareh.Trim();
                            //SaveHamlDto requestSaveHaml = new SaveHamlDto
                            //{
                            //    BarAvordUserId = BarAvordUserId,
                            //    Year = Year,
                            //    ItemFBShomareh = FBShomareh,
                            //    NoeFBId = NoeFBId,
                            //    Shomareh = RizMetre.Shomareh,
                            //    MeghdarJoz = MeghdarJoz,
                            //    LevelNumber = 1,
                            //};
                            //await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            //List<Guid> lstRMHaml = await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            //clsPayKaniInfoForBarAvordEzafeBahaHamlRizMetre PayKaniInfoForBarAvordEzafeBahaHamlRizMetre
                            //    = new clsPayKaniInfoForBarAvordEzafeBahaHamlRizMetre
                            //    {
                            //        PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID
                            //    };

                            //foreach (var RMHaml in lstRMHaml)
                            //{
                            //    PayKaniInfoForBarAvordEzafeBahaHamlRizMetre.RizMetreId = RMHaml;
                            //    _context.PayKaniInfoForBarAvordEzafeBahaHamlRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaHamlRizMetre);
                            //}

                        }
                        break;
                    }
                case "(z+y)*m":
                    {
                        //decimal? d1 = 0;
                        //decimal? d2 = 0;
                        PayKaniInfoForBarAvordDetailsMore =
                                     item.lstPayKaniInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "haml");
                        if (PayKaniInfoForBarAvordDetailsMore != null)
                        {
                            MeghdarJoz = PayKaniInfoForBarAvordDetailsMore.Value;
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh++;
                            RizMetre.Sharh = item.NoeKhakBardariName + " - " + "بارگیری جهت حمل به دپو";
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

                            clsPayKaniInfoForBarAvordEzafeBahaRizMetre PayKaniInfoForBarAvordEzafeBahaRizMetre
                                = new clsPayKaniInfoForBarAvordEzafeBahaRizMetre
                                {
                                    PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaRizMetre);
                        }


                        PayKaniInfoForBarAvordDetailsMore =
                                     item.lstPayKaniInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "reusehajm");
                        if (PayKaniInfoForBarAvordDetailsMore != null)
                        {
                            MeghdarJoz = PayKaniInfoForBarAvordDetailsMore.Value;
                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh++;
                            RizMetre.Sharh = item.NoeKhakBardariName + " - " + "بارگیری جهت حمل به خاکریزی";
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

                            clsPayKaniInfoForBarAvordEzafeBahaRizMetre PayKaniInfoForBarAvordEzafeBahaRizMetre
                                = new clsPayKaniInfoForBarAvordEzafeBahaRizMetre
                                {
                                    PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaRizMetre);

                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///

                            //string FBShomareh = item.FBShomareh.Trim();
                            //SaveHamlDto requestSaveHaml = new SaveHamlDto
                            //{
                            //    BarAvordUserId = BarAvordUserId,
                            //    Year = Year,
                            //    ItemFBShomareh = FBShomareh,
                            //    NoeFBId = NoeFBId,
                            //    Shomareh = RizMetre.Shomareh,
                            //    MeghdarJoz = MeghdarJoz,
                            //    LevelNumber = 1,
                            //};
                            //List<Guid> lstRMHaml = await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            //clsPayKaniInfoForBarAvordEzafeBahaHamlRizMetre PayKaniInfoForBarAvordEzafeBahaHamlRizMetre
                            //    = new clsPayKaniInfoForBarAvordEzafeBahaHamlRizMetre
                            //    {
                            //        PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID
                            //    };

                            //foreach (var RMHaml in lstRMHaml)
                            //{
                            //    PayKaniInfoForBarAvordEzafeBahaHamlRizMetre.RizMetreId = RMHaml;
                            //    _context.PayKaniInfoForBarAvordEzafeBahaHamlRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaHamlRizMetre);
                            //}

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

                        clsPayKaniInfoForBarAvordEzafeBahaRizMetre PayKaniInfoForBarAvordEzafeBahaRizMetre
                            = new clsPayKaniInfoForBarAvordEzafeBahaRizMetre
                            {
                                PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID,
                                RizMetreUserId = RizMetre.ID
                            };
                        _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaRizMetre);

                        //Start///////////////////////
                        ////////////حمل///////////////
                        //////////////////////////////
                        ///آیتم های حمل درج میگردد
                        ///

                        //string FBShomareh = item.FBShomareh.Trim();
                        //SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //{
                        //    BarAvordUserId = BarAvordUserId,
                        //    Year = Year,
                        //    ItemFBShomareh = FBShomareh,
                        //    NoeFBId = NoeFBId,
                        //    Shomareh = RizMetre.Shomareh,
                        //    MeghdarJoz = null,
                        //    LevelNumber = 1,
                        //};
                        //await HamlCommon.SaveHaml(requestSaveHaml, _context);

                        //List<Guid> lstRMHaml = await HamlCommon.SaveHaml(requestSaveHaml, _context);

                        //clsPayKaniInfoForBarAvordEzafeBahaHamlRizMetre PayKaniInfoForBarAvordEzafeBahaHamlRizMetre
                        //    = new clsPayKaniInfoForBarAvordEzafeBahaHamlRizMetre
                        //    {
                        //        PayKaniInfoForBarAvordEzafeBahaId = PayKaniInfoForBarAvordEzafeBaha.ID
                        //    };

                        //foreach (var RMHaml in lstRMHaml)
                        //{
                        //    PayKaniInfoForBarAvordEzafeBahaHamlRizMetre.RizMetreId = RMHaml;
                        //    _context.PayKaniInfoForBarAvordEzafeBahaHamlRizMetres.Add(PayKaniInfoForBarAvordEzafeBahaHamlRizMetre);
                        //}
                        break;
                    }
            }


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

    public async Task<bool> DeletePayKaniInfoForBarAvord(DeletePayKaniInfoForBarAvordDto request, clsPayKaniInfoForBarAvord? currentPayKaniInfoForBarAvord, ApplicationDbContext _context)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        Guid KMKhakBardariId = request.KMPayKaniId;
        long Year = request.Year;
        int Num = request.KMNum;


        try
        {


            /////
            /////دریافت اضافه بهاهای درج شده
            /////
            if (currentPayKaniInfoForBarAvord != null)
            {
                List<clsPayKaniInfoForBarAvordDetails> lstPayKaniInfoForBarAvordDetails = _context.PayKaniInfoForBarAvordDetailses.Where(x => x.PayKaniInfoForBarAvordId == KMKhakBardariId).ToList();

                List<Guid> lstPayKaniInfoForBarAvordDetailIds = lstPayKaniInfoForBarAvordDetails.Select(x => x.ID).ToList();

                List<clsPayKaniInfoForBarAvordDetailsRizMetre> lstPayKaniInfoForBarAvordDetailsRizMetres
                    = _context.PayKaniInfoForBarAvordDetailsRizMetres.Where(x => lstPayKaniInfoForBarAvordDetailIds.Contains(x.PayKaniInfoForBarAvordDetailsId)).ToList();

                if (lstPayKaniInfoForBarAvordDetails.Count != 0)
                {
                    _context.PayKaniInfoForBarAvordDetailses.RemoveRange(lstPayKaniInfoForBarAvordDetails);
                    _context.PayKaniInfoForBarAvordDetailsRizMetres.RemoveRange(lstPayKaniInfoForBarAvordDetailsRizMetres);

                    List<clsRizMetreUsers> lstRizMetrePayKaniInfoForBarAvordDetailsRizMetres =
                        _context.RizMetreUserses.Where(x => lstPayKaniInfoForBarAvordDetailsRizMetres.Select(x => x.RizMetreUserId).Contains(x.ID)).ToList();
                    _context.RizMetreUserses.RemoveRange(lstRizMetrePayKaniInfoForBarAvordDetailsRizMetres);

                }

                List<clsPayKaniInfoForBarAvordMore> lstPayKaniInfoForBarAvordMore = _context.PayKaniInfoForBarAvordMores.Where(x => x.PayKaniInfoForBarAvordId == KMKhakBardariId).ToList();
                if (lstPayKaniInfoForBarAvordMore.Count != 0)
                {
                    _context.PayKaniInfoForBarAvordMores.RemoveRange(lstPayKaniInfoForBarAvordMore);
                }

                List<clsPayKaniInfoForBarAvordDetailsMore> lstPayKaniInfoForBarAvordDetailsMores
                    = _context.PayKaniInfoForBarAvordDetailsMores.Where(x => lstPayKaniInfoForBarAvordDetailIds.Contains(x.PayKaniInfoForBarAvordDetailsId)).ToList();

                _context.PayKaniInfoForBarAvordDetailsMores.RemoveRange(lstPayKaniInfoForBarAvordDetailsMores);

                List<clsPayKaniInfoForBarAvordEzafeBaha> lstAKhForBEB =
                        _context.PayKaniInfoForBarAvordEzafeBahas.Where(x => x.PayKaniInfoForBarAvordId == currentPayKaniInfoForBarAvord.ID).ToList();

                ///حذف اضافه بهاهای قبلی
                _context.PayKaniInfoForBarAvordEzafeBahas.RemoveRange(lstAKhForBEB);

                List<Guid> lstlstAKhForBEBIds = _context.PayKaniInfoForBarAvordEzafeBahas.Select(x => x.ID).ToList();

                List<clsPayKaniInfoForBarAvordEzafeBahaRizMetre> lstPayKaniInfoForBarAvordEzafeBahaRizMetres =
                        _context.PayKaniInfoForBarAvordEzafeBahaRizMetres.Where(x => lstlstAKhForBEBIds.Contains(x.PayKaniInfoForBarAvordEzafeBahaId)).ToList();

                List<clsRizMetreUsers> lstRizMetrePayKaniInfoForBarAvordEzafeBaha = _context.RizMetreUserses.Where(x => lstPayKaniInfoForBarAvordEzafeBahaRizMetres.Select(x => x.RizMetreUserId).Contains(x.ID)).ToList();
                _context.RizMetreUserses.RemoveRange(lstRizMetrePayKaniInfoForBarAvordEzafeBaha);


                ///حذف حمل های درج شده
                //List<clsPayKaniInfoForBarAvordEzafeBahaHamlRizMetre> lstPayKaniInfoForBarAvordEzafeBahaHamlRizMetres =
                //    _context.PayKaniInfoForBarAvordEzafeBahaHamlRizMetres.Where(x => lstAKhForBEB.Select(x => x.ID).Contains(x.PayKaniInfoForBarAvordEzafeBahaId)).ToList();
                //_context.PayKaniInfoForBarAvordEzafeBahaHamlRizMetres.RemoveRange(lstPayKaniInfoForBarAvordEzafeBahaHamlRizMetres);

                //List<clsRizMetreUsers> lstRizMetreHaml = _context.RizMetreUserses.Where(x => lstPayKaniInfoForBarAvordEzafeBahaHamlRizMetres.Select(x => x.RizMetreId).Contains(x.ID)).ToList();
                //_context.RizMetreUserses.RemoveRange(lstRizMetreHaml);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
