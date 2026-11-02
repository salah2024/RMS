using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using System;
using System.Data;
using System.Security.Cryptography;
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

        public async Task<JsonResult> DeleteItemsHasConditionAddedToFBAndRizMetre([FromBody] DeleteItemsHasConditionAddedToFBAndRizMetreInputDto request)
        {
            //try
            //{

            Guid BarAvordId = request.BarAvordId;
            string strFBShomareh = request.strFBShomareh.Trim();
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
                                                     ItemShomareh = ItemsHasCondition.ItemFBShomareh,
                                                     IHC_CC_Related = ItemsHasCondition_ConditionContext.IHC_CC_Related,
                                                     ConditionContextId= ItemsHasCondition_ConditionContext.ConditionContextId
                                                 }).Where(x => x.BarAvordId == BarAvordId && x.FBShomareh.Substring(0, 6) == strFBShomareh && x.ConditionGroupId == ConditionGroupId).ToList();


            if (varItemsHasConditionAddedToFB.Count != 0)
            {

                List<long> strItemsHasCondition_ConditionContext = new List<long>();
                for (int i = 0; i < varItemsHasConditionAddedToFB.Count; i++)
                {
                    strItemsHasCondition_ConditionContext.Add(varItemsHasConditionAddedToFB[i].ItemsHasCondition_ConditionContextId);
                }
                bool blnCheckIsDel = false;
                try
                {
                    var ItemsHasConditionAddedToFB = _context.ItemsHasConditionAddedToFBs.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh.Substring(0, 6) == strFBShomareh && x.ConditionGroupId == ConditionGroupId).ToList();
                    if (ItemsHasConditionAddedToFB.Count != 0)
                    {
                        _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
                        //_context.SaveChanges();
                    }
                    blnCheckIsDel = true;
                }
                catch (Exception)
                {
                    blnCheckIsDel = false;
                    //throw;
                }



                if (blnCheckIsDel)
                {

                    List<clsItemsAddingToFB> lstItemsAddingToFB = _context.ItemsAddingToFBs
                                .Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();

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

                    for (int Counter = 0; Counter < varItemsHasConditionAddedToFB.Count; Counter++)
                    {
                        decimal Meghdar = varItemsHasConditionAddedToFB[Counter].Meghdar;
                        string RBCode = varItemsHasConditionAddedToFB[Counter].ItemsHasCondition_ConditionContextId.ToString().Trim(); //DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();

                        long ConditionContextId = varItemsHasConditionAddedToFB[Counter].ConditionContextId;
                        long ItemsHasCondition_ConditionContextId = long.Parse(RBCode);

                        clsItemsHasConditionAddedToFB? itemsHasConditionAddedToFB =
                            _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.FBShomareh.Substring(0, 6) == strFBShomareh &&
                                x.BarAvordId == BarAvordId && x.ItemsHasCondition_ConditionContextId == ItemsHasCondition_ConditionContextId && x.NoeFB == NoeFB);


                        List<ItemsAddingToFBForShowAddItemsDto> ItemsAddingToFB =
                            lstItemsAddingToFBForShowAddItems.Where(x => x.ItemsHasCondition_ConditionContextId == long.Parse(RBCode)).ToList();

                        List<ItemsAddingToFBForShowAddItemsDto> lstItemsAdding = lstItemsAddingToFBForShowAddItems.Where(x => x.ItemsHasCondition_ConditionContextId == long.Parse(RBCode)).ToList();
                        if (lstItemsAdding.Count != 0)
                        {
                            foreach (var ItemsAdding in lstItemsAdding)
                            {

                                string strFBShomarehAdded = ItemsAdding.AddedItems.ToString().Trim();
                                string strCharacterPlus = ItemsAdding.CharacterPlus == null ? "" : ItemsAdding.CharacterPlus.ToString().Trim();

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

                                                //string strCharacterPlus = ItemsAdding.CharacterPlus==null?"": ItemsAdding.CharacterPlus.ToString().Trim();

                                                clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus);


                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                if (varFBUsersAdded != null)
                                                {
                                                    foreach (var RMetre in lstRizMetre)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            ConditionContextId= ConditionContextId,
                                                            RIzMetreId = RMetre.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = varFBUsersAdded.Shomareh,
                                                            RMShomareh = RMetre.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, _context);
                                                    }
                                                }
                                            }

                                            await _context.SaveChangesAsync();

                                            //string strCharacterPlus = ItemsAdding.CharacterPlus==null?"": ItemsAdding.CharacterPlus.ToString().Trim();

                                            //clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus);

                                            //if (varFBUsersAdded != null)
                                            //{
                                            //    Guid FBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            //    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                            //    if (tblRizMetreUser.Count != 0)
                                            //    {
                                            //        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);


                                            //        ///////////
                                            //        //حذف حمل//
                                            //        ///////////
                                            //        ///
                                            //        foreach (var item in tblRizMetreUser)
                                            //        {
                                            //            DeleteHamlDto deleteHaml = new DeleteHamlDto
                                            //            {
                                            //                BarAvordId = BarAvordId,
                                            //                FBShomareh = varFBUsersAdded.Shomareh,
                                            //                RMShomareh = item.Shomareh,
                                            //                NoeFBId = NoeFB
                                            //            };
                                            //            await HamlCommon.DeleteHaml(deleteHaml, context);
                                            //        }
                                            //        /////////////
                                            //        //////////////
                                            //        //////////////
                                            //        ///
                                            //        _context.SaveChanges();
                                            //    }
                                            //}
                                            break;
                                        }
                                    case "2":
                                        {
                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid FBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                if (tblRizMetreUser.Count != 0)
                                                {
                                                    _context.RizMetreUserses.RemoveRange(tblRizMetreUser);

                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///
                                                    foreach (var item in tblRizMetreUser)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId = item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = varFBUsersAdded.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }

                                                    /////////////
                                                    //////////////
                                                    //////////////

                                                    //bool blnHasHaml = false;
                                                    //List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = _context.ItemsRelatedToItemHamls
                                                    //    .Where(x => x.ItemFB.Trim() == strFBShomarehAdded.Trim() && x.Year == Year).ToList();

                                                    //foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
                                                    //{
                                                    //    string strItemHamlFB = "";
                                                    //    if (itemsRelatedToItemHaml != null)
                                                    //    {
                                                    //        strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                                                    //        blnHasHaml = true;

                                                    //        if (blnHasHaml)
                                                    //        {
                                                    //            string ItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB;
                                                    //            clsFB? FbHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemHamlFB);
                                                    //            if (FbHaml != null)
                                                    //            {
                                                    //                List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == FbHaml.ID && x.ForItem == strFBShomareh.Trim() && x.Type == "3").ToList();
                                                    //                _context.RizMetreUserses.RemoveRange(lstRMForDelForHaml);
                                                    //            }
                                                    //        }
                                                    //    }
                                                    //}

                                                    //_context.SaveChanges();

                                                }
                                                //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                            }
                                            await _context.SaveChangesAsync();

                                            break;
                                        }
                                    case "3":
                                        {
                                            decimal dPercent = decimal.Parse(ItemsAdding.FinalWorking.ToString());
                                            string strStatus = dPercent > 0 ? "B" : "e";
                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strStatus);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + strStatus + "'");
                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid FBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                if (tblRizMetreUser.Count != 0)
                                                {
                                                    _context.RizMetreUserses.RemoveRange(tblRizMetreUser);


                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///
                                                    foreach (var item in tblRizMetreUser)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId=item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = varFBUsersAdded.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////
                                                    //_context.SaveChanges();
                                                }
                                                //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["ID"].ToString().Trim() + " and _ForItem='" + strFBShomareh.Trim() + "'");
                                            }
                                            break;
                                        }
                                    case "4":
                                        {
                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                string strCondition = ItemsAdding.Condition.ToString().Trim();
                                                string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                                string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                                StringToFormula StringToFormula = new StringToFormula();
                                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                                                if (blnCheck)
                                                {
                                                    string strForItem = "";
                                                    string strUseItem = "";
                                                    if (ItemsAdding.UseItemForAdd.ToString().Trim() == "")
                                                    {
                                                        Guid FBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                        var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
                                                        if (tblRizMetreUser.Count != 0)
                                                        {
                                                            _context.RizMetreUserses.RemoveRange(tblRizMetreUser);

                                                            ///////////
                                                            //حذف حمل//
                                                            ///////////
                                                            ///
                                                            foreach (var item in tblRizMetreUser)
                                                            {
                                                                DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                                {
                                                                    RIzMetreId=item.ID,
                                                                    BarAvordId = BarAvordId,
                                                                    FBShomareh = varFBUsersAdded.Shomareh,
                                                                    RMShomareh = item.Shomareh,
                                                                    NoeFBId = NoeFB
                                                                };
                                                                await HamlCommon.DeleteHaml(deleteHaml, context);
                                                            }
                                                            /////////////
                                                            //////////////

                                                            _context.SaveChanges();
                                                        }
                                                        //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
                                                    }
                                                    else
                                                    {
                                                        strForItem = ItemsAdding.UseItemForAdd.ToString().Trim();
                                                        strUseItem = strFBShomareh.Trim();
                                                        Guid FBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                        var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim() && x.UseItem == strUseItem).ToList();
                                                        if (tblRizMetreUser.Count != 0)
                                                        {
                                                            _context.RizMetreUserses.RemoveRange(tblRizMetreUser);

                                                            ///////////
                                                            //حذف حمل//
                                                            ///////////
                                                            ///
                                                            foreach (var item in tblRizMetreUser)
                                                            {
                                                                DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                                {
                                                                    RIzMetreId=item.ID,
                                                                    BarAvordId = BarAvordId,
                                                                    FBShomareh = varFBUsersAdded.Shomareh,
                                                                    RMShomareh = item.Shomareh,
                                                                    NoeFBId = NoeFB
                                                                };
                                                                await HamlCommon.DeleteHaml(deleteHaml, context);
                                                            }
                                                            /////////////
                                                            //////////////
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
                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {

                                                    List<clsRizMetreUsers> tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ToList();

                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///
                                                    foreach (var item in tblRizMetreUser)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId=item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = varFBUsersAdded.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////
                                                    ///
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "6":
                                        {

                                            var varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);

                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {

                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///

                                                    List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim() && x.Type == "2" && x.LevelNumber == LevelNumber).ToList();
                                                    foreach (var item in lstRM)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId=item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = Fb.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }


                                                    /////////////
                                                    //////////////
                                                    //////////////

                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim() && x.Type == "2" && x.LevelNumber == LevelNumber).ExecuteDelete();
                                                    //_context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim() && x.Type == "3").ExecuteDelete();


                                                    _context.SaveChanges();
                                                }
                                            }

                                            //bool blnHasHaml = false;
                                            //List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = _context.ItemsRelatedToItemHamls
                                            //    .Where(x => x.ItemFB.Trim() == strFBShomarehAdded.Trim() && x.Year == Year).ToList();

                                            //foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
                                            //{

                                            //    string strItemHamlFB = "";
                                            //    if (itemsRelatedToItemHaml != null)
                                            //    {
                                            //        strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                                            //        blnHasHaml = true;
                                            //    }

                                            //    if (blnHasHaml)
                                            //    {
                                            //        string ItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB;
                                            //        clsFB? FbHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemHamlFB);
                                            //        if (FbHaml != null)
                                            //        {
                                            //            List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == FbHaml.ID && x.ForItem == strFBShomareh.Trim() && x.Type == "3").ToList();

                                            //            _context.RizMetreUserses.RemoveRange(lstRMForDelForHaml);

                                            //            ///////////
                                            //            //حذف حمل//
                                            //            ///////////
                                            //            ///
                                            //            foreach (var item in lstRMForDelForHaml)
                                            //            {
                                            //                DeleteHamlDto deleteHaml = new DeleteHamlDto
                                            //                {
                                            //                    BarAvordId = BarAvordId,
                                            //                    FBShomareh = FbHaml.Shomareh,
                                            //                    RMShomareh = item.Shomareh,
                                            //                    NoeFBId = NoeFB
                                            //                };
                                            //                HamlCommon.DeleteHaml(deleteHaml, context);
                                            //            }
                                            //            /////////////
                                            //            //////////////
                                            //            ///
                                            //        }
                                            //    }
                                            //}

                                            //clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);

                                            ////DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            ////if (DtFBUsersAdded.Rows.Count != 0)
                                            //if (varFBUsersAdded != null)
                                            //{
                                            //    Guid guFBUsersAddedId = varFBUsersAdded.ID;  //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                            //    clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                            //    if (Fb != null)
                                            //    {
                                            //        List<clsRizMetreUsers> lstRMForDel = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim() && x.Type == "2").ToList();
                                            //        if (lstRMForDel.Count != 0)
                                            //        {
                                            //            _context.RizMetreUserses.RemoveRange(lstRMForDel);
                                            //            long ConditionGroupId1 = varItemsHasConditionAddedToFB[Counter].ConditionGroupId;
                                            //            ///
                                            //            ///ConditionGroup=12
                                            //            ///برای پخش، آبپاشی، تسطیح و کوبیدن قشر زیراساس میباشد
                                            //            ///که در این صورت بایستی اضافه بهای شانه سازی چک گردد و در صورت وجود ریز متره
                                            //            ///برای اضافه بها شانه سازی باستی در آنجا نیز حذف گردند
                                            //            ///

                                            //            if (ConditionGroupId1 == 12)
                                            //            {
                                            //                //پیدا کردن ریزمتره های درج شده در اضافه بها شانه سازی
                                            //                RizMetreCommon rizMetreCommon = new RizMetreCommon();
                                            //                long[] lngConditionGroupId = { 15 };

                                            //                GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                                            //                {
                                            //                    ShomarehFB = strFBShomareh,
                                            //                    BarAvordUserId = BarAvordId,
                                            //                    NoeFB = NoeFB,
                                            //                    Year = Year,
                                            //                    LevelNumber = LevelNumber,
                                            //                    ConditionGroupId = lngConditionGroupId
                                            //                };
                                            //                List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);
                                            //                if (lstRM.Count != 0)
                                            //                {
                                            //                    foreach (var RMForDel in lstRM)
                                            //                    {
                                            //                        clsRizMetreUsers? RMDeleted = _context.RizMetreUserses.FirstOrDefault(x => x.ID == RMForDel.Id);
                                            //                        if (RMDeleted != null)
                                            //                        {
                                            //                            _context.RizMetreUserses.Remove(RMDeleted);

                                            //                            ///////////
                                            //                            //حذف حمل//
                                            //                            ///////////
                                            //                            ///
                                            //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
                                            //                            {
                                            //                                BarAvordId = BarAvordId,
                                            //                                FBShomareh = Fb.Shomareh,
                                            //                                RMShomareh = RMDeleted.Shomareh,
                                            //                                NoeFBId = NoeFB
                                            //                            };
                                            //                            HamlCommon.DeleteHaml(deleteHaml, context);
                                            //                            /////////////
                                            //                            //////////////
                                            //                        }
                                            //                    }
                                            //                }
                                            //            }
                                            //        }
                                            //    }
                                            //}
                                            //_context.SaveChanges();

                                            break;
                                        }
                                    case "7":
                                        {
                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {

                                                    List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ToList();
                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///
                                                    foreach (var item in lstRMForDelForHaml)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId=item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = Fb.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////

                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "8":
                                        {
                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {

                                                    List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ToList();

                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///
                                                    foreach (var item in lstRMForDelForHaml)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId = item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = Fb.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "10":
                                        {
                                            strCharacterPlus = ItemsAdding.CharacterPlus == null ? "" : ItemsAdding.CharacterPlus.Trim().ToString();

                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus);
                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {


                                                    List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ToList();

                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///
                                                    foreach (var item in lstRMForDelForHaml)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId = item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = Fb.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////
                                                    ///                  
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();

                                                    _context.SaveChanges();
                                                }
                                            }
                                            break;
                                        }
                                    case "11":
                                        {
                                            string strFBShomareh1 = ItemsAdding.Shomareh;
                                            strCharacterPlus = ItemsAdding.CharacterPlus.ToString();

                                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomareh1 + strCharacterPlus);

                                            //DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                                            //if (DtFBUsersAdded.Rows.Count != 0)
                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID; //Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {

                                                    List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ToList();

                                                    ///////////
                                                    //حذف حمل//
                                                    ///////////
                                                    ///
                                                    foreach (var item in lstRMForDelForHaml)
                                                    {
                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                        {
                                                            RIzMetreId = item.ID,
                                                            BarAvordId = BarAvordId,
                                                            FBShomareh = Fb.Shomareh,
                                                            RMShomareh = item.Shomareh,
                                                            NoeFBId = NoeFB
                                                        };
                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                    }
                                                    /////////////
                                                    //////////////
                                                    ///
                                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strFBShomareh.Trim()).ExecuteDelete();
                                                    await _context.SaveChangesAsync();
                                                }
                                            }
                                            break;
                                        }
                                    case "13":
                                        {
                                            decimal Meghdar2 = varItemsHasConditionAddedToFB[Counter].Meghdar2;
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

                                                        if (varFBUser != null)
                                                        {
                                                            Guid FBId = varFBUser.ID;

                                                            string strShomareh1 = strFBShomareh;
                                                            //Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                                                            List<clsRizMetreUsers> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                                            if (RizMetre != null)
                                                            {
                                                                if (RizMetre.Count != 0)
                                                                {
                                                                    _context.RizMetreUserses.RemoveRange(RizMetre);


                                                                    ///////////
                                                                    //حذف حمل//
                                                                    ///////////
                                                                    ///
                                                                    foreach (var item in RizMetre)
                                                                    {
                                                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                                        {
                                                                            RIzMetreId = item.ID,
                                                                            BarAvordId = BarAvordId,
                                                                            FBShomareh = varFBUser.Shomareh,
                                                                            RMShomareh = item.Shomareh,
                                                                            NoeFBId = NoeFB
                                                                        };
                                                                        await HamlCommon.DeleteHaml(deleteHaml, context);
                                                                    }
                                                                    /////////////
                                                                    //////////////
                                                                    ///

                                                                    _context.SaveChanges();
                                                                }
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
                                            clsBaravordUser? varBA = _context.BaravordUsers.FirstOrDefault(x => x.ID == BarAvordId);

                                            if (varBA != null)
                                            {
                                                Guid guBAId = varBA.ID;

                                                string strCondition = ItemsAdding.Condition.ToString().Trim() != null ? ItemsAdding.Condition.ToString().Trim() : "";
                                                string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                                                StringToFormula StringToFormula = new StringToFormula();
                                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                                                if (blnCheck)
                                                {
                                                    string strAddedItems = ItemsAdding.AddedItems.ToString().Trim();
                                                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                                                    Guid intFBId = new Guid();
                                                    if (varFBUser != null)
                                                    {
                                                        intFBId = varFBUser.ID;

                                                        string strShomareh1 = strFBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                                                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();

                                                        _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);

                                                        ///////////
                                                        //حذف حمل//
                                                        ///////////
                                                        ///
                                                        foreach (var item in lstRizMetreUsersCurrent)
                                                        {
                                                            DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                            {
                                                                RIzMetreId = item.ID,
                                                                BarAvordId = BarAvordId,
                                                                FBShomareh = varFBUser.Shomareh,
                                                                RMShomareh = item.Shomareh,
                                                                NoeFBId = NoeFB
                                                            };
                                                            await HamlCommon.DeleteHaml(deleteHaml, context);
                                                        }
                                                        /////////////
                                                        //////////////
                                                        _context.SaveChanges();
                                                    }
                                                }


                                            }
                                            break;
                                        }
                                    case "15":
                                        {
                                            /////////////
                                            /////حذف قبلی ها
                                            /////////////

                                            strCharacterPlus = ItemsAdding.CharacterPlus.ToString();

                                            string strCondition = ItemsAdding.Condition.ToString().Trim();
                                            string[] strConditionSplit = strCondition.Split("_");

                                            string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                            DataTable DtRizMetreUserses = new DataTable();
                                            string strForItem = "";
                                            string strUseItem = "";
                                            string strItemFBShomareh = ItemsAdding.AddedItems.ToString().Trim();

                                            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh);

                                            Guid guFBId = Guid.Parse(varFBUser.ID.ToString());
                                            strUseItem = strItemShomareh;// varFBUser.Shomareh.Trim();
                                            strForItem = ItemsAdding.UseItemForAdd == null ? "" : ItemsAdding.UseItemForAdd.ToString().Trim();


                                            string strItemShomareh1 = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus;

                                            clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh1).FirstOrDefault();




                                            Guid intFBId1 = new Guid();
                                            if (FBUser != null)
                                            {
                                                intFBId1 = FBUser.ID;

                                                string strShomareh1 = strItemFBShomareh.Substring(0, 6);
                                                List<clsRizMetreUsers> lstRizMetreUsersBase =
                                                                _context.RizMetreUserses.Where(x => x.FBId == guFBId).ToList();

                                                List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                                                                _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2").ToList();


                                                List<Guid> lstRMIds = lstRizMetreUsersBase.Select(x => x.ID).ToList();
                                                List<clsRizMetreUsersHistory> lstRMHistories = _context.RizMetreUsersHistories.Where(x => lstRMIds.Contains(x.RizMetreUsersId)).ToList();
                                                foreach (var RMCurrent in lstRizMetreUsersBase)
                                                {
                                                    clsRizMetreUsersHistory? RMHistory = lstRMHistories.FirstOrDefault(x => x.RizMetreUsersId == RMCurrent.ID);
                                                    if (RMHistory != null)
                                                    {
                                                        RMCurrent.Sharh = RMHistory.Sharh;
                                                        RMCurrent.Tedad = RMHistory.Tedad;
                                                        RMCurrent.Tool = RMHistory.Tool;
                                                        RMCurrent.Arz = RMHistory.Arz;
                                                        RMCurrent.Ertefa = RMHistory.Ertefa;
                                                        RMCurrent.Vazn = RMHistory.Vazn;
                                                        RMCurrent.MeghdarJoz = RMHistory.MeghdarJoz;
                                                        RMCurrent.Des = RMHistory.Des;
                                                    }
                                                }

                                                _context.RizMetreUsersHistories.RemoveRange(lstRMHistories);
                                                _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                foreach (var item in lstRizMetreUsersCurrent)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        RIzMetreId = item.ID,
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = FBUser.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFB
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);

                                                }
                                                /////////////
                                                //////////////
                                            }
                                            await _context.SaveChangesAsync();
                                            break;
                                        }

                                    case "17":
                                        {
                                            strCharacterPlus = ItemsAdding.CharacterPlus != null ? ItemsAdding.CharacterPlus.ToString().Trim() : "";

                                            var varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus);

                                            if (varFBUsersAdded != null)
                                            {
                                                Guid guFBUsersAddedId = varFBUsersAdded.ID;
                                                List<clsRizMetreUsers> lstRizMetres = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strFBShomareh.Trim()
                                                    && x.LevelNumber == LevelNumber).ToList();

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
                                                        NoeFBId = NoeFB
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////

                                            }
                                            await _context.SaveChangesAsync();

                                            break;
                                        }
                                    case "19":
                                        {
                                            /////////////
                                            /////حذف قبلی ها
                                            /////////////

                                            strCharacterPlus = ItemsAdding.CharacterPlus.ToString();

                                            string strCondition = ItemsAdding.Condition.ToString().Trim();
                                            string[] strConditionSplit = strCondition.Split("_");

                                            string strFinalWorking = ItemsAdding.FinalWorking.ToString();
                                            DataTable DtRizMetreUserses = new DataTable();
                                            string strForItem = "";
                                            string strUseItem = "";
                                            string strItemFBShomareh = ItemsAdding.AddedItems.ToString().Trim();

                                            //clsBaravordUser? varBA = _context.BaravordUsers.FirstOrDefault(x => x.ID == BarAvordId);
                                            //Guid guBAId = varBA.ID;
                                            string strItemShomareh = ItemsAdding.AddedItems.ToString().Trim();
                                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh);

                                            Guid guFBId = Guid.Parse(varFBUser.ID.ToString());
                                            strUseItem = strItemShomareh;// varFBUser.Shomareh.Trim();
                                            strForItem = ItemsAdding.UseItemForAdd == null ? "" : ItemsAdding.UseItemForAdd.ToString().Trim();

                                            //List<clsRizMetreUsers> varRizMetreUsersesCurrent = _context.RizMetreUserses
                                            //    .Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();


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
                                                                                                                }).Where(x => x.FBId == guFBId).OrderBy(x => x.Shomareh).ToList();

                                            string strItemShomareh1 = ItemsAdding.AddedItems.ToString().Trim() + strCharacterPlus;

                                            clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh1).FirstOrDefault();

                                            Guid intFBId1 = new Guid();
                                            if (FBUser != null)
                                            {
                                                intFBId1 = FBUser.ID;

                                                string strShomareh1 = strItemFBShomareh.Substring(0, 6);

                                                List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                                                                _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2").ToList();

                                                _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);

                                                ///////////
                                                //حذف حمل//
                                                ///////////
                                                ///
                                                foreach (var item in lstRizMetreUsersCurrent)
                                                {
                                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
                                                    {
                                                        RIzMetreId = item.ID,
                                                        BarAvordId = BarAvordId,
                                                        FBShomareh = FBUser.Shomareh,
                                                        RMShomareh = item.Shomareh,
                                                        NoeFBId = NoeFB
                                                    };
                                                    await HamlCommon.DeleteHaml(deleteHaml, context);
                                                }
                                                /////////////
                                                //////////////
                                            }

                                            _context.SaveChanges();

                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    long strItemsHasCondition_ConditionContextId = varItemsHasConditionAddedToFB.First().ItemsHasCondition_ConditionContextId; //long.Parse(DtItemsHasConditionAddedToFB.Rows[0]["ItemsHasCondition_ConditionContextId"].ToString().Trim());

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

                    //for (int i = 0; i < DtOperation_ItemsFB.Rows.Count; i++)
                    //{
                    //    DeleteItemsHasConditionAddedToFBAndRizMetreSubCondition(BarAvordId, DtOperation_ItemsFB.Rows[i]["ItemShomareh"].ToString().Trim(), long.Parse(DtOperation_ItemsFB.Rows[i]["ID"].ToString()));
                    //}

                }
            }

            await _context.SaveChangesAsync();
            return new JsonResult("OK");
            //}
            //catch (Exception)
            //{
            //    return Json("NOK", JsonRequestBehavior.AllowGet);
            //}
        }

        //public bool DeleteItemsHasConditionAddedToFBAndRizMetreSubCondition(Guid BarAvordId, string strFBShomareh, long Id)
        //{
        //    try
        //    {
        //        var varItemsHasConditionAddedToFB = (from ItemsHasConditionAddedToFB in _context.ItemsHasConditionAddedToFBs
        //                                             join ItemsHasCondition_ConditionContext in _context.ItemsHasCondition_ConditionContexts
        //                                             on ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals ItemsHasCondition_ConditionContext.Id
        //                                             join ItemsHasCondition in _context.ItemsHasConditions on ItemsHasCondition_ConditionContext.ItemsHasConditionId equals ItemsHasCondition.Id
        //                                             select new
        //                                             {
        //                                                 ID = ItemsHasConditionAddedToFB.ID,
        //                                                 FBShomareh = ItemsHasConditionAddedToFB.FBShomareh,
        //                                                 BarAvordId = ItemsHasConditionAddedToFB.BarAvordId,
        //                                                 Meghdar = ItemsHasConditionAddedToFB.Meghdar,
        //                                                 ItemsHasCondition_ConditionContextId = ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
        //                                                 ConditionGroupId = ItemsHasConditionAddedToFB.ConditionGroupId,
        //                                                 ItemShomareh = ItemsHasCondition.ItemFBShomareh
        //                                             }).Where(x => x.BarAvordId == BarAvordId && x.FBShomareh == strFBShomareh && x.ItemsHasCondition_ConditionContextId == Id).ToList();
        //        DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
        //        //DataTable DtItemsHasConditionAddedToFB = clsItemsHasConditionAddedToFB.ListWithParameterSimple("BarAvordId=" + intBarAvordId + " and FBShomareh='" + strFBShomareh + "' and ItemsHasCondition_ConditionContextId=" + Id);

        //        if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
        //        {
        //            //string strItemsHasCondition_ConditionContext = "";
        //            //if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
        //            //{
        //            //    strItemsHasCondition_ConditionContext += "ItemsHasCondition_ConditionContextId in(";
        //            //    for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
        //            //    {
        //            //        if ((i + 1) < DtItemsHasConditionAddedToFB.Rows.Count)
        //            //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString() + ",";
        //            //        else
        //            //            strItemsHasCondition_ConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString();
        //            //    }
        //            //    strItemsHasCondition_ConditionContext += ")";
        //            //}
        //            long[] strItemsHasCondition_ConditionContext = new long[] { };
        //            if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
        //            {
        //                for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
        //                {
        //                    strItemsHasCondition_ConditionContext[i] = long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());
        //                }
        //            }

        //            bool blnChekIsDel = false;
        //            try
        //            {
        //                clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = _context.ItemsHasConditionAddedToFBs.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh == strFBShomareh.Trim()).FirstOrDefault();
        //                if (ItemsHasConditionAddedToFB != null)
        //                {
        //                    _context.ItemsHasConditionAddedToFBs.Remove(ItemsHasConditionAddedToFB);
        //                    _context.SaveChanges();
        //                }
        //                blnChekIsDel = true;
        //            }
        //            catch (Exception)
        //            {
        //                blnChekIsDel = false;
        //                //throw;
        //            }


        //            //if (clsItemsHasConditionAddedToFB.Delete("BarAvordId=" + intBarAvordId + " and FBShomareh='" + strFBShomareh + "'"))
        //            if (blnChekIsDel)
        //            {

        //                List<clsItemsAddingToFB> lstItemsAddingToFB = _context.ItemsAddingToFBs
        //                .Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();


        //                List<ItemsAddingToFBForShowAddItemsDto> lstItemsAddingToFBForShowAddItems = new List<ItemsAddingToFBForShowAddItemsDto>();

        //                foreach (var ItemsAddingToFB in lstItemsAddingToFB)
        //                {
        //                    string[] currentItemsAdding = ItemsAddingToFB.AddedItems.Split('_');

        //                    foreach (var ItemsAdding in currentItemsAdding)
        //                    {
        //                        //clsFehrestBaha? FehrestBaha = _context.FehrestBahas.FirstOrDefault(x => x.Sal == Year && x.NoeFB == NoeFB && x.Shomareh.Trim() == ItemsAdding.Trim());

        //                        //if (FehrestBaha != null)
        //                        //{
        //                        ItemsAddingToFBForShowAddItemsDto ItemsAddingToFBForShowAddItems = new ItemsAddingToFBForShowAddItemsDto
        //                        {
        //                            Id = ItemsAddingToFB.Id,
        //                            ItemsHasCondition_ConditionContextId = ItemsAddingToFB.ItemsHasCondition_ConditionContextId,
        //                            AddedItems = ItemsAddingToFB.AddedItems,
        //                            Condition = ItemsAddingToFB.Condition,
        //                            FinalWorking = ItemsAddingToFB.FinalWorking,
        //                            ConditionType = ItemsAddingToFB.ConditionType,
        //                            DesOfAddingItems = ItemsAddingToFB.DesOfAddingItems,
        //                            UseItemForAdd = ItemsAddingToFB.UseItemForAdd,
        //                            FieldsAdding = ItemsAddingToFB.FieldsAdding,
        //                            CharacterPlus = ItemsAddingToFB.CharacterPlus,
        //                            //Shomareh = FehrestBaha.Shomareh,
        //                            //Sharh = FehrestBaha.Sharh
        //                        };

        //                        lstItemsAddingToFBForShowAddItems.Add(ItemsAddingToFBForShowAddItems);
        //                        //}
        //                    }

        //                }



        //                var varItemsAddingToFB = _context.ItemsAddingToFBs.Where(x => strItemsHasCondition_ConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
        //                DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);
        //                //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasCondition_ConditionContext);
        //                for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
        //                {
        //                    decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
        //                    string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();

        //                    List<ItemsAddingToFBForShowAddItemsDto> ItemsAddingToFB =
        //                            lstItemsAddingToFBForShowAddItems.Where(x => x.ItemsHasCondition_ConditionContextId == long.Parse(RBCode)).ToList();
        //                    //if (Dr.Length != 0)
        //                    if (ItemsAddingToFB != null)
        //                    {
        //                        //DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
        //                        //if (Dr.Length != 0)
        //                        //{
        //                        //for (int idr = 0; idr < Dr.Length; idr++)
        //                        foreach (var ItemsAdding in ItemsAddingToFB)
        //                        {
        //                            string strFBShomarehAdded = ItemsAdding.AddedItems.ToString().Trim();
        //                            switch (ItemsAdding.ConditionType.ToString())
        //                            {
        //                                case "1":
        //                                    {
        //                                        string strCharacterPlus = ItemsAdding.CharacterPlus.ToString().Trim();
        //                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
        //                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
        //                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "A'");
        //                                        if (DtFBUsersAdded.Rows.Count != 0)
        //                                        {
        //                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
        //                                            if (tblRizMetreUser.Count != 0)
        //                                            {
        //                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                _context.SaveChanges();
        //                                            }
        //                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
        //                                        }
        //                                        break;
        //                                    }
        //                                case "2":
        //                                    {
        //                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
        //                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
        //                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
        //                                        if (DtFBUsersAdded.Rows.Count != 0)
        //                                        {
        //                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
        //                                            if (tblRizMetreUser.Count != 0)
        //                                            {
        //                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                _context.SaveChanges();
        //                                            }
        //                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
        //                                        }
        //                                        break;
        //                                    }
        //                                case "3":
        //                                    {
        //                                        decimal dPercent = decimal.Parse(ItemsAdding.FinalWorking.ToString());
        //                                        string strStatus = dPercent > 0 ? "B" : "e";
        //                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strStatus).ToList();
        //                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
        //                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + strStatus + "'");
        //                                        if (DtFBUsersAdded.Rows.Count != 0)
        //                                        {
        //                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
        //                                            if (tblRizMetreUser.Count != 0)
        //                                            {
        //                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                _context.SaveChanges();
        //                                            }
        //                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
        //                                        }
        //                                        break;
        //                                    }
        //                                case "4":
        //                                    {
        //                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
        //                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
        //                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
        //                                        if (DtFBUsersAdded.Rows.Count != 0)
        //                                        {
        //                                            string strCondition = ItemsAdding.Condition.ToString().Trim();
        //                                            string strFinalWorking = ItemsAdding.FinalWorking.ToString();
        //                                            string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
        //                                            StringToFormula StringToFormula = new StringToFormula();
        //                                            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                                            if (blnCheck)
        //                                            {
        //                                                string strForItem = "";
        //                                                string strUseItem = "";
        //                                                if (ItemsAdding.UseItemForAdd.ToString().Trim() == "")
        //                                                {
        //                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
        //                                                    if (tblRizMetreUser.Count != 0)
        //                                                    {
        //                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                        _context.SaveChanges();
        //                                                    }
        //                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
        //                                                }
        //                                                else
        //                                                {
        //                                                    strForItem = ItemsAdding.UseItemForAdd.ToString().Trim();
        //                                                    strUseItem = strFBShomareh.Trim();
        //                                                    Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                                    var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strForItem && x.UseItem == strUseItem.Trim()).ToList();
        //                                                    if (tblRizMetreUser.Count != 0)
        //                                                    {
        //                                                        _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                        _context.SaveChanges();
        //                                                    }
        //                                                    //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strForItem + "' and UseItem='" + strUseItem + "'");
        //                                                }
        //                                            }
        //                                        }
        //                                        break;
        //                                    }
        //                                case "5":
        //                                    {
        //                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
        //                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
        //                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
        //                                        if (DtFBUsersAdded.Rows.Count != 0)
        //                                        {
        //                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
        //                                            if (tblRizMetreUser.Count != 0)
        //                                            {
        //                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                _context.SaveChanges();
        //                                            }
        //                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
        //                                        }
        //                                        break;
        //                                    }
        //                                case "6":
        //                                    {
        //                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
        //                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
        //                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
        //                                        if (DtFBUsersAdded.Rows.Count != 0)
        //                                        {
        //                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
        //                                            if (tblRizMetreUser.Count != 0)
        //                                            {
        //                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                _context.SaveChanges();
        //                                            }
        //                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
        //                                        }
        //                                        break;
        //                                    }
        //                                case "7":
        //                                    {
        //                                        var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
        //                                        DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
        //                                        //DataTable DtFBUsersAdded = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
        //                                        if (DtFBUsersAdded.Rows.Count != 0)
        //                                        {
        //                                            Guid FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
        //                                            var tblRizMetreUser = _context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == strFBShomareh.Trim()).ToList();
        //                                            if (tblRizMetreUser.Count != 0)
        //                                            {
        //                                                _context.RizMetreUserses.RemoveRange(tblRizMetreUser);
        //                                                _context.SaveChanges();
        //                                            }
        //                                            //clsRizMetreUserses.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "'");
        //                                        }
        //                                        break;
        //                                    }
        //                                default:
        //                                    break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}
