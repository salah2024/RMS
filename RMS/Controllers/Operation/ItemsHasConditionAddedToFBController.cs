using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation
{
    public class ItemsHasConditionAddedToFBController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult DeleteItemsHasConditionAddedToFBAndRizMetre([FromBody] DeleteItemsHasConditionAddedToFBAndRizMetreInputDto request)
        {
            //try
            //{

            Guid BarAvordId = request.BarAvordId;
            string strFBShomareh = request.strFBShomareh;
            long ConditionGroupId = request.ConditionGroupId;
            int LevelNumber = request.LevelNumber;
            int Year = request.Year;
            NoeFehrestBaha NoeFB = request.NoeFB;

            var varItemsHasConditionAddedToFB = (from ItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
                                                 join ItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                                 on ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals ItemsHasCondition_ConditionContext.Id
                                                 join ItemsHasCondition in _context.ItemsHasConditions on ItemsHasCondition_ConditionContext.ItemsHasConditionId equals ItemsHasCondition.Id
                                                 select new
                                                 {
                                                     ID = ItemsHasConditionAddedToFB.ID,
                                                     FBShomareh = ItemsHasConditionAddedToFB.FBShomareh,
                                                     BarAvordId = ItemsHasConditionAddedToFB.BarAvordId,
                                                     Meghdar = ItemsHasConditionAddedToFB.Meghdar,
                                                     Meghdar2 = ItemsHasConditionAddedToFB.Meghdar2,
                                                     ItemsHasCondition_ConditionContextId = ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                     ConditionGroupId = ItemsHasConditionAddedToFB.ConditionGroupId,
                                                     ItemShomareh = ItemsHasCondition.ItemFBShomareh
                                                 }).Where(x => x.BarAvordId == BarAvordId && x.FBShomareh.Substring(0, 6) == strFBShomareh && x.ConditionGroupId == ConditionGroupId).ToList();
            DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
            //DataTable DtItemsHasConditionAddedToFB = clsItemsHasConditionAddedToFB.ListWithParameterSimple("BarAvordId=" + intBarAvordId + " and FBShomareh='" + strFBShomareh + "' and ConditionGroup=" + ConditionGroupId);

            if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
            {
                //string strItemsHasCondition_ConditionContext = "";
                //if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                //{
                //    strItemsHasCondition_ConditionContext += "ItemsHasCondition_ConditionContextId in(";
                //    for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                //    {
                //        if ((i + 1) < DtItemsHasConditionAddedToFB.Rows.Count)
                //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString() + ",";
                //        else
                //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString();
                //    }
                //    strItemsHasCondition_ConditionContext += ")";
                //}

                List<long> strItemsHasCondition_ConditionContext = new List<long>();
                if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                {
                    for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                    {
                        strItemsHasCondition_ConditionContext.Add(long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString()));
                    }
                }
                bool blnCheckIsDel = false;
                try
                {
                    var ItemsHasConditionAddedToFB = _context.ItemsHasConditionAddedToFBs.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh.Substring(0, 6) == strFBShomareh.Trim() && x.ConditionGroupId == ConditionGroupId).ToList();
                    if (ItemsHasConditionAddedToFB.Count != 0)
                    {
                        _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
                        _context.SaveChanges();
                    }
                    blnCheckIsDel = true;
                }
                catch (Exception)
                {
                    blnCheckIsDel = false;
                    //throw;
                }


                //if (clsItemsHasConditionAddedToFB.Delete("BarAvordId=" + intBarAvordId + " and FBShomareh='" + strFBShomareh + "' and ConditionGroup=" + ConditionGroupId))
                if (blnCheckIsDel)
                {

                    var varItemsAddingToFB = _context.ItemsAddingToFBs.Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
                    DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);

                    for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
                    {
                        decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
                        string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();
                        DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
                        if (Dr.Length != 0)
                        {
                            for (int idr = 0; idr < Dr.Length; idr++)
                            {
                                string strFBShomarehAdded = Dr[idr]["AddedItems"].ToString().Trim();
                                switch (Dr[idr]["ConditionType"].ToString())
                                {
                                    case "1":
                                        {
                                            string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString();

                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "A'");
                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                if (tblRizMetreUser.Count != 0)
                                                {
                                                    _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                    _context.SaveChanges();
                                                }
                                                //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                            }
                                            break;
                                        }
                                    case "2":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                if (tblRizMetreUser.Count != 0)
                                                {
                                                    _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                    _context.SaveChanges();
                                                }
                                                //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                            }
                                            break;
                                        }
                                    case "3":
                                        {
                                            decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
                                            string strStatus = dPercent > 0 ? "B" : "e";
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strStatus).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + strStatus + "'");
                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                if (tblRizMetreUser.Count != 0)
                                                {
                                                    _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                    _context.SaveChanges();
                                                }
                                                //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["ID"].ToString().Trim() + " and _ForItem='" + strFBShomareh.Trim() + "'");
                                            }
                                            break;
                                        }
                                    case "4":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                                string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                                string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                                StringToFormula StringToFormula = new StringToFormula();
                                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                                                if (blnCheck)
                                                {
                                                    string strForItem = "";
                                                    string strUseItem = "";
                                                    if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
                                                    {
                                                        Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                        var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                        if (tblRizMetreUser.Count != 0)
                                                        {
                                                            _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                            _context.SaveChanges();
                                                        }
                                                        //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                    }
                                                    else
                                                    {
                                                        strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
                                                        strUseItem = strFBShomareh.Trim();
                                                        Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                        var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim() && x.UseItem == strUseItem).ToList();
                                                        if (tblRizMetreUser.Count != 0)
                                                        {
                                                            _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                            _context.SaveChanges();
                                                        }
                                                        //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strForItem + "' and UseItem='" + strUseItem + "'");
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case "5":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "6":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    List<clsRizMetreUsers> lstRMForDel = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    if (lstRMForDel.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(lstRMForDel);
                                                        long ConditionGroupId1 = long.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["ConditionGroupId"].ToString());
                                                        ///
                                                        ///ConditionGroup=12
                                                        ///برای پخش، آبپاشی، تسطیح و کوبیدن قشر زیراساس میباشد
                                                        ///که در این صورت بایستی اضافه بهای شانه سازی چک گردد و در صورت وجود ریز متره
                                                        ///برای اضافه بها شانه سازی باستی در آنجا نیز حذف گردند
                                                        ///

                                                        if (ConditionGroupId1 == 12)
                                                        {
                                                            //پیدا کردن ریزمتره های درج شده در اضافه بها شانه سازی
                                                            RizMetreCommon rizMetreCommon = new RizMetreCommon();
                                                            long[] lngConditionGroupId = { 15 };

                                                            GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                                                            {
                                                                ShomarehFB = strFBShomareh,
                                                                BarAvordUserId = BarAvordId,
                                                                NoeFB = NoeFB,
                                                                Year = Year,
                                                                LevelNumber = LevelNumber,
                                                                ConditionGroupId = lngConditionGroupId
                                                            };
                                                            List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);
                                                            if (lstRM.Count != 0)
                                                            {
                                                                foreach (var RMForDel in lstRM)
                                                                {
                                                                    clsRizMetreUsers? RMDeleted = _context.RizMetreUserses.FirstOrDefault(x => x.ID == RMForDel.Id);
                                                                    if (RMDeleted != null)
                                                                        _context.RizMetreUserses.Remove(RMDeleted);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            _context.SaveChanges();

                                            break;
                                        }
                                    case "7":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "8":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "10":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "11":
                                        {
                                            string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString();

                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomareh + strCharacterPlus).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "13":
                                        {
                                            decimal Meghdar2 = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar2"].ToString());
                                            decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);

                                            string[] strCondition = Dr[idr]["Condition"].ToString().Trim().Split("_");
                                            string strCondition1 = strCondition[0];
                                            string strCondition2 = strCondition[1];
                                            string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                            string strConditionOp = strCondition1.Replace("x", Meghdar.ToString().Trim());
                                            StringToFormula StringToFormula = new StringToFormula();
                                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                            if (blnCheck)
                                            {
                                                string strConditionOp2 = strCondition2.Replace("x", Meghdar2.ToString().Trim());

                                                StringToFormula StringToFormula2 = new StringToFormula();
                                                bool blnCheck2 = StringToFormula2.RelationalExpression2(strConditionOp2);
                                                if (blnCheck2)
                                                {


                                                    clsBaravordUser? varBA = _context.BaravordUsers.FirstOrDefault(x => x.ID == BarAvordId);

                                                    Guid guBAId = varBA.ID;
                                                    string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh);

                                                    Guid FBId = varFBUser.ID;


                                                    string strShomareh1 = strFBShomareh;
                                                    //Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                                    List<clsRizMetreUsers> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                                    if (RizMetre != null)
                                                    {
                                                        if (RizMetre.Count != 0)
                                                        {
                                                            _context.RizMetreUserses.RemoveRange(RizMetre);
                                                            _context.SaveChanges();
                                                        }
                                                    }


                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                long strItemsHasCondition_ConditionContextId = long.Parse(DtItemsHasConditionAddedToFB.Rows[0]["ItemsHasCondition_ConditionContextId"].ToString().Trim());

                var varOperation_ItemsFB = (from ItemsHasCondition in _context.ItemsHasConditions
                                            join ItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                            on ItemsHasCondition.Id equals ItemsHasCondition_ConditionContext.ItemsHasConditionId
                                            join ConditionContext in _context.ConditionContexts on ItemsHasCondition_ConditionContext.ConditionContextId equals ConditionContext.Id
                                            join ConditionGroup in _context.ConditionGroups on ConditionContext.ConditionGroupId equals ConditionGroup.Id
                                            select new
                                            {
                                                ID = ItemsHasCondition_ConditionContext.Id,
                                                ItemsHasConditionId = ItemsHasCondition.Id,
                                                ItemShomareh = ItemsHasCondition.ItemFBShomareh,
                                                HasEnteringValue = ItemsHasCondition_ConditionContext.HasEnteringValue,
                                                Context = ConditionContext.Context,
                                                Des = ItemsHasCondition_ConditionContext.Des,
                                                ConditionGroupName = ConditionGroup.ConditionGroupName,
                                                ConditionGroupId = ConditionGroup.Id,
                                                DefaultValue = ItemsHasCondition_ConditionContext.DefaultValue,
                                                IsShow = ItemsHasCondition_ConditionContext.IsShow,
                                                ParentId = ItemsHasCondition_ConditionContext.ParentId,
                                                MoveToRel = ItemsHasCondition_ConditionContext.MoveToRel,
                                                ViewCheckAllRecords = ItemsHasCondition_ConditionContext.ViewCheckAllRecords
                                            }).Where(x => x.ParentId == strItemsHasCondition_ConditionContextId).ToList();
                DataTable DtOperation_ItemsFB = clsConvert.ToDataTable(varOperation_ItemsFB);
                //DataTable DtOperation_ItemsFB = clsOperation_ItemsFB.ItemsHasConditionListWithParameter("ParentId=" + strItemsHasCondition_ConditionContextId);

                for (int i = 0; i < DtOperation_ItemsFB.Rows.Count; i++)
                {
                    DeleteItemsHasConditionAddedToFBAndRizMetreSubCondition(BarAvordId, DtOperation_ItemsFB.Rows[i]["ItemShomareh"].ToString().Trim(), long.Parse(DtOperation_ItemsFB.Rows[i]["ID"].ToString()));
                }

            }
            return new JsonResult("OK");
            //}
            //catch (Exception)
            //{
            //    return Json("NOK", JsonRequestBehavior.AllowGet);
            //}
        }

        public bool DeleteItemsHasConditionAddedToFBAndRizMetreSubCondition(Guid BarAvordId, string strFBShomareh, long Id)
        {
            try
            {
                var varItemsHasConditionAddedToFB = (from ItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
                                                     join ItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                                     on ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals ItemsHasCondition_ConditionContext.Id
                                                     join ItemsHasCondition in _context.ItemsHasConditions on ItemsHasCondition_ConditionContext.ItemsHasConditionId equals ItemsHasCondition.Id
                                                     select new
                                                     {
                                                         ID = ItemsHasConditionAddedToFB.ID,
                                                         FBShomareh = ItemsHasConditionAddedToFB.FBShomareh,
                                                         BarAvordId = ItemsHasConditionAddedToFB.BarAvordId,
                                                         Meghdar = ItemsHasConditionAddedToFB.Meghdar,
                                                         ItemsHasCondition_ConditionContextId = ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                         ConditionGroupId = ItemsHasConditionAddedToFB.ConditionGroupId,
                                                         ItemShomareh = ItemsHasCondition.ItemFBShomareh
                                                     }).Where(x => x.BarAvordId == BarAvordId && x.FBShomareh == strFBShomareh && x.ItemsHasCondition_ConditionContextId == Id).ToList();
                DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
                //DataTable DtItemsHasConditionAddedToFB = clsItemsHasConditionAddedToFB.ListWithParameterSimple("BarAvordId=" + intBarAvordId + " and FBShomareh='" + strFBShomareh + "' and ItemsHasCondition_ConditionContextId=" + Id);

                if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                {
                    //string strItemsHasCondition_ConditionContext = "";
                    //if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                    //{
                    //    strItemsHasCondition_ConditionContext += "ItemsHasCondition_ConditionContextId in(";
                    //    for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                    //    {
                    //        if ((i + 1) < DtItemsHasConditionAddedToFB.Rows.Count)
                    //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString() + ",";
                    //        else
                    //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString();
                    //    }
                    //    strItemsHasCondition_ConditionContext += ")";
                    //}
                    long[] strItemsHasCondition_ConditionContext = new long[] { };
                    if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                    {
                        for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                        {
                            strItemsHasCondition_ConditionContext[i] = long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());
                        }
                    }

                    bool blnChekIsDel = false;
                    try
                    {
                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = _context.ItemsHasConditionAddedToFBs.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh == strFBShomareh.Trim()).FirstOrDefault();
                        if (ItemsHasConditionAddedToFB != null)
                        {
                            _context.ItemsHasConditionAddedToFBs.Remove(ItemsHasConditionAddedToFB);
                            _context.SaveChanges();
                        }
                        blnChekIsDel = true;
                    }
                    catch (Exception)
                    {
                        blnChekIsDel = false;
                        //throw;
                    }


                    //if (clsItemsHasConditionAddedToFB.Delete("BarAvordId=" + intBarAvordId + " and FBShomareh='" + strFBShomareh + "'"))
                    if (blnChekIsDel)
                    {
                        var varItemsAddingToFB = _context.ItemsAddingToFBs.Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
                        DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);
                        //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasCondition_ConditionContext);
                        for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
                        {
                            decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
                            string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();
                            DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
                            if (Dr.Length != 0)
                            {
                                for (int idr = 0; idr < Dr.Length; idr++)
                                {
                                    string strFBShomarehAdded = Dr[idr]["AddedItems"].ToString().Trim();
                                    switch (Dr[idr]["ConditionType"].ToString())
                                    {
                                        case "1":
                                            {
                                                string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString().Trim();
                                                var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                                DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                                //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "A'");
                                                if (DtFBUsersAdded.Rows.Count != 0)
                                                {
                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    if (tblRizMetreUser.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                        _context.SaveChanges();
                                                    }
                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                }
                                                break;
                                            }
                                        case "2":
                                            {
                                                var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                                DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                                //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                                if (DtFBUsersAdded.Rows.Count != 0)
                                                {
                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    if (tblRizMetreUser.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                        _context.SaveChanges();
                                                    }
                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                }
                                                break;
                                            }
                                        case "3":
                                            {
                                                decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
                                                string strStatus = dPercent > 0 ? "B" : "e";
                                                var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strStatus).ToList();
                                                DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                                //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + strStatus + "'");
                                                if (DtFBUsersAdded.Rows.Count != 0)
                                                {
                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    if (tblRizMetreUser.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                        _context.SaveChanges();
                                                    }
                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                }
                                                break;
                                            }
                                        case "4":
                                            {
                                                var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                                DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                                //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                                if (DtFBUsersAdded.Rows.Count != 0)
                                                {
                                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                                    StringToFormula StringToFormula = new StringToFormula();
                                                    bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                                                    if (blnCheck)
                                                    {
                                                        string strForItem = "";
                                                        string strUseItem = "";
                                                        if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
                                                        {
                                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                            if (tblRizMetreUser.Count != 0)
                                                            {
                                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                                _context.SaveChanges();
                                                            }
                                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                        }
                                                        else
                                                        {
                                                            strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
                                                            strUseItem = strFBShomareh.Trim();
                                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strForItem && x.UseItem == strUseItem.Trim()).ToList();
                                                            if (tblRizMetreUser.Count != 0)
                                                            {
                                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                                _context.SaveChanges();
                                                            }
                                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strForItem + "' and UseItem='" + strUseItem + "'");
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        case "5":
                                            {
                                                var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                                DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                                //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                                if (DtFBUsersAdded.Rows.Count != 0)
                                                {
                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    if (tblRizMetreUser.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                        _context.SaveChanges();
                                                    }
                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                }
                                                break;
                                            }
                                        case "6":
                                            {
                                                var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                                DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                                //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                                if (DtFBUsersAdded.Rows.Count != 0)
                                                {
                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    if (tblRizMetreUser.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                        _context.SaveChanges();
                                                    }
                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                }
                                                break;
                                            }
                                        case "7":
                                            {
                                                var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                                DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                                //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                                if (DtFBUsersAdded.Rows.Count != 0)
                                                {
                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    if (tblRizMetreUser.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
                                                        _context.SaveChanges();
                                                    }
                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                }
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
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
}
