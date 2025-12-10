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

            clsEzafeBahaKhakRizi ezafeBahaKhakRizi = _context.EzafeBahaKhakRizis.First(x => x.ConditionContextId == ConditionContextId && x.Year == Year);

            List<clsConditionContext> lstConditionContext = _context.ConditionContexts
                    .AsNoTracking()
                    .Where(c =>
                        _context.ConditionContexts
                          .Where(x => x.Id == ezafeBahaKhakRizi.ConditionContextId)
                          .Select(x => x.ConditionGroupId)
                          .Contains(c.ConditionGroupId))
            .ToList();
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
            };
            _context.KhakRiziEzafeBahas.Add(khakRiziEzafeBaha);




            List<clsKhakRiziDarsad> lstKhakRiziDarsad = _context.KhakRiziDarsads.Where(x => x.NoeRah == RoadTypeId && x.NoeDaneBandi == NoeDaneBandiId && x.Year == Year).ToList();

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

            List<clsKhakRiziDarsad> lstKhakRiziDarsad1 = lstKhakRiziDarsad.Where(x => hajmKhakRiziEnums.Contains(x.NoeHajmKhakRizi)).ToList();
            List<clsEzafeBahaKhakRiziAddItems> lstEzafeBahaKhakRiziAddItems =
                 _context.EzafeBahaKhakRiziAddItemses.Include(x => x.EzafeBahaKhakRizi)
                 .Where(x => x.EzafeBahaKhakRizi.Year == Year && x.EzafeBahaKhakRiziId == ezafeBahaKhakRizi.Id).ToList();

            long Shomareh = 1;
            clsRizMetreUsers? rizMetreUser = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordUserId);
            if (rizMetreUser != null)
            {
                Shomareh = rizMetreUser.Shomareh + 1;
            }

            foreach (var itemDarsad in lstKhakRiziDarsad1)
            {
                string ItemFBShomareh = "";
                string? CharacterPlus = "";
                foreach (var EzafeBahaKhakRiziAddItem in lstEzafeBahaKhakRiziAddItems)
                {
                    string strCondition = EzafeBahaKhakRiziAddItem.Condition != null ? EzafeBahaKhakRiziAddItem.Condition.Trim() : "";
                    if (strCondition != "")
                    {
                        string strConditionOp = strCondition.Replace("x", itemDarsad.Darsad.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            ItemFBShomareh = EzafeBahaKhakRiziAddItem.ItemFBShomareh;
                            CharacterPlus = EzafeBahaKhakRiziAddItem.CharacterPlus;
                            break;
                        }
                    }


                    //clsKhakRiziEzafeBahaBarAvord khakRiziEzafeBahaBarAvord = new clsKhakRiziEzafeBahaBarAvord
                    //{
                    //    BarAvordId = BarAvordUserId,
                    //    EzafeBahaKhakRiziId = EzafeBahaKhakRiziAddItem.EzafeBahaKhakRiziId,
                    //};

                    //_context.KhakRiziEzafeBahaBarAvords.Add(khakRiziEzafeBahaBarAvord);

                }

                //decimal? MeghdarJoz = null;
                //foreach (var item in lstHajmKhakRiziValue)
                //{
                //    if ((EnumHajmKhakRizi)item.Id == itemDarsad.NoeHajmKhakRizi)
                //    {
                //        MeghdarJoz = item.Value;
                //    }
                //}


                clsFB? FB = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordUserId && x.Shomareh == ItemFBShomareh + CharacterPlus);
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
                        Shomareh = ItemFBShomareh + CharacterPlus,
                        NoeFBId=NoeFBId
                    };
                    _context.FBs.Add(newFB);
                    gFBId = newFB.ID;
                }

                clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                RizMetre.Shomareh = Shomareh++;
                RizMetre.Sharh = "";
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

                //clsKhakRiziBarAvord? khakRiziBarAvord = _context.KhakRiziBarAvords.FirstOrDefault(x => x.BarAvordId == BarAvordUserId);

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
            return "OK";
        }
    }
}
