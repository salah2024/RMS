using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AmalyateKhaki.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.Operation.Common;

public static class HamlCommon
{
    public static bool SaveHaml(SaveHamlDto request, ApplicationDbContext _context)
    {
        try
        {
            DateTime Now = DateTime.Now;
            Guid BarAvordUserId = request.BarAvordUserId;
            long Year = request.Year;
            string ItemFBShomareh = request.ItemFBShomareh.Trim();
            Guid BarAvordHamlId = request.BarAvordHamlId;
            decimal? MeghdarJoz = request.MeghdarJoz;

            long Shomareh = 1;
            clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
            if (rizMetreUser != null)
            {
                Shomareh = rizMetreUser.Shomareh + 1;
            }


            List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year).ToList();

            ///آیتم های حمل درج میگردد
            string FBShomareh = ItemFBShomareh;
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
                        Shomareh = strItemHamlFB
                    };
                    _context.FBs.Add(newFBHaml);
                    gFBIdHaml = newFBHaml.ID;
                }

                ///
                ///درج ریز متره
                ///

                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh++;
                RizMetre.Sharh = " حمل ";
                RizMetre.Tedad = null;
                RizMetre.Tool = null;
                RizMetre.Arz = null;
                RizMetre.Ertefa = null;
                RizMetre.Vazn = null;
                RizMetre.Des = "";
                RizMetre.FBId = gFBIdHaml;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "3";
                RizMetre.ForItem = strItemHamlFB;
                RizMetre.UseItem = "";
                RizMetre.MeghdarJoz = MeghdarJoz;
                _context.RizMetreUserses.Add(RizMetre);

                clsBarAvordHamlRizMetre BarAvordHamlRizMetre
                    = new clsBarAvordHamlRizMetre
                    {
                        BarAvordHamlId = BarAvordHamlId,
                        RizMetreId = RizMetre.ID
                    };
                _context.BarAvordHamlRizMetres.Add(BarAvordHamlRizMetre);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
