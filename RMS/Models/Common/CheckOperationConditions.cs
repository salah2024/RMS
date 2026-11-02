using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common.Dto;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using Stimulsoft.Report.Components;
using System.Data;
using System.Web.Mvc;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common
{
    public class CheckOperationConditions
    {
        public async Task<bool> fnCheckOperationCondition(ApplicationDbContext _context, ItemsAddingToFBForCheckOperationDto itemsAddingToFBForCheckOperation,
            List<ItemsFieldsDto> ItemsFields, ItemsHasConditionConditionContextForCheckOperationDto ItemHasCon, Guid FBId,
            clsRizMetreUsers RM, int LevelNumber, int Year, NoeFehrestBaha NoeFBId,Guid? SingleRMIdForAdd)
        {
            List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year).ToList();

            bool blnCheckAgain = true;
            string Condition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition.Trim() : "";
            string FinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking.Trim() : "";
            string AddedItems = itemsAddingToFBForCheckOperation.AddedItems != null ? itemsAddingToFBForCheckOperation.AddedItems.Substring(0, 6).Trim() : "";
            string DesOfAddingItems = itemsAddingToFBForCheckOperation.DesOfAddingItems != null ? itemsAddingToFBForCheckOperation.DesOfAddingItems.Trim() : "";
            string UseItemForAdd = itemsAddingToFBForCheckOperation.UseItemForAdd != null ? itemsAddingToFBForCheckOperation.UseItemForAdd.Trim() : "";
            DateTime Now = DateTime.Now;

            string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";
            Guid BarAvordId = ItemHasCon.BarAvordId;
            long? ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
            string? ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
            long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
            long ConditionGroupId = ItemHasCon.ConditionGroupId;
            string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();
            string? FieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding;

            string strWarning = "شرایط لازم برای اعمال این آیتم برقرار نیست، لطفا اطلاعات ورودی را بررسی نمایید";
            bool Status = false;
            bool blnCheckIsExistWarning = true;

            List<bool> blnResult1 = new List<bool>();
            bool blnResult = false;

            string[] FieldsAddingSplit = itemsAddingToFBForCheckOperation.FieldsAdding == null ? "".Split(",") : itemsAddingToFBForCheckOperation.FieldsAdding.Split(",");

            decimal Meghdar = ItemHasCon.Meghdar;
            decimal Meghdar2 = ItemHasCon.Meghdar2;

            string strCurrentFBShomareh = ItemHasCon.FBShomareh;

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

            Guid guBAId = ItemHasCon.BarAvordId;
            switch (itemsAddingToFBForCheckOperation.ConditionType)
            {
                case 1:
                    {
                        //decimal dSumAllRizMetre = 0;
                        //var matchCondition = Regex.Match(FinalWorking, @"\b([a-zA-Z])\b");
                        //string variableNameCondition = matchCondition.Success ? matchCondition.Groups[1].Value : null;
                        //string strConditionOp = "";

                        decimal FinalMeghdar = (Meghdar == 0 ? 1 : Meghdar) * (Meghdar2 == 0 ? 1 : Meghdar2);


                         ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            blnCheckAgain = blnCheckAgain,
                            RMIdForEdit = SingleRMIdForAdd
                        };

                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        //bool blnResult = await CCC.Case1(RequestCase1, _context);
                        ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case1(RequestCase1, _context);

                        blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                        strWarning = ResultCCC.strWarning;
                        Status = ResultCCC.blnStatus;

                        blnResult1.Add(ResultCCC.blnStatus);
                        blnResult = ResultCCC.blnStatus;

                        break;
                    }
                case 2:
                    {
                        ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            blnCheckAgain = blnCheckAgain,
                            RMIdForEdit = SingleRMIdForAdd
                        };

                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case2(RequestCase1, _context);

                        blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                        strWarning = ResultCCC.strWarning;
                        Status = ResultCCC.blnStatus;

                        blnResult1.Add(ResultCCC.blnStatus);
                        blnResult = ResultCCC.blnStatus;

                        break;
                    }
                case 3:
                    {
                        string FinalWorking1 = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "0";
                        decimal dPercent = decimal.Parse(FinalWorking1);
                        string strAddedItems = AddedItems;
                        string strStatus = dPercent > 0 ? "B" : "e";
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strStatus && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);
                        //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + strStatus + "'");
                        Guid intFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB Fb = new clsFB();
                            Fb.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            Fb.Shomareh = AddedItems + strStatus;// Dr[0]["AddedItems"].ToString().Trim() + strStatus;
                            Fb.BahayeVahedZarib = dPercent;
                            Fb.NoeFBId = NoeFBId;
                            _context.FBs.Add(Fb);
                            _context.SaveChanges();
                            intFBId = Fb.ID;
                            //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[0]["AddedItems"].ToString().Trim() + strStatus, dPercent);
                        }
                        else
                            intFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);// DtFB.Rows[0]["Shomareh"].ToString().Trim();
                        var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
                                                       join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                       where RizMetreUsers.LevelNumber == LevelNumber
                                                       select new
                                                       {
                                                           RizMetreUsers.ID,
                                                           RizMetreUsers.Shomareh,
                                                           RizMetreUsers.Tedad,
                                                           RizMetreUsers.Tool,
                                                           RizMetreUsers.Arz,
                                                           RizMetreUsers.Ertefa,
                                                           RizMetreUsers.Vazn,
                                                           RizMetreUsers.Sharh,
                                                           RizMetreUsers.Des,
                                                           RizMetreUsers.FBId,
                                                           RizMetreUsers.OperationsOfHamlId,
                                                           RizMetreUsers.ForItem,
                                                           RizMetreUsers.Type,
                                                           RizMetreUsers.UseItem,
                                                           FB.BarAvordId
                                                       }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").OrderBy(x => x.Shomareh).ToList();
                        DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);


                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + RM.Shomareh);
                        if (DrRizMetreUsersCurrent.Length == 0)
                        {
                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                            RizMetreUsers.Shomareh = RM.Shomareh;
                            ShomareNew++;
                            RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                            RizMetreUsers.Sharh = RM.Sharh;

                            if (ItemsFields[0].IsEnteringValue == true)
                                RizMetreUsers.Tedad = RM.Tedad;

                            if (ItemsFields[1].IsEnteringValue == true)
                                RizMetreUsers.Tool = RM.Tool;

                            if (ItemsFields[2].IsEnteringValue == true)
                                RizMetreUsers.Arz = RM.Arz;

                            if (ItemsFields[3].IsEnteringValue == true)
                                RizMetreUsers.Ertefa = RM.Ertefa;

                            if (ItemsFields[0].IsEnteringValue == true)
                                RizMetreUsers.Vazn = RM.Vazn;

                            RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh;
                            RizMetreUsers.FBId = intFBId;
                            RizMetreUsers.OperationsOfHamlId = 1;
                            RizMetreUsers.Type = "2";
                            RizMetreUsers.ForItem = ItemHasCon.FBShomareh;
                            RizMetreUsers.UseItem = "";
                            RizMetreUsers.LevelNumber = LevelNumber;
                            RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                            RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                            RizMetreUsers.InsertDateTime = Now;


                            decimal dMeghdarJoz = 0;
                            if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                                dMeghdarJoz = 0;
                            else
                                dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                                (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                                * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                            _context.RizMetreUserses.Add(RizMetreUsers);


                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///

                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                            {
                                BarAvordUserId = ItemHasCon.BarAvordId,
                                Year = Year,
                                ItemFBShomareh = strAddedItems,
                                NoeFBId = NoeFBId,
                                //BarAvordHamlId = gBarAvordHamlId,
                                Shomareh = RizMetreUsers.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                LevelNumber = 1,
                            };
                            await HamlCommon.SaveHaml(requestSaveHaml, _context);

                            _context.SaveChanges();

                        }
                        break;
                    }
                case 4:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            //List<RizMetreUsersForItemsAddingToFBInputDto> lstRizMetreUserses = new List<RizMetreUsersForItemsAddingToFBInputDto>();
                            //strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                            //decimal dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                            string strShomarehAdd = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd && x.NoeFBId == NoeFBId);
                            //DtFBUser = clsConvert.ToDataTable(varFBUser);
                            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
                            Guid intFBId = new Guid();
                            //if (DtFBUser.Rows.Count == 0)
                            if (varFBUser == null)
                            {
                                clsFB Fb = new clsFB();
                                Fb.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                Fb.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                                Fb.BahayeVahedZarib = 0;
                                Fb.NoeFBId = NoeFBId;
                                _context.FBs.Add(Fb);
                                _context.SaveChanges();
                                intFBId = Fb.ID;
                                //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
                            }
                            else
                                intFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                            DataTable DtRizMetreUsers = new DataTable();
                            string strForItem = ItemHasCon.FBShomareh;
                            string strUseItem = "";
                            //string strItemFBShomareh = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();


                            var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
                                                           join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                           where RizMetreUsers.LevelNumber == LevelNumber
                                                           select new
                                                           {
                                                               ID = RizMetreUsers.ID,
                                                               Shomareh = RizMetreUsers.Shomareh,
                                                               Tedad = RizMetreUsers.Tedad,
                                                               Tool = RizMetreUsers.Tool,
                                                               Arz = RizMetreUsers.Arz,
                                                               Ertefa = RizMetreUsers.Ertefa,
                                                               Vazn = RizMetreUsers.Vazn,
                                                               Des = RizMetreUsers.Des,
                                                               FBId = RizMetreUsers.FBId,
                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
                                                               ForItem = RizMetreUsers.ForItem,
                                                               Type = RizMetreUsers.Type,
                                                               UseItem = RizMetreUsers.UseItem,
                                                               BaravordUserId = FB.BarAvordId
                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strForItem && x.UseItem == strUseItem && x.Type == "2").ToList();
                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);


                            DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + RM.Shomareh);
                            if (DrRizMetreUsersCurrent.Length == 0)
                            {
                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();

                                RizMetreUsers.ID = Guid.NewGuid();
                                RizMetreUsers.Shomareh = RM.Shomareh;
                                ShomareNew++;
                                RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                RizMetreUsers.Sharh = RM.Sharh; //" آیتم " + AddedItems;

                                if (ItemsFields[0].IsEnteringValue == true)
                                    RizMetreUsers.Tedad = RM.Tedad;// (RM.Tedad == 0 ? 1 : RM.Tedad) * dMultiple;

                                if (ItemsFields[1].IsEnteringValue == true)
                                    RizMetreUsers.Tool = RM.Tool;

                                if (ItemsFields[2].IsEnteringValue == true)
                                    RizMetreUsers.Arz = RM.Arz;

                                if (ItemsFields[3].IsEnteringValue == true)
                                    RizMetreUsers.Ertefa = RM.Ertefa;

                                if (ItemsFields[4].IsEnteringValue == true)
                                    RizMetreUsers.Vazn = RM.Vazn;

                                RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + strItemFBShomareh;
                                RizMetreUsers.FBId = intFBId;
                                RizMetreUsers.OperationsOfHamlId = 1;
                                RizMetreUsers.Type = "2";
                                RizMetreUsers.ForItem = strForItem;
                                RizMetreUsers.UseItem = strUseItem;
                                RizMetreUsers.LevelNumber = LevelNumber;
                                RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                RizMetreUsers.InsertDateTime = Now;


                                decimal dMeghdarJoz = 0;
                                if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                                    dMeghdarJoz = 0;
                                else
                                    dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                                    (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                                    * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);
                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                _context.RizMetreUserses.Add(RizMetreUsers);


                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///


                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = strShomarehAdd,
                                    NoeFBId = NoeFBId,
                                    //BarAvordHamlId = gBarAvordHamlId,
                                    Shomareh = RizMetreUsers.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);
                                _context.SaveChanges();
                                //RizMetreUsers.Save();
                            }

                        }
                        break;
                    }
                case 5:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        string strFinalWorking = FinalWorking;
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                            decimal? dMultiple = null;
                            if (strFinalWorking != "")
                            {
                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                            }

                            string strAddedItems = AddedItems;

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFBId && x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems && x.NoeFBId == NoeFBId);
                            Guid intFBId = new Guid();
                            if (varFBUser == null)
                            {
                                clsFB FB = new clsFB();
                                FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                FB.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                                FB.BahayeVahedZarib = 0;
                                FB.NoeFBId = NoeFBId;
                                _context.FBs.Add(FB);
                                _context.SaveChanges();
                                intFBId = FB.ID;
                            }
                            else
                                intFBId = varFBUser.ID;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                            string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();
                            var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
                                                           join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                           where RizMetreUsers.LevelNumber == LevelNumber
                                                           select new
                                                           {
                                                               ID = RizMetreUsers.ID,
                                                               Shomareh = RizMetreUsers.Shomareh,
                                                               Tedad = RizMetreUsers.Tedad,
                                                               Tool = RizMetreUsers.Tool,
                                                               Arz = RizMetreUsers.Arz,
                                                               Ertefa = RizMetreUsers.Ertefa,
                                                               Vazn = RizMetreUsers.Vazn,
                                                               Des = RizMetreUsers.Des,
                                                               FBId = RizMetreUsers.FBId,
                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
                                                               ForItem = RizMetreUsers.ForItem,
                                                               Type = RizMetreUsers.Type,
                                                               UseItem = RizMetreUsers.UseItem,
                                                               BaravordUserId = FB.BarAvordId
                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

                            DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + RM.Shomareh);
                            if (DrRizMetreUsersCurrent.Length == 0)
                            {
                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                RizMetreUsers.Shomareh = RM.Shomareh;
                                ShomareNew++;
                                RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                RizMetreUsers.Sharh = RM.Sharh;

                                if (lstCurrentItemsFields[0] == true)
                                    RizMetreUsers.Tedad = RM.Tedad;

                                if (lstCurrentItemsFields[1] == true)
                                    RizMetreUsers.Tool = RM.Tool;

                                if (lstCurrentItemsFields[2] == true)
                                    RizMetreUsers.Arz = RM.Arz;

                                if (lstCurrentItemsFields[3] == true)
                                    RizMetreUsers.Ertefa = RM.Ertefa;

                                if (lstCurrentItemsFields[4] == true)
                                    RizMetreUsers.Vazn = dMultiple;

                                RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh;
                                RizMetreUsers.FBId = intFBId;
                                RizMetreUsers.OperationsOfHamlId = 1;
                                RizMetreUsers.Type = "2";
                                RizMetreUsers.ForItem = ItemHasCon.FBShomareh;
                                RizMetreUsers.UseItem = "";
                                RizMetreUsers.LevelNumber = LevelNumber;
                                RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                RizMetreUsers.InsertDateTime = Now;

                                decimal dMeghdarJoz = 0;
                                if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                                    dMeghdarJoz = 0;
                                else
                                    dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                                    (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                                    * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                _context.RizMetreUserses.Add(RizMetreUsers);


                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///

                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = strAddedItems,
                                    NoeFBId = NoeFBId,
                                    //BarAvordHamlId = gBarAvordHamlId,
                                    Shomareh = RizMetreUsers.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                _context.SaveChanges();
                                //RizMetreUsers.Save();
                            }
                        }
                        break;
                    }
                case 7:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strConditionOp = strCondition.Replace("x", RM.Arz.ToString().Trim());
                        string strFinalWorking = FinalWorking;
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            string strAddedItems = AddedItems;

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFBId && x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems && x.NoeFBId == NoeFBId);
                            Guid intFBId = new Guid();
                            if (varFBUser == null)
                            {
                                clsFB FB = new clsFB();
                                FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                FB.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                                FB.BahayeVahedZarib = 0;
                                FB.NoeFBId = NoeFBId;
                                _context.FBs.Add(FB);
                                _context.SaveChanges();
                                intFBId = FB.ID;
                            }
                            else
                                intFBId = varFBUser.ID;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                            string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();
                            var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
                                                           join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                           where RizMetreUsers.LevelNumber == LevelNumber
                                                           select new
                                                           {
                                                               ID = RizMetreUsers.ID,
                                                               Shomareh = RizMetreUsers.Shomareh,
                                                               Tedad = RizMetreUsers.Tedad,
                                                               Tool = RizMetreUsers.Tool,
                                                               Arz = RizMetreUsers.Arz,
                                                               Ertefa = RizMetreUsers.Ertefa,
                                                               Vazn = RizMetreUsers.Vazn,
                                                               Des = RizMetreUsers.Des,
                                                               FBId = RizMetreUsers.FBId,
                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
                                                               ForItem = RizMetreUsers.ForItem,
                                                               Type = RizMetreUsers.Type,
                                                               UseItem = RizMetreUsers.UseItem,
                                                               BaravordUserId = FB.BarAvordId
                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

                            DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + RM.Shomareh);
                            if (DrRizMetreUsersCurrent.Length == 0)
                            {
                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                RizMetreUsers.Shomareh = RM.Shomareh;
                                ShomareNew++;
                                RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                RizMetreUsers.Sharh = RM.Sharh;

                                if (lstCurrentItemsFields[0] == true)
                                    RizMetreUsers.Tedad = RM.Tedad;

                                if (lstCurrentItemsFields[1] == true)
                                    RizMetreUsers.Tool = RM.Tool;

                                if (lstCurrentItemsFields[2] == true)
                                    RizMetreUsers.Arz = RM.Arz;

                                if (lstCurrentItemsFields[3] == true)
                                    RizMetreUsers.Ertefa = RM.Ertefa;

                                //if (lstCurrentItemsFields[4] == true)
                                //    RizMetreUsers.Vazn = dMultiple;

                                RizMetreUsers.Des = RM.Des;
                                RizMetreUsers.FBId = intFBId;
                                RizMetreUsers.OperationsOfHamlId = 1;
                                RizMetreUsers.Type = "2";
                                RizMetreUsers.ForItem = ItemHasCon.FBShomareh;
                                RizMetreUsers.UseItem = "";
                                RizMetreUsers.LevelNumber = LevelNumber;
                                RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                RizMetreUsers.InsertDateTime = Now;

                                decimal dMeghdarJoz = 0;
                                if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                                    dMeghdarJoz = 0;
                                else
                                    dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                                    (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                                    * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                _context.RizMetreUserses.Add(RizMetreUsers);


                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///

                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = strAddedItems,
                                    NoeFBId = NoeFBId,
                                    //BarAvordHamlId = gBarAvordHamlId,
                                    Shomareh = RizMetreUsers.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);


                                _context.SaveChanges();
                            }
                        }
                        break;
                    }
                case 8:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        StringToFormula StringToFormula = new StringToFormula();
                        string strAddedItems = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems && x.NoeFBId == NoeFBId);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
                        Guid intFBId = new Guid();
                        if (varFBUser == null)
                        {
                            clsFB FB = new clsFB();
                            FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            FB.Shomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                            FB.BahayeVahedZarib = 0;
                            FB.NoeFBId = NoeFBId;
                            _context.FBs.Add(FB);
                            _context.SaveChanges();
                            intFBId = FB.ID;
                            //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
                        }
                        else
                            intFBId = varFBUser.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());





                        string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                        var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
                                                       join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                       where RizMetreUsers.LevelNumber == LevelNumber
                                                       select new
                                                       {
                                                           ID = RizMetreUsers.ID,
                                                           Shomareh = RizMetreUsers.Shomareh,
                                                           Tedad = RizMetreUsers.Tedad,
                                                           Tool = RizMetreUsers.Tool,
                                                           Arz = RizMetreUsers.Arz,
                                                           Ertefa = RizMetreUsers.Ertefa,
                                                           Vazn = RizMetreUsers.Vazn,
                                                           Des = RizMetreUsers.Des,
                                                           FBId = RizMetreUsers.FBId,
                                                           OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
                                                           ForItem = RizMetreUsers.ForItem,
                                                           Type = RizMetreUsers.Type,
                                                           UseItem = RizMetreUsers.UseItem,
                                                           BaravordUserId = FB.BarAvordId
                                                       }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                        DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

                        string strConditionOp = strCondition.Replace("x", RM.Arz.ToString().Trim());
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + RM.Shomareh);
                            if (DrRizMetreUsersCurrent.Length == 0)
                            {
                                decimal ArzEzafi = 1;
                                string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                                if (strFinalWorking != "")
                                {
                                    strFinalWorking = strFinalWorking.Replace("x", RM.Arz.ToString());
                                    ArzEzafi = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                                }

                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                RizMetreUsers.Shomareh = RM.Shomareh;
                                ShomareNew++;
                                RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                RizMetreUsers.Sharh = RM.Sharh;

                                if (ItemsFields[0].IsEnteringValue == true)
                                    RizMetreUsers.Tedad = RM.Tedad * ArzEzafi;

                                if (ItemsFields[1].IsEnteringValue == true)
                                    RizMetreUsers.Tool = RM.Tool;

                                if (ItemsFields[2].IsEnteringValue == true)
                                    RizMetreUsers.Arz = RM.Arz;

                                if (ItemsFields[3].IsEnteringValue == true)
                                    RizMetreUsers.Ertefa = RM.Ertefa;

                                if (ItemsFields[4].IsEnteringValue == true)
                                    RizMetreUsers.Vazn = RM.Vazn;

                                RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh;
                                RizMetreUsers.FBId = intFBId;
                                RizMetreUsers.OperationsOfHamlId = 1;
                                RizMetreUsers.Type = "2";
                                RizMetreUsers.ForItem = ItemHasCon.FBShomareh;
                                RizMetreUsers.UseItem = "";
                                RizMetreUsers.LevelNumber = LevelNumber;
                                RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                RizMetreUsers.InsertDateTime = Now;


                                decimal dMeghdarJoz = 0;
                                if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                                    dMeghdarJoz = 0;
                                else
                                    dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                                    (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                                    * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                _context.RizMetreUserses.Add(RizMetreUsers);


                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///

                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = strAddedItems,
                                    NoeFBId = NoeFBId,
                                    //BarAvordHamlId = gBarAvordHamlId,
                                    Shomareh = RizMetreUsers.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);


                                _context.SaveChanges();
                            }
                        }
                        break;
                    }
                case 9:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strAddedItems = AddedItems;
                        string strConditionOp = strCondition.Replace("x", RM.Tool.ToString().Trim()).Replace("y", RM.Arz.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {

                            strAddedItems = strAddedItems.Trim();
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems && x.NoeFBId == NoeFBId);

                            Guid intFBId = new Guid();
                            if (varFBUsersAdded == null)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = ItemHasCon.BarAvordId;
                                FBSave.Shomareh = strAddedItems.Trim();
                                //FBSave.BahayeVahedZarib = 0;
                                FBSave.NoeFBId = NoeFBId;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                intFBId = FBSave.ID;
                            }
                            else
                                intFBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                            long lngShomareh = RM.Shomareh; //long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                            var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
                                                    join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                    where RizMetreUsers.LevelNumber == LevelNumber
                                                    select new
                                                    {
                                                        RizMetreUsers.ID,
                                                        RizMetreUsers.Shomareh,
                                                        RizMetreUsers.Tedad,
                                                        RizMetreUsers.Tool,
                                                        RizMetreUsers.Arz,
                                                        RizMetreUsers.Ertefa,
                                                        RizMetreUsers.Vazn,
                                                        RizMetreUsers.Sharh,
                                                        RizMetreUsers.Des,
                                                        RizMetreUsers.FBId,
                                                        RizMetreUsers.OperationsOfHamlId,
                                                        RizMetreUsers.ForItem,
                                                        RizMetreUsers.Type,
                                                        RizMetreUsers.UseItem,
                                                        FB.BarAvordId
                                                    }).Where(x => x.FBId == FBId && x.ForItem == ItemHasCon.FBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();
                            DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                            if (DtRizMetreUsers.Rows.Count == 0)
                            {
                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                RizMetreUsers.ID = Guid.NewGuid();
                                RizMetreUsers.Shomareh = RM.Shomareh;
                                ShomareNew++;
                                RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                RizMetreUsers.Sharh = RM.Sharh; //" آیتم " + strAddedItems; //RM.Sharh;// DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();


                                decimal? dTedad = null;
                                decimal? dTool = null;
                                decimal? dArz = null;
                                decimal? dErtefa = null;
                                decimal? dVazn = null;

                                if (ItemsFields[0].IsEnteringValue == true)
                                {
                                    dTedad = RM.Tedad;
                                }

                                if (ItemsFields[1].IsEnteringValue == true)
                                {
                                    dTool = RM.Tool;
                                }

                                if (ItemsFields[2].IsEnteringValue == true)
                                {
                                    dArz = RM.Arz;
                                }

                                if (ItemsFields[3].IsEnteringValue == true)
                                {
                                    dErtefa = RM.Ertefa;
                                }

                                if (ItemsFields[4].IsEnteringValue == true)
                                {
                                    dVazn = RM.Vazn;
                                }

                                RizMetreUsers.Tedad = dTedad;
                                RizMetreUsers.Tool = dTool;
                                RizMetreUsers.Arz = dArz;
                                RizMetreUsers.Ertefa = dErtefa;
                                RizMetreUsers.Vazn = dVazn;
                                RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh + " - ریز متره " + lngShomareh;
                                RizMetreUsers.FBId = intFBId;
                                RizMetreUsers.OperationsOfHamlId = 1;
                                RizMetreUsers.Type = "2";
                                RizMetreUsers.ForItem = ItemHasCon.FBShomareh;
                                RizMetreUsers.UseItem = "";
                                RizMetreUsers.LevelNumber = LevelNumber;
                                RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                RizMetreUsers.InsertDateTime = Now;


                                ///محاسبه مقدار جزء
                                decimal dMeghdarJoz = 0;
                                if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                    dMeghdarJoz = 0;
                                else
                                    dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                    (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                _context.RizMetreUserses.Add(RizMetreUsers);


                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///

                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = strAddedItems,
                                    NoeFBId = NoeFBId,
                                    //BarAvordHamlId = gBarAvordHamlId,
                                    Shomareh = RizMetreUsers.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                _context.SaveChanges();
                                //RizMetreUsers.Save();
                            }
                            //else
                            //{
                            //    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                            //    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID).ToList();

                            //    if (clsRizMetreUsers.Count != 0)
                            //    {
                            //        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                            //        context.SaveChanges();
                            //    }
                            //    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                            //}
                            //string strResult = SubItemsAddingToFB(int.Parse(DtItemsAddingToFB.Rows[i]["ID"].ToString()), Ertefa, RizMetreId, BarAvordId, strAddedItems);
                        }
                        break;
                    }
                case 10:
                    {
                        if (blnCheckAgain)
                        {
                            string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                            string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                            string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString());
                            StringToFormula StringToFormula = new StringToFormula();
                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                            if (blnCheck)
                            {
                                blnCheckAgain = false;

                                strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString());
                                decimal dZarib = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                                if (dZarib == 0)
                                {
                                    clsFB? varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == AddedItems).FirstOrDefault();
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
                                    //return new JsonResult("CI");//CheckInput
                                }
                                else
                                {
                                    dZarib = dZarib < 0 ? dZarib * -1 : dZarib;
                                    var varBA = _context.BaravordUsers.Where(x => x.ID == ItemHasCon.BarAvordId).ToList();
                                    DataTable DtBA = clsConvert.ToDataTable(varBA);
                                    //Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    string strItemShomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId).FirstOrDefault();

                                    //blnCheckIsExistWarning = true;
                                    //if (dZarib > 1)
                                    //{
                                    //    strWarning = "در محدوده رواداری غیر مجاز";
                                    //}
                                    //else
                                    //{
                                    //    strWarning = "در محدوده رواداری مجاز";
                                    //}

                                    Guid intFBId = new Guid();
                                    if (FBUser == null)
                                    {
                                        clsFB FB = new clsFB();
                                        FB.BarAvordId = ItemHasCon.BarAvordId;//  Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        FB.Shomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                                        FB.BahayeVahedZarib = dZarib;
                                        FB.NoeFBId = NoeFBId;
                                        _context.FBs.Add(FB);
                                        _context.SaveChanges();
                                        intFBId = FB.ID;
                                    }
                                    else
                                    {
                                        intFBId = FBUser.ID;
                                    }


                                    string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
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
                                    //DataTable DtRizMetreUsersesCurrent = clsRizMetreUserses.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
                                    //for (int i = 0; i < DtRizMetreUserses.Rows.Count; i++)
                                    DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh);
                                    if (DrRizMetreUsersesCurrent.Length == 0)
                                    {
                                        decimal? Tedad = RM.Tedad;
                                        decimal? Tool = RM.Tool;
                                        decimal? Arz = RM.Arz;
                                        decimal? Ertefa = RM.Ertefa;
                                        decimal? Vazn = dZarib;

                                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                        RizMetreUserses.Shomareh = RM.Shomareh;
                                        ShomareNew++;
                                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                        RizMetreUserses.Sharh = RM.Sharh;

                                        if (ItemsFields[0].IsEnteringValue == true)
                                            RizMetreUserses.Tedad = Tedad;

                                        if (ItemsFields[1].IsEnteringValue == true)
                                            RizMetreUserses.Tool = Tool;

                                        if (ItemsFields[2].IsEnteringValue == true)
                                            RizMetreUserses.Arz = Arz;

                                        if (ItemsFields[3].IsEnteringValue == true)
                                            RizMetreUserses.Ertefa = Ertefa;

                                        if (ItemsFields[4].IsEnteringValue == true)
                                            RizMetreUserses.Vazn = Vazn;

                                        RizMetreUserses.Des = RM.Des;// DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh
                                                                     //+ " - ریز متره شماره " + RM.Shomareh;
                                        RizMetreUserses.FBId = intFBId;
                                        RizMetreUserses.OperationsOfHamlId = 1;
                                        RizMetreUserses.Type = "2";
                                        RizMetreUserses.ForItem = ItemHasCon.FBShomareh;
                                        RizMetreUserses.UseItem = "";
                                        RizMetreUserses.LevelNumber = LevelNumber;
                                        RizMetreUserses.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                        RizMetreUserses.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                        RizMetreUserses.InsertDateTime = Now;

                                        ///محاسبه مقدار جزء
                                        decimal dMeghdarJoz = 0;
                                        if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                                            dMeghdarJoz = 0;
                                        else
                                            dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;


                                        _context.RizMetreUserses.Add(RizMetreUserses);


                                        //Start///////////////////////
                                        ////////////حمل///////////////
                                        //////////////////////////////
                                        ///آیتم های حمل درج میگردد
                                        ///

                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                        {
                                            BarAvordUserId = ItemHasCon.BarAvordId,
                                            Year = Year,
                                            ItemFBShomareh = strItemShomareh,
                                            NoeFBId = NoeFBId,
                                            //BarAvordHamlId = gBarAvordHamlId,
                                            Shomareh = RizMetreUserses.Shomareh,
                                            MeghdarJoz = dMeghdarJoz,
                                            LevelNumber = 1,
                                        };
                                        await HamlCommon.SaveHaml(requestSaveHaml, _context);


                                        _context.SaveChanges();
                                        //}
                                    }
                                }
                            }
                            else
                            {
                                //if (blnCheckIsExistErrors != "false")
                                //{
                                //    blnCheckIsExistErrors = "true";
                                //    strErrors = "عدد وارد شده در محدوده قابل قبولی نمیباشد "; //strCondition.Replace("x", "عدد وارد شده");
                                //}
                            }
                        }
                        break;
                    }
                case 11:
                    {
                        //string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                        string strItemShomareh = ItemHasCon.FBShomareh.Substring(0, 6);
                        string strItemShomarehAdd = AddedItems + strCharacterPlus;
                        string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                        string strDesOfAddingItems = DesOfAddingItems;
                        decimal dPercent = decimal.Parse(strFinalWorking);
                        //blnCheckIsExistErrors = "false";
                        //clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //ItemsHasConditionAddedToFB.BarAvordId = ItemHasCon.BarAvordId;
                        //ItemsHasConditionAddedToFB.FBShomareh = strItemShomareh;
                        //ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = itemsAddingToFBForCheckOperation.ItemsHasCondition_ConditionContextId;
                        //ItemsHasConditionAddedToFB.Meghdar = ItemHasCon.Meghdar;
                        //ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;
                        //bool blnCheckSave = false;
                        //try
                        //{
                        //    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                        //    _context.SaveChanges();
                        //    blnCheckSave = true;
                        //}
                        //catch (Exception)
                        //{
                        //    blnCheckSave = false;
                        //}

                        //if (blnCheckSave)
                        //{
                        //clsBaravordUser BA = _context.BaravordUsers.Where(x => x.ID == ItemHasCon.BarAvordId).FirstOrDefault();
                        //if (BA != null)
                        //{

                        clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomarehAdd && x.NoeFBId == NoeFBId).FirstOrDefault();

                        Guid intFBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FB = new clsFB();
                            FB.BarAvordId = ItemHasCon.BarAvordId;
                            FB.Shomareh = strItemShomarehAdd;
                            FB.BahayeVahedZarib = dPercent;
                            FB.BahayeVahedSharh = strDesOfAddingItems;
                            FB.NoeFBId = NoeFBId;
                            _context.FBs.Add(FB);
                            _context.SaveChanges();
                            intFBId = FB.ID;
                        }
                        else
                        {
                            intFBId = FBUser.ID;
                        }


                        //string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);
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
                                                         }).Where(x => x.FBId == intFBId && x.ForItem == strItemShomareh && x.Type == "2").ToList();
                        DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

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

                            if (ItemsFields[0].IsEnteringValue == true)
                                RizMetreUserses.Tedad = Tedad;

                            if (ItemsFields[1].IsEnteringValue == true)
                                RizMetreUserses.Tool = Tool;

                            if (ItemsFields[2].IsEnteringValue == true)
                                RizMetreUserses.Arz = Arz;

                            if (ItemsFields[3].IsEnteringValue == true)
                                RizMetreUserses.Ertefa = Ertefa;

                            if (ItemsFields[4].IsEnteringValue == true)
                                RizMetreUserses.Vazn = Vazn;

                            RizMetreUserses.Des = ""; //Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                                                      //+ " - ریز متره شماره " + RM.Shomareh;
                            RizMetreUserses.FBId = intFBId;
                            RizMetreUserses.OperationsOfHamlId = 1;
                            RizMetreUserses.Type = "2";
                            RizMetreUserses.ForItem = strItemShomareh; //ItemHasCon.FBShomareh.Substring(0, 6);
                            RizMetreUserses.UseItem = "";
                            RizMetreUserses.LevelNumber = LevelNumber;
                            RizMetreUserses.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                            RizMetreUserses.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                            RizMetreUserses.InsertDateTime = Now;

                            ///محاسبه مقدار جزء
                            decimal dMeghdarJoz = 0;
                            if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                                dMeghdarJoz = 0;
                            else
                                dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                            //Start///////////////////////
                            ////////////حمل///////////////
                            //////////////////////////////
                            ///آیتم های حمل درج میگردد
                            ///

                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                            {
                                BarAvordUserId = ItemHasCon.BarAvordId,
                                Year = Year,
                                ItemFBShomareh = strItemShomareh,
                                NoeFBId = NoeFBId,
                                //BarAvordHamlId = gBarAvordHamlId,
                                Shomareh = RizMetreUserses.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                LevelNumber = 1,
                            };
                            await HamlCommon.SaveHaml(requestSaveHaml, _context);


                            _context.RizMetreUserses.Add(RizMetreUserses);
                            await _context.SaveChangesAsync();
                            //RizMetreUserses.Save();
                        }
                        break;
                    }
                case 12:
                    {
                        string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];
                        string strCondition = Condition;
                        string[] strConditionSplit = strCondition.Split("_");
                        string strAddedItems = AddedItems;
                        string strFinalWorking = FinalWorking;
                        DataTable DtRizMetreUserses = new DataTable();
                        string strForItem = "";
                        string strUseItem = "";
                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        Guid guFBId = FBId;
                        strUseItem = strItemFBShomareh;
                        strForItem = UseItemForAdd;

                        decimal? dErtefa1 = null;
                        string strConditionOp = strConditionSplit[0].Replace("x", RM.Tool == null ? "0" : RM.Tool.Value.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck2 = true;
                        if (strConditionSplit.Length == 2)
                        {
                            string strConditionOp2 = strConditionSplit[1].Replace("z", RM.Ertefa == null ? "0" : RM.Ertefa.Value.ToString().Trim());
                            blnCheck2 = StringToFormula.RelationalExpression2(strConditionOp2);
                            ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                            ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                            ///Ertefa-3
                            ///
                            strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa == null ? "0" : RM.Ertefa.Value.ToString().Trim());
                            dErtefa1 = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                        }
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            if (blnCheck2)
                            {
                                //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                                clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                     .FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.FBShomareh.Trim() == strCurrentFBShomareh.Trim() &&
                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                if (currentItemsHasConditionAddedToFBs == null)
                                {
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = ItemHasCon.BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                    ItemsHasConditionAddedToFB.Meghdar = RM.Tool != null ? RM.Tool.Value : 0;
                                    ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;
                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    _context.SaveChanges();
                                }

                                if (strFinalWorking != "")
                                {
                                    strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");
                                    string strItemShomareh = AddedItems;
                                    strAddedItems = strAddedItems.Trim();

                                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems && x.NoeFBId == NoeFBId).FirstOrDefault();

                                    Guid intFBId = new Guid();
                                    if (FBUser == null)
                                    {
                                        clsFB FBSave = new clsFB();
                                        FBSave.BarAvordId = ItemHasCon.BarAvordId;
                                        FBSave.Shomareh = strAddedItems.Trim();
                                        //FBSave.BahayeVahedZarib = 0;
                                        FBSave.NoeFBId = NoeFBId;
                                        _context.FBs.Add(FBSave);
                                        _context.SaveChanges();
                                        intFBId = FBSave.ID;
                                    }
                                    else
                                        intFBId = FBUser.ID;

                                    long lngShomareh = RM.Shomareh;
                                    var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
                                                            join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                            where RizMetreUsers.LevelNumber == LevelNumber
                                                            select new
                                                            {
                                                                RizMetreUsers.ID,
                                                                RizMetreUsers.Shomareh,
                                                                RizMetreUsers.Tedad,
                                                                RizMetreUsers.Tool,
                                                                RizMetreUsers.Arz,
                                                                RizMetreUsers.Ertefa,
                                                                RizMetreUsers.Vazn,
                                                                RizMetreUsers.Sharh,
                                                                RizMetreUsers.Des,
                                                                RizMetreUsers.FBId,
                                                                RizMetreUsers.OperationsOfHamlId,
                                                                RizMetreUsers.ForItem,
                                                                RizMetreUsers.Type,
                                                                RizMetreUsers.UseItem,
                                                                FB.BarAvordId
                                                            }).Where(x => x.FBId == FBId && x.ForItem == strItemFBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();
                                    DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);


                                    if (DtRizMetreUsers.Rows.Count == 0)
                                    {
                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                        RizMetreUsers.Shomareh = RM.Shomareh;
                                        ShomareNew++;
                                        RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                        RizMetreUsers.Sharh = RM.Sharh;// " آیتم " + strAddedItems; //DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();

                                        List<string> lst = new List<string>();
                                        for (int j = 0; j < strFieldsAdding.Length; j++)
                                        {
                                            lst.Add(strFieldsAdding[j]);
                                        }

                                        decimal? dTedad = null;
                                        decimal? dTool = null;
                                        decimal? dArz = null;
                                        //decimal? dErtefa = null;
                                        decimal? dVazn = null;

                                        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dTedad = RM.Tedad;
                                        }
                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dTool = RM.Tool;
                                        }
                                        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dArz = RM.Arz;
                                        }
                                        //strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                        //if (strCal.Count != 0)
                                        //{
                                        //    dErtefa = RM.Ertefa;
                                        //}
                                        strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dVazn = RM.Vazn;
                                        }

                                        RizMetreUsers.Tedad = dTedad;
                                        RizMetreUsers.Tool = dTool;
                                        RizMetreUsers.Arz = dArz;
                                        RizMetreUsers.Ertefa = dErtefa1;
                                        RizMetreUsers.Vazn = dVazn;
                                        RizMetreUsers.Des = RM.Des; //DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh + " - ریز متره " + lngShomareh;
                                        RizMetreUsers.FBId = intFBId;
                                        RizMetreUsers.OperationsOfHamlId = 1;
                                        RizMetreUsers.Type = "2";
                                        RizMetreUsers.ForItem = strItemFBShomareh;
                                        RizMetreUsers.UseItem = "";
                                        RizMetreUsers.LevelNumber = LevelNumber;
                                        RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                        RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                        RizMetreUsers.InsertDateTime = Now;


                                        ///محاسبه مقدار جزء
                                        decimal dMeghdarJoz = 0;
                                        if (dTedad == null && dTool == null && dArz == null && dVazn == null)
                                            dMeghdarJoz = 0;
                                        else
                                            dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                            (dArz == null ? 1 : dArz.Value) * (dErtefa1 == null ? 1 : dErtefa1.Value) * (dVazn == null ? 1 : dVazn.Value);
                                        RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                        _context.RizMetreUserses.Add(RizMetreUsers);


                                        //Start///////////////////////
                                        ////////////حمل///////////////
                                        //////////////////////////////
                                        ///آیتم های حمل درج میگردد
                                        ///

                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                        {
                                            BarAvordUserId = ItemHasCon.BarAvordId,
                                            Year = Year,
                                            ItemFBShomareh = strItemShomareh,
                                            NoeFBId = NoeFBId,
                                            //BarAvordHamlId = gBarAvordHamlId,
                                            Shomareh = RizMetreUsers.Shomareh,
                                            MeghdarJoz = dMeghdarJoz,
                                            LevelNumber = 1,
                                        };
                                        await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                        _context.SaveChanges();
                                    }
                                }
                            }

                        }

                        break;
                    }
                case 13:
                    {
                        //decimal Meghdar = ItemHasCon.Meghdar;
                        //decimal Meghdar2 = ItemHasCon.Meghdar2;
                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);

                        //clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //ItemsHasConditionAddedToFB.BarAvordId = ItemHasCon.BarAvordId;
                        //ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;
                        //ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = ItemHasCon.ConditionContextId;
                        //ItemsHasConditionAddedToFB.Meghdar = Meghdar;
                        //ItemsHasConditionAddedToFB.Meghdar2 = Meghdar2;
                        //ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;

                        string[] strCondition = Condition.Split("_");
                        string strCondition1 = strCondition[0];
                        string strCondition2 = strCondition[1];
                        string strFinalWorking = FinalWorking;
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

                                //bool blnCheckSave = false;
                                //try
                                //{
                                //    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                //    _context.SaveChanges();
                                //    blnCheckSave = true;
                                //}
                                //catch (Exception)
                                //{
                                //    blnCheckSave = false;
                                //}
                                //if (blnCheckSave)
                                //{
                                var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                DataTable DtBA = clsConvert.ToDataTable(varBA);


                                clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == AddedItems && x.NoeFBId == NoeFBId).FirstOrDefault();

                                Guid intFBId = new Guid();
                                if (FBUser == null)
                                {
                                    clsFB FBSave = new clsFB();
                                    FBSave.BarAvordId = ItemHasCon.BarAvordId;
                                    FBSave.Shomareh = AddedItems.Trim();
                                    //FBSave.BahayeVahedZarib = 0;
                                    FBSave.NoeFBId = NoeFBId;
                                    _context.FBs.Add(FBSave);
                                    _context.SaveChanges();
                                    intFBId = FBSave.ID;
                                }
                                else
                                    intFBId = FBUser.ID;



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
                                                          }).Where(x => x.FBId == FBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                                DataTable DtRizMetreUserses = clsConvert.ToDataTable(varRizMetreUserses);

                                string strShomareh1 = ItemHasCon.FBShomareh;
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


                                DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh);
                                if (DrRizMetreUsersesCurrent.Length == 0)
                                {
                                    decimal? Tedad = RM.Tedad;
                                    decimal? Tool = RM.Tool;
                                    decimal? Arz = RM.Arz;
                                    decimal? Ertefa = RM.Ertefa;
                                    //decimal? Vazn = DtRizMetreUserses.Rows[i]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUserses.Rows[i]["Vazn"].ToString());


                                    clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                    RizMetreUserses.Shomareh = RM.Shomareh;
                                    ShomareNew++;
                                    RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUserses.Sharh = RM.Sharh;

                                    if (ItemsFields[0].IsEnteringValue == true)
                                        RizMetreUserses.Tedad = Tedad;

                                    if (ItemsFields[1].IsEnteringValue == true)
                                        RizMetreUserses.Tool = Tool;

                                    if (ItemsFields[2].IsEnteringValue == true)
                                        RizMetreUserses.Arz = Arz;

                                    if (ItemsFields[3].IsEnteringValue == true)
                                        RizMetreUserses.Ertefa = Ertefa;

                                    if (ItemsFields[4].IsEnteringValue == true)
                                        RizMetreUserses.Vazn = dZaribVazn;

                                    RizMetreUserses.Des = RM.Des;
                                    RizMetreUserses.FBId = intFBId;
                                    RizMetreUserses.OperationsOfHamlId = 1;
                                    RizMetreUserses.Type = "2";
                                    RizMetreUserses.ForItem = strShomareh1;
                                    RizMetreUserses.UseItem = "";
                                    RizMetreUserses.LevelNumber = LevelNumber;
                                    RizMetreUserses.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                    RizMetreUserses.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                    RizMetreUserses.InsertDateTime = Now;

                                    ///محاسبه مقدار جزء
                                    decimal dMeghdarJoz = 0;
                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0)
                                        dMeghdarJoz = 0;
                                    else
                                        dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                        (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * dZaribVazn;
                                    RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                    _context.RizMetreUserses.Add(RizMetreUserses);


                                    //Start///////////////////////
                                    ////////////حمل///////////////
                                    //////////////////////////////
                                    ///آیتم های حمل درج میگردد
                                    ///

                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                    {
                                        BarAvordUserId = ItemHasCon.BarAvordId,
                                        Year = Year,
                                        ItemFBShomareh = AddedItems,
                                        NoeFBId = NoeFBId,
                                        //BarAvordHamlId = gBarAvordHamlId,
                                        Shomareh = RizMetreUserses.Shomareh,
                                        MeghdarJoz = dMeghdarJoz,
                                        LevelNumber = 1,
                                    };
                                    await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                    _context.SaveChanges();
                                    //RizMetreUserses.Save();
                                }
                            }
                            //}
                        }
                        //else
                        //{
                        //    return new JsonResult("CI");//CheckInput
                        //}

                        //else
                        //{
                        //    return new JsonResult("CI");//CheckInput
                        //}
                        break;
                    }
                case 14:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        string[] strFinalWorking1 = FinalWorking.Split(',');
                        string strFinalWorking = strFinalWorking1[0];
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                            decimal? dMultiple = null;
                            if (strFinalWorking != "")
                            {
                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                            }

                            string strAddedItems = AddedItems;

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strAddedItems && x.NoeFB == NoeFBId)
                                    .OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                            clsFB varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems && x.NoeFBId == NoeFBId);
                            Guid intFBId = new Guid();
                            if (varFBUser == null)
                            {
                                clsFB FB = new clsFB();
                                FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                FB.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                                FB.BahayeVahedZarib = 0;
                                FB.NoeFBId = NoeFBId;
                                _context.FBs.Add(FB);
                                _context.SaveChanges();
                                intFBId = FB.ID;
                            }
                            else
                                intFBId = varFBUser.ID;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                            string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();
                            var varRizMetreUsersCurrent = (from RizMetreUsers in _context.RizMetreUserses
                                                           join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                           where RizMetreUsers.LevelNumber == LevelNumber
                                                           select new
                                                           {
                                                               ID = RizMetreUsers.ID,
                                                               Shomareh = RizMetreUsers.Shomareh,
                                                               Tedad = RizMetreUsers.Tedad,
                                                               Tool = RizMetreUsers.Tool,
                                                               Arz = RizMetreUsers.Arz,
                                                               Ertefa = RizMetreUsers.Ertefa,
                                                               Vazn = RizMetreUsers.Vazn,
                                                               Des = RizMetreUsers.Des,
                                                               FBId = RizMetreUsers.FBId,
                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
                                                               ForItem = RizMetreUsers.ForItem,
                                                               Type = RizMetreUsers.Type,
                                                               UseItem = RizMetreUsers.UseItem,
                                                               BaravordUserId = FB.BarAvordId
                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

                            DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + RM.Shomareh);
                            if (DrRizMetreUsersCurrent.Length == 0)
                            {
                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                RizMetreUsers.Shomareh = RM.Shomareh;
                                ShomareNew++;
                                RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                RizMetreUsers.Sharh = RM.Sharh;

                                if (lstCurrentItemsFields[0] == true)
                                    RizMetreUsers.Tedad = RM.Tedad;

                                if (lstCurrentItemsFields[1] == true)
                                    RizMetreUsers.Tool = RM.Tool;

                                if (lstCurrentItemsFields[2] == true)
                                    RizMetreUsers.Arz = RM.Arz;

                                RizMetreUsers.Ertefa = dMultiple;

                                decimal? dVazn = null;
                                if (lstCurrentItemsFields[4] == true)
                                    dVazn = RM.Vazn;

                                if (strFinalWorking1.Length > 1)
                                {
                                    string[] strVazn = strFinalWorking1[1].Split('_');
                                    dVazn = decimal.Parse(strVazn[1]);
                                }

                                RizMetreUsers.Vazn = dVazn;

                                RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh;
                                RizMetreUsers.FBId = intFBId;
                                RizMetreUsers.OperationsOfHamlId = 1;
                                RizMetreUsers.Type = "2";
                                RizMetreUsers.ForItem = ItemHasCon.FBShomareh;
                                RizMetreUsers.UseItem = "";
                                RizMetreUsers.LevelNumber = LevelNumber;
                                RizMetreUsers.ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
                                RizMetreUsers.ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
                                RizMetreUsers.InsertDateTime = Now;

                                decimal dMeghdarJoz = 0;
                                if (RizMetreUsers.Tedad == null && RizMetreUsers.Tool == null && RizMetreUsers.Arz == null && RizMetreUsers.Ertefa == null && RizMetreUsers.Vazn == null)
                                    dMeghdarJoz = 0;
                                else
                                    dMeghdarJoz += (RizMetreUsers.Tedad == null ? 1 : RizMetreUsers.Tedad.Value) * (RizMetreUsers.Tool == null ? 1 : RizMetreUsers.Tool.Value) *
                                    (RizMetreUsers.Arz == null ? 1 : RizMetreUsers.Arz.Value) * (RizMetreUsers.Ertefa == null ? 1 : RizMetreUsers.Ertefa.Value)
                                    * (RizMetreUsers.Vazn == null ? 1 : RizMetreUsers.Vazn.Value);

                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                _context.RizMetreUserses.Add(RizMetreUsers);



                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///

                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = AddedItems,
                                    NoeFBId = NoeFBId,
                                    //BarAvordHamlId = gBarAvordHamlId,
                                    Shomareh = RizMetreUsers.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                _context.SaveChanges();
                                //RizMetreUsers.Save();
                            }
                        }
                        break;
                    }
                case 15:
                    {

                        ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            blnCheckAgain = blnCheckAgain,
                            RMIdForEdit = SingleRMIdForAdd
                        };

                        //ConditionCaseCommon CCC = new ConditionCaseCommon();
                        //ConditionCaseCommonCaseResultDto blnResult = await CCC.Case15(RequestCase1, _context);

                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case15(RequestCase1, _context);

                        blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                        strWarning = ResultCCC.strWarning;
                        Status = ResultCCC.blnStatus;

                        blnResult1.Add(ResultCCC.blnStatus);
                        blnResult = ResultCCC.blnStatus;
                        break;


                        //بررسی اینکه در تراش یا لکه گیری هست یا خیر
                        //اگر باشد دیگر صعوبت درج نمیشود

                        //RizMetreCommon rizMetreCommon = new RizMetreCommon();
                        //long[] lngConditionGroupId = { 31, 4 };

                        //string strFBShomareh = ItemHasCon.FBShomareh.Trim();
                        //GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                        //{
                        //    ShomarehFB = strFBShomareh,
                        //    BarAvordUserId = guBAId,
                        //    NoeFB = NoeFBId,
                        //    Year = Year,
                        //    LevelNumber = LevelNumber,
                        //    ConditionGroupId = lngConditionGroupId
                        //};
                        //List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);

                        //string CharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";

                        //string strCondition = Condition;
                        //string[] strConditionSplit = strCondition.Split("_");

                        //string strFinalWorking1 = FinalWorking;
                        //string[] strFinalWorkingSplit = strFinalWorking1.Split('_');
                        //string strFinalWorking = strFinalWorkingSplit[0];
                        //decimal dBahayeVahedZarib = strFinalWorkingSplit.Length > 1 ? decimal.Parse(strFinalWorkingSplit[1]) : 0;

                        //DataTable DtRizMetreUserses = new DataTable();
                        ////string strItemFBShomareh = strFBShomareh;
                        //string strDesOfAddingItems = DesOfAddingItems;

                        //Guid guFBId = FBId;// Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                        //string strUseItem = UseItemForAdd;
                        //string strForItem = strItemFBShomareh;
                        ////clsFB? varFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strForItem).ToList();

                        ////if (varFBUser != null)
                        ////{
                        //List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                        //                                                                    join fb in _context.FBs on RUsers.FBId equals fb.ID
                        //                                                                    where RUsers.LevelNumber == LevelNumber
                        //                                                                    select new RizMetreUsersForItemsAddingToFBInputDto
                        //                                                                    {
                        //                                                                        Id = RUsers.ID,
                        //                                                                        Shomareh = RUsers.Shomareh,
                        //                                                                        Sharh = RUsers.Sharh,
                        //                                                                        Tedad = RUsers.Tedad,
                        //                                                                        Tool = RUsers.Tool,
                        //                                                                        Arz = RUsers.Arz,
                        //                                                                        Ertefa = RUsers.Ertefa,
                        //                                                                        Vazn = RUsers.Vazn,
                        //                                                                        Des = RUsers.Des,
                        //                                                                        ForItem = RUsers.ForItem,
                        //                                                                        Type = RUsers.Type,
                        //                                                                        FBId = RUsers.FBId
                        //                                                                    }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();

                        ////}

                        //List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUsersesWithoutLakeAndTarash = new List<RizMetreUsersForItemsAddingToFBInputDto>();

                        //foreach (var RM1 in varRizMetreUserses)
                        //{
                        //    RizMetreForGetAndShowAddItemsDto? FindRM = lstRM.FirstOrDefault(x => x.Shomareh == RM1.Shomareh);
                        //    if (FindRM == null)
                        //    {
                        //        varRizMetreUsersesWithoutLakeAndTarash.Add(varRizMetreUserses.FirstOrDefault(x => x.Shomareh == RM1.Shomareh));
                        //    }
                        //}

                        //List<Guid> lstRMIds = varRizMetreUsersesWithoutLakeAndTarash.Select(x => x.Id).ToList();

                        //List<clsRizMetreUsers> lstRizMetreUser = _context.RizMetreUserses.Where(x => lstRMIds.Contains(x.ID)).ToList();

                        //if (varRizMetreUsersesWithoutLakeAndTarash.Count != 0)
                        //{
                        //    var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                        //                                     join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                        //                                     where RizMetreUserses.LevelNumber == LevelNumber
                        //                                     select new
                        //                                     {
                        //                                         ID = RizMetreUserses.ID,
                        //                                         Shomareh = RizMetreUserses.Shomareh,
                        //                                         Tedad = RizMetreUserses.Tedad,
                        //                                         Tool = RizMetreUserses.Tool,
                        //                                         Arz = RizMetreUserses.Arz,
                        //                                         Ertefa = RizMetreUserses.Ertefa,
                        //                                         Vazn = RizMetreUserses.Vazn,
                        //                                         Des = RizMetreUserses.Des,
                        //                                         FBId = RizMetreUserses.FBId,
                        //                                         OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                        //                                         ForItem = RizMetreUserses.ForItem,
                        //                                         Type = RizMetreUserses.Type,
                        //                                         UseItem = RizMetreUserses.UseItem,
                        //                                         BarAvordId = FB.BarAvordId
                        //                                     }).Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();

                        //    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                        //    List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.NoeFBId == NoeFBId).ToList();
                        //    string strItemShomareh = AddedItems + CharacterPlus;
                        //    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                        //    Guid intFBId = new Guid();
                        //    if (varFBUser == null)
                        //    {
                        //        clsFB FB = new clsFB();
                        //        FB.BarAvordId = guBAId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                        //        FB.Shomareh = strItemShomareh; //AddedItems.Trim();
                        //        FB.BahayeVahedZarib = FinalWorking.Length > 1 ? decimal.Parse(FinalWorking) : 0;
                        //        FB.BahayeVahedSharh = strDesOfAddingItems;
                        //        FB.NoeFBId = NoeFBId;
                        //        _context.FBs.Add(FB);
                        //        _context.SaveChanges();
                        //        intFBId = FB.ID;
                        //    }
                        //    else
                        //        intFBId = varFBUser.ID;

                        //    string strConditionOp = strConditionSplit[0].Replace("x", RM.Arz.ToString().Trim());
                        //    StringToFormula StringToFormula = new StringToFormula();
                        //    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        //    if (blnCheck)
                        //    {
                        //        //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                        //        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                        //             .FirstOrDefault(x => x.BarAvordId == guBAId && x.FBShomareh == strCurrentFBShomareh &&
                        //                 x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                        //        if (currentItemsHasConditionAddedToFBs == null)
                        //        {
                        //            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //            ItemsHasConditionAddedToFB.BarAvordId = guBAId;
                        //            ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                        //            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                        //            ItemsHasConditionAddedToFB.Meghdar = RM.Tool != null ? RM.Tool.Value : 0;
                        //            ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;
                        //            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                        //            _context.SaveChanges();
                        //        }

                        //        decimal? dArz = null;
                        //        if (RM.Arz <= 2)
                        //            dArz = RM.Arz;
                        //        else
                        //        {
                        //            dArz = 2;
                        //            ///بایستی سابقه در جدول سوابق ریزمتره دخیره گردد تا در صورتی که تیک تعریض برداشته شد، آیتم اصلی به حالت اولش برگردد
                        //            ///
                        //            clsRizMetreUsersHistory rizMetreUsersHistory = new clsRizMetreUsersHistory
                        //            {
                        //                RizMetreUsersId = RM.ID,
                        //                Shomareh = RM.Shomareh,
                        //                ShomarehNew = RM.ShomarehNew,
                        //                Sharh = RM.Sharh,
                        //                Tedad = RM.Tedad,
                        //                Tool = RM.Tool,
                        //                Arz = RM.Arz,
                        //                Ertefa = RM.Ertefa,
                        //                Vazn = RM.Vazn,
                        //                MeghdarJoz = RM.MeghdarJoz,
                        //                Des = RM.Des
                        //            };
                        //            _context.RizMetreUsersHistories.Add(rizMetreUsersHistory);

                        //            RM.Arz = 2;

                        //            decimal? dMeghdarJoz = null;

                        //            dMeghdarJoz = (RM.Tedad == null ? 1 : RM.Tedad.Value) * (RM.Tool == null ? 1 : RM.Tool.Value) *
                        //            2 * (RM.Ertefa == null ? 1 : RM.Ertefa.Value) * (RM.Vazn == null ? 1 : RM.Vazn.Value);

                        //            RM.MeghdarJoz = dMeghdarJoz;
                        //        }


                        //        //string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus;


                        //        DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RM.Shomareh + " and FBId='" + FBId + "'");
                        //        if (DrRizMetreUsersesCurrent.Length == 0)
                        //        {
                        //            decimal? Tedad = RM.Tedad == 0 ? 1 : RM.Tedad;
                        //            decimal? Tool = RM.Tool;
                        //            //decimal? Arz = RM.Arz;
                        //            decimal? Ertefa = RM.Ertefa;
                        //            decimal? Vazn = RM.Vazn;

                        //            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //            RizMetreUserses.Shomareh = RM.Shomareh;
                        //            ShomareNew++;
                        //            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //            RizMetreUserses.Sharh = RM.Sharh;
                        //            RizMetreUserses.Tedad = Tedad;
                        //            RizMetreUserses.Tool = Tool;
                        //            RizMetreUserses.Arz = dArz;
                        //            RizMetreUserses.Ertefa = Ertefa;
                        //            RizMetreUserses.Vazn = Vazn;
                        //            RizMetreUserses.Des = RM.Des;

                        //            RizMetreUserses.FBId = intFBId;
                        //            RizMetreUserses.OperationsOfHamlId = 1;
                        //            RizMetreUserses.Type = "2";
                        //            RizMetreUserses.ForItem = strForItem;
                        //            RizMetreUserses.UseItem = strUseItem;
                        //            RizMetreUserses.LevelNumber = LevelNumber;
                        //            RizMetreUserses.InsertDateTime = Now;

                        //            ///محاسبه مقدار جزء
                        //            decimal? dMeghdarJoz = null;
                        //            if (Tedad == null && Tool == null && dArz == null && Ertefa == null && dArz == null && Vazn == null)
                        //                dMeghdarJoz = null;
                        //            else
                        //                dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                        //                (dArz == null ? 1 : dArz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);

                        //            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //            _context.RizMetreUserses.Add(RizMetreUserses);


                        //            //Start///////////////////////
                        //            ////////////حمل///////////////
                        //            //////////////////////////////
                        //            ///آیتم های حمل درج میگردد
                        //            ///

                        //            SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //            {
                        //                BarAvordUserId = ItemHasCon.BarAvordId,
                        //                Year = Year,
                        //                ItemFBShomareh = AddedItems,
                        //                NoeFBId = NoeFBId,
                        //                //BarAvordHamlId = gBarAvordHamlId,
                        //                Shomareh = RizMetreUserses.Shomareh,
                        //                MeghdarJoz = dMeghdarJoz,
                        //                LevelNumber = 1,
                        //            };
                        //            await HamlCommon.SaveHaml(requestSaveHaml, _context);

                        //            _context.SaveChanges();


                        //        }
                        //    }
                        //}
                        //break;
                    }
                case 16:
                    {
                        ///بررسی پخش، آبپاشی، تسطیح و کوبیدن قشر زیراساس  
                        /////در صورت یافتن ریز متره درج شده برای این شرط بایستی به 
                        ///اضافه بها بابت سختی اجرا در شانه سازی ها به عرض تا 2 متر
                        ///اضافه گردد
                        RizMetreCommon rizMetreCommon = new RizMetreCommon();
                        long[] lngConditionGroupId = { 12 };

                        string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];

                        GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                        {
                            ShomarehFB = ItemHasCon.FBShomareh,
                            BarAvordUserId = BarAvordId,
                            NoeFB = NoeFBId,
                            Year = Year,
                            LevelNumber = LevelNumber,
                            ConditionGroupId = lngConditionGroupId
                        };
                        List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);


                        if (lstRM.Count != 0)
                        {
                            //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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
                                ItemsHasConditionAddedToFB.Meghdar = ItemHasCon.Meghdar;
                                ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;

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
                                var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                DataTable DtBA = clsConvert.ToDataTable(varBA);

                                //Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                string strItemShomareh = AddedItems;
                                var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                Guid intFBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    FB.Shomareh = AddedItems;
                                    FB.BahayeVahedZarib = 0;
                                    FB.BahayeVahedSharh = "";
                                    FB.NoeFBId = NoeFBId;
                                    _context.FBs.Add(FB);
                                    _context.SaveChanges();
                                    intFBId = FB.ID;
                                }
                                else
                                {
                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                }


                                //string[] FieldsAddingSplit = strFieldsAdding;

                                string strShomareh1 = ItemHasCon.FBShomareh;
                                //Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
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

                                foreach (var itemRM in lstRM)
                                {
                                    string strCondition = Condition;

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
                                            RizMetreUserses.FBId = intFBId;
                                            RizMetreUserses.OperationsOfHamlId = 1;
                                            RizMetreUserses.Type = "2";
                                            RizMetreUserses.ForItem = ItemHasCon.FBShomareh;
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
                                            //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                            //RizMetreUserses.ConditionContextId = ConditionContextId;

                                            _context.RizMetreUserses.Add(RizMetreUserses);



                                            //Start///////////////////////
                                            ////////////حمل///////////////
                                            //////////////////////////////
                                            ///آیتم های حمل درج میگردد
                                            ///

                                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                                            {
                                                BarAvordUserId = ItemHasCon.BarAvordId,
                                                Year = Year,
                                                ItemFBShomareh = AddedItems,
                                                NoeFBId = NoeFBId,
                                                //BarAvordHamlId = gBarAvordHamlId,
                                                Shomareh = RizMetreUserses.Shomareh,
                                                MeghdarJoz = dMeghdarJoz,
                                                LevelNumber = 1,
                                            };
                                            await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                            _context.SaveChanges();
                                            //RizMetreUserses.Save();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                case 17:
                    {

                        ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            blnCheckAgain = blnCheckAgain,
                            RMIdForEdit = SingleRMIdForAdd
                        };


                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        ConditionCaseCommonCaseResultDto ResultCCC = await CCC.Case17(RequestCase1, _context);

                        blnCheckIsExistWarning = ResultCCC.blnCheckIsExistWarning;
                        strWarning = ResultCCC.strWarning;
                        Status = ResultCCC.blnStatus;

                        blnResult1.Add(ResultCCC.blnStatus);
                        blnResult = ResultCCC.blnStatus;
                        break;

                        //string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";

                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();
                        //string strUseItem = UseItemForAdd;
                        //string strForItem = strItemFBShomareh;


                        ////long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                        //clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                        //                 .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                        //                     x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                        //bool blnCheckSave = false;
                        //Guid currentItemsHasConditionAddedToFBId = new Guid();
                        //if (currentItemsHasConditionAddedToFBs == null)
                        //{
                        //    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                        //    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                        //    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                        //    ItemsHasConditionAddedToFB.Meghdar = 0;
                        //    ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;

                        //    try
                        //    {
                        //        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                        //        await _context.SaveChangesAsync();
                        //        blnCheckSave = true;
                        //        currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                        //    }
                        //    catch (Exception)
                        //    {
                        //        blnCheckSave = false;
                        //    }
                        //}
                        //else
                        //{
                        //    blnCheckSave = true;
                        //    currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                        //}


                        //if (blnCheckSave)
                        //{
                        //    string[] strFinalWorkingBahayeVahedZarib = FinalWorking.Split(',');
                        //    string[] strFinalWorkingSplit1 = FinalWorking.Split(',')[0].Split('_');
                        //    int intDivided = strFinalWorkingSplit1.Length > 1 ? int.Parse(strFinalWorkingSplit1[1]) : 1;
                        //    string[] strFinalWorkingSplit = strFinalWorkingSplit1[0].Split('-');

                        //    List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();
                        //    string strItemShomareh = AddedItems.Trim() + strCharacterPlus.Trim();


                        //    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                        //    Guid intFBId = new Guid();
                        //    if (varFBUser == null)
                        //    {
                        //        clsFB FB = new clsFB();
                        //        FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                        //        FB.Shomareh = AddedItems.Trim() + strCharacterPlus.Trim();
                        //        FB.BahayeVahedZarib = strFinalWorkingBahayeVahedZarib.Length > 1 ? decimal.Parse(strFinalWorkingBahayeVahedZarib[1]) : 0;
                        //        FB.BahayeVahedSharh = DesOfAddingItems;
                        //        FB.NoeFBId = NoeFBId;
                        //        _context.FBs.Add(FB);
                        //        await _context.SaveChangesAsync();
                        //        intFBId = FB.ID;
                        //    }
                        //    else
                        //        intFBId = varFBUser.ID;

                        //    string CharForReplace = "";
                        //    string strTAEForReoplace = "";

                        //    bool blnCheckEqual = false;
                        //    string[] strFinalWorkingSplitEqual = strFinalWorkingSplit[0].Split('=');
                        //    if (strFinalWorkingSplitEqual.Length > 1)
                        //    {
                        //        CharForReplace = strFinalWorkingSplitEqual[0].Trim();
                        //        strTAEForReoplace = strFinalWorkingSplitEqual[1].ToString().Trim();
                        //        blnCheckEqual = true;
                        //    }


                        //    if (blnCheckEqual)
                        //    {
                        //        string strConditionOp = Condition.Replace(CharForReplace, RM.Ertefa.ToString());
                        //        StringToFormula stringToFormula = new StringToFormula();
                        //        bool blnCheck = stringToFormula.RelationalExpression2(strConditionOp);
                        //        if (blnCheck)
                        //        {
                        //            decimal? dErtefa = decimal.Parse(strTAEForReoplace);

                        //            decimal? Tedad = RM.Tedad == 0 ? 1 : RM.Tedad;
                        //            decimal? Tool = RM.Tool;
                        //            decimal? Arz = RM.Arz;
                        //            decimal? Vazn = RM.Vazn;

                        //            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //            RizMetreUserses.Shomareh = RM.Shomareh;
                        //            ShomareNew++;
                        //            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //            RizMetreUserses.Sharh = RM.Sharh;
                        //            RizMetreUserses.Tedad = Tedad;
                        //            RizMetreUserses.Tool = Tool;
                        //            RizMetreUserses.Arz = Arz;
                        //            RizMetreUserses.Ertefa = dErtefa;
                        //            RizMetreUserses.Vazn = Vazn;
                        //            RizMetreUserses.Des = RM.Des;

                        //            RizMetreUserses.FBId = intFBId;
                        //            RizMetreUserses.OperationsOfHamlId = 1;
                        //            RizMetreUserses.Type = "2";
                        //            RizMetreUserses.ForItem = strForItem;
                        //            RizMetreUserses.UseItem = strUseItem;
                        //            RizMetreUserses.LevelNumber = LevelNumber;
                        //            RizMetreUserses.InsertDateTime = Now;

                        //            //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                        //            //RizMetreUserses.ConditionContextId = ConditionContextId;

                        //            /////محاسبه مقدار جزء
                        //            decimal? dMeghdarJoz = null;
                        //            if (Tedad == null && Tool == null && dErtefa == null && Arz == null && Vazn == null)
                        //                dMeghdarJoz = null;
                        //            else
                        //                dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                        //                (Arz == null ? 1 : Arz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                        //            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //            _context.RizMetreUserses.Add(RizMetreUserses);

                        //            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                new clsItemsHasConditionAddedToFBRizMetre
                        //                {
                        //                    ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                    RizMetreId = RizMetreUserses.ID,
                        //                    BaseRizMetreId = RM.ID
                        //                });


                        //            //dSumAllRizMetre += dMeghdarJoz ?? 0;

                        //            //Start///////////////////////
                        //            ////////////حمل///////////////
                        //            //////////////////////////////
                        //            ///آیتم های حمل درج میگردد
                        //            ///

                        //            SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //            {
                        //                BarAvordUserId = BarAvordId,
                        //                Year = Year,
                        //                ItemFBShomareh = strItemShomareh,
                        //                NoeFBId = NoeFBId,
                        //                //BarAvordHamlId = gBarAvordHamlId,
                        //                Shomareh = RizMetreUserses.Shomareh,
                        //                MeghdarJoz = dMeghdarJoz,
                        //                LevelNumber = 1,
                        //            };
                        //            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //        }
                        //    }
                        //    else
                        //    {


                        //        decimal? dErtefa = null;
                        //        decimal? dTool = null;
                        //        if (strFinalWorkingSplit[0] == "z")
                        //        {
                        //            CharForReplace = "z";
                        //            strTAEForReoplace = RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "";
                        //            dErtefa = intDivided;
                        //            dTool = RM.Tool;
                        //        }
                        //        else if (strFinalWorkingSplit[0] == "x")
                        //        {
                        //            CharForReplace = "x";
                        //            strTAEForReoplace = RM.Tool != null ? RM.Tool.Value.ToString().Trim() : "";
                        //            dErtefa = RM.Ertefa;
                        //            dTool = intDivided;
                        //        }
                        //        string strConditionOp = Condition.Replace(CharForReplace, strTAEForReoplace);
                        //        StringToFormula stringToFormula = new StringToFormula();
                        //        bool blnCheck = stringToFormula.RelationalExpression2(strConditionOp);
                        //        if (blnCheck)
                        //        {
                        //            if (FinalWorking != "")
                        //            {



                        //                string strFinalWorking11 = strFinalWorkingSplit1[0].Replace(CharForReplace, strTAEForReoplace);

                        //                int intReapetCount = (int.Parse(Math.Floor(stringToFormula.Eval(strFinalWorking11)).ToString("n0")) / intDivided);

                        //                //به تعداد intReapetCount
                        //                //رکورد با ارتفاع intDivided درج می گردد
                        //                decimal? Tedad = RM.Tedad == 0 ? 1 : RM.Tedad;
                        //                //decimal? Tool = RM.Tool;
                        //                decimal? Arz = RM.Arz;
                        //                decimal? Vazn = 1;
                        //                for (int i = 0; i < intReapetCount; i++)
                        //                {
                        //                    //decimal dErtefa = intDivided;
                        //                    clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                        //                    RizMetreUserses1.Shomareh = RM.Shomareh;
                        //                    ShomareNew++;
                        //                    RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                        //                    RizMetreUserses1.Sharh = RM.Sharh;
                        //                    RizMetreUserses1.Tedad = Tedad;
                        //                    RizMetreUserses1.Tool = dTool;
                        //                    RizMetreUserses1.Arz = Arz;
                        //                    RizMetreUserses1.Ertefa = dErtefa;
                        //                    RizMetreUserses1.Vazn = Vazn++;
                        //                    RizMetreUserses1.Des = RM.Des;

                        //                    RizMetreUserses1.FBId = intFBId;
                        //                    RizMetreUserses1.OperationsOfHamlId = 1;
                        //                    RizMetreUserses1.Type = "2";
                        //                    RizMetreUserses1.ForItem = strForItem;
                        //                    RizMetreUserses1.UseItem = strUseItem;
                        //                    RizMetreUserses1.LevelNumber = LevelNumber;
                        //                    RizMetreUserses1.InsertDateTime = Now;

                        //                    //RizMetreUserses1.ConditionContextRel = ConditionContextRel;
                        //                    //RizMetreUserses1.ConditionContextId = ConditionContextId;

                        //                    ///محاسبه مقدار جزء
                        //                    decimal? dMeghdarJoz1 = null;
                        //                    if (Tedad == null && dTool == null && Arz == null && Vazn == null)
                        //                        dMeghdarJoz1 = null;
                        //                    else
                        //                        dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (dTool == null ? 1 : dTool.Value) *
                        //                        (Arz == null ? 1 : Arz.Value) * dErtefa * (Vazn == null ? 1 : Vazn.Value);
                        //                    RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                        //                    _context.RizMetreUserses.Add(RizMetreUserses1);


                        //                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                        new clsItemsHasConditionAddedToFBRizMetre
                        //                        {
                        //                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                            RizMetreId = RizMetreUserses1.ID,
                        //                            BaseRizMetreId = RM.ID

                        //                        });

                        //                }
                        //                int intRemain = intReapetCount * intDivided;

                        //                string strFinalWorking1 = strFinalWorkingSplit1[0].Replace(CharForReplace, strTAEForReoplace);

                        //                decimal? dLastErtefa = RM.Ertefa;
                        //                decimal? dLastTool = RM.Tool;
                        //                //رکورد آخری هم بنا به شرط فاینل ارتفاع یا طول درج میگردد
                        //                if (strFinalWorkingSplit[0] == "z")
                        //                {
                        //                    dLastErtefa = decimal.Parse(stringToFormula.Eval(strFinalWorking1).ToString("0.##"));
                        //                    dLastErtefa -= intRemain;
                        //                    if (dLastErtefa > 0)
                        //                    {

                        //                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //                        RizMetreUserses.Shomareh = RM.Shomareh;
                        //                        ShomareNew++;
                        //                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //                        RizMetreUserses.Sharh = RM.Sharh;
                        //                        RizMetreUserses.Tedad = Tedad;
                        //                        RizMetreUserses.Tool = dLastTool;
                        //                        RizMetreUserses.Arz = Arz;
                        //                        RizMetreUserses.Ertefa = dLastErtefa;
                        //                        RizMetreUserses.Vazn = Vazn;
                        //                        RizMetreUserses.Des = RM.Des;

                        //                        RizMetreUserses.FBId = intFBId;
                        //                        RizMetreUserses.OperationsOfHamlId = 1;
                        //                        RizMetreUserses.Type = "2";
                        //                        RizMetreUserses.ForItem = strForItem;
                        //                        RizMetreUserses.UseItem = strUseItem;
                        //                        RizMetreUserses.LevelNumber = LevelNumber;
                        //                        RizMetreUserses.InsertDateTime = Now;

                        //                        //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                        //                        //RizMetreUserses.ConditionContextId = ConditionContextId;

                        //                        ///محاسبه مقدار جزء
                        //                        decimal? dMeghdarJoz = null;
                        //                        if (Tedad == null && dLastTool == null && dLastErtefa == null && Arz == null && Vazn == null)
                        //                            dMeghdarJoz = null;
                        //                        else
                        //                            dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (dLastTool == null ? 1 : dLastTool.Value) *
                        //                            (Arz == null ? 1 : Arz.Value) * (dLastErtefa == null ? 1 : dLastErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                        //                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //                        _context.RizMetreUserses.Add(RizMetreUserses);

                        //                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                            new clsItemsHasConditionAddedToFBRizMetre
                        //                            {
                        //                                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                                RizMetreId = RizMetreUserses.ID,
                        //                                BaseRizMetreId = RM.ID

                        //                            });


                        //                        //Start///////////////////////
                        //                        ////////////حمل///////////////
                        //                        //////////////////////////////
                        //                        ///آیتم های حمل درج میگردد
                        //                        ///

                        //                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //                        {
                        //                            BarAvordUserId = ItemHasCon.BarAvordId,
                        //                            Year = Year,
                        //                            ItemFBShomareh = AddedItems,
                        //                            NoeFBId = NoeFBId,
                        //                            //BarAvordHamlId = gBarAvordHamlId,
                        //                            Shomareh = RizMetreUserses.Shomareh,
                        //                            MeghdarJoz = dMeghdarJoz,
                        //                            LevelNumber = 1,
                        //                        };
                        //                        await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //                    }
                        //                }
                        //                else if (strFinalWorkingSplit[0] == "x")
                        //                {
                        //                    dLastTool = decimal.Parse(stringToFormula.Eval(strFinalWorking1).ToString("0.##"));
                        //                    dLastTool -= intRemain;
                        //                    if (dLastTool > 0)
                        //                    {

                        //                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //                        RizMetreUserses.Shomareh = RM.Shomareh;
                        //                        ShomareNew++;
                        //                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //                        RizMetreUserses.Sharh = RM.Sharh;
                        //                        RizMetreUserses.Tedad = Tedad;
                        //                        RizMetreUserses.Tool = dLastTool;
                        //                        RizMetreUserses.Arz = Arz;
                        //                        RizMetreUserses.Ertefa = dLastErtefa;
                        //                        RizMetreUserses.Vazn = Vazn;
                        //                        RizMetreUserses.Des = RM.Des;

                        //                        RizMetreUserses.FBId = intFBId;
                        //                        RizMetreUserses.OperationsOfHamlId = 1;
                        //                        RizMetreUserses.Type = "2";
                        //                        RizMetreUserses.ForItem = strForItem;
                        //                        RizMetreUserses.UseItem = strUseItem;
                        //                        RizMetreUserses.LevelNumber = LevelNumber;
                        //                        RizMetreUserses.InsertDateTime = Now;

                        //                        //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                        //                        //RizMetreUserses.ConditionContextId = ConditionContextId;

                        //                        ///محاسبه مقدار جزء
                        //                        decimal? dMeghdarJoz = null;
                        //                        if (Tedad == null && dLastTool == null && dLastErtefa == null && Arz == null && Vazn == null)
                        //                            dMeghdarJoz = null;
                        //                        else
                        //                            dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (dLastTool == null ? 1 : dLastTool.Value) *
                        //                            (Arz == null ? 1 : Arz.Value) * (dLastErtefa == null ? 1 : dLastErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                        //                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //                        _context.RizMetreUserses.Add(RizMetreUserses);

                        //                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                        new clsItemsHasConditionAddedToFBRizMetre
                        //                        {
                        //                            ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                            RizMetreId = RizMetreUserses.ID,
                        //                            BaseRizMetreId = RM.ID

                        //                        });


                        //                        //Start///////////////////////
                        //                        ////////////حمل///////////////
                        //                        //////////////////////////////
                        //                        ///آیتم های حمل درج میگردد
                        //                        ///

                        //                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //                        {
                        //                            BarAvordUserId = ItemHasCon.BarAvordId,
                        //                            Year = Year,
                        //                            ItemFBShomareh = AddedItems,
                        //                            NoeFBId = NoeFBId,
                        //                            //BarAvordHamlId = gBarAvordHamlId,
                        //                            Shomareh = RizMetreUserses.Shomareh,
                        //                            MeghdarJoz = dMeghdarJoz,
                        //                            LevelNumber = 1,
                        //                        };
                        //                        await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //                    }
                        //                }
                        //            }
                        //            //await _context.SaveChangesAsync();
                        //            //RizMetreUserses.Save();
                        //            //}
                        //        }
                        //    }
                        //}
                        //break;
                    }
                case 18:
                    {

                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        string strCondition = Condition;
                        string strFinalWorking = FinalWorking;
                        string strAddedItems = AddedItems;
                        string strUseItem = UseItemForAdd;
                        string strForItem = strItemFBShomareh;
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            //blnCheckIsExistErrors = "false";
                            //blnCheckIsExistWarning = false;

                            List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();

                            if (strFinalWorking != "")
                            {
                                string[] strFinalWorkingSplit = strFinalWorking.Split('-');
                                int intReapetCount = (int.Parse(strFinalWorkingSplit[1]) - 20) / 5;
                                string strItemShomareh = strAddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                                clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                Guid intFBId = new Guid();
                                if (varFBUser == null)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    FB.Shomareh = strAddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                                    FB.BahayeVahedZarib = 0;
                                    FB.NoeFBId = NoeFBId;
                                    _context.FBs.Add(FB);
                                    _context.SaveChanges();
                                    intFBId = FB.ID;
                                }
                                else
                                    intFBId = varFBUser.ID;



                                //به تعداد intReapetCount
                                //رکورد با ارتفاع 5 درج می گردد
                                decimal? Tedad = RM.Tedad == 0 ? 1 : RM.Tedad;
                                decimal? Tool = RM.Tool;
                                decimal? Arz = RM.Arz;
                                decimal? Vazn = 1;
                                for (int i = 0; i < intReapetCount; i++)
                                {
                                    decimal dErtefa = 5;
                                    clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                                    RizMetreUserses1.Shomareh = RM.Shomareh;
                                    ShomareNew++;
                                    RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUserses1.Sharh = RM.Sharh;
                                    RizMetreUserses1.Tedad = Tedad;
                                    RizMetreUserses1.Tool = Tool;
                                    RizMetreUserses1.Arz = Arz;
                                    RizMetreUserses1.Ertefa = dErtefa;
                                    RizMetreUserses1.Vazn = Vazn++;
                                    RizMetreUserses1.Des = RM.Des;

                                    RizMetreUserses1.FBId = intFBId;
                                    RizMetreUserses1.OperationsOfHamlId = 1;
                                    RizMetreUserses1.Type = "2";
                                    RizMetreUserses1.ForItem = strForItem;
                                    RizMetreUserses1.UseItem = strUseItem;
                                    RizMetreUserses1.LevelNumber = LevelNumber;
                                    RizMetreUserses1.InsertDateTime = Now;

                                    //RizMetreUserses1.ConditionContextRel = ConditionContextRel;
                                    //RizMetreUserses1.ConditionContextId = ConditionContextId;

                                    ///محاسبه مقدار جزء
                                    decimal? dMeghdarJoz1 = null;
                                    if (Tedad == null && Tool == null && Arz == null && Vazn == null)
                                        dMeghdarJoz1 = null;
                                    else
                                        dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                        (Arz == null ? 1 : Arz.Value) * dErtefa * (Vazn == null ? 1 : Vazn.Value);
                                    RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                                    _context.RizMetreUserses.Add(RizMetreUserses1);
                                }


                                strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());

                                //رکورد آخری هم بنا به شرط فاینل ارتفاع درج میگردد
                                decimal Ertefa = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));

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
                                RizMetreUserses.Des = RM.Des;

                                RizMetreUserses.FBId = intFBId;
                                RizMetreUserses.OperationsOfHamlId = 1;
                                RizMetreUserses.Type = "2";
                                RizMetreUserses.ForItem = strForItem;
                                RizMetreUserses.UseItem = strUseItem;
                                RizMetreUserses.LevelNumber = LevelNumber;
                                RizMetreUserses.InsertDateTime = Now;

                                //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                //RizMetreUserses.ConditionContextId = ConditionContextId;

                                ///محاسبه مقدار جزء
                                decimal? dMeghdarJoz = null;
                                if (Tedad == null && Tool == null && Arz == null && Vazn == null)
                                    dMeghdarJoz = null;
                                else
                                    dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                    (Arz == null ? 1 : Arz.Value) * Ertefa * (Vazn == null ? 1 : Vazn.Value);
                                RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                _context.RizMetreUserses.Add(RizMetreUserses);


                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///

                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = AddedItems,
                                    NoeFBId = NoeFBId,
                                    //BarAvordHamlId = gBarAvordHamlId,
                                    Shomareh = RizMetreUserses.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                _context.SaveChanges();
                            }
                            //}
                            //}
                            //else
                            //{
                            //    if (blnCheckIsExistErrors != "false")
                            //    {
                            //        blnCheckIsExistErrors = "true";
                            //        blnCheckIsExistWarning = true;
                            //    }
                            //}
                        }
                        break;
                    }
                case 19:
                    {
                        //string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";

                        string[] strCondition2Condition = Condition.Trim().Split(',');
                        string strConditionY = strCondition2Condition[0].Trim();
                        string strConditionZ = strCondition2Condition[1].Trim();

                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();
                        string strUseItem = UseItemForAdd;
                        string strForItem = strItemFBShomareh;

                        //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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
                            ItemsHasConditionAddedToFB.Meghdar = 0;
                            ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;

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
                            List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();
                            string strItemShomareh = AddedItems.Trim() + strCharacterPlus.Trim();
                            string strDesOfAddingItems = DesOfAddingItems;//.Replace("x", Meghdar.ToString().Trim()).Replace("y", Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim()).Replace("z", (dPercent * 100).ToString("0.##")).Trim();

                            string strConditionOpY = strConditionY.Replace("y", RM.Arz != null ? RM.Arz.Value.ToString().Trim() : "");
                            string strConditionOpZ = strConditionZ.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");

                            StringToFormula stringToFormula = new StringToFormula();
                            bool blnCheckY = stringToFormula.RelationalExpression2(strConditionOpY);
                            bool blnCheckZ = stringToFormula.RelationalExpression2(strConditionOpZ);
                            if (blnCheckY && blnCheckZ)
                            {
                                if (FinalWorking != "")
                                {
                                    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                    Guid intFBId = new Guid();
                                    if (varFBUser == null)
                                    {
                                        clsFB FB = new clsFB();
                                        FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        FB.Shomareh = strItemShomareh; //AddedItems.Trim();
                                        FB.BahayeVahedZarib = FinalWorking.Length > 1 ? decimal.Parse(FinalWorking) : 0;
                                        FB.BahayeVahedSharh = strDesOfAddingItems;
                                        FB.NoeFBId = NoeFBId;
                                        _context.FBs.Add(FB);
                                        _context.SaveChanges();
                                        intFBId = FB.ID;
                                    }
                                    else
                                        intFBId = varFBUser.ID;

                                    decimal? Tedad = RM.Tedad;
                                    decimal? Tool = RM.Tool;
                                    decimal? Arz = RM.Arz;
                                    decimal? Ertefa = RM.Ertefa;
                                    decimal? Vazn = RM.Vazn;

                                    clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                                    RizMetreUserses1.Shomareh = RM.Shomareh;
                                    ShomareNew++;
                                    RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUserses1.Sharh = RM.Sharh;
                                    RizMetreUserses1.Tedad = Tedad;
                                    RizMetreUserses1.Tool = Tool;
                                    RizMetreUserses1.Arz = Arz;
                                    RizMetreUserses1.Ertefa = Ertefa;
                                    RizMetreUserses1.Vazn = Vazn;

                                    RizMetreUserses1.Des = RM.Des;

                                    RizMetreUserses1.FBId = intFBId;
                                    RizMetreUserses1.OperationsOfHamlId = 1;
                                    RizMetreUserses1.Type = "2";
                                    RizMetreUserses1.ForItem = strForItem;
                                    RizMetreUserses1.UseItem = strUseItem;
                                    RizMetreUserses1.LevelNumber = LevelNumber;
                                    RizMetreUserses1.InsertDateTime = Now;

                                    ///محاسبه مقدار جزء
                                    decimal? dMeghdarJoz1 = null;
                                    if (Tedad == null && Tool == null && Arz == null && Vazn == null && Ertefa == null)
                                        dMeghdarJoz1 = null;
                                    else
                                        dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                        (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                    RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                                    _context.RizMetreUserses.Add(RizMetreUserses1);


                                    //Start///////////////////////
                                    ////////////حمل///////////////
                                    //////////////////////////////
                                    ///آیتم های حمل درج میگردد
                                    ///

                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                    {
                                        BarAvordUserId = ItemHasCon.BarAvordId,
                                        Year = Year,
                                        ItemFBShomareh = AddedItems,
                                        NoeFBId = NoeFBId,
                                        //BarAvordHamlId = gBarAvordHamlId,
                                        Shomareh = RizMetreUserses1.Shomareh,
                                        MeghdarJoz = dMeghdarJoz1,
                                        LevelNumber = 1,
                                    };
                                    await HamlCommon.SaveHaml(requestSaveHaml, _context);

                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                        break;
                    }

                default:
                    break;
            }


            //if (blnCheckIsExistErrors == "true")
            //{
            //    return new JsonResult("CI");//CheckInput
            //                                //return new JsonResult("NOK_" + strErrors);
            //}
            //else
            //{
                if (blnCheckIsExistWarning)
                {
                    return false;
                }
                else
                {
                    //await _context.SaveChangesAsync();
                    return blnResult;
                }

                // return "OK_";
            //}
        }
        //public async void fnCheckOperationConditionForDelete(ApplicationDbContext _context, ItemsAddingToFBForCheckOperationDto itemsAddingToFBForCheckOperation,
        //    List<ItemsFieldsDto> ItemsFields, ItemsHasConditionConditionContextForCheckOperationDto ItemHasCon, Guid FBId, int Year, clsRizMetreUsers RizMetre, NoeFehrestBaha NoeFBId)
        //{
        //    bool blnCheckAgain = true;
        //    string Condition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition.Trim() : "";
        //    string FinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking.Trim() : "";
        //    string AddedItems = itemsAddingToFBForCheckOperation.AddedItems != null ? itemsAddingToFBForCheckOperation.AddedItems.Substring(0, 6).Trim() : "";
        //    string DesOfAddingItems = itemsAddingToFBForCheckOperation.DesOfAddingItems != null ? itemsAddingToFBForCheckOperation.DesOfAddingItems.Trim() : "";
        //    string UseItemForAdd = itemsAddingToFBForCheckOperation.UseItemForAdd != null ? itemsAddingToFBForCheckOperation.UseItemForAdd.Trim() : "";

        //    Guid guBAId = ItemHasCon.BarAvordId;
        //    switch (itemsAddingToFBForCheckOperation.ConditionType)
        //    {
        //        case 1:
        //            {
        //                string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
        //                string strCondition = Condition;
        //                string strFinalWorking = FinalWorking;
        //                string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
        //                    decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

        //                    string strAddedItems = AddedItems;
        //                    clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strCharacterPlus);

        //                    Guid intFBId = new Guid();

        //                    if (varFBUsersAdded != null)
        //                    {
        //                        intFBId = varFBUsersAdded.ID;

        //                        string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                        clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                        if (RizMetreUsersCurrent != null)
        //                        {
        //                            if (lstRizMetreBase.Count == 1)
        //                            {
        //                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                if (ItemsHasConditionAddedToFB != null)
        //                                {
        //                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                }
        //                            }

        //                            _context.RizMetreUserses.Remove(RizMetreUsersCurrent);


        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///
        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = varFBUsersAdded.Shomareh,
        //                                RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                        }
        //                    }

        //                }
        //                break;
        //            }
        //        case 2:
        //            {
        //                string strShomarehAdd = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
        //                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);//.ToList();
        //                Guid intFBId = new Guid();

        //                if (varFBUser != null)
        //                {
        //                    intFBId = varFBUser.ID;


        //                    string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6); //DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                    List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                    clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                    List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                    if (RizMetreUsersCurrent != null)
        //                    {
        //                        if (lstRizMetreBase.Count == 1)
        //                        {
        //                            clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                     && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                            if (ItemsHasConditionAddedToFB != null)
        //                            {
        //                                _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                            }
        //                        }
        //                        _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///
        //                        DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                        {
        //                            BarAvordId = ItemHasCon.BarAvordId,
        //                            FBShomareh = varFBUser.Shomareh,
        //                            RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                            NoeFBId = NoeFBId
        //                        };
        //                        await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                        /////////////
        //                        //////////////
        //                        //////////////

        //                    }
        //                    //List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = _context.ItemsRelatedToItemHamls
        //                    //       .Where(x => x.ItemFB.Trim() == strShomarehAdd && x.Year == Year).ToList();

        //                    //List<string> strItemHamlFB = new List<string>();
        //                    //if (lstItemsRelatedToItemHaml.Count != 0)
        //                    //{
        //                    //    strItemHamlFB = lstItemsRelatedToItemHaml.Select(x => x.ItemHamlFB.Trim()).ToList();
        //                    //    List<clsFB> lstFbHaml = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && strItemHamlFB.Contains(x.Shomareh)).ToList();
        //                    //    if (lstFbHaml.Count != 0)
        //                    //    {
        //                    //        List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => lstFbHaml.Select(x => x.ID).Contains(x.FBId)
        //                    //            && x.ForItem == strShomareh1.Trim() && x.Type == "3" && x.Shomareh == RizMetre.Shomareh).ToList();
        //                    //        _context.RizMetreUserses.RemoveRange(lstRMForDelForHaml);

        //                    //    }
        //                    //}
        //                }
        //                break;
        //            }
        //        case 3:
        //            {
        //                string FinalWorking1 = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "0";
        //                decimal dPercent = decimal.Parse(FinalWorking1);
        //                string strAddedItems = AddedItems;
        //                string strStatus = dPercent > 0 ? "B" : "e";
        //                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strStatus);

        //                Guid intFBId = new Guid();

        //                if (varFBUser != null)
        //                {
        //                    intFBId = varFBUser.ID;

        //                    string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);// DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                    List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                    clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                    List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                    if (RizMetreUsersCurrent != null)
        //                    {
        //                        if (lstRizMetreBase.Count == 1)
        //                        {
        //                            clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                     && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                            if (ItemsHasConditionAddedToFB != null)
        //                            {
        //                                _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                            }
        //                        }

        //                        _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///
        //                        DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                        {
        //                            BarAvordId = ItemHasCon.BarAvordId,
        //                            FBShomareh = varFBUser.Shomareh,
        //                            RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                            NoeFBId = NoeFBId
        //                        };
        //                        await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                        /////////////
        //                        //////////////
        //                        //////////////
        //                        ///
        //                    }
        //                }
        //                break;
        //            }
        //        case 4:
        //            {
        //                string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
        //                string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                if (blnCheck)
        //                {

        //                    string strShomarehAdd = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
        //                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);

        //                    Guid intFBId = new Guid();

        //                    if (varFBUser != null)
        //                    {
        //                        intFBId = varFBUser.ID;

        //                        DataTable DtRizMetreUsers = new DataTable();
        //                        string strForItem = ItemHasCon.FBShomareh;
        //                        string strUseItem = "";
        //                        string strItemFBShomareh = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strForItem && x.UseItem == strUseItem && x.Type == "2").ToList();
        //                        clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                        if (RizMetreUsersCurrent != null)
        //                        {
        //                            if (lstRizMetreBase.Count == 1)
        //                            {
        //                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                if (ItemsHasConditionAddedToFB != null)
        //                                {
        //                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                }
        //                            }
        //                            _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///
        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = varFBUser.Shomareh,
        //                                RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                            ///
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //        case 5:
        //            {
        //                string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    string strAddedItems = AddedItems;
        //                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
        //                    Guid intFBId = new Guid();
        //                    if (varFBUser != null)
        //                    {
        //                        intFBId = varFBUser.ID;

        //                        string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                        clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                        if (RizMetreUsersCurrent != null)
        //                        {
        //                            if (lstRizMetreBase.Count == 1)
        //                            {
        //                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                if (ItemsHasConditionAddedToFB != null)
        //                                {
        //                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                }
        //                            }
        //                            _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///
        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = varFBUser.Shomareh,
        //                                RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                            ///
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //        case 6:
        //            {
        //                string strCondition = Condition;
        //                string strAddedItems = AddedItems;
        //                string strFinalWorking = FinalWorking;
        //                DataTable DtRizMetreUserses = new DataTable();
        //                string strForItem = "";
        //                string strUseItem = "";
        //                string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

        //                Guid guFBId = FBId;
        //                strUseItem = strItemFBShomareh;
        //                strForItem = UseItemForAdd;

        //                string strConditionOp = strCondition.Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();

        //                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
        //                    long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());
        //                    string strItemShomareh = AddedItems;
        //                    strAddedItems = strAddedItems.Trim();
        //                    clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

        //                    Guid intFBId = new Guid();
        //                    if (varFBUsersAdded != null)
        //                    {
        //                        intFBId = varFBUsersAdded.ID;

        //                        long lngShomareh = RizMetre.Shomareh;

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh).OrderBy(x => x.Shomareh).ToList();
        //                        clsRizMetreUsers? RizMetreUsers = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == lngShomareh);

        //                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                        if (RizMetreUsers != null)
        //                        {
        //                            if (lstRizMetreBase.Count == 1)
        //                            {
        //                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                if (ItemsHasConditionAddedToFB != null)
        //                                {
        //                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                }
        //                            }
        //                            _context.RizMetreUserses.Remove(RizMetreUsers);

        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///

        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = varFBUsersAdded.Shomareh,
        //                                RMShomareh = RizMetreUsers.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                            ///
        //                        }


        //                    }

        //                }
        //                break;
        //            }
        //        case 7:
        //            {
        //                string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                string strConditionOp = strCondition.Replace("x", RizMetre.Arz.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    string strAddedItems = AddedItems;
        //                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
        //                    Guid intFBId = new Guid();
        //                    if (varFBUser != null)
        //                    {
        //                        intFBId = varFBUser.ID;

        //                        string strShomareh1 = ItemHasCon.FBShomareh;

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                        clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                        if (RizMetreUsersCurrent != null)
        //                        {
        //                            if (lstRizMetreBase.Count == 1)
        //                            {
        //                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                if (ItemsHasConditionAddedToFB != null)
        //                                {
        //                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                }
        //                            }
        //                            _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///

        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = varFBUser.Shomareh,
        //                                RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                            ///

        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //        case 8:
        //            {
        //                string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                StringToFormula StringToFormula = new StringToFormula();
        //                string strAddedItems = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
        //                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);

        //                Guid intFBId = new Guid();
        //                if (varFBUser != null)
        //                {
        //                    intFBId = varFBUser.ID;

        //                    string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                    List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                    clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                    List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                    if (RizMetreUsersCurrent != null)
        //                    {
        //                        if (lstRizMetreBase.Count == 1)
        //                        {
        //                            clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                     && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                            if (ItemsHasConditionAddedToFB != null)
        //                            {
        //                                _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                            }
        //                        }

        //                        _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///

        //                        DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                        {
        //                            BarAvordId = ItemHasCon.BarAvordId,
        //                            FBShomareh = varFBUser.Shomareh,
        //                            RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                            NoeFBId = NoeFBId
        //                        };
        //                        await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                        /////////////
        //                        //////////////
        //                        //////////////
        //                        ///
        //                    }
        //                }

        //                break;
        //            }
        //        case 9:
        //            {
        //                string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                string strAddedItems = AddedItems;
        //                string strConditionOp = strCondition.Replace("x", RizMetre.Tool.ToString().Trim()).Replace("y", RizMetre.Arz.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
        //                if (blnCheck)
        //                {

        //                    strAddedItems = strAddedItems.Trim();
        //                    clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

        //                    Guid intFBId = new Guid();
        //                    if (varFBUsersAdded != null)
        //                    {
        //                        intFBId = varFBUsersAdded.ID;

        //                        long lngShomareh = RizMetre.Shomareh; //long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh).ToList();
        //                        clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == lngShomareh);

        //                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                        if (RizMetreUsersCurrent != null)
        //                        {
        //                            if (lstRizMetreBase.Count == 1)
        //                            {
        //                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                if (ItemsHasConditionAddedToFB != null)
        //                                {
        //                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                }
        //                            }

        //                            _context.RizMetreUserses.Remove(RizMetreUsersCurrent);


        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///

        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = varFBUsersAdded.Shomareh,
        //                                RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                            ///
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //        case 10:
        //            {
        //                if (blnCheckAgain)
        //                {
        //                    string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                    string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
        //                    string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString());
        //                    StringToFormula StringToFormula = new StringToFormula();
        //                    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
        //                    if (blnCheck)
        //                    {
        //                        blnCheckAgain = false;

        //                        strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString());
        //                        decimal dZarib = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
        //                        if (dZarib == 0)
        //                        {
        //                            clsFB? varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == AddedItems).FirstOrDefault();
        //                            if (varFBUsersAdded != null)
        //                            {
        //                                Guid guFBUsersAddedId = varFBUsersAdded.ID;
        //                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
        //                                if (Fb != null)
        //                                {

        //                                    ///////////
        //                                    //حذف حمل//
        //                                    ///////////
        //                                    ///

        //                                    List<clsRizMetreUsers> lstRM = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ToList();
        //                                    foreach (var item in lstRM)
        //                                    {
        //                                        DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                                        {
        //                                            BarAvordId = ItemHasCon.BarAvordId,
        //                                            FBShomareh = varFBUsersAdded.Shomareh,
        //                                            RMShomareh = item.Shomareh,
        //                                            NoeFBId = NoeFBId
        //                                        };
        //                                        await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                                    }
        //                                    /////////////
        //                                    //////////////
        //                                    //////////////

        //                                    _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

        //                                    _context.FBs.Remove(Fb);
        //                                    _context.SaveChanges();
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {

        //                            dZarib = dZarib < 0 ? dZarib * -1 : dZarib;
        //                            var varBA = _context.BaravordUsers.Where(x => x.ID == ItemHasCon.BarAvordId).ToList();
        //                            DataTable DtBA = clsConvert.ToDataTable(varBA);

        //                            string strItemShomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
        //                            clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

        //                            Guid intFBId = new Guid();
        //                            if (FBUser != null)
        //                            {
        //                                intFBId = FBUser.ID;



        //                                string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                List<clsRizMetreUsers> lstRizMetreUsersCurrent =
        //                                    _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

        //                                clsRizMetreUsers? RizMetreUsersCurrent =
        //                                    lstRizMetreUsersCurrent.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2");

        //                                List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                                if (RizMetreUsersCurrent != null)
        //                                {
        //                                    if (lstRizMetreBase.Count == 1)
        //                                    {
        //                                        clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                            _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                                 && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                        if (ItemsHasConditionAddedToFB != null)
        //                                        {
        //                                            _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                        }
        //                                    }

        //                                    _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                                    ///////////
        //                                    //حذف حمل//
        //                                    ///////////
        //                                    ///
        //                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                                    {
        //                                        BarAvordId = ItemHasCon.BarAvordId,
        //                                        FBShomareh = FBUser.Shomareh,
        //                                        RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                        NoeFBId = NoeFBId
        //                                    };
        //                                    await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                                    /////////////
        //                                    //////////////
        //                                    //////////////
        //                                    ///
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //if (blnCheckIsExistErrors != "false")
        //                        //{
        //                        //    blnCheckIsExistErrors = "true";
        //                        //    strErrors = "عدد وارد شده در محدوده قابل قبولی نمیباشد "; //strCondition.Replace("x", "عدد وارد شده");
        //                        //}
        //                    }
        //                }
        //                break;
        //            }
        //        case 11:
        //            {
        //                string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
        //                string strItemShomareh = ItemHasCon.FBShomareh.Substring(0, 6) + strCharacterPlus;
        //                string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
        //                string strDesOfAddingItems = DesOfAddingItems;
        //                decimal dPercent = decimal.Parse(strFinalWorking);

        //                clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

        //                Guid intFBId = new Guid();
        //                if (FBUser != null)
        //                {
        //                    intFBId = FBUser.ID;

        //                    string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);

        //                    List<clsRizMetreUsers> lstRizMetreUsersCurrent =
        //                                    _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                    clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                    List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                    if (RizMetreUsersCurrent != null)
        //                    {
        //                        if (lstRizMetreBase.Count == 1)
        //                        {
        //                            clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                     && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                            if (ItemsHasConditionAddedToFB != null)
        //                            {
        //                                _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                            }
        //                        }
        //                        _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///
        //                        DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                        {
        //                            BarAvordId = ItemHasCon.BarAvordId,
        //                            FBShomareh = FBUser.Shomareh,
        //                            RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                            NoeFBId = NoeFBId
        //                        };
        //                        await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                        /////////////
        //                        //////////////
        //                        //////////////
        //                        ///
        //                    }
        //                }
        //                break;
        //            }
        //        case 12:
        //            {
        //                //string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding!=null? itemsAddingToFBForCheckOperation.FieldsAdding.Split(','):new string[0];
        //                string strCondition = Condition;
        //                string[] strConditionSplit = strCondition.Split("_");
        //                string strAddedItems = AddedItems;
        //                string strFinalWorking = FinalWorking;
        //                DataTable DtRizMetreUserses = new DataTable();
        //                string strForItem = "";
        //                string strUseItem = "";
        //                string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

        //                Guid guFBId = FBId;
        //                strUseItem = strItemFBShomareh;
        //                strForItem = UseItemForAdd;

        //                decimal? dErtefa1 = null;
        //                string strConditionOp = strConditionSplit[0].Replace("x", RizMetre.Tool == null ? "0" : RizMetre.Tool.Value.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck2 = true;
        //                if (strConditionSplit.Length == 2)
        //                {
        //                    string strConditionOp2 = strConditionSplit[1].Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
        //                    blnCheck2 = StringToFormula.RelationalExpression2(strConditionOp2);
        //                    ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
        //                    ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
        //                    ///Ertefa-3
        //                    ///
        //                    strFinalWorking = strFinalWorking.Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
        //                    dErtefa1 = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
        //                }
        //                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    if (blnCheck2)
        //                    {
        //                        string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
        //                        long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());

        //                        if (strFinalWorking != "")
        //                        {
        //                            strFinalWorking = strFinalWorking.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");
        //                            string strItemShomareh = AddedItems;
        //                            strAddedItems = strAddedItems.Trim();
        //                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);
        //                            //DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

        //                            Guid intFBId = new Guid();
        //                            if (varFBUsersAdded != null)
        //                            {
        //                                intFBId = varFBUsersAdded.ID;

        //                                long lngShomareh = RizMetre.Shomareh;

        //                                List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh).OrderBy(x => x.Shomareh).ToList();
        //                                clsRizMetreUsers? RizMetreUsers = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == lngShomareh);

        //                                List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                                if (RizMetreUsers != null)
        //                                {
        //                                    if (lstRizMetreBase.Count == 1)
        //                                    {
        //                                        clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                            _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                                 && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                        if (ItemsHasConditionAddedToFB != null)
        //                                        {
        //                                            _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                        }
        //                                    }
        //                                    _context.RizMetreUserses.Remove(RizMetreUsers);

        //                                    ///////////
        //                                    //حذف حمل//
        //                                    ///////////
        //                                    ///
        //                                    DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                                    {
        //                                        BarAvordId = ItemHasCon.BarAvordId,
        //                                        FBShomareh = varFBUsersAdded.Shomareh,
        //                                        RMShomareh = RizMetreUsers.Shomareh,
        //                                        NoeFBId = NoeFBId
        //                                    };
        //                                    await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                                    /////////////
        //                                    //////////////
        //                                    //////////////
        //                                    ///
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //        case 13:
        //            {
        //                decimal Meghdar = ItemHasCon.Meghdar;
        //                decimal Meghdar2 = ItemHasCon.Meghdar2;
        //                decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);

        //                Guid BarAvordId = ItemHasCon.BarAvordId;

        //                string[] strCondition = Condition.Trim().Split("_");
        //                string strCondition1 = strCondition[0];
        //                string strCondition2 = strCondition[1];
        //                string strConditionOp = strCondition1.Replace("x", Meghdar.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    string strConditionOp2 = strCondition2.Replace("x", Meghdar2.ToString().Trim());

        //                    StringToFormula StringToFormula2 = new StringToFormula();
        //                    bool blnCheck2 = StringToFormula2.RelationalExpression2(strConditionOp2);
        //                    if (blnCheck2)
        //                    {
        //                        string strItemShomareh = AddedItems;
        //                        clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh);

        //                        Guid intFBId = new Guid();
        //                        if (FBUser != null)
        //                        {
        //                            intFBId = FBUser.ID;

        //                            string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);
        //                            List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                            clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                            List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                            if (RizMetreUsersCurrent != null)
        //                            {
        //                                if (lstRizMetreBase.Count == 1)
        //                                {
        //                                    clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                        _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == BarAvordId
        //                                             && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                    if (ItemsHasConditionAddedToFB != null)
        //                                    {
        //                                        _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                    }
        //                                }
        //                                _context.RizMetreUserses.Remove(RizMetreUsersCurrent);


        //                                ///////////
        //                                //حذف حمل//
        //                                ///////////
        //                                ///
        //                                DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                                {
        //                                    BarAvordId = ItemHasCon.BarAvordId,
        //                                    FBShomareh = FBUser.Shomareh,
        //                                    RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                    NoeFBId = NoeFBId
        //                                };
        //                                await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                                /////////////
        //                                //////////////
        //                                //////////////
        //                                ///
        //                            }
        //                        }

        //                        break;
        //                    }
        //                }
        //                break;
        //            }
        //        case 14:
        //            {
        //                string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    string strAddedItems = AddedItems;
        //                    clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
        //                    Guid intFBId = new Guid();
        //                    if (varFBUser != null)
        //                    {
        //                        intFBId = varFBUser.ID;

        //                        string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                        clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                        if (RizMetreUsersCurrent != null)
        //                        {
        //                            if (lstRizMetreBase.Count == 1)
        //                            {
        //                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                                if (ItemsHasConditionAddedToFB != null)
        //                                {
        //                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                                }
        //                            }
        //                            _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///
        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = varFBUser.Shomareh,
        //                                RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                            ///
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //        case 15:
        //            {
        //                /////////////
        //                /////حذف قبلی ها
        //                /////////////
        //                string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";

        //                string strCondition = Condition;
        //                string[] strConditionSplit = strCondition.Split("_");

        //                string strItemFBShomareh = AddedItems;
        //                string strFinalWorking = FinalWorking;
        //                DataTable DtRizMetreUserses = new DataTable();
        //                string strUseItem = UseItemForAdd;
        //                string strForItem = strItemFBShomareh;

        //                string strItemShomareh1 = AddedItems + strCharacterPlus;

        //                clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh1).FirstOrDefault();

        //                Guid intFBId1 = new Guid();
        //                if (FBUser != null)
        //                {
        //                    intFBId1 = FBUser.ID;

        //                    string strShomareh1 = strItemFBShomareh.Substring(0, 6);
        //                    List<clsRizMetreUsers> lstRizMetreBase =
        //                                    _context.RizMetreUserses.Where(x => x.FBId == intFBId1).ToList();

        //                    List<clsRizMetreUsers> lstRizMetreUsersCurrent =
        //                                    _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                    clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                    clsRizMetreUsers? RizMetreUsersBaseCurrent = lstRizMetreBase.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);



        //                    List<Guid> lstRMIds = lstRizMetreBase.Select(x => x.ID).ToList();

        //                    if (RizMetreUsersCurrent != null)
        //                    {
        //                        if (lstRizMetreBase.Count == 1)
        //                        {
        //                            clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                     && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                            if (ItemsHasConditionAddedToFB != null)
        //                            {
        //                                _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                            }
        //                        }
        //                        _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///
        //                        DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                        {
        //                            BarAvordId = ItemHasCon.BarAvordId,
        //                            FBShomareh = FBUser.Shomareh,
        //                            RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                            NoeFBId = NoeFBId
        //                        };
        //                        await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                        /////////////
        //                        //////////////
        //                        //////////////
        //                        ///

        //                    }
        //                    if (RizMetreUsersBaseCurrent != null)
        //                    {
        //                        clsRizMetreUsersHistory? RMHistory = _context.RizMetreUsersHistories.FirstOrDefault(x => x.RizMetreUsersId == RizMetreUsersBaseCurrent.ID);

        //                        if (RMHistory != null)
        //                        {

        //                            _context.RizMetreUsersHistories.Remove(RMHistory);
        //                        }
        //                    }
        //                }

        //                _context.SaveChanges();
        //                break;
        //            }
        //        case 16:
        //            {
        //                string strAddedItems = AddedItems;
        //                clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
        //                Guid intFBId = new Guid();
        //                if (varFBUser != null)
        //                {
        //                    intFBId = varFBUser.ID;

        //                    string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                    List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
        //                    clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

        //                    List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

        //                    if (RizMetreUsersCurrent != null)
        //                    {
        //                        if (lstRizMetreBase.Count == 1)
        //                        {
        //                            clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
        //                                _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
        //                                     && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

        //                            if (ItemsHasConditionAddedToFB != null)
        //                            {
        //                                _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
        //                            }
        //                        }

        //                        _context.RizMetreUserses.Remove(RizMetreUsersCurrent);

        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///
        //                        DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                        {
        //                            BarAvordId = ItemHasCon.BarAvordId,
        //                            FBShomareh = varFBUser.Shomareh,
        //                            RMShomareh = RizMetreUsersCurrent.Shomareh,
        //                            NoeFBId = NoeFBId
        //                        };
        //                        await HamlCommon.DeleteHaml(deleteHaml, _context);

        //                        /////////////
        //                        //////////////
        //                        //////////////
        //                        ///
        //                    }
        //                }
        //                break;
        //            }
        //        case 17:
        //            {
        //                string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";

        //                string strErtefa = RizMetre.Ertefa != null ? RizMetre.Ertefa.ToString() : "0";

        //                string[] strFinalWorkingBahayeVahedZarib = FinalWorking.Split(',');
        //                string[] strFinalWorkingSplit1 = FinalWorking.Split(',')[0].Split('_');

        //                int intDivided = int.Parse(strFinalWorkingSplit1[1]);
        //                string[] strFinalWorkingSplit = strFinalWorkingSplit1[0].Split('-');

        //                string CharForReplace = "";
        //                string strTAEForReoplace = "";

        //                decimal? dErtefa = null;
        //                decimal? dTool = null;

        //                if (strFinalWorkingSplit[0] == "z")
        //                {
        //                    CharForReplace = "z";
        //                    strTAEForReoplace = RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "";
        //                    dErtefa = intDivided;
        //                    dTool = RizMetre.Tool;
        //                }
        //                else if (strFinalWorkingSplit[0] == "x")
        //                {
        //                    CharForReplace = "x";
        //                    strTAEForReoplace = RizMetre.Tool != null ? RizMetre.Tool.Value.ToString().Trim() : "";
        //                    dErtefa = RizMetre.Ertefa;
        //                    dTool = intDivided;
        //                }


        //                //string strFinalWorking1 = FinalWorking.Replace("z", strErtefa);
        //                StringToFormula StringToFormula1 = new StringToFormula();
        //                string strCondition1 = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
        //                string strConditionOp1 = strCondition1.Replace(CharForReplace, strTAEForReoplace);
        //                bool blnCheck1 = StringToFormula1.RelationalExpression(strConditionOp1);
        //                if (blnCheck1)
        //                {
        //                    string strItemShomareh1 = AddedItems + strCharacterPlus;

        //                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh1).FirstOrDefault();

        //                    Guid intFBId1 = new Guid();
        //                    if (FBUser != null)
        //                    {
        //                        intFBId1 = FBUser.ID;

        //                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);



        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrentAll =
        //                                        _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2").ToList();

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = lstRizMetreUsersCurrentAll.Where(x => x.Shomareh == RizMetre.Shomareh).ToList();

        //                        //if (lstRizMetreUsersCurrent.Count==1)
        //                        //{
        //                        //    clsItemsHasConditionAddedToFB itemsHasConditionAddedToFB =
        //                        //        _context.ItemsHasConditionAddedToFBs.First(x => x.ID == Guid.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["ID"].ToString()));

        //                        //}
        //                        _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);

        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///

        //                        foreach (var item in lstRizMetreUsersCurrent)
        //                        {
        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = FBUser.Shomareh,
        //                                RMShomareh = item.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                        }
        //                        /////////////
        //                        //////////////
        //                        //////////////
        //                        ///
        //                    }


        //                }



        //                //string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";

        //                //string strItemShomareh = AddedItems;

        //                //clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh + strCharacterPlus).FirstOrDefault();

        //                //Guid intFBId = new Guid();
        //                //if (FBUser != null)
        //                //    intFBId = FBUser.ID;

        //                //string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);

        //                //List<clsRizMetreUsers> lstRizMetreUsersCurrent =
        //                //                _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

        //                //_context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);


        //                break;
        //            }
        //        case 18:
        //            {

        //                Guid BarAvordId = ItemHasCon.BarAvordId;

        //                string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

        //                string strCondition = Condition;
        //                string strFinalWorking = FinalWorking;
        //                string strAddedItems = AddedItems;
        //                string strUseItem = UseItemForAdd;
        //                string strForItem = strItemFBShomareh;
        //                string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
        //                StringToFormula StringToFormula = new StringToFormula();
        //                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
        //                if (blnCheck)
        //                {
        //                    List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId).ToList();

        //                    if (strFinalWorking != "")
        //                    {
        //                        string strItemShomareh = strAddedItems;
        //                        clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

        //                        Guid intFBId = new Guid();
        //                        if (varFBUser != null)
        //                        {
        //                            intFBId = varFBUser.ID;

        //                            List<clsRizMetreUsers> lstRizMetreUsersCurrent =
        //                                _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strItemFBShomareh && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

        //                            _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);


        //                            ///////////
        //                            //حذف حمل//
        //                            ///////////
        //                            ///

        //                            foreach (var item in lstRizMetreUsersCurrent)
        //                            {
        //                                DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                                {
        //                                    BarAvordId = ItemHasCon.BarAvordId,
        //                                    FBShomareh = varFBUser.Shomareh,
        //                                    RMShomareh = item.Shomareh,
        //                                    NoeFBId = NoeFBId
        //                                };
        //                                await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                            }
        //                            /////////////
        //                            //////////////
        //                            //////////////
        //                            ///
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //        case 19:
        //            {
        //                string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";

        //                string[] strCondition2Condition = Condition.Trim().Split(',');
        //                string strConditionY = strCondition2Condition[0].Trim();
        //                string strConditionZ = strCondition2Condition[1].Trim();

        //                string strConditionOpY = strConditionY.Replace("y", RizMetre.Arz != null ? RizMetre.Arz.Value.ToString().Trim() : "");
        //                string strConditionOpZ = strConditionZ.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");

        //                StringToFormula stringToFormula = new StringToFormula();
        //                bool blnCheckY = stringToFormula.RelationalExpression2(strConditionOpY);
        //                bool blnCheckZ = stringToFormula.RelationalExpression2(strConditionOpZ);
        //                if (blnCheckY && blnCheckZ)
        //                {
        //                    string strItemShomareh1 = AddedItems + strCharacterPlus;

        //                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh1).FirstOrDefault();

        //                    Guid intFBId1 = new Guid();
        //                    if (FBUser != null)
        //                    {
        //                        intFBId1 = FBUser.ID;

        //                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);

        //                        List<clsRizMetreUsers> lstRizMetreUsersCurrent =
        //                                        _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

        //                        _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);


        //                        ///////////
        //                        //حذف حمل//
        //                        ///////////
        //                        ///

        //                        foreach (var item in lstRizMetreUsersCurrent)
        //                        {
        //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
        //                            {
        //                                BarAvordId = ItemHasCon.BarAvordId,
        //                                FBShomareh = FBUser.Shomareh,
        //                                RMShomareh = item.Shomareh,
        //                                NoeFBId = NoeFBId
        //                            };
        //                            await HamlCommon.DeleteHaml(deleteHaml, _context);
        //                        }
        //                        /////////////
        //                        //////////////
        //                        //////////////
        //                        ///

        //                    }

        //                }

        //                break;
        //            }

        //        default:
        //            break;
        //    }
        //}
        public async Task fnCheckOperationConditionForUpdate(ApplicationDbContext _context, ItemsAddingToFBForCheckOperationDto itemsAddingToFBForCheckOperation,
            List<ItemsFieldsDto> ItemsFields, ItemsHasConditionConditionContextForCheckOperationDto ItemHasCon, Guid FBId, clsRizMetreUsers RizMetre, RizMetreInputDto OldRizMetre
            , int LevelNumber, int Year, NoeFehrestBaha NoeFBId, Guid RMForEditId)
        {
            bool blnCheckAgain = true;
            string Condition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition.Trim() : "";
            string FinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking.Trim() : "";
            string AddedItems = itemsAddingToFBForCheckOperation.AddedItems != null ? itemsAddingToFBForCheckOperation.AddedItems.Substring(0, 6).Trim() : "";
            string DesOfAddingItems = itemsAddingToFBForCheckOperation.DesOfAddingItems != null ? itemsAddingToFBForCheckOperation.DesOfAddingItems.Trim() : "";
            string UseItemForAdd = itemsAddingToFBForCheckOperation.UseItemForAdd != null ? itemsAddingToFBForCheckOperation.UseItemForAdd.Trim() : "";
            string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";
            Guid BarAvordId = ItemHasCon.BarAvordId;
            long? ConditionContextId = itemsAddingToFBForCheckOperation.ConditionContextId;
            string? ConditionContextRel = itemsAddingToFBForCheckOperation.ConditionContextRel;
            long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// itemsAddingToFBForCheckOperation.ItemsHasCondition_ConditionContextId;
            long ConditionGroupId = ItemHasCon.ConditionGroupId;
            string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();


            string? FieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding;

            string[] FieldsAddingSplit = itemsAddingToFBForCheckOperation.FieldsAdding == null ? "".Split(",") : itemsAddingToFBForCheckOperation.FieldsAdding.Split(",");

            decimal Meghdar = ItemHasCon.Meghdar;
            decimal Meghdar2 = ItemHasCon.Meghdar2;

            DateTime Now = DateTime.Now;

            List<clsItemsRelatedToItemHaml> lstRelatedToItemHaml = _context.ItemsRelatedToItemHamls.Where(x => x.Year == Year).ToList();

            long ShomareNew = 1;
            clsRizMetreUsers? RizMetre1 = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.InsertDateTime).ThenByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == ItemHasCon.BarAvordId);
            if (RizMetre1 != null)
            {
                long currentShomareNew = RizMetre1.ShomarehNew == null || RizMetre1.ShomarehNew.Trim() == "" ? 1 : long.Parse(RizMetre1.ShomarehNew);
                if (currentShomareNew > RizMetre1.Shomareh)
                {
                    ShomareNew = currentShomareNew;
                }
                else
                    ShomareNew = RizMetre1.Shomareh;
            }
            Guid guBAId = ItemHasCon.BarAvordId;

            switch (itemsAddingToFBForCheckOperation.ConditionType)
            {
                case 1:
                    {
                        //decimal dSumAllRizMetre = 0;
                        //var matchCondition = Regex.Match(FinalWorking, @"\b([a-zA-Z])\b");
                        //string variableNameCondition = matchCondition.Success ? matchCondition.Groups[1].Value : null;
                        //string strConditionOp = "";
                        decimal FinalMeghdar = (Meghdar == 0 ? 1 : Meghdar) * (Meghdar2 == 0 ? 1 : Meghdar2);


                        ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            blnCheckAgain = blnCheckAgain,
                            RMIdForEdit=RMForEditId
                        };

                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        ConditionCaseCommonCaseResultDto blnResult = await CCC.Case1(RequestCase1, _context);


                        ///طبق متغییر موجود در شرط بررسی میگردد که مقدار ورودی جایگزین کدام متغییر گردد
                        ///و در نهایت برای فیلدهای طول عرض ارتفاع وزن استفاده میگردد
                        //switch (variableNameCondition)
                        //{
                        //    case "x":
                        //        {
                        //            strConditionOp = Condition.Replace("x", FinalMeghdar.ToString().Trim());
                        //            break;
                        //        }
                        //    case "y":
                        //        {
                        //            strConditionOp = Condition.Replace("y", FinalMeghdar.ToString().Trim());
                        //            break;
                        //        }
                        //    case "z":
                        //        {
                        //            strConditionOp = Condition.Replace("z", FinalMeghdar.ToString().Trim());
                        //            break;
                        //        }
                        //    case "w":
                        //        {
                        //            strConditionOp = Condition.Replace("w", FinalMeghdar.ToString().Trim());
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

                        //    //blnCheckIsExistErrors = "false";

                        //    //string strItemFBShomareh = strItemsFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                        //    bool blnCheckSave = false;

                        //    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                        //                     .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strItemFBShomareh && x.NoeFB == NoeFBId &&
                        //                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                        //    if (currentItemsHasConditionAddedToFBs == null)
                        //    {
                        //        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //        ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                        //        ItemsHasConditionAddedToFB.FBShomareh = strItemFBShomareh;
                        //        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
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
                        //        var match = Regex.Match(FinalWorking, @"\b([a-zA-Z])\b");

                        //        string variableName = match.Success ? match.Groups[1].Value : null;
                        //        ///
                        //        //مقدار ورودی بنا به این در بخش عملیات نهایی چه حرفی گذاشته شده است
                        //        //در یکی از فیلدهای طول عرض ارتفاع وزن گذاشته میشود
                        //        switch (variableName)
                        //        {
                        //            //طول
                        //            case "x":
                        //                {

                        //                    FinalWorking = FinalWorking.Replace("x", FinalMeghdar.ToString().Trim());
                        //                    break;
                        //                }
                        //            //عرض
                        //            case "y":
                        //                {
                        //                    FinalWorking = FinalWorking.Replace("y", FinalMeghdar.ToString().Trim());
                        //                    break;
                        //                }
                        //            //ارتفاع
                        //            case "z":
                        //                {
                        //                    FinalWorking = FinalWorking.Replace("z", FinalMeghdar.ToString().Trim());
                        //                    break;
                        //                }
                        //            //وزن
                        //            case "w":
                        //                {
                        //                    FinalWorking = FinalWorking.Replace("w", FinalMeghdar.ToString().Trim());
                        //                    break;
                        //                }
                        //            default:
                        //                break;
                        //        }

                        //        decimal dPercent = decimal.Parse(StringToFormula.Eval(FinalWorking).ToString());
                        //        string strDesOfAddingItems = DesOfAddingItems.ToString().Replace("x", FinalMeghdar.ToString().Trim()).Replace("y", strItemFBShomareh).Replace("z", (dPercent * 100).ToString("0.##")).Trim();

                        //        clsBaravordUser? varBA = _context.BaravordUsers.First(x => x.ID == BarAvordId);
                        //        //DataTable DtBA = clsConvert.ToDataTable(varBA);

                        //        //Guid guBAId = varBA.ID; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());


                        //        ///////////////////////////////////////
                        //        /////فیلدهای آیتمی که درج میگردد//////
                        //        ///////////////////////////////////////

                        //        List<clsItemsFields> lstCurrentItemFields = await _context.ItemsFieldses.Where(x => x.ItemShomareh == AddedItems && x.NoeFB == NoeFBId).ToListAsync();


                        //        string strItemShomareh = AddedItems + strCharacterPlus; //"A";
                        //        clsFB? varFBUser = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh && x.NoeFBId == NoeFBId);
                        //        //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);


                        //        List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = await (from RUsers in _context.RizMetreUserses
                        //                                                                                  join fb in _context.FBs on RUsers.FBId equals fb.ID
                        //                                                                                  where RUsers.LevelNumber == LevelNumber && RUsers.FBId == FBId && RUsers.Type == "1"
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


                        //        string strShomareh1 = strItemFBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                        //        //Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());

                        //        var varRizMetreUsersesCurrent = await (from RizMetreUserses1 in _context.RizMetreUserses
                        //                                               join FB in _context.FBs on RizMetreUserses1.FBId equals FB.ID
                        //                                               where RizMetreUserses1.LevelNumber == LevelNumber
                        //                                               select new
                        //                                               {
                        //                                                   ID = RizMetreUserses1.ID,
                        //                                                   Shomareh = RizMetreUserses1.Shomareh,
                        //                                                   Tedad = RizMetreUserses1.Tedad,
                        //                                                   Tool = RizMetreUserses1.Tool,
                        //                                                   Arz = RizMetreUserses1.Arz,
                        //                                                   Ertefa = RizMetreUserses1.Ertefa,
                        //                                                   Vazn = RizMetreUserses1.Vazn,
                        //                                                   Des = RizMetreUserses1.Des,
                        //                                                   FBId = RizMetreUserses1.FBId,
                        //                                                   OperationsOfHamlId = RizMetreUserses1.OperationsOfHamlId,
                        //                                                   ForItem = RizMetreUserses1.ForItem,
                        //                                                   Type = RizMetreUserses1.Type,
                        //                                                   UseItem = RizMetreUserses1.UseItem,
                        //                                                   BarAvordId = FB.BarAvordId
                        //                                               }).Where(x => x.FBId == FBId && x.ForItem == strShomareh1 && x.Type == "2").ToListAsync();

                        //        //string[] FieldsAddingSplit = FieldsAdding.Split(",");

                        //        decimal? dTedad = null;
                        //        decimal? dTool = null;
                        //        decimal? dArz = null;
                        //        decimal? dErtefa = null;
                        //        decimal? dVazn = null;

                        //        List<string> lst = new List<string>();
                        //        for (int j = 0; j < FieldsAddingSplit.Length; j++)
                        //        {
                        //            lst.Add(FieldsAddingSplit[j]);
                        //        }


                        //        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            var tedadField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tedad);
                        //            dTedad = tedadField?.DefaultValue ?? RizMetre.Tedad;

                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            var toolField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Tool);
                        //            dTool = toolField?.DefaultValue ?? RizMetre.Tool;
                        //            if (variableName == "x")
                        //            {
                        //                dTool = dPercent;
                        //            }

                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            var arzField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Arz);
                        //            dArz = arzField?.DefaultValue ?? RizMetre.Arz;
                        //            if (variableName == "y")
                        //            {
                        //                dArz = dPercent;
                        //            }
                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                        //        if (strCal.Count != 0)
                        //        {

                        //            var ertefaField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Ertefa);
                        //            dErtefa = ertefaField?.DefaultValue ?? RizMetre.Ertefa;
                        //            if (variableName == "z")
                        //            {
                        //                dErtefa = dPercent;
                        //            }
                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                        //        if (strCal.Count != 0)
                        //        {

                        //            var vaznField = lstCurrentItemFields.FirstOrDefault(x => x.FieldType == ItemFieldType.Vazn);
                        //            dVazn = vaznField?.DefaultValue ?? RizMetre.Vazn;
                        //            if (variableName == "w")
                        //            {
                        //                dVazn = dPercent;
                        //            }
                        //        }

                        //        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //        RizMetreUserses.Shomareh = RizMetre.Shomareh;
                        //        ShomareNew++;
                        //        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //        RizMetreUserses.Sharh = RizMetre.Sharh;// "].ToString().Trim();
                        //        RizMetreUserses.Tedad = dTedad;
                        //        RizMetreUserses.Tool = dTool;
                        //        RizMetreUserses.Arz = dArz;
                        //        RizMetreUserses.Ertefa = dErtefa;
                        //        RizMetreUserses.Vazn = dVazn;
                        //        RizMetreUserses.Des = RizMetre.Des;// "].ToString().Trim();// ItemsAdding.DesOfAddingItems.ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim()
                        //                                           //+ " - ریز متره شماره " + DtRizMetreUserses.Rows[i]["Shomareh"].ToString();
                        //        RizMetreUserses.FBId = varFBUser.ID;
                        //        RizMetreUserses.OperationsOfHamlId = 1;
                        //        RizMetreUserses.Type = "2";
                        //        RizMetreUserses.ForItem = strItemFBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                        //        RizMetreUserses.UseItem = "";
                        //        RizMetreUserses.LevelNumber = LevelNumber;
                        //        RizMetreUserses.ConditionContextRel = ConditionContextRel;
                        //        RizMetreUserses.ConditionContextId = ConditionContextId;
                        //        RizMetreUserses.InsertDateTime = Now;

                        //        ///محاسبه مقدار جزء
                        //        decimal? dMeghdarJoz = null;
                        //        if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                        //            dMeghdarJoz = null;
                        //        else
                        //            dMeghdarJoz = (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                        //            (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                        //        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //        _context.RizMetreUserses.Add(RizMetreUserses);


                        //        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //            new clsItemsHasConditionAddedToFBRizMetre
                        //            {
                        //                ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                RizMetreId = RizMetreUserses.ID,
                        //                BaseRizMetreId = RizMetre.ID
                        //            });

                        //        dSumAllRizMetre += dMeghdarJoz ?? 0;


                        //        //Start///////////////////////
                        //        ////////////حمل///////////////
                        //        //////////////////////////////
                        //        ///آیتم های حمل درج میگردد
                        //        ///

                        //        SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //        {
                        //            BarAvordUserId = guBAId,
                        //            Year = Year,
                        //            ItemFBShomareh = strItemShomareh,
                        //            NoeFBId = NoeFBId,
                        //            //BarAvordHamlId = gBarAvordHamlId,
                        //            Shomareh = RizMetreUserses.Shomareh,
                        //            MeghdarJoz = dMeghdarJoz,
                        //            LevelNumber = 1,
                        //        };
                        //        await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //        //RizMetreUserses.Save();



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
                        break;
                    }

                //case 1:
                //    {
                //        string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                //        string strCondition = Condition;
                //        string strFinalWorking = FinalWorking;
                //        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                //        StringToFormula StringToFormula = new StringToFormula();
                //        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                //        if (blnCheck)
                //        {
                //            strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                //            decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                //            string strAddedItems = AddedItems;
                //            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strCharacterPlus);

                //            Guid intFBId = new Guid();

                //            if (varFBUsersAdded != null)
                //            {
                //                //varFBUsersAdded.BahayeVahedZarib = dPercent;
                //                intFBId = varFBUsersAdded.ID;


                //                string strShomareh1 = ItemHasCon.FBShomareh;

                //                clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);

                //                if (RizMetreUsersCurrent != null)
                //                {
                //                    RizMetre.ID = RizMetreUsersCurrent.ID;
                //                    RizMetre.FBId = RizMetreUsersCurrent.FBId;
                //                    RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                //                    RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                //                    RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                //                    RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                //                    RizMetre.Type = RizMetreUsersCurrent.Type;
                //                    RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                //                    RizMetre.Des = RizMetreUsersCurrent.Des;

                //                    decimal? dMeghdarJoz = RizMetre.MeghdarJoz;


                //                    ////////////////////////////
                //                    ///حمل قبلی ویرایش میگردد///
                //                    ////////////////////////////
                //                    UpdateHamlDto UHamlDto = new UpdateHamlDto
                //                    {
                //                        BarAvordId = ItemHasCon.BarAvordId,
                //                        FBShomareh = ItemHasCon.FBShomareh,
                //                        RMShomareh = RizMetreUsersCurrent.Shomareh,
                //                        MeghdarJoz = dMeghdarJoz,
                //                        Year = Year,
                //                        NoeFB = NoeFBId
                //                    };
                //                    HamlCommon.UpdateHaml(UHamlDto, _context);
                //                }
                //                _context.SaveChanges();
                //            }

                //        }
                //        break;
                //    }
                //case 2:
                //    {
                //        string strShomarehAdd = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                //        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);//.ToList();
                //        Guid intFBId = new Guid();

                //        if (varFBUser != null)
                //            intFBId = varFBUser.ID;


                //        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6); //DtFB.Rows[0]["Shomareh"].ToString().Trim();

                //        List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFBId && x.ItemShomareh == strShomarehAdd).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                //        clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                //        if (RizMetreUsersCurrent != null)
                //        {
                //            //RizMetre.ID = RizMetreUsersCurrent.ID;
                //            //RizMetre.FBId = RizMetreUsersCurrent.FBId;
                //            //RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                //            //RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                //            //RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                //            //RizMetre.ShomarehNew = RizMetreUsersCurrent.ShomarehNew;
                //            //RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                //            //RizMetre.Type = RizMetreUsersCurrent.Type;
                //            //RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                //            //RizMetre.Des = RizMetreUsersCurrent.Des;

                //            //if (lstCurrentItemsFields[0] != true)
                //            //    RizMetre.Tedad = null;


                //            //if (lstCurrentItemsFields[1] != true)
                //            //    RizMetre.Tool = null;


                //            //if (lstCurrentItemsFields[2] != true)
                //            //    RizMetre.Arz = null;


                //            //if (lstCurrentItemsFields[3] != true)
                //            //    RizMetre.Ertefa = null;


                //            //if (lstCurrentItemsFields[4] != true)
                //            //    RizMetre.Vazn = null;



                //            ///محاسبه مقدار جزء
                //            decimal? dMeghdarJoz = null;
                //            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                //                dMeghdarJoz = null;
                //            else
                //                dMeghdarJoz = (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                //                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                //            RizMetre.MeghdarJoz = dMeghdarJoz;

                //            _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(new
                //            {
                //                Tedad = RizMetre.Tedad,
                //                Tool = RizMetre.Tool,
                //                Arz = RizMetre.Arz,
                //                Ertefa = RizMetre.Ertefa,
                //                Vazn = RizMetre.Vazn,
                //                MeghdarJoz = RizMetre.MeghdarJoz
                //            });
                //            _context.SaveChanges();

                //            ////////////////////////////
                //            ///حمل قبلی ویرایش میگردد///
                //            ////////////////////////////
                //            UpdateHamlDto UHamlDto = new UpdateHamlDto
                //            {
                //                BarAvordId = ItemHasCon.BarAvordId,
                //                FBShomareh = ItemHasCon.FBShomareh,
                //                RMShomareh = RizMetreUsersCurrent.Shomareh,
                //                MeghdarJoz = dMeghdarJoz,
                //                Year = Year,
                //                NoeFB = NoeFBId,
                //            };
                //            HamlCommon.UpdateHaml(UHamlDto, _context);

                //            //bool blnHasHaml = false;
                //            //List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = _context.ItemsRelatedToItemHamls
                //            //    .Where(x => x.ItemFB.Trim() == strShomarehAdd && x.Year == Year).ToList();

                //            //foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
                //            //{
                //            //    string strItemHamlFB = "";
                //            //    if (itemsRelatedToItemHaml != null)
                //            //    {
                //            //        strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                //            //        //blnHasHaml = true;
                //            //        decimal? Zarib1 = itemsRelatedToItemHaml.Zarib1;
                //            //        decimal? Zarib2 = itemsRelatedToItemHaml.Zarib2;
                //            //        decimal? Zarib3 = itemsRelatedToItemHaml.Zarib3;
                //            //        decimal? dAllZarib = (Zarib1 != null ? Zarib1.Value : 1) * (Zarib2 != null ? Zarib2.Value : 1) * (Zarib3 != null ? Zarib3.Value : 1);

                //            //        //if (blnHasHaml)
                //            //        //{
                //            //        string ItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB;
                //            //        clsFB? FbHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == ItemHamlFB);
                //            //        if (FbHaml != null)
                //            //        {
                //            //            List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == FbHaml.ID
                //            //                && x.ForItem == ItemHasCon.FBShomareh.Substring(0, 6).Trim() && x.Type == "3" && x.Shomareh == RizMetreUsersCurrent.Shomareh).ToList();

                //            //            foreach (var RMForDelForHaml in lstRMForDelForHaml)
                //            //            {
                //            //                _context.Entry(RMForDelForHaml).CurrentValues.SetValues(new
                //            //                {
                //            //                    Vazn = dMeghdarJoz,
                //            //                    MeghdarJoz = dAllZarib * dMeghdarJoz
                //            //                });
                //            //            }
                //            //        }
                //            //    }
                //            //}

                //            _context.SaveChanges();
                //        }
                //        break;
                //    }
                case 3:
                    {
                        string FinalWorking1 = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "0";
                        decimal dPercent = decimal.Parse(FinalWorking1);
                        string strAddedItems = AddedItems;
                        string strStatus = dPercent > 0 ? "B" : "e";
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strStatus);

                        Guid intFBId = new Guid();

                        if (varFBUser != null)
                        {
                            intFBId = varFBUser.ID;
                            //varFBUser.BahayeVahedZarib = dPercent;
                        }

                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                        clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                        if (RizMetreUsersCurrent != null)
                        {
                            RizMetre.ID = RizMetreUsersCurrent.ID;
                            RizMetre.FBId = RizMetreUsersCurrent.FBId;
                            RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                            RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                            RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                            RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                            RizMetre.Type = RizMetreUsersCurrent.Type;
                            RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                            RizMetre.Des = RizMetreUsersCurrent.Des;

                            _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                            decimal? dMeghdarJoz = RizMetre.MeghdarJoz;
                            ////////////////////////////
                            ///حمل قبلی ویرایش میگردد///
                            ////////////////////////////
                            UpdateHamlDto UHamlDto = new UpdateHamlDto
                            {
                                BarAvordId = ItemHasCon.BarAvordId,
                                FBShomareh = ItemHasCon.FBShomareh,
                                RMShomareh = RizMetreUsersCurrent.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                Year = Year,
                                NoeFB = NoeFBId,
                            };
                            HamlCommon.UpdateHaml(UHamlDto, _context);
                            _context.SaveChanges();
                        }
                        break;
                    }
                case 4:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {

                            string strShomarehAdd = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);

                            Guid intFBId = new Guid();

                            if (varFBUser != null)
                                intFBId = varFBUser.ID;

                            DataTable DtRizMetreUsers = new DataTable();
                            string strForItem = ItemHasCon.FBShomareh;
                            string strUseItem = "";
                            //string strItemFBShomareh = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();


                            clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strForItem && x.UseItem == strUseItem && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                            if (RizMetreUsersCurrent != null)
                            {
                                RizMetre.ID = RizMetreUsersCurrent.ID;
                                RizMetre.FBId = RizMetreUsersCurrent.FBId;
                                RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                                RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                                RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                                RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                                RizMetre.Type = RizMetreUsersCurrent.Type;
                                RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                                RizMetre.Des = RizMetreUsersCurrent.Des;

                                _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                                decimal? dMeghdarJoz = RizMetre.MeghdarJoz;
                                ////////////////////////////
                                ///حمل قبلی ویرایش میگردد///
                                ////////////////////////////
                                UpdateHamlDto UHamlDto = new UpdateHamlDto
                                {
                                    BarAvordId = ItemHasCon.BarAvordId,
                                    FBShomareh = ItemHasCon.FBShomareh,
                                    RMShomareh = RizMetreUsersCurrent.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    Year = Year,
                                    NoeFB = NoeFBId,
                                };
                                HamlCommon.UpdateHaml(UHamlDto, _context);

                                _context.SaveChanges();
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        string strFinalWorking = FinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        decimal? dMultiple = null;
                        StringToFormula StringToFormula = new StringToFormula();

                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            if (strFinalWorking != "")
                            {
                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                            }

                            string strAddedItems = AddedItems;
                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                            Guid intFBId = new Guid();
                            if (varFBUser != null)
                                intFBId = varFBUser.ID;

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFBId && x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                            string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                            clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                            if (RizMetreUsersCurrent != null)
                            {
                                RizMetre.ID = RizMetreUsersCurrent.ID;
                                RizMetre.FBId = RizMetreUsersCurrent.FBId;
                                RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                                RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                                RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                                RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                                RizMetre.Type = RizMetreUsersCurrent.Type;
                                RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                                RizMetre.Des = RizMetreUsersCurrent.Des;


                                if (lstCurrentItemsFields[0] != true)
                                    RizMetre.Tedad = null;

                                if (lstCurrentItemsFields[1] != true)
                                    RizMetre.Tool = null;

                                if (lstCurrentItemsFields[2] != true)
                                    RizMetre.Arz = null;

                                if (lstCurrentItemsFields[3] != true)
                                    RizMetre.Ertefa = null;

                                RizMetre.Vazn = RizMetreUsersCurrent.Vazn;

                                //if (lstCurrentItemsFields[4] == true)
                                //    RizMetre.Vazn = dMultiple;
                                //else
                                //    RizMetre.Vazn = null;

                                _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                                decimal? dMeghdarJoz = RizMetre.MeghdarJoz;
                                ////////////////////////////
                                ///حمل قبلی ویرایش میگردد///
                                ////////////////////////////
                                UpdateHamlDto UHamlDto = new UpdateHamlDto
                                {
                                    BarAvordId = ItemHasCon.BarAvordId,
                                    FBShomareh = ItemHasCon.FBShomareh,
                                    RMShomareh = RizMetreUsersCurrent.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    Year = Year,
                                    NoeFB = NoeFBId,
                                };
                                HamlCommon.UpdateHaml(UHamlDto, _context);

                                _context.SaveChanges();
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            blnCheckAgain = blnCheckAgain,
                            RMIdForEdit = RMForEditId
                        };

                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        ConditionCaseCommonCaseResultDto blnResult = await CCC.Case2(RequestCase1, _context);

                        break;
                        //string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];
                        //string strCondition = Condition;
                        //string strAddedItems = AddedItems;
                        //string strFinalWorking = FinalWorking;
                        //DataTable DtRizMetreUserses = new DataTable();
                        //string strForItem = "";
                        //string strUseItem = "";
                        ////string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        //Guid guFBId = FBId;
                        //strUseItem = strItemFBShomareh;
                        //strForItem = UseItemForAdd;
                        /////ریز متره قبلی حذف میگردد

                        //string strConditionOp = strCondition.Replace("z", OldRizMetre.Ertefa == null ? "0" : OldRizMetre.Ertefa.Value.ToString().Trim());
                        //StringToFormula StringToFormula = new StringToFormula();

                        //bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        //if (blnCheck)
                        //{
                        //    string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                        //                                                    //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());

                        //    string strItemShomareh = AddedItems;
                        //    strAddedItems = strAddedItems.Trim();
                        //    clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

                        //    //DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                        //    Guid intFBId = new Guid();
                        //    if (varFBUsersAdded != null)
                        //    {
                        //        intFBId = varFBUsersAdded.ID;

                        //        long lngShomareh = OldRizMetre.Shomareh;

                        //        List<clsRizMetreUsers> lstRizMetreUsers = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                        //        if (lstRizMetreUsers.Count != 0)
                        //        {
                        //            _context.RizMetreUserses.RemoveRange(lstRizMetreUsers);

                        //            ///////////
                        //            //حذف حمل//
                        //            ///////////
                        //            DeleteHamlDto deleteHaml = new DeleteHamlDto
                        //            {
                        //                BarAvordId = ItemHasCon.BarAvordId,
                        //                FBShomareh = varFBUsersAdded.Shomareh,
                        //                RMShomareh = RizMetre.Shomareh,
                        //                NoeFBId = NoeFBId
                        //            };
                        //            await HamlCommon.DeleteHaml(deleteHaml, _context);


                        //            ///حمل قبلی حذف میگردد///
                        //            //bool blnHasHaml = false;
                        //            //List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = _context.ItemsRelatedToItemHamls
                        //            //    .Where(x => x.ItemFB.Trim() == strAddedItems && x.Year == Year).ToList();

                        //            //foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
                        //            //{
                        //            //    string strItemHamlFB = "";
                        //            //    if (itemsRelatedToItemHaml != null)
                        //            //    {
                        //            //        strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                        //            //        blnHasHaml = true;

                        //            //        if (blnHasHaml)
                        //            //        {
                        //            //            string ItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB;
                        //            //            clsFB? FbHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == ItemHamlFB);
                        //            //            if (FbHaml != null)
                        //            //            {
                        //            //                List<clsRizMetreUsers> lstRMForDelForHaml = _context.RizMetreUserses.Where(x => x.FBId == FbHaml.ID
                        //            //                    && x.ForItem == strCurrentFBShomareh.Trim() && x.Type == "3" && x.Shomareh == lngShomareh).ToList();
                        //            //                _context.RizMetreUserses.RemoveRange(lstRMForDelForHaml);
                        //            //            }
                        //            //        }
                        //            //    }
                        //            //}
                        //        }
                        //        _context.SaveChanges();
                        //    }

                        //}

                        ////////////////
                        /////////////////
                        /////////////////
                        /////ریز متره جدید درج میگردد
                        ////////////////
                        //////////////
                        ///////////////
                        /////      
                        //string strFinalWorkingNew = FinalWorking;
                        //decimal? dErtefaNew1 = null;
                        //string strConditionOpNew = strCondition.Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
                        //StringToFormula StringToFormulaNew = new StringToFormula();

                        //bool blnCheckNew = StringToFormulaNew.RelationalExpression2(strConditionOpNew);
                        //if (blnCheckNew)
                        //{

                        //    string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                        //                                                    //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                        //    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                        //         .FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.FBShomareh.Trim() == strCurrentFBShomareh.Trim() &&
                        //             x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                        //    if (currentItemsHasConditionAddedToFBs == null)
                        //    {
                        //        clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //        ItemsHasConditionAddedToFB.BarAvordId = ItemHasCon.BarAvordId;
                        //        ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                        //        ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                        //        ItemsHasConditionAddedToFB.Meghdar = 0;// RizMetre.Tool != null ? RizMetre.Tool.Value : 0;
                        //        ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;
                        //        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                        //        _context.SaveChanges();
                        //    }

                        //    string strItemShomareh = AddedItems;
                        //    strAddedItems = strAddedItems.Trim();

                        //    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems).FirstOrDefault();

                        //    Guid intFBId = new Guid();
                        //    if (FBUser != null)
                        //        intFBId = FBUser.ID;


                        //    long lngShomareh = RizMetre.Shomareh;
                        //    var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
                        //                            join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                        //                            where RizMetreUsers.LevelNumber == RizMetre.LevelNumber
                        //                            select new
                        //                            {
                        //                                RizMetreUsers.ID,
                        //                                RizMetreUsers.Shomareh,
                        //                                RizMetreUsers.Tedad,
                        //                                RizMetreUsers.Tool,
                        //                                RizMetreUsers.Arz,
                        //                                RizMetreUsers.Ertefa,
                        //                                RizMetreUsers.Vazn,
                        //                                RizMetreUsers.Sharh,
                        //                                RizMetreUsers.Des,
                        //                                RizMetreUsers.FBId,
                        //                                RizMetreUsers.OperationsOfHamlId,
                        //                                RizMetreUsers.ForItem,
                        //                                RizMetreUsers.Type,
                        //                                RizMetreUsers.UseItem,
                        //                                FB.BarAvordId
                        //                            }).Where(x => x.FBId == FBId && x.ForItem == strItemFBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();
                        //    DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);


                        //    if (DtRizMetreUsers.Rows.Count == 0)
                        //    {
                        //        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                        //        RizMetreUsers.Shomareh = RizMetre.Shomareh;
                        //        ShomareNew++;
                        //        RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                        //        RizMetreUsers.Sharh = RizMetre.Sharh;// " آیتم " + strAddedItems; //DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();

                        //        List<string> lst = new List<string>();
                        //        for (int j = 0; j < strFieldsAdding.Length; j++)
                        //        {
                        //            lst.Add(strFieldsAdding[j]);
                        //        }

                        //        decimal? dTedad = null;
                        //        decimal? dTool = null;
                        //        decimal? dArz = null;
                        //        decimal? dErtefa = null;
                        //        decimal? dVazn = null;

                        //        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            dTedad = RizMetre.Tedad;
                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            dTool = RizMetre.Tool;
                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            dArz = RizMetre.Arz;
                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            dErtefa = RizMetre.Ertefa;
                        //        }
                        //        strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                        //        if (strCal.Count != 0)
                        //        {
                        //            dVazn = RizMetre.Vazn;
                        //        }

                        //        RizMetreUsers.Tedad = dTedad;
                        //        RizMetreUsers.Tool = dTool;
                        //        RizMetreUsers.Arz = dArz;
                        //        RizMetreUsers.Ertefa = dErtefa;
                        //        RizMetreUsers.Vazn = dVazn;
                        //        RizMetreUsers.Des = RizMetre.Des; //DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh + " - ریز متره " + lngShomareh;
                        //        RizMetreUsers.FBId = intFBId;
                        //        RizMetreUsers.OperationsOfHamlId = 1;
                        //        RizMetreUsers.Type = "2";
                        //        RizMetreUsers.ForItem = strItemFBShomareh;
                        //        RizMetreUsers.UseItem = "";
                        //        RizMetreUsers.LevelNumber = RizMetre.LevelNumber;
                        //        RizMetreUsers.InsertDateTime = Now;

                        //        ///محاسبه مقدار جزء
                        //        decimal dMeghdarJoz = 0;
                        //        if (dTedad == null && dTool == null && dArz == null && dVazn == null)
                        //            dMeghdarJoz = 0;
                        //        else
                        //            dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                        //            (dArz == null ? 1 : dArz.Value) * (dErtefaNew1 == null ? 1 : dErtefaNew1.Value) * (dVazn == null ? 1 : dVazn.Value);
                        //        RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                        //        _context.RizMetreUserses.Add(RizMetreUsers);


                        //        //Start///////////////////////
                        //        ////////////حمل///////////////
                        //        //////////////////////////////
                        //        ///آیتم های حمل درج میگردد
                        //        ///
                        //        SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //        {
                        //            BarAvordUserId = ItemHasCon.BarAvordId,
                        //            Year = Year,
                        //            ItemFBShomareh = strItemShomareh,
                        //            NoeFBId = NoeFBId,
                        //            Shomareh = RizMetre.Shomareh,
                        //            MeghdarJoz = dMeghdarJoz,
                        //            LevelNumber = 1,
                        //        };
                        //        await HamlCommon.SaveHaml(requestSaveHaml, _context);

                        //        /////////////
                        //        //////////////
                        //        //////////////

                        //        //Start///////////////////////
                        //        ////////////حمل///////////////
                        //        //////////////////////////////
                        //        ///آیتم های حمل درج میگردد
                        //        //string FBShomareh = strAddedItems;
                        //        //bool blnHasHaml = false;
                        //        //List<clsItemsRelatedToItemHaml> lstItemsRelatedToItemHaml = lstRelatedToItemHaml.Where(x => x.ItemFB.Trim() == FBShomareh).ToList();
                        //        //foreach (var itemsRelatedToItemHaml in lstItemsRelatedToItemHaml)
                        //        //{

                        //        //    string strItemHamlFB = "";
                        //        //    decimal? Zarib1 = null;
                        //        //    decimal? Zarib2 = null;
                        //        //    decimal? Zarib3 = null;
                        //        //    if (itemsRelatedToItemHaml != null)
                        //        //    {
                        //        //        strItemHamlFB = itemsRelatedToItemHaml.ItemHamlFB.Trim();
                        //        //        Zarib1 = itemsRelatedToItemHaml.Zarib1;
                        //        //        Zarib2 = itemsRelatedToItemHaml.Zarib2;
                        //        //        Zarib3 = itemsRelatedToItemHaml.Zarib3;

                        //        //        clsFB? FBHaml = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strItemHamlFB && x.NoeFBId == NoeFBId);
                        //        //        Guid gFBIdHaml = new Guid();
                        //        //        if (FBHaml != null)
                        //        //        {
                        //        //            gFBIdHaml = FBHaml.ID;
                        //        //        }
                        //        //        else
                        //        //        {
                        //        //            clsFB newFBHaml = new clsFB
                        //        //            {
                        //        //                BarAvordId = guBAId,
                        //        //                InsertDateTime = Now,
                        //        //                Shomareh = strItemHamlFB,
                        //        //                NoeFBId = NoeFBId
                        //        //            };
                        //        //            _context.FBs.Add(newFBHaml);
                        //        //            gFBIdHaml = newFBHaml.ID;
                        //        //        }

                        //        //        ///
                        //        //        ///درج ریز متره
                        //        //        ///

                        //        //        clsRizMetreUsers RizMetreHaml = new clsRizMetreUsers();
                        //        //        RizMetreHaml.Shomareh = RizMetreUsers.Shomareh;
                        //        //        ShomareNew++;
                        //        //        RizMetreHaml.ShomarehNew = ShomareNew.ToString();
                        //        //        RizMetreHaml.Sharh = " حمل ";
                        //        //        RizMetreHaml.Tedad = null;
                        //        //        RizMetreHaml.Tool = Zarib1;
                        //        //        RizMetreHaml.Arz = Zarib2;
                        //        //        RizMetreHaml.Ertefa = Zarib3;
                        //        //        //RizMetreHaml.Vazn = dVazn;
                        //        //        RizMetreHaml.Des = " آیتم - " + strAddedItems;
                        //        //        RizMetreHaml.FBId = gFBIdHaml;
                        //        //        RizMetreHaml.OperationsOfHamlId = 1;
                        //        //        RizMetreHaml.LevelNumber = LevelNumber;
                        //        //        RizMetreHaml.Type = "3";
                        //        //        RizMetreHaml.ForItem = ItemHasCon.FBShomareh;//DtFB.Rows[0]["Shomareh"].ToString().Trim();
                        //        //        RizMetreHaml.UseItem = "";
                        //        //        ///محاسبه مقدار جزء
                        //        //        //decimal dMeghdarJozHaml = 0;
                        //        //        //if (dTedad == null && dTool == null && dArz == null && dVazn == null)
                        //        //        //    dMeghdarJozHaml = 0;
                        //        //        //else
                        //        //        //    dMeghdarJozHaml += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                        //        //        //    ((dArz == null || dArz == 0) ? 1 : dArz.Value) * ((dErtefa == null || dErtefa == 0) ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);

                        //        //        decimal? dAllZarib = (Zarib1 != null ? Zarib1.Value : 1) * (Zarib2 != null ? Zarib2.Value : 1) * (Zarib3 != null ? Zarib3.Value : 1);

                        //        //        RizMetreHaml.Vazn = dMeghdarJoz;
                        //        //        RizMetreHaml.MeghdarJoz = dAllZarib * dMeghdarJoz;
                        //        //        _context.RizMetreUserses.Add(RizMetreHaml);
                        //        //    }

                        //        //    //End//////////////////////////
                        //        //    /////////////حمل//////////////
                        //        //    //////////////////////////////
                        //        //}

                        //        _context.SaveChanges();
                        //        //RizMetreUsers.Save();
                        //    }
                        //}
                        //break;
                    }
                //case 7:
                //    {
                //        StringToFormula StringToFormula = new StringToFormula();
                //        ///حذف قبلی ها
                //        string strCondition1 = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                //        string strConditionOp1 = strCondition1.Replace("x", OldRizMetre.Arz.ToString().Trim());
                //        bool blnCheck1 = StringToFormula.RelationalExpression(strConditionOp1);
                //        if (blnCheck1)
                //        {
                //            string strAddedItems = AddedItems;
                //            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                //            Guid intFBId = new Guid();
                //            if (varFBUser != null)
                //                intFBId = varFBUser.ID;

                //            //List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                //            string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                //            clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == OldRizMetre.Shomareh);
                //            if (RizMetreUsersCurrent != null)
                //            {
                //                _context.RizMetreUserses.Remove(RizMetreUsersCurrent);
                //            }

                //            ///////////
                //            //حذف حمل//
                //            ///////////
                //            DeleteHamlDto deleteHaml = new DeleteHamlDto
                //            {

                //                BarAvordId = ItemHasCon.BarAvordId,
                //                FBShomareh = strAddedItems,
                //                RMShomareh = RizMetre.Shomareh,
                //                NoeFBId = NoeFBId
                //            };
                //            await HamlCommon.DeleteHaml(deleteHaml, _context);

                //        }



                //        ///درج جدید
                //        ///

                //        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();
                //        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                //        string strConditionOp = strCondition.Replace("x", RizMetre.Arz.ToString().Trim());
                //        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                //        if (blnCheck)
                //        {
                //            string strAddedItems = AddedItems;
                //            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                //            Guid intFBId = new Guid();
                //            if (varFBUser != null)
                //                intFBId = varFBUser.ID;

                //            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFBId && x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                //            string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                //            clsRizMetreUsers RizMetreNew = new clsRizMetreUsers();


                //            RizMetreNew.FBId = intFBId;
                //            RizMetreNew.ForItem = strShomareh1;
                //            RizMetreNew.UseItem = null;
                //            RizMetreNew.Shomareh = OldRizMetre.Shomareh;
                //            RizMetreNew.OperationsOfHamlId = RizMetre.OperationsOfHamlId;
                //            RizMetreNew.Type = "2";
                //            RizMetreNew.LevelNumber = RizMetre.LevelNumber;
                //            RizMetreNew.Des = RizMetre.Des;
                //            RizMetreNew.Sharh = RizMetre.Sharh;


                //            if (lstCurrentItemsFields[0] != true)
                //                RizMetreNew.Tedad = null;
                //            else
                //                RizMetreNew.Tedad = RizMetre.Tedad;

                //            if (lstCurrentItemsFields[1] != true)
                //                RizMetreNew.Tool = null;
                //            else
                //                RizMetreNew.Tool = RizMetre.Tool;

                //            if (lstCurrentItemsFields[2] != true)
                //                RizMetreNew.Arz = null;
                //            else
                //                RizMetreNew.Arz = RizMetre.Arz;

                //            if (lstCurrentItemsFields[3] != true)
                //                RizMetreNew.Ertefa = null;
                //            else
                //                RizMetreNew.Ertefa = RizMetre.Ertefa;

                //            if (lstCurrentItemsFields[4] == true)
                //                RizMetreNew.Vazn = null;
                //            else
                //                RizMetreNew.Vazn = RizMetre.Vazn;


                //            ///محاسبه مقدار جزء
                //            decimal? dMeghdarJoz = null;
                //            if (RizMetreNew.Tedad == null && RizMetreNew.Tool == null && RizMetreNew.Arz == null && RizMetreNew.Ertefa == null && RizMetreNew.Vazn == null)
                //                dMeghdarJoz = null;
                //            else
                //                dMeghdarJoz = (RizMetreNew.Tedad == null ? 1 : RizMetreNew.Tedad.Value) * (RizMetreNew.Tool == null ? 1 : RizMetreNew.Tool.Value) *
                //                (RizMetreNew.Arz == null ? 1 : RizMetreNew.Arz.Value) * (RizMetreNew.Ertefa == null ? 1 : RizMetreNew.Ertefa.Value) * (RizMetreNew.Vazn == null ? 1 : RizMetreNew.Vazn.Value);
                //            RizMetreNew.MeghdarJoz = dMeghdarJoz;

                //            _context.RizMetreUserses.Add(RizMetreNew);

                //            //Start///////////////////////
                //            ////////////حمل///////////////
                //            //////////////////////////////
                //            ///آیتم های حمل درج میگردد
                //            ///
                //            SaveHamlDto requestSaveHaml = new SaveHamlDto
                //            {
                //                BarAvordUserId = ItemHasCon.BarAvordId,
                //                Year = Year,
                //                ItemFBShomareh = strAddedItems,
                //                NoeFBId = NoeFBId,
                //                Shomareh = RizMetre.Shomareh,
                //                MeghdarJoz = dMeghdarJoz,
                //                LevelNumber = 1,
                //            };
                //            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                //            /////////////
                //            //////////////
                //            //////////////

                //            _context.SaveChanges();

                //        }
                //        break;
                //    }

                case 8:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        StringToFormula StringToFormula = new StringToFormula();
                        string strAddedItems = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);

                        Guid intFBId = new Guid();
                        if (varFBUser != null)
                            intFBId = varFBUser.ID;

                        string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();

                        clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                        if (RizMetreUsersCurrent != null)
                        {
                            RizMetre.ID = RizMetreUsersCurrent.ID;
                            RizMetre.FBId = RizMetreUsersCurrent.FBId;
                            RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                            RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                            RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                            RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                            RizMetre.Type = RizMetreUsersCurrent.Type;
                            RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                            RizMetre.Des = RizMetreUsersCurrent.Des;

                            _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                            decimal? dMeghdarJoz = RizMetre.MeghdarJoz;
                            ////////////////////////////
                            ///حمل قبلی ویرایش میگردد///
                            ////////////////////////////
                            UpdateHamlDto UHamlDto = new UpdateHamlDto
                            {
                                BarAvordId = ItemHasCon.BarAvordId,
                                FBShomareh = ItemHasCon.FBShomareh,
                                RMShomareh = RizMetreUsersCurrent.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                Year = Year,
                                NoeFB = NoeFBId,
                            };
                            HamlCommon.UpdateHaml(UHamlDto, _context);

                            _context.SaveChanges();
                        }

                        break;
                    }
                case 9:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strAddedItems = AddedItems;
                        string strConditionOp = strCondition.Replace("x", RizMetre.Tool.ToString().Trim()).Replace("y", RizMetre.Arz.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {

                            strAddedItems = strAddedItems.Trim();
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

                            Guid intFBId = new Guid();
                            if (varFBUsersAdded != null)
                                intFBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                            long lngShomareh = RizMetre.Shomareh; //long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());


                            clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh && x.Shomareh == lngShomareh);
                            if (RizMetreUsersCurrent != null)
                            {
                                RizMetre.ID = RizMetreUsersCurrent.ID;
                                RizMetre.FBId = RizMetreUsersCurrent.FBId;
                                RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                                RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                                RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                                RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                                RizMetre.Type = RizMetreUsersCurrent.Type;
                                RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                                RizMetre.Des = RizMetreUsersCurrent.Des;

                                _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                                decimal? dMeghdarJoz = RizMetre.MeghdarJoz;

                                ////////////////////////////
                                ///حمل قبلی ویرایش میگردد///
                                ////////////////////////////
                                UpdateHamlDto UHamlDto = new UpdateHamlDto
                                {
                                    BarAvordId = ItemHasCon.BarAvordId,
                                    FBShomareh = ItemHasCon.FBShomareh,
                                    RMShomareh = RizMetreUsersCurrent.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    Year = Year,
                                    NoeFB = NoeFBId,
                                };
                                HamlCommon.UpdateHaml(UHamlDto, _context);

                                _context.SaveChanges();
                            }
                        }

                        break;
                    }
                //case 10:
                //    {
                //        if (blnCheckAgain)
                //        {
                //            string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                //            string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                //            string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString());
                //            StringToFormula StringToFormula = new StringToFormula();
                //            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                //            if (blnCheck)
                //            {
                //                blnCheckAgain = false;

                //                strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString());
                //                decimal dZarib = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                //                if (dZarib == 0)
                //                {
                //                    clsFB? varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == AddedItems).FirstOrDefault();
                //                    if (varFBUsersAdded != null)
                //                    {
                //                        Guid guFBUsersAddedId = varFBUsersAdded.ID;
                //                        clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                //                        if (Fb != null)
                //                        {
                //                            _context.RizMetreUserses.Where(x => x.FBId == Fb.ID).ExecuteDelete();

                //                            _context.FBs.Remove(Fb);

                //                            ///////////
                //                            //حذف حمل//
                //                            ///////////
                //                            DeleteHamlDto deleteHaml = new DeleteHamlDto
                //                            {
                //                                BarAvordId = ItemHasCon.BarAvordId,
                //                                FBShomareh = varFBUsersAdded.Shomareh,
                //                                RMShomareh = RizMetre.Shomareh,
                //                                NoeFBId = NoeFBId
                //                            };
                //                            await HamlCommon.DeleteHaml(deleteHaml, _context);

                //                            _context.SaveChanges();
                //                        }
                //                    }
                //                }
                //                else
                //                {

                //                    dZarib = dZarib < 0 ? dZarib * -1 : dZarib;

                //                    string strItemShomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                //                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

                //                    Guid intFBId = new Guid();
                //                    if (FBUser != null)
                //                    {
                //                        intFBId = FBUser.ID;
                //                        //FBUser.BahayeVahedZarib = dZarib;
                //                    }



                //                    string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                //                    clsRizMetreUsers? RizMetreUsersCurrent =
                //                        _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                //                    if (RizMetreUsersCurrent != null)
                //                    {
                //                        RizMetre.ID = RizMetreUsersCurrent.ID;
                //                        RizMetre.FBId = RizMetreUsersCurrent.FBId;
                //                        RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                //                        RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                //                        RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                //                        RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                //                        RizMetre.Type = RizMetreUsersCurrent.Type;
                //                        RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                //                        RizMetre.Des = RizMetreUsersCurrent.Des;

                //                        _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                //                        decimal? dMeghdarJoz = RizMetre.MeghdarJoz;
                //                        ////////////////////////////
                //                        ///حمل قبلی ویرایش میگردد///
                //                        ////////////////////////////
                //                        UpdateHamlDto UHamlDto = new UpdateHamlDto
                //                        {
                //                            BarAvordId = ItemHasCon.BarAvordId,
                //                            FBShomareh = ItemHasCon.FBShomareh,
                //                            RMShomareh = RizMetreUsersCurrent.Shomareh,
                //                            MeghdarJoz = dMeghdarJoz,
                //                            Year = Year,
                //                            NoeFB = NoeFBId,
                //                        };
                //                        HamlCommon.UpdateHaml(UHamlDto, _context);

                //                        _context.SaveChanges();
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                //if (blnCheckIsExistErrors != "false")
                //                //{
                //                //    blnCheckIsExistErrors = "true";
                //                //    strErrors = "عدد وارد شده در محدوده قابل قبولی نمیباشد "; //strCondition.Replace("x", "عدد وارد شده");
                //                //}
                //            }
                //        }
                //        break;
                //    }
                case 11:
                    {
                        //string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                        string strItemShomareh = ItemHasCon.FBShomareh.Substring(0, 6) + strCharacterPlus;
                        string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                        string strDesOfAddingItems = DesOfAddingItems;
                        decimal dPercent = decimal.Parse(strFinalWorking);

                        clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

                        Guid intFBId = new Guid();
                        if (FBUser != null)
                        {
                            intFBId = FBUser.ID;
                            //FBUser.BahayeVahedZarib = dPercent;
                        }

                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);

                        clsRizMetreUsers? RizMetreUsersCurrent =
                                        _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                        if (RizMetreUsersCurrent != null)
                        {
                            RizMetre.ID = RizMetreUsersCurrent.ID;
                            RizMetre.FBId = RizMetreUsersCurrent.FBId;
                            RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                            RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                            RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                            RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                            RizMetre.Type = RizMetreUsersCurrent.Type;
                            RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                            RizMetre.Des = RizMetreUsersCurrent.Des;

                            _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                            decimal? dMeghdarJoz = RizMetre.MeghdarJoz;
                            ////////////////////////////
                            ///حمل قبلی ویرایش میگردد///
                            ////////////////////////////
                            UpdateHamlDto UHamlDto = new UpdateHamlDto
                            {
                                BarAvordId = ItemHasCon.BarAvordId,
                                FBShomareh = ItemHasCon.FBShomareh,
                                RMShomareh = RizMetreUsersCurrent.Shomareh,
                                MeghdarJoz = dMeghdarJoz,
                                Year = Year,
                                NoeFB = NoeFBId,
                            };
                            HamlCommon.UpdateHaml(UHamlDto, _context);

                            _context.SaveChanges();
                        }
                        break;
                    }
                case 12:
                    {
                        string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];
                        string strCondition = Condition;
                        string[] strConditionSplit = strCondition.Split("_");
                        string strAddedItems = AddedItems;
                        string strFinalWorking = FinalWorking;
                        DataTable DtRizMetreUserses = new DataTable();
                        string strForItem = "";
                        string strUseItem = "";
                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        Guid guFBId = FBId;
                        strUseItem = strItemFBShomareh;
                        strForItem = UseItemForAdd;
                        ///ریز متره قبلی حذف میگردد

                        decimal? dErtefa1 = null;
                        string strConditionOp = strConditionSplit[0].Replace("x", OldRizMetre.Tool == null ? "0" : OldRizMetre.Tool.Value.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck2 = true;
                        if (strConditionSplit.Length == 2)
                        {
                            string strConditionOp2 = strConditionSplit[1].Replace("z", OldRizMetre.Ertefa == null ? "0" : OldRizMetre.Ertefa.Value.ToString().Trim());
                            blnCheck2 = StringToFormula.RelationalExpression2(strConditionOp2);
                            ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                            ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                            ///Ertefa-3
                            ///
                            strFinalWorking = strFinalWorking.Replace("z", OldRizMetre.Ertefa == null ? "0" : OldRizMetre.Ertefa.Value.ToString().Trim());
                            dErtefa1 = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                        }
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            if (blnCheck2)
                            {
                                string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                                                //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());

                                if (strFinalWorking != "")
                                {
                                    strFinalWorking = strFinalWorking.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");
                                    string strItemShomareh = AddedItems;
                                    strAddedItems = strAddedItems.Trim();
                                    clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

                                    //DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                                    Guid intFBId = new Guid();
                                    if (varFBUsersAdded != null)
                                    {
                                        intFBId = varFBUsersAdded.ID;

                                        long lngShomareh = OldRizMetre.Shomareh;

                                        List<clsRizMetreUsers> lstRizMetreUsers = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                                        if (lstRizMetreUsers.Count != 0)
                                        {
                                            _context.RizMetreUserses.RemoveRange(lstRizMetreUsers);

                                            ///////////
                                            //حذف حمل//
                                            ///////////
                                            DeleteHamlDto deleteHaml = new DeleteHamlDto
                                            {
                                                BarAvordId = ItemHasCon.BarAvordId,
                                                FBShomareh = varFBUsersAdded.Shomareh,
                                                RMShomareh = RizMetre.Shomareh,
                                                NoeFBId = NoeFBId
                                            };
                                            await HamlCommon.DeleteHaml(deleteHaml, _context);

                                            _context.SaveChanges();
                                        }
                                    }
                                }
                            }

                        }
                        //////////////
                        ///////////////
                        ///////////////
                        ///ریز متره جدید درج میگردد
                        //////////////
                        ////////////
                        /////////////
                        ///      
                        string strFinalWorkingNew = FinalWorking;
                        decimal? dErtefaNew1 = null;
                        string strConditionOpNew = strConditionSplit[0].Replace("x", RizMetre.Tool == null ? "0" : RizMetre.Tool.Value.ToString().Trim());
                        StringToFormula StringToFormulaNew = new StringToFormula();
                        bool blnCheckNew2 = true;
                        if (strConditionSplit.Length == 2)
                        {
                            string strConditionOpNew2 = strConditionSplit[1].Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
                            blnCheckNew2 = StringToFormulaNew.RelationalExpression2(strConditionOpNew2);
                            ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                            ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                            ///Ertefa-3
                            ///
                            strFinalWorkingNew = strFinalWorkingNew.Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
                            dErtefaNew1 = decimal.Parse(StringToFormula.Eval(strFinalWorkingNew).ToString("0.##"));
                        }
                        bool blnCheckNew = StringToFormulaNew.RelationalExpression2(strConditionOpNew);
                        if (blnCheckNew)
                        {
                            if (blnCheckNew2)
                            {
                                string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                                                //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                                clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                     .FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.FBShomareh.Trim() == strCurrentFBShomareh.Trim() &&
                                         x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                                if (currentItemsHasConditionAddedToFBs == null)
                                {
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = ItemHasCon.BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                    ItemsHasConditionAddedToFB.Meghdar = RizMetre.Tool != null ? RizMetre.Tool.Value : 0;
                                    ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;
                                    _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    _context.SaveChanges();
                                }

                                if (strFinalWorkingNew != "")
                                {
                                    strFinalWorkingNew = strFinalWorkingNew.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");
                                    string strItemShomareh = AddedItems;
                                    strAddedItems = strAddedItems.Trim();

                                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems).FirstOrDefault();

                                    Guid intFBId = new Guid();
                                    if (FBUser != null)
                                        intFBId = FBUser.ID;


                                    long lngShomareh = RizMetre.Shomareh;
                                    var varRizMetreUsers = (from RizMetreUsers in _context.RizMetreUserses
                                                            join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                                                            where RizMetreUsers.LevelNumber == RizMetre.LevelNumber
                                                            select new
                                                            {
                                                                RizMetreUsers.ID,
                                                                RizMetreUsers.Shomareh,
                                                                RizMetreUsers.Tedad,
                                                                RizMetreUsers.Tool,
                                                                RizMetreUsers.Arz,
                                                                RizMetreUsers.Ertefa,
                                                                RizMetreUsers.Vazn,
                                                                RizMetreUsers.Sharh,
                                                                RizMetreUsers.Des,
                                                                RizMetreUsers.FBId,
                                                                RizMetreUsers.OperationsOfHamlId,
                                                                RizMetreUsers.ForItem,
                                                                RizMetreUsers.Type,
                                                                RizMetreUsers.UseItem,
                                                                FB.BarAvordId
                                                            }).Where(x => x.FBId == FBId && x.ForItem == strItemFBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();
                                    DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);


                                    if (DtRizMetreUsers.Rows.Count == 0)
                                    {
                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                        RizMetreUsers.Shomareh = RizMetre.Shomareh;
                                        ShomareNew++;
                                        RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                        RizMetreUsers.Sharh = RizMetre.Sharh;// " آیتم " + strAddedItems; //DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();

                                        List<string> lst = new List<string>();
                                        for (int j = 0; j < strFieldsAdding.Length; j++)
                                        {
                                            lst.Add(strFieldsAdding[j]);
                                        }

                                        decimal? dTedad = null;
                                        decimal? dTool = null;
                                        decimal? dArz = null;
                                        //decimal? dErtefa = null;
                                        decimal? dVazn = null;

                                        var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dTedad = RizMetre.Tedad;
                                        }
                                        strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dTool = RizMetre.Tool;
                                        }
                                        strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dArz = RizMetre.Arz;
                                        }
                                        //strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                        //if (strCal.Count != 0)
                                        //{
                                        //    dErtefa = RM.Ertefa;
                                        //}
                                        strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                        if (strCal.Count != 0)
                                        {
                                            dVazn = RizMetre.Vazn;
                                        }

                                        RizMetreUsers.Tedad = dTedad;
                                        RizMetreUsers.Tool = dTool;
                                        RizMetreUsers.Arz = dArz;
                                        RizMetreUsers.Ertefa = dErtefaNew1;
                                        RizMetreUsers.Vazn = dVazn;
                                        RizMetreUsers.Des = RizMetre.Des; //DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh + " - ریز متره " + lngShomareh;
                                        RizMetreUsers.FBId = intFBId;
                                        RizMetreUsers.OperationsOfHamlId = 1;
                                        RizMetreUsers.Type = "2";
                                        RizMetreUsers.ForItem = strItemFBShomareh;
                                        RizMetreUsers.UseItem = "";
                                        RizMetreUsers.LevelNumber = RizMetre.LevelNumber;
                                        RizMetreUsers.InsertDateTime = Now;


                                        ///محاسبه مقدار جزء
                                        decimal dMeghdarJoz = 0;
                                        if (dTedad == null && dTool == null && dArz == null && dVazn == null)
                                            dMeghdarJoz = 0;
                                        else
                                            dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                            (dArz == null ? 1 : dArz.Value) * (dErtefaNew1 == null ? 1 : dErtefaNew1.Value) * (dVazn == null ? 1 : dVazn.Value);
                                        RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                        _context.RizMetreUserses.Add(RizMetreUsers);


                                        //Start///////////////////////
                                        ////////////حمل///////////////
                                        //////////////////////////////
                                        ///آیتم های حمل درج میگردد
                                        ///
                                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                                        {
                                            BarAvordUserId = ItemHasCon.BarAvordId,
                                            Year = Year,
                                            ItemFBShomareh = strAddedItems,
                                            NoeFBId = NoeFBId,
                                            Shomareh = RizMetre.Shomareh,
                                            MeghdarJoz = dMeghdarJoz,
                                            LevelNumber = 1,
                                        };
                                        await HamlCommon.SaveHaml(requestSaveHaml, _context);
                                        /////////////
                                        //////////////
                                        //////////////
                                        _context.SaveChanges();
                                    }

                                }
                            }

                        }

                        break;
                    }
                case 13:
                    {
                        //decimal Meghdar = ItemHasCon.Meghdar;
                        //decimal Meghdar2 = ItemHasCon.Meghdar2;
                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        //Guid BarAvordId = ItemHasCon.BarAvordId;
                        decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);


                        string[] strCondition = Condition.Split("_");
                        string strCondition1 = strCondition[0];
                        string strCondition2 = strCondition[1];
                        string strFinalWorking = FinalWorking;
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

                                var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                DataTable DtBA = clsConvert.ToDataTable(varBA);


                                clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == AddedItems).FirstOrDefault();

                                Guid intFBId = new Guid();
                                if (FBUser != null)
                                    intFBId = FBUser.ID;


                                string strShomareh1 = ItemHasCon.FBShomareh;

                                clsRizMetreUsers? currentRizMetre =
                                    _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                                if (currentRizMetre != null)
                                {
                                    decimal? Tedad = RizMetre.Tedad;
                                    decimal? Tool = RizMetre.Tool;
                                    decimal? Arz = RizMetre.Arz;
                                    decimal? Ertefa = RizMetre.Ertefa;

                                    currentRizMetre.Tedad = Tedad;
                                    currentRizMetre.Tool = Tool;
                                    currentRizMetre.Arz = Arz;
                                    currentRizMetre.Ertefa = Ertefa;
                                    currentRizMetre.Des = RizMetre.Des;
                                    currentRizMetre.Sharh = RizMetre.Sharh;

                                    ///محاسبه مقدار جزء
                                    decimal dMeghdarJoz = 0;
                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0)
                                        dMeghdarJoz = 0;
                                    else
                                        dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                        (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * dZaribVazn;

                                    currentRizMetre.MeghdarJoz = dMeghdarJoz;

                                    ////////////////////////////
                                    ///حمل قبلی ویرایش میگردد///
                                    ////////////////////////////
                                    UpdateHamlDto UHamlDto = new UpdateHamlDto
                                    {
                                        BarAvordId = ItemHasCon.BarAvordId,
                                        FBShomareh = ItemHasCon.FBShomareh,
                                        RMShomareh = currentRizMetre.Shomareh,
                                        MeghdarJoz = dMeghdarJoz,
                                        Year = Year,
                                        NoeFB = NoeFBId,
                                    };
                                    HamlCommon.UpdateHaml(UHamlDto, _context);

                                    _context.SaveChanges();
                                }
                            }
                            //}
                        }
                        //else
                        //{
                        //    return new JsonResult("CI");//CheckInput
                        //}

                        //else
                        //{
                        //    return new JsonResult("CI");//CheckInput
                        //}
                        break;
                    }
                case 14:
                    {
                        string[] FinalWorking1 = FinalWorking.Split(',');
                        FinalWorking = FinalWorking1[0];

                        string strFinalWorking = FinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        decimal? dMultiple = null;
                        StringToFormula StringToFormula = new StringToFormula();

                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            if (strFinalWorking != "")
                            {
                                dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                            }

                            string strAddedItems = AddedItems;
                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                            Guid intFBId = new Guid();
                            if (varFBUser != null)
                            {
                                intFBId = varFBUser.ID;

                                List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.NoeFB == NoeFBId && x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                                string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                                clsRizMetreUsers? RizMetreUsersCurrent = _context.RizMetreUserses.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh);
                                if (RizMetreUsersCurrent != null)
                                {
                                    //RizMetre.ID = RizMetreUsersCurrent.ID;
                                    //RizMetre.FBId = RizMetreUsersCurrent.FBId;
                                    //RizMetre.ForItem = RizMetreUsersCurrent.ForItem;
                                    //RizMetre.UseItem = RizMetreUsersCurrent.UseItem;
                                    //RizMetre.Shomareh = RizMetreUsersCurrent.Shomareh;
                                    //RizMetre.OperationsOfHamlId = RizMetreUsersCurrent.OperationsOfHamlId;
                                    //RizMetre.Type = RizMetreUsersCurrent.Type;
                                    //RizMetre.LevelNumber = RizMetreUsersCurrent.LevelNumber;
                                    //RizMetre.Des = RizMetreUsersCurrent.Des;


                                    if (lstCurrentItemsFields[0] != true)
                                        RizMetreUsersCurrent.Tedad = null;

                                    if (lstCurrentItemsFields[1] != true)
                                        RizMetreUsersCurrent.Tool = null;

                                    if (lstCurrentItemsFields[2] != true)
                                        RizMetreUsersCurrent.Arz = null;

                                    RizMetreUsersCurrent.Ertefa = dMultiple;

                                    if (lstCurrentItemsFields[4] != true)
                                        RizMetreUsersCurrent.Vazn = null;

                                    if (FinalWorking1.Length > 1)
                                    {
                                        string[] strVazn = FinalWorking1[1].Split('_');
                                        RizMetreUsersCurrent.Vazn = decimal.Parse(strVazn[1]);
                                    }

                                    //if (lstCurrentItemsFields[3] != true)
                                    //    RizMetre.Ertefa = null;

                                    //if (lstCurrentItemsFields[4] == true)
                                    //    RizMetre.Vazn = dMultiple;
                                    //else
                                    //    RizMetre.Vazn = null;

                                    //_context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);

                                    ///محاسبه مقدار جزء
                                    decimal? dMeghdarJoz = null;
                                    if (RizMetreUsersCurrent.Tedad == null && RizMetreUsersCurrent.Tool == null && RizMetreUsersCurrent.Arz == null && RizMetreUsersCurrent.Ertefa == null && RizMetreUsersCurrent.Vazn == null)
                                        dMeghdarJoz = null;
                                    else
                                        dMeghdarJoz =
                                            (RizMetreUsersCurrent.Tedad == null ? 1 : RizMetreUsersCurrent.Tedad.Value) * (RizMetreUsersCurrent.Tool == null ? 1 : RizMetreUsersCurrent.Tool.Value) *
                                            (RizMetreUsersCurrent.Arz == null ? 1 : RizMetreUsersCurrent.Arz.Value) * (RizMetreUsersCurrent.Ertefa == null ? 1 : RizMetreUsersCurrent.Ertefa.Value)
                                            * (RizMetreUsersCurrent.Vazn == null ? 1 : RizMetreUsersCurrent.Vazn.Value);

                                    RizMetreUsersCurrent.MeghdarJoz = dMeghdarJoz;

                                    //decimal? dMeghdarJoz = RizMetreUsersCurrent.MeghdarJoz;
                                    ////////////////////////////
                                    ///حمل قبلی ویرایش میگردد///
                                    ////////////////////////////
                                    UpdateHamlDto UHamlDto = new UpdateHamlDto
                                    {
                                        BarAvordId = ItemHasCon.BarAvordId,
                                        FBShomareh = ItemHasCon.FBShomareh,
                                        RMShomareh = RizMetreUsersCurrent.Shomareh,
                                        MeghdarJoz = dMeghdarJoz,
                                        Year = Year,
                                        NoeFB = NoeFBId,
                                    };
                                    HamlCommon.UpdateHaml(UHamlDto, _context);

                                    _context.SaveChanges();
                                }
                            }
                        }
                        break;
                    }
                case 15:
                    {


                        ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            RMIdForEdit = RMForEditId
                        };

                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        ConditionCaseCommonCaseResultDto blnResult = await CCC.Case15(RequestCase1, _context);
                        break;

                        /////////////
                        /////حذف قبلی ها
                        /////////////
                        //string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";

                        //string strCondition = Condition;
                        //string[] strConditionSplit = strCondition.Split("_");

                        //string strItemFBShomarehAdded = AddedItems;
                        ////string strFinalWorking = FinalWorking;
                        //DataTable DtRizMetreUserses = new DataTable();
                        //string strUseItem = UseItemForAdd;
                        //string strForItem = strItemFBShomarehAdded;

                        //string strItemShomareh1 = AddedItems + strCharacterPlus;

                        //clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh1).FirstOrDefault();

                        //Guid intFBId1 = new Guid();
                        //if (FBUser != null)
                        //    intFBId1 = FBUser.ID;

                        //string strShomareh1 = strItemFBShomarehAdded.Substring(0, 6);
                        //List<clsRizMetreUsers> lstRizMetreBase =
                        //                _context.RizMetreUserses.Where(x => x.FBId == intFBId1).ToList();

                        //List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                        //                _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                        //clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

                        //clsRizMetreUsers? RizMetreUsersBaseCurrent = lstRizMetreBase.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);


                        //List<Guid> lstRMIds = lstRizMetreBase.Select(x => x.ID).ToList();

                        //if (RizMetreUsersCurrent != null)
                        //{
                        //    if (lstRizMetreBase.Count == 1)
                        //    {
                        //        clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
                        //            _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
                        //                 && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

                        //        if (ItemsHasConditionAddedToFB != null)
                        //        {
                        //            _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
                        //        }
                        //    }
                        //    _context.RizMetreUserses.Remove(RizMetreUsersCurrent);
                        //}
                        //if (RizMetreUsersBaseCurrent != null)
                        //{
                        //    clsRizMetreUsersHistory? RMHistory = _context.RizMetreUsersHistories.FirstOrDefault(x => x.RizMetreUsersId == RizMetreUsersBaseCurrent.ID);

                        //    if (RMHistory != null)
                        //    {

                        //        _context.RizMetreUsersHistories.Remove(RMHistory);
                        //    }
                        //}

                        /////////////
                        ////حذف حمل//
                        /////////////
                        //DeleteHamlDto deleteHaml = new DeleteHamlDto
                        //{
                        //    BarAvordId = ItemHasCon.BarAvordId,
                        //    FBShomareh = strItemFBShomarehAdded,
                        //    RMShomareh = RizMetre.Shomareh,
                        //    NoeFBId = NoeFBId
                        //};
                        //await HamlCommon.DeleteHaml(deleteHaml, _context);

                        //////////////
                        /////درج جدید
                        //////////////
                        ////بررسی اینکه در تراش یا لکه گیری هست یا خیر
                        ////اگر باشد دیگر صعوبت درج نمیشود

                        //RizMetreCommon rizMetreCommon = new RizMetreCommon();
                        //long[] lngConditionGroupId = { 31, 4 };

                        //string strFBShomareh = ItemHasCon.FBShomareh.Trim();
                        //GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                        //{
                        //    ShomarehFB = strFBShomareh,
                        //    BarAvordUserId = guBAId,
                        //    NoeFB = NoeFBId,
                        //    Year = Year,
                        //    LevelNumber = LevelNumber,
                        //    ConditionGroupId = lngConditionGroupId
                        //};
                        //List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);

                        //string CharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";

                        ////string strCondition = Condition;
                        ////string[] strConditionSplit = strCondition.Split("_");

                        //string strFinalWorking1 = FinalWorking;
                        //string[] strFinalWorkingSplit = strFinalWorking1.Split('_');
                        //string strFinalWorking = strFinalWorkingSplit[0];
                        //decimal dBahayeVahedZarib = strFinalWorkingSplit.Length > 1 ? decimal.Parse(strFinalWorkingSplit[1]) : 0;


                        //string strDesOfAddingItems = DesOfAddingItems;

                        //Guid guFBId = FBId;// Guid.Parse(DtFB.Rows[0]["ID"].ToString());

                        //List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUserses = (from RUsers in _context.RizMetreUserses
                        //                                                                    join fb in _context.FBs on RUsers.FBId equals fb.ID
                        //                                                                    where RUsers.LevelNumber == LevelNumber
                        //                                                                    select new RizMetreUsersForItemsAddingToFBInputDto
                        //                                                                    {
                        //                                                                        Id = RUsers.ID,
                        //                                                                        Shomareh = RUsers.Shomareh,
                        //                                                                        Sharh = RUsers.Sharh,
                        //                                                                        Tedad = RUsers.Tedad,
                        //                                                                        Tool = RUsers.Tool,
                        //                                                                        Arz = RUsers.Arz,
                        //                                                                        Ertefa = RUsers.Ertefa,
                        //                                                                        Vazn = RUsers.Vazn,
                        //                                                                        Des = RUsers.Des,
                        //                                                                        ForItem = RUsers.ForItem,
                        //                                                                        Type = RUsers.Type,
                        //                                                                        FBId = RUsers.FBId
                        //                                                                    }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();



                        //List<RizMetreUsersForItemsAddingToFBInputDto> varRizMetreUsersesWithoutLakeAndTarash = new List<RizMetreUsersForItemsAddingToFBInputDto>();

                        //foreach (var RM1 in varRizMetreUserses)
                        //{
                        //    RizMetreForGetAndShowAddItemsDto? FindRM = lstRM.FirstOrDefault(x => x.Shomareh == RM1.Shomareh);
                        //    if (FindRM == null)
                        //    {
                        //        varRizMetreUsersesWithoutLakeAndTarash.Add(varRizMetreUserses.FirstOrDefault(x => x.Shomareh == RM1.Shomareh));
                        //    }
                        //}

                        //List<Guid> lstRMIds1 = varRizMetreUsersesWithoutLakeAndTarash.Select(x => x.Id).ToList();

                        //List<clsRizMetreUsers> lstRizMetreUser = _context.RizMetreUserses.Where(x => lstRMIds1.Contains(x.ID)).ToList();

                        //if (varRizMetreUsersesWithoutLakeAndTarash.Count != 0)
                        //{
                        //    var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
                        //                                     join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
                        //                                     where RizMetreUserses.LevelNumber == LevelNumber
                        //                                     select new
                        //                                     {
                        //                                         ID = RizMetreUserses.ID,
                        //                                         Shomareh = RizMetreUserses.Shomareh,
                        //                                         Tedad = RizMetreUserses.Tedad,
                        //                                         Tool = RizMetreUserses.Tool,
                        //                                         Arz = RizMetreUserses.Arz,
                        //                                         Ertefa = RizMetreUserses.Ertefa,
                        //                                         Vazn = RizMetreUserses.Vazn,
                        //                                         Des = RizMetreUserses.Des,
                        //                                         FBId = RizMetreUserses.FBId,
                        //                                         OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
                        //                                         ForItem = RizMetreUserses.ForItem,
                        //                                         Type = RizMetreUserses.Type,
                        //                                         UseItem = RizMetreUserses.UseItem,
                        //                                         BarAvordId = FB.BarAvordId
                        //                                     }).Where(x => x.ForItem == strForItem && x.Type == "2" && x.UseItem == strUseItem).ToList();

                        //    DataTable DtRizMetreUsersesCurrent = clsConvert.ToDataTable(varRizMetreUsersesCurrent);

                        //    List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.NoeFBId == NoeFBId).ToList();
                        //    string strItemShomareh = AddedItems + CharacterPlus;
                        //    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                        //    Guid intFBId = new Guid();
                        //    if (varFBUser == null)
                        //    {
                        //        clsFB FB = new clsFB();
                        //        FB.BarAvordId = guBAId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                        //        FB.Shomareh = strItemShomareh; //AddedItems.Trim();
                        //        FB.BahayeVahedZarib = FinalWorking.Length > 1 ? decimal.Parse(FinalWorking) : 0;
                        //        FB.BahayeVahedSharh = strDesOfAddingItems;
                        //        FB.NoeFBId = NoeFBId;
                        //        _context.FBs.Add(FB);
                        //        _context.SaveChanges();
                        //        intFBId = FB.ID;
                        //    }
                        //    else
                        //        intFBId = varFBUser.ID;

                        //    string strConditionOp = strConditionSplit[0].Replace("x", RizMetre.Arz.ToString().Trim());
                        //    StringToFormula StringToFormula = new StringToFormula();
                        //    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        //    if (blnCheck)
                        //    {
                        //        string strCurrentFBShomareh = strItemFBShomarehAdded;
                        //        //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                        //        clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                        //             .FirstOrDefault(x => x.BarAvordId == guBAId && x.FBShomareh == strCurrentFBShomareh &&
                        //                 x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                        //        if (currentItemsHasConditionAddedToFBs == null)
                        //        {
                        //            clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //            ItemsHasConditionAddedToFB.BarAvordId = guBAId;
                        //            ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                        //            ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                        //            ItemsHasConditionAddedToFB.Meghdar = RizMetre.Tool != null ? RizMetre.Tool.Value : 0;
                        //            ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;
                        //            _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                        //            _context.SaveChanges();
                        //        }

                        //        decimal? dArz = null;
                        //        if (RizMetre.Arz <= 2)
                        //            dArz = RizMetre.Arz;
                        //        else
                        //        {
                        //            dArz = 2;
                        //            ///بایستی سابقه در جدول سوابق ریزمتره دخیره گردد تا در صورتی که تیک تعریض برداشته شد، آیتم اصلی به حالت اولش برگردد
                        //            ///
                        //            clsRizMetreUsersHistory rizMetreUsersHistory = new clsRizMetreUsersHistory
                        //            {
                        //                RizMetreUsersId = RizMetre.ID,
                        //                Shomareh = RizMetre.Shomareh,
                        //                ShomarehNew = RizMetre.ShomarehNew,
                        //                Sharh = RizMetre.Sharh,
                        //                Tedad = RizMetre.Tedad,
                        //                Tool = RizMetre.Tool,
                        //                Arz = RizMetre.Arz,
                        //                Ertefa = RizMetre.Ertefa,
                        //                Vazn = RizMetre.Vazn,
                        //                MeghdarJoz = RizMetre.MeghdarJoz,
                        //                Des = RizMetre.Des
                        //            };
                        //            _context.RizMetreUsersHistories.Add(rizMetreUsersHistory);

                        //            RizMetre.Arz = 2;

                        //            decimal? dMeghdarJoz = null;

                        //            dMeghdarJoz = (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                        //            2 * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                        //            RizMetre.MeghdarJoz = dMeghdarJoz;
                        //        }


                        //        //string strItemShomareh = Dr[idr]["AddedItems"].ToString().Trim() + strCharacterPlus;


                        //        DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + RizMetre.Shomareh + " and FBId='" + FBId + "'");
                        //        if (DrRizMetreUsersesCurrent.Length == 0)
                        //        {
                        //            decimal? Tedad = RizMetre.Tedad == 0 ? 1 : RizMetre.Tedad;
                        //            decimal? Tool = RizMetre.Tool;
                        //            //decimal? Arz = RM.Arz;
                        //            decimal? Ertefa = RizMetre.Ertefa;
                        //            decimal? Vazn = RizMetre.Vazn;

                        //            clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //            RizMetreUserses.Shomareh = RizMetre.Shomareh;
                        //            ShomareNew++;
                        //            RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //            RizMetreUserses.Sharh = RizMetre.Sharh;
                        //            RizMetreUserses.Tedad = Tedad;
                        //            RizMetreUserses.Tool = Tool;
                        //            RizMetreUserses.Arz = dArz;
                        //            RizMetreUserses.Ertefa = Ertefa;
                        //            RizMetreUserses.Vazn = Vazn;
                        //            RizMetreUserses.Des = RizMetre.Des;

                        //            RizMetreUserses.FBId = intFBId;
                        //            RizMetreUserses.OperationsOfHamlId = 1;
                        //            RizMetreUserses.Type = "2";
                        //            RizMetreUserses.ForItem = strForItem;
                        //            RizMetreUserses.UseItem = strUseItem;
                        //            RizMetreUserses.LevelNumber = LevelNumber;
                        //            RizMetreUserses.InsertDateTime = Now;

                        //            ///محاسبه مقدار جزء
                        //            decimal? dMeghdarJoz = null;
                        //            if (Tedad == null && Tool == null && dArz == null && Ertefa == null && dArz == null && Vazn == null)
                        //                dMeghdarJoz = null;
                        //            else
                        //                dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                        //                (dArz == null ? 1 : dArz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);

                        //            RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //            _context.RizMetreUserses.Add(RizMetreUserses);

                        //            //Start///////////////////////
                        //            ////////////حمل///////////////
                        //            //////////////////////////////
                        //            ///آیتم های حمل درج میگردد
                        //            ///
                        //            SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //            {
                        //                BarAvordUserId = ItemHasCon.BarAvordId,
                        //                Year = Year,
                        //                ItemFBShomareh = strItemShomareh,
                        //                NoeFBId = NoeFBId,
                        //                Shomareh = RizMetre.Shomareh,
                        //                MeghdarJoz = dMeghdarJoz,
                        //                LevelNumber = 1,
                        //            };
                        //            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //            /////////////
                        //            //////////////
                        //            //////////////
                        //            _context.SaveChanges();
                        //        }
                        //    }
                        //}
                        //break;
                    }

                case 16:
                    {
                        ////
                        ///ابتدا ریزه متره موجود را حذف میکنیم
                        ///
                        string strAddedItems = AddedItems;
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems && x.NoeFBId == NoeFBId);
                        Guid intFBId = new Guid();
                        if (varFBUser != null)
                            intFBId = varFBUser.ID;

                        string strShomareh1 = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                        List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                        clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

                        List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

                        if (RizMetreUsersCurrent != null)
                        {
                            if (lstRizMetreBase.Count == 1)
                            {
                                clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
                                    _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId
                                         && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

                                if (ItemsHasConditionAddedToFB != null)
                                {
                                    _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
                                }
                            }
                            _context.RizMetreUserses.Remove(RizMetreUsersCurrent);


                            ///////////
                            //حذف حمل//
                            ///////////
                            DeleteHamlDto deleteHaml = new DeleteHamlDto
                            {
                                BarAvordId = ItemHasCon.BarAvordId,
                                FBShomareh = strAddedItems,
                                RMShomareh = RizMetre.Shomareh,
                                NoeFBId = NoeFBId
                            };
                            await HamlCommon.DeleteHaml(deleteHaml, _context);

                            _context.SaveChanges();
                        }


                        ///بررسی پخش، آبپاشی، تسطیح و کوبیدن قشر زیراساس  
                        /////در صورت یافتن ریز متره درج شده برای این شرط بایستی به 
                        ///اضافه بها بابت سختی اجرا در شانه سازی ها به عرض تا 2 متر
                        ///اضافه گردد
                        RizMetreCommon rizMetreCommon = new RizMetreCommon();
                        long[] lngConditionGroupId = { 12 };

                        string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];

                        //Guid BarAvordId = ItemHasCon.BarAvordId;

                        GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                        {
                            ShomarehFB = ItemHasCon.FBShomareh,
                            BarAvordUserId = BarAvordId,
                            NoeFB = NoeFBId,
                            Year = Year,
                            LevelNumber = LevelNumber,
                            ConditionGroupId = lngConditionGroupId
                        };
                        List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);


                        if (lstRM.Count != 0)
                        {
                            string strCurrentFBShomareh = ItemHasCon.FBShomareh; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                                                 //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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
                                ItemsHasConditionAddedToFB.Meghdar = ItemHasCon.Meghdar;
                                ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;

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
                                var varBA = _context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
                                DataTable DtBA = clsConvert.ToDataTable(varBA);

                                //Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                string strItemShomareh = AddedItems;
                                //var varFBUser1 = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                //DataTable DtFBUser = clsConvert.ToDataTable(varFBUser1);

                                if (varFBUser == null)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    FB.Shomareh = AddedItems;
                                    FB.BahayeVahedZarib = 0;
                                    FB.BahayeVahedSharh = "";
                                    FB.NoeFBId = NoeFBId;
                                    _context.FBs.Add(FB);
                                    _context.SaveChanges();
                                    intFBId = FB.ID;
                                }



                                //string[] FieldsAddingSplit = strFieldsAdding;

                                //string strShomareh1 = ItemHasCon.FBShomareh;
                                //Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
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

                                foreach (var itemRM in lstRM)
                                {
                                    string strCondition = Condition;

                                    string strConditionOp = strCondition.Replace("x", itemRM.Arz.ToString().Trim());
                                    StringToFormula StringToFormula = new StringToFormula();
                                    bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                    if (blnCheck)
                                    {
                                        DataRow[] DrRizMetreUsersesCurrent = DtRizMetreUsersesCurrent.Select("shomareh=" + itemRM.Shomareh);

                                        if (DrRizMetreUsersesCurrent.Length == 0)
                                        {
                                            decimal? Tedad = itemRM.Tedad;
                                            decimal? Tool = itemRM.Tool;
                                            decimal? Arz = itemRM.Arz;
                                            decimal? Ertefa = itemRM.Ertefa;
                                            decimal? Vazn = itemRM.Vazn;

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
                                            RizMetreUserses.Des = itemRM.Des;
                                            RizMetreUserses.FBId = intFBId;
                                            RizMetreUserses.OperationsOfHamlId = 1;
                                            RizMetreUserses.Type = "2";
                                            RizMetreUserses.ForItem = ItemHasCon.FBShomareh;
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
                                            //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                            //RizMetreUserses.ConditionContextId = ConditionContextId;

                                            _context.RizMetreUserses.Add(RizMetreUserses);

                                            //Start///////////////////////
                                            ////////////حمل///////////////
                                            //////////////////////////////
                                            ///آیتم های حمل درج میگردد
                                            ///
                                            SaveHamlDto requestSaveHaml = new SaveHamlDto
                                            {
                                                BarAvordUserId = ItemHasCon.BarAvordId,
                                                Year = Year,
                                                ItemFBShomareh = strItemShomareh,
                                                NoeFBId = NoeFBId,
                                                Shomareh = RizMetre.Shomareh,
                                                MeghdarJoz = dMeghdarJoz,
                                                LevelNumber = 1,
                                            };
                                            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                                            /////////////
                                            //////////////
                                            //////////////


                                            _context.SaveChanges();
                                            //RizMetreUserses.Save();
                                        }
                                    }
                                }
                            }
                        }

                        break;
                    }
                case 17:
                    {
                        ConditionCaseCommonCase1Dto RequestCase1 = new ConditionCaseCommonCase1Dto
                        {
                            strFinalWorking = FinalWorking,
                            Meghdar = Meghdar,
                            Meghdar2 = Meghdar2,
                            strCondition = Condition,
                            strItemsFBShomareh = strItemFBShomareh,
                            lngItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId,
                            ConditionGroupId = ConditionGroupId,
                            BarAvordId = BarAvordId,
                            NoeFBId = NoeFBId,
                            strCharacterPlus = strCharacterPlus,
                            FBShomarehAddedItem = AddedItems,
                            DesOfAddingItems = DesOfAddingItems,
                            BaseFBId = FBId,
                            Year = Year,
                            FieldsAdding = FieldsAdding,
                            ShomarehNew = ShomareNew,
                            Now = Now,
                            ConditionContextRel = ConditionContextRel,
                            ConditionContextId = ConditionContextId,
                            blnCheckIsExistErrors = "",
                            LevelNumber = LevelNumber,
                            blnCheckAgain = blnCheckAgain,
                            RMIdForEdit = RMForEditId
                        };

                        ConditionCaseCommon CCC = new ConditionCaseCommon();
                        ConditionCaseCommonCaseResultDto blnResult = await CCC.Case17(RequestCase1, _context);

                        break;


                        //string strErtefa = OldRizMetre.Ertefa != null ? OldRizMetre.Ertefa.Value.ToString() : "0";

                        //string[] strFinalWorkingBahayeVahedZarib = FinalWorking.Split(',');
                        //string[] strFinalWorkingSplit1 = FinalWorking.Split(',')[0].Split('_');

                        //int intDivided = strFinalWorkingSplit1.Length > 1 ? int.Parse(strFinalWorkingSplit1[1]) : 1;

                        //string[] strFinalWorkingSplit = strFinalWorkingSplit1[0].Split('-');


                        //string CharForReplace = "";
                        //string strTAEForReoplace = "";



                        //string strCurrentFBShomareh = ItemHasCon.FBShomareh.Trim(); //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                        //string strUseItem = UseItemForAdd;
                        //string strForItem = strCurrentFBShomareh;

                        ////long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                        //clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                        //                 .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh == strCurrentFBShomareh &&
                        //                     x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);


                        //Guid currentItemsHasConditionAddedToFBId = new Guid();
                        //bool blnCheckSave = false;
                        //if (currentItemsHasConditionAddedToFBs == null)
                        //{
                        //    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                        //    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                        //    ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                        //    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                        //    ItemsHasConditionAddedToFB.Meghdar = 0;
                        //    ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;

                        //    try
                        //    {
                        //        _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                        //        //_context.SaveChanges();
                        //        blnCheckSave = true;
                        //        currentItemsHasConditionAddedToFBId = ItemsHasConditionAddedToFB.ID;
                        //    }
                        //    catch (Exception)
                        //    {
                        //        blnCheckSave = false;
                        //    }
                        //}
                        //else
                        //{
                        //    blnCheckSave = true;
                        //    currentItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBs.ID;
                        //}

                        //if (blnCheckSave)
                        //{

                        //    string[] strFinalWorkingBahayeVahedZaribUp = FinalWorking.Split(',');
                        //    string[] strFinalWorkingSplitUp1 = FinalWorking.Split(',')[0].Split('_');
                        //    int intDividedUp = strFinalWorkingSplitUp1.Length > 1 ? int.Parse(strFinalWorkingSplitUp1[1]) : 1;
                        //    string[] strFinalWorkingSplitUp = strFinalWorkingSplitUp1[0].Split('-');

                        //    List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId).ToList();
                        //    string strItemShomareh = AddedItems.Trim() + strCharacterPlus.Trim();

                        //    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);
                        //    Guid intFBId = new Guid();
                        //    if (varFBUser != null)
                        //        intFBId = varFBUser.ID;

                        //    bool blnCheckEqual = false;

                        //    string CharForReplaceUp = "";
                        //    string strTAEForReoplaceUp = "";

                        //    string[] strFinalWorkingSplitEqual = strFinalWorkingSplit[0].Split('=');
                        //    if (strFinalWorkingSplitEqual.Length > 1)
                        //    {
                        //        CharForReplace = strFinalWorkingSplitEqual[0].Trim();
                        //        strTAEForReoplace = strFinalWorkingSplitEqual[1].ToString().Trim();
                        //        blnCheckEqual = true;
                        //    }


                        //    if (blnCheckEqual)
                        //    {
                        //        string strConditionOp = Condition.Replace(CharForReplace, RizMetre.Ertefa.ToString());
                        //        StringToFormula stringToFormula = new StringToFormula();
                        //        bool blnCheck = stringToFormula.RelationalExpression2(strConditionOp);
                        //        if (blnCheck)
                        //        {
                        //            decimal? dErtefaUp = decimal.Parse(strTAEForReoplace);

                        //            decimal? Tedad = RizMetre.Tedad == 0 ? 1 : RizMetre.Tedad;
                        //            decimal? Tool = RizMetre.Tool;
                        //            decimal? Arz = RizMetre.Arz;
                        //            decimal? Vazn = RizMetre.Vazn;

                        //            clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                        //            RizMetreUserses1.Shomareh = RizMetre.Shomareh;
                        //            ShomareNew++;
                        //            RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                        //            RizMetreUserses1.Sharh = RizMetre.Sharh;
                        //            RizMetreUserses1.Tedad = Tedad;
                        //            RizMetreUserses1.Tool = Tool;
                        //            RizMetreUserses1.Arz = Arz;
                        //            RizMetreUserses1.Ertefa = dErtefaUp;
                        //            RizMetreUserses1.Vazn = Vazn;
                        //            RizMetreUserses1.Des = RizMetre.Des;

                        //            RizMetreUserses1.FBId = intFBId;
                        //            RizMetreUserses1.OperationsOfHamlId = 1;
                        //            RizMetreUserses1.Type = "2";
                        //            RizMetreUserses1.ForItem = strForItem;
                        //            RizMetreUserses1.UseItem = strUseItem;
                        //            RizMetreUserses1.LevelNumber = LevelNumber;
                        //            RizMetreUserses1.InsertDateTime = Now;


                        //            decimal? dMeghdarJoz1 = null;
                        //            if (Tedad == null && Tool == null && Arz == null && dErtefaUp == null && Vazn == null)
                        //                dMeghdarJoz1 = null;
                        //            else
                        //                dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                        //                (Arz == null ? 1 : Arz.Value) * dErtefaUp * (Vazn == null ? 1 : Vazn.Value);
                        //            RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                        //            _context.RizMetreUserses.Add(RizMetreUserses1);

                        //            _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                                new clsItemsHasConditionAddedToFBRizMetre
                        //                                {
                        //                                    ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                                    RizMetreId = RizMetreUserses1.ID,
                        //                                    BaseRizMetreId = RizMetre.ID
                        //                                });

                        //            //dSumAllRizMetre += dMeghdarJoz ?? 0;

                        //            //Start///////////////////////
                        //            ////////////حمل///////////////
                        //            //////////////////////////////
                        //            ///آیتم های حمل درج میگردد
                        //            ///

                        //            SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //            {
                        //                BarAvordUserId = BarAvordId,
                        //                Year = Year,
                        //                ItemFBShomareh = strItemShomareh,
                        //                NoeFBId = NoeFBId,
                        //                //BarAvordHamlId = gBarAvordHamlId,
                        //                Shomareh = RizMetreUserses1.Shomareh,
                        //                MeghdarJoz = dMeghdarJoz1,
                        //                LevelNumber = 1,
                        //            };
                        //            await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //        }
                        //    }
                        //    else
                        //    {


                        //        decimal? dErtefaUp = null;
                        //        decimal? dToolUp = null;

                        //        if (strFinalWorkingSplitUp[0] == "z")
                        //        {
                        //            CharForReplaceUp = "z";
                        //            strTAEForReoplaceUp = RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "";
                        //            dErtefaUp = intDividedUp;
                        //            dToolUp = RizMetre.Tool;
                        //        }
                        //        else if (strFinalWorkingSplitUp[0] == "x")
                        //        {
                        //            CharForReplaceUp = "x";
                        //            strTAEForReoplaceUp = RizMetre.Tool != null ? RizMetre.Tool.Value.ToString().Trim() : "";
                        //            dErtefaUp = RizMetre.Ertefa;
                        //            dToolUp = intDivided;
                        //        }
                        //        string strConditionOp = Condition.Replace(CharForReplaceUp, strTAEForReoplaceUp);
                        //        StringToFormula stringToFormula = new StringToFormula();
                        //        bool blnCheck = stringToFormula.RelationalExpression2(strConditionOp);
                        //        if (blnCheck)
                        //        {
                        //            if (FinalWorking != "")
                        //            {
                        //                string strFinalWorking11 = strFinalWorkingSplitUp1[0].Replace(CharForReplaceUp, strTAEForReoplaceUp);

                        //                int intReapetCount = (int.Parse(Math.Floor(stringToFormula.Eval(strFinalWorking11)).ToString("n0")) / intDivided);

                        //                //به تعداد intReapetCount
                        //                //رکورد با ارتفاع intDivided درج می گردد
                        //                decimal? Tedad = RizMetre.Tedad == 0 ? 1 : RizMetre.Tedad;
                        //                //decimal? Tool = RM.Tool;
                        //                decimal? Arz = RizMetre.Arz;
                        //                decimal? Vazn = 1;
                        //                for (int i = 0; i < intReapetCount; i++)
                        //                {
                        //                    //decimal dErtefa = intDivided;
                        //                    clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                        //                    RizMetreUserses1.Shomareh = RizMetre.Shomareh;
                        //                    ShomareNew++;
                        //                    RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                        //                    RizMetreUserses1.Sharh = RizMetre.Sharh;
                        //                    RizMetreUserses1.Tedad = Tedad;
                        //                    RizMetreUserses1.Tool = dToolUp;
                        //                    RizMetreUserses1.Arz = Arz;
                        //                    RizMetreUserses1.Ertefa = dErtefaUp;
                        //                    RizMetreUserses1.Vazn = Vazn;
                        //                    RizMetreUserses1.Des = RizMetre.Des;

                        //                    RizMetreUserses1.FBId = intFBId;
                        //                    RizMetreUserses1.OperationsOfHamlId = 1;
                        //                    RizMetreUserses1.Type = "2";
                        //                    RizMetreUserses1.ForItem = strForItem;
                        //                    RizMetreUserses1.UseItem = strUseItem;
                        //                    RizMetreUserses1.LevelNumber = LevelNumber;
                        //                    RizMetreUserses1.InsertDateTime = Now;

                        //                    //RizMetreUserses1.ConditionContextRel = ConditionContextRel;
                        //                    //RizMetreUserses1.ConditionContextId = ConditionContextId;

                        //                    ///محاسبه مقدار جزء
                        //                    decimal? dMeghdarJoz1 = null;
                        //                    if (Tedad == null && dToolUp == null && Arz == null && dErtefaUp == null && Vazn == null)
                        //                        dMeghdarJoz1 = null;
                        //                    else
                        //                        dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (dToolUp == null ? 1 : dToolUp.Value) *
                        //                        (Arz == null ? 1 : Arz.Value) * dErtefaUp * (Vazn == null ? 1 : Vazn.Value);
                        //                    RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                        //                    Vazn++;
                        //                    _context.RizMetreUserses.Add(RizMetreUserses1);

                        //                    _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                                new clsItemsHasConditionAddedToFBRizMetre
                        //                                {
                        //                                    ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                                    RizMetreId = RizMetreUserses1.ID,
                        //                                    BaseRizMetreId = RizMetre.ID

                        //                                });

                        //                    //Start///////////////////////
                        //                    ////////////حمل///////////////
                        //                    //////////////////////////////
                        //                    ///آیتم های حمل درج میگردد
                        //                    ///
                        //                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //                    {
                        //                        BarAvordUserId = ItemHasCon.BarAvordId,
                        //                        Year = Year,
                        //                        ItemFBShomareh = strItemShomareh,
                        //                        NoeFBId = NoeFBId,
                        //                        Shomareh = RizMetre.Shomareh,
                        //                        MeghdarJoz = dMeghdarJoz1,
                        //                        LevelNumber = 1,
                        //                    };
                        //                    await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //                    /////////////
                        //                    //////////////
                        //                    //////////////
                        //                }
                        //                int intRemain = intReapetCount * intDivided;

                        //                string strFinalWorking1 = strFinalWorkingSplitUp1[0].Replace(CharForReplaceUp, strTAEForReoplaceUp);

                        //                decimal? dLastErtefa = RizMetre.Ertefa;
                        //                decimal? dLastTool = RizMetre.Tool;
                        //                //رکورد آخری هم بنا به شرط فاینل ارتفاع یا طول درج میگردد
                        //                if (strFinalWorkingSplitUp[0] == "z")
                        //                {
                        //                    dLastErtefa = decimal.Parse(stringToFormula.Eval(strFinalWorking1).ToString("0.##"));
                        //                    dLastErtefa -= intRemain;
                        //                    if (dLastErtefa > 0)
                        //                    {

                        //                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //                        RizMetreUserses.Shomareh = RizMetre.Shomareh;
                        //                        ShomareNew++;
                        //                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //                        RizMetreUserses.Sharh = RizMetre.Sharh;
                        //                        RizMetreUserses.Tedad = Tedad;
                        //                        RizMetreUserses.Tool = dLastTool;
                        //                        RizMetreUserses.Arz = Arz;
                        //                        RizMetreUserses.Ertefa = dLastErtefa;
                        //                        RizMetreUserses.Vazn = Vazn;
                        //                        RizMetreUserses.Des = RizMetre.Des;

                        //                        RizMetreUserses.FBId = intFBId;
                        //                        RizMetreUserses.OperationsOfHamlId = 1;
                        //                        RizMetreUserses.Type = "2";
                        //                        RizMetreUserses.ForItem = strForItem;
                        //                        RizMetreUserses.UseItem = strUseItem;
                        //                        RizMetreUserses.LevelNumber = LevelNumber;
                        //                        RizMetreUserses.InsertDateTime = Now;

                        //                        //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                        //                        //RizMetreUserses.ConditionContextId = ConditionContextId;

                        //                        ///محاسبه مقدار جزء
                        //                        decimal? dMeghdarJoz = null;
                        //                        if (Tedad == null && dLastTool == null && dLastErtefa == null && Arz == null && Vazn == null)
                        //                            dMeghdarJoz = null;
                        //                        else
                        //                            dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (dLastTool == null ? 1 : dLastTool.Value) *
                        //                            (Arz == null ? 1 : Arz.Value) * (dLastErtefa == null ? 1 : dLastErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                        //                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //                        _context.RizMetreUserses.Add(RizMetreUserses);

                        //                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                                new clsItemsHasConditionAddedToFBRizMetre
                        //                                {
                        //                                    ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                                    RizMetreId = RizMetreUserses.ID,
                        //                                    BaseRizMetreId = RizMetre.ID

                        //                                });

                        //                        //Start///////////////////////
                        //                        ////////////حمل///////////////
                        //                        //////////////////////////////
                        //                        ///آیتم های حمل درج میگردد
                        //                        ///
                        //                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //                        {
                        //                            BarAvordUserId = ItemHasCon.BarAvordId,
                        //                            Year = Year,
                        //                            ItemFBShomareh = strItemShomareh,
                        //                            NoeFBId = NoeFBId,
                        //                            Shomareh = RizMetre.Shomareh,
                        //                            MeghdarJoz = dMeghdarJoz,
                        //                            LevelNumber = 1,
                        //                        };
                        //                        await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //                        /////////////
                        //                        //////////////
                        //                        //////////////
                        //                    }
                        //                }
                        //                else if (strFinalWorkingSplitUp[0] == "x")
                        //                {
                        //                    dLastTool = decimal.Parse(stringToFormula.Eval(strFinalWorking1).ToString("0.##"));
                        //                    dLastTool -= intRemain;
                        //                    if (dLastTool > 0)
                        //                    {

                        //                        clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                        //                        RizMetreUserses.Shomareh = RizMetre.Shomareh;
                        //                        ShomareNew++;
                        //                        RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                        //                        RizMetreUserses.Sharh = RizMetre.Sharh;
                        //                        RizMetreUserses.Tedad = Tedad;
                        //                        RizMetreUserses.Tool = dLastTool;
                        //                        RizMetreUserses.Arz = Arz;
                        //                        RizMetreUserses.Ertefa = dLastErtefa;
                        //                        RizMetreUserses.Vazn = Vazn;
                        //                        RizMetreUserses.Des = RizMetre.Des;

                        //                        RizMetreUserses.FBId = intFBId;
                        //                        RizMetreUserses.OperationsOfHamlId = 1;
                        //                        RizMetreUserses.Type = "2";
                        //                        RizMetreUserses.ForItem = strForItem;
                        //                        RizMetreUserses.UseItem = strUseItem;
                        //                        RizMetreUserses.LevelNumber = LevelNumber;
                        //                        RizMetreUserses.InsertDateTime = Now;

                        //                        //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                        //                        //RizMetreUserses.ConditionContextId = ConditionContextId;

                        //                        ///محاسبه مقدار جزء
                        //                        decimal? dMeghdarJoz = null;
                        //                        if (Tedad == null && dLastTool == null && dLastErtefa == null && Arz == null && Vazn == null)
                        //                            dMeghdarJoz = null;
                        //                        else
                        //                            dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (dLastTool == null ? 1 : dLastTool.Value) *
                        //                            (Arz == null ? 1 : Arz.Value) * (dLastErtefa == null ? 1 : dLastErtefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                        //                        RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                        //                        _context.RizMetreUserses.Add(RizMetreUserses);

                        //                        _context.ItemsHasConditionAddedToFBRizMetres.Add(
                        //                                new clsItemsHasConditionAddedToFBRizMetre
                        //                                {
                        //                                    ItemsHasConditionAddedToFBId = currentItemsHasConditionAddedToFBId,
                        //                                    RizMetreId = RizMetreUserses.ID,
                        //                                    BaseRizMetreId = RizMetre.ID

                        //                                });
                        //                        //Start///////////////////////
                        //                        ////////////حمل///////////////
                        //                        //////////////////////////////
                        //                        ///آیتم های حمل درج میگردد
                        //                        ///
                        //                        SaveHamlDto requestSaveHaml = new SaveHamlDto
                        //                        {
                        //                            BarAvordUserId = ItemHasCon.BarAvordId,
                        //                            Year = Year,
                        //                            ItemFBShomareh = strItemShomareh,
                        //                            NoeFBId = NoeFBId,
                        //                            Shomareh = RizMetre.Shomareh,
                        //                            MeghdarJoz = dMeghdarJoz,
                        //                            LevelNumber = 1,
                        //                        };
                        //                        await HamlCommon.SaveHaml(requestSaveHaml, _context);
                        //                        /////////////
                        //                        //////////////
                        //                        //////////////


                        //                    }
                        //                }


                        //                await _context.SaveChangesAsync();
                        //                //RizMetreUserses.Save();
                        //                //}
                        //            }
                        //        }
                        //    }
                        //}

                        //break;
                    }
                case 18:
                    {

                        //Guid BarAvordId = ItemHasCon.BarAvordId;

                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        string strCondition = Condition;
                        string strFinalWorking = FinalWorking;
                        string strAddedItems = AddedItems;
                        string strUseItem = UseItemForAdd;
                        string strForItem = strItemFBShomareh;
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            //////////////////
                            ///حذف قبلی ها///
                            /////////////////
                            ///

                            List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();

                            if (strFinalWorking != "")
                            {
                                string strItemShomareh = strAddedItems;
                                clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                Guid intFBId = new Guid();
                                if (varFBUser != null)
                                    intFBId = varFBUser.ID;

                                List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                                    _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strItemFBShomareh && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

                                _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);


                                ///////////
                                //حذف حمل//
                                ///////////
                                DeleteHamlDto deleteHaml = new DeleteHamlDto
                                {
                                    BarAvordId = ItemHasCon.BarAvordId,
                                    FBShomareh = strItemShomareh,
                                    RMShomareh = RizMetre.Shomareh,
                                    NoeFBId = NoeFBId
                                };
                                await HamlCommon.DeleteHaml(deleteHaml, _context);
                            }

                            //////////////
                            ///درج جدید///
                            //////////////

                            if (strFinalWorking != "")
                            {
                                string[] strFinalWorkingSplit = strFinalWorking.Split('-');
                                int intReapetCount = (int.Parse(strFinalWorkingSplit[1]) - 20) / 5;
                                string strItemShomareh = strAddedItems;
                                clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                Guid intFBId = new Guid();
                                if (varFBUser == null)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId;
                                    FB.Shomareh = strAddedItems;
                                    FB.BahayeVahedZarib = 0;
                                    FB.NoeFBId = NoeFBId;
                                    _context.FBs.Add(FB);
                                    _context.SaveChanges();
                                    intFBId = FB.ID;
                                }
                                else
                                    intFBId = varFBUser.ID;

                                //به تعداد intReapetCount
                                //رکورد با ارتفاع 5 درج می گردد
                                decimal? Tedad = RizMetre.Tedad == 0 ? 1 : RizMetre.Tedad;
                                decimal? Tool = RizMetre.Tool;
                                decimal? Arz = RizMetre.Arz;
                                decimal? Vazn = 1;
                                for (int i = 0; i < intReapetCount; i++)
                                {
                                    decimal dErtefa = 5;
                                    clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                                    RizMetreUserses1.Shomareh = RizMetre.Shomareh;
                                    ShomareNew++;
                                    RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUserses1.Sharh = RizMetre.Sharh;
                                    RizMetreUserses1.Tedad = Tedad;
                                    RizMetreUserses1.Tool = Tool;
                                    RizMetreUserses1.Arz = Arz;
                                    RizMetreUserses1.Ertefa = dErtefa;
                                    RizMetreUserses1.Vazn = Vazn++;
                                    RizMetreUserses1.Des = RizMetre.Des;

                                    RizMetreUserses1.FBId = intFBId;
                                    RizMetreUserses1.OperationsOfHamlId = 1;
                                    RizMetreUserses1.Type = "2";
                                    RizMetreUserses1.ForItem = strForItem;
                                    RizMetreUserses1.UseItem = strUseItem;
                                    RizMetreUserses1.LevelNumber = LevelNumber;
                                    RizMetreUserses1.InsertDateTime = Now;

                                    //RizMetreUserses1.ConditionContextRel = ConditionContextRel;
                                    //RizMetreUserses1.ConditionContextId = ConditionContextId;

                                    ///محاسبه مقدار جزء
                                    decimal? dMeghdarJoz1 = null;
                                    if (Tedad == null && Tool == null && Arz == null && Vazn == null)
                                        dMeghdarJoz1 = null;
                                    else
                                        dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                        (Arz == null ? 1 : Arz.Value) * dErtefa * (Vazn == null ? 1 : Vazn.Value);
                                    RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                                    _context.RizMetreUserses.Add(RizMetreUserses1);
                                }


                                strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());

                                //رکورد آخری هم بنا به شرط فاینل ارتفاع درج میگردد
                                decimal Ertefa = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));

                                clsRizMetreUsers RizMetreUserses = new clsRizMetreUsers();
                                RizMetreUserses.Shomareh = RizMetre.Shomareh;
                                ShomareNew++;
                                RizMetreUserses.ShomarehNew = ShomareNew.ToString();

                                RizMetreUserses.Sharh = RizMetre.Sharh;
                                RizMetreUserses.Tedad = Tedad;
                                RizMetreUserses.Tool = Tool;
                                RizMetreUserses.Arz = Arz;
                                RizMetreUserses.Ertefa = Ertefa;
                                RizMetreUserses.Vazn = Vazn;
                                RizMetreUserses.Des = RizMetre.Des;

                                RizMetreUserses.FBId = intFBId;
                                RizMetreUserses.OperationsOfHamlId = 1;
                                RizMetreUserses.Type = "2";
                                RizMetreUserses.ForItem = strForItem;
                                RizMetreUserses.UseItem = strUseItem;
                                RizMetreUserses.LevelNumber = LevelNumber;
                                RizMetreUserses.InsertDateTime = Now;

                                //RizMetreUserses.ConditionContextRel = ConditionContextRel;
                                //RizMetreUserses.ConditionContextId = ConditionContextId;

                                ///محاسبه مقدار جزء
                                decimal? dMeghdarJoz = null;
                                if (Tedad == null && Tool == null && Arz == null && Vazn == null)
                                    dMeghdarJoz = null;
                                else
                                    dMeghdarJoz = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                    (Arz == null ? 1 : Arz.Value) * Ertefa * (Vazn == null ? 1 : Vazn.Value);
                                RizMetreUserses.MeghdarJoz = dMeghdarJoz;

                                _context.RizMetreUserses.Add(RizMetreUserses);

                                //Start///////////////////////
                                ////////////حمل///////////////
                                //////////////////////////////
                                ///آیتم های حمل درج میگردد
                                ///
                                SaveHamlDto requestSaveHaml = new SaveHamlDto
                                {
                                    BarAvordUserId = ItemHasCon.BarAvordId,
                                    Year = Year,
                                    ItemFBShomareh = strItemShomareh,
                                    NoeFBId = NoeFBId,
                                    Shomareh = RizMetre.Shomareh,
                                    MeghdarJoz = dMeghdarJoz,
                                    LevelNumber = 1,
                                };
                                await HamlCommon.SaveHaml(requestSaveHaml, _context);
                                /////////////
                                //////////////
                                //////////////
                            }
                        }
                        break;
                    }

                case 19:
                    {

                        /////////////
                        /////حذف قبلی ها
                        /////////////

                        string[] strCondition2Condition = Condition.Trim().Split(',');
                        string strConditionY = strCondition2Condition[0].Trim();
                        string strConditionZ = strCondition2Condition[1].Trim();

                        string strConditionOpY = strConditionY.Replace("y", OldRizMetre.Arz != null ? OldRizMetre.Arz.Value.ToString().Trim() : "");
                        string strConditionOpZ = strConditionZ.Replace("z", OldRizMetre.Ertefa != null ? OldRizMetre.Ertefa.Value.ToString().Trim() : "");

                        StringToFormula stringToFormula = new StringToFormula();
                        bool blnCheckY = stringToFormula.RelationalExpression2(strConditionOpY);
                        bool blnCheckZ = stringToFormula.RelationalExpression2(strConditionOpZ);
                        if (blnCheckY && blnCheckZ)
                        {
                            string strItemShomareh1 = AddedItems + strCharacterPlus;

                            clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh1).FirstOrDefault();

                            Guid intFBId1 = new Guid();
                            if (FBUser != null)
                                intFBId1 = FBUser.ID;

                            string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);

                            List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                                            _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

                            _context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);


                            ///////////
                            //حذف حمل//
                            ///////////
                            DeleteHamlDto deleteHaml = new DeleteHamlDto
                            {
                                BarAvordId = ItemHasCon.BarAvordId,
                                FBShomareh = strItemShomareh1,
                                RMShomareh = RizMetre.Shomareh,
                                NoeFBId = NoeFBId
                            };
                            await HamlCommon.DeleteHaml(deleteHaml, _context);

                        }

                        //    string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus.Trim() : "";

                        //string[] strCondition2Condition = Condition.Trim().Split(',');
                        //string strConditionY = strCondition2Condition[0].Trim();
                        //string strConditionZ = strCondition2Condition[1].Trim();

                        //string strConditionOpY = strConditionY.Replace("y", RizMetre.Tool != null ? RizMetre.Tool.Value.ToString().Trim() : "");
                        //string strConditionOpZ = strConditionZ.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");

                        //StringToFormula stringToFormula = new StringToFormula();
                        //bool blnCheckX = stringToFormula.RelationalExpression2(strConditionOpY);
                        //bool blnCheckZ = stringToFormula.RelationalExpression2(strConditionOpZ);
                        ////if (blnCheckX && blnCheckZ)
                        ////{
                        //string strItemShomareh1 = AddedItems + strCharacterPlus;

                        //clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh1).FirstOrDefault();

                        //Guid intFBId1 = new Guid();
                        //if (FBUser != null)
                        //    intFBId1 = FBUser.ID;

                        //string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);

                        //List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                        //                _context.RizMetreUserses.Where(x => x.FBId == intFBId1 && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

                        //_context.RizMetreUserses.RemoveRange(lstRizMetreUsersCurrent);
                        //_context.SaveChanges();

                        //}

                        //////////
                        ////درج جدید
                        //////////
                        //Guid BarAvordId = ItemHasCon.BarAvordId;

                        //string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();
                        string strUseItem = UseItemForAdd;
                        string strForItem = strItemFBShomareh;

                        string strCurrentFBShomareh = ItemHasCon.FBShomareh; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                                                             //long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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
                            ItemsHasConditionAddedToFB.Meghdar = 0;
                            ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;

                            try
                            {
                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
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
                            List<clsFB> lstFBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId).ToList();
                            string strItemShomareh = AddedItems.Trim() + strCharacterPlus.Trim();
                            string strDesOfAddingItems = DesOfAddingItems;//.Replace("x", Meghdar.ToString().Trim()).Replace("y", Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim()).Replace("z", (dPercent * 100).ToString("0.##")).Trim();

                            string strConditionOpForAddY = strConditionY.Replace("y", RizMetre.Arz != null ? RizMetre.Arz.Value.ToString().Trim() : "");
                            string strConditionOpForAddZ = strConditionZ.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");
                            //StringToFormula stringToFormula = new StringToFormula();
                            bool blnCheckForAddY = stringToFormula.RelationalExpression2(strConditionOpForAddY);
                            bool blnCheckForAddZ = stringToFormula.RelationalExpression2(strConditionOpForAddZ);

                            if (blnCheckForAddY && blnCheckForAddZ)
                            {
                                if (FinalWorking != "")
                                {
                                    clsFB? varFBUser = lstFBUser.FirstOrDefault(x => x.Shomareh == strItemShomareh);

                                    Guid intFBId = new Guid();
                                    if (varFBUser == null)
                                    {
                                        clsFB FB = new clsFB();
                                        FB.BarAvordId = BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                        FB.Shomareh = strItemShomareh;
                                        FB.BahayeVahedZarib = FinalWorking.Length > 1 ? decimal.Parse(FinalWorking) : 0;
                                        FB.BahayeVahedSharh = strDesOfAddingItems;
                                        FB.NoeFBId = NoeFBId;
                                        _context.FBs.Add(FB);
                                        _context.SaveChanges();
                                        intFBId = FB.ID;
                                    }
                                    else
                                        intFBId = varFBUser.ID;

                                    decimal? Tedad = RizMetre.Tedad;
                                    decimal? Tool = RizMetre.Tool;
                                    decimal? Arz = RizMetre.Arz;
                                    decimal? Ertefa = RizMetre.Ertefa;
                                    decimal? Vazn = RizMetre.Vazn;

                                    clsRizMetreUsers RizMetreUserses1 = new clsRizMetreUsers();
                                    RizMetreUserses1.Shomareh = RizMetre.Shomareh;
                                    ShomareNew++;
                                    RizMetreUserses1.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUserses1.Sharh = RizMetre.Sharh;
                                    RizMetreUserses1.Tedad = Tedad;
                                    RizMetreUserses1.Tool = Tool;
                                    RizMetreUserses1.Arz = Arz;
                                    RizMetreUserses1.Ertefa = Ertefa;
                                    RizMetreUserses1.Vazn = Vazn;
                                    RizMetreUserses1.Des = RizMetre.Des;

                                    RizMetreUserses1.FBId = intFBId;
                                    RizMetreUserses1.OperationsOfHamlId = 1;
                                    RizMetreUserses1.Type = "2";
                                    RizMetreUserses1.ForItem = strForItem;
                                    RizMetreUserses1.UseItem = strUseItem;
                                    RizMetreUserses1.LevelNumber = LevelNumber;
                                    RizMetreUserses1.InsertDateTime = Now;

                                    ///محاسبه مقدار جزء
                                    decimal? dMeghdarJoz1 = null;
                                    if (Tedad == null && Tool == null && Arz == null && Vazn == null && Ertefa == null)
                                        dMeghdarJoz1 = null;
                                    else
                                        dMeghdarJoz1 = (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                                        (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
                                    RizMetreUserses1.MeghdarJoz = dMeghdarJoz1;

                                    _context.RizMetreUserses.Add(RizMetreUserses1);


                                    //Start///////////////////////
                                    ////////////حمل///////////////
                                    //////////////////////////////
                                    ///آیتم های حمل درج میگردد
                                    ///
                                    SaveHamlDto requestSaveHaml = new SaveHamlDto
                                    {
                                        BarAvordUserId = ItemHasCon.BarAvordId,
                                        Year = Year,
                                        ItemFBShomareh = strItemShomareh,
                                        NoeFBId = NoeFBId,
                                        Shomareh = RizMetre.Shomareh,
                                        MeghdarJoz = dMeghdarJoz1,
                                        LevelNumber = 1,
                                    };
                                    await HamlCommon.SaveHaml(requestSaveHaml, _context);
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
}
