using Microsoft.EntityFrameworkCore;
using RMS.Controllers.KhakRizi.Dto;
using RMS.Controllers.KhakRizi.EnumKhakRizi;
using RMS.Models.Common;
using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi.Common;

public class KhakRiziCommon
{
    public static string SaveEzafeBahaKhakRizi(requestSaveEBKhakRiziDto request, ApplicationDbContext _context)
    {
        clsKhakRiziBarAvord? currentKhakRiziBar = _context.KhakRiziBarAvords.FirstOrDefault(x => x.BarAvordId == request.BarAvordUserId && x.KMNum == request.Num);
        if (currentKhakRiziBar == null)
        {
            return "NOK";
        }
        else
        {
            decimal dFrom = decimal.Parse(currentKhakRiziBar.FromKM);
            decimal dTo = decimal.Parse(currentKhakRiziBar.ToKM);

            decimal dFaseleh = (dTo - dFrom) * 1000;

            Guid KhakRiziBarAvordId = currentKhakRiziBar.ID;
            long ConditionContextId = request.ConditionContextId;

            clsBaravordUser currentBaravordUser = _context.BaravordUsers.First(x => x.ID == request.BarAvordUserId);

            DateTime Now = DateTime.Now;
            Guid BarAvordUserId = currentKhakRiziBar.BarAvordId;
            NoeFehrestBaha NoeFBId = request.NoeFBId;
            string FromKM = currentKhakRiziBar.FromKM;
            string ToKM = currentKhakRiziBar.ToKM;
            EnumRoadType RoadTypeId = currentKhakRiziBar.NoeRah;
            EnumNoeDaneBandi NoeDaneBandiId = currentKhakRiziBar.NoeDaneBandi;
            string? HajmKhakRiziValue = currentKhakRiziBar.NoeHajmKhakRizi_Value;
            long Year = currentBaravordUser.Year;

            clsEzafeBahaKhakRizi ezafeBahaKhakRizi = _context.EzafeBahaKhakRizis.First(x => x.ConditionContextId == ConditionContextId && x.Year == Year && x.NoeFB == NoeFBId);

            switch (ezafeBahaKhakRizi.Type)
            {
                case 1:
                    {

                        List<clsConditionContext> lstConditionContext = _context.ConditionContexts
                        .AsNoTracking()
                        .Where(c =>
                          _context.ConditionContexts.Where(x => x.Id == ezafeBahaKhakRizi.ConditionContextId)
                         .Select(x => x.ConditionGroupId)
                         .Contains(c.ConditionGroupId)).ToList();

                        List<long> lstIds = lstConditionContext.Select(x => x.Id).ToList();
                        List<clsEzafeBahaKhakRizi> lstEzafeBahaKhakRizi = _context.EzafeBahaKhakRizis.Where(x => lstIds.Contains(x.ConditionContextId)).ToList();
                        List<clsKhakRiziEzafeBaha> lstKhakRiziEzafeBaha =
                            _context.KhakRiziEzafeBahas.Include(x => x.KhakRiziEzafeBahaRizMetres)
                             .Where(x => x.KhakRiziBarAvordId == KhakRiziBarAvordId && lstEzafeBahaKhakRizi.Select(x => x.Id).ToList().Contains(x.EzafeBahaKhakRiziId))
                        .ToList();

                        _context.KhakRiziEzafeBahas.RemoveRange(lstKhakRiziEzafeBaha);

                        clsKhakRiziEzafeBaha khakRiziEzafeBaha = new clsKhakRiziEzafeBaha
                        {
                            KhakRiziBarAvordId = KhakRiziBarAvordId,
                            EzafeBahaKhakRiziId = ezafeBahaKhakRizi.Id,
                            InsertDateTime = Now,
                        };
                        _context.KhakRiziEzafeBahas.Add(khakRiziEzafeBaha);


                        List<clsKhakRiziCondition> lstKhakRiziCondition = _context.KhakRiziConditions.Where(x => x.NoeRah == RoadTypeId && x.NoeDaneBandi == NoeDaneBandiId && x.NoeFB == NoeFBId && x.Year == Year).ToList();

                        List<HajmKhakRiziValueDto> lstHajmKhakRiziValue = new List<HajmKhakRiziValueDto>();
                        if (HajmKhakRiziValue != null)
                        {
                            string[] HajmKhakRiziValueSplit = HajmKhakRiziValue.Split(",");
                            foreach (var item in HajmKhakRiziValueSplit)
                            {
                                if (item.Trim() != "")
                                {
                                    string[] strItem = item.Split("_");
                                    if (strItem[1].Trim() != "0")
                                    {
                                        long HajmKhakRiziId = long.Parse(strItem[0].Trim());
                                        decimal dValue = decimal.Parse(strItem[1].Trim());

                                        lstHajmKhakRiziValue.Add(new HajmKhakRiziValueDto
                                        {
                                            Id = HajmKhakRiziId,
                                            Value = dValue,
                                        });
                                    }
                                }
                            }

                        }

                        List<EnumHajmKhakRizi> hajmKhakRiziEnums =
                            lstHajmKhakRiziValue
                        .Where(x => Enum.IsDefined(typeof(EnumHajmKhakRizi), (int)x.Id))
                        .Select(x => (EnumHajmKhakRizi)(int)x.Id)
                        .ToList();

                        EnumHajmKhakRizi hajmKhakRizi = hajmKhakRiziEnums.First();
                        if (hajmKhakRiziEnums.Count > 1)
                        {
                            hajmKhakRizi = hajmKhakRiziEnums.First(x => x == EnumHajmKhakRizi.Low30);

                        }

                        List<clsKhakRiziCondition> lstKhakRiziCondition1 = lstKhakRiziCondition.Where(x => x.NoeHajmKhakRizi == hajmKhakRizi && x.FBShomarehCondition2!="").ToList();

                        long Shomareh = 1;
                        clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
                        if (rizMetreUser != null)
                        {
                            Shomareh = rizMetreUser.Shomareh + 1;
                        }

                        foreach (var itemCondition in lstKhakRiziCondition1)
                        {
                            string ItemFBShomareh = itemCondition.FBShomarehCondition2;
                            //decimal dErtefa = 0;

                            decimal? MeghdarJoz = null;
                            foreach (var item in lstHajmKhakRiziValue)
                            {
                                if ((EnumHajmKhakRizi)item.Id == itemCondition.NoeHajmKhakRizi)
                                {
                                    MeghdarJoz = item.Value;
                                }
                            }


                            clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == ItemFBShomareh && x.NoeFBId == NoeFBId);
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
                                    Shomareh = ItemFBShomareh,
                                    NoeFBId = NoeFBId
                                };
                                _context.FBs.Add(newFB);
                                gFBId = newFB.ID;
                            }

                            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                            RizMetre.Shomareh = Shomareh++;
                            RizMetre.Sharh = "";
                            RizMetre.Tedad = null;
                            RizMetre.Tool = dFaseleh >= 0 ? dFaseleh : null;
                            RizMetre.Arz = null;
                            RizMetre.Ertefa = null;
                            RizMetre.Vazn = null;
                            RizMetre.Des = "کیلومتراژ " + FromKM + " تا " + ToKM;
                            RizMetre.FBId = gFBId;
                            RizMetre.OperationsOfHamlId = 1;
                            RizMetre.Type = "1";
                            RizMetre.ForItem = "";
                            RizMetre.UseItem = "";

                            //RizMetre.MeghdarJoz = MeghdarJoz;

                            _context.RizMetreUserses.Add(RizMetre);

                            if (khakRiziEzafeBaha != null)
                            {
                                clsKhakRiziEzafeBahaRizMetre KhakRiziEzafeBahaRizMetre
                                    = new clsKhakRiziEzafeBahaRizMetre
                                    {
                                        KhakRiziEzafeBahaId = khakRiziEzafeBaha.ID,
                                        RizMetreUserId = RizMetre.ID
                                    };
                                _context.KhakRiziEzafeBahaRizMetres.Add(KhakRiziEzafeBahaRizMetre);
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        decimal dTextVal = decimal.Parse(request.TextVal);

                        List<clsConditionContext> lstConditionContext = _context.ConditionContexts
                        .AsNoTracking()
                        .Where(c =>
                          _context.ConditionContexts.Where(x => x.Id == ezafeBahaKhakRizi.ConditionContextId)
                         .Select(x => x.ConditionGroupId)
                         .Contains(c.ConditionGroupId)).ToList();

                        List<long> lstIds = lstConditionContext.Select(x => x.Id).ToList();

                        //if (lstIds.Count > 1)
                        //{
                        List<clsEzafeBahaKhakRizi> lstEzafeBahaKhakRizi = _context.EzafeBahaKhakRizis.Where(x => lstIds.Contains(x.ConditionContextId)).ToList();
                        List<clsKhakRiziEzafeBaha> lstKhakRiziEzafeBaha =
                            _context.KhakRiziEzafeBahas.Include(x => x.KhakRiziEzafeBahaRizMetres)
                             .Where(x => x.KhakRiziBarAvordId == KhakRiziBarAvordId && lstEzafeBahaKhakRizi.Select(x => x.Id).ToList().Contains(x.EzafeBahaKhakRiziId))
                        .ToList();

                        _context.KhakRiziEzafeBahas.RemoveRange(lstKhakRiziEzafeBaha);


                        //}


                        clsKhakRiziEzafeBaha khakRiziEzafeBaha = new clsKhakRiziEzafeBaha
                        {
                            KhakRiziBarAvordId = KhakRiziBarAvordId,
                            EzafeBahaKhakRiziId = ezafeBahaKhakRizi.Id,
                            Meghdar = dTextVal,
                            InsertDateTime = Now,
                        };
                        _context.KhakRiziEzafeBahas.Add(khakRiziEzafeBaha);




                        List<clsKhakRiziCondition> lstKhakRiziCondition = _context.KhakRiziConditions.Where(x => x.NoeRah == RoadTypeId && x.NoeDaneBandi == NoeDaneBandiId && x.NoeFB == NoeFBId && x.Year == Year).ToList();

                        List<HajmKhakRiziValueDto> lstHajmKhakRiziValue = new List<HajmKhakRiziValueDto>();
                        if (HajmKhakRiziValue != null)
                        {
                            string[] HajmKhakRiziValueSplit = HajmKhakRiziValue.Split(",");
                            foreach (var item in HajmKhakRiziValueSplit)
                            {
                                if (item.Trim() != "")
                                {
                                    string[] strItem = item.Split("_");
                                    if (strItem[1].Trim() != "0")
                                    {
                                        long HajmKhakRiziId = long.Parse(strItem[0].Trim());
                                        decimal dValue = decimal.Parse(strItem[1].Trim());

                                        lstHajmKhakRiziValue.Add(new HajmKhakRiziValueDto
                                        {
                                            Id = HajmKhakRiziId,
                                            Value = dValue,
                                        });
                                    }
                                }
                            }

                        }

                        //List<long> HajmKhakRiziIds = lstHajmKhakRiziValue.Select(x => x.Id).ToList();

                        List<EnumHajmKhakRizi> hajmKhakRiziEnums =
                            lstHajmKhakRiziValue
                        .Where(x => Enum.IsDefined(typeof(EnumHajmKhakRizi), (int)x.Id))
                        .Select(x => (EnumHajmKhakRizi)(int)x.Id)
                        .ToList();

                        EnumHajmKhakRizi hajmKhakRizi = hajmKhakRiziEnums.First();
                        if (hajmKhakRiziEnums.Count > 1)
                        {
                            hajmKhakRizi = hajmKhakRiziEnums.First(x => x == EnumHajmKhakRizi.Low30);

                        }


                        List<clsKhakRiziCondition> lstKhakRiziCondition1 = lstKhakRiziCondition.Where(x => x.NoeHajmKhakRizi == hajmKhakRizi).ToList();

                        //List<clsEzafeBahaKhakRiziAddItems> lstEzafeBahaKhakRiziAddItems =
                        //     _context.EzafeBahaKhakRiziAddItemses.Include(x => x.EzafeBahaKhakRizi)
                        //     .Where(x => x.EzafeBahaKhakRizi.Year == Year && x.EzafeBahaKhakRiziId == ezafeBahaKhakRizi.Id).ToList();

                        long Shomareh = 1;
                        clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
                        if (rizMetreUser != null)
                        {
                            Shomareh = rizMetreUser.Shomareh + 1;
                        }

                        foreach (var itemCondition in lstKhakRiziCondition1)
                        {
                            string strFinalWorking = "";
                            string strCharacterPlus = "";
                            string ItemFBShomareh = "";
                            decimal dErtefa = 0;
                            //foreach (var KhakRiziItem in lstKhakRiziItem)
                            //{
                            string strCondition = itemCondition.Condition.Trim();
                            if (strCondition != "")
                            {
                                //strCharacterPlus = itemCondition.CharacterPlus.Trim();
                                string strConditionOp = strCondition.Replace("z", dTextVal.ToString());
                                StringToFormula StringToFormula = new StringToFormula();
                                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                if (blnCheck)
                                {
                                    ItemFBShomareh = itemCondition.FBShomareh;
                                    strFinalWorking = itemCondition.FinalWork.Replace("z", dTextVal.ToString());
                                    //StringToFormula StringToFormula = new StringToFormula();
                                    decimal dFinalWork = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                    dErtefa = dFinalWork;
                                    //break;

                                    //}

                                    decimal? MeghdarJoz = null;
                                    foreach (var item in lstHajmKhakRiziValue)
                                    {
                                        if ((EnumHajmKhakRizi)item.Id == itemCondition.NoeHajmKhakRizi)
                                        {
                                            MeghdarJoz = item.Value;
                                        }
                                    }


                                    clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == ItemFBShomareh && x.NoeFBId == NoeFBId);
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
                                            Shomareh = ItemFBShomareh + strCharacterPlus,
                                            NoeFBId = NoeFBId
                                        };
                                        _context.FBs.Add(newFB);
                                        gFBId = newFB.ID;
                                    }

                                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                                    RizMetre.Shomareh = Shomareh++;
                                    RizMetre.Sharh = "";
                                    RizMetre.Tedad = null;
                                    RizMetre.Tool = dFaseleh >= 0 ? dFaseleh : null;
                                    RizMetre.Arz = null;
                                    RizMetre.Ertefa = dErtefa;
                                    RizMetre.Vazn = null;
                                    RizMetre.Des = "کیلومتراژ " + FromKM + " تا " + ToKM;
                                    RizMetre.FBId = gFBId;
                                    RizMetre.OperationsOfHamlId = 1;
                                    RizMetre.Type = "1";
                                    RizMetre.ForItem = "";
                                    RizMetre.UseItem = "";

                                    //RizMetre.MeghdarJoz = MeghdarJoz;

                                    _context.RizMetreUserses.Add(RizMetre);

                                    if (khakRiziEzafeBaha != null)
                                    {
                                        clsKhakRiziEzafeBahaRizMetre KhakRiziEzafeBahaRizMetre
                                            = new clsKhakRiziEzafeBahaRizMetre
                                            {
                                                KhakRiziEzafeBahaId = khakRiziEzafeBaha.ID,
                                                RizMetreUserId = RizMetre.ID
                                            };
                                        _context.KhakRiziEzafeBahaRizMetres.Add(KhakRiziEzafeBahaRizMetre);
                                    }
                                }
                            }
                        }


