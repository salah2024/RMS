using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Common;

public static class HamlCommon
{
    public static bool SaveHaml(SaveHamlDto request, ApplicationDbContext _context)
    {
        try
        {
            DateTime Now = DateTime.Now;
            Guid BarAvordUserId = request.BarAvordUserId;
            NoeFehrestBaha NoeFBId = request.NoeFBId;
            long Year = request.Year;
            string ItemFBShomareh = request.ItemFBShomareh.Trim();
            //Guid BarAvordHamlId = request.BarAvordHamlId;
            long Shomareh = request.Shomareh;
            decimal? MeghdarJoz = request.MeghdarJoz;
            int LevelNumber = request.LevelNumber;

            long NewShomareh = 1;
            clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
            if (rizMetreUser != null)
            {
                NewShomareh = rizMetreUser.Shomareh + 1;
            }
            List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year).ToList();

            ///آیتم های حمل درج میگردد
            string FBShomareh = ItemFBShomareh;
            //bool blnHasHaml = false;
            List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = lstRelatedToItemHaml.Where(x => x.ItemFB.Trim() == FBShomareh).ToList();
            string strItemHamlFB = "";
            decimal? Zarib1 = null;
            decimal? Zarib2 = null;
            decimal? Zarib3 = null;
            if (lstItemsRelatedToItemHaml.Count != 0)
            {
                foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
                {
                    Guid gBarAvordHamlId = new Guid();
                    strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                    Zarib1 = itemsRelatedToItemHaml.Zarib1;
                    Zarib2 = itemsRelatedToItemHaml.Zarib2;
                    Zarib3 = itemsRelatedToItemHaml.Zarib3;

                    clsBarAvordHaml? currentBarAvordHaml = _context.BarAvordHamls.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.FBShomareh == FBShomareh && x.FBShomarehHaml == strItemHamlFB);
                    if (currentBarAvordHaml == null)
                    {

                        gBarAvordHamlId = Guid.NewGuid();
                        clsBarAvordHaml barAvordHaml = new clsBarAvordHaml
                        {
                            ID = gBarAvordHamlId,
                            BarAvordId = BarAvordUserId,
                            FBShomareh = FBShomareh,
                            FBShomarehHaml = strItemHamlFB,
                            NoeFBId = NoeFBId
                        };
                        _context.BarAvordHamls.Add(barAvordHaml);
                    }
                    else
                    {
                        gBarAvordHamlId = currentBarAvordHaml.ID;
                    }

                    //blnHasHaml = true;
                    //if (blnHasHaml)
                    //{
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
                    RizMetre.Shomareh = Shomareh;
                    RizMetre.ShomarehNew = NewShomareh.ToString();
                    RizMetre.Sharh = " حمل ";
                    RizMetre.Tedad = null;
                    RizMetre.Tool = Zarib1;
                    RizMetre.Arz = Zarib2;
                    RizMetre.Ertefa = Zarib3;
                    //RizMetre.Vazn = null;
                    RizMetre.Des = " آیتم - " + ItemFBShomareh;
                    RizMetre.FBId = gFBIdHaml;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "3";
                    RizMetre.ForItem = strItemHamlFB;
                    RizMetre.UseItem = "";
                    RizMetre.LevelNumber = LevelNumber;

                    NewShomareh++;

                    decimal? dAllZarib = (Zarib1 != null ? Zarib1.Value : 1) * (Zarib2 != null ? Zarib2.Value : 1) * (Zarib3 != null ? Zarib3.Value : 1);
                    ///محاسبه مقدار جزء
                    //decimal? dMeghdarJozHaml = dAllZarib;
                    //if (Tedad == null && Tool == null && Arz == null && Ertefa == null)
                    //    dMeghdarJozHaml = 0;
                    //else
                    //    dMeghdarJozHaml += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                    //    (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Zarib == null ? 1 : Zarib.Value);

                    RizMetre.Vazn = MeghdarJoz;
                    RizMetre.MeghdarJoz = dAllZarib * MeghdarJoz;
                    _context.RizMetreUserses.Add(RizMetre);

                    clsBarAvordHamlRizMetre BarAvordHamlRizMetre
                        = new clsBarAvordHamlRizMetre
                        {
                            BarAvordHamlId = gBarAvordHamlId,
                            RizMetreId = RizMetre.ID
                        };
                    _context.BarAvordHamlRizMetres.Add(BarAvordHamlRizMetre);
                    //}
                }
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
            NoeFehrestBaha NoeFBId = request.NoeFBId;

            List<clsBarAvordHaml> lstBarAvordHaml = _context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.FBShomareh == FBShomareh).ToList();
            if (lstBarAvordHaml.Count != 0)
            {
                List<clsBarAvordHamlRizMetre> lstBarAvordHamlRizMetre = _context.BarAvordHamlRizMetres.Where(x => lstBarAvordHaml.Select(x => x.ID).Contains(x.BarAvordHamlId)).ToList();

                List<Guid> lstBarAvordHamlRMIds = lstBarAvordHamlRizMetre.Select(x => x.RizMetreId).ToList();
                List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => x.Shomareh == RMShomareh && lstBarAvordHamlRMIds.Contains(x.ID)).ToList();
                if (lstRizMetre.Count != 0)
                {
                    _context.RizMetreUserses.RemoveRange(lstRizMetre);
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
