using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.AmalyateKhaki.Common;

public class PayKaniCommon
{
    public bool SaveEzafeBahaPayKani(SaveEzafeBahaPayKaniDto request, ApplicationDbContext _context)
    {
        clsNoeKhakBardariEzafeBaha? NoeKhB_EB = _context.NoeKhakBardariEzafeBahas.FirstOrDefault(x => x.Id == request.NoeKhakBardariEzafeBahaId);
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
        clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
        if (rizMetreUser != null)
        {
            Shomareh = rizMetreUser.Shomareh + 1;
        }

        List<long> noeKhB = _context.NoeKhakBardari_NoeKhakBardariEzafeBahas
              .Where(x => x.NoeKhakBardariEzafeBahaId == request.NoeKhakBardariEzafeBahaId).Select(x => x.NoeKhakBardariId).ToList();


        List<PayKaniInfoForBarAvordDetailsInsertedDto> lstPayKaniForBD =
            _context.PayKaniInfoForBarAvordDetailsRizMetres.Include(x => x.PayKaniInfoForBarAvordDetails)
                .ThenInclude(x => x.lstPayKaniInfoForBarAvordDetailsMore)
                .Include(x => x.PayKaniInfoForBarAvordDetails).ThenInclude(x => x.NoeKhakBardari)
             .Where(x => x.PayKaniInfoForBarAvordDetails.PayKaniInfoForBarAvordId == request.PayKaniInfoForBarAvordId
                         && noeKhB.Contains(x.PayKaniInfoForBarAvordDetails.NoeKhakBardariId))
             .Select(x => new PayKaniInfoForBarAvordDetailsInsertedDto
             {
                 RizMetreId = x.RizMetreUserId,
                 //AmalyateKhakiInfoForBarAvordDetailId = x.AmalyateKhakiInfoForBarAvordDetailsId,
                 //AmalyateKhakiInfoForBarAvordId = x.AmalyateKhakiInfoForBarAvordDetails.AmalyateKhakiInfoForBarAvordId,
                 NoeKhakBardariId = x.PayKaniInfoForBarAvordDetails.NoeKhakBardariId,
                 NoeKhakBardariName = x.PayKaniInfoForBarAvordDetails.NoeKhakBardari.Title,
                 lstPayKaniInfoForBarAvordDetailsMore = x.PayKaniInfoForBarAvordDetails.lstPayKaniInfoForBarAvordDetailsMore.ToList()
             }).ToList();



        clsPayKaniInfoForBarAvordEzafeBaha payKaniInfoForBarAvordEzafeBaha = new clsPayKaniInfoForBarAvordEzafeBaha
        {
            PayKaniInfoForBarAvordId = request.PayKaniInfoForBarAvordId,
            NoeKhakBardariEzafeBahaId = NoeKhB_EB.Id,
            InsertDateTime = Now
        };
        _context.PayKaniInfoForBarAvordEzafeBahas.Add(payKaniInfoForBarAvordEzafeBaha);

        clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strCurrentShomareh);
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
                Shomareh = strCurrentShomareh
            };
            _context.FBs.Add(newFB);
            gFBId = newFB.ID;
        }
        foreach (var item in lstPayKaniForBD)
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
                            RizMetre.Sharh = item.NoeKhakBardariName+" - " + "حمل به دپو";
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
                                    PayKaniInfoForBarAvordEzafeBahaId = payKaniInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.PayKaniInfoForBarAvordEzafeBahas.Add(payKaniInfoForBarAvordEzafeBaha);
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

                            clsPayKaniInfoForBarAvordEzafeBahaRizMetre PayKaniInfoForBarAvordEzafeBahaRizMetre
                                = new clsPayKaniInfoForBarAvordEzafeBahaRizMetre
                                {
                                    PayKaniInfoForBarAvordEzafeBahaId = payKaniInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.PayKaniInfoForBarAvordEzafeBahas.Add(payKaniInfoForBarAvordEzafeBaha);
                        }


                        PayKaniInfoForBarAvordDetailsMore =
                                     item.lstPayKaniInfoForBarAvordDetailsMore.FirstOrDefault(x => x.Name.ToLower() == "reusehajm");
                        if (PayKaniInfoForBarAvordDetailsMore != null)
                        {
                            MeghdarJoz = PayKaniInfoForBarAvordDetailsMore.Value;
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

                            clsPayKaniInfoForBarAvordEzafeBahaRizMetre PayKaniInfoForBarAvordEzafeBahaRizMetre
                                = new clsPayKaniInfoForBarAvordEzafeBahaRizMetre
                                {
                                    PayKaniInfoForBarAvordEzafeBahaId = payKaniInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.PayKaniInfoForBarAvordEzafeBahas.Add(payKaniInfoForBarAvordEzafeBaha);
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
                default:
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
                                PayKaniInfoForBarAvordEzafeBahaId = payKaniInfoForBarAvordEzafeBaha.ID,
                                RizMetreUserId = RizMetre.ID
                            };
                        _context.PayKaniInfoForBarAvordEzafeBahas.Add(payKaniInfoForBarAvordEzafeBaha);
                        break;
                    }
            }

        }
        return true;
    }

}