                        break;
                    }
                default:
                    break;
            }

            //foreach (var itemCondition in lstKhakRiziCondition1)
            //{
            //    string ItemFBShomareh = "";
            //    string? CharacterPlus = "";
            //    foreach (var EzafeBahaKhakRiziAddItem in lstEzafeBahaKhakRiziAddItems)
            //    {
            //        string strCondition = EzafeBahaKhakRiziAddItem.Condition != null ? EzafeBahaKhakRiziAddItem.Condition.Trim() : "";
            //        if (strCondition != "")
            //        {
            //            string strConditionOp = strCondition.Replace("x", itemCondition.Condition.ToString().Trim());
            //            StringToFormula StringToFormula = new StringToFormula();
            //            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
            //            if (blnCheck)
            //            {
            //                ItemFBShomareh = EzafeBahaKhakRiziAddItem.ItemFBShomareh;
            //                CharacterPlus = EzafeBahaKhakRiziAddItem.CharacterPlus;
            //                break;
            //            }
            //        }


            //        //clsKhakRiziEzafeBahaBarAvord khakRiziEzafeBahaBarAvord = new clsKhakRiziEzafeBahaBarAvord
            //        //{
            //        //    BarAvordId = BarAvordUserId,
            //        //    EzafeBahaKhakRiziId = EzafeBahaKhakRiziAddItem.EzafeBahaKhakRiziId,
            //        //};

            //        //_context.KhakRiziEzafeBahaBarAvords.Add(khakRiziEzafeBahaBarAvord);

            //    }

            //    //decimal? MeghdarJoz = null;
            //    //foreach (var item in lstHajmKhakRiziValue)
            //    //{
            //    //    if ((EnumHajmKhakRizi)item.Id == itemDarsad.NoeHajmKhakRizi)
            //    //    {
            //    //        MeghdarJoz = item.Value;
            //    //    }
            //    //}


            //    clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == ItemFBShomareh + CharacterPlus && x.NoeFBId == NoeFBId);
            //    Guid gFBId = new Guid();
            //    if (FB != null)
            //    {
            //        gFBId = FB.ID;
            //    }
            //    else
            //    {
            //        clsFB newFB = new clsFB
            //        {
            //            BarAvordId = BarAvordUserId,
            //            InsertDateTime = Now,
            //            Shomareh = ItemFBShomareh + CharacterPlus,
            //            NoeFBId = NoeFBId
            //        };
            //        _context.FBs.Add(newFB);
            //        gFBId = newFB.ID;
            //    }

            //    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            //    RizMetre.Shomareh = Shomareh++;
            //    RizMetre.Sharh = "";
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

            //    RizMetre.MeghdarJoz = null;

            //    _context.RizMetreUserses.Add(RizMetre);

            //    //clsKhakRiziBarAvord? khakRiziBarAvord = _context.KhakRiziBarAvords.FirstOrDefault(x => x.BarAvordId == BarAvordUserId);

            //    if (khakRiziEzafeBaha != null)
            //    {
            //        clsKhakRiziEzafeBahaRizMetre KhakRiziEzafeBahaRizMetre
            //            = new clsKhakRiziEzafeBahaRizMetre
            //            {
            //                KhakRiziEzafeBahaId = khakRiziEzafeBaha.ID,
            //                RizMetreUserId = RizMetre.ID
            //            };
            //        _context.KhakRiziEzafeBahaRizMetres.Add(KhakRiziEzafeBahaRizMetre);
            //    }
            //}
            return "OK";
        }
    }
}
