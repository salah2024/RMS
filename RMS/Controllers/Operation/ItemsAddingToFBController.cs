using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using Stimulsoft.Report.Components;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Text.RegularExpressions;
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

        public async Task<JsonResult> SaveItemsAddingToFB([FromBody] SaveItemsAddingToFBInputDto request)
        {
            string StrRBCode = request.strRBCode;
            long OperationId = request.OperationId;
            Guid BarAvordId = request.BarAvordUserId;
            long ConditionGroupId = request.ConditionGroupId;
            int LevelNumber = request.LevelNumber;
            NoeFehrestBaha NoeFBId = request.NoeFBId;

            clsBaravordUser? BaraAvordUser = _context.BaravordUsers.Where(x => x.ID == BarAvordId).FirstOrDefault();
            if (BaraAvordUser == null)
            {
                return new JsonResult("برآورد یافت نشد");
            }

            int Year = BaraAvordUser.Year;

            //string strParam = "";
            //strParam = "OperationId=" + OperationId;
            clsOperation_ItemsFB? varDt = _context.Operation_ItemsFBs.FirstOrDefault(x => x.OperationId == OperationId);
            if (varDt == null)
            {
                return new JsonResult(null);
            }
            //DataTable Dt = clsConvert.ToDataTable(varDt);
            //DataTable Dt = clsOperation_ItemsFB.ListWithParameter(strParam);
            string strItemsFBShomareh = varDt.ItemsFBShomareh.Trim();// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            clsFB? varFB = _context.FBs.First(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemsFBShomareh && x.NoeFBId == NoeFBId);


            Guid BaseFBId = varFB.ID;
            string varFBShomareh = varFB.Shomareh;

            //DataTable DtFB = clsConvert.ToDataTable(varFB);
            //DataTable DtFB = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "'");
            //string strFBShomareh = varDt.ItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

            //DataTable DtItemsAddingToFB = new DataTable();
            var varItemsHasConditionAddedToFB = (from clsItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
                                                 join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                                 on clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasCondition_ConditionContext.Id
                                                 join tblItemsHasCondition in _context.ItemsHasConditions on tblItemsHasCondition_ConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
                                                 where tblItemsHasCondition.Year == Year && tblItemsHasCondition.NoeFB == NoeFBId
                                                 select new
                                                 {
                                                     clsItemsHasConditionAddedToFB.ID,
                                                     clsItemsHasConditionAddedToFB.FBShomareh,
                                                     clsItemsHasConditionAddedToFB.BarAvordId,
                                                     clsItemsHasConditionAddedToFB.Meghdar,
                                                     clsItemsHasConditionAddedToFB.Meghdar2,
                                                     clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                     clsItemsHasConditionAddedToFB.ConditionGroupId,
                                                     ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                                 }).Where(x => x.FBShomareh.Substring(0, 6) == strItemsFBShomareh && x.BarAvordId == BarAvordId && x.ConditionGroupId == ConditionGroupId).ToList();


            //DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);

            if (varItemsHasConditionAddedToFB.Count != 0)
            {
                List<long> strItemsHasCondition_ConditionContext = new List<long>();
                foreach (var IHCATFB in varItemsHasConditionAddedToFB)
                {
                    strItemsHasCondition_ConditionContext.Add(IHCATFB.ItemsHasCondition_ConditionContextId);
                }
                //for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                //{
                //    strItemsHasCondition_ConditionContext.Add(long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString()));
                //}

                //var varItemsAddingToFB1 = _context.ItemsAddingToFBs.Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
                //DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB1);


                List<clsItemsAddingToFB> lstItemsAddingToFBs = _context.ItemsAddingToFBs
                        .Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)
                          && x.Year == Year && x.NoeFB == NoeFBId).ToList();



                List<ItemsAddingToFBForShowAddItemsDto> lstItemsAddingToFBForShowAddItems1 = new List<ItemsAddingToFBForShowAddItemsDto>();

                foreach (var ItemsAddingToFB in lstItemsAddingToFBs)
                {
                    string[] currentItemsAdding = ItemsAddingToFB.AddedItems.Split('_');

                    foreach (var ItemsAdding in currentItemsAdding)
                    {
                        clsFehrestBaha? FehrestBaha = _context.FehrestBahas.FirstOrDefault(x => x.Sal == Year && x.NoeFB == NoeFBId && x.Shomareh.Trim() == ItemsAdding.Trim());

                        if (FehrestBaha != null)
                        {
                            ItemsAddingToFBForShowAddItemsDto ItemsAddingToFBForShowAddItems = new ItemsAddingToFBForShowAddItemsDto
                            {
                                Id = ItemsAddingToFB.Id,
                                ItemsHasCondition_ConditionContextId = ItemsAddingToFB.ItemsHasCondition_ConditionContextId,
                                AddedItems = ItemsAddingToFB.AddedItems,
                                Condition = ItemsAddingToFB.Condition,
                                FinalWorking = ItemsAddingToFB.FinalWorking,
                                ConditionType = ItemsAddingToFB.ConditionType,
                                DesOfAddingItems = ItemsAddingToFB.DesOfAddingItems,
                                UseItemForAdd = ItemsAddingToFB.UseItemForAdd,
                                FieldsAdding = ItemsAddingToFB.FieldsAdding,
                                CharacterPlus = ItemsAddingToFB.CharacterPlus,
                                Shomareh = FehrestBaha.Shomareh,
                                Sharh = FehrestBaha.Sharh
                            };

                            lstItemsAddingToFBForShowAddItems1.Add(ItemsAddingToFBForShowAddItems);
                        }
                    }
                }





                //for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
                foreach (var itemHCATFB in varItemsHasConditionAddedToFB)
                {
                    decimal Meghdar = itemHCATFB.Meghdar;// decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
                    string RBCode = itemHCATFB.ItemsHasCondition_ConditionContextId.ToString();// DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();



                    long ItemsHasCondition_ConditionContextId = long.Parse(RBCode);

                    clsItemsHasConditionAddedToFB? itemsHasConditionAddedToFB =
                        _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.FBShomareh.Substring(0, 6) == strItemsFBShomareh &&
                            x.BarAvordId == BarAvordId && x.ItemsHasCondition_ConditionContextId == ItemsHasCondition_ConditionContextId && x.NoeFB == NoeFBId);


                    List<ItemsAddingToFBForShowAddItemsDto> ItemsAddingToFB =
                        lstItemsAddingToFBForShowAddItems1.Where(x => x.ItemsHasCondition_ConditionContextId == long.Parse(RBCode)).ToList();

                    if (ItemsAddingToFB != null)
                    {
                        foreach (var ItemsAdding in ItemsAddingToFB)
                        {
                            string strFBShomarehAdded = ItemsAdding.AddedItems; //?ItemsAdding.AddedItems.ToString().Trim();
                            string strCharacterPlus = ItemsAdding.CharacterPlus == null ? "" : ItemsAdding.CharacterPlus.ToString().Trim();// ItemsAdding.CharacterPlus.ToString().Trim();

                            switch (ItemsAdding.ConditionType.ToString())
                            {
                                case "1":
                                    {
                                        if (itemsHasConditionAddedToFB != null)
                                        {
                                            List<Guid> lstRizMetresIds =
                                                _context.ItemsHasConditionAddedToFBRizMetres.Where(x => x.ItemsHasConditionAddedToFBId == itemsHasConditionAddedToFB.ID).Select(x => x.RizMetreId).ToList();
                                            List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstRizMetresIds.Contains(x.ID)).ToList();

                                            _context.RizMetreUserses.RemoveRange(lstRizMetre);

                                            ///////////
                                            //حذف حمل//
                                            ///////////
                                            ///
                                            foreach (var RMetre in lstRizMetre)
                                            {
                                                DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                {
                                                    RIzMetreId = RMetre.ID,
                                                    BarAvordId = BarAvordId,
                                                    FBShomareh = strItemsFBShomareh,
                                                    RMShomareh = RMetre.Shomareh,
                                                    NoeFBId = NoeFBId
                                                };
                                                await HamlCommon.DeleteHaml(deleteHaml, _context);
                                            }
                                        }



                                        //var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        //if (DtFBUsersAdded.Rows.Count != 0)
                                        //{
                                        //    Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                        //    clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                        //    if (Fb != null)
                                        //    {
                                        //        _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                        //        _context.FBs.Remove(Fb);



                                        //        ///////////
                                        //        //حذف حمل//
                                        //        ///////////
                                        //        ///

                                        //        List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ToList();
                                        //        foreach (var item in lstRM)
                                        //        {
                                        //            DeleteHamlDto deleteHaml = new DeleteHamlDto
                                        //            {
                                        //                BarAvordId = BarAvordId,
                                        //                FBShomareh = Fb.Shomareh,
                                        //                RMShomareh = item.Shomareh,
                                        //                NoeFBId = NoeFBId
                                        //            };
                                        //            await HamlCommon.DeleteHaml(deleteHaml, context);
                                        //        }
                                        //        /////////////
                                        //        //////////////
                                        //        //////////////

                                        //        _context.SaveChanges();
                                        //    }
                                        //}
                                        break;
                                    }
                                case "2":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        //if (DtFBUsersAdded.Rows.Count != 0)
                                        if (varFBUsersAdded != null)
                                        {
                                            Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            var RizMetre1 = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim() && x.LevelNumber == LevelNumber).ToList();
                                            if (RizMetre1 != null)
                                            {
                                                if (RizMetre1.Count != 0)
                                                {
                                                    _context.RizMetreUserses.RemoveRange(RizMetre1);

                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///

                                                    foreach (var item in RizMetre1)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId = item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = varFBUsersAdded.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFBId
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////
                                                    //////////////
                                                    _context.SaveChanges();
                                                }
                                            }
                                        }
                                        break;
                                    }
                                case "3":
                                    {
                                        decimal dPercent = decimal.Parse(ItemsAdding.FinalWorking.ToString()); //decimal.Parse(Dr[0]["FinalWorking.ToString());
                                        string strStatus = dPercent > 0 ? "B" : "e";
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strStatus).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                                _context.FBs.Remove(Fb);

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///

                                                List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ToList();
                                                foreach (var item in lstRM)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        RIzMetreId = item.ID,
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                                //////////////

                                                _context.SaveChanges();
                                            }
                                        }
                                        break;
                                    }
                                case "4":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                        //if (DtFBUsersAdded.Rows.Count != 0)
                                        if (varFBUsersAdded != null)
                                        {
                                            string strCondition = ItemsAdding.Condition;
                                            string strFinalWorking = ItemsAdding.FinalWorking;
                                            string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                            StringToFormula StringToFormula = new StringToFormula();
                                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                            if (blnCheck)
                                            {
                                                string strForItem = "";
                                                string strUseItem = "";

                                                if (ItemsAdding.UseItemForAdd.Trim() == "")
                                                    strForItem = varFBShomareh;//DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                else
                                                {
                                                    strForItem = ItemsAdding.UseItemForAdd.Trim();
                                                    strUseItem = varFBShomareh;//DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                }

                                                Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var RizMetre1 = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strForItem && x.UseItem == strUseItem && x.LevelNumber == LevelNumber).ToList();
                                                if (RizMetre1 != null)
                                                {
                                                    if (RizMetre1.Count != 0)
                                                    {
                                                        _context.RizMetreUserses.RemoveRange(RizMetre1);

                                                        ///////////
                                                        //حذف حمل//
                                                        ///////////
                                                        ///
                                                        foreach (var item in RizMetre1)
                                                        {
                                                            DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                            {
                                                                BarAvordId = BarAvordId,
                                                                FBShomareh = varFBUsersAdded.Shomareh,
                                                                RMShomareh = item.Shomareh,
                                                                NoeFBId = NoeFBId
                                                            };
                                                            await HamlCommon.DeleteHaml(deleteHaml, context);
                                                        }
                                                        /////////////
                                                        //////////////
                                                        //////////////

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
                                case "7":
                                    {
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded && x.NoeFBId == NoeFBId).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///

                                                List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).ToList();
                                                foreach (var item in lstRM)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                                //////////////

                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).ExecuteDelete();
                                                _context.SaveChanges();
                                            }
                                        }
                                        break;
                                    }
                                case "6":
                                    {
                                        var varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (varFBUsersAdded != null)
                                        {
                                            Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///

                                                List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim() && x.Type == "2" && x.LevelNumber == LevelNumber).ToList();
                                                foreach (var item in lstRM)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }

                                                List<clsRizMetreUsers> lstRM1 = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim() && x.Type == "3").ToList();
                                                foreach (var item in lstRM1)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                                //////////////

                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim() && x.Type == "2" && x.LevelNumber == LevelNumber).ExecuteDelete();
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim() && x.Type == "3").ExecuteDelete();


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

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim() && x.LevelNumber == LevelNumber).ToList();
                                                foreach (var item in lstRM)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                                //////////////

                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim() && x.LevelNumber == LevelNumber).ExecuteDelete();

                                                _context.SaveChanges();
                                            }
                                        }

                                        break;
                                    }
                                case "10":
                                    {
                                        strCharacterPlus = ItemsAdding.CharacterPlus == null ? "" : ItemsAdding.CharacterPlus.Trim().ToString();
                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {

                                            //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                            //strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            //StringToFormula StringToFormula = new StringToFormula();
                                            //decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).ToList();
                                                foreach (var item in lstRM)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                                //////////////
                                                ///

                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).ExecuteDelete();

                                                _context.SaveChanges();
                                            }
                                        }
                                        break;
                                    }
                                case "11":
                                    {
                                        string strCharacterPlus1 = ItemsAdding.CharacterPlus == null ? "" : ItemsAdding.CharacterPlus.ToString();
                                        strItemsFBShomareh = ItemsAdding.Shomareh == null ? "" : ItemsAdding.Shomareh;

                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemsFBShomareh + strCharacterPlus1).ToList();
                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        if (DtFBUsersAdded.Rows.Count != 0)
                                        {
                                            Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            if (Fb != null)
                                            {

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim() + strCharacterPlus1).ToList();
                                                foreach (var item in lstRM)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                                //////////////
                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).ExecuteDelete();


                                                _context.SaveChanges();
                                            }
                                        }
                                        break;
                                    }
                                case "12":
                                    {
                                        string strCondition = ItemsAdding.Condition.ToString().Trim();
                                        string[] strConditionSplit = strCondition.Split("_");

                                        string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                        DataTable DtRizMetreUserses = new DataTable();
                                        string strForItem = "";
                                        string strUseItem = "";
                                        string strItemFBShomareh = varFBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();

                                        Guid guFBId = BaseFBId;
                                        strUseItem = varFBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                        strForItem = ItemsAdding.UseItemForAdd.ToString().Trim();

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


                                                    if (strFinalWorking != "")
                                                    {
                                                        strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");
                                                        ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                                                        ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                                                        ///Ertefa-3
                                                        decimal Ertefa = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                                                        string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                        clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                                        Guid FBId = new Guid();
                                                        if (varFBUser != null)
                                                        {
                                                            FBId = varFBUser.ID;

                                                            List<clsRizMetreUsers> RizMetre1 = varRizMetreUsersesCurrent.Where(x => x.Shomareh == RM.Shomareh && x.FBId == FBId).ToList();
                                                            if (RizMetre1 != null)
                                                            {
                                                                if (RizMetre1.Count != 0)
                                                                {
                                                                    _context.RizMetreUserses.RemoveRange(RizMetre1);


                                                                    ///////////
                                                                    //حذف حمل//
                                                                    ///////////
                                                                    ///

                                                                    foreach (var item in RizMetre1)
                                                                    {
                                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                                        {
                                                                            BarAvordId = BarAvordId,
                                                                            FBShomareh = varFBUser.Shomareh,
                                                                            RMShomareh = item.Shomareh,
                                                                            NoeFBId = NoeFBId
                                                                        };
                                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                                    }
                                                                    /////////////
                                                                    //////////////
                                                                    //////////////
                                                                    _context.SaveChanges();
                                                                }
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
                                        decimal Meghdar2 = itemHCATFB.Meghdar2;
                                        decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);

                                        string[] strCondition = ItemsAdding.Condition.ToString().Trim().Split("_");
                                        string strCondition1 = strCondition[0];
                                        string strCondition2 = strCondition[1];
                                        string strFinalWorking = ItemsAdding.FinalWorking == null ? "" : ItemsAdding.FinalWorking.ToString();
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

                                                if (varBA != null)
                                                {
                                                    Guid guBAId = varBA.ID;
                                                    string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh);

                                                    Guid FBId = varFBUser.ID;

                                                    string strShomareh1 = varFBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                                    //Guid guFBId = BaseFBId;
                                                    List<clsRizMetreUsers> RizMetre1 = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                                    if (RizMetre1 != null)
                                                    {
                                                        if (RizMetre1.Count != 0)
                                                        {
                                                            _context.RizMetreUserses.RemoveRange(RizMetre1);

                                                            ///////////
                                                            //حذف حمل//
                                                            ///////////
                                                            ///
                                                            foreach (var item in RizMetre1)
                                                            {
                                                                DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                                {
                                                                    BarAvordId = BarAvordId,
                                                                    FBShomareh = varFBUser.Shomareh,
                                                                    RMShomareh = item.Shomareh,
                                                                    NoeFBId = NoeFBId
                                                                };
                                                                await HamlCommon.DeleteHaml(deleteHaml, context);
                                                            }
                                                            /////////////
                                                            //////////////
                                                            //////////////
                                                            ///
                                                            _context.SaveChanges();
                                                        }
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


                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).ToList();
                                                foreach (var item in lstRM)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = Fb.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                                //////////////

                                                _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).ExecuteDelete();

                                                _context.SaveChanges();
                                            }
                                        }
                                        break;
                                    }
                                case "17":
                                case "18":
                                    {
                                        string strCharacterPlus1 = ItemsAdding.CharacterPlus != null ? ItemsAdding.CharacterPlus.ToString().Trim() : "";

                                        var varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus1);

                                        if (varFBUsersAdded != null)
                                        {
                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                            List<clsRizMetreUsers> lstRizMetres = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim()
                                                && x.LevelNumber == LevelNumber).ToList();
                                            if (lstRizMetres != null)
                                            {
                                                _context.RizMetreUserses.RemoveRange(lstRizMetres);

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                foreach (var item in lstRizMetres)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        RIzMetreId = item.ID,
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = varFBUsersAdded.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFBId
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                            }
                                        }

                                        break;
                                    }
                                case "19":
                                    {
                                        /////////////
                                        /////حذف قبلی ها
                                        /////////////

                                        string strCondition = ItemsAdding.Condition.ToString().Trim();
                                        string[] strConditionSplit = strCondition.Split("_");

                                        string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                        DataTable DtRizMetreUserses = new DataTable();
                                        string strForItem = "";
                                        string strUseItem = "";
                                        string strItemFBShomareh = varFBShomareh;//DtFB.Rows[0]["Shomareh"].ToString().Trim();

                                        Guid guFBId = BaseFBId;
                                        strUseItem = varFBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                        strForItem = ItemsAdding.UseItemForAdd.ToString().Trim();

                                        List<clsRizMetreUsers> varRizMetreUsersesCurrent = _context.RizMetreUserses
                                            .Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();


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

                                            string[] strCondition2Condition = strCondition.Trim().Split(',');
                                            string strConditionY = strCondition2Condition[0].Trim();
                                            string strConditionZ = strCondition2Condition[1].Trim();

                                            string strConditionOpY = strConditionY.Replace("y", RM.Arz != null ? RM.Arz.Value.ToString().Trim() : "");
                                            string strConditionOpZ = strConditionZ.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");

                                            StringToFormula stringToFormula = new StringToFormula();
                                            bool blnCheckY = stringToFormula.RelationalExpression2(strConditionOpY);
                                            bool blnCheckZ = stringToFormula.RelationalExpression2(strConditionOpZ);
                                            if (blnCheckY && blnCheckZ)
                                            {
                                                string strItemShomareh1 = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus;

                                                clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh1).FirstOrDefault();

                                                Guid intFBId1 = new Guid();
                                                if (FBUser != null)
                                                {
                                                    intFBId1 = FBUser.ID;

                                                    string strShomareh1 = strItemFBShomareh.Substring(0, 6);

                                                    List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                                                                    _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RM.Shomareh).ToList();

                                                    _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);


                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    foreach (var item in lstRizMetreUsersCurrent)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId=item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = FBUser.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFBId
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////
                                                    //////////////
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

                    Guid guItemsHasConditionAddedToFBId = itemHCATFB.ID;
                    clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB = _context.ItemsHasConditionAddedToFBs.Where(x => x.ID == guItemsHasConditionAddedToFBId).FirstOrDefault();
                    if (ItemsHasConditionAddedToFB != null)
                    {
                        _context.ItemsHasConditionAddedToFBs.Remove(ItemsHasConditionAddedToFB);
                        await _context.SaveChangesAsync();
                    }
                    //clsItemsHasConditionAddedToFB.Delete("Id=" + DtItemsHasConditionAddedToFB.Rows[Counter]["Id"].ToString());
                }
            }

            string strErrors = "";
            string strWarning = "شرایط لازم برای اعمال این آیتم برقرار نیست، لطفا اطلاعات ورودی را بررسی نمایید";
            bool Status = false;

            string blnCheckIsExistErrors = "";
            bool blnCheckIsExistWarning = true;


            List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToList();


            List<ItemsAddingToFBForShowAddItemsDto> lstItemsAddingToFB = _context.ItemsAddingToFBs
                .Include(x => x.ItemsHasCondition_ConditionContext).ThenInclude(x => x.ConditionContext)
                    .Where(x => x.Year == Year && x.NoeFB == NoeFBId)
                    .Select(ItemsAddingToFB => new ItemsAddingToFBForShowAddItemsDto
                    {
                        Id = ItemsAddingToFB.Id,
                        ItemsHasCondition_ConditionContextId = ItemsAddingToFB.ItemsHasCondition_ConditionContextId,
                        AddedItems = ItemsAddingToFB.AddedItems,
                        Condition = ItemsAddingToFB.Condition,
                        FinalWorking = ItemsAddingToFB.FinalWorking,
                        ConditionType = ItemsAddingToFB.ConditionType,
                        DesOfAddingItems = ItemsAddingToFB.DesOfAddingItems,
                        UseItemForAdd = ItemsAddingToFB.UseItemForAdd,
                        FieldsAdding = ItemsAddingToFB.FieldsAdding,
                        CharacterPlus = ItemsAddingToFB.CharacterPlus,
                        ConditionContextRel = ItemsAddingToFB.ItemsHasCondition_ConditionContext.ConditionContext.ConditionContextRel == null ? "" : ItemsAddingToFB.ItemsHasCondition_ConditionContext.ConditionContext.ConditionContextRel,
                        ConditionContextId = ItemsAddingToFB.ItemsHasCondition_ConditionContext.ConditionContextId,
                        MaxValueCheck = ItemsAddingToFB.ItemsHasCondition_ConditionContext.MaxValue,
                        MinValueCheck = ItemsAddingToFB.ItemsHasCondition_ConditionContext.MinValue,
                        EnteringCount = ItemsAddingToFB.ItemsHasCondition_ConditionContext.EnteringCount,

                    }).ToList();

            List<ItemsAddingToFBForShowAddItemsDto> lstItemsAddingToFBForShowAddItems = new List<ItemsAddingToFBForShowAddItemsDto>();


            // تمام شماره‌های آیتم‌های اضافه شده را یکجا استخراج می‌کنیم
            var shomarehList = lstItemsAddingToFB
                 .SelectMany(x => x.AddedItems.Split('_'))
                 .Select(x => x.Trim())
                 .Where(x => !string.IsNullOrWhiteSpace(x))
                 .Distinct()
                 .ToList();

            // فقط یک بار از دیتابیس دریافت می‌کنیم
            var fehrestBahaDict = _context.FehrestBahas
                .Where(x =>
                    x.Sal == Year &&
                    x.NoeFB == NoeFBId &&
                    shomarehList.Contains(x.Shomareh.Trim()))
                .ToDictionary(x => x.Shomareh.Trim(), x => x);

            // ساخت لیست نهایی
            foreach (var item in lstItemsAddingToFB)
            {
                foreach (var shomareh in item.AddedItems.Split('_'))
                {
                    var shomareh1 = shomareh.Trim();

                    if (string.IsNullOrWhiteSpace(shomareh1))
                        continue;

                    if (!fehrestBahaDict.TryGetValue(shomareh1, out var fehrestBaha))
                        continue;

                    lstItemsAddingToFBForShowAddItems.Add(
                        new ItemsAddingToFBForShowAddItemsDto
                        {
                            Id = item.Id,
                            ItemsHasCondition_ConditionContextId =
                                item.ItemsHasCondition_ConditionContextId,

                            AddedItems = item.AddedItems,
                            Condition = item.Condition,
                            FinalWorking = item.FinalWorking,
                            ConditionType = item.ConditionType,
                            DesOfAddingItems = item.DesOfAddingItems,
                            UseItemForAdd = item.UseItemForAdd,
                            FieldsAdding = item.FieldsAdding,
                            CharacterPlus = item.CharacterPlus,
                            ConditionContextRel = item.ConditionContextRel,
                            ConditionContextId = item.ConditionContextId,

                            Shomareh = fehrestBaha.Shomareh,
                            Sharh = fehrestBaha.Sharh,
                            MaxValueCheck = item.MaxValueCheck == null ? "" : item.MaxValueCheck,
                            MinValueCheck = item.MinValueCheck == null ? "" : item.MinValueCheck,
                            EnteringCount = item.EnteringCount

                        });
                }
            }




            DateTime Now = DateTime.Now;

            long ShomareNew = 1;
            clsRizMetreUsers? RizMetre = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.InsertDateTime).ThenByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordId);
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

            string[] RBCodeArray = StrRBCode.Split(',');
            for (int counter = 0; counter < RBCodeArray.Length - 1; counter++)
            {


                string[] RBCodeArraySplit = RBCodeArray[counter].Split('_');
                int RBCode = int.Parse(RBCodeArraySplit[0].ToString());
                decimal Meghdar = decimal.Parse(RBCodeArraySplit.Length > 1 ? RBCodeArraySplit[1].ToString() != "" ? RBCodeArraySplit[1].ToString() : "0" : "0");
                decimal Meghdar2 = decimal.Parse(RBCodeArraySplit.Length > 2 ? RBCodeArraySplit[2].ToString() != "" ? RBCodeArraySplit[2].ToString() : "0" : "0");

                if (Meghdar < 0)
                {
                    return new JsonResult("CI");//CheckInput
                }

                List<ItemsAddingToFBForShowAddItemsDto> ItemsAddingToFBForShowAddItems = lstItemsAddingToFBForShowAddItems.Where(x => x.ItemsHasCondition_ConditionContextId == RBCode).ToList();


                if (ItemsAddingToFBForShowAddItems.Count != 0)
                {
                    bool blnCheckAgain = true;

                    List<bool> blnResult1 = new List<bool>();

                    foreach (var ItemsAdding in ItemsAddingToFBForShowAddItems)
                    {

                        bool CheckMeghdar = CheckEntryMeghdar(ItemsAdding.MinValueCheck, ItemsAdding.MaxValueCheck, Meghdar, Meghdar2, ItemsAdding.EnteringCount);
                        if (!CheckMeghdar)
                        {
                            return new JsonResult("CI");//CheckInput
                        }

                        //string[] MaxValueCheck = ItemsAdding.MaxValueCheck == null ? "".Split('_') : ItemsAdding.MaxValueCheck.Split('_');
                        //string[] MinValueCheck = ItemsAdding.MinValueCheck == null ? "".Split('_') : ItemsAdding.MinValueCheck.Split('_');

                        string strCondition = ItemsAdding.Condition == null ? "" : ItemsAdding.Condition.ToString().Trim();
                        string strFinalWorking = ItemsAdding.FinalWorking == null ? "" : ItemsAdding.FinalWorking.ToString();
                        string? ConditionContextRel = ItemsAdding.ConditionContextRel == null ? "" : ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                        string FieldsAdding = ItemsAdding.FieldsAdding == null ? "" : ItemsAdding.FieldsAdding.ToString();
                        long? ConditionContextId = long.TryParse(ItemsAdding.ConditionContextId.ToString().Trim()?.ToString()?.Trim(), out var id) ? id : null;
                        string strCharacterPlus = ItemsAdding.CharacterPlus == null ? "" : ItemsAdding.CharacterPlus.ToString().Trim();
                        long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                        string FBShomarehAddedItem = ItemsAdding.AddedItems.ToString().Trim();
                        string DesOfAddingItems = ItemsAdding.DesOfAddingItems == null ? "" : ItemsAdding.DesOfAddingItems;

                        switch (ItemsAdding.ConditionType.ToString())
                        {

                            case "1":
                                {
                                    ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                                    {
                                        strFinalWorking = strFinalWorking,
                                        Meghdar = Meghdar,
                                        Meghdar2 = Meghdar2,
                                        strCondition = strCondition,
                                        strItemsFBShomareh = strItemsFBShomareh,
                                        lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                                        ConditionGroupId = ConditionGroupId,
                                        BarAvordId = BarAvordId,
                                        NoeFBId = NoeFBId,
                                        strCharacterPlus = strCharacterPlus,
                                        FBShomarehAddedItem = FBShomarehAddedItem,
                                        DesOfAddingItems = DesOfAddingItems,
                                        BaseFBId = BaseFBId,
                                        Year = Year,
                                        FieldsAdding = FieldsAdding,
                                        ShomarehNew = ShomareNew,
                                        Now = Now,
                                        ConditionContextRel = ConditionContextRel,
                                        ConditionContextId = ConditionContextId,
                                        blnCheckIsExistErrors = blnCheckIsExistErrors,
                                        LevelNumber = LevelNumber,
                                        blnCheckAgain = blnCheckAgain,
                                        RMIdForEdit = null,
                                        blnCheckIsExistWarning = blnCheckIsExistWarning,
                                        strWarning = strWarning,
                                        Status = Status
                                    };

                                    ConditionCaseCommon CCC = new ConditionCaseCommon();
                                    ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case1(RequestCase1, context);

                                    blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                                    strWarning = ResultCCC.strWarning;
                                    Status = ResultCCC.blnStatus;

                                    blnResult1.Add(ResultCCC.blnStatus);

                                    if (Status)
                                    {
                                        await _context.SaveChangesAsync();
                                    }
                                    if (!blnCheckAgain)
                                    {
                                        ResultCCC.blnStatus = blnResult1.Contains(true);
                                        if (!ResultCCC.blnStatus)
                                        {
                                            clsFB? varFBUsersAdded = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus);
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                                    _context.FBs.Remove(Fb);
                                                    await _context.SaveChangesAsync();
                                                }
                                            }
                                            return new JsonResult("CI");//CheckInput

                                        }
                                    }
                                    //decimal dSumAllRizMetre = 0;
                                    //var matchCondition = Regex.Match(strFinalWorking, @"\b([a-zA-Z])\b");
                                    //string variableNameCondition = matchCondition.Success ? matchCondition.Groups[1].Value : null;
                                    //string strConditionOp = "";
                                    //decimal FinalMeghdar = (Meghdar == 0 ? 1 : Meghdar) * (Meghdar2 == 0 ? 1 : Meghdar2);

                                    /////طبق متغییر موجود در شرط بررسی میگردد که مقدار ورودی جایگزین کدام متغییر گردد
                                    /////و در نهایت برای فیلدهای طول عرض ارتفاع وزن استفاده میگردد
                                    //switch (variableNameCondition)
                                    //{
                                    //    case "x":
                                    //        {
                                    //            strConditionOp = strCondition.Replace("x", FinalMeghdar.ToString().Trim());
                                    //            break;
                                    //        }
                                    //    case "y":
                                    //        {
                                    //            strConditionOp = strCondition.Replace("y", FinalMeghdar.ToString().Trim());
                                    //            break;
                                    //        }
                                    //    case "z":
                                    //        {
                                    //            strConditionOp = strCondition.Replace("z", FinalMeghdar.ToString().Trim());
                                    //            break;
                                    //        }
                                    //    case "w":
                                    //        {
                                    //            strConditionOp = strCondition.Replace("w", FinalMeghdar.ToString().Trim());
                                    //            break;
                                    //        }
                                    //    default:
                                    //        break;
                                    //}


                                    //bool blnCheck = true;
                                    //StringToFormula StringToFormula = new StringToFormula();
                                    //if (strConditionOp != "")
                                    //{
                                    //    blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    //}
                                    //if (blnCheck)
                                    //{
                                    //    Guid currentItemsHasConditionAddedToFBId = new Guid();

                                    //    blnCheckIsExistErrors = "false";

                                    //    bool blnCheckSave = false;


                                    //    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                    //                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                    //                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                    //    if (currentItemsHasConditionAddedToFBs == null)
                                    //    {
                                    //        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    //        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    //        ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;
                                    //        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                    //        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                    //        ItemsHasConditionAddedToFB.Meghdar2 = Meghdar2;
                                    //        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                    //        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                    //        try
                                    //        {
                                    //            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    //            await _context.SaveChangesAsync();
                                    //            blnCheckSave = true;
                                    //            currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                    //        }
                                    //        catch (Exception)
                                    //        {
                                    //            blnCheckSave = false;
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        blnCheckSave = true;
                                    //        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                    //    }


                                    //    if (blnCheckSave)
                                    //    {
                                    //        var match = Regex.Match(strFinalWorking, @"\b([a-zA-Z])\b");

                                    //        string variableName = match.Success ? match.Groups[1].Value : null;
                                    //        ///
                                    //        //مقدار ورودی بنا به این در بخش عملیات نهایی چه حرفی گذاشته شده است
                                    //        //در یکی از فیلدهای طول عرض ارتفاع وزن گذاشته میشود
                                    //        switch (variableName)
                                    //        {
                                    //            //طول
                                    //            case "x":
                                    //                {

                                    //                    strFinalWorking = strFinalWorking.Replace("x", FinalMeghdar.ToString().Trim());
                                    //                    break;
                                    //                }
                                    //            //عرض
                                    //            case "y":
                                    //                {
                                    //                    strFinalWorking = strFinalWorking.Replace("y", FinalMeghdar.ToString().Trim());
                                    //                    break;
                                    //                }
                                    //            //ارتفاع
                                    //            case "z":
                                    //                {
                                    //                    strFinalWorking = strFinalWorking.Replace("z", FinalMeghdar.ToString().Trim());
                                    //                    break;
                                    //                }
                                    //            //وزن
                                    //            case "w":
                                    //                {
                                    //                    strFinalWorking = strFinalWorking.Replace("w", FinalMeghdar.ToString().Trim());
                                    //                    break;
                                    //                }
                                    //            default:
                                    //                break;
                                    //        }

                                    //        decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                    //        string strDesOfAddingItems = ItemsAdding.DesOfAddingItems.ToString().Replace("x", FinalMeghdar.ToString().Trim()).Replace("y", strItemsFBShomareh).Replace("z", (dPercent * 100).ToString("0.##")).Trim();

                                    //        clsBaravordUser? varBA = _context.BaravordUsers.First(x => x.ID == BarAvordId);
                                    //        //DataTable DtBA = clsConvert.ToDataTable(varBA);

                                    //        Guid guBAId = varBA.ID; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());


                                    //        ///////////////////////////////////////
                                    //        /////فیلدهای آیتمی که درج میگردد//////
                                    //        ///////////////////////////////////////

                                    //        List<clsItemsFields> lstCurrentItemFields = await _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems.ToString().Trim() && x.NoeFB == NoeFBId).ToListAsync();


                                    //        string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus; //"A";
                                    //        clsFB? varFBUser = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);
                                    //        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);


                                    //        Guid FBId = new Guid();
                                    //        if (varFBUser == null)
                                    //        {
                                    //            clsFB FB = new clsFB();
                                    //            FB.BarAvordId = guBAId;//Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    //            FB.NoeFBId = NoeFBId;
                                    //            FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus; //"A";
                                    //            FB.BahayeVahedZarib = 0;// dPercent;
                                    //            FB.BahayeVahedSharh = strDesOfAddingItems;
                                    //            FB.NoeFBId = NoeFBId;
                                    //            _context.FBs.Add(FB);
                                    //            await _context.SaveChangesAsync();
                                    //            FBId = FB.ID;
                                    //            varFBUser = FB;

                                    //        }
                                    //        else
                                    //        {
                                    //            varFBUser.BahayeVahedSharh = strDesOfAddingItems;
                                    //            FBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                    //        }


                                    //        List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                    //                                                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                    //                                                                                  where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                                    //                                                                                  select new RizMetreUsersForItemsAddingToFBInputDto()
                                    //                                                                                  {
                                    //                                                                                      Id = RUsers.ID,
                                    //                                                                                      Shomareh = RUsers.Shomareh,
                                    //                                                                                      Sharh = RUsers.Sharh,
                                    //                                                                                      Tedad = RUsers.Tedad,
                                    //                                                                                      Tool = RUsers.Tool,
                                    //                                                                                      Arz = RUsers.Arz,
                                    //                                                                                      Ertefa = RUsers.Ertefa,
                                    //                                                                                      Vazn = RUsers.Vazn,
                                    //                                                                                      Des = RUsers.Des,
                                    //                                                                                      ForItem = RUsers.ForItem,
                                    //                                                                                      Type = RUsers.Type,
                                    //                                                                                      UseItem = RUsers.UseItem,
                                    //                                                                                      BarAvordId = fb.BarAvordId,
                                    //                                                                                      FBId = RUsers.FBId
                                    //                                                                                  }).OrderBy(x => x.Shomareh).ToListAsync();


                                    //        //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);


                                    //        string strShomareh1 = strItemsFBShomareh;//varFBShomareh;
                                    //        Guid guFBId = BaseFBId;

                                    //        var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
                                    //                                               join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                                    //                                               where RizMetreUserses.LevelNumber == LevelNumber
                                    //                                               select new
                                    //                                               {
                                    //                                                   ID = RizMetreUserses.ID,
                                    //                                                   Shomareh = RizMetreUserses.Shomareh,
                                    //                                                   Tedad = RizMetreUserses.Tedad,
                                    //                                                   Tool = RizMetreUserses.Tool,
                                    //                                                   Arz = RizMetreUserses.Arz,
                                    //                                                   Ertefa = RizMetreUserses.Ertefa,
                                    //                                                   Vazn = RizMetreUserses.Vazn,
                                    //                                                   Des = RizMetreUserses.Des,
                                    //                                                   FBId = RizMetreUserses.FBId,
                                    //                                                   OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                                    //                                                   ForItem = RizMetreUserses.ForItem,
                                    //                                                   Type = RizMetreUserses.Type,
                                    //                                                   UseItem = RizMetreUserses.UseItem,
                                    //                                                   BarAvordId = FB.BarAvordId
                                    //                                               }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();

                                    //        string[] FieldsAddingSplit = FieldsAdding.Split(",");

                                    //        foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                    //        {
                                    //            var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList(); //DtRizMetreUsersesCurrent.Select("shomareh=" + long.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                    //            if (DrRizMetreUsersesCurrent.Count == 0)
                                    //            {
                                    //                decimal? dTedad = null;
                                    //                decimal? dTool = null;
                                    //                decimal? dArz = null;
                                    //                decimal? dErtefa = null;
                                    //                decimal? dVazn = null;

                                    //                List<string> lst = new List<string>();
                                    //                for (int j = 0; j < FieldsAddingSplit.Length; j++)
                                    //                {
                                    //                    lst.Add(FieldsAddingSplit[j]);
                                    //                }

                                    //                var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                    //                if (strCal.Count != 0)
                                    //                {
                                    //                    var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                    //                    dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                    //                    if (tedadField != null)
                                    //                    {
                                    //                        if (tedadField.EssentialValue)
                                    //                            if (dTedad == null)
                                    //                                dTedad = 0;
                                    //                    }

                                    //                }
                                    //                strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                    //                if (strCal.Count != 0)
                                    //                {
                                    //                    var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                    //                    dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                    //                    if (toolField != null)
                                    //                    {
                                    //                        if (toolField.EssentialValue)
                                    //                            if (dTool == null)
                                    //                                dTool = 0;
                                    //                    }

                                    //                    if (variableName == "x")
                                    //                    {
                                    //                        dTool = dPercent;
                                    //                    }

                                    //                }
                                    //                strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                    //                if (strCal.Count != 0)
                                    //                {
                                    //                    var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                    //                    dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                    //                    if (arzField != null)
                                    //                    {
                                    //                        if (arzField.EssentialValue)
                                    //                            if (dArz == null)
                                    //                                dArz = 0;
                                    //                    }
                                    //                    if (variableName == "y")
                                    //                    {
                                    //                        dArz = dPercent;
                                    //                    }
                                    //                }
                                    //                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                    //                if (strCal.Count != 0)
                                    //                {

                                    //                    var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                    //                    dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                    //                    if (ertefaField != null)
                                    //                    {
                                    //                        if (ertefaField.EssentialValue)
                                    //                            if (dErtefa == null)
                                    //                                dErtefa = 0;
                                    //                    }
                                    //                    if (variableName == "z")
                                    //                    {
                                    //                        dErtefa = dPercent;
                                    //                    }
                                    //                }
                                    //                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                    //                if (strCal.Count != 0)
                                    //                {

                                    //                    var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                    //                    dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;
                                    //                    if (vaznField != null)
                                    //                    {
                                    //                        if (vaznField.EssentialValue)
                                    //                            if (dVazn == null)
                                    //                                dVazn = 0;
                                    //                    }
                                    //                    if (variableName == "w")
                                    //                    {
                                    //                        dVazn = dPercent;
                                    //                    }
                                    //                }

                                    //                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                    //                RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                    //                ShomareNew++;
                                    //                RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                    //                RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;// "].ToString().Trim();
                                    //                RizMetreUserses.Tedad = dTedad;
                                    //                RizMetreUserses.Tool = dTool;
                                    //                RizMetreUserses.Arz = dArz;
                                    //                RizMetreUserses.Ertefa = dErtefa;
                                    //                RizMetreUserses.Vazn = dVazn;
                                    //                RizMetreUserses.Des = DtRizMetreUserses11.Des;// "].ToString().Trim();// ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + varFBShomareh
                                    //                                                              //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                    //                RizMetreUserses.FBId = FBId;
                                    //                RizMetreUserses.OperationsOfHamlId = 1;
                                    //                RizMetreUserses.Type = "2";
                                    //                RizMetreUserses.ForItem = strItemsFBShomareh; //varFBShomareh;
                                    //                RizMetreUserses.UseItem = "";
                                    //                RizMetreUserses.LevelNumber = LevelNumber;
                                    //                RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                    //                RizMetreUserses.ConditionContextId = ConditionContextId;
                                    //                RizMetreUserses.InsertDateTime = Now;

                                    //                ///محاسبه مقدار جزء
                                    //                decimal? dMeghdarJoz = null;
                                    //                if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                                    //                    dMeghdarJoz = null;
                                    //                else
                                    //                    dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                    //                    (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                    //                RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                    //                _context.RizMetreUserses.Add(RizMetreUserses);


                                    //                _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                    //                    new clsItemsHasConditionAddedToFBRizMetre
                                    //                    {
                                    //                        ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                    //                        RizMetreId = RizMetreUserses.ID,
                                    //                        BaseRizMetreId = DtRizMetreUserses11.Id
                                    //                    });

                                    //                dSumAllRizMetre += dMeghdarJoz ?? 0;


                                    //                //Start///////////////////////
                                    //                ////////////حمل///////////////
                                    //                //////////////////////////////
                                    //                ///آیتم های حمل درج میگردد
                                    //                ///

                                    //                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                    //                {
                                    //                    BarAvordUserId = guBAId,
                                    //                    Year = Year,
                                    //                    ItemFBShomareh = strItemShomareh,
                                    //                    NoeFBId = NoeFBId,
                                    //                    //BarAvordHamlId = gBarAvordHamlId,
                                    //                    Shomareh = RizMetreUserses.Shomareh,
                                    //                    MeghdarJoz = dMeghdarJoz,
                                    //                    LevelNumber = 1,
                                    //                };
                                    //                await HamlCommon.SaveHaml(requestSaveHaml, context);
                                    //                //RizMetreUserses.Save();
                                    //            }
                                    //        }

                                    //        /////////////////////////////////////
                                    //        //////دریافت جمع ریزمتره های جاری///
                                    //        /////////////////////////////////////
                                    //        decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                    //        decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                    //               ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                    //               : null;

                                    //        varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                    //        await _context.SaveChangesAsync();
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    //if (blnCheckIsExistErrors != "false")
                                    //    //{
                                    //    //    blnCheckIsExistErrors = "true";
                                    //    //    strErrors = strCondition.Replace("x", "عدد وارد شده");
                                    //    //}

                                    //    clsFB? varFBUsersAdded = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus);
                                    //    if (varFBUsersAdded != null)
                                    //    {
                                    //        Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                    //        clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                    //        if (Fb != null)
                                    //        {
                                    //            _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                    //            _context.FBs.Remove(Fb);
                                    //            await _context.SaveChangesAsync();
                                    //        }
                                    //    }
                                    //    return new JsonResult("CI");//CheckInput
                                    //}
                                    break;
                                }
                            //case "2":
                            //    {

                            //        decimal dSumAllRizMetre = 0;
                            //        //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();

                            //        //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                            //        //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                            //        string strItemsFBShomareh1 = strItemsFBShomareh; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();


                            //        //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());

                            //        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                            //                         .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemsFBShomareh1 && x.NoeFB == NoeFBId &&
                            //                             x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);
                            //        bool blnCheckSave = false;

                            //        Guid currentItemsHasConditionAddedToFBId = new Guid();
                            //        if (currentItemsHasConditionAddedToFBs == null)
                            //        {
                            //            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                            //            ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                            //            ItemsHasConditionAddedToFB.FBShomareh = strItemsFBShomareh1; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                            //            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                            //            ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                            //            ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                            //            ItemsHasConditionAddedToFB.NoeFB = NoeFBId;


                            //            try
                            //            {
                            //                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                            //                await _context.SaveChangesAsync();
                            //                blnCheckSave = true;
                            //                currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                            //            }
                            //            catch (Exception)
                            //            {
                            //                blnCheckSave = false;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                            //            blnCheckSave = true;
                            //        }



                            //        if (blnCheckSave)
                            //        {
                            //            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();

                            //            ///در صورتی که خالی نباشد در  جای مشخص شده درج مینمایم
                            //            ///x= طول
                            //            ///y= عرض
                            //            ///z= ارتفاع
                            //            //string strFinalWorking = ItemsAdding.FinalWorking == null ? "" : ItemsAdding.FinalWorking.ToString().Trim();

                            //            DataTable DtBA = clsConvert.ToDataTable(varBA);

                            //            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            //            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();


                            //            ///////////////////////////////////////
                            //            /////فیلدهای آیتمی که درج میگردد//////
                            //            ///////////////////////////////////////
                            //            List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems && x.NoeFB == NoeFBId).ToList();

                            //            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);
                            //            Guid FBId = new Guid();
                            //            if (varFBUser == null)
                            //            {
                            //                clsFB FB = new clsFB();
                            //                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            //                FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                            //                FB.BahayeVahedZarib = 0;
                            //                FB.BahayeVahedSharh = "";
                            //                FB.NoeFBId = NoeFBId;
                            //                _context.FBs.Add(FB);
                            //                await _context.SaveChangesAsync();
                            //                FBId = FB.ID;
                            //                varFBUser = FB;
                            //            }
                            //            else
                            //                FBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                            //            List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                            //                                                                                join fb in _context.FBs on RUsers.FBId equals fb.ID
                            //                                                                                where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                            //                                                                                select new RizMetreUsersForItemsAddingToFBInputDto()
                            //                                                                                {
                            //                                                                                    Id = RUsers.ID,
                            //                                                                                    Shomareh = RUsers.Shomareh,
                            //                                                                                    Sharh = RUsers.Sharh,
                            //                                                                                    Tedad = RUsers.Tedad,
                            //                                                                                    Tool = RUsers.Tool,
                            //                                                                                    Arz = RUsers.Arz,
                            //                                                                                    Ertefa = RUsers.Ertefa,
                            //                                                                                    Vazn = RUsers.Vazn,
                            //                                                                                    Des = RUsers.Des,
                            //                                                                                    ForItem = RUsers.ForItem,
                            //                                                                                    Type = RUsers.Type,
                            //                                                                                    UseItem = RUsers.UseItem,
                            //                                                                                    BarAvordId = fb.BarAvordId,
                            //                                                                                    FBId = RUsers.FBId
                            //                                                                                }).OrderBy(x => x.Shomareh).ToList();

                            //            //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                            //            string[] FieldsAddingSplit = FieldsAdding.Split(",");

                            //            string strShomareh1 = varFBShomareh;
                            //            Guid guFBId = BaseFBId;
                            //            var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                            //                                             join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                            //                                             where RizMetreUserses.LevelNumber == LevelNumber
                            //                                             select new
                            //                                             {
                            //                                                 ID = RizMetreUserses.ID,
                            //                                                 Shomareh = RizMetreUserses.Shomareh,
                            //                                                 Tedad = RizMetreUserses.Tedad,
                            //                                                 Tool = RizMetreUserses.Tool,
                            //                                                 Arz = RizMetreUserses.Arz,
                            //                                                 Ertefa = RizMetreUserses.Ertefa,
                            //                                                 Vazn = RizMetreUserses.Vazn,
                            //                                                 Des = RizMetreUserses.Des,
                            //                                                 FBId = RizMetreUserses.FBId,
                            //                                                 OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                            //                                                 ForItem = RizMetreUserses.ForItem,
                            //                                                 Type = RizMetreUserses.Type,
                            //                                                 UseItem = RizMetreUserses.UseItem,
                            //                                                 BarAvordId = FB.BarAvordId
                            //                                             }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                            //            DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                            //            foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                            //            {
                            //                var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                            //                if (DrRizMetreUsersesCurrent.Count == 0)
                            //                {

                            //                    //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                            //                    //{
                            //                    //DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                            //                    //if (DrRizMetreUsersesCurrent.Length == 0)
                            //                    //{




                            //                    List<string> lst = new List<string>();
                            //                    for (int j = 0; j < FieldsAddingSplit.Length; j++)
                            //                    {
                            //                        lst.Add(FieldsAddingSplit[j]);
                            //                    }

                            //                    decimal? dTedad = null;
                            //                    decimal? dTool = null;
                            //                    decimal? dArz = null;
                            //                    decimal? dErtefa = null;
                            //                    decimal? dVazn = null;



                            //                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                            //                    if (strCal.Count != 0)
                            //                    {

                            //                        clsItemsFields? tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                            //                        dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                            //                        if (tedadField != null)
                            //                        {
                            //                            if (tedadField.EssentialValue)
                            //                                if (dTedad == null)
                            //                                    dTedad = 0;
                            //                        }

                            //                    }
                            //                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                            //                    if (strCal.Count != 0)
                            //                    {
                            //                        clsItemsFields? toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                            //                        dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                            //                        if (toolField != null)
                            //                        {
                            //                            if (toolField.EssentialValue)
                            //                                if (dTool == null)
                            //                                    dTool = 0;
                            //                        }
                            //                    }
                            //                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                            //                    if (strCal.Count != 0)
                            //                    {
                            //                        clsItemsFields? arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                            //                        dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                            //                        if (arzField != null)
                            //                        {
                            //                            if (arzField.EssentialValue)
                            //                                if (dArz == null)
                            //                                    dArz = 0;
                            //                        }

                            //                    }
                            //                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                            //                    if (strCal.Count != 0)
                            //                    {
                            //                        clsItemsFields? ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                            //                        dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                            //                        if (ertefaField != null)
                            //                        {
                            //                            if (ertefaField.EssentialValue)
                            //                                if (dErtefa == null)
                            //                                    dErtefa = 0;
                            //                        }

                            //                    }
                            //                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                            //                    if (strCal.Count != 0)
                            //                    {
                            //                        clsItemsFields? vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                            //                        dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;
                            //                        if (vaznField != null)
                            //                        {
                            //                            if (vaznField.EssentialValue)
                            //                                if (dVazn == null)
                            //                                    dVazn = 0;
                            //                        }
                            //                    }

                            //                    if (strFinalWorking != "")
                            //                    {
                            //                        string[] FinalWorkingSplit = strFinalWorking.Split("=");

                            //                        switch (FinalWorkingSplit[0])
                            //                        {
                            //                            case "x":
                            //                                {
                            //                                    dTool = decimal.Parse(FinalWorkingSplit[1]);
                            //                                    break;
                            //                                }
                            //                            case "y":
                            //                                {
                            //                                    dArz = decimal.Parse(FinalWorkingSplit[1]);
                            //                                    break;
                            //                                }
                            //                            case "z":
                            //                                {
                            //                                    dErtefa = decimal.Parse(FinalWorkingSplit[1]);
                            //                                    break;
                            //                                }
                            //                            case "w":
                            //                                {
                            //                                    dVazn = decimal.Parse(FinalWorkingSplit[1]);
                            //                                    break;
                            //                                }
                            //                            default:
                            //                                {
                            //                                    break;
                            //                                }
                            //                        }
                            //                    }

                            //                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                            //                    RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                            //                    ShomareNew++;
                            //                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                            //                    RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;

                            //                    RizMetreUserses.Tedad = dTedad;
                            //                    RizMetreUserses.Tool = dTool;
                            //                    RizMetreUserses.Arz = dArz;
                            //                    RizMetreUserses.Ertefa = dErtefa;
                            //                    RizMetreUserses.Vazn = dVazn;

                            //                    RizMetreUserses.Des = DtRizMetreUserses11.Des; //Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + varFBShomareh
                            //                                                                   //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                            //                    RizMetreUserses.FBId = FBId;
                            //                    RizMetreUserses.OperationsOfHamlId = 1;
                            //                    RizMetreUserses.Type = "2";
                            //                    RizMetreUserses.ForItem = varFBShomareh;
                            //                    RizMetreUserses.UseItem = "";
                            //                    RizMetreUserses.LevelNumber = LevelNumber;
                            //                    RizMetreUserses.InsertDateTime = Now;

                            //                    ///محاسبه مقدار جزء
                            //                    decimal? dMeghdarJoz = null;
                            //                    if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                            //                        dMeghdarJoz = null;
                            //                    else
                            //                        dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                            //                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                            //                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;
                            //                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                            //                    RizMetreUserses.ConditionContextId = ConditionContextId;

                            //                    _context.RizMetreUserses.Add(RizMetreUserses);


                            //                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                            //                        new clsItemsHasConditionAddedToFBRizMetre
                            //                        {
                            //                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                            //                            RizMetreId = RizMetreUserses.ID,
                            //                            BaseRizMetreId = DtRizMetreUserses11.Id
                            //                        });


                            //                    dSumAllRizMetre += dMeghdarJoz ?? 0;


                            //                    //Start///////////////////////
                            //                    ////////////حمل///////////////
                            //                    //////////////////////////////
                            //                    ///آیتم های حمل درج میگردد
                            //                    ///


                            //                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                            //                    {
                            //                        BarAvordUserId = guBAId,
                            //                        Year = Year,
                            //                        ItemFBShomareh = strItemShomareh,
                            //                        NoeFBId = NoeFBId,
                            //                        //BarAvordHamlId = gBarAvordHamlId,
                            //                        Shomareh = RizMetreUserses.Shomareh,
                            //                        MeghdarJoz = dMeghdarJoz,
                            //                        LevelNumber = 1,
                            //                    };
                            //                    await HamlCommon.SaveHaml(requestSaveHaml, context);



                            //                }
                            //            }

                            //            /////////////////////////////////////
                            //            //////دریافت جمع ریزمتره های جاری///
                            //            /////////////////////////////////////
                            //            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                            //            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                            //                   ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                            //                   : null;

                            //            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                            //            await _context.SaveChangesAsync();
                            //        }
                            //        break;
                            //    }
                            case "3":
                                {

                                    ///در این حالت بایستی آیتم اصلی برگشت شده و ضریب ارسالی در بهای واحد ضرب شود
                                    ///و عدد بدست آمده در ضریب بهای واحد قرار گیرد
                                    ///
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    // string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    // long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    string ItemsFBShomareh = strItemsFBShomareh;//Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();


                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());

                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == ItemsFBShomareh && x.NoeFB == NoeFBId &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                    Guid currentItemsHasConditionAddedToFBId = new Guid();

                                    bool blnCheckSave = false;
                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = ItemsFBShomareh;
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            await _context.SaveChangesAsync();
                                            blnCheckSave = true;
                                            currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = true;
                                        }
                                    }
                                    else
                                    {
                                        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                        blnCheckSave = true;
                                    }


                                    decimal NewBahayeVahed = 0;



                                    //if (ItemsHasConditionAddedToFB.Save())
                                    if (blnCheckSave)
                                    {
                                        decimal dPercent = decimal.Parse(ItemsAdding.FinalWorking.ToString());
                                        clsFehrestBaha? FehrestBahasItem = _context.FehrestBahas.Where(x => x.Sal == Year && x.Shomareh == ItemsFBShomareh).FirstOrDefault();
                                        if (FehrestBahasItem != null)
                                        {
                                            string BahayeVahed1 = FehrestBahasItem.BahayeVahed == null ? "0" : FehrestBahasItem.BahayeVahed;
                                            NewBahayeVahed = decimal.Parse(BahayeVahed1) * dPercent;
                                        }
                                        var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                        DataTable DtBA = clsConvert.ToDataTable(varBA);

                                        ///////////////////////////////////////
                                        /////فیلدهای آیتمی که درج میگردد//////
                                        ///////////////////////////////////////

                                        List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems.ToString().Trim() && x.NoeFB == NoeFBId).ToList();

                                        Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();// + strStatus;
                                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);
                                        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                        Guid FBId = new Guid();
                                        if (varFBUser == null)
                                        {
                                            clsFB FB = new clsFB();
                                            FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            FB.Shomareh = strItemShomareh;
                                            FB.BahayeVahedZarib = NewBahayeVahed;
                                            FB.NoeFBId = NoeFBId;
                                            _context.FBs.Add(FB);
                                            await _context.SaveChangesAsync();
                                            FBId = FB.ID;
                                            varFBUser = FB;
                                        }
                                        else
                                        {
                                            FBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                        }
                                        List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                                                                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                  where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                                                                                                                  select new RizMetreUsersForItemsAddingToFBInputDto()
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
                                                                                                                      UseItem = RUsers.UseItem,
                                                                                                                      BarAvordId = fb.BarAvordId,
                                                                                                                      FBId = RUsers.FBId
                                                                                                                  }).OrderBy(x => x.Shomareh).ToListAsync();

                                        //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                        string strShomareh1 = varFBShomareh;
                                        Guid guFBId = BaseFBId;
                                        var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
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
                                                                               }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();
                                        DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                        //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                        //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + varFBShomareh + "' and Type=2");

                                        string[] FieldsAddingSplit = FieldsAdding.Split(",");

                                        foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                        {
                                            var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                            if (DrRizMetreUsersesCurrent.Count == 0)
                                            {

                                                //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                                //{
                                                //DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                //if (DrRizMetreUsersesCurrent.Length == 0)
                                                //{



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
                                                    var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                    dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                    dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                    dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                    dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                                    dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;
                                                }

                                                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                ShomareNew++;
                                                RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                RizMetreUserses.Tedad = dTedad;
                                                RizMetreUserses.Tool = dTool;
                                                RizMetreUserses.Arz = dArz;
                                                RizMetreUserses.Ertefa = dErtefa;
                                                RizMetreUserses.Vazn = dVazn;
                                                RizMetreUserses.Des = DtRizMetreUserses11.Des; //Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + varFBShomareh
                                                                                               //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                RizMetreUserses.FBId = FBId;
                                                RizMetreUserses.OperationsOfHamlId = 1;
                                                RizMetreUserses.Type = "2";
                                                RizMetreUserses.ForItem = varFBShomareh;
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

                                                dSumAllRizMetre += dMeghdarJoz ?? 0;


                                                //Start///////////////////////
                                                ////////////حمل///////////////
                                                //////////////////////////////
                                                ///آیتم های حمل درج میگردد
                                                ///

                                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                {
                                                    BarAvordUserId = guBAId,
                                                    Year = Year,
                                                    ItemFBShomareh = strItemShomareh,
                                                    NoeFBId = NoeFBId,
                                                    //BarAvordHamlId = gBarAvordHamlId,
                                                    Shomareh = RizMetreUserses.Shomareh,
                                                    MeghdarJoz = dMeghdarJoz,
                                                    LevelNumber = 1,
                                                };
                                                await HamlCommon.SaveHaml(requestSaveHaml, context);
                                            }
                                        }
                                        /////////////////////////////////////
                                        //////دریافت جمع ریزمتره های جاری///
                                        /////////////////////////////////////
                                        decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                        decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                               ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                               : null;

                                        varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                        await _context.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case "4":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCondition = ItemsAdding.Condition.ToString().Trim();
                                    //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                    StringToFormula StringToFormula = new StringToFormula();
                                    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    if (blnCheck)
                                    {
                                        //DataTable DtRizMetreUserses = new DataTable();
                                        List<RizMetreUsersesForConditionDto> varRizMetreUserses = new List<RizMetreUsersesForConditionDto>();

                                        string strForItem = "";
                                        string strUseItem = "";
                                        string strItemFBShomareh = varFBShomareh;
                                        if (ItemsAdding.UseItemForAdd.ToString().Trim() == "")
                                        {
                                            strForItem = strItemFBShomareh;
                                            Guid guFBId = BaseFBId;
                                            varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                  where RUsers.LevelNumber == LevelNumber
                                                                  select new RizMetreUsersesForConditionDto
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
                                                                      UseItem = RUsers.UseItem,
                                                                      BarAvordId = fb.BarAvordId,
                                                                      FBId = RUsers.FBId
                                                                  }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                            //DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);
                                            //DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                        }
                                        else
                                        {
                                            Guid guFBId = BaseFBId;
                                            strUseItem = varFBShomareh;
                                            strForItem = ItemsAdding.UseItemForAdd.ToString().Trim();
                                            var varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strForItem).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
                                            //DtFB = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strForItem + "'");
                                            if (varFB != null)
                                            {


                                                varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                      join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                      where RUsers.LevelNumber == LevelNumber
                                                                              && RUsers.ForItem == strItemFBShomareh
                                                                              && RUsers.FBId == BaseFBId && RUsers.Type == "4"
                                                                      select new RizMetreUsersesForConditionDto()
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
                                                                          UseItem = RUsers.UseItem,
                                                                          BarAvordId = fb.BarAvordId,
                                                                          FBId = RUsers.FBId
                                                                      }).OrderBy(x => x.Shomareh).ToList();


                                                //DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);
                                                //DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and ForItem='" + strItemFBShomareh + "' and Type=4");
                                            }
                                        }

                                        //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                        string strCurrentFBShomareh = strItemsFBShomareh;//Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        blnCheckIsExistErrors = "false";
                                        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId && x.NoeFB == NoeFBId);

                                        Guid currentItemsHasConditionAddedToFBId = new Guid();
                                        bool blnCheckSave = false;
                                        if (currentItemsHasConditionAddedToFBs == null)
                                        {
                                            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                            ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                            ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                            ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                            ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                            ItemsHasConditionAddedToFB.NoeFB = NoeFBId;


                                            try
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                await _context.SaveChangesAsync();
                                                blnCheckSave = true;
                                                currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                            }
                                            catch (Exception)
                                            {
                                                blnCheckSave = false;
                                            }
                                        }
                                        else
                                        {
                                            currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                            blnCheckSave = true;
                                        }
                                        if (blnCheckSave)
                                        {
                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            decimal dMultiple = 0;
                                            if (strFinalWorking != "")
                                            {
                                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            }

                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);

                                            ///////////////////////////////////////
                                            /////فیلدهای آیتمی که درج میگردد//////
                                            ///////////////////////////////////////

                                            List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems.ToString().Trim()).ToList();

                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();

                                            clsFB? varFBUser = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);

                                            Guid FBId = new Guid();
                                            if (varFBUser == null)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                FB.BahayeVahedZarib = dMultiple;
                                                FB.NoeFBId = NoeFBId;
                                                _context.FBs.Add(FB);
                                                await _context.SaveChangesAsync();
                                                FBId = FB.ID;
                                                varFBUser = FB;
                                            }
                                            else
                                                FBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                            string strShomareh1 = varFBShomareh;
                                            Guid guFBId = BaseFBId;
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
                                            //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                            //{
                                            //    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                            //    if (DrRizMetreUsersesCurrent.Length == 0)
                                            //    {

                                            string[] FieldsAddingSplit = FieldsAdding.Split(",");
                                            foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                            {
                                                var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                                if (DrRizMetreUsersesCurrent.Count == 0)
                                                {
                                                    //decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString()) * dMultiple;
                                                    //decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                    //decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                    //decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                    //decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


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
                                                        var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                        dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                        dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                        dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                        dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                                        dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;
                                                    }

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                    RizMetreUserses.Tedad = dTedad;
                                                    RizMetreUserses.Tool = dTool;
                                                    RizMetreUserses.Arz = dArz;
                                                    RizMetreUserses.Ertefa = dErtefa;
                                                    RizMetreUserses.Vazn = dVazn;
                                                    RizMetreUserses.Des = DtRizMetreUserses11.Des; //ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + strForItem
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
                                                    if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses.ConditionContextId = ConditionContextId;


                                                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses.ID,
                                                            BaseRizMetreId = DtRizMetreUserses11.Id
                                                        });

                                                    _context.RizMetreUserses.Add(RizMetreUserses);

                                                    dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                    //Start///////////////////////
                                                    ////////////حمل///////////////
                                                    //////////////////////////////
                                                    ///آیتم های حمل درج میگردد
                                                    ///


                                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                    {
                                                        BarAvordUserId = guBAId,
                                                        Year = Year,
                                                        ItemFBShomareh = strItemShomareh,
                                                        NoeFBId = NoeFBId,
                                                        //BarAvordHamlId = gBarAvordHamlId,
                                                        Shomareh = RizMetreUserses.Shomareh,
                                                        MeghdarJoz = dMeghdarJoz,
                                                        LevelNumber = 1,
                                                    };
                                                    await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                }
                                            }


                                            /////////////////////////////////////
                                            //////دریافت جمع ریزمتره های جاری///
                                            /////////////////////////////////////
                                            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                   ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                   : null;

                                            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                    else
                                    {
                                        if (blnCheckIsExistErrors != "false")
                                        {
                                            blnCheckIsExistErrors = "true";
                                            //strErrors += "<div class=\"col-md-12\"><span class=\"spanStyleMitraMedium\">" + ItemsAdding.DesOfAddingItems.ToString() + "</span></div>";
                                            strErrors = strCondition.Replace("x", "عدد وارد شده");
                                        }
                                    }
                                    break;
                                }
                            case "5":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString().Trim() == "" ? null : ItemsAdding.FieldsAdding.ToString().Trim();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCondition = ItemsAdding.Condition.ToString().Trim();
                                    //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
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
                                        blnCheckIsExistWarning = false;

                                        string strCurrentFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());

                                        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                         .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                             x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId && x.NoeFB == NoeFBId);
                                        bool blnCheckSave = false;

                                        Guid currentItemsHasConditionAddedToFBId = new Guid();

                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        if (currentItemsHasConditionAddedToFBs == null)
                                        {
                                            ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                            ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                            ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                            ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                            ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            await _context.SaveChangesAsync();
                                            blnCheckSave = true;
                                            currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                        }
                                        else
                                        {
                                            blnCheckSave = true;
                                            currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;

                                        }

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

                                            ///////////////////////////////////////
                                            /////فیلدهای آیتمی که درج میگردد//////
                                            ///////////////////////////////////////

                                            List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems.ToString().Trim() && x.NoeFB == NoeFBId).ToList();


                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                            clsFB? varFBUser = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);
                                            //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
                                            Guid FBId = new Guid();
                                            if (varFBUser == null)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                FB.BahayeVahedZarib = 0;// dMultiple;
                                                FB.NoeFBId = NoeFBId;
                                                _context.FBs.Add(FB);
                                                await _context.SaveChangesAsync();
                                                FBId = FB.ID;
                                                varFBUser = FB;
                                                //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), ItemsAdding.AddedItems.ToString().Trim(), 0);
                                            }
                                            else
                                                FBId = varFBUser.ID;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                                            List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                                join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                                                                                                                select new RizMetreUsersForItemsAddingToFBInputDto()
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
                                                                                                                    UseItem = RUsers.UseItem,
                                                                                                                    BarAvordId = fb.BarAvordId,
                                                                                                                    FBId = RUsers.FBId
                                                                                                                }).OrderBy(x => x.Shomareh).ToList();
                                            //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                            string strShomareh1 = varFBShomareh;
                                            Guid guFBId = BaseFBId;
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
                                            //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + varFBShomareh + "' and Type=2");
                                            //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                            //{
                                            //    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                            //    if (DrRizMetreUsersesCurrent.Length == 0)
                                            //    {


                                            foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                            {
                                                var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                                if (DrRizMetreUsersesCurrent.Count == 0)
                                                {
                                                    decimal? dTedad = null;
                                                    decimal? dTool = null;
                                                    decimal? dArz = null;
                                                    decimal? dErtefa = null;
                                                    decimal? Vazn = null;

                                                    string[] strFieldsAdding = FieldsAdding.Split(",");
                                                    List<string> lst = new List<string>();
                                                    for (int j = 0; j < strFieldsAdding.Length; j++)
                                                    {
                                                        lst.Add(strFieldsAdding[j]);
                                                    }

                                                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                        dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;

                                                        //dTedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                        dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;

                                                        //dTool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                        dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;

                                                        //dArz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                        dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;

                                                        //dErtefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                    }

                                                    Vazn = dMultiple;

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                    RizMetreUserses.Tedad = dTedad;
                                                    RizMetreUserses.Tool = dTool;
                                                    RizMetreUserses.Arz = dArz;
                                                    RizMetreUserses.Ertefa = dErtefa;
                                                    RizMetreUserses.Vazn = dMultiple;
                                                    RizMetreUserses.Des = DtRizMetreUserses11.Des; //ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + varFBShomareh
                                                                                                   //                               + " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();

                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = varFBShomareh;
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


                                                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses.ID,
                                                            BaseRizMetreId = DtRizMetreUserses11.Id
                                                        });

                                                    dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                    //Start///////////////////////
                                                    ////////////حمل///////////////
                                                    //////////////////////////////
                                                    ///آیتم های حمل درج میگردد
                                                    ///


                                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                    {
                                                        BarAvordUserId = guBAId,
                                                        Year = Year,
                                                        ItemFBShomareh = strItemShomareh,
                                                        NoeFBId = NoeFBId,
                                                        //BarAvordHamlId = gBarAvordHamlId,
                                                        Shomareh = RizMetreUserses.Shomareh,
                                                        MeghdarJoz = dMeghdarJoz,
                                                        LevelNumber = 1,
                                                    };
                                                    await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                }
                                            }


                                            /////////////////////////////////////
                                            //////دریافت جمع ریزمتره های جاری///
                                            /////////////////////////////////////
                                            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                   ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                   : null;

                                            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                    else
                                    {
                                        if (blnCheckIsExistErrors != "false")
                                        {
                                            blnCheckIsExistErrors = "true";
                                            blnCheckIsExistWarning = true;
                                            //return new JsonResult("CI");//CheckInput
                                        }

                                        //if (blnCheckIsExistErrors != "false")
                                        //{
                                        //    blnCheckIsExistErrors = "true";
                                        //    //strErrors += "<div class=\"col-md-12\"><span class=\"spanStyleMitraMedium\">" + ItemsAdding.DesOfAddingItems.ToString() + "</span></div>";
                                        //    strErrors = strCondition.Replace("x", "عدد وارد شده");
                                        //}
                                    }
                                    break;
                                }
                            case "2":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //StringToFormula StringToFormula = new StringToFormula();
                                    blnCheckIsExistErrors = "false";

                                    //string strCurrentFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

                                    ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                                    {
                                        strFinalWorking = strFinalWorking,
                                        Meghdar = Meghdar,
                                        Meghdar2 = Meghdar2,
                                        strCondition = strCondition,
                                        strItemsFBShomareh = strItemsFBShomareh,
                                        lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                                        ConditionGroupId = ConditionGroupId,
                                        BarAvordId = BarAvordId,
                                        NoeFBId = NoeFBId,
                                        strCharacterPlus = strCharacterPlus,
                                        FBShomarehAddedItem = FBShomarehAddedItem,
                                        DesOfAddingItems = DesOfAddingItems,
                                        BaseFBId = BaseFBId,
                                        Year = Year,
                                        FieldsAdding = FieldsAdding,
                                        ShomarehNew = ShomareNew,
                                        Now = Now,
                                        ConditionContextRel = ConditionContextRel,
                                        ConditionContextId = ConditionContextId,
                                        blnCheckIsExistErrors = blnCheckIsExistErrors,
                                        LevelNumber = LevelNumber,
                                        blnCheckAgain = blnCheckAgain,
                                        RMIdForEdit = null
                                    };

                                    ConditionCaseCommon CCC = new ConditionCaseCommon();
                                    //bool blnResult = await CCC.Case2(RequestCase1, context);
                                    ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case2(RequestCase1, context);

                                    blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                                    strWarning = ResultCCC.strWarning;
                                    Status = ResultCCC.blnStatus;

                                    break;
                                }
                            case "7":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString().Trim() == "" ? null : ItemsAdding.FieldsAdding.ToString().Trim();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCondition = ItemsAdding.Condition.ToString().Trim();
                                    //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                    //StringToFormula StringToFormula = new StringToFormula();

                                    blnCheckIsExistErrors = "false";
                                    blnCheckIsExistWarning = false;

                                    string strCurrentFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());

                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId && x.NoeFB == NoeFBId);
                                    bool blnCheckSave = false;

                                    Guid currentItemsHasConditionAddedToFBId = new Guid();
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                        await _context.SaveChangesAsync();
                                        blnCheckSave = true;
                                        currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                    }
                                    else
                                    {
                                        blnCheckSave = true;
                                        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                    }

                                    if (blnCheckSave)
                                    {
                                        var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                        DataTable DtBA = clsConvert.ToDataTable(varBA);

                                        ///////////////////////////////////////
                                        /////فیلدهای آیتمی که درج میگردد//////
                                        ///////////////////////////////////////

                                        List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems.ToString().Trim() && x.NoeFB == NoeFBId).ToList();


                                        Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                        clsFB? varFBUser = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);

                                        Guid FBId = new Guid();
                                        if (varFBUser == null)
                                        {
                                            clsFB FB = new clsFB();
                                            FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                            FB.BahayeVahedZarib = 0;// dMultiple;
                                            FB.NoeFBId = NoeFBId;
                                            _context.FBs.Add(FB);
                                            await _context.SaveChangesAsync();
                                            FBId = FB.ID;
                                            varFBUser = FB;
                                        }
                                        else
                                            FBId = varFBUser.ID; ;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                        List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                                                                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                  where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                                                                                                                  select new RizMetreUsersForItemsAddingToFBInputDto()
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
                                                                                                                      UseItem = RUsers.UseItem,
                                                                                                                      BarAvordId = fb.BarAvordId,
                                                                                                                      FBId = RUsers.FBId
                                                                                                                  }).OrderBy(x => x.Shomareh).ToListAsync();

                                        //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                        string strShomareh1 = varFBShomareh;
                                        Guid guFBId = BaseFBId;
                                        var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
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
                                                                               }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();
                                        DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                        foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                        {
                                            var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                            if (DrRizMetreUsersesCurrent.Count == 0)
                                            {


                                                //        for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                                //{
                                                //    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                //    if (DrRizMetreUsersesCurrent.Length == 0)
                                                //    {

                                                var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                decimal? Arz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;


                                                decimal? dTedad = null;
                                                decimal? dTool = null;
                                                //decimal? dArz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                decimal? dArz = Arz;
                                                decimal? dErtefa = null;
                                                decimal? dVazn = null;

                                                //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                                if (dArz != null)
                                                {
                                                    string strConditionOp = strCondition.Replace("x", dArz.ToString().Trim());
                                                    StringToFormula StringToFormula = new StringToFormula();
                                                    bool blnCheck = true;
                                                    if (strConditionOp != "")
                                                    {
                                                        blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                                    }
                                                    if (blnCheck)
                                                    {
                                                        string[] strFieldsAdding = FieldsAdding.Split(",");
                                                        List<string> lst = new List<string>();
                                                        for (int j = 0; j < strFieldsAdding.Length; j++)
                                                        {
                                                            lst.Add(strFieldsAdding[j]);
                                                        }

                                                        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                            dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                                            //dTedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                            dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;

                                                            //dTool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                        }
                                                        //strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                        //if (strCal.Count != 0)
                                                        //{
                                                        //    var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                        //    decimal? Arz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;

                                                        //    //dArz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                        //}
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                            dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;

                                                            //dErtefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                        }

                                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                        RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                        ShomareNew++;
                                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                        RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                        RizMetreUserses.Tedad = dTedad;
                                                        RizMetreUserses.Tool = dTool;
                                                        RizMetreUserses.Arz = dArz;
                                                        RizMetreUserses.Ertefa = dErtefa;
                                                        RizMetreUserses.Vazn = dVazn;
                                                        RizMetreUserses.Des = DtRizMetreUserses11.Des;
                                                        RizMetreUserses.FBId = FBId;
                                                        RizMetreUserses.OperationsOfHamlId = 1;
                                                        RizMetreUserses.Type = "2";
                                                        RizMetreUserses.ForItem = varFBShomareh;
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

                                                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses.ID,
                                                            BaseRizMetreId = DtRizMetreUserses11.Id
                                                        });

                                                        dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                        //Start///////////////////////
                                                        ////////////حمل///////////////
                                                        //////////////////////////////
                                                        ///آیتم های حمل درج میگردد
                                                        ///


                                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                        {
                                                            BarAvordUserId = guBAId,
                                                            Year = Year,
                                                            ItemFBShomareh = strItemShomareh,
                                                            NoeFBId = NoeFBId,
                                                            //BarAvordHamlId = gBarAvordHamlId,
                                                            Shomareh = RizMetreUserses.Shomareh,
                                                            MeghdarJoz = dMeghdarJoz,
                                                            LevelNumber = 1,
                                                        };
                                                        await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                    }
                                                }
                                            }
                                        }

                                        /////////////////////////////////////
                                        //////دریافت جمع ریزمتره های جاری///
                                        /////////////////////////////////////
                                        decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                        decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                               ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                               : null;

                                        varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;
                                        await _context.SaveChangesAsync();
                                    }
                                    break;
                                }
                            case "8":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    blnCheckIsExistErrors = "false";
                                    bool blnCheckSave = false;


                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                    string strItemFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);


                                    Guid currentItemsHasConditionAddedToFBId = new Guid();

                                    if (varItemsHasConditionAddedToFB.Count == 0)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                        ItemsHasConditionAddedToFB.Meghdar = 0;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                        await _context.SaveChangesAsync();
                                        blnCheckSave = true;
                                    }
                                    else
                                    {
                                        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                        blnCheckSave = true;
                                    }


                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    // string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCondition = ItemsAdding.Condition.ToString().Trim();
                                    StringToFormula StringToFormula = new StringToFormula();
                                    //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                    var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                    DataTable DtBA = clsConvert.ToDataTable(varBA);

                                    ///////////////////////////////////////
                                    /////فیلدهای آیتمی که درج میگردد//////
                                    ///////////////////////////////////////
                                    List<clsItemsFields> lstCurrentItemFields = await _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems.ToString().Trim() && x.NoeFB == NoeFBId).ToListAsync();

                                    Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);

                                    Guid FBId = new Guid();
                                    if (varFBUser == null)
                                    {
                                        clsFB FB = new clsFB();
                                        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                        FB.BahayeVahedZarib = 0;
                                        FB.NoeFBId = NoeFBId;
                                        _context.FBs.Add(FB);
                                        await _context.SaveChangesAsync();
                                        FBId = FB.ID;
                                        varFBUser = FB;
                                    }
                                    else
                                        FBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                    Guid guFBId = BaseFBId;

                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                                                                                              join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                              where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                                                                                                              select new RizMetreUsersForItemsAddingToFBInputDto()
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
                                                                                                                  UseItem = RUsers.UseItem,
                                                                                                                  BarAvordId = fb.BarAvordId,
                                                                                                                  FBId = RUsers.FBId
                                                                                                              }).OrderBy(x => x.Shomareh).ToListAsync();

                                    //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                    string strShomareh1 = varFBShomareh;
                                    var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
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
                                                                           }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();

                                    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);
                                    //DataTable DtRizMetreUserses = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
                                    //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + varFBShomareh + "' and Type=2");
                                    //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                    //{

                                    string[] FieldsAddingSplit = FieldsAdding.Split(",");
                                    foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                    {
                                        string strConditionOp = strCondition.Replace("x", DtRizMetreUserses11.Arz.ToString());
                                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        if (blnCheck)
                                        {
                                            decimal ArzEzafi = 1;
                                            //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                            if (strFinalWorking != "")
                                            {
                                                strFinalWorking = strFinalWorking.Replace("x", DtRizMetreUserses11.Arz.ToString().Trim());
                                                ArzEzafi = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            }

                                            var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                            if (DrRizMetreUsersesCurrent.Count == 0)
                                            {
                                                //    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + DtRizMetreUserses1.Shomareh));
                                                //if (DrRizMetreUsersesCurrent.Length == 0)
                                                //{
                                                //decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                //decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                //decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                //decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                //decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                ShomareNew++;
                                                RizMetreUserses.ShomarehNew = ShomareNew.ToString();


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
                                                    var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                    dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                    dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                    dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                    dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                                }
                                                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                if (strCal.Count != 0)
                                                {
                                                    var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                                    dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;
                                                }

                                                RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                RizMetreUserses.Tedad = dTedad;
                                                RizMetreUserses.Tool = dTool;
                                                RizMetreUserses.Arz = dArz;
                                                RizMetreUserses.Ertefa = dErtefa;
                                                RizMetreUserses.Vazn = dVazn;
                                                RizMetreUserses.Des = DtRizMetreUserses11.Des; //ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + varFBShomareh
                                                                                               //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                RizMetreUserses.FBId = FBId;
                                                RizMetreUserses.OperationsOfHamlId = 1;
                                                RizMetreUserses.Type = "2";
                                                RizMetreUserses.ForItem = varFBShomareh;
                                                RizMetreUserses.UseItem = "";
                                                RizMetreUserses.LevelNumber = LevelNumber;
                                                RizMetreUserses.InsertDateTime = Now;

                                                RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                RizMetreUserses.ConditionContextId = ConditionContextId;

                                                ///محاسبه مقدار جزء
                                                decimal? dMeghdarJoz = null;
                                                if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                                    dMeghdarJoz = null;
                                                else
                                                    dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                    (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                                RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                _context.RizMetreUserses.Add(RizMetreUserses);

                                                _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                    new clsItemsHasConditionAddedToFBRizMetre
                                                    {
                                                        ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                        RizMetreId = RizMetreUserses.ID,
                                                        BaseRizMetreId = DtRizMetreUserses11.Id
                                                    });

                                                dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                //Start///////////////////////
                                                ////////////حمل///////////////
                                                //////////////////////////////
                                                ///آیتم های حمل درج میگردد
                                                ///


                                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                {
                                                    BarAvordUserId = guBAId,
                                                    Year = Year,
                                                    ItemFBShomareh = strItemShomareh,
                                                    NoeFBId = NoeFBId,
                                                    //BarAvordHamlId = gBarAvordHamlId,
                                                    Shomareh = RizMetreUserses.Shomareh,
                                                    MeghdarJoz = dMeghdarJoz,
                                                    LevelNumber = 1,
                                                };
                                                await HamlCommon.SaveHaml(requestSaveHaml, context);

                                                /////////////////////////////////////
                                                //////دریافت جمع ریزمتره های جاری///
                                                /////////////////////////////////////
                                                decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                                decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                       ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                       : null;

                                                varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;
                                                await _context.SaveChangesAsync();
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "10":
                                {
                                    if (blnCheckAgain)
                                    {
                                        //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                        //string strCondition = ItemsAdding.Condition.ToString().Trim();
                                        //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                        string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());

                                        string strDesOfAddingItems = ItemsAdding.DesOfAddingItems.ToString().Trim();

                                        //string strCharacterPlus = ItemsAdding.CharacterPlus == null ? "" : ItemsAdding.CharacterPlus.ToString().Trim();

                                        //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                        //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                        ///////////////////////////////////////
                                        /////فیلدهای آیتمی که درج میگردد//////
                                        ///////////////////////////////////////

                                        List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems.ToString().Trim() && x.NoeFB == NoeFBId).ToList();

                                        StringToFormula StringToFormula = new StringToFormula();
                                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        if (blnCheck)
                                        {
                                            blnCheckAgain = false;

                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            decimal dZarib = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.###"));
                                            if (dZarib == 0)
                                            {
                                                clsFB? varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemsAdding.AddedItems.ToString().Trim()).FirstOrDefault();
                                                if (varFBUsersAdded != null)
                                                {
                                                    Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                                    clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                    if (Fb != null)
                                                    {
                                                        _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                                                        _context.FBs.Remove(Fb);
                                                        await _context.SaveChangesAsync();
                                                    }
                                                }
                                                return new JsonResult("CI");//CheckInput
                                            }
                                            else
                                            {

                                                decimal dSumAllRizMetre = 0;
                                                blnCheckIsExistErrors = "false";

                                                //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());


                                                string strItemFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

                                                clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                                 .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                                                     x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                                bool blnCheckSave = false;
                                                Guid currentItemsHasConditionAddedToFBId = new Guid();
                                                if (currentItemsHasConditionAddedToFBs == null)
                                                {
                                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                                    ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;//Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                                    ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                                    ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                                    try
                                                    {
                                                        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                        await _context.SaveChangesAsync();
                                                        blnCheckSave = true;
                                                        currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                                    }
                                                    catch (Exception)
                                                    {
                                                        blnCheckSave = false;
                                                    }
                                                }
                                                else
                                                {
                                                    currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                                    blnCheckSave = true;
                                                }


                                                //if (ItemsHasConditionAddedToFB.Save())
                                                if (blnCheckSave)
                                                {

                                                    dZarib = dZarib < 0 ? dZarib * -1 : dZarib;
                                                    var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                                    DataTable DtBA = clsConvert.ToDataTable(varBA);
                                                    Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                    string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus;
                                                    clsFB varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId).FirstOrDefault();

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
                                                    if (varFBUser == null)
                                                    {
                                                        clsFB FB = new clsFB();
                                                        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                        FB.Shomareh = strItemShomareh;//ItemsAdding.AddedItems.ToString().Trim();
                                                        FB.BahayeVahedZarib = 0;// dZarib;
                                                        FB.BahayeVahedSharh = strDesOfAddingItems;
                                                        FB.NoeFBId = NoeFBId;
                                                        _context.FBs.Add(FB);
                                                        await _context.SaveChangesAsync();
                                                        FBId = FB.ID;
                                                        varFBUser = FB;
                                                    }
                                                    else
                                                    {
                                                        FBId = varFBUser.ID;
                                                    }


                                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                                                                                                              join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                              where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
                                                                                                                              select new RizMetreUsersForItemsAddingToFBInputDto()
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
                                                                                                                                  UseItem = RUsers.UseItem,
                                                                                                                                  BarAvordId = fb.BarAvordId,
                                                                                                                                  FBId = RUsers.FBId
                                                                                                                              }).OrderBy(x => x.Shomareh).ToListAsync();
                                                    //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                                    string strShomareh1 = varFBShomareh;
                                                    Guid guFBId = BaseFBId;
                                                    var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
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
                                                                                           }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();
                                                    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                                    string[] FieldsAddingSplit = FieldsAdding.Split(",");

                                                    foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                                    {
                                                        var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                                        if (DrRizMetreUsersesCurrent.Count == 0)
                                                        {

                                                            //        for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                                            //{
                                                            //    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                            //    if (DrRizMetreUsersesCurrent.Length == 0)
                                                            //    {
                                                            //decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                            //decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                            //decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                            //decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                            //decimal? Vazn = dZarib; //DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                                            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                            RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                            ShomareNew++;
                                                            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

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
                                                                var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                                dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                                            }
                                                            strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                            if (strCal.Count != 0)
                                                            {
                                                                var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                                dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                                            }
                                                            strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                            if (strCal.Count != 0)
                                                            {
                                                                var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                                dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                                            }
                                                            strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                            if (strCal.Count != 0)
                                                            {
                                                                var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                                dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                                            }
                                                            strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                            if (strCal.Count != 0)
                                                            {
                                                                dVazn = dZarib;
                                                            }


                                                            RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                            RizMetreUserses.Tedad = dTedad;
                                                            RizMetreUserses.Tool = dTool;
                                                            RizMetreUserses.Arz = dArz;
                                                            RizMetreUserses.Ertefa = dErtefa;
                                                            RizMetreUserses.Vazn = dVazn;
                                                            RizMetreUserses.Des = DtRizMetreUserses11.Des; //ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + varFBShomareh
                                                                                                           //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                            RizMetreUserses.FBId = FBId;
                                                            RizMetreUserses.OperationsOfHamlId = 1;
                                                            RizMetreUserses.Type = "2";
                                                            RizMetreUserses.ForItem = varFBShomareh;
                                                            RizMetreUserses.UseItem = "";
                                                            RizMetreUserses.LevelNumber = LevelNumber;
                                                            RizMetreUserses.InsertDateTime = Now;

                                                            RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                            RizMetreUserses.ConditionContextId = ConditionContextId;

                                                            ///محاسبه مقدار جزء
                                                            decimal? dMeghdarJoz = null;
                                                            if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                                                dMeghdarJoz = null;
                                                            else
                                                                dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                                (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;
                                                            _context.RizMetreUserses.Add(RizMetreUserses);

                                                            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                                new clsItemsHasConditionAddedToFBRizMetre
                                                                {
                                                                    ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                                    RizMetreId = RizMetreUserses.ID,
                                                                    BaseRizMetreId = DtRizMetreUserses11.Id
                                                                });
                                                            dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                            //Start///////////////////////
                                                            ////////////حمل///////////////
                                                            //////////////////////////////
                                                            ///آیتم های حمل درج میگردد
                                                            ///


                                                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                            {
                                                                BarAvordUserId = guBAId,
                                                                Year = Year,
                                                                ItemFBShomareh = strItemShomareh,
                                                                NoeFBId = NoeFBId,
                                                                //BarAvordHamlId = gBarAvordHamlId,
                                                                Shomareh = RizMetreUserses.Shomareh,
                                                                MeghdarJoz = dMeghdarJoz,
                                                                LevelNumber = 1,
                                                            };
                                                            await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                        }
                                                    }

                                                    /////////////////////////////////////
                                                    //////دریافت جمع ریزمتره های جاری///
                                                    /////////////////////////////////////
                                                    decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                                    decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                           ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                           : null;

                                                    varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;
                                                    await _context.SaveChangesAsync();
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
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCharacterPlus = ItemsAdding.CharacterPlus.ToString().Trim();

                                    //string[] AddedItems = ItemsAdding.AddedItems.ToString().Trim().Split("_");

                                    //foreach (var AddedItem in AddedItems)
                                    //{



                                    ///////////////////////////////////////
                                    /////فیلدهای آیتمی که درج میگردد//////
                                    ///////////////////////////////////////

                                    string strShomareh1 = varFBShomareh;

                                    List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems && x.NoeFB == NoeFBId).ToList();

                                    string strItemShomarehAdd = ItemsAdding.Shomareh + strCharacterPlus; //ItemsAdding.AddedItems + strCharacterPlus; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + strCharacterPlus;
                                    string strFBShomareh1 = ItemsAdding.Shomareh;
                                    //string strFinalWorking = ItemsAdding.FinalWorking.ToString().Trim();
                                    string strDesOfAddingItems = ItemsAdding.DesOfAddingItems.ToString().Trim();
                                    decimal dPercent = decimal.Parse(strFinalWorking);
                                    blnCheckIsExistErrors = "false";

                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strShomareh1 && x.NoeFB == NoeFBId &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);



                                    bool blnCheckSave = false;
                                    Guid currentItemsHasConditionAddedToFBId = new Guid();
                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strShomareh1;//strItemShomarehAdd;
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            await _context.SaveChangesAsync();
                                            blnCheckSave = true;
                                            currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = false;

                                        }
                                    }
                                    else
                                    {
                                        blnCheckSave = true;
                                        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                    }


                                    if (blnCheckSave)
                                    {
                                        decimal dSumAllRizMetre = 0;

                                        clsBaravordUser? BA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).FirstOrDefault();
                                        if (BA != null)
                                        {

                                            clsFB? varFBUser = _context.FBs.Where(x => x.BarAvordId == BA.ID && x.Shomareh == strItemShomarehAdd && x.NoeFBId == NoeFBId).FirstOrDefault();

                                            Guid FBId = new Guid();
                                            if (varFBUser == null)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = BA.ID;
                                                FB.Shomareh = strItemShomarehAdd;  //varFBShomareh;
                                                FB.BahayeVahedZarib = dPercent;
                                                FB.BahayeVahedSharh = strDesOfAddingItems;
                                                FB.NoeFBId = NoeFBId;
                                                _context.FBs.Add(FB);
                                                await _context.SaveChangesAsync();
                                                FBId = FB.ID;
                                                varFBUser = FB;
                                            }
                                            else
                                            {
                                                FBId = varFBUser.ID;
                                            }



                                            ///به دست آوردن ریزه متره های آیتم جاری

                                            clsFB? FBUserCurrentItem = _context.FBs.Where(x => x.BarAvordId == BA.ID && x.Shomareh == strShomareh1).FirstOrDefault();

                                            if (FBUserCurrentItem != null)
                                            {
                                                List<RizMetreUsersForItemsAddingToFBInputDto> lstRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                                                                                                          join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                          where RUsers.LevelNumber == LevelNumber && RUsers.FBId == FBUserCurrentItem.ID
                                                                                                                          select new RizMetreUsersForItemsAddingToFBInputDto()
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
                                                                                                                              FBId = RUsers.FBId,
                                                                                                                              Type = RUsers.Type
                                                                                                                          }).OrderBy(x => x.Shomareh).ToListAsync();
                                                //}).Where(x => x.FBId == BaseFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();




                                                Guid guFBId = BaseFBId;
                                                var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
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
                                                                                       }).Where(x => x.FBId == FBId && x.ForItem == strFBShomareh1 && x.Type == "2").ToListAsync();
                                                //DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                                //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                                //{

                                                string[] FieldsAddingSplit = FieldsAdding.Split(",");
                                                foreach (var RM in lstRizMetreUserses)
                                                {
                                                    var RizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == RM.Shomareh && x.FBId == RM.FBId).FirstOrDefault();

                                                    if (RizMetreUsersesCurrent == null)
                                                    {

                                                        //decimal? Tedad = RM.Tedad;
                                                        //decimal? Tool = RM.Tool;
                                                        //decimal? Arz = RM.Arz;
                                                        //decimal? Ertefa = RM.Ertefa;
                                                        //decimal? Vazn = RM.Vazn;

                                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                        RizMetreUserses.Shomareh = RM.Shomareh;
                                                        ShomareNew++;
                                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

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
                                                            var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                            dTedad = tedadField?.DefaultValue ?? RM.Tedad;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                            dTool = toolField?.DefaultValue ?? RM.Tool;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                            dArz = arzField?.DefaultValue ?? RM.Arz;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                            dErtefa = ertefaField?.DefaultValue ?? RM.Ertefa;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                                            dVazn = vaznField?.DefaultValue ?? RM.Vazn;
                                                        }


                                                        RizMetreUserses.Sharh = RM.Sharh;
                                                        RizMetreUserses.Tedad = dTedad;
                                                        RizMetreUserses.Tool = dTool;
                                                        RizMetreUserses.Arz = dArz;
                                                        RizMetreUserses.Ertefa = dErtefa;
                                                        RizMetreUserses.Vazn = dVazn;
                                                        RizMetreUserses.Des = RM.Des; //ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + varFBShomareh
                                                                                      //+ " - ریز متره شماره " + RM.Shomareh;
                                                        RizMetreUserses.FBId = FBId;
                                                        RizMetreUserses.OperationsOfHamlId = 1;
                                                        RizMetreUserses.Type = "2";
                                                        RizMetreUserses.ForItem = varFBShomareh;
                                                        RizMetreUserses.UseItem = "";
                                                        RizMetreUserses.LevelNumber = LevelNumber;
                                                        RizMetreUserses.InsertDateTime = Now;

                                                        RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                        RizMetreUserses.ConditionContextId = ConditionContextId;

                                                        ///محاسبه مقدار جزء
                                                        decimal? dMeghdarJoz = null;
                                                        if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                                                            dMeghdarJoz = null;
                                                        else
                                                            dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                            (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;
                                                        _context.RizMetreUserses.Add(RizMetreUserses);

                                                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses.ID,
                                                            BaseRizMetreId = RM.Id
                                                        });
                                                        dSumAllRizMetre += dMeghdarJoz ?? 0;


                                                        //Start///////////////////////
                                                        ////////////حمل///////////////
                                                        //////////////////////////////
                                                        ///آیتم های حمل درج میگردد
                                                        ///


                                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                        {
                                                            BarAvordUserId = BarAvordId,
                                                            Year = Year,
                                                            ItemFBShomareh = strItemShomarehAdd,
                                                            NoeFBId = NoeFBId,
                                                            //BarAvordHamlId = gBarAvordHamlId,
                                                            Shomareh = RizMetreUserses.Shomareh,
                                                            MeghdarJoz = dMeghdarJoz,
                                                            LevelNumber = 1,
                                                        };
                                                        await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                        //RizMetreUserses.Save();
                                                    }
                                                }
                                            }
                                            /////////////////////////////////////
                                            //////دریافت جمع ریزمتره های جاری///
                                            /////////////////////////////////////
                                            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                   ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                   : null;

                                            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;
                                            await _context.SaveChangesAsync();
                                            //}
                                        }
                                    }
                                    break;
                                }
                            case "12":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCondition = ItemsAdding.Condition.ToString().Trim();
                                    string[] strConditionSplit = strCondition.Split("_");

                                    //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                    DataTable DtRizMetreUserses = new DataTable();
                                    string strForItem = "";
                                    string strUseItem = "";
                                    string strItemFBShomareh = varFBShomareh;

                                    Guid guFBId = BaseFBId;
                                    strUseItem = varFBShomareh;
                                    strForItem = ItemsAdding.UseItemForAdd.ToString().Trim();
                                    //clsFB? varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strForItem).ToList();

                                    //if (varFBUser != null)
                                    //{

                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                                                                                              join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                              where RUsers.LevelNumber == LevelNumber && RUsers.FBId == guFBId && RUsers.ForItem == strItemFBShomareh && RUsers.Type == "1"
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
                                                                                                              }).OrderBy(x => x.Shomareh).ToListAsync();

                                    //}

                                    var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
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
                                                                           }).Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToListAsync();
                                    //DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);



                                    ///////////////////////////////////////
                                    /////فیلدهای آیتمی که درج میگردد//////
                                    ///////////////////////////////////////

                                    List<clsItemsFields> lstCurrentItemFields = await _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems).ToListAsync();
                                    List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();

                                    string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                    Guid FBId = new Guid();
                                    if (varFBUser == null)
                                    {
                                        clsFB FB = new clsFB();
                                        FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                        FB.BahayeVahedZarib = 0;
                                        FB.NoeFBId = NoeFBId;
                                        _context.FBs.Add(FB);
                                        await _context.SaveChangesAsync();
                                        FBId = FB.ID;
                                        varFBUser = FB;
                                    }
                                    else
                                        FBId = varFBUser.ID;

                                    string[] FieldsAddingSplit = FieldsAdding.Split(",");
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
                                                string strCurrentFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                                clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh && x.NoeFB == NoeFBId &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);


                                                bool blnCheckSave = false;
                                                Guid currentItemsHasConditionAddedToFBId = new Guid();
                                                if (currentItemsHasConditionAddedToFBs != null)
                                                {
                                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                                    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                                    ItemsHasConditionAddedToFB.Meghdar = RM.Tool != null ? RM.Tool.Value : 0;
                                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                                    ItemsHasConditionAddedToFB.NoeFB = NoeFBId;
                                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                    await _context.SaveChangesAsync();
                                                    currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                                }
                                                else

                                                {
                                                    currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                                    blnCheckSave = true;
                                                }

                                                if (strFinalWorking != "")
                                                {
                                                    strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");
                                                    ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                                                    ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                                                    ///Ertefa-3
                                                    decimal Ertefa = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));


                                                    var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == RM.Shomareh && x.FBId == FBId).ToList();
                                                    //DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh + " and FBId=" + FBId);
                                                    if (DrRizMetreUsersesCurrent.Count == 0)
                                                    {


                                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                        RizMetreUserses.Shomareh = RM.Shomareh;
                                                        ShomareNew++;
                                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                        List<string> lst = new List<string>();
                                                        for (int j = 0; j < FieldsAddingSplit.Length; j++)
                                                        {
                                                            lst.Add(FieldsAddingSplit[j]);
                                                        }


                                                        decimal? dTedad = null;
                                                        decimal? dTool = null;
                                                        decimal? dArz = null;
                                                        decimal? dErtefa = null;



                                                        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                            dTedad = tedadField?.DefaultValue ?? RM.Tedad;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                            dTool = toolField?.DefaultValue ?? RM.Tool;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                            dArz = arzField?.DefaultValue ?? RM.Arz;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            dErtefa = Ertefa;
                                                        }

                                                        RizMetreUserses.Sharh = RM.Sharh;
                                                        RizMetreUserses.Tedad = dTedad;
                                                        RizMetreUserses.Tool = dTool;
                                                        RizMetreUserses.Arz = dArz;
                                                        RizMetreUserses.Ertefa = dErtefa;
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
                                                        if (dTedad == null && dTool == null && dArz == null)
                                                            dMeghdarJoz = null;
                                                        else
                                                            dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                            (dArz == null ? 1 : dArz.Value) * dErtefa;
                                                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;
                                                        _context.RizMetreUserses.Add(RizMetreUserses);

                                                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses.ID,
                                                            BaseRizMetreId = RM.Id
                                                        });

                                                        dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                        //Start///////////////////////
                                                        ////////////حمل///////////////
                                                        //////////////////////////////
                                                        ///آیتم های حمل درج میگردد
                                                        ///


                                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                        {
                                                            BarAvordUserId = BarAvordId,
                                                            Year = Year,
                                                            ItemFBShomareh = strItemShomareh,
                                                            NoeFBId = NoeFBId,
                                                            //BarAvordHamlId = gBarAvordHamlId,
                                                            Shomareh = RizMetreUserses.Shomareh,
                                                            MeghdarJoz = dMeghdarJoz,
                                                            LevelNumber = 1,
                                                        };
                                                        await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                    }
                                                }
                                            }
                                            /////////////////////////////////////
                                            //////دریافت جمع ریزمتره های جاری///
                                            /////////////////////////////////////
                                            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                   ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                   : null;

                                            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                            await _context.SaveChangesAsync();
                                        }
                                    }

                                    break;
                                }
                            case "13":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);


                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                    string strItemFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                    Guid currentItemsHasConditionAddedToFBId = new Guid();
                                    bool blnCheckSave = false;
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.Meghdar2 = Meghdar2;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                    }
                                    else
                                    {
                                        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                        blnCheckSave = true;
                                    }


                                    ///////////////////////////////////////
                                    /////فیلدهای آیتمی که درج میگردد//////
                                    ///////////////////////////////////////

                                    List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems && x.NoeFB == NoeFBId).ToList();

                                    string[] strCondition_1 = ItemsAdding.Condition.ToString().Trim().Split("_");
                                    string strCondition1 = strCondition_1[0];
                                    string strCondition2 = strCondition_1[1];
                                    //string strFinalWorking = ItemsAdding.FinalWorking == null ? "" : ItemsAdding.FinalWorking.ToString();
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

                                            //bool blnCheckSave = false;
                                            try
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                await _context.SaveChangesAsync();
                                                blnCheckSave = true;
                                                currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;

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
                                                string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);

                                                Guid FBId = new Guid();
                                                if (varFBUser == null)
                                                {
                                                    clsFB FB = new clsFB();
                                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                    FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                    FB.BahayeVahedZarib = 0;
                                                    FB.BahayeVahedSharh = "";
                                                    FB.NoeFBId = NoeFBId;
                                                    _context.FBs.Add(FB);
                                                    await _context.SaveChangesAsync();
                                                    FBId = FB.ID;
                                                    varFBUser = FB;
                                                }
                                                else
                                                {
                                                    FBId = varFBUser.ID;
                                                }



                                                List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                                                                                                                          join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                          where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
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
                                                                                                                              ForItem = RUsers.ForItem,
                                                                                                                              Type = RUsers.Type,
                                                                                                                              UseItem = RUsers.UseItem,
                                                                                                                              BarAvordId = fb.BarAvordId,
                                                                                                                              FBId = RUsers.FBId
                                                                                                                          }).OrderBy(x => x.Shomareh).ToListAsync();

                                                //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                                string strShomareh1 = varFBShomareh;
                                                Guid guFBId = BaseFBId;
                                                var varRizMetreUsersesCurrent = await (from RizMetreUserses in _context.RizMetreUserses
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
                                                                                       }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();
                                                DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                                                //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                                //{
                                                //    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                                //    if (DrRizMetreUsersesCurrent.Length == 0)
                                                //    {

                                                string[] FieldsAddingSplit = FieldsAdding.Split(",");
                                                foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                                {
                                                    var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                                    if (DrRizMetreUsersesCurrent.Count == 0)
                                                    {
                                                        //decimal? Tedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                        //decimal? Tool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                        //decimal? Arz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                        //decimal? Ertefa = DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                        //decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());





                                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                        RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                        ShomareNew++;
                                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();


                                                        List<string> lst = new List<string>();
                                                        for (int j = 0; j < FieldsAddingSplit.Length; j++)
                                                        {
                                                            lst.Add(FieldsAddingSplit[j]);
                                                        }


                                                        decimal? dTedad = null;
                                                        decimal? dTool = null;
                                                        decimal? dArz = null;
                                                        decimal? dErtefa = null;
                                                        decimal? dVazn = dZaribVazn;



                                                        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                            dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                            dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                            dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                            dErtefa = ertefaField?.DefaultValue ?? DtRizMetreUserses11.Ertefa;
                                                        }


                                                        RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                        RizMetreUserses.Tedad = dTedad;
                                                        RizMetreUserses.Tool = dTool;
                                                        RizMetreUserses.Arz = dArz;
                                                        RizMetreUserses.Ertefa = dErtefa;
                                                        RizMetreUserses.Vazn = dVazn;
                                                        RizMetreUserses.Des = DtRizMetreUserses11.Des;
                                                        RizMetreUserses.FBId = FBId;
                                                        RizMetreUserses.OperationsOfHamlId = 1;
                                                        RizMetreUserses.Type = "2";
                                                        RizMetreUserses.ForItem = varFBShomareh;
                                                        RizMetreUserses.UseItem = "";
                                                        RizMetreUserses.LevelNumber = LevelNumber;
                                                        RizMetreUserses.InsertDateTime = Now;

                                                        RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                        RizMetreUserses.ConditionContextId = ConditionContextId;
                                                        ///محاسبه مقدار جزء
                                                        decimal? dMeghdarJoz = null;
                                                        if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0)
                                                            dMeghdarJoz = null;
                                                        else
                                                            dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                            (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * dZaribVazn;
                                                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                        _context.RizMetreUserses.Add(RizMetreUserses);

                                                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses.ID,
                                                            BaseRizMetreId = DtRizMetreUserses11.Id
                                                        });

                                                        dSumAllRizMetre += dMeghdarJoz ?? 0;


                                                        //Start///////////////////////
                                                        ////////////حمل///////////////
                                                        //////////////////////////////
                                                        ///آیتم های حمل درج میگردد
                                                        ///


                                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                        {
                                                            BarAvordUserId = guBAId,
                                                            Year = Year,
                                                            ItemFBShomareh = strItemShomareh,
                                                            NoeFBId = NoeFBId,
                                                            //BarAvordHamlId = gBarAvordHamlId,
                                                            Shomareh = RizMetreUserses.Shomareh,
                                                            MeghdarJoz = dMeghdarJoz,
                                                            LevelNumber = 1,
                                                        };
                                                        await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                        //RizMetreUserses.Save();
                                                    }
                                                }

                                                /////////////////////////////////////
                                                //////دریافت جمع ریزمتره های جاری///
                                                /////////////////////////////////////
                                                decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                                decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                       ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                       : null;

                                                varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;
                                                await _context.SaveChangesAsync();
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
                                    decimal dSumRizMetreAll = 0;

                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString().Trim() == "" ? null : ItemsAdding.FieldsAdding.ToString().Trim();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCondition = ItemsAdding.Condition.ToString().Trim();

                                    string[] strFinalWorking1 = ItemsAdding.FinalWorking.ToString().Split(',');
                                    string strFinalWorking2 = strFinalWorking1[0]; //ItemsAdding.FinalWorking.ToString();
                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());

                                    ///////////////////////////////////////
                                    /////فیلدهای آیتمی که درج میگردد//////
                                    ///////////////////////////////////////

                                    List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems && x.NoeFB == NoeFBId).ToList();

                                    StringToFormula StringToFormula = new StringToFormula();
                                    bool blnCheck = true;
                                    if (strConditionOp != "")
                                    {
                                        blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    }
                                    if (blnCheck)
                                    {
                                        blnCheckIsExistErrors = "false";

                                        //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());

                                        bool blnCheckSave = false;

                                        string strItemFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                         .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                                             x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);
                                        Guid currentItemsHasConditionAddedToFBId = new Guid();
                                        if (currentItemsHasConditionAddedToFBs == null)
                                        {
                                            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                            ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                            ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;  //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                            ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                            ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                            ItemsHasConditionAddedToFB.NoeFB = NoeFBId;

                                            try
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                await _context.SaveChangesAsync();
                                                currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                                blnCheckSave = true;
                                            }
                                            catch (Exception)
                                            {
                                                blnCheckSave = false;
                                            }
                                        }
                                        else
                                        {
                                            currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                            blnCheckSave = true;
                                        }

                                        //if (ItemsHasConditionAddedToFB.Save())
                                        if (blnCheckSave)
                                        {
                                            strFinalWorking2 = strFinalWorking2.Replace("x", Meghdar.ToString().Trim());
                                            decimal dMultiple = 0;
                                            if (strFinalWorking2 != "")
                                            {
                                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking2).ToString());
                                            }

                                            //clsOperation_ItemsFB Operation_ItemsFB = new clsOperation_ItemsFB();
                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);


                                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
                                            //DataTable DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + ItemsAdding.AddedItems.ToString().Trim() + "'");
                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);

                                            Guid FBId = new Guid();
                                            if (varFBUser == null)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                FB.BahayeVahedZarib = 0;// dMultiple;
                                                FB.NoeFBId = NoeFBId;
                                                _context.FBs.Add(FB);
                                                await _context.SaveChangesAsync();
                                                FBId = FB.ID;
                                                varFBUser = FB;
                                                //FBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), ItemsAdding.AddedItems.ToString().Trim(), 0);
                                            }
                                            else
                                                FBId = varFBUser.ID;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                            List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                                join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                                where RUsers.LevelNumber == LevelNumber && RUsers.FBId == BaseFBId && RUsers.Type == "1"
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
                                                                                                                    ForItem = RUsers.ForItem,
                                                                                                                    Type = RUsers.Type,
                                                                                                                    UseItem = RUsers.UseItem,
                                                                                                                    BarAvordId = fb.BarAvordId,
                                                                                                                    FBId = RUsers.FBId
                                                                                                                }).OrderBy(x => x.Shomareh).ToList();
                                            //DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                            string strShomareh1 = varFBShomareh;
                                            Guid guFBId = BaseFBId;
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
                                            //    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + int.Parse(DtRizMetreUserses.Rows[i]["Shomareh"].ToString()));
                                            //    if (DrRizMetreUsersesCurrent.Length == 0)
                                            //    {



                                            foreach (var DtRizMetreUserses11 in varRizMetreUserses)
                                            {
                                                var DrRizMetreUsersesCurrent = varRizMetreUsersesCurrent.Where(x => x.Shomareh == DtRizMetreUserses11.Shomareh).ToList();
                                                if (DrRizMetreUsersesCurrent.Count == 0)
                                                {

                                                    decimal? dTedad = null;
                                                    decimal? dTool = null;
                                                    decimal? dArz = null;
                                                    decimal? dVazn = null;

                                                    string[] strFieldsAdding = FieldsAdding.Split(",");
                                                    List<string> lst = new List<string>();
                                                    for (int j = 0; j < strFieldsAdding.Length; j++)
                                                    {
                                                        lst.Add(strFieldsAdding[j]);
                                                    }

                                                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var tedadField = lstCurrentItemFields
                                                            .FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                        dTedad = tedadField?.DefaultValue ?? DtRizMetreUserses11.Tedad;

                                                        //dTedad = DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var toolField = lstCurrentItemFields
                                                            .FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                        dTool = toolField?.DefaultValue ?? DtRizMetreUserses11.Tool;

                                                        //dTool = DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                    if (strCal.Count != 0)
                                                    {

                                                        var arzField = lstCurrentItemFields
                                                            .FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                        dArz = arzField?.DefaultValue ?? DtRizMetreUserses11.Arz;

                                                        //dArz = DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                    if (strCal.Count != 0)
                                                    {

                                                        var vaznField = lstCurrentItemFields
                                                            .FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                                        dVazn = vaznField?.DefaultValue ?? DtRizMetreUserses11.Vazn;

                                                        //dVazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());
                                                    }


                                                    decimal? dErtefa = dMultiple;

                                                    decimal? Vazn = dVazn; //DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());
                                                    if (strFinalWorking1.Length > 1)
                                                    {
                                                        string[] strVazn = strFinalWorking1[1].Split('_');
                                                        Vazn = decimal.Parse(strVazn[1]);
                                                    }
                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = DtRizMetreUserses11.Shomareh;
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = DtRizMetreUserses11.Sharh;
                                                    RizMetreUserses.Tedad = dTedad;
                                                    RizMetreUserses.Tool = dTool;
                                                    RizMetreUserses.Arz = dArz;
                                                    RizMetreUserses.Ertefa = dErtefa;
                                                    RizMetreUserses.Vazn = Vazn;
                                                    // RizMetreUserses.Zarib = dMultiple;
                                                    RizMetreUserses.Des = DtRizMetreUserses11.Des; //ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + varFBShomareh
                                                                                                   //                               + " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();

                                                    RizMetreUserses.FBId = FBId;
                                                    RizMetreUserses.OperationsOfHamlId = 1;
                                                    RizMetreUserses.Type = "2";
                                                    RizMetreUserses.ForItem = varFBShomareh;
                                                    RizMetreUserses.UseItem = "";
                                                    RizMetreUserses.LevelNumber = LevelNumber;
                                                    RizMetreUserses.InsertDateTime = Now;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz = null;
                                                    if (dTedad == null && dTool == null && dArz == null && Vazn == null && dErtefa == null)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                    dSumRizMetreAll += dMeghdarJoz.HasValue
                                                           ? (dMeghdarJoz ?? 0) : 0;

                                                    RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses.ConditionContextId = ConditionContextId;

                                                    _context.RizMetreUserses.Add(RizMetreUserses);

                                                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses.ID,
                                                            BaseRizMetreId = DtRizMetreUserses11.Id
                                                        });

                                                    //Start///////////////////////
                                                    ////////////حمل///////////////
                                                    //////////////////////////////
                                                    ///آیتم های حمل درج میگردد
                                                    ///


                                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                    {
                                                        BarAvordUserId = guBAId,
                                                        Year = Year,
                                                        ItemFBShomareh = strItemShomareh,
                                                        NoeFBId = NoeFBId,
                                                        //BarAvordHamlId = gBarAvordHamlId,
                                                        Shomareh = RizMetreUserses.Shomareh,
                                                        MeghdarJoz = dMeghdarJoz,
                                                        LevelNumber = 1,
                                                    };
                                                    await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                    //RizMetreUserses.Save();
                                                }
                                            }


                                            /////////////////////////////////////
                                            //////دریافت جمع ریزمتره های جاری///
                                            /////////////////////////////////////
                                            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                   ? (dSumRizMetreCurrentFB ?? 0) + dSumRizMetreAll
                                                   : null;

                                            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                    else
                                    {
                                        return new JsonResult("CI");//CheckInput


                                        //if (blnCheckIsExistErrors != "false")
                                        //{
                                        //    blnCheckIsExistErrors = "true";
                                        //    //strErrors += "<div class=\"col-md-12\"><span class=\"spanStyleMitraMedium\">" + ItemsAdding.DesOfAddingItems.ToString() + "</span></div>";
                                        //    strErrors = strCondition.Replace("x", "عدد وارد شده");
                                        //}
                                    }
                                    break;
                                }
                            case "15":
                                {
                                    ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                                    {
                                        strFinalWorking = strFinalWorking,
                                        Meghdar = Meghdar,
                                        Meghdar2 = Meghdar2,
                                        strCondition = strCondition,
                                        strItemsFBShomareh = strItemsFBShomareh,
                                        lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                                        ConditionGroupId = ConditionGroupId,
                                        BarAvordId = BarAvordId,
                                        NoeFBId = NoeFBId,
                                        strCharacterPlus = strCharacterPlus,
                                        FBShomarehAddedItem = FBShomarehAddedItem,
                                        DesOfAddingItems = DesOfAddingItems,
                                        BaseFBId = BaseFBId,
                                        Year = Year,
                                        FieldsAdding = FieldsAdding,
                                        ShomarehNew = ShomareNew,
                                        Now = Now,
                                        ConditionContextRel = ConditionContextRel,
                                        ConditionContextId = ConditionContextId,
                                        blnCheckIsExistErrors = blnCheckIsExistErrors,
                                        LevelNumber = LevelNumber,
                                        blnCheckIsExistWarning = false,
                                        strWarning = "",
                                        RMIdForEdit = null
                                    };

                                    ConditionCaseCommon CCC = new ConditionCaseCommon();
                                    ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case15(RequestCase1, context);

                                    blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                                    strWarning = ResultCCC.strWarning;
                                    Status = ResultCCC.blnStatus;

                                    //bool blnResult = await CCC.Case15(RequestCase1, _context);

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

                                    decimal dSumAllRizMetre = 0;
                                    GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                                    {
                                        ShomarehFB = strItemsFBShomareh,//strFBShomareh,
                                        BarAvordUserId = BarAvordId,
                                        NoeFB = NoeFBId,
                                        Year = Year,
                                        LevelNumber = LevelNumber,
                                        ConditionGroupId = lngConditionGroupId
                                    };
                                    List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);

                                    // string FieldsAdding = ItemsAdding.FieldsAdding.ToString();

                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    if (lstRM.Count != 0)
                                    {
                                        string strCurrentFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();

                                        //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                         .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh && x.NoeFB == NoeFBId &&
                                                             x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                        bool blnCheckSave = false;
                                        Guid currentItemsHasConditionAddedToFBId = new Guid();
                                        if (currentItemsHasConditionAddedToFBs == null)
                                        {
                                            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                            ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                            ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                            ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                            ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                            ItemsHasConditionAddedToFB.NoeFB = NoeFBId;


                                            try
                                            {
                                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                                await _context.SaveChangesAsync();
                                                blnCheckSave = true;
                                            }
                                            catch (Exception)
                                            {
                                                blnCheckSave = false;
                                            }
                                        }
                                        else
                                        {
                                            currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                            blnCheckSave = true;
                                        }

                                        if (blnCheckSave)
                                        {

                                            var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                            DataTable DtBA = clsConvert.ToDataTable(varBA);


                                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);
                                            //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);


                                            Guid FBId = new Guid();
                                            if (varFBUser == null)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                FB.BahayeVahedZarib = 0;
                                                FB.BahayeVahedSharh = "";
                                                FB.NoeFBId = NoeFBId;
                                                _context.FBs.Add(FB);
                                                await _context.SaveChangesAsync();
                                                FBId = FB.ID;
                                                varFBUser = FB;
                                            }
                                            else
                                            {
                                                FBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                            }



                                            ///////////////////////////////////////
                                            /////فیلدهای آیتمی که درج میگردد//////
                                            ///////////////////////////////////////

                                            List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems && x.NoeFB == NoeFBId).ToList();

                                            string[] FieldsAddingSplit = FieldsAdding.Split(",");

                                            string strShomareh1 = varFBShomareh;
                                            Guid guFBId = BaseFBId;
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

                                            foreach (var itemRM in lstRM)
                                            {
                                                //string strCondition = ItemsAdding.Condition.ToString().Trim();

                                                string strConditionOp = strCondition.Replace("x", itemRM.Arz.ToString().Trim());
                                                StringToFormula StringToFormula = new StringToFormula();
                                                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                                if (blnCheck)
                                                {
                                                    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + itemRM.Shomareh);

                                                    if (DrRizMetreUsersesCurrent.Length == 0)
                                                    {
                                                        //decimal? Tedad = itemRM.Tedad;// DtRizMetreUserses.Rows[i]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tedad"].ToString());
                                                        //decimal? Tool = itemRM.Tool;// DtRizMetreUserses.Rows[i]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Tool"].ToString());
                                                        //decimal? Arz = itemRM.Arz;// DtRizMetreUserses.Rows[i]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Arz"].ToString());
                                                        //decimal? Ertefa = itemRM.Ertefa;// DtRizMetreUserses.Rows[i]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Ertefa"].ToString());
                                                        //decimal? Vazn = itemRM.Vazn;// DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());



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
                                                            var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                            dTedad = tedadField?.DefaultValue ?? itemRM.Tedad;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                            dTool = toolField?.DefaultValue ?? itemRM.Tool;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                            dArz = arzField?.DefaultValue ?? itemRM.Arz;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                            dErtefa = ertefaField?.DefaultValue ?? itemRM.Ertefa;
                                                        }
                                                        strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                        if (strCal.Count != 0)
                                                        {
                                                            var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                                            dVazn = vaznField?.DefaultValue ?? itemRM.Vazn;
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

                                                        RizMetreUserses.Des = itemRM.Des; //DtRizMetreUserses.Rows[i]["Des"].ToString().Trim(); //Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + varFBShomareh
                                                                                          //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                                                        RizMetreUserses.FBId = FBId;
                                                        RizMetreUserses.OperationsOfHamlId = 1;
                                                        RizMetreUserses.Type = "2";
                                                        RizMetreUserses.ForItem = varFBShomareh;
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


                                                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                            new clsItemsHasConditionAddedToFBRizMetre
                                                            {
                                                                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                                RizMetreId = RizMetreUserses.ID,
                                                                BaseRizMetreId = itemRM.Id
                                                            });

                                                        dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                        //Start///////////////////////
                                                        ////////////حمل///////////////
                                                        //////////////////////////////
                                                        ///آیتم های حمل درج میگردد
                                                        ///


                                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                        {
                                                            BarAvordUserId = guBAId,
                                                            Year = Year,
                                                            ItemFBShomareh = strItemShomareh,
                                                            NoeFBId = NoeFBId,
                                                            //BarAvordHamlId = gBarAvordHamlId,
                                                            Shomareh = RizMetreUserses.Shomareh,
                                                            MeghdarJoz = dMeghdarJoz,
                                                            LevelNumber = 1,
                                                        };
                                                        await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                        //RizMetreUserses.Save();
                                                    }
                                                }
                                            }

                                            /////////////////////////////////////
                                            //////دریافت جمع ریزمتره های جاری///
                                            /////////////////////////////////////
                                            decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                            decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                   ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                   : null;

                                            varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                            await _context.SaveChangesAsync();
                                        }
                                    }
                                    break;
                                }
                            case "17":
                                {
                                    ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                                    {
                                        strFinalWorking = strFinalWorking,
                                        Meghdar = Meghdar,
                                        Meghdar2 = Meghdar2,
                                        strCondition = strCondition,
                                        strItemsFBShomareh = strItemsFBShomareh,
                                        lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                                        ConditionGroupId = ConditionGroupId,
                                        BarAvordId = BarAvordId,
                                        NoeFBId = NoeFBId,
                                        strCharacterPlus = strCharacterPlus,
                                        FBShomarehAddedItem = FBShomarehAddedItem,
                                        DesOfAddingItems = DesOfAddingItems,
                                        BaseFBId = BaseFBId,
                                        Year = Year,
                                        FieldsAdding = FieldsAdding,
                                        ShomarehNew = ShomareNew,
                                        Now = Now,
                                        ConditionContextRel = ConditionContextRel,
                                        ConditionContextId = ConditionContextId,
                                        blnCheckIsExistErrors = blnCheckIsExistErrors,
                                        LevelNumber = LevelNumber,
                                        blnCheckAgain = blnCheckAgain,
                                        blnCheckIsExistWarning = blnCheckIsExistWarning,
                                        strWarning = strWarning,
                                        RMIdForEdit = null
                                    };

                                    ConditionCaseCommon CCC = new ConditionCaseCommon();
                                    //bool blnResult = await CCC.Case17(RequestCase1, context);
                                    ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case17(RequestCase1, context);

                                    blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                                    strWarning = ResultCCC.strWarning;
                                    Status = ResultCCC.blnStatus;

                                    break;
                                }
                            case "18":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    //string strCondition = ItemsAdding.Condition.ToString().Trim();

                                    //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                    DataTable DtRizMetreUserses = new DataTable();
                                    string strForItem = "";
                                    string strUseItem = "";
                                    string strItemFBShomareh = varFBShomareh;

                                    Guid guFBId = BaseFBId;
                                    strUseItem = varFBShomareh;
                                    strForItem = varFBShomareh;// ItemsAdding.UseItemForAdd.ToString().Trim();


                                    ///////////////////////////////////////
                                    /////فیلدهای آیتمی که درج میگردد//////
                                    ///////////////////////////////////////

                                    List<clsItemsFields> lstCurrentItemFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems).ToList();

                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                        join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                        where RUsers.LevelNumber == LevelNumber && RUsers.FBId == guFBId && RUsers.Type == "1"
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
                                                                                                        }).OrderBy(x => x.Shomareh).ToList();


                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                    bool blnCheckSave = false;

                                    Guid currentItemsHasConditionAddedToFBId = new Guid();
                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                        ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;


                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            await _context.SaveChangesAsync();
                                            blnCheckSave = true;
                                            currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = false;
                                        }
                                    }
                                    else
                                    {
                                        blnCheckSave = true;
                                        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                    }

                                    //if (ItemsHasConditionAddedToFB.Save())
                                    if (blnCheckSave)
                                    {
                                        string[] FieldsAddingSplit = FieldsAdding.Split(",");
                                        string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                        StringToFormula StringToFormula = new StringToFormula();
                                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        if (blnCheck)
                                        {
                                            blnCheckIsExistErrors = "false";
                                            blnCheckIsExistWarning = false;

                                            List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();
                                            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();

                                            clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                            Guid FBId = new Guid();
                                            if (varFBUser == null)
                                            {
                                                clsFB FB = new clsFB();
                                                FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                                FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim();
                                                FB.BahayeVahedZarib = 0;
                                                FB.NoeFBId = NoeFBId;
                                                _context.FBs.Add(FB);
                                                await _context.SaveChangesAsync();
                                                FBId = FB.ID;
                                                varFBUser = FB;
                                            }
                                            else
                                                FBId = varFBUser.ID;

                                            foreach (var RM in varRizMetreUserses)
                                            {
                                                if (strFinalWorking != "")
                                                {
                                                    string[] strFinalWorkingSplit = strFinalWorking.Split('-');
                                                    int intReapetCount = (int.Parse(strFinalWorkingSplit[1]) - 20) / 5;

                                                    //به تعداد intReapetCount
                                                    //رکورد با ارتفاع 5 درج می گردد
                                                    //decimal? Tedad = RM.Tedad == 0 ? 1 : RM.Tedad;
                                                    //decimal? Tool = RM.Tool;
                                                    //decimal? Arz = RM.Arz;
                                                    //decimal? Vazn = 1;


                                                    List<string> lst = new List<string>();
                                                    for (int j = 0; j < FieldsAddingSplit.Length; j++)
                                                    {
                                                        lst.Add(FieldsAddingSplit[j]);
                                                    }


                                                    decimal? dTedad = null;
                                                    decimal? dTool = null;
                                                    decimal? dArz = null;
                                                    decimal? dErtefa = 1;
                                                    decimal? dVazn = 1;



                                                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                        dTedad = tedadField?.DefaultValue ?? RM.Tedad;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                        dTool = toolField?.DefaultValue ?? RM.Tool;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                        dArz = arzField?.DefaultValue ?? RM.Arz;
                                                    }

                                                    for (int i = 0; i < intReapetCount; i++)
                                                    {
                                                        dErtefa = 5;
                                                        clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                                                        RizMetreUserses1.Shomareh = RM.Shomareh;
                                                        ShomareNew++;
                                                        RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                                                        RizMetreUserses1.Sharh = RM.Sharh;
                                                        RizMetreUserses1.Tedad = dTedad;
                                                        RizMetreUserses1.Tool = dTool;
                                                        RizMetreUserses1.Arz = dArz;
                                                        RizMetreUserses1.Ertefa = dErtefa;
                                                        RizMetreUserses1.Vazn = dVazn++;
                                                        RizMetreUserses1.Des = RM.Des;

                                                        RizMetreUserses1.FBId = FBId;
                                                        RizMetreUserses1.OperationsOfHamlId = 1;
                                                        RizMetreUserses1.Type = "2";
                                                        RizMetreUserses1.ForItem = strForItem;
                                                        RizMetreUserses1.UseItem = strUseItem;
                                                        RizMetreUserses1.LevelNumber = LevelNumber;
                                                        RizMetreUserses1.InsertDateTime = Now;

                                                        RizMetreUserses1.ConditionContextRel = ConditionContextRel;
                                                        RizMetreUserses1.ConditionContextId = ConditionContextId;

                                                        ///محاسبه مقدار جزء
                                                        decimal? dMeghdarJoz1 = null;
                                                        if (dTedad == null && dTool == null && dArz == null && dVazn == null)
                                                            dMeghdarJoz1 = null;
                                                        else
                                                            dMeghdarJoz1 = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                            (dArz == null ? 1 : dArz.Value) * dErtefa * (dVazn == null ? 1 : dVazn.Value);
                                                        RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                                                        _context.RizMetreUserses.Add(RizMetreUserses1);

                                                        dSumAllRizMetre += dMeghdarJoz1 ?? 0;
                                                    }


                                                    strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());

                                                    //رکورد آخری هم بنا به شرط فاینل ارتفاع درج میگردد
                                                    decimal Ertefa = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));


                                                    //DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh + " and FBId=" + FBId);
                                                    //if (DrRizMetreUsersesCurrent.Length == 0)
                                                    //{

                                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                                    RizMetreUserses.Shomareh = RM.Shomareh;
                                                    ShomareNew++;
                                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                                    RizMetreUserses.Sharh = RM.Sharh;
                                                    RizMetreUserses.Tedad = dTedad;
                                                    RizMetreUserses.Tool = dTool;
                                                    RizMetreUserses.Arz = dArz;
                                                    RizMetreUserses.Ertefa = Ertefa;
                                                    RizMetreUserses.Vazn = dVazn;
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
                                                    if (dTedad == null && dTool == null && dArz == null && dVazn == null)
                                                        dMeghdarJoz = null;
                                                    else
                                                        dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                        (dArz == null ? 1 : dArz.Value) * Ertefa * (dVazn == null ? 1 : dVazn.Value);
                                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                                    _context.RizMetreUserses.Add(RizMetreUserses);

                                                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                    new clsItemsHasConditionAddedToFBRizMetre
                                                    {
                                                        ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                        RizMetreId = RizMetreUserses.ID,
                                                        BaseRizMetreId = RM.Id
                                                    });

                                                    dSumAllRizMetre += dMeghdarJoz ?? 0;

                                                    //Start///////////////////////
                                                    ////////////حمل///////////////
                                                    //////////////////////////////
                                                    ///آیتم های حمل درج میگردد
                                                    ///


                                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                    {
                                                        BarAvordUserId = BarAvordId,
                                                        Year = Year,
                                                        ItemFBShomareh = strItemShomareh,
                                                        NoeFBId = NoeFBId,
                                                        //BarAvordHamlId = gBarAvordHamlId,
                                                        Shomareh = RizMetreUserses.Shomareh,
                                                        MeghdarJoz = dMeghdarJoz,
                                                        LevelNumber = 1,
                                                    };
                                                    await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                }
                                                /////////////////////////////////////
                                                //////دریافت جمع ریزمتره های جاری///
                                                /////////////////////////////////////
                                                decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                                decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                                       ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                                       : null;

                                                varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                                await _context.SaveChangesAsync();
                                            }
                                        }
                                        else
                                        {
                                            if (blnCheckIsExistErrors != "false")
                                            {
                                                blnCheckIsExistErrors = "true";
                                                blnCheckIsExistWarning = true;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "19":
                                {
                                    decimal dSumAllRizMetre = 0;
                                    //string FieldsAdding = ItemsAdding.FieldsAdding.ToString();
                                    //string? ConditionContextRel = ItemsAdding.ConditionContextRel.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextRel.ToString().Trim();
                                    //long? ConditionContextId = long.Parse(ItemsAdding.ConditionContextId.ToString().Trim() == "" ? null : ItemsAdding.ConditionContextId.ToString().Trim());

                                    string[] strCondition2Condition = ItemsAdding.Condition.ToString().Trim().Split(',');

                                    string strConditionY = strCondition2Condition[0].Trim();
                                    string strConditionZ = strCondition2Condition[1].Trim();
                                    //string strCharacterPlus = ItemsAdding.CharacterPlus.ToString().Trim();

                                    //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                    DataTable DtRizMetreUserses = new DataTable();
                                    string strForItem = "";
                                    string strUseItem = "";
                                    string strItemFBShomareh = varFBShomareh;

                                    Guid guFBId = BaseFBId;
                                    strUseItem = varFBShomareh;
                                    strForItem = varFBShomareh;


                                    ///////////////////////////////////////
                                    /////فیلدهای آیتمی که درج میگردد//////
                                    ///////////////////////////////////////
                                    List<clsItemsFields> lstCurrentItemFields = await _context.ItemsFieldses.Where(x => x.ItemShomareh == ItemsAdding.AddedItems && x.NoeFB == NoeFBId).ToListAsync();

                                    List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                                                                                                        join fb in _context.FBs on RUsers.FBId equals fb.ID
                                                                                                        where RUsers.LevelNumber == LevelNumber && RUsers.FBId == guFBId && RUsers.Type == "1"
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
                                                                                                        }).OrderBy(x => x.Shomareh).ToList();


                                    //long lngItemsHasCondition_ConditionContextId = long.Parse(ItemsAdding.ItemsHasCondition_ConditionContextId.ToString());
                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                    bool blnCheckSave = false;
                                    Guid currentItemsHasConditionAddedToFBId = new Guid();
                                    if (currentItemsHasConditionAddedToFBs == null)
                                    {
                                        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                        ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;
                                        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                        ItemsHasConditionAddedToFB.Meghdar = 0;
                                        ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                        ItemsHasConditionAddedToFB.NoeFB = NoeFBId;


                                        try
                                        {
                                            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                            await _context.SaveChangesAsync();
                                            blnCheckSave = true;
                                            currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                                        }
                                        catch (Exception)
                                        {
                                            blnCheckSave = false;
                                        }
                                    }
                                    else
                                    {
                                        blnCheckSave = true;
                                        currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                                    }

                                    if (blnCheckSave)
                                    {

                                        List<clsFB> lstFBUser = await _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToListAsync();
                                        string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus.Trim();
                                        string strDesOfAddingItems = ItemsAdding.DesOfAddingItems.ToString();

                                        clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                        Guid FBId = new Guid();
                                        if (varFBUser == null)
                                        {
                                            clsFB FB = new clsFB();
                                            FB.BarAvordId = BarAvordId;
                                            FB.Shomareh = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus.Trim();
                                            FB.BahayeVahedZarib = strFinalWorking.Length > 1 ? decimal.Parse(strFinalWorking) : 0;
                                            FB.BahayeVahedSharh = strDesOfAddingItems;
                                            FB.NoeFBId = NoeFBId;
                                            _context.FBs.Add(FB);
                                            await _context.SaveChangesAsync();
                                            FBId = FB.ID;
                                            varFBUser = FB;
                                        }
                                        else
                                            FBId = varFBUser.ID;


                                        string[] FieldsAddingSplit = FieldsAdding.Split(",");
                                        foreach (var RM in varRizMetreUserses)
                                        {
                                            string strConditionOpY = strConditionY.Replace("y", RM.Arz != null ? RM.Arz.Value.ToString().Trim() : "");
                                            string strConditionOpZ = strConditionZ.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");

                                            StringToFormula stringToFormula = new StringToFormula();
                                            bool blnCheckY = stringToFormula.RelationalExpression2(strConditionOpY);
                                            bool blnCheckZ = stringToFormula.RelationalExpression2(strConditionOpZ);
                                            if (blnCheckY && blnCheckZ)
                                            {
                                                if (strFinalWorking != "")
                                                {

                                                    //decimal? Tedad = RM.Tedad;
                                                    //decimal? Tool = RM.Tool;
                                                    //decimal? Arz = RM.Arz;
                                                    //decimal? Ertefa = RM.Ertefa;
                                                    //decimal? Vazn = RM.Vazn;

                                                    clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                                                    RizMetreUserses1.Shomareh = RM.Shomareh;
                                                    ShomareNew++;
                                                    RizMetreUserses1.ShomarehNew = ShomareNew.ToString();


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
                                                        var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                                                        dTedad = tedadField?.DefaultValue ?? RM.Tedad;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                                                        dTool = toolField?.DefaultValue ?? RM.Tool;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                                                        dArz = arzField?.DefaultValue ?? RM.Arz;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                                                        dErtefa = ertefaField?.DefaultValue ?? RM.Ertefa;
                                                    }
                                                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                                    if (strCal.Count != 0)
                                                    {
                                                        var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                                                        dVazn = vaznField?.DefaultValue ?? RM.Vazn;
                                                    }

                                                    RizMetreUserses1.Sharh = RM.Sharh;
                                                    RizMetreUserses1.Tedad = dTedad;
                                                    RizMetreUserses1.Tool = dTool;
                                                    RizMetreUserses1.Arz = dArz;
                                                    RizMetreUserses1.Ertefa = dErtefa;
                                                    RizMetreUserses1.Vazn = dVazn;
                                                    RizMetreUserses1.Des = RM.Des;

                                                    RizMetreUserses1.FBId = FBId;
                                                    RizMetreUserses1.OperationsOfHamlId = 1;
                                                    RizMetreUserses1.Type = "2";
                                                    RizMetreUserses1.ForItem = strForItem;
                                                    RizMetreUserses1.UseItem = strUseItem;
                                                    RizMetreUserses1.LevelNumber = LevelNumber;
                                                    RizMetreUserses1.InsertDateTime = Now;

                                                    RizMetreUserses1.ConditionContextRel = ConditionContextRel;
                                                    RizMetreUserses1.ConditionContextId = ConditionContextId;

                                                    ///محاسبه مقدار جزء
                                                    decimal? dMeghdarJoz1 = null;
                                                    if (dTedad == null && dTool == null && dArz == null && dVazn == null && dErtefa == null)
                                                        dMeghdarJoz1 = null;
                                                    else
                                                        dMeghdarJoz1 = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                                    RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                                                    _context.RizMetreUserses.Add(RizMetreUserses1);


                                                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                                                        new clsItemsHasConditionAddedToFBRizMetre
                                                        {
                                                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                                                            RizMetreId = RizMetreUserses1.ID,
                                                            BaseRizMetreId = RM.Id
                                                        });

                                                    dSumAllRizMetre += dMeghdarJoz1 ?? 0;

                                                    //Start///////////////////////
                                                    ////////////حمل///////////////
                                                    //////////////////////////////
                                                    ///آیتم های حمل درج میگردد
                                                    ///


                                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                                    {
                                                        BarAvordUserId = BarAvordId,
                                                        Year = Year,
                                                        ItemFBShomareh = strItemShomareh,
                                                        NoeFBId = NoeFBId,
                                                        //BarAvordHamlId = gBarAvordHamlId,
                                                        Shomareh = RizMetreUserses1.Shomareh,
                                                        MeghdarJoz = dMeghdarJoz1,
                                                        LevelNumber = 1,
                                                    };
                                                    await HamlCommon.SaveHaml(requestSaveHaml, context);
                                                }
                                            }
                                        }

                                        /////////////////////////////////////
                                        //////دریافت جمع ریزمتره های جاری///
                                        /////////////////////////////////////
                                        decimal? dSumRizMetreCurrentFB = RizMetreCommon.SumRizMetreCurrentFB(FBId, _context);

                                        decimal? dSumRizMetreCurrentFBAll = dSumRizMetreCurrentFB.HasValue
                                               ? (dSumRizMetreCurrentFB ?? 0) + dSumAllRizMetre
                                               : null;

                                        varFBUser.SumRizMetre = dSumRizMetreCurrentFBAll;

                                        await _context.SaveChangesAsync();
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
                    return new JsonResult("NOOK_" + strWarning);
                }
                else
                {
                    await _context.SaveChangesAsync();
                    return new JsonResult("OK");
                }

                // return "OK_";
            }

        }


        private bool CheckEntryMeghdar(string? MinValueCheck, string? MaxValueCheck, decimal Meghdar, decimal Meghdar2, int EnteringCount)
        {
            string[] maxValues = string.IsNullOrWhiteSpace(MaxValueCheck)
                ? new string[2]
                : MaxValueCheck.Split('_');

            string[] minValues = string.IsNullOrWhiteSpace(MinValueCheck)
                ? new string[2]
                : MinValueCheck.Split('_');


            // =========================
            // بررسی Meghdar
            // =========================
            //if (Meghdar != 0)
            //{
            decimal? minValue = null;
            decimal? maxValue = null;

            // Min برای Meghdar
            if (minValues.Length > 0 &&
                !string.IsNullOrWhiteSpace(minValues[0]) &&
                decimal.TryParse(minValues[0], out decimal min))
            {
                minValue = min;
            }

            // Max برای Meghdar
            if (maxValues.Length > 0 &&
                !string.IsNullOrWhiteSpace(maxValues[0]) &&
                decimal.TryParse(maxValues[0], out decimal max))
            {
                maxValue = max;
            }

            // بررسی Min
            if (minValue.HasValue && Meghdar < minValue.Value)
            {
                // شرط برقرار نیست
                return false;
            }

            // بررسی Max
            if (maxValue.HasValue && Meghdar > maxValue.Value)
            {
                // شرط برقرار نیست
                return false;
            }
            //}


            // =========================
            // بررسی Meghdar2
            // =========================
            bool blnCheckMeghdar2 = true;
            if (EnteringCount > 1)
            {
                if (Meghdar2 == 0)
                {
                    blnCheckMeghdar2 = false;
                    return false;
                }
            }
            if (blnCheckMeghdar2)
            {
                minValue = null;
                maxValue = null;

                // Min برای Meghdar2
                if (minValues.Length > 1 &&
                    !string.IsNullOrWhiteSpace(minValues[1]) &&
                    decimal.TryParse(minValues[1], out decimal min1))
                {
                    minValue = min1;
                }

                // Max برای Meghdar2
                if (maxValues.Length > 1 &&
                    !string.IsNullOrWhiteSpace(maxValues[1]) &&
                    decimal.TryParse(maxValues[1], out decimal max1))
                {
                    maxValue = max1;
                }

                // بررسی Min
                if (minValue.HasValue && Meghdar2 < minValue.Value)
                {
                    // شرط برقرار نیست
                    return false;
                }

                // بررسی Max
                if (maxValue.HasValue && Meghdar2 > maxValue.Value)
                {
                    // شرط برقرار نیست
                    return false;
                }
            }

            // همه شروط برقرار است
            return true;

        }


        [HttpPost]
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
            var varFB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strItemsFBShomareh).ToList();
            DataTable DtFB = clsConvert.ToDataTable(varFB);
            string strFBShomareh = Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            //DataTable DtItemsAddingToFB = new DataTable();

            var varItemsHasConditionAddedToFB = (from clsItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
                                                 join tblItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
                                                 on clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasCondition_ConditionContext.Id
                                                 join tblItemsHasCondition in _context.ItemsHasConditions on tblItemsHasCondition_ConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
                                                 where tblItemsHasCondition.Year == Year && tblItemsHasCondition.NoeFB == NoeFB
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


                List<clsItemsAddingToFB> lstItemsAddingToFB = _context.ItemsAddingToFBs
                            .Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)
                                          && x.Year == Year && x.NoeFB == NoeFB).ToList();



                List<ItemsAddingToFBForShowAddItemsDto> lstItemsAddingToFBForShowAddItems = new List<ItemsAddingToFBForShowAddItemsDto>();

                foreach (var ItemsAddingToFB in lstItemsAddingToFB)
                {
                    string[] currentItemsAdding = ItemsAddingToFB.AddedItems.Split('_');

                    foreach (var ItemsAdding in currentItemsAdding)
                    {
                        clsFehrestBaha? FehrestBaha = _context.FehrestBahas.FirstOrDefault(x => x.Sal == Year && x.NoeFB == NoeFB && x.Shomareh.Trim() == ItemsAdding.Trim());

                        if (FehrestBaha != null)
                        {
                            ItemsAddingToFBForShowAddItemsDto ItemsAddingToFBForShowAddItems = new ItemsAddingToFBForShowAddItemsDto
                            {
                                Id = ItemsAddingToFB.Id,
                                ItemsHasCondition_ConditionContextId = ItemsAddingToFB.ItemsHasCondition_ConditionContextId,
                                AddedItems = ItemsAddingToFB.AddedItems,
                                Condition = ItemsAddingToFB.Condition,
                                FinalWorking = ItemsAddingToFB.FinalWorking,
                                ConditionType = ItemsAddingToFB.ConditionType,
                                DesOfAddingItems = ItemsAddingToFB.DesOfAddingItems,
                                UseItemForAdd = ItemsAddingToFB.UseItemForAdd,
                                FieldsAdding = ItemsAddingToFB.FieldsAdding,
                                CharacterPlus = ItemsAddingToFB.CharacterPlus,
                                Shomareh = FehrestBaha.Shomareh,
                                Sharh = FehrestBaha.Sharh
                            };

                            lstItemsAddingToFBForShowAddItems.Add(ItemsAddingToFBForShowAddItems);
                        }
                    }

                }

                //var lstItemsAddingToFB = (from ItemsAddingToFB in _context.ItemsAddingToFBs
                //                          join FehrestBaha in _context.FehrestBahas on ItemsAddingToFB.AddedItems.Trim() equals FehrestBaha.Shomareh.Trim()
                //                          where strItemsHasCondition_ConditionContext.Contains(ItemsAddingToFB.ItemsHasCondition_ConditionContextId)
                //                          && FehrestBaha.Sal == Year && FehrestBaha.NoeFB == NoeFB
                //                          select new
                //                          {
                //                              ItemsAddingToFB.Id,
                //                              ItemsAddingToFB.ItemsHasCondition_ConditionContextId,
                //                              ItemsAddingToFB.AddedItems,
                //                              ItemsAddingToFB.Condition,
                //                              ItemsAddingToFB.FinalWorking,
                //                              ItemsAddingToFB.ConditionType,
                //                              ItemsAddingToFB.DesOfAddingItems,
                //                              ItemsAddingToFB.UseItemForAdd,
                //                              ItemsAddingToFB.FieldsAdding,
                //                              ItemsAddingToFB.CharacterPlus,
                //                              FehrestBaha.Sharh,
                //                              FehrestBaha.Shomareh,
                //                          }).ToList();
                //DtItemsAddingToFB = clsConvert.ToDataTable(lstItemsAddingToFB);

                List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> lstItemFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFB).Select(x => new ItemFBShomarehForGetAndShowAddItemsFieldsDto
                {
                    Shomareh = x.ItemShomareh,
                    FieldType = x.FieldType,
                    Vahed = x.Vahed,
                    IsEnteringValue = x.IsEnteringValue,
                    EssentialValue = x.EssentialValue
                }).ToList();

                for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
                {
                    bool blnViewCheckAllRecords = DtItemsHasConditionAddedToFB.Rows[Counter]["ViewCheckAllRecords"].ToString().Trim() == "True" ? true : false;
                    bool blnEnableEditing = DtItemsHasConditionAddedToFB.Rows[Counter]["EnableEditing"].ToString().Trim() == "True" ? true : false;
                    bool blnEnableDeleting = DtItemsHasConditionAddedToFB.Rows[Counter]["EnableDeleting"].ToString().Trim() == "True" ? true : false;
                    decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
                    string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();
                    //DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);

                    List<ItemsAddingToFBForShowAddItemsDto> ItemsAddingToFB =
                        lstItemsAddingToFBForShowAddItems.Where(x => x.ItemsHasCondition_ConditionContextId == long.Parse(RBCode)).ToList();
                    //if (Dr.Length != 0)
                    if (ItemsAddingToFB != null)
                    {
                        //for (int idr = 0; idr < Dr.Length; idr++)
                        foreach (var itemAdding in ItemsAddingToFB)
                        {
                            string strFBShomarehAdded = itemAdding.AddedItems; //ItemsAdding.AddedItems.ToString().Trim();

                            List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> ItemFields = new List<ItemFBShomarehForGetAndShowAddItemsFieldsDto>();
                            ItemFields = lstItemFields.Where(x => x.Shomareh.Trim() == strFBShomarehAdded).ToList();

                            string strCharacterPlus = itemAdding.CharacterPlus == null ? "" : itemAdding.CharacterPlus.ToString().Trim(); //ItemsAdding.CharacterPlus.ToString().Trim();
                            string strSharh = itemAdding.Sharh == null ? "" : itemAdding.Sharh.ToString().Trim(); //ItemsAdding.Sharh"].ToString().Trim();
                            ItemFBShomarehForGetAndShowAddItemsDto ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto();
                            //{
                            //    ItemFBShomareh = strFBShomarehAdded,
                            //    Des = strSharh,
                            //    ItemFields = ItemFields

                            //};
                            //lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);


                            //switch (ItemsAdding.ConditionType"].ToString())
                            switch (itemAdding.ConditionType.ToString())
                            {
                                case "1":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded.Trim() + strCharacterPlus);

                                        if (varFBUsersAdded != null)
                                        {

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim() + strCharacterPlus))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded.Trim() + strCharacterPlus,
                                                    Des = varFBUsersAdded.BahayeVahedSharh == "" ? strSharh : varFBUsersAdded.BahayeVahedSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }
                                            string[] strFinalWorking1 = itemAdding.FinalWorking == null ? "".Split('_') : itemAdding.FinalWorking.ToString().Split('_');
                                            string strFinalWorking = strFinalWorking1[0];


                                            //var match = Regex.Match(strFinalWorking, @"\b([a-zA-Z])\b");

                                            //string variableName = match.Success ? match.Groups[1].Value : null;
                                            ///
                                            //مقدار ورودی بنا به این در بخش عملیات نهایی چه حرفی گذاشته شده است
                                            //در یکی از فیلدهای طول عرض ارتفاع وزن گذاشته میشود
                                            //switch (variableName)
                                            //{
                                            //    //طول
                                            //    case "x":
                                            //        {

                                            //            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            //            break;
                                            //        }
                                            //    //عرض
                                            //    case "y":
                                            //        {
                                            //            strFinalWorking = strFinalWorking.Replace("y", Meghdar.ToString().Trim());
                                            //            break;
                                            //        }
                                            //    //ارتفاع
                                            //    case "z":
                                            //        {
                                            //            strFinalWorking = strFinalWorking.Replace("z", Meghdar.ToString().Trim());
                                            //            break;
                                            //        }
                                            //    //وزن
                                            //    case "w":
                                            //        {
                                            //            strFinalWorking = strFinalWorking.Replace("w", Meghdar.ToString().Trim());
                                            //            break;
                                            //        }
                                            //    default:
                                            //        break;
                                            //}

                                            //strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                                            //StringToFormula StringToFormula = new StringToFormula();

                                            //decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;

                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre =
                                                _context.RizMetreUserses.Include(x => x.FB)
                                                .Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh)
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
                                                    ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded.Trim() + strCharacterPlus,
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
                                case "15":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded.Trim() + strCharacterPlus);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        if (varFBUsersAdded != null)
                                        {
                                            //lstItemFBShomarehForGet.Clear();
                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim() + strCharacterPlus))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded.Trim() + strCharacterPlus,// varFBUsersAdded.Shomareh,// DtFBUsersAdded.Rows[0]["Shomareh"].ToString(),
                                                    Des = varFBUsersAdded.BahayeVahedSharh,// DtFBUsersAdded.Rows[0]["BahayeVahedSharh"].ToString(),
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }
                                            string[] strFinalWorking1 = itemAdding.FinalWorking.ToString().Split('_'); //ItemsAdding.FinalWorking.ToString().Split('_');
                                            string strFinalWorking = strFinalWorking1[0];
                                            strFinalWorking = strFinalWorking.Replace("y", Meghdar.ToString().Trim());
                                            StringToFormula StringToFormula = new StringToFormula();
                                            decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                            Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                                                        //clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();

                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre =
                                                _context.RizMetreUserses.Include(x => x.FB)
                                                .Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh)
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
                                                    ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded.Trim() + strCharacterPlus,
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
                                case "17":
                                case "2":
                                case "16":
                                case "18":
                                case "19":
                                    {
                                        var varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded + strCharacterPlus);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (varFBUsersAdded != null)
                                        {
                                            //lstItemFBShomarehForGet.Clear();

                                            string BahayeVahedSharh = varFBUsersAdded.BahayeVahedSharh;

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded + strCharacterPlus,
                                                    Des = varFBUsersAdded.BahayeVahedSharh == "" ? strSharh : varFBUsersAdded.BahayeVahedSharh,

                                                    //Des = strCharacterPlus != "" ? BahayeVahedSharh : strSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }
                                            Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            var RizMetre = _context.RizMetreUserses.Include(x => x.FB)
                                                .Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()
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
                                                    ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded + strCharacterPlus,
                                                    HasDelButton = blnEnableDeleting,
                                                    HasEditButton = blnEnableEditing
                                                }).ToList();
                                            //if (RizMetre != null)
                                            //{
                                            //    if (RizMetre.Count != 0)
                                            //    {
                                            //        lstFbs.AddRange(RizMetre);
                                            //    }
                                            //}



                                            if (RizMetre != null && RizMetre.Count != 0)
                                            {
                                                var existingIds = lstFbs.Select(x => x.Id).ToHashSet();
                                                foreach (var item in RizMetre)
                                                {
                                                    if (existingIds.Add(item.Id)) { lstFbs.Add(item); }
                                                }
                                            }

                                        }
                                        break;
                                    }
                                case "3":
                                case "4":
                                case "5":
                                case "14":
                                    {

                                        string strCondition = itemAdding.Condition; //ItemsAdding.Condition.ToString().Trim();
                                        string strFinalWorking = itemAdding.FinalWorking;// ItemsAdding.FinalWorking.ToString();
                                        string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                        StringToFormula StringToFormula = new StringToFormula();
                                        bool blnCheck = true;
                                        if (strConditionOp != "")
                                        {
                                            blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                        }

                                        if (blnCheck)
                                        {
                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded + strCharacterPlus);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);


                                            if (varFBUsersAdded != null)
                                            {
                                                //lstItemFBShomarehForGet.Clear();

                                                if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                                {

                                                    ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                    {
                                                        ItemFBShomareh = strFBShomarehAdded,
                                                        Des = strSharh,
                                                        ItemFields = ItemFields
                                                    };
                                                    lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                                }

                                                Guid guFBUsersAddedId = varFBUsersAdded.ID;// Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
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
                                                        ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
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
                                        }
                                        break;
                                    }
                                case "6":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (varFBUsersAdded != null)
                                        {

                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    Des = strSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;//Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
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
                                                    ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
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
                                case "7":
                                    {
                                        //decimal dPercent = decimal.Parse(Dr[0]["FinalWorking.ToString());
                                        //string strStatus = dPercent > 0 ? "B" : "e";
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (varFBUsersAdded != null)
                                        {

                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    Des = strSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                                                        //clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                                                        //if (Fb != null)
                                                                                        //{
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
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
                                                ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
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
                                            //}
                                        }
                                        break;
                                    }
                                case "8":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (varFBUsersAdded != null)
                                        {

                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    Des = strSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre =
                                                _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim() && x.LevelNumber == LevelNumber)
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
                                                    ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
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
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        if (varFBUsersAdded != null)
                                        {

                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    Des = strSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }

                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            //string strCondition = ItemsAdding.Condition.ToString().Trim();
                                            //string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                            //string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                            //StringToFormula StringToFormula = new StringToFormula();
                                            //bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                            //if (blnCheck)
                                            //{
                                            string strForItem = "";
                                            string strUseItem = "";
                                            //if (ItemsAdding.UseItemForAdd.ToString().Trim() == "")
                                            string strUseItemForAdd = itemAdding.UseItemForAdd != null ? itemAdding.UseItemForAdd.Trim() : "";
                                            if (strUseItemForAdd == "")
                                                strForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            else
                                            {
                                                strForItem = strUseItemForAdd;// itemAdding.UseItemForAdd; //ItemsAdding.UseItemForAdd.ToString().Trim();
                                                strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                            }

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;// Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
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
                                                            ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
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
                                        strCharacterPlus = itemAdding.CharacterPlus == null ? "" : itemAdding.CharacterPlus.Trim();
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded + strCharacterPlus);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        //if (DtFBUsersAdded.Rows.Count != 0)


                                        if (varFBUsersAdded != null)
                                        {
                                            string BahayeVahedSharh = varFBUsersAdded.BahayeVahedSharh.Trim();

                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded + strCharacterPlus,
                                                    Des = strCharacterPlus != "" ? BahayeVahedSharh : strSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                                                        //clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
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
                                                ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
                                                ItemFBShomareh = strFBShomarehAdded + strCharacterPlus,
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

                                        string strFBShomarehAdded1 = itemAdding.Shomareh == null ? "" : itemAdding.Shomareh;// itemAdding.AddedItems.Split("_"); //ItemsAdding.AddedItems.ToString().Trim().Split("_");


                                        strCharacterPlus = itemAdding.CharacterPlus == null ? "" : itemAdding.CharacterPlus.ToString().Trim();
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded1 + strCharacterPlus);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                        //if (DtFBUsersAdded.Rows.Count != 0)
                                        if (varFBUsersAdded != null)
                                        {
                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded1.Trim() + strCharacterPlus))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = varFBUsersAdded.Shomareh,// DtFBUsersAdded.Rows[0]["Shomareh"].ToString(),
                                                    Des = varFBUsersAdded.BahayeVahedSharh,// DtFBUsersAdded.Rows[0]["BahayeVahedSharh"].ToString(),
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }

                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;// Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                                                       //clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses
                                                .Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
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
                                                    ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
                                                    ItemFBShomareh = strFBShomarehAdded1 + strCharacterPlus,
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
                                case "12":
                                    {
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded);
                                        if (varFBUsersAdded != null)
                                        {
                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {

                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = varFBUsersAdded.Shomareh,
                                                    Des = varFBUsersAdded.BahayeVahedSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }

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
                                                            ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
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
                                        clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFB && x.Shomareh == strFBShomarehAdded);
                                        //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                        //if (DtFBUsersAdded.Rows.Count != 0)
                                        if (varFBUsersAdded != null)
                                        {

                                            //lstItemFBShomarehForGet.Clear();

                                            if (!lstItemFBShomarehForGet.Exists(x => x.ItemFBShomareh.Trim() == strFBShomarehAdded.Trim()))
                                            {
                                                ItemFBShomarehForGet = new ItemFBShomarehForGetAndShowAddItemsDto
                                                {
                                                    ItemFBShomareh = strFBShomarehAdded,
                                                    Des = strSharh,
                                                    ItemFields = ItemFields
                                                };
                                                lstItemFBShomarehForGet.Add(ItemFBShomarehForGet);
                                            }


                                            Guid guFBUsersAddedId = varFBUsersAdded.ID;// Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
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
                                                    ShomarehNew = x.ShomarehNew == null ? x.Shomareh.ToString() : x.ShomarehNew,
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

                    if (lstFbs.Count == 0)
                    {
                        clsItemsHasConditionAddedToFB itemsHasConditionAddedToFB =
                            _context.ItemsHasConditionAddedToFBs.First(x => x.ID == Guid.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["ID"].ToString()));

                        _context.ItemsHasConditionAddedToFBs.Remove(itemsHasConditionAddedToFB);
                    }
                }
            }
            _context.SaveChanges();
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
