using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation
{
    public class ItemsAddingToFBController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: ItemsAddingToFB
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveItemsAddingToFB([FromBody] SaveItemsAddingToFBInputDto request)
        {
            string StrRBCode = request.strRBCode;
            long OperationId = request.OperationId;
            Guid BarAvordId = request.BarAvordUserId;
            long ConditionGroupId = request.ConditionGroupId;
            int LevelNumber = request.LevelNumber;
            NoeFehrestBaha NoeFB = request.NoeFB;

            clsBaravordUser? BaraAvordUser = _context.BaravordUsers.Where(x => x.ID == BarAvordId).FirstOrDefault();
            if (BaraAvordUser == null)
            {
                return new JsonResult("برآورد یافت نشد");
            }

            int Year = BaraAvordUser.Year;

            //string strParam = "";
            //strParam = "OperationId=" + OperationId;
            var varDt = _context.Operation_ItemsFBs.Where(x => x.OperationId == OperationId).ToList();
            DataTable Dt = clsConvert.ToDataTable(varDt);
            //DataTable Dt = clsOperation_ItemsFB.ListWithParameter(strParam);
            string strItemsFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            var varFB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemsFBShomareh).ToList();
            DataTable DtFB = clsConvert.ToDataTable(varFB);
            //DataTable DtFB = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'");
            string strFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            DataTable DtItemsAddingToFB = new DataTable();
            var varItemsHasConditionAddedToFB = (from clsItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
                                                 join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                                 on clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasCondition_ConditionContext.Id
                                                 join tblItemsHasCondition in _context.ItemsHasConditions on tblItemsHasCondition_ConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
                                                 where tblItemsHasCondition.Year == Year
                                                 select new
                                                 {
                                                     clsItemsHasConditionAddedToFB.ID,
                                                     clsItemsHasConditionAddedToFB.FBShomareh,
                                                     clsItemsHasConditionAddedToFB.BarAvordId,
                                                     clsItemsHasConditionAddedToFB.Meghdar,
                                                     clsItemsHasConditionAddedToFB.Meghdar2,
                                                     clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                     clsItemsHasConditionAddedToFB.ConditionGroupId,
                                                     ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh
                                                 }).Where(x => x.FBShomareh.Substring(0, 6) == strFBShomareh && x.BarAvordId == BarAvordId && x.ConditionGroupId == ConditionGroupId).ToList();
            DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);

            //List<ItemsHasConditionAddedToFBDto> varItemsHasConditionAddedToFB = _context.ItemsHasConditionAddedToFBs.Include(x => x.ItemsHasCondition_ConditionContext).ThenInclude(x => x.ConditionContext)
            //                                                                        .Include(x => x.ItemsHasCondition_ConditionContext).ThenInclude(x => x.ItemsHasCondition)
            //                                                                        .Select(x => new ItemsHasConditionAddedToFBDto
            //                                                                        {
            //                                                                            ID = x.ID,
            //                                                                            FBShomareh = x.FBShomareh,
            //                                                                            BarAvordId=x.BarAvordId,
            //                                                                            Meghdar=x.Meghdar,
            //                                                                            Meghdar2=x.Meghdar2,
            //                                                                            ItemsHasCondition_ConditionContextId=x.ItemsHasCondition_ConditionContextId,
            //                                                                            ConditionGroupId=x.ConditionGroupId,
            //                                                                            ItemFBShomareh=x.ItemsHasCondition_ConditionContext.ItemsHasCondition.ItemFBShomareh,
            //                                                                            ConditionContextId=x.ItemsHasCondition_ConditionContext.ConditionContextId,
            //                                                                        }).Where(x => x.FBShomareh.Substring(0, 6) == strFBShomareh && x.BarAvordId == BarAvordId && x.ConditionGroupId == ConditionGroupId).ToList();
            //DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);

            //DataTable DtItemsHasConditionAddedToFB = clsItemsHasConditionAddedToFB.ListWithParameterSimple("FbShomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "' and Baravordid=" + BarAvordId + " and ConditionGroup=" + ConditionGroupId);
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

                var varItemsAddingToFB1 = _context.ItemsAddingToFBs.Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
                DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB1);

                //DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasCondition_ConditionContext);
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
                            string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString().Trim();
                            switch (Dr[idr]["ConditionType"].ToString())
                            {
                                case "1":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strFBShomarehAdded + "A'");
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            //string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                            //strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            //StringToFormula StringToFormula = new StringToFormula();
                                            //decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            //clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                            //RizMetreUserses.ForItem = strFBShomareh.Trim();
                                            //RizMetreUserses.FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                                _context.FBs.Remove(Fb);
                                                _context.SaveChanges();
                                            }
                                            //Operation_ItemsFB.DeleteFBWithParameter("Id=" + int.Parse(DtFBUsersAdded.Rows[0]["Id"].ToString().Trim()));
                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            //clsRizMetreUserses RizMetreUserses = new clsRizMetreUserses();
                                            //RizMetreUserses.ForItem = strFBShomareh.Trim();
                                            //RizMetreUserses.FBId = int.Parse(DtFBUsersAdded.Rows[0]["Id"].ToString().Trim());
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            var RizMetre1 = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim() && x.LevelNumber == LevelNumber).ToList();
                                            if (RizMetre1 != null)
                                            {
                                                if (RizMetre1.Count != 0)
                                                {
                                                    _context.RizMetreUserses.RemoveRange(RizMetre1);
                                                    _context.SaveChanges();
                                                }
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

                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strFBShomarehAdded + strStatus + "'");
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            //clsRizMetreUserses RizMetreUserses = new clsRizMetreUserses();
                                            //RizMetreUserses.ForItem = strFBShomareh.Trim();
                                            //RizMetreUserses.FBId = int.Parse(DtFBUsersAdded.Rows[0]["Id"].ToString().Trim());
                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                                _context.FBs.Remove(Fb);
                                                _context.SaveChanges();
                                            }
                                            //Operation_ItemsFB.DeleteFBWithParameter("Id=" + int.Parse(DtFBUsersAdded.Rows[0]["Id"].ToString().Trim()));
                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                        }
                                        break;
                                    }
                                case "4":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                            string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                            string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                            StringToFormula StringToFormula = new StringToFormula();
                                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                            if (blnCheck)
                                            {
                                                string strForItem = "";
                                                string strUseItem = "";
                                                if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
                                                    strForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                else
                                                {
                                                    strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
                                                    strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                }

                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var RizMetre1 = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strForItem && x.UseItem == strUseItem && x.LevelNumber == LevelNumber).ToList();
                                                if (RizMetre1 != null)
                                                {
                                                    if (RizMetre1.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(RizMetre1);
                                                        _context.SaveChanges();
                                                    }
                                                }
                                                //clsRizMetreUserses RizMetreUserses = new clsRizMetreUserses();
                                                //RizMetreUserses.ForItem = strFBShomareh.Trim();
                                                //RizMetreUserses.FBId = int.Parse(DtFBUsersAdded.Rows[0]["Id"].ToString().Trim());
                                                //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strForItem + "' and UseItem='" + strUseItem + "'");
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
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim() && x.LevelNumber == LevelNumber).ExecuteDelete();
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
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim() && x.LevelNumber == LevelNumber).ExecuteDelete();
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
                                            //string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                            //strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            //StringToFormula StringToFormula = new StringToFormula();
                                            //decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
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
                                        string strCharacterPlus1 = Dr[idr]["CharacterPlus"].ToString();

                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomareh + strCharacterPlus1).ToList();
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
                                case "12":
                                    {
                                        string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                        string[] strConditionSplit = strCondition.Split("_");

                                        string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                        DataTable DtRizMetreUserses = new DataTable();
                                        string strForItem = "";
                                        string strUseItem = "";
                                        string strItemFBShomareh = DtFB.Rows[0]["Shomareh"].ToString().Trim();

                                        Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                        strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                        strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();

                                        List<clsRizMetreUsers> varRizMetreUsersesCurrent = _context.RizMetreUserses
                                            .Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();
                                        //DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);


                                        List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                            join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                            where RUsers.LevelNumber == LevelNumber
                                                                                                            select new RizMetreUsersForItemsAddingToFBInputDto
                                                                                                            {
                                                                                                                Shomareh = RUsers.Shomareh,
                                                                                                                Sharh = RUsers.Sharh,
                                                                                                                Tedad = RUsers.Tedad,
                                                                                                                Tool = RUsers.Tool,
                                                                                                                Arz = RUsers.Arz,
                                                                                                                Ertefa = RUsers.Ertefa,
                                                                                                                Vazn = RUsers.Vazn,
                                                                                                                Des = RUsers.Des,
                                                                                                                ForItem = RUsers.ForItem,
                                                                                                                Type = RUsers.Type,
                                                                                                                FBId = RUsers.FBId
                                                                                                            }).Where(x => x.FBId == guFBId && x.ForItem == strItemFBShomareh && x.Type == "1").OrderBy(x => x.Shomareh).ToList();

                                        List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId).ToList();
                                        foreach (var RM in varRizMetreUserses)
                                        {
                                            string strConditionOp = strConditionSplit[0].Replace("x", RM.Tool.ToString().Trim());
                                            string strConditionOp2 = strConditionSplit[1].Replace("z", RM.Ertefa.ToString().Trim());
                                            StringToFormula StringToFormula = new StringToFormula();
                                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                            if (blnCheck)
                                            {
                                                bool blnCheck2 = StringToFormula.RelationalExpression2(strConditionOp2);
                                                if (blnCheck2)
                                                {
                                                    //string strCurrentFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                                    //clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                    //     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                    //         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                                    //if (currentItemsHasConditionAddedToFBs != null)
                                                    //{
                                                    //    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                                    //    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                                    //    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                                    //    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                                    //    ItemsHasConditionAddedToFB.Meghdar = RM.Tool != null ? RM.Tool.Value : 0;
                                                    //    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                                    //    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                    //    _context.SaveChanges();
                                                    //}

                                                    if (strFinalWorking != "")
                                                    {
                                                        strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");
                                                        ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                                                        ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                                                        ///Ertefa-3
                                                        decimal Ertefa = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                                                        string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                        clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                                        Guid FBId = new Guid();
                                                        if (varFBUser != null)
                                                            FBId = varFBUser.ID;

                                                        List<clsRizMetreUsers> RizMetre1 = varRizMetreUsersesCurrent.Where(x => x.Shomareh == RM.Shomareh && x.FBId == FBId).ToList();
                                                        if (RizMetre1 != null)
                                                        {
                                                            if (RizMetre1.Count != 0)
                                                            {
                                                                _context.RizMetreUserses.RemoveRange(RizMetre1);
                                                                _context.SaveChanges();
                                                            }
                                                        }
                                                    }
                                                }

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


                                                string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                //Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                                List<clsRizMetreUsers> RizMetre1 = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                                if (RizMetre1 != null)
                                                {
                                                    if (RizMetre1.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(RizMetre1);
                                                        _context.SaveChanges();
                                                    }
                                                }


                                                break;
                                            }
                                        }
                                        break;
                                    }
                                case "14":
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

                                default:
                                    break;
                            }
                        }
                    }

                    Guid guItemsHasConditionAddedToFBId = Guid.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["ID"].ToString());
                    clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB = _context.ItemsHasConditionAddedToFBs.Where(x => x.ID == guItemsHasConditionAddedToFBId).FirstOrDefault();
                    if (ItemsHasConditionAddedToFB != null)
                    {
                        _context.ItemsHasConditionAddedToFBs.Remove(ItemsHasConditionAddedToFB);
                        _context.SaveChanges();
                    }
                    //clsItemsHasConditionAddedToFB.Delete("Id=" + DtItemsHasConditionAddedToFB.Rows[Counter]["Id"].ToString());
                }
            }

            string strErrors = "";
            string strWarning = "";
            //string strErrors4 = "";
            //string strErrors5 = "";
            string blnCheckIsExistErrors = "";
            bool blnCheckIsExistWarning = false;
            //bool blnCheckIsExistErrors2 = true;
            //bool blnCheckIsExistErrors3 = true;
            //bool blnCheckIsExistErrors4 = true;
            //bool blnCheckIsExistErrors5 = true;
            //var varItemsAddingToFB = _context.ItemsAddingToFBs.Where(x => x.Year == Year).ToList();

            List<ItemsAddingToFBEzafeBahaAndLakeGiriDto> varItemsAddingToFB = context.ItemsAddingToFBs
           .Include(x => x.ItemsHasCondition_ConditionContext).ThenInclude(x => x.ConditionContext)
           .Where(x => x.ItemsHasCondition_ConditionContext.Year == Year)
           .Select(x => new ItemsAddingToFBEzafeBahaAndLakeGiriDto
           {
               ID = x.Id,
               ItemsHasCondition_ConditionContextId = x.ItemsHasCondition_ConditionContextId,
               ConditionType = x.ConditionType,
               AddedItems = x.AddedItems,
               Condition = x.Condition,
               FinalWorking = x.FinalWorking,
               CharacterPlus = x.CharacterPlus,
               FieldsAdding = x.FieldsAdding,
               ConditionContextRel = x.ItemsHasCondition_ConditionContext.ConditionContext.ConditionContextRel,
               ConditionContextId = x.ItemsHasCondition_ConditionContext.ConditionContextId,
               UseItemForAdd = x.UseItemForAdd,
               DesOfAddingItems = x.DesOfAddingItems
           }).ToList();

            DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);

            DateTime Now = DateTime.Now;

            long ShomareNew = 1;
            clsRizMetreUsers? RizMetre = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x=>x.InsertDateTime).ThenByDescending(x=>x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordId);
            if (RizMetre != null)
            {
                long currentShomareNew = RizMetre.ShomarehNew == null || RizMetre.ShomarehNew.Trim() == "" ? 1 : long.Parse(RizMetre.ShomarehNew);
                if (currentShomareNew > RizMetre.Shomareh)
                {
                    ShomareNew = currentShomareNew;
                }
                else
                    ShomareNew = RizMetre.Shomareh;
            }
            //DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter("");
            string[] RBCodeArray = StrRBCode.Split(',');
            for (int counter = 0; counter < RBCodeArray.Length - 1; counter++)
            {
                string[] RBCodeArraySplit = RBCodeArray[counter].Split('_');
                int RBCode = int.Parse(RBCodeArraySplit[0].ToString());
                decimal Meghdar = decimal.Parse(RBCodeArraySplit.Length > 1 ? RBCodeArraySplit[1].ToString() != "" ? RBCodeArraySplit[1].ToString() : "0" : "0");
                if (Meghdar < 0)
                {
                    return new JsonResult("CI");//CheckInput
                }
                DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
                if (Dr.Length != 0)
                {
                    bool blnCheckAgain = true;

                    for (int idr = 0; idr < Dr.Length; idr++)
                    {
                        // DataTable DtItemsHasCondition = clsOperation_ItemsFB.ItemsHasConditionListWithParameter("tblItemsHasCondition_ConditionContext.Id=" + Dr[idr]["Id"].ToString());
                        switch (Dr[idr]["ConditionType"].ToString())
                        {

                            case "1":
                                {
                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());
                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                    string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString().Trim();

                                    StringToFormula StringToFormula = new StringToFormula();
                                    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    if (blnCheck)
                                    {
                                        blnCheckIsExistErrors = "false";
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        bool blnCheckSave = false;
                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            _context.SaveChanges();
                                            blnCheckSave = true;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = false;
                                        }

                                        //if (ItemsHasConditionAddedToFB.Save())
                                        if (blnCheckSave)
                                        {
                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            string strDesOfAddingItems = Dr[idr]["DesOfAddingItems"].ToString().Replace("x", Meghdar.ToString().Trim()).Replace("y", Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim()).Replace("z", (dPercent * 100).ToString("0.##")).Trim();
                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);
                                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus; //"A";
                                            var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                            //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "A'");
                                            Guid FBId = new Guid();
                                            if (DtFBUser.Rows.Count == 0)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus; //"A";
                                                FB.BahayeVahedZarib = dPercent;
                                                FB.BahayeVahedSharh = strDesOfAddingItems;
                                                _context.FBs.Add(FB);
                                                _context.SaveChanges();
                                                FBId = FB.ID;
                                                //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim() + "A", dPercent);
                                            }
                                            else
                                            {
                                                FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                            }

                                            var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                      join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                      where RUsers.LevelNumber == LevelNumber
                                                                      select new
                                                                      {
                                                                          RUsers.Shomareh,
                                                                          RUsers.Sharh,
                                                                          RUsers.Tedad,
                                                                          RUsers.Tool,
                                                                          RUsers.Arz,
                                                                          RUsers.Ertefa,
                                                                          RUsers.Vazn,
                                                                          RUsers.Des,
                                                                          RUsers.ForItem,
                                                                          RUsers.Type,
                                                                          RUsers.UseItem,
                                                                          fb.BarAvordId,
                                                                          RUsers.FBId
                                                                      }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                            DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);
                                            //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");

                                            string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                             where RizMetreUserses.LevelNumber == LevelNumber
                                                                             select new
                                                                             {
                                                                                 ID = RizMetreUserses.ID,
                                                                                 Shomareh = RizMetreUserses.Shomareh,
                                                                                 Tedad = RizMetreUserses.Tedad,
                                                                                 Tool = RizMetreUserses.Tool,
                                                                                 Arz = RizMetreUserses.Arz,
                                                                                 Ertefa = RizMetreUserses.Ertefa,
                                                                                 Vazn = RizMetreUserses.Vazn,
                                                                                 Des = RizMetreUserses.Des,
                                                                                 FBId = RizMetreUserses.FBId,
                                                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                 ForItem = RizMetreUserses.ForItem,
                                                                                 Type = RizMetreUserses.Type,
                                                                                 UseItem = RizMetreUserses.UseItem,
                                                                                 BarAvordId = FB.BarAvordId
                                                                             }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                            DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                            //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                            for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                            {
                                                DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                if (DrRizMetreUsersesCurrent.Length == 0)
                                                {
                                                    decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                    decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                    decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                    decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                    decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                    RizMetreUserses.Tedad = Tedad;
                                                    RizMetreUserses.Tool = Tool;
                                                    RizMetreUserses.Arz = Arz;
                                                    RizMetreUserses.Ertefa = Ertefa;
                                                    RizMetreUserses.Vazn = Vazn;
                                                    RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim();// Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                             //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                    RizMetreUserses.UseItem = "";
                                                    RizMetreUserses.LevelNumber = LevelNumber;
                                                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses.ConditionContextId = ConditionContextId;
                                                    RizMetreUserses.InsertDateTime = Now;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz = null;
                                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                        (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;


                                                    _context.RizMetreUserses.Add(RizMetreUserses);
                                                    _context.SaveChanges();
                                                    //RizMetreUserses.Save();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //if (blnCheckIsExistErrors != "false")
                                        //{
                                        //    blnCheckIsExistErrors = "true";
                                        //    strErrors = strCondition.Replace("x", "عدد وارد شده");
                                        //}

                                        clsFB? varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus).FirstOrDefault();
                                        if (varFBUsersAdded != null)
                                        {
                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                                _context.FBs.Remove(Fb);
                                                _context.SaveChanges();
                                            }
                                        }
                                        return new JsonResult("CI");//CheckInput
                                    }
                                    break;
                                }
                            case "2":
                                {
                                    string FieldsAdding = Dr[idr]["FieldsAdding"].ToString();

                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                    ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;

                                    bool blnCheckSave = false;
                                    try
                                    {
                                        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                        _context.SaveChanges();
                                        blnCheckSave = true;
                                    }
                                    catch (Exception)
                                    {
                                        blnCheckSave = false;
                                    }
                                    //if (ItemsHasConditionAddedToFB.Save())
                                    if (blnCheckSave)
                                    {
                                        //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                        var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();

                                        ///در صورتی که خالی نباشد در  جای مشخص شده درج مینمایم
                                        ///x= طول
                                        ///y= عرض
                                        ///z= ارتفاع
                                        string strFinalWorking = Dr[idr]["FinalWorking"].ToString().Trim();

                                        DataTable DtBA = clsConvert.ToDataTable(varBA);

                                        //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                        Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                        var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                        DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                        //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + "'");
                                        Guid FBId = new Guid();
                                        if (DtFBUser.Rows.Count == 0)
                                        {
                                            clsFB FB = new clsFB();
                                            FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                            FB.BahayeVahedZarib = 0;
                                            FB.BahayeVahedSharh = "";
                                            _context.FBs.Add(FB);
                                            _context.SaveChanges();
                                            FBId = FB.ID;

                                            //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[0]["AddedItems"].ToString().Trim(), 0);
                                        }
                                        else
                                        {
                                            FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                        }



                                        var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                  where RUsers.LevelNumber == LevelNumber
                                                                  select new
                                                                  {
                                                                      Shomareh = RUsers.Shomareh,
                                                                      Sharh = RUsers.Sharh,
                                                                      Tedad = RUsers.Tedad,
                                                                      Tool = RUsers.Tool,
                                                                      Arz = RUsers.Arz,
                                                                      Ertefa = RUsers.Ertefa,
                                                                      Vazn = RUsers.Vazn,
                                                                      Des = RUsers.Des,
                                                                      ForItem = RUsers.ForItem,
                                                                      Type = RUsers.Type,
                                                                      UseItem = RUsers.UseItem,
                                                                      BarAvordId = fb.BarAvordId,
                                                                      FBId = RUsers.FBId
                                                                  }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                        DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                        string[] FieldsAddingSplit = FieldsAdding.Split(",");

                                        string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                        Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                        var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                         join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                         where RizMetreUserses.LevelNumber == LevelNumber
                                                                         select new
                                                                         {
                                                                             ID = RizMetreUserses.ID,
                                                                             Shomareh = RizMetreUserses.Shomareh,
                                                                             Tedad = RizMetreUserses.Tedad,
                                                                             Tool = RizMetreUserses.Tool,
                                                                             Arz = RizMetreUserses.Arz,
                                                                             Ertefa = RizMetreUserses.Ertefa,
                                                                             Vazn = RizMetreUserses.Vazn,
                                                                             Des = RizMetreUserses.Des,
                                                                             FBId = RizMetreUserses.FBId,
                                                                             OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                             ForItem = RizMetreUserses.ForItem,
                                                                             Type = RizMetreUserses.Type,
                                                                             UseItem = RizMetreUserses.UseItem,
                                                                             BarAvordId = FB.BarAvordId
                                                                         }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                        DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                        //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                        //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                        for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                        {
                                            DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                            if (DrRizMetreUsersesCurrent.Length == 0)
                                            {
                                                decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                                List<string> lst = new List<string>();
                                                for (int j = 0; j < FieldsAddingSplit.Length; j++)
                                                {
                                                    lst.Add(FieldsAddingSplit[j]);
                                                }

                                                decimal? dTedad = null;
                                                decimal? dTool = null;
                                                decimal? dArz = null;
                                                decimal? dErtefa = null;
                                                decimal? dVazn = null;


                                                if (strFinalWorking != "")
                                                {
                                                    string[] FinalWorkingSplit = strFinalWorking.Split("=");

                                                    switch (FinalWorkingSplit[0])
                                                    {
                                                        case "x":
                                                            {
                                                                Tool = decimal.Parse(FinalWorkingSplit[1]);
                                                                break;
                                                            }
                                                        case "y":
                                                            {
                                                                Arz = decimal.Parse(FinalWorkingSplit[1]);
                                                                break;
                                                            }
                                                        case "z":
                                                            {
                                                                Ertefa = decimal.Parse(FinalWorkingSplit[1]);
                                                                break;
                                                            }
                                                        default:
                                                            {
                                                                break;
                                                            }
                                                    }
                                                }
                                                var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    dTedad = Tedad;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    dTool = Tool;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    dArz = Arz;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    dErtefa = Ertefa;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    dVazn = Vazn;
                                                }

                                                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                ShomareNew++;
                                                RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();

                                                RizMetreUserses.Tedad = dTedad;
                                                RizMetreUserses.Tool = dTool;
                                                RizMetreUserses.Arz = dArz;
                                                RizMetreUserses.Ertefa = dErtefa;
                                                RizMetreUserses.Vazn = dVazn;

                                                RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                RizMetreUserses.FBId = FBId;
                                                RizMetreUserses.OperationsOfHamlId = 1;
                                                RizMetreUserses.Type = "2";
                                                RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                RizMetreUserses.UseItem = "";
                                                RizMetreUserses.LevelNumber = LevelNumber;
                                                RizMetreUserses.InsertDateTime = Now;

                                                ///محاسبه مقدار جزء
                                                decimal? dMeghdarJoz = null;
                                                if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                                    dMeghdarJoz = null;
                                                else
                                                    dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                    (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                                RizMetreUserses.MeghdarJoz = dMeghdarJoz;
                                                RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                RizMetreUserses.ConditionContextId = ConditionContextId;

                                                _context.RizMetreUserses.Add(RizMetreUserses);
                                                _context.SaveChanges();
                                                //RizMetreUserses.Save();
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "3":
                                {

                                    ///در این حالت بایستی آیتم اصلی برگشت شده و ضریب ارسالی در بهای واحد ضرب شود
                                    ///و عدد بدست آمده در ضریب بهای واحد قرار گیرد
                                    ///
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string ItemsFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = ItemsFBShomareh;
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                    ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                    bool blnCheckSave = false;
                                    try
                                    {
                                        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                        _context.SaveChanges();
                                        blnCheckSave = true;
                                    }
                                    catch (Exception)
                                    {
                                        blnCheckSave = true;
                                    }

                                    decimal NewBahayeVahed = 0;

                                    //if (ItemsHasConditionAddedToFB.Save())
                                    if (blnCheckSave)
                                    {
                                        decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
                                        clsFehrestBaha? FehrestBahasItem = _context.FehrestBahas.Where(x => x.Sal == Year && x.Shomareh == ItemsFBShomareh).FirstOrDefault();
                                        if (FehrestBahasItem != null)
                                        {
                                            string aaaa = FehrestBahasItem.BahayeVahed == null ? "0" : FehrestBahasItem.BahayeVahed;
                                            NewBahayeVahed = decimal.Parse(aaaa) * dPercent;
                                        }
                                        var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                        DataTable DtBA = clsConvert.ToDataTable(varBA);


                                        //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                        //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                        //string strStatus = dPercent > 0 ? "B" : "e";
                                        Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();// + strStatus;
                                        var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                        DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
                                        //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + strStatus + "'");
                                        Guid FBId = new Guid();
                                        if (DtFBUser.Rows.Count == 0)
                                        {
                                            clsFB FB = new clsFB();
                                            FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            FB.Shomareh = strItemShomareh;// Dr[idr]["AddedItems"].ToString().Trim(); //+ strStatus;
                                            FB.BahayeVahedZarib = NewBahayeVahed;
                                            _context.FBs.Add(FB);
                                            _context.SaveChanges();
                                            FBId = FB.ID;

                                            //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[0]["AddedItems"].ToString().Trim() + strStatus, dPercent);
                                        }
                                        else
                                        {
                                            FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                        }

                                        var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                  where RUsers.LevelNumber == LevelNumber
                                                                  select new
                                                                  {
                                                                      Shomareh = RUsers.Shomareh,
                                                                      Sharh = RUsers.Sharh,
                                                                      Tedad = RUsers.Tedad,
                                                                      Tool = RUsers.Tool,
                                                                      Arz = RUsers.Arz,
                                                                      Ertefa = RUsers.Ertefa,
                                                                      Vazn = RUsers.Vazn,
                                                                      Des = RUsers.Des,
                                                                      ForItem = RUsers.ForItem,
                                                                      Type = RUsers.Type,
                                                                      UseItem = RUsers.UseItem,
                                                                      BarAvordId = fb.BarAvordId,
                                                                      FBId = RUsers.FBId
                                                                  }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                        DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                        string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                        Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                        var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                         join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                         where RizMetreUserses.LevelNumber == LevelNumber
                                                                         select new
                                                                         {
                                                                             ID = RizMetreUserses.ID,
                                                                             Shomareh = RizMetreUserses.Shomareh,
                                                                             Tedad = RizMetreUserses.Tedad,
                                                                             Tool = RizMetreUserses.Tool,
                                                                             Arz = RizMetreUserses.Arz,
                                                                             Ertefa = RizMetreUserses.Ertefa,
                                                                             Vazn = RizMetreUserses.Vazn,
                                                                             Des = RizMetreUserses.Des,
                                                                             FBId = RizMetreUserses.FBId,
                                                                             OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                             ForItem = RizMetreUserses.ForItem,
                                                                             Type = RizMetreUserses.Type,
                                                                             UseItem = RizMetreUserses.UseItem,
                                                                             BarAvordId = FB.BarAvordId
                                                                         }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                        DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                        //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                        //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                        for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                        {
                                            DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                            if (DrRizMetreUsersesCurrent.Length == 0)
                                            {

                                                decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                ShomareNew++;
                                                RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                RizMetreUserses.Tedad = Tedad;
                                                RizMetreUserses.Tool = Tool;
                                                RizMetreUserses.Arz = Arz;
                                                RizMetreUserses.Ertefa = Ertefa;
                                                RizMetreUserses.Vazn = Vazn;
                                                RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                RizMetreUserses.FBId = FBId;
                                                RizMetreUserses.OperationsOfHamlId = 1;
                                                RizMetreUserses.Type = "2";
                                                RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                RizMetreUserses.UseItem = "";
                                                RizMetreUserses.LevelNumber = LevelNumber;
                                                RizMetreUserses.InsertDateTime = Now;

                                                ///محاسبه مقدار جزء
                                                decimal? dMeghdarJoz = null;
                                                if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                                                    dMeghdarJoz = null;
                                                else
                                                    dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                    (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                RizMetreUserses.ConditionContextId = ConditionContextId;

                                                _context.RizMetreUserses.Add(RizMetreUserses);
                                                _context.SaveChanges();
                                                //RizMetreUserses.Save();
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "4":
                                {
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                    StringToFormula StringToFormula = new StringToFormula();
                                    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    if (blnCheck)
                                    {
                                        DataTable DtRizMetreUserses = new DataTable();
                                        string strForItem = "";
                                        string strUseItem = "";
                                        string strItemFBShomareh = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                        if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
                                        {
                                            strForItem = strItemFBShomareh;
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                      join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                      where RUsers.LevelNumber == LevelNumber
                                                                      select new
                                                                      {
                                                                          RUsers.ID,
                                                                          RUsers.Shomareh,
                                                                          RUsers.Sharh,
                                                                          RUsers.Tedad,
                                                                          RUsers.Tool,
                                                                          RUsers.Arz,
                                                                          RUsers.Ertefa,
                                                                          RUsers.Vazn,
                                                                          RUsers.Des,
                                                                          RUsers.ForItem,
                                                                          RUsers.Type,
                                                                          RUsers.UseItem,
                                                                          fb.BarAvordId,
                                                                          RUsers.FBId
                                                                      }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                            DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);
                                            //DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                        }
                                        else
                                        {
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
                                            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strForItem).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
                                            //DtFB = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strForItem + "'");
                                            if (DtFB.Rows.Count != 0)
                                            {
                                                var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                          join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                          where RUsers.LevelNumber == LevelNumber
                                                                          select new
                                                                          {
                                                                              RUsers.ID,
                                                                              RUsers.Shomareh,
                                                                              RUsers.Sharh,
                                                                              RUsers.Tedad,
                                                                              RUsers.Tool,
                                                                              RUsers.Arz,
                                                                              RUsers.Ertefa,
                                                                              RUsers.Vazn,
                                                                              RUsers.Des,
                                                                              RUsers.ForItem,
                                                                              RUsers.Type,
                                                                              RUsers.UseItem,
                                                                              fb.BarAvordId,
                                                                              RUsers.FBId
                                                                          }).Where(x => x.FBId == guFBId && x.ForItem == strItemFBShomareh && x.Type == "4").OrderBy(x => x.Shomareh).ToList();
                                                DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                                //DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and ForItem='" + strItemFBShomareh + "' and Type=4");
                                            }
                                        }

                                        long lngItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                        string strCurrentFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        blnCheckIsExistErrors = "false";
                                        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                        bool blnCheckSave = false;
                                        if (currentItemsHasConditionAddedToFBs == null)
                                        {
                                            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                            ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                            ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                            ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                            ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;

                                            try
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                _context.SaveChanges();
                                                blnCheckSave = true;
                                            }
                                            catch (Exception)
                                            {
                                                blnCheckSave = false;
                                            }
                                        }
                                        if (blnCheckSave)
                                        //if (ItemsHasConditionAddedToFB.Save())
                                        {
                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            decimal dMultiple = 0;
                                            if (strFinalWorking != "")
                                            {
                                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            }

                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);

                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                            var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                            //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");

                                            Guid FBId = new Guid();
                                            if (DtFBUser.Rows.Count == 0)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                FB.BahayeVahedZarib = dMultiple;
                                                _context.FBs.Add(FB);
                                                _context.SaveChanges();
                                                FBId = FB.ID;

                                                //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
                                            }
                                            else
                                                FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                            string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                             where RizMetreUserses.LevelNumber == LevelNumber
                                                                             select new
                                                                             {
                                                                                 ID = RizMetreUserses.ID,
                                                                                 Shomareh = RizMetreUserses.Shomareh,
                                                                                 Tedad = RizMetreUserses.Tedad,
                                                                                 Tool = RizMetreUserses.Tool,
                                                                                 Arz = RizMetreUserses.Arz,
                                                                                 Ertefa = RizMetreUserses.Ertefa,
                                                                                 Vazn = RizMetreUserses.Vazn,
                                                                                 Des = RizMetreUserses.Des,
                                                                                 FBId = RizMetreUserses.FBId,
                                                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                 ForItem = RizMetreUserses.ForItem,
                                                                                 Type = RizMetreUserses.Type,
                                                                                 UseItem = RizMetreUserses.UseItem,
                                                                                 BarAvordId = FB.BarAvordId
                                                                             }).Where(x => x.FBId == FBId && x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();
                                            DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                            //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + strForItem + "' and Type=2 and UseItem='" + strUseItem + "'");
                                            for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                            {
                                                DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                if (DrRizMetreUsersesCurrent.Length == 0)
                                                {
                                                    decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString()) * dMultiple;
                                                    decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                    decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                    decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                    decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                    RizMetreUserses.Tedad = Tedad;
                                                    RizMetreUserses.Tool = Tool;
                                                    RizMetreUserses.Arz = Arz;
                                                    RizMetreUserses.Ertefa = Ertefa;
                                                    RizMetreUserses.Vazn = Vazn;
                                                    RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + strForItem
                                                                                                                              //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();

                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = strForItem;
                                                    RizMetreUserses.UseItem = strUseItem;
                                                    RizMetreUserses.LevelNumber = LevelNumber;
                                                    RizMetreUserses.InsertDateTime = Now;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz = null;
                                                    if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                        (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses.ConditionContextId = ConditionContextId;

                                                    _context.RizMetreUserses.Add(RizMetreUserses);
                                                    _context.SaveChanges();
                                                    //RizMetreUserses.Save();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (blnCheckIsExistErrors != "false")
                                        {
                                            blnCheckIsExistErrors = "true";
                                            //strErrors += "<div class=\"col-md-12\"><span class=\"spanStyleMitraMedium\">" + Dr[idr]["DesOfAddingItems"].ToString() + "</span></div>";
                                            strErrors = strCondition.Replace("x", "عدد وارد شده");
                                        }
                                    }
                                    break;
                                }
                            case "5":
                                {
                                    string FieldsAdding = Dr[idr]["FieldsAdding"].ToString().Trim() == "" ? null : Dr[idr]["FieldsAdding"].ToString().Trim();
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                    StringToFormula StringToFormula = new StringToFormula();
                                    bool blnCheck = true;
                                    if (strConditionOp != "")
                                    {
                                        blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    }
                                    if (blnCheck)
                                    {
                                        blnCheckIsExistErrors = "false";
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        bool blnCheckSave = false;
                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            _context.SaveChanges();
                                            blnCheckSave = true;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = false;
                                        }
                                        //if (ItemsHasConditionAddedToFB.Save())
                                        if (blnCheckSave)
                                        {
                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            decimal dMultiple = 0;
                                            if (strFinalWorking != "")
                                            {
                                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            }

                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);


                                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                            //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                            var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
                                            Guid FBId = new Guid();
                                            if (DtFBUser.Rows.Count == 0)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                FB.BahayeVahedZarib = 0;// dMultiple;
                                                _context.FBs.Add(FB);
                                                _context.SaveChanges();
                                                FBId = FB.ID;

                                                //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
                                            }
                                            else
                                                FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                            var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                      join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                      where RUsers.LevelNumber == LevelNumber
                                                                      select new
                                                                      {
                                                                          Shomareh = RUsers.Shomareh,
                                                                          Sharh = RUsers.Sharh,
                                                                          Tedad = RUsers.Tedad,
                                                                          Tool = RUsers.Tool,
                                                                          Arz = RUsers.Arz,
                                                                          Ertefa = RUsers.Ertefa,
                                                                          Vazn = RUsers.Vazn,
                                                                          Des = RUsers.Des,
                                                                          ForItem = RUsers.ForItem,
                                                                          Type = RUsers.Type,
                                                                          UseItem = RUsers.UseItem,
                                                                          BarAvordId = fb.BarAvordId,
                                                                          FBId = RUsers.FBId
                                                                      }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                            DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                            string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                             where RizMetreUserses.LevelNumber == LevelNumber
                                                                             select new
                                                                             {
                                                                                 ID = RizMetreUserses.ID,
                                                                                 Shomareh = RizMetreUserses.Shomareh,
                                                                                 Tedad = RizMetreUserses.Tedad,
                                                                                 Tool = RizMetreUserses.Tool,
                                                                                 Arz = RizMetreUserses.Arz,
                                                                                 Ertefa = RizMetreUserses.Ertefa,
                                                                                 Vazn = RizMetreUserses.Vazn,
                                                                                 Des = RizMetreUserses.Des,
                                                                                 FBId = RizMetreUserses.FBId,
                                                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                 ForItem = RizMetreUserses.ForItem,
                                                                                 Type = RizMetreUserses.Type,
                                                                                 UseItem = RizMetreUserses.UseItem,
                                                                                 BarAvordId = FB.BarAvordId
                                                                             }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                            DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                            //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                            //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                            for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                            {
                                                DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                if (DrRizMetreUsersesCurrent.Length == 0)
                                                {
                                                    decimal? dTedad = null;
                                                    decimal? dTool = null;
                                                    decimal? dArz = null;
                                                    decimal? dErtefa = null;

                                                    string[] strFieldsAdding = FieldsAdding.Split(",");
                                                    List<string> lst = new List<string>();
                                                    for (int j = 0; j < strFieldsAdding.Length; j++)
                                                    {
                                                        lst.Add(strFieldsAdding[j]);
                                                    }

                                                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        dTedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        dTool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        dArz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        dErtefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                    }

                                                    decimal? Vazn = dMultiple; //DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                    RizMetreUserses.Tedad = dTedad;
                                                    RizMetreUserses.Tool = dTool;
                                                    RizMetreUserses.Arz = dArz;
                                                    RizMetreUserses.Ertefa = dErtefa;
                                                    RizMetreUserses.Vazn = Vazn;
                                                    RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                              //                               + " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();

                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                    RizMetreUserses.UseItem = "";
                                                    RizMetreUserses.LevelNumber = LevelNumber;
                                                    RizMetreUserses.InsertDateTime = Now;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz = null;
                                                    if (dTedad == null && dTool == null && dArz == null && dErtefa == null && Vazn == null)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;


                                                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses.ConditionContextId = ConditionContextId;

                                                    _context.RizMetreUserses.Add(RizMetreUserses);
                                                    _context.SaveChanges();
                                                    //RizMetreUserses.Save();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return new JsonResult("CI");//CheckInput


                                        //if (blnCheckIsExistErrors != "false")
                                        //{
                                        //    blnCheckIsExistErrors = "true";
                                        //    //strErrors += "<div class=\"col-md-12\"><span class=\"spanStyleMitraMedium\">" + Dr[idr]["DesOfAddingItems"].ToString() + "</span></div>";
                                        //    strErrors = strCondition.Replace("x", "عدد وارد شده");
                                        //}
                                    }
                                    break;
                                }
                            case "6":
                                {
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    StringToFormula StringToFormula = new StringToFormula();
                                    blnCheckIsExistErrors = "false";

                                    string strCurrentFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                    long lngItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());

                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                        ItemsHasConditionAddedToFB.Meghdar = 0;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                        _context.SaveChanges();
                                    }

                                    //if (DtItemsHasConditionAddedToFB.Rows.Count == 0)
                                    //{
                                    //    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    //    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    //    ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                    //    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                    //    ItemsHasConditionAddedToFB.Meghdar = 0;
                                    //    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                    //    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    //    _context.SaveChanges();
                                    //    //ItemsHasConditionAddedToFB.Save();
                                    //}
                                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                    var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                    DataTable DtBA = clsConvert.ToDataTable(varBA);

                                    Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                    //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                    //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
                                    Guid FBId = new Guid();
                                    if (DtFBUser.Rows.Count == 0)
                                    {
                                        clsFB FB = new clsFB();
                                        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                        FB.BahayeVahedZarib = 0;
                                        _context.FBs.Add(FB);
                                        _context.SaveChanges();
                                        FBId = FB.ID;

                                        //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
                                    }
                                    else
                                        FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                                    var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                              join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                              where RUsers.LevelNumber == LevelNumber
                                                              select new
                                                              {
                                                                  Shomareh = RUsers.Shomareh,
                                                                  Sharh = RUsers.Sharh,
                                                                  Tedad = RUsers.Tedad,
                                                                  Tool = RUsers.Tool,
                                                                  Arz = RUsers.Arz,
                                                                  Ertefa = RUsers.Ertefa,
                                                                  Vazn = RUsers.Vazn,
                                                                  Des = RUsers.Des,
                                                                  ForItem = RUsers.ForItem,
                                                                  Type = RUsers.Type,
                                                                  UseItem = RUsers.UseItem,
                                                                  BarAvordId = fb.BarAvordId,
                                                                  FBId = RUsers.FBId
                                                              }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                    DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                    string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                    Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                    var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                     join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                     where RizMetreUserses.LevelNumber == LevelNumber
                                                                     select new
                                                                     {
                                                                         ID = RizMetreUserses.ID,
                                                                         Shomareh = RizMetreUserses.Shomareh,
                                                                         Tedad = RizMetreUserses.Tedad,
                                                                         Tool = RizMetreUserses.Tool,
                                                                         Arz = RizMetreUserses.Arz,
                                                                         Ertefa = RizMetreUserses.Ertefa,
                                                                         Vazn = RizMetreUserses.Vazn,
                                                                         Des = RizMetreUserses.Des,
                                                                         FBId = RizMetreUserses.FBId,
                                                                         OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                         ForItem = RizMetreUserses.ForItem,
                                                                         Type = RizMetreUserses.Type,
                                                                         UseItem = RizMetreUserses.UseItem,
                                                                         BarAvordId = FB.BarAvordId
                                                                     }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                    //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                    //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                    for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                    {
                                        string strConditionOp = strCondition.Replace("z", DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim());
                                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        if (blnCheck)
                                        {
                                            DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                            if (DrRizMetreUsersesCurrent.Length == 0)
                                            {
                                                decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());

                                                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                ShomareNew++;
                                                RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                RizMetreUserses.Tedad = Tedad;
                                                RizMetreUserses.Tool = Tool;
                                                RizMetreUserses.Arz = Arz;
                                                RizMetreUserses.Ertefa = Ertefa;
                                                RizMetreUserses.Vazn = Vazn;
                                                RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                RizMetreUserses.FBId = FBId;
                                                RizMetreUserses.OperationsOfHamlId = 1;
                                                RizMetreUserses.Type = "2";
                                                RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                RizMetreUserses.UseItem = "";
                                                RizMetreUserses.LevelNumber = LevelNumber;
                                                RizMetreUserses.InsertDateTime = Now;

                                                ///محاسبه مقدار جزء
                                                decimal? dMeghdarJoz = null;
                                                if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                                                    dMeghdarJoz = null;
                                                else
                                                    dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                    (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                RizMetreUserses.ConditionContextId = ConditionContextId;

                                                _context.RizMetreUserses.Add(RizMetreUserses);
                                                _context.SaveChanges();
                                                //RizMetreUserses.Save();
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "8":
                                {
                                    blnCheckIsExistErrors = "false";
                                    if (DtItemsHasConditionAddedToFB.Rows.Count == 0)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                        ItemsHasConditionAddedToFB.Meghdar = 0;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                        _context.SaveChanges();
                                        //ItemsHasConditionAddedToFB.Save();
                                    }

                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    StringToFormula StringToFormula = new StringToFormula();
                                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                    var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                    DataTable DtBA = clsConvert.ToDataTable(varBA);

                                    Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                    var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                    //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                    //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
                                    Guid intFBId = new Guid();
                                    if (DtFBUser.Rows.Count == 0)
                                    {
                                        clsFB FB = new clsFB();
                                        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                        FB.BahayeVahedZarib = 0;
                                        _context.FBs.Add(FB);
                                        _context.SaveChanges();
                                        intFBId = FB.ID;

                                        //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
                                    }
                                    else
                                        intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                    Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());

                                    var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                              join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                              where RUsers.LevelNumber == LevelNumber
                                                              select new
                                                              {
                                                                  Shomareh = RUsers.Shomareh,
                                                                  Sharh = RUsers.Sharh,
                                                                  Tedad = RUsers.Tedad,
                                                                  Tool = RUsers.Tool,
                                                                  Arz = RUsers.Arz,
                                                                  Ertefa = RUsers.Ertefa,
                                                                  Vazn = RUsers.Vazn,
                                                                  Des = RUsers.Des,
                                                                  ForItem = RUsers.ForItem,
                                                                  Type = RUsers.Type,
                                                                  UseItem = RUsers.UseItem,
                                                                  BarAvordId = fb.BarAvordId,
                                                                  FBId = RUsers.FBId
                                                              }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                    DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                    string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                    var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                     join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                     where RizMetreUserses.LevelNumber == LevelNumber
                                                                     select new
                                                                     {
                                                                         ID = RizMetreUserses.ID,
                                                                         Shomareh = RizMetreUserses.Shomareh,
                                                                         Tedad = RizMetreUserses.Tedad,
                                                                         Tool = RizMetreUserses.Tool,
                                                                         Arz = RizMetreUserses.Arz,
                                                                         Ertefa = RizMetreUserses.Ertefa,
                                                                         Vazn = RizMetreUserses.Vazn,
                                                                         Des = RizMetreUserses.Des,
                                                                         FBId = RizMetreUserses.FBId,
                                                                         OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                         ForItem = RizMetreUserses.ForItem,
                                                                         Type = RizMetreUserses.Type,
                                                                         UseItem = RizMetreUserses.UseItem,
                                                                         BarAvordId = FB.BarAvordId
                                                                     }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                    //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                    //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                    for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                    {
                                        string strConditionOp = strCondition.Replace("x", DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim());
                                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        if (blnCheck)
                                        {
                                            decimal ArzEzafi = 1;
                                            string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                            if (strFinalWorking != "")
                                            {
                                                strFinalWorking = strFinalWorking.Replace("x", DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim());
                                                ArzEzafi = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            }

                                            DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                            if (DrRizMetreUsersesCurrent.Length == 0)
                                            {
                                                decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                ShomareNew++;
                                                RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                RizMetreUserses.Tedad = Tedad;
                                                RizMetreUserses.Tool = Tool;
                                                RizMetreUserses.Arz = Arz;
                                                RizMetreUserses.Ertefa = Ertefa;
                                                RizMetreUserses.Vazn = Vazn;
                                                RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                RizMetreUserses.FBId = intFBId;
                                                RizMetreUserses.OperationsOfHamlId = 1;
                                                RizMetreUserses.Type = "2";
                                                RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                RizMetreUserses.UseItem = "";
                                                RizMetreUserses.LevelNumber = LevelNumber;
                                                RizMetreUserses.InsertDateTime = Now;

                                                RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                RizMetreUserses.ConditionContextId = ConditionContextId;

                                                ///محاسبه مقدار جزء
                                                decimal? dMeghdarJoz = null;
                                                if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                                                    dMeghdarJoz = null;
                                                else
                                                    dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                    (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                _context.RizMetreUserses.Add(RizMetreUserses);
                                                _context.SaveChanges();
                                                //RizMetreUserses.Save();
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "10":
                                {
                                    if (blnCheckAgain)
                                    {
                                        string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                        string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                        string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());

                                        string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                        long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                        StringToFormula StringToFormula = new StringToFormula();
                                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        if (blnCheck)
                                        {
                                            blnCheckAgain = false;

                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            decimal dZarib = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                                            if (dZarib == 0)
                                            {
                                                clsFB? varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == Dr[idr]["AddedItems"].ToString().Trim()).FirstOrDefault();
                                                if (varFBUsersAdded != null)
                                                {
                                                    Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                                    clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                    if (Fb != null)
                                                    {
                                                        _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                                        _context.FBs.Remove(Fb);
                                                        _context.SaveChanges();
                                                    }
                                                }
                                                return new JsonResult("CI");//CheckInput
                                            }
                                            else
                                            {
                                                blnCheckIsExistErrors = "false";
                                                clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                                ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                                ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                                ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                                ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                                bool blnCheckSave = false;
                                                try
                                                {
                                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                    _context.SaveChanges();
                                                    blnCheckSave = true;
                                                }
                                                catch (Exception)
                                                {
                                                    blnCheckSave = false;
                                                }

                                                //if (ItemsHasConditionAddedToFB.Save())
                                                if (blnCheckSave)
                                                {

                                                    dZarib = dZarib < 0 ? dZarib * -1 : dZarib;
                                                    var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                                    DataTable DtBA = clsConvert.ToDataTable(varBA);
                                                    Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                    string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                    clsFB FBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).FirstOrDefault();

                                                    blnCheckIsExistWarning = true;
                                                    if (dZarib > 1)
                                                    {
                                                        strWarning = "در محدوده رواداری غیر مجاز";
                                                    }
                                                    else
                                                    {
                                                        strWarning = "در محدوده رواداری مجاز";
                                                    }

                                                    Guid FBId = new Guid();
                                                    if (FBUser == null)
                                                    {
                                                        clsFB FB = new clsFB();
                                                        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                        FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                        FB.BahayeVahedZarib = 0;// dZarib;
                                                        _context.FBs.Add(FB);
                                                        _context.SaveChanges();
                                                        FBId = FB.ID;
                                                    }
                                                    else
                                                    {
                                                        FBId = FBUser.ID;
                                                    }

                                                    var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                              join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                              where RUsers.LevelNumber == LevelNumber
                                                                              select new
                                                                              {
                                                                                  RUsers.Shomareh,
                                                                                  RUsers.Sharh,
                                                                                  RUsers.Tedad,
                                                                                  RUsers.Tool,
                                                                                  RUsers.Arz,
                                                                                  RUsers.Ertefa,
                                                                                  RUsers.Vazn,
                                                                                  RUsers.Des,
                                                                                  RUsers.ForItem,
                                                                                  RUsers.Type,
                                                                                  RUsers.UseItem,
                                                                                  fb.BarAvordId,
                                                                                  RUsers.FBId
                                                                              }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                                    DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                                    string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                    Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                                    var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                                     join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                                     where RizMetreUserses.LevelNumber == LevelNumber
                                                                                     select new
                                                                                     {
                                                                                         ID = RizMetreUserses.ID,
                                                                                         Shomareh = RizMetreUserses.Shomareh,
                                                                                         Tedad = RizMetreUserses.Tedad,
                                                                                         Tool = RizMetreUserses.Tool,
                                                                                         Arz = RizMetreUserses.Arz,
                                                                                         Ertefa = RizMetreUserses.Ertefa,
                                                                                         Vazn = RizMetreUserses.Vazn,
                                                                                         Des = RizMetreUserses.Des,
                                                                                         FBId = RizMetreUserses.FBId,
                                                                                         OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                         ForItem = RizMetreUserses.ForItem,
                                                                                         Type = RizMetreUserses.Type,
                                                                                         UseItem = RizMetreUserses.UseItem,
                                                                                         BarAvordId = FB.BarAvordId
                                                                                     }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                                    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                                    //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                                    for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                                    {
                                                        DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                        if (DrRizMetreUsersesCurrent.Length == 0)
                                                        {
                                                            decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                            decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                            decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                            decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                            decimal? Vazn = dZarib; //DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());

                                                            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                            RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                            ShomareNew++;
                                                            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                            RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                            RizMetreUserses.Tedad = Tedad;
                                                            RizMetreUserses.Tool = Tool;
                                                            RizMetreUserses.Arz = Arz;
                                                            RizMetreUserses.Ertefa = Ertefa;
                                                            RizMetreUserses.Vazn = Vazn;
                                                            RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                                      //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                            RizMetreUserses.FBId = FBId;
                                                            RizMetreUserses.OperationsOfHamlId = 1;
                                                            RizMetreUserses.Type = "2";
                                                            RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                            RizMetreUserses.UseItem = "";
                                                            RizMetreUserses.LevelNumber = LevelNumber;
                                                            RizMetreUserses.InsertDateTime = Now;

                                                            RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                            RizMetreUserses.ConditionContextId = ConditionContextId;

                                                            ///محاسبه مقدار جزء
                                                            decimal? dMeghdarJoz = null;
                                                            if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                                                                dMeghdarJoz = null;
                                                            else
                                                                dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                                (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;


                                                            _context.RizMetreUserses.Add(RizMetreUserses);
                                                            _context.SaveChanges();
                                                            //RizMetreUserses.Save();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (blnCheckIsExistErrors != "false")
                                            {
                                                blnCheckIsExistErrors = "true";
                                                strErrors = "عدد وارد شده در محدوده قابل قبولی نمیباشد "; //strCondition.Replace("x", "عدد وارد شده");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "11":
                                {
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString().Trim();
                                    string strItemShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + strCharacterPlus;
                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString().Trim();
                                    string strDesOfAddingItems = Dr[idr]["DesOfAddingItems"].ToString().Trim();
                                    decimal dPercent = decimal.Parse(strFinalWorking);
                                    blnCheckIsExistErrors = "false";

                                    long lngItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemShomareh &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                    bool blnCheckSave = false;
                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strItemShomareh;
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;

                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            _context.SaveChanges();
                                            blnCheckSave = true;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = false;
                                        }
                                    }
                                    else
                                        blnCheckSave = true;


                                    if (blnCheckSave)
                                    {
                                        clsBaravordUser BA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).FirstOrDefault();
                                        if (BA != null)
                                        {

                                            clsFB FBUser = _context.FBs.Where(x => x.BarAvordId == BA.ID && x.Shomareh == strItemShomareh).FirstOrDefault();

                                            Guid FBId = new Guid();
                                            if (FBUser == null)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = BA.ID;
                                                FB.Shomareh = strItemShomareh;
                                                FB.BahayeVahedZarib = dPercent;
                                                FB.BahayeVahedSharh = strDesOfAddingItems;
                                                _context.FBs.Add(FB);
                                                _context.SaveChanges();
                                                FBId = FB.ID;
                                            }
                                            else
                                            {
                                                FBId = FBUser.ID;
                                            }

                                            List<RizMetreUsersForItemsAddingToFBInputDto> lstRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                                join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                where RUsers.LevelNumber == LevelNumber
                                                                                                                select new RizMetreUsersForItemsAddingToFBInputDto()
                                                                                                                {
                                                                                                                    Shomareh = RUsers.Shomareh,
                                                                                                                    Sharh = RUsers.Sharh,
                                                                                                                    Tedad = RUsers.Tedad,
                                                                                                                    Tool = RUsers.Tool,
                                                                                                                    Arz = RUsers.Arz,
                                                                                                                    Ertefa = RUsers.Ertefa,
                                                                                                                    Vazn = RUsers.Vazn,
                                                                                                                    Des = RUsers.Des,
                                                                                                                    FBId = RUsers.FBId,
                                                                                                                    Type = RUsers.Type
                                                                                                                }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();

                                            string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                             where RizMetreUserses.LevelNumber == LevelNumber
                                                                             select new
                                                                             {
                                                                                 ID = RizMetreUserses.ID,
                                                                                 Shomareh = RizMetreUserses.Shomareh,
                                                                                 Tedad = RizMetreUserses.Tedad,
                                                                                 Tool = RizMetreUserses.Tool,
                                                                                 Arz = RizMetreUserses.Arz,
                                                                                 Ertefa = RizMetreUserses.Ertefa,
                                                                                 Vazn = RizMetreUserses.Vazn,
                                                                                 Des = RizMetreUserses.Des,
                                                                                 FBId = RizMetreUserses.FBId,
                                                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                 ForItem = RizMetreUserses.ForItem,
                                                                                 Type = RizMetreUserses.Type,
                                                                                 UseItem = RizMetreUserses.UseItem,
                                                                                 BarAvordId = FB.BarAvordId
                                                                             }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                            DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                            //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                            //{
                                            foreach (var RM in lstRizMetreUserses)
                                            {
                                                DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh);
                                                if (DrRizMetreUsersesCurrent.Length == 0)
                                                {
                                                    decimal? Tedad = RM.Tedad;
                                                    decimal? Tool = RM.Tool;
                                                    decimal? Arz = RM.Arz;
                                                    decimal? Ertefa = RM.Ertefa;
                                                    decimal? Vazn = RM.Vazn;

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = RM.Shomareh;
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = RM.Sharh;
                                                    RizMetreUserses.Tedad = Tedad;
                                                    RizMetreUserses.Tool = Tool;
                                                    RizMetreUserses.Arz = Arz;
                                                    RizMetreUserses.Ertefa = Ertefa;
                                                    RizMetreUserses.Vazn = Vazn;
                                                    RizMetreUserses.Des = RM.Des; //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                  //+ " - ریز متره شماره " + RM.Shomareh;
                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                    RizMetreUserses.UseItem = "";
                                                    RizMetreUserses.LevelNumber = LevelNumber;
                                                    RizMetreUserses.InsertDateTime = Now;

                                                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses.ConditionContextId = ConditionContextId;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz = null;
                                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                        (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;


                                                    _context.RizMetreUserses.Add(RizMetreUserses);
                                                    _context.SaveChanges();
                                                    //RizMetreUserses.Save();
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "12":
                                {
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    string[] strConditionSplit = strCondition.Split("_");

                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                    DataTable DtRizMetreUserses = new DataTable();
                                    string strForItem = "";
                                    string strUseItem = "";
                                    string strItemFBShomareh = DtFB.Rows[0]["Shomareh"].ToString().Trim();

                                    Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                    strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                    strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
                                    //clsFB? varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strForItem).ToList();

                                    //if (varFBUser != null)
                                    //{
                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                        join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                        where RUsers.LevelNumber == LevelNumber
                                                                                                        select new RizMetreUsersForItemsAddingToFBInputDto
                                                                                                        {
                                                                                                            Shomareh = RUsers.Shomareh,
                                                                                                            Sharh = RUsers.Sharh,
                                                                                                            Tedad = RUsers.Tedad,
                                                                                                            Tool = RUsers.Tool,
                                                                                                            Arz = RUsers.Arz,
                                                                                                            Ertefa = RUsers.Ertefa,
                                                                                                            Vazn = RUsers.Vazn,
                                                                                                            Des = RUsers.Des,
                                                                                                            ForItem = RUsers.ForItem,
                                                                                                            Type = RUsers.Type,
                                                                                                            FBId = RUsers.FBId
                                                                                                        }).Where(x => x.FBId == guFBId && x.ForItem == strItemFBShomareh && x.Type == "1").OrderBy(x => x.Shomareh).ToList();

                                    //}

                                    var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                     join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                     where RizMetreUserses.LevelNumber == LevelNumber
                                                                     select new
                                                                     {
                                                                         ID = RizMetreUserses.ID,
                                                                         Shomareh = RizMetreUserses.Shomareh,
                                                                         Tedad = RizMetreUserses.Tedad,
                                                                         Tool = RizMetreUserses.Tool,
                                                                         Arz = RizMetreUserses.Arz,
                                                                         Ertefa = RizMetreUserses.Ertefa,
                                                                         Vazn = RizMetreUserses.Vazn,
                                                                         Des = RizMetreUserses.Des,
                                                                         FBId = RizMetreUserses.FBId,
                                                                         OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                         ForItem = RizMetreUserses.ForItem,
                                                                         Type = RizMetreUserses.Type,
                                                                         UseItem = RizMetreUserses.UseItem,
                                                                         BarAvordId = FB.BarAvordId
                                                                     }).Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();
                                    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                    List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId).ToList();
                                    foreach (var RM in varRizMetreUserses)
                                    {
                                        string strConditionOp = strConditionSplit[0].Replace("x", RM.Tool.ToString().Trim());
                                        string strConditionOp2 = strConditionSplit[1].Replace("z", RM.Ertefa.ToString().Trim());
                                        StringToFormula StringToFormula = new StringToFormula();
                                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        if (blnCheck)
                                        {
                                            bool blnCheck2 = StringToFormula.RelationalExpression2(strConditionOp2);
                                            if (blnCheck2)
                                            {
                                                string strCurrentFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                long lngItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                                clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                                if (currentItemsHasConditionAddedToFBs != null)
                                                {
                                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                                    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                                    ItemsHasConditionAddedToFB.Meghdar = RM.Tool != null ? RM.Tool.Value : 0;
                                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                    _context.SaveChanges();
                                                }

                                                if (strFinalWorking != "")
                                                {
                                                    strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");
                                                    ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                                                    ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                                                    ///Ertefa-3
                                                    decimal Ertefa = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                                                    string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                                    Guid FBId = new Guid();
                                                    if (varFBUser == null)
                                                    {
                                                        clsFB FB = new clsFB();
                                                        FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                        FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                        FB.BahayeVahedZarib = 0;
                                                        _context.FBs.Add(FB);
                                                        _context.SaveChanges();
                                                        FBId = FB.ID;
                                                    }
                                                    else
                                                        FBId = varFBUser.ID;

                                                    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh + " and FBId=" + FBId);
                                                    if (DrRizMetreUsersesCurrent.Length == 0)
                                                    {
                                                        decimal? Tedad = RM.Tedad == 0 ? 1 : RM.Tedad;
                                                        decimal? Tool = RM.Tool;
                                                        decimal? Arz = RM.Arz;
                                                        //decimal Ertefa = Ertefa;
                                                        decimal? Vazn = null;

                                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                        RizMetreUserses.Shomareh = RM.Shomareh;
                                                        ShomareNew++;
                                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                        RizMetreUserses.Sharh = RM.Sharh;
                                                        RizMetreUserses.Tedad = Tedad;
                                                        RizMetreUserses.Tool = Tool;
                                                        RizMetreUserses.Arz = Arz;
                                                        RizMetreUserses.Ertefa = Ertefa;
                                                        RizMetreUserses.Vazn = null;
                                                        RizMetreUserses.Des = RM.Des;

                                                        RizMetreUserses.FBId = FBId;
                                                        RizMetreUserses.OperationsOfHamlId = 1;
                                                        RizMetreUserses.Type = "2";
                                                        RizMetreUserses.ForItem = strForItem;
                                                        RizMetreUserses.UseItem = strUseItem;
                                                        RizMetreUserses.LevelNumber = LevelNumber;
                                                        RizMetreUserses.InsertDateTime = Now;

                                                        RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                        RizMetreUserses.ConditionContextId = ConditionContextId;

                                                        ///محاسبه مقدار جزء
                                                        decimal? dMeghdarJoz = null;
                                                        if (Tedad == null && Tool == null && Arz == null && Vazn == null)
                                                            dMeghdarJoz = null;
                                                        else
                                                            dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                            (Arz == null ? 1 : Arz.Value) * Ertefa * (Vazn == null ? 1 : Vazn.Value);
                                                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                        _context.RizMetreUserses.Add(RizMetreUserses);
                                                        _context.SaveChanges();
                                                        //RizMetreUserses.Save();
                                                    }
                                                }
                                            }

                                        }
                                    }

                                    break;
                                }
                            case "13":
                                {
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    decimal Meghdar2 = decimal.Parse(RBCodeArraySplit.Length > 2 ? RBCodeArraySplit[2].ToString() != "" ? RBCodeArraySplit[2].ToString() : "0" : "0");
                                    decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                    ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                    ItemsHasConditionAddedToFB.Meghdar2 = Meghdar2;
                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;

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
                                            blnCheckIsExistErrors = "false";

                                            bool blnCheckSave = false;
                                            try
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                _context.SaveChanges();
                                                blnCheckSave = true;
                                            }
                                            catch (Exception)
                                            {
                                                blnCheckSave = false;
                                            }
                                            if (blnCheckSave)
                                            {
                                                var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                                DataTable DtBA = clsConvert.ToDataTable(varBA);

                                                Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                                Guid FBId = new Guid();
                                                if (DtFBUser.Rows.Count == 0)
                                                {
                                                    clsFB FB = new clsFB();
                                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                    FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                    FB.BahayeVahedZarib = 0;
                                                    FB.BahayeVahedSharh = "";
                                                    _context.FBs.Add(FB);
                                                    _context.SaveChanges();
                                                    FBId = FB.ID;
                                                }
                                                else
                                                {
                                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                                }



                                                var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                          join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                          where RUsers.LevelNumber == LevelNumber
                                                                          select new
                                                                          {
                                                                              Shomareh = RUsers.Shomareh,
                                                                              Sharh = RUsers.Sharh,
                                                                              Tedad = RUsers.Tedad,
                                                                              Tool = RUsers.Tool,
                                                                              Arz = RUsers.Arz,
                                                                              Ertefa = RUsers.Ertefa,
                                                                              Vazn = RUsers.Vazn,
                                                                              Des = RUsers.Des,
                                                                              ForItem = RUsers.ForItem,
                                                                              Type = RUsers.Type,
                                                                              UseItem = RUsers.UseItem,
                                                                              BarAvordId = fb.BarAvordId,
                                                                              FBId = RUsers.FBId
                                                                          }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                                DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                                string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                                var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                                 join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                                 where RizMetreUserses.LevelNumber == LevelNumber
                                                                                 select new
                                                                                 {
                                                                                     ID = RizMetreUserses.ID,
                                                                                     Shomareh = RizMetreUserses.Shomareh,
                                                                                     Tedad = RizMetreUserses.Tedad,
                                                                                     Tool = RizMetreUserses.Tool,
                                                                                     Arz = RizMetreUserses.Arz,
                                                                                     Ertefa = RizMetreUserses.Ertefa,
                                                                                     Vazn = RizMetreUserses.Vazn,
                                                                                     Des = RizMetreUserses.Des,
                                                                                     FBId = RizMetreUserses.FBId,
                                                                                     OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                     ForItem = RizMetreUserses.ForItem,
                                                                                     Type = RizMetreUserses.Type,
                                                                                     UseItem = RizMetreUserses.UseItem,
                                                                                     BarAvordId = FB.BarAvordId
                                                                                 }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                                DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                                for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                                {
                                                    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                    if (DrRizMetreUsersesCurrent.Length == 0)
                                                    {
                                                        decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                        decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                        decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                        decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                        //decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                        RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                        ShomareNew++;
                                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                        RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                        RizMetreUserses.Tedad = Tedad;
                                                        RizMetreUserses.Tool = Tool;
                                                        RizMetreUserses.Arz = Arz;
                                                        RizMetreUserses.Ertefa = Ertefa;
                                                        RizMetreUserses.Vazn = dZaribVazn;
                                                        RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim();
                                                        RizMetreUserses.FBId = FBId;
                                                        RizMetreUserses.OperationsOfHamlId = 1;
                                                        RizMetreUserses.Type = "2";
                                                        RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                        RizMetreUserses.UseItem = "";
                                                        RizMetreUserses.LevelNumber = LevelNumber;
                                                        RizMetreUserses.InsertDateTime = Now;

                                                        RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                        RizMetreUserses.ConditionContextId = ConditionContextId;
                                                        ///محاسبه مقدار جزء
                                                        decimal? dMeghdarJoz = null;
                                                        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0)
                                                            dMeghdarJoz = null;
                                                        else
                                                            dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * dZaribVazn;
                                                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                        _context.RizMetreUserses.Add(RizMetreUserses);
                                                        _context.SaveChanges();
                                                        //RizMetreUserses.Save();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            return new JsonResult("CI");//CheckInput
                                        }
                                    }
                                    else
                                    {
                                        return new JsonResult("CI");//CheckInput
                                    }
                                    break;
                                }
                            case "14":
                                {
                                    string FieldsAdding = Dr[idr]["FieldsAdding"].ToString().Trim() == "" ? null : Dr[idr]["FieldsAdding"].ToString().Trim();
                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                    StringToFormula StringToFormula = new StringToFormula();
                                    bool blnCheck = true;
                                    if (strConditionOp != "")
                                    {
                                        blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    }
                                    if (blnCheck)
                                    {
                                        blnCheckIsExistErrors = "false";
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        bool blnCheckSave = false;
                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            _context.SaveChanges();
                                            blnCheckSave = true;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = false;
                                        }
                                        //if (ItemsHasConditionAddedToFB.Save())
                                        if (blnCheckSave)
                                        {
                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            decimal dMultiple = 0;
                                            if (strFinalWorking != "")
                                            {
                                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            }

                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);


                                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                            //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                            var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
                                            Guid FBId = new Guid();
                                            if (DtFBUser.Rows.Count == 0)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                FB.BahayeVahedZarib = 0;// dMultiple;
                                                _context.FBs.Add(FB);
                                                _context.SaveChanges();
                                                FBId = FB.ID;

                                                //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
                                            }
                                            else
                                                FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                            var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                      join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                      where RUsers.LevelNumber == LevelNumber
                                                                      select new
                                                                      {
                                                                          Shomareh = RUsers.Shomareh,
                                                                          Sharh = RUsers.Sharh,
                                                                          Tedad = RUsers.Tedad,
                                                                          Tool = RUsers.Tool,
                                                                          Arz = RUsers.Arz,
                                                                          Ertefa = RUsers.Ertefa,
                                                                          Vazn = RUsers.Vazn,
                                                                          Des = RUsers.Des,
                                                                          ForItem = RUsers.ForItem,
                                                                          Type = RUsers.Type,
                                                                          UseItem = RUsers.UseItem,
                                                                          BarAvordId = fb.BarAvordId,
                                                                          FBId = RUsers.FBId
                                                                      }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                            DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                            string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                             where RizMetreUserses.LevelNumber == LevelNumber
                                                                             select new
                                                                             {
                                                                                 ID = RizMetreUserses.ID,
                                                                                 Shomareh = RizMetreUserses.Shomareh,
                                                                                 Tedad = RizMetreUserses.Tedad,
                                                                                 Tool = RizMetreUserses.Tool,
                                                                                 Arz = RizMetreUserses.Arz,
                                                                                 Ertefa = RizMetreUserses.Ertefa,
                                                                                 Vazn = RizMetreUserses.Vazn,
                                                                                 Des = RizMetreUserses.Des,
                                                                                 FBId = RizMetreUserses.FBId,
                                                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                 ForItem = RizMetreUserses.ForItem,
                                                                                 Type = RizMetreUserses.Type,
                                                                                 UseItem = RizMetreUserses.UseItem,
                                                                                 BarAvordId = FB.BarAvordId
                                                                             }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                            DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                            for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                            {
                                                DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                if (DrRizMetreUsersesCurrent.Length == 0)
                                                {
                                                    decimal? dTedad = null;
                                                    decimal? dTool = null;
                                                    decimal? dArz = null;

                                                    string[] strFieldsAdding = FieldsAdding.Split(",");
                                                    List<string> lst = new List<string>();
                                                    for (int j = 0; j < strFieldsAdding.Length; j++)
                                                    {
                                                        lst.Add(strFieldsAdding[j]);
                                                    }

                                                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        dTedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        dTool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        dArz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                    }


                                                    decimal? dErtefa = dMultiple;
                                                    decimal Vazn = decimal.Parse("0.9"); //DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString());
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = DtRizMetreUserses.Rows[i]["Sharh"].ToString().Trim();
                                                    RizMetreUserses.Tedad = dTedad;
                                                    RizMetreUserses.Tool = dTool;
                                                    RizMetreUserses.Arz = dArz;
                                                    RizMetreUserses.Ertefa = dErtefa;
                                                    RizMetreUserses.Vazn = Vazn;
                                                    RizMetreUserses.Des = DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                                                              //                               + " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();

                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                    RizMetreUserses.UseItem = "";
                                                    RizMetreUserses.LevelNumber = LevelNumber;
                                                    RizMetreUserses.InsertDateTime = Now;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz = null;
                                                    if (dTedad == null && dTool == null && dArz == null && dErtefa == null)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * Vazn;
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;


                                                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses.ConditionContextId = ConditionContextId;

                                                    _context.RizMetreUserses.Add(RizMetreUserses);
                                                    _context.SaveChanges();
                                                    //RizMetreUserses.Save();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return new JsonResult("CI");//CheckInput


                                        //if (blnCheckIsExistErrors != "false")
                                        //{
                                        //    blnCheckIsExistErrors = "true";
                                        //    //strErrors += "<div class=\"col-md-12\"><span class=\"spanStyleMitraMedium\">" + Dr[idr]["DesOfAddingItems"].ToString() + "</span></div>";
                                        //    strErrors = strCondition.Replace("x", "عدد وارد شده");
                                        //}
                                    }
                                    break;
                                }
                            case "15":
                                {

                                    //بررسی اینکه در تراش یا لکه گیری هست یا خیر
                                    //اگر باشد دیگر صعوبت درج نمیشود

                                    RizMetreCommon rizMetreCommon = new RizMetreCommon();
                                    long[] lngConditionGroupId = { 31, 4 };

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

                                    string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString().Trim();

                                    string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                    string[] strConditionSplit = strCondition.Split("_");

                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                    DataTable DtRizMetreUserses = new DataTable();
                                    string strForItem = "";
                                    string strUseItem = "";
                                    string strItemFBShomareh = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                    string strDesOfAddingItems = Dr[idr]["DesOfAddingItems"].ToString().Trim();

                                    Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                    strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                    strForItem = Dr[idr]["AddedItems"].ToString().Trim();
                                    //clsFB? varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strForItem).ToList();

                                    //if (varFBUser != null)
                                    //{
                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                        join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                        where RUsers.LevelNumber == LevelNumber
                                                                                                        select new RizMetreUsersForItemsAddingToFBInputDto
                                                                                                        {
                                                                                                            Id = RUsers.ID,
                                                                                                            Shomareh = RUsers.Shomareh,
                                                                                                            Sharh = RUsers.Sharh,
                                                                                                            Tedad = RUsers.Tedad,
                                                                                                            Tool = RUsers.Tool,
                                                                                                            Arz = RUsers.Arz,
                                                                                                            Ertefa = RUsers.Ertefa,
                                                                                                            Vazn = RUsers.Vazn,
                                                                                                            Des = RUsers.Des,
                                                                                                            ForItem = RUsers.ForItem,
                                                                                                            Type = RUsers.Type,
                                                                                                            FBId = RUsers.FBId
                                                                                                        }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();

                                    //}

                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUsersesWithoutLakeAndTarash = new List<RizMetreUsersForItemsAddingToFBInputDto>();

                                    foreach (var RM1 in varRizMetreUserses)
                                    {
                                        RizMetreForGetAndShowAddItemsDto? FindRM = lstRM.FirstOrDefault(x => x.Shomareh == RM1.Shomareh);
                                        if (FindRM == null)
                                        {
                                            varRizMetreUsersesWithoutLakeAndTarash.Add(varRizMetreUserses.FirstOrDefault(x => x.Shomareh == RM1.Shomareh));
                                        }
                                    }

                                    if (varRizMetreUsersesWithoutLakeAndTarash.Count != 0)
                                    {
                                        var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                         join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                         where RizMetreUserses.LevelNumber == LevelNumber
                                                                         select new
                                                                         {
                                                                             ID = RizMetreUserses.ID,
                                                                             Shomareh = RizMetreUserses.Shomareh,
                                                                             Tedad = RizMetreUserses.Tedad,
                                                                             Tool = RizMetreUserses.Tool,
                                                                             Arz = RizMetreUserses.Arz,
                                                                             Ertefa = RizMetreUserses.Ertefa,
                                                                             Vazn = RizMetreUserses.Vazn,
                                                                             Des = RizMetreUserses.Des,
                                                                             FBId = RizMetreUserses.FBId,
                                                                             OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                             ForItem = RizMetreUserses.ForItem,
                                                                             Type = RizMetreUserses.Type,
                                                                             UseItem = RizMetreUserses.UseItem,
                                                                             BarAvordId = FB.BarAvordId
                                                                         }).Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();
                                        DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                        List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId).ToList();
                                        string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus;
                                        clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                        Guid FBId = new Guid();
                                        if (varFBUser == null)
                                        {
                                            clsFB FB = new clsFB();
                                            FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            FB.Shomareh = strItemShomareh;
                                            FB.BahayeVahedZarib = 0;
                                            FB.BahayeVahedSharh = strDesOfAddingItems;
                                            _context.FBs.Add(FB);
                                            _context.SaveChanges();
                                            FBId = FB.ID;
                                        }
                                        else
                                            FBId = varFBUser.ID;

                                        //List<clsFB> varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh).ToList();

                                        foreach (var RM in varRizMetreUsersesWithoutLakeAndTarash)
                                        {
                                            string strConditionOp = strConditionSplit[0].Replace("x", RM.Arz.ToString().Trim());
                                            StringToFormula StringToFormula = new StringToFormula();
                                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                            if (blnCheck)
                                            {
                                                string strCurrentFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                long lngItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                                clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                                if (currentItemsHasConditionAddedToFBs == null)
                                                {
                                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                                    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                                    ItemsHasConditionAddedToFB.Meghdar = RM.Tool != null ? RM.Tool.Value : 0;
                                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                    _context.SaveChanges();
                                                }

                                                decimal? dArz = null;
                                                if (RM.Arz <= 2)
                                                    dArz = RM.Arz;
                                                else
                                                    dArz = 2;


                                                //string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus;


                                                DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh + " and FBId='" + FBId + "'");
                                                if (DrRizMetreUsersesCurrent.Length == 0)
                                                {
                                                    decimal? Tedad = RM.Tedad == 0 ? 1 : RM.Tedad;
                                                    decimal? Tool = RM.Tool;
                                                    //decimal? Arz = RM.Arz;
                                                    decimal? Ertefa = RM.Ertefa;
                                                    decimal? Vazn = RM.Vazn;

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = RM.Shomareh;
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = RM.Sharh;
                                                    RizMetreUserses.Tedad = Tedad;
                                                    RizMetreUserses.Tool = Tool;
                                                    RizMetreUserses.Arz = dArz;
                                                    RizMetreUserses.Ertefa = Ertefa;
                                                    RizMetreUserses.Vazn = Vazn;
                                                    RizMetreUserses.Des = RM.Des;

                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = strForItem;
                                                    RizMetreUserses.UseItem = strUseItem;
                                                    RizMetreUserses.LevelNumber = LevelNumber;
                                                    RizMetreUserses.InsertDateTime = Now;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz = null;
                                                    if (Tedad == null && Tool == null && dArz == null && Ertefa == null && dArz == null && Vazn == null)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                                        (dArz == null ? 1 : dArz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);

                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                    _context.RizMetreUserses.Add(RizMetreUserses);
                                                    _context.SaveChanges();


                                                    ///بایستی ریزمتره اصلی نیز آپدیت گردد
                                                    ///
                                                    RM.Arz = dArz;

                                                    UpdateRizMetreUsersInputDto request1 = new UpdateRizMetreUsersInputDto
                                                    {
                                                        Id = RM.Id,
                                                        Sharh = RM.Sharh,
                                                        Tedad = RM.Tedad,
                                                        Tool = RM.Tool,
                                                        Arz = RM.Arz,
                                                        Ertefa = RM.Ertefa,
                                                        Vazn = RM.Vazn,
                                                        Des = RM.Des,
                                                        NoeFB = NoeFB,
                                                        Year = Year,
                                                        BarAvordUserId = BarAvordId,
                                                        LevelNumber = LevelNumber,
                                                        Code = RBCode.ToString(),
                                                        OperationId = OperationId,
                                                        FBId = FBId
                                                    };

                                                    rizMetreCommon.UpdateRizMetreFromAddedItems(request1, _context);

                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "16":
                                {
                                    ///بررسی پخش، آبپاشی، تسطیح و کوبیدن قشر زیراساس  
                                    /////در صورت یافتن ریز متره درج شده برای این شرط بایستی به 
                                    ///اضافه بها بابت سختی اجرا در شانه سازی ها به عرض تا 2 متر
                                    ///اضافه گردد
                                    RizMetreCommon rizMetreCommon = new RizMetreCommon();
                                    long[] lngConditionGroupId = { 12 };

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

                                    string FieldsAdding = Dr[idr]["FieldsAdding"].ToString();

                                    string? ConditionContextRel = Dr[idr]["ConditionContextRel"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextRel"].ToString().Trim();
                                    long? ConditionContextId = long.Parse(Dr[idr]["ConditionContextId"].ToString().Trim() == "" ? null : Dr[idr]["ConditionContextId"].ToString().Trim());

                                    if (lstRM.Count != 0)
                                    {
                                        string strCurrentFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

                                        long lngItemsHasCondition_ConditionContextId = long.Parse(Dr[idr]["ItemsHasCondition_ConditionContextId"].ToString());
                                        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                         .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                             x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                        bool blnCheckSave = false;
                                        if (currentItemsHasConditionAddedToFBs == null)
                                        {
                                            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                            ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                            ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                            ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                            ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;

                                            try
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                _context.SaveChanges();
                                                blnCheckSave = true;
                                            }
                                            catch (Exception)
                                            {
                                                blnCheckSave = false;
                                            }
                                        }
                                        //if (ItemsHasConditionAddedToFB.Save())
                                        if (blnCheckSave)
                                        {
                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);

                                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                            var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                            //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + "'");
                                            Guid FBId = new Guid();
                                            if (DtFBUser.Rows.Count == 0)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
                                                FB.BahayeVahedZarib = 0;
                                                FB.BahayeVahedSharh = "";
                                                _context.FBs.Add(FB);
                                                _context.SaveChanges();
                                                FBId = FB.ID;

                                                //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[0]["AddedItems"].ToString().Trim(), 0);
                                            }
                                            else
                                            {
                                                FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                            }



                                            //var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                            //                          join fb in _context.FBs on RUsers.FBId equals fb.ID
                                            //                          where RUsers.LevelNumber == LevelNumber
                                            //                          select new
                                            //                          {
                                            //                              Shomareh = RUsers.Shomareh,
                                            //                              Sharh = RUsers.Sharh,
                                            //                              Tedad = RUsers.Tedad,
                                            //                              Tool = RUsers.Tool,
                                            //                              Arz = RUsers.Arz,
                                            //                              Ertefa = RUsers.Ertefa,
                                            //                              Vazn = RUsers.Vazn,
                                            //                              Des = RUsers.Des,
                                            //                              ForItem = RUsers.ForItem,
                                            //                              Type = RUsers.Type,
                                            //                              UseItem = RUsers.UseItem,
                                            //                              BarAvordId = fb.BarAvordId,
                                            //                              FBId = RUsers.FBId
                                            //                          }).Where(x => x.FBId == Guid.Parse(DtFB.Rows[0]["Id"].ToString()) && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                            //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                            string[] FieldsAddingSplit = FieldsAdding.Split(",");

                                            string strShomareh1 = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                                                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                                                             where RizMetreUserses.LevelNumber == LevelNumber
                                                                             select new
                                                                             {
                                                                                 ID = RizMetreUserses.ID,
                                                                                 Shomareh = RizMetreUserses.Shomareh,
                                                                                 Tedad = RizMetreUserses.Tedad,
                                                                                 Tool = RizMetreUserses.Tool,
                                                                                 Arz = RizMetreUserses.Arz,
                                                                                 Ertefa = RizMetreUserses.Ertefa,
                                                                                 Vazn = RizMetreUserses.Vazn,
                                                                                 Des = RizMetreUserses.Des,
                                                                                 FBId = RizMetreUserses.FBId,
                                                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                                                                 ForItem = RizMetreUserses.ForItem,
                                                                                 Type = RizMetreUserses.Type,
                                                                                 UseItem = RizMetreUserses.UseItem,
                                                                                 BarAvordId = FB.BarAvordId
                                                                             }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                            DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                            //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                            //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                            foreach (var itemRM in lstRM)
                                            {
                                                string strCondition = Dr[idr]["Condition"].ToString().Trim();

                                                string strConditionOp = strCondition.Replace("x", itemRM.Arz.ToString().Trim());
                                                StringToFormula StringToFormula = new StringToFormula();
                                                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                                if (blnCheck)
                                                {
                                                    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + itemRM.Shomareh);

                                                    if (DrRizMetreUsersesCurrent.Length == 0)
                                                    {
                                                        decimal? Tedad = itemRM.Tedad;// DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                        decimal? Tool = itemRM.Tool;// DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                        decimal? Arz = itemRM.Arz;// DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                        decimal? Ertefa = itemRM.Ertefa;// DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                        decimal? Vazn = itemRM.Vazn;// DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());

                                                        if (true)
                                                        {

                                                        }
                                                        List<string> lst = new List<string>();
                                                        for (int j = 0; j < FieldsAddingSplit.Length; j++)
                                                        {
                                                            lst.Add(FieldsAddingSplit[j]);
                                                        }

                                                        decimal? dTedad = null;
                                                        decimal? dTool = null;
                                                        decimal? dArz = null;
                                                        decimal? dErtefa = null;
                                                        decimal? dVazn = null;

                                                        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            dTedad = Tedad;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            dTool = Tool;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            dArz = Arz;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            dErtefa = Ertefa;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            dVazn = Vazn;
                                                        }

                                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                        RizMetreUserses.Shomareh = itemRM.Shomareh;
                                                        ShomareNew++;
                                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                        RizMetreUserses.Sharh = itemRM.Sharh;

                                                        RizMetreUserses.Tedad = dTedad;
                                                        RizMetreUserses.Tool = dTool;
                                                        RizMetreUserses.Arz = dArz;
                                                        RizMetreUserses.Ertefa = dErtefa;
                                                        RizMetreUserses.Vazn = dVazn;

                                                        RizMetreUserses.Des = itemRM.Des; //DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                        RizMetreUserses.FBId = FBId;
                                                        RizMetreUserses.OperationsOfHamlId = 1;
                                                        RizMetreUserses.Type = "2";
                                                        RizMetreUserses.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                        RizMetreUserses.UseItem = "";
                                                        RizMetreUserses.LevelNumber = LevelNumber;
                                                        RizMetreUserses.InsertDateTime = Now;

                                                        ///محاسبه مقدار جزء
                                                        decimal? dMeghdarJoz = null;
                                                        if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                                            dMeghdarJoz = null;
                                                        else
                                                            dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                            (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;
                                                        RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                        RizMetreUserses.ConditionContextId = ConditionContextId;

                                                        _context.RizMetreUserses.Add(RizMetreUserses);
                                                        _context.SaveChanges();
                                                        //RizMetreUserses.Save();
                                                    }
                                                }
                                            }
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

            if (blnCheckIsExistErrors == "true")
            {
                return new JsonResult("CI");//CheckInput
                //return new JsonResult("NOK_" + strErrors);
            }
            else
            {
                if (blnCheckIsExistWarning)
                {
                    return new JsonResult("OK_" + strWarning);
                }
                else
                    return new JsonResult("OK");


                // return "OK_";
            }

        }


        public JsonResult GetAndShowAddItems([FromBody] GetAndShowAddItemsInputDto request)
        {
            string StrRBCode = request.strRBCode;
            long OperationId = request.OperationId;
            Guid BarAvordId = request.BarAvordUserId;
            long ConditionGroupId = request.ConditionGroupId;
            int LevelNumber = request.LevelNumber;
            NoeFehrestBaha NoeFB = request.NoeFB;

            clsOperation_ItemsFB? Operation_ItemsFBs = _context.Operation_ItemsFBs.Where(x => x.OperationId == OperationId).FirstOrDefault();
            if (Operation_ItemsFBs == null)
            {
                return new JsonResult("NOK_اطلاعات بدرستی یافت نشد");
            }

            clsBaravordUser? BaraAvordUser = _context.BaravordUsers.Where(x => x.ID == BarAvordId).FirstOrDefault();
            if (BaraAvordUser == null)
            {
                return new JsonResult("برآورد یافت نشد");
            }

            int Year = BaraAvordUser.Year;

            var varDt = _context.Operation_ItemsFBs.Where(x => x.OperationId == OperationId).ToList();
            DataTable Dt = clsConvert.ToDataTable(varDt);
            string strItemsFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            var varFB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemsFBShomareh).ToList();
            DataTable DtFB = clsConvert.ToDataTable(varFB);
            string strFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            DataTable DtItemsAddingToFB = new DataTable();

            var varItemsHasConditionAddedToFB = (from clsItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
                                                 join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                                 on clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasCondition_ConditionContext.Id
                                                 join tblItemsHasCondition in _context.ItemsHasConditions on tblItemsHasCondition_ConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
                                                 where tblItemsHasCondition.Year == Year
                                                 select new
                                                 {
                                                     clsItemsHasConditionAddedToFB.ID,
                                                     clsItemsHasConditionAddedToFB.FBShomareh,
                                                     clsItemsHasConditionAddedToFB.BarAvordId,
                                                     clsItemsHasConditionAddedToFB.Meghdar,
                                                     clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                     clsItemsHasConditionAddedToFB.ConditionGroupId,
                                                     ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                                     tblItemsHasCondition_ConditionContext.ViewCheckAllRecords,
                                                     tblItemsHasCondition_ConditionContext.EnableEditing,
                                                     tblItemsHasCondition_ConditionContext.EnableDeleting
                                                 }).Where(x => x.FBShomareh.Substring(0, 6) == strFBShomareh && x.BarAvordId == BarAvordId && x.ConditionGroupId == ConditionGroupId).ToList();
            DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
            List<RizMetreForGetAndShowAddItemsDto> lstFbs = new List<RizMetreForGetAndShowAddItemsDto>();

            List<ItemFBShomarehForGetAndShowAddItemsDto> lstItemFBShomarehForGet = new List<ItemFBShomarehForGetAndShowAddItemsDto>();

            decimal dZaribEzafeYaKasreBahaGiri = 0;
            if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
            {
                List<long> strItemsHasCondition_ConditionContext = new List<long>();
                if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                {
                    for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                    {
                        strItemsHasCondition_ConditionContext.Add(long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString()));
                    }
                }

                //var varItemsAddingToFB1 = _context.ItemsAddingToFBs.Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();

                var lstItemsAddingToFB = (from ItemsAddingToFB in _context.ItemsAddingToFBs
                                          join FehrestBaha in _context.FehrestBahas on ItemsAddingToFB.AddedItems.Trim() equals FehrestBaha.Shomareh.Trim()
                                          where strItemsHasCondition_ConditionContext.Contains(ItemsAddingToFB.ItemsHasCondition_ConditionContextId)
                                          && FehrestBaha.Sal == Year
                                          select new
                                          {
                                              ItemsAddingToFB.Id,
                                              ItemsAddingToFB.ItemsHasCondition_ConditionContextId,
                                              ItemsAddingToFB.AddedItems,
                                              ItemsAddingToFB.Condition,
                                              ItemsAddingToFB.FinalWorking,
                                              ItemsAddingToFB.ConditionType,
                                              ItemsAddingToFB.DesOfAddingItems,
                                              ItemsAddingToFB.UseItemForAdd,
                                              ItemsAddingToFB.FieldsAdding,
                                              ItemsAddingToFB.CharacterPlus,
                                              FehrestBaha.Sharh,
                                              FehrestBaha.Shomareh,
                                          }).ToList();
                DtItemsAddingToFB = clsConvert.ToDataTable(lstItemsAddingToFB);

                List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
                {
                    Shomareh = x.ItemShomareh,
                    FieldType = x.FieldType,
                    Vahed = x.Vahed,
                    IsEnteringValue = x.IsEnteringValue
                }).ToList();

                for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
                {
                    bool blnViewCheckAllRecords = DtItemsHasConditionAddedToFB.Rows[Counter]["ViewCheckAllRecords"].ToString().Trim() == "True" ? true : false;
                    bool blnEnableEditing = DtItemsHasConditionAddedToFB.Rows[Counter]["EnableEditing"].ToString().Trim() == "True" ? true : false;
                    bool blnEnableDeleting = DtItemsHasConditionAddedToFB.Rows[Counter]["EnableDeleting"].ToString().Trim() == "True" ? true : false;
                    decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
                    string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();
                    DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
                    if (Dr.Length != 0)
                    {
                        for (int idr = 0; idr < Dr.Length; idr++)
                        {
                            string strFBShomarehAdded = Dr[idr]["AddedItems"].ToString().Trim();

                            List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> ItemFields = new List<ItemFBShomarehForGetAndShowAddItemsFieldsDto>();
                            ItemFields = lstItemFields.Where(x => x.Shomareh.Trim() == strFBShomarehAdded).ToList();

                            string strCharacterPlus = Dr[idr]["CharacterPlus"].ToString().Trim();
                            string strSharh = Dr[idr]["Sharh"].ToString().Trim();
                            ItemFBShomarehForGetAndShowAddItemsDto ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                            {
                                ItemFBShomareh = strFBShomarehAdded,
                                Des = strSharh,
                                ItemFields = ItemFields

                            };
                            lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);


                            switch (Dr[idr]["ConditionType"].ToString())
                            {
                                case "1":
                                case "15":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            lstItemFBShomarehForGet.Clear();

                                            ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                            {
                                                ItemFBShomareh = DtFBUsersAdded.Rows[0]["Shomareh"].ToString(),
                                                Des = DtFBUsersAdded.Rows[0]["BahayeVahedSharh"].ToString(),
                                                ItemFields = ItemFields
                                            };
                                            lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);

                                            string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            StringToFormula StringToFormula = new StringToFormula();
                                            decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();

                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh)
                                                .Select(x => new RizMetreForGetAndShowAddItemsDto
                                                {
                                                    Id = x.ID,
                                                    Tedad = x.Tedad,
                                                    Tool = x.Tool,
                                                    Arz = x.Arz,
                                                    Ertefa = x.Ertefa,
                                                    Vazn = x.Vazn,
                                                    Sharh = x.Sharh,
                                                    Des = x.Des,
                                                    MeghdarJoz = x.MeghdarJoz,
                                                    Shomareh = x.Shomareh,
                                                    ShomarehNew = x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs = RizMetre;

                                                }
                                            }

                                        }
                                        break;
                                    }
                                case "2":
                                case "16":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            var RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()
                                                && x.LevelNumber == LevelNumber).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                                {
                                                    Id = x.ID,
                                                    Tedad = x.Tedad,
                                                    Tool = x.Tool,
                                                    Arz = x.Arz,
                                                    Ertefa = x.Ertefa,
                                                    Vazn = x.Vazn,
                                                    Sharh = x.Sharh,
                                                    Des = x.Des,
                                                    MeghdarJoz = x.MeghdarJoz,
                                                    Shomareh = x.Shomareh,
                                                    ShomarehNew = x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs = RizMetre;
                                                }
                                            }

                                        }
                                        break;
                                    }
                                case "3":
                                    {
                                        //decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
                                        //string strStatus = dPercent > 0 ? "B" : "e";
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {
                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                                {
                                                    Id = x.ID,
                                                    Tedad = x.Tedad,
                                                    Tool = x.Tool,
                                                    Arz = x.Arz,
                                                    Ertefa = x.Ertefa,
                                                    Vazn = x.Vazn,
                                                    Sharh = x.Sharh,
                                                    Des = x.Des,
                                                    MeghdarJoz = x.MeghdarJoz,
                                                    Shomareh = x.Shomareh,
                                                    ShomarehNew = x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();

                                                if (RizMetre != null)
                                                {
                                                    if (RizMetre.Count != 0)
                                                    {
                                                        lstFbs = RizMetre;
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case "4":
                                case "5":
                                case "14":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim() && x.LevelNumber == LevelNumber)
                                                .OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                                {
                                                    Id = x.ID,
                                                    Tedad = x.Tedad,
                                                    Tool = x.Tool,
                                                    Arz = x.Arz,
                                                    Ertefa = x.Ertefa,
                                                    Vazn = x.Vazn,
                                                    Sharh = x.Sharh,
                                                    Des = x.Des,
                                                    MeghdarJoz = x.MeghdarJoz,
                                                    Shomareh = x.Shomareh,
                                                    ShomarehNew = x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs = RizMetre;
                                                }
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
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim() && x.LevelNumber == LevelNumber)
                                                .OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                                {
                                                    Id = x.ID,
                                                    Tedad = x.Tedad,
                                                    Tool = x.Tool,
                                                    Arz = x.Arz,
                                                    Ertefa = x.Ertefa,
                                                    Vazn = x.Vazn,
                                                    Sharh = x.Sharh,
                                                    Des = x.Des,
                                                    MeghdarJoz = x.MeghdarJoz,
                                                    Shomareh = x.Shomareh,
                                                    ShomarehNew = x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs.AddRange(RizMetre);
                                                }
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
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim() && x.LevelNumber == LevelNumber)
                                                .OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                                {
                                                    Id = x.ID,
                                                    Tedad = x.Tedad,
                                                    Tool = x.Tool,
                                                    Arz = x.Arz,
                                                    Ertefa = x.Ertefa,
                                                    Vazn = x.Vazn,
                                                    Sharh = x.Sharh,
                                                    Des = x.Des,
                                                    MeghdarJoz = x.MeghdarJoz,
                                                    Shomareh = x.Shomareh,
                                                    ShomarehNew = x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs = RizMetre;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case "9":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            //string strCondition = Dr[idr]["Condition"].ToString().Trim();
                                            //string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                                            //string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                            //StringToFormula StringToFormula = new StringToFormula();
                                            //bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                            //if (blnCheck)
                                            //{
                                            string strForItem = "";
                                            string strUseItem = "";
                                            if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
                                                strForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            else
                                            {
                                                strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
                                                strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            }

                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strForItem
                                                        && x.UseItem == strUseItem && x.LevelNumber == LevelNumber).OrderBy(x => x.Shomareh)
                                                        .Select(x => new RizMetreForGetAndShowAddItemsDto
                                                        {
                                                            Id = x.ID,
                                                            Tedad = x.Tedad,
                                                            Tool = x.Tool,
                                                            Arz = x.Arz,
                                                            Ertefa = x.Ertefa,
                                                            Vazn = x.Vazn,
                                                            Sharh = x.Sharh,
                                                            Des = x.Des,
                                                            MeghdarJoz = x.MeghdarJoz,
                                                            Shomareh = x.Shomareh,
                                                            ShomarehNew = x.ShomarehNew,
                                                            ItemFBShomareh = strFBShomarehAdded,
                                                            HasDelButton = blnEnableDeleting,
                                                            HasEditButton = blnEnableEditing
                                                        }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs.AddRange(RizMetre);
                                                }
                                            }
                                            //}
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
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                            {
                                                Id = x.ID,
                                                Tedad = x.Tedad,
                                                Tool = x.Tool,
                                                Arz = x.Arz,
                                                Ertefa = x.Ertefa,
                                                Vazn = x.Vazn,
                                                Sharh = x.Sharh,
                                                Des = x.Des,
                                                MeghdarJoz = x.MeghdarJoz,
                                                Shomareh = x.Shomareh,
                                                ShomarehNew = x.ShomarehNew,
                                                ItemFBShomareh = strFBShomarehAdded,
                                                HasDelButton = blnEnableDeleting,
                                                HasEditButton = blnEnableEditing
                                            }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs = RizMetre;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case "11":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemsFBShomareh + Dr[idr]["CharacterPlus"].ToString()).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            lstItemFBShomarehForGet.Clear();

                                            ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                            {
                                                ItemFBShomareh = DtFBUsersAdded.Rows[0]["Shomareh"].ToString(),
                                                Des = DtFBUsersAdded.Rows[0]["BahayeVahedSharh"].ToString(),
                                                ItemFields = ItemFields
                                            };
                                            lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);

                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                            {
                                                Id = x.ID,
                                                Tedad = x.Tedad,
                                                Tool = x.Tool,
                                                Arz = x.Arz,
                                                Ertefa = x.Ertefa,
                                                Vazn = x.Vazn,
                                                Sharh = x.Sharh,
                                                Des = x.Des,
                                                MeghdarJoz = x.MeghdarJoz,
                                                Shomareh = x.Shomareh,
                                                ShomarehNew = x.ShomarehNew,
                                                ItemFBShomareh = strFBShomarehAdded,
                                                HasDelButton = blnEnableDeleting,
                                                HasEditButton = blnEnableEditing
                                            }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs = RizMetre;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case "12":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                        if (varFBUsersAdded != null)
                                        {
                                            //lstItemFBShomarehForGet.Clear();

                                            //ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                            //{
                                            //    ItemFBShomareh = varFBUsersAdded.Shomareh,
                                            //    Des = varFBUsersAdded.BahayeVahedSharh
                                            //};
                                            //lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh)
                                                        .Select(x => new RizMetreForGetAndShowAddItemsDto
                                                        {
                                                            Id = x.ID,
                                                            Tedad = x.Tedad,
                                                            Tool = x.Tool,
                                                            Arz = x.Arz,
                                                            Ertefa = x.Ertefa,
                                                            Vazn = x.Vazn,
                                                            Sharh = x.Sharh,
                                                            Des = x.Des,
                                                            MeghdarJoz = x.MeghdarJoz,
                                                            Shomareh = x.Shomareh,
                                                            ShomarehNew = x.ShomarehNew,
                                                            ItemFBShomareh = strFBShomarehAdded,
                                                            HasDelButton = blnEnableDeleting,
                                                            HasEditButton = blnEnableEditing
                                                        }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs.AddRange(RizMetre);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case "13":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            var RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()
                                                && x.LevelNumber == LevelNumber).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
                                                {
                                                    Id = x.ID,
                                                    Tedad = x.Tedad,
                                                    Tool = x.Tool,
                                                    Arz = x.Arz,
                                                    Ertefa = x.Ertefa,
                                                    Vazn = x.Vazn,
                                                    Sharh = x.Sharh,
                                                    Des = x.Des,
                                                    MeghdarJoz = x.MeghdarJoz,
                                                    Shomareh = x.Shomareh,
                                                    ShomarehNew = x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();
                                            if (RizMetre != null)
                                            {
                                                if (RizMetre.Count != 0)
                                                {
                                                    lstFbs = RizMetre;
                                                }
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
            var lst = lstFbs.OrderBy(x => x.Shomareh);
            var Result = new
            {
                lst,
                lstItemFBShomarehForGet
            };

            return new JsonResult(Result);
        }
    }
}
