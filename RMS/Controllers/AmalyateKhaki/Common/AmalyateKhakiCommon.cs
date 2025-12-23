using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Common;

public class AmalyateKhakiCommon
{
    public bool SaveEzafeBahaAKh(SaveEzafeBahaAKhDto request, ApplicationDbContext _context)
    {
        long Year = request.Year;
        NoeFehrestBaha NoeFBId = request.NoeFBId;

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


        List<AmalyateKhakiInfoForBarAvordDetailsInsertedDto> lstAKhForBD =
            _context.AmalyateKhakiInfoForBarAvordDetailsRizMetres.Include(x => x.AmalyateKhakiInfoForBarAvordDetails)
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
             }).ToList();



        clsAmalyateKhakiInfoForBarAvordEzafeBaha amalyateKhakiInfoForBarAvordEzafeBaha = new clsAmalyateKhakiInfoForBarAvordEzafeBaha
        {
            AmalyateKhakiInfoForBarAvordId = request.AmalyateKhakiInfoForBarAvordId,
            NoeKhakBardariEzafeBahaId = NoeKhB_EB.Id,
            InsertDateTime = Now
        };
        _context.AmalyateKhakiInfoForBarAvordEzafeBahas.Add(amalyateKhakiInfoForBarAvordEzafeBaha);

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
                Shomareh = strCurrentShomareh,
                NoeFBId=NoeFBId
            };
            _context.FBs.Add(newFB);
            gFBId = newFB.ID;
        }

        List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year).ToList();

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

                            clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);
                        }
                        break;
                    }
                case "(z+y)*m":
                    {
                        //decimal? d1 = 0;
                        //decimal? d2 = 0;
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

                            clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);
                        }


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

                            clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                                {
                                    AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                                    RizMetreUserId = RizMetre.ID
                                };
                            _context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);
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

                        //clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        //RizMetre.Shomareh = Shomareh++;
                        //RizMetre.Sharh = item.NoeKhakBardariName;
                        //RizMetre.Tedad = null;
                        //RizMetre.Tool = null;
                        //RizMetre.Arz = null;
                        //RizMetre.Ertefa = null;
                        //RizMetre.Vazn = null;
                        //RizMetre.Des = "";
                        //RizMetre.FBId = gFBId;
                        //RizMetre.OperationsOfHamlId = 1;
                        //RizMetre.Type = "2";
                        //RizMetre.ForItem = "";
                        //RizMetre.UseItem = "";
                        //RizMetre.MeghdarJoz = null;
                        //_context.RizMetreUserses.Add(RizMetre);

                        //clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                        //    = new clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre
                        //    {
                        //        AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                        //        RizMetreUserId = RizMetre.ID
                        //    };
                        //_context.AmalyateKhakiInfoForBarAvordEzafeBahaRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaRizMetre);
                        break;
                    }
            }

            ///آیتم های حمل درج میگردد
            string FBShomareh = item.FBShomareh.Trim();
            bool blnHasHaml = false;
            clsItemsRelatedToItemHaml? itemsRelatedToItemHaml = lstRelatedToItemHaml.FirstOrDefault(x => x.ItemFB.Trim() == FBShomareh);
            string strItemHamlFB = "";
            if (itemsRelatedToItemHaml != null)
            {
                strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                blnHasHaml = true;
            }

            if (blnHasHaml)
            {
                clsFB? FBHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strItemHamlFB);
                Guid gFBIdHaml = new Guid();
                if (FBHaml != null)
                {
                    gFBIdHaml = FBHaml.ID;
                }
                else
                {
                    clsFB newFBHaml = new clsFB
                    {
                        BarAvordId = BarAvordUserId,
                        InsertDateTime = Now,
                        Shomareh = strItemHamlFB,
                        NoeFBId = NoeFBId
                    };
                    _context.FBs.Add(newFBHaml);
                    gFBIdHaml = newFBHaml.ID;
                }

                ///
                ///درج ریز متره
                ///

                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh++;
                RizMetre.Sharh = " - حمل " + item.NoeKhakBardariName;
                RizMetre.Tedad = null;
                RizMetre.Tool = null;
                RizMetre.Arz = null;
                RizMetre.Ertefa = null;
                RizMetre.Vazn = null;
                RizMetre.Des = "";
                RizMetre.FBId = gFBIdHaml;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "3";
                RizMetre.ForItem = "";
                RizMetre.UseItem = "";
                RizMetre.MeghdarJoz = MeghdarJoz;
                _context.RizMetreUserses.Add(RizMetre);

                clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                    = new clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre
                    {
                        AmalyateKhakiInfoForBarAvordEzafeBahaId = amalyateKhakiInfoForBarAvordEzafeBaha.ID,
                        RizMetreId = RizMetre.ID
                    };
                _context.AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetres.Add(AmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre);
            }
        }

        return true;
    }
}
