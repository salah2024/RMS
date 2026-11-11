using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Common;

public static class HamlCommon
{
    public static async Task<List<Guid>> SaveHaml(SaveHamlDto request, ApplicationDbContext _context)
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
            long? ConditionContextId = request.ConditionContextId;
            string? BaseFBItem = request.BaseFBItem;


            long NewShomareh = 1;
            clsRizMetreUsers? rizMetreUser = await _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefaultAsync(x => x.FB.BarAvordId == BarAvordUserId);
            if (rizMetreUser != null)
            {
                NewShomareh = rizMetreUser.Shomareh + 1;
            }
            List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = await _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToListAsync();

            clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
            clsBaravordUser? baravordUserBase = null;
            List<clsBarAvordHaml> lstBaseCurrentBarAvordHaml = new List<clsBarAvordHaml>();
            string FBShomareh = ItemFBShomareh;
            if (baravordUser != null)
            {
                if (baravordUser.Type == NoeBarAvord.SooratVaziat)
                {
                    baravordUserBase = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == baravordUser.ParentId);
                    if (baravordUserBase != null)
                        lstBaseCurrentBarAvordHaml = await _context.BarAvordHamls.Where(x => x.BarAvordId == baravordUserBase.ID
                                                            && x.NoeFBId == NoeFBId && x.FBShomareh == FBShomareh).ToListAsync();
                }
            }
            ///آیتم های حمل درج میگردد
            //bool blnHasHaml = false;
            List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = lstRelatedToItemHaml.Where(x => x.ItemFB.Trim() == FBShomareh).ToList();
            string strItemHamlFB = "";
            decimal? Zarib1 = null;
            decimal? Zarib2 = null;
            decimal? Zarib3 = null;

            List<clsBarAvordHaml> lstCurrentBarAvordHaml = await _context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordUserId
                                                && x.NoeFBId == NoeFBId && x.FBShomareh == FBShomareh).ToListAsync();


            List<Guid> lstRMIds = new List<Guid>();
            if (lstItemsRelatedToItemHaml.Count != 0)
            {
                foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
                {
                    Guid gBarAvordHamlId = new Guid();
                    strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                    Zarib1 = itemsRelatedToItemHaml.Zarib1;
                    Zarib2 = itemsRelatedToItemHaml.Zarib2;
                    Zarib3 = itemsRelatedToItemHaml.Zarib3;

                    decimal? dAllZarib = (Zarib1 != null ? Zarib1.Value : 1) * (Zarib2 != null ? Zarib2.Value : 1) * (Zarib3 != null ? Zarib3.Value : 1);

                    if (dAllZarib < 0)
                    {
                        List<clsBarAvordHaml> lstBarAvordHaml =
                            _context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.FBShomareh == BaseFBItem && x.FBShomarehHaml == itemsRelatedToItemHaml.ItemHamlFB).ToList();

                        if (lstBarAvordHaml.Count != 0)
                        {
                            List<clsBarAvordHamlRizMetre> lstBarAvordHamlRizMetre = _context.BarAvordHamlRizMetres.Where(x => lstBarAvordHaml.Select(x => x.ID).Contains(x.BarAvordHamlId)).ToList();

                            List<Guid> lstBarAvordHamlRMIds = lstBarAvordHamlRizMetre.Select(x => x.RizMetreId).ToList();
                            List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstBarAvordHamlRMIds.Contains(x.ID)).ToList();
                            if (lstRizMetre.Count != 0)
                            {
                                //_context.RizMetreUserses.RemoveRange(lstRizMetre);

                                clsItemHasConditionBaseItemRemoved itemHasConditionBaseItemRemoved = new clsItemHasConditionBaseItemRemoved
                                {
                                    BarAvordId = BarAvordUserId,
                                    ConditionContextId = ConditionContextId.Value,
                                };
                                ///
                                /// درج در جدول
                                /// tblItemHasConditionBaseItemRemoved
                                /// تا موقعی که اضافه بها حذف شد موارد مربوط به آیتم اصلی اضافه گردد
                                foreach (var RizMetre in lstRizMetre)
                                {
                                    itemHasConditionBaseItemRemoved.RizMetreId = RizMetre.ID;
                                    _context.ItemHasConditionBaseItemRemoveds.Add(itemHasConditionBaseItemRemoved);
                                    RizMetre.IsDeleted = true;
                                }
                            }
                        }
                    }
                    else
                    {

                        clsBarAvordHaml? currentBarAvordHaml = lstCurrentBarAvordHaml.FirstOrDefault(x => x.FBShomarehHaml == strItemHamlFB);
                        if (currentBarAvordHaml == null)
                        {
                            decimal? dBAHamlValue = null;
                            decimal? dBAHamlValueBase = null;
                            NoeBarAvord NoeBAType = NoeBarAvord.BarAvord;
                            if (baravordUser != null)
                            {
                                NoeBAType = baravordUser.Type;
                            }
                            if (NoeBAType == NoeBarAvord.SooratVaziat)
                            {
                                clsBarAvordHaml? BaseCurrentBarAvordHaml = lstBaseCurrentBarAvordHaml.FirstOrDefault(x => x.FBShomarehHaml == strItemHamlFB);
                                if (BaseCurrentBarAvordHaml != null)
                                {
                                    dBAHamlValue = BaseCurrentBarAvordHaml.Value;
                                    dBAHamlValueBase = BaseCurrentBarAvordHaml.ValueBase;
                                }
                            }

                            gBarAvordHamlId = Guid.NewGuid();
                            clsBarAvordHaml barAvordHaml = new clsBarAvordHaml
                            {
                                ID = gBarAvordHamlId,
                                BarAvordId = BarAvordUserId,
                                FBShomareh = FBShomareh,
                                FBShomarehHaml = strItemHamlFB,
                                NoeFBId = NoeFBId,
                                Value = dBAHamlValue,
                                ValueBase = dBAHamlValueBase,
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
                        clsFB? FBHaml = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordUserId && x.Shomareh == strItemHamlFB && x.NoeFBId == NoeFBId);

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
                            FBHaml = newFBHaml;
                        }

                        ///
                        ///درج ریز متره
                        ///

                        clsBarAvordHaml? barAvordHamlCurrent =
                                        await _context.BarAvordHamls.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.FBShomarehHaml.Trim() == strItemHamlFB.Trim());// && x.FBShomareh == ItemFBShomareh);

                        decimal? dTedad = null;
                        if (barAvordHamlCurrent != null)
                        {
                            dTedad = barAvordHamlCurrent.Value;
                        }

                        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                        RizMetre.Shomareh = Shomareh;
                        RizMetre.ShomarehNew = NewShomareh.ToString();
                        RizMetre.Sharh = " حمل ";
                        RizMetre.Tedad = dTedad;
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

                        /////////////////////////////////////
                        //////دریافت جمع ریزمتره های جاری///
                        /////////////////////////////////////
                        decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(gFBIdHaml, _context);

                        decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue || RizMetre.MeghdarJoz.HasValue
                               ? (dSumRizMetreCurrentFB ?? 0) + (RizMetre.MeghdarJoz ?? 0)
                               : null;

                        FBHaml.SumRizMetre = dSumRizMetreCurrentFBAll;


                        clsBarAvordHamlRizMetre BarAvordHamlRizMetre
                            = new clsBarAvordHamlRizMetre
                            {
                                BarAvordHamlId = gBarAvordHamlId,
                                RizMetreId = RizMetre.ID
                            };
                        _context.BarAvordHamlRizMetres.Add(BarAvordHamlRizMetre);

                        lstRMIds.Add(RizMetre.ID);

                        //}
                    }
                }
                await _context.SaveChangesAsync();
            }
            return lstRMIds;// true;
        }
        catch (Exception)
        {
            return new List<Guid>();
        }
    }

    public static async Task<bool> DeleteHaml(DeleteHamlDto request, ApplicationDbContext _context)
    {
        try
        {
            Guid BarAvordId = request.BarAvordId;
            Guid RizMetreId = request.RIzMetreId;
            string FBShomareh = request.FBShomareh;
            long RMShomareh = request.RMShomareh;
            NoeFehrestBaha NoeFBId = request.NoeFBId;
            long? ConditionContextId = request.ConditionContextId;

            //List<Guid> lstBarAvordHamlRizMetreIds = _context.BarAvordHamlRizMetres.Where(x => x.RizMetreId == RizMetreId).Select(x => x.RizMetreId).ToList();

            //List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstBarAvordHamlRizMetreIds.Contains(x.ID)).ToList();

            List<clsBarAvordHaml> lstBarAvordHaml = _context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.FBShomareh == FBShomareh).ToList();
            if (lstBarAvordHaml.Count != 0)
            {
                List<clsBarAvordHamlRizMetre> lstBarAvordHamlRizMetre = _context.BarAvordHamlRizMetres.Where(x => lstBarAvordHaml.Select(x => x.ID).Contains(x.BarAvordHamlId)).ToList();

                List<Guid> lstBarAvordHamlRMIds = lstBarAvordHamlRizMetre.Select(x => x.RizMetreId).ToList();
                List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => x.Shomareh == RMShomareh && lstBarAvordHamlRMIds.Contains(x.ID)).ToList();
                if (lstRizMetre.Count != 0)
                {
                    _context.RizMetreUserses.RemoveRange(lstRizMetre);


                    ///
                    ///بررسی اینکه در صورتی که تیک اضافه بهایی برداشته شود
                    ///بایستی بررسی گردد که آیتم حملی جایگزین آیتم حمل آیتم اصلی شده اشت یا خیر
                    ///
                    if (ConditionContextId != null)
                    {
                        List<clsItemHasConditionBaseItemRemoved> lstItemHasConditionBaseItemRemoved =
                            await _context.ItemHasConditionBaseItemRemoveds.Where(x => x.BarAvordId == BarAvordId && ConditionContextId == ConditionContextId.Value).ToListAsync();

                        if (lstItemHasConditionBaseItemRemoved.Count != 0)
                        {
                            List<Guid> lstRizMetreIds = lstItemHasConditionBaseItemRemoved.Select(x => x.RizMetreId).ToList();

                            List<clsRizMetreUsers> lstRizMetreHaml = await _context.RizMetreUserses.IgnoreQueryFilters().Where(x => lstRizMetreIds.Contains(x.ID) && x.IsDeleted==true).ToListAsync();
                            if (lstRizMetreHaml.Count != 0)
                            {
                                foreach (var RM in lstRizMetreHaml)
                                {
                                    RM.IsDeleted = false;
                                }
                            }

                            _context.ItemHasConditionBaseItemRemoveds.RemoveRange(lstItemHasConditionBaseItemRemoved);
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool UpdateHaml(UpdateHamlDto request, ApplicationDbContext _context)
    {
        try
        {
            int Year = request.Year;
            string FBShomareh = request.FBShomareh;
            decimal? dMeghdarJoz = request.MeghdarJoz;
            Guid BarAvordId = request.BarAvordId;
            long RMShomareh = request.RMShomareh;
            NoeFehrestBaha NoeFB = request.NoeFB;

            List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = _context.ItemsRelatedToItemHamls
                                .Where(x => x.ItemFB.Trim() == FBShomareh && x.Year == Year && x.NoeFB == NoeFB).ToList();

            foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
            {
                string strItemHamlFB = "";
                if (itemsRelatedToItemHaml != null)
                {
                    strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                    decimal? Zarib1 = itemsRelatedToItemHaml.Zarib1;
                    decimal? Zarib2 = itemsRelatedToItemHaml.Zarib2;
                    decimal? Zarib3 = itemsRelatedToItemHaml.Zarib3;
                    decimal? dAllZarib = (Zarib1 != null ? Zarib1.Value : 1) * (Zarib2 != null ? Zarib2.Value : 1) * (Zarib3 != null ? Zarib3.Value : 1);

                    string ItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB;
                    clsFB? FbHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemHamlFB);
                    if (FbHaml != null)
                    {
                        List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == FbHaml.ID
                            && x.ForItem == FBShomareh.Substring(0, 6).Trim() && x.Type == "3" && x.Shomareh == RMShomareh).ToList();

                        foreach (var RMForDelForHaml in lstRMForDelForHaml)
                        {
                            _context.Entry(RMForDelForHaml).CurrentValues.SetValues(new
                            {
                                Vazn = dMeghdarJoz,
                                MeghdarJoz = dAllZarib * dMeghdarJoz
                            });
                        }
                    }
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
