using Microsoft.EntityFrameworkCore;
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
            long Shomareh = request.Shomareh;
            decimal? Tedad = request.Tedad;
            decimal? Tool = request.Tool;
            decimal? Arz = request.Arz;
            decimal? Ertefa = request.Ertefa;

            long NewShomareh = 1;
            clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
            if (rizMetreUser != null)
            {
                NewShomareh = rizMetreUser.Shomareh + 1;
            }


            List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year).ToList();

            ///آیتم های حمل درج میگردد
            string FBShomareh = ItemFBShomareh;
            bool blnHasHaml = false;
            clsItemsRelatedToItemHaml? itemsRelatedToItemHaml = lstRelatedToItemHaml.FirstOrDefault(x => x.ItemFB.Trim() == FBShomareh);
            string strItemHamlFB = "";
            decimal? Zarib = 0;
            if (itemsRelatedToItemHaml != null)
            {
                strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                Zarib = itemsRelatedToItemHaml.Zarib;
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
                RizMetre.Shomareh = Shomareh;
                RizMetre.ShomarehNew = NewShomareh.ToString();
                RizMetre.Sharh = " حمل ";
                RizMetre.Tedad = Tedad;
                RizMetre.Tool = Tool;
                RizMetre.Arz = Arz;
                RizMetre.Ertefa = Ertefa;
                RizMetre.Vazn = Zarib;
                RizMetre.Des = " آیتم - " + ItemFBShomareh;
                RizMetre.FBId = gFBIdHaml;
                RizMetre.OperationsOfHamlId = 1;
                RizMetre.Type = "3";
                RizMetre.ForItem = strItemHamlFB;
                RizMetre.UseItem = "";

                NewShomareh++;
                ///محاسبه مقدار جزء
                decimal dMeghdarJozHaml = 0;
                if (Tedad == null && Tool == null && Arz == null && Ertefa == null)
                    dMeghdarJozHaml = 0;
                else
                    dMeghdarJozHaml += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                    (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Zarib == null ? 1 : Zarib.Value);

                RizMetre.MeghdarJoz = dMeghdarJozHaml;
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

    public static bool DeleteHaml(DeleteHamlDto request, ApplicationDbContext _context)
    {
        try
        {
            Guid BarAvordId = request.BarAvordId;
            string FBShomareh = request.FBShomareh;
            long RMShomareh = request.RMShomareh;

            clsBarAvordHaml? barAvordHaml = _context.BarAvordHamls.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == FBShomareh);
            if (barAvordHaml != null)
            {
                List<clsBarAvordHamlRizMetre> lstBarAvordHamlRizMetre = _context.BarAvordHamlRizMetres.Where(x => x.BarAvordHamlId == barAvordHaml.ID).ToList();

                List<Guid> lstBarAvordHamlRMIds = lstBarAvordHamlRizMetre.Select(x => x.RizMetreId).ToList();
                clsRizMetreUsers? RizMetre = _context.RizMetreUserses.FirstOrDefault(x => x.Shomareh == RMShomareh && lstBarAvordHamlRMIds.Contains(x.ID));
                if (RizMetre != null)
                {
                    _context.RizMetreUserses.Remove(RizMetre);
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
