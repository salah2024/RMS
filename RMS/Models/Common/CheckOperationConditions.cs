using System.Data;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common.Dto;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common
{
    public class CheckOperationConditions
    {
        public void fnCheckOperationCondition(ApplicationDbContext _context, ItemsAddingToFBForCheckOperationDto itemsAddingToFBForCheckOperation,
            List<ItemsFieldsDto> ItemsFields, ItemsHasConditionConditionContextForCheckOperationDto ItemHasCon, Guid FBId, clsRizMetreUsers RM, int LevelNumber, int Year, NoeFehrestBaha NoeFB)
        {
            bool blnCheckAgain = true;
            string Condition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition.Trim() : "";
            string FinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking.Trim() : "";
            string AddedItems = itemsAddingToFBForCheckOperation.AddedItems != null ? itemsAddingToFBForCheckOperation.AddedItems.Substring(0, 6).Trim() : "";
            string DesOfAddingItems = itemsAddingToFBForCheckOperation.DesOfAddingItems != null ? itemsAddingToFBForCheckOperation.DesOfAddingItems.Trim() : "";
            string UseItemForAdd = itemsAddingToFBForCheckOperation.UseItemForAdd != null ? itemsAddingToFBForCheckOperation.UseItemForAdd.Trim() : "";
            DateTime Now = DateTime.Now;

            long ShomareNew = 1;
            clsRizMetreUsers? RizMetre = _context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.InsertDateTime).ThenByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == ItemHasCon.BarAvordId);
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
                        string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";

                        string strCondition = Condition;
                        string strFinalWorking = FinalWorking;
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                            decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                            //guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            string strAddedItems = AddedItems;
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strCharacterPlus);
                            //DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                            //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "A'");
                            Guid intFBId = new Guid();
                            //if (DtFBUser.Rows.Count == 0)
                            if (varFBUsersAdded == null)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = guBAId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                FBSave.Shomareh = AddedItems + strCharacterPlus;//"A";
                                FBSave.BahayeVahedZarib = dPercent;
                                _context.FBs.Add(FBSave);
                                _context.SaveChanges();
                                intFBId = FBSave.ID;
                                //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[idr]["AddedItems"].ToString().Trim() + "A", dPercent);
                            }
                            else
                                intFBId = varFBUsersAdded.ID; //Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                            string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                            //Guid guFBId = FBId; //Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                            //List<RizMetreUsersForItemsAddingToFBInputDto> lstRizMetreUserses = (from RizMetreUsers in _context.RizMetreUserses
                            //                                                                    join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                            //                                                                    where RizMetreUsers.LevelNumber == LevelNumber
                            //                                                                    select new RizMetreUsersForItemsAddingToFBInputDto()
                            //                                                                    {
                            //                                                                        Shomareh = RizMetreUsers.Shomareh,
                            //                                                                        Tedad = RizMetreUsers.Tedad,
                            //                                                                        Tool = RizMetreUsers.Tool,
                            //                                                                        Arz = RizMetreUsers.Arz,
                            //                                                                        Ertefa = RizMetreUsers.Ertefa,
                            //                                                                        Vazn = RizMetreUsers.Vazn,
                            //                                                                        Sharh = RizMetreUsers.Sharh,
                            //                                                                        Des = RizMetreUsers.Des,
                            //                                                                        FBId = RizMetreUsers.FBId,
                            //                                                                        //  RizMetreUsers.OperationsOfHamlId,
                            //                                                                        //  RizMetreUsers.ForItem,
                            //                                                                        Type = RizMetreUsers.Type,
                            //                                                                        // RizMetreUsers.UseItem,
                            //                                                                        // FB.BarAvordId
                            //                                                                    }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();

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

                                if (ItemsFields[4].IsEnteringValue == true)
                                    RizMetreUsers.Vazn = RM.Vazn;

                                RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                RizMetreUsers.FBId = intFBId;
                                RizMetreUsers.OperationsOfHamlId = 1;
                                RizMetreUsers.Type = "2";
                                RizMetreUsers.ForItem = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
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
                                _context.SaveChanges();
                            }

                        }
                        break;
                    }
                case 2:
                    {
                        string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];

                        string strShomarehAdd = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);//.ToList();

                        ///در صورتی که خالی نباشد در  جای مشخص شده درج مینمایم
                        ///x= طول
                        ///y= عرض
                        ///z= ارتفاع
                        string strFinalWorking = FinalWorking;

                        //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + "'");
                        Guid intFBId = new Guid();
                        //if (DtFBUser.Rows.Count == 0)
                        if (varFBUser == null)
                        {
                            clsFB Fb = new clsFB();
                            Fb.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            Fb.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                            Fb.BahayeVahedZarib = 0;
                            _context.FBs.Add(Fb);
                            _context.SaveChanges();
                            intFBId = Fb.ID;
                            //intFBId = Operation_ItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["ID"].ToString()), Dr[0]["AddedItems"].ToString().Trim(), 0);
                        }
                        else
                            intFBId = varFBUser.ID;//Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6); //DtFB.Rows[0]["Shomareh"].ToString().Trim();
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

                        //for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
                        //{

                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + RM.Shomareh);
                        if (DrRizMetreUsersCurrent.Length == 0)
                        {
                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                            RizMetreUsers.Shomareh = RM.Shomareh;
                            ShomareNew++;
                            RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                            RizMetreUsers.Sharh = RM.Sharh;

                            List<string> lst = new List<string>();
                            for (int j = 0; j < strFieldsAdding.Length; j++)
                            {
                                lst.Add(strFieldsAdding[j]);
                            }

                            decimal? dTedad = null;
                            decimal? dTool = null;
                            decimal? dArz = null;
                            decimal? dErtefa = null;
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
                            strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                            if (strCal.Count != 0)
                            {
                                dErtefa = RM.Ertefa;
                            }
                            strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                            if (strCal.Count != 0)
                            {
                                dVazn = RM.Vazn;
                            }

                            if (strFinalWorking != "")
                            {
                                string[] FinalWorkingSplit = strFinalWorking.Split("=");

                                switch (FinalWorkingSplit[0])
                                {
                                    case "x":
                                        {
                                            dTool = decimal.Parse(FinalWorkingSplit[1]);
                                            break;
                                        }
                                    case "y":
                                        {
                                            dArz = decimal.Parse(FinalWorkingSplit[1]);
                                            break;
                                        }
                                    case "z":
                                        {
                                            dErtefa = decimal.Parse(FinalWorkingSplit[1]);
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                            }

                            RizMetreUsers.Tedad = dTedad;

                            RizMetreUsers.Tool = dTool;

                            RizMetreUsers.Arz = dArz;

                            RizMetreUsers.Ertefa = dErtefa;

                            RizMetreUsers.Vazn = dVazn;

                            RizMetreUsers.Des = RM.Des; //DesOfAddingItems + " به آیتم " + ItemHasCon.FBShomareh;
                            RizMetreUsers.FBId = intFBId;
                            RizMetreUsers.OperationsOfHamlId = 1;
                            RizMetreUsers.Type = "2";
                            RizMetreUsers.ForItem = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
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
                            _context.SaveChanges();

                        }
                        break;
                    }
                case 3:
                    {
                        string FinalWorking1 = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "0";
                        decimal dPercent = decimal.Parse(FinalWorking1);
                        string strAddedItems = AddedItems;
                        string strStatus = dPercent > 0 ? "B" : "e";
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strStatus);
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
                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);
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
                            string strItemFBShomareh = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                            ////if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
                            //if (UseItemForAdd == "")
                            //{
                            //    strForItem = ItemHasCon.FBShomareh;//DtFB.Rows[0]["Shomareh"].ToString().Trim();
                            //    Guid guFBId = FBId; //Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                            //    lstRizMetreUserses = (from RizMetreUsers in _context.RizMetreUserses
                            //                          join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                            //                          where RizMetreUsers.LevelNumber == LevelNumber
                            //                          select new RizMetreUsersForItemsAddingToFBInputDto()
                            //                          {
                            //                              Shomareh = RizMetreUsers.Shomareh,
                            //                              Tedad = RizMetreUsers.Tedad,
                            //                              Tool = RizMetreUsers.Tool,
                            //                              Arz = RizMetreUsers.Arz,
                            //                              Ertefa = RizMetreUsers.Ertefa,
                            //                              Vazn = RizMetreUsers.Vazn,
                            //                              Sharh = RizMetreUsers.Sharh,
                            //                              Des = RizMetreUsers.Des,
                            //                              FBId = RizMetreUsers.FBId,
                            //                              //  RizMetreUsers.OperationsOfHamlId,
                            //                              //  RizMetreUsers.ForItem,
                            //                              Type = RizMetreUsers.Type,
                            //                              // RizMetreUsers.UseItem,
                            //                              // FB.BarAvordId
                            //                          }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
                            //    //DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                            //}
                            //else
                            //{
                            //    strUseItem = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();
                            //    strForItem = UseItemForAdd; //Dr[idr]["UseItemForAdd"].ToString().Trim();
                            //    varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strForItem);
                            //    //DtFB = clsConvert.ToDataTable(varFBUser);
                            //    //DtFB = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strForItem + "'");
                            //    Guid guFBId = FBId; //Guid.Parse(DtFB.Rows[0]["ID"].ToString());
                            //    lstRizMetreUserses = (from RizMetreUsers in _context.RizMetreUserses
                            //                          join FB in _context.FBs on RizMetreUsers.FBId equals FB.ID
                            //                          where RizMetreUsers.LevelNumber == LevelNumber
                            //                          select new RizMetreUsersForItemsAddingToFBInputDto()
                            //                          {
                            //                              Shomareh = RizMetreUsers.Shomareh,
                            //                              Tedad = RizMetreUsers.Tedad,
                            //                              Tool = RizMetreUsers.Tool,
                            //                              Arz = RizMetreUsers.Arz,
                            //                              Ertefa = RizMetreUsers.Ertefa,
                            //                              Vazn = RizMetreUsers.Vazn,
                            //                              Sharh = RizMetreUsers.Sharh,
                            //                              Des = RizMetreUsers.Des,
                            //                              FBId = RizMetreUsers.FBId,
                            //                              //  RizMetreUsers.OperationsOfHamlId,
                            //                              ForItem = RizMetreUsers.ForItem,
                            //                              Type = RizMetreUsers.Type,
                            //                              // RizMetreUsers.UseItem,
                            //                              // FB.BarAvordId
                            //                          }).Where(x => x.FBId == guFBId && x.ForItem == strItemFBShomareh && x.Type == "4").OrderBy(x => x.Shomareh).ToList();
                            //    //DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                            //    //DtRizMetreUsers = clsRizMetreUsers.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["ID"].ToString() + " and ForItem='" + strItemFBShomareh + "' and Type=4");
                            //}
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

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                            clsFB varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                            Guid intFBId = new Guid();
                            if (varFBUser == null)
                            {
                                clsFB FB = new clsFB();
                                FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                FB.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                                FB.BahayeVahedZarib = 0;
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
                                _context.SaveChanges();
                                //RizMetreUsers.Save();
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        StringToFormula StringToFormula = new StringToFormula();
                        string strAddedItems = AddedItems;
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);

                        Guid intFBId = new Guid();
                        if (varFBUser == null)
                        {
                            clsFB FB = new clsFB();
                            FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            FB.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                            FB.BahayeVahedZarib = 0;
                            _context.FBs.Add(FB);
                            _context.SaveChanges();
                            intFBId = FB.ID;
                        }
                        else
                            intFBId = varFBUser.ID;// Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


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
                                                       }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                        DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

                        string strConditionOp = strCondition.Replace("z", RM.Ertefa.ToString());
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
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
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                        //DtFBUser = clsConvert.ToDataTable(varFBUser);

                        //DtFBUser = clsOperation_ItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["ID"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
                        Guid intFBId = new Guid();
                        if (varFBUser == null)
                        {
                            clsFB FB = new clsFB();
                            FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                            FB.Shomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                            FB.BahayeVahedZarib = 0;
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
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

                            Guid intFBId = new Guid();
                            if (varFBUsersAdded == null)
                            {
                                clsFB FBSave = new clsFB();
                                FBSave.BarAvordId = ItemHasCon.BarAvordId;
                                FBSave.Shomareh = strAddedItems.Trim();
                                FBSave.BahayeVahedZarib = 0;
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
                                    //blnCheckIsExistErrors = "false";
                                    //clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    //ItemsHasConditionAddedToFB.BarAvordId = ItemHasCon.BarAvordId;
                                    //ItemsHasConditionAddedToFB.FBShomareh = ItemHasCon.FBShomareh;
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

                                    ////if (ItemsHasConditionAddedToFB.Save())
                                    //if (blnCheckSave)
                                    //{

                                    dZarib = dZarib < 0 ? dZarib * -1 : dZarib;
                                    var varBA = _context.BaravordUsers.Where(x => x.ID == ItemHasCon.BarAvordId).ToList();
                                    DataTable DtBA = clsConvert.ToDataTable(varBA);
                                    //Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    string strItemShomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

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
                        string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                        string strItemShomareh = ItemHasCon.FBShomareh.Substring(0, 6) + strCharacterPlus;
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

                        clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

                        Guid intFBId = new Guid();
                        if (FBUser == null)
                        {
                            clsFB FB = new clsFB();
                            FB.BarAvordId = ItemHasCon.BarAvordId;
                            FB.Shomareh = strItemShomareh;
                            FB.BahayeVahedZarib = dPercent;
                            FB.BahayeVahedSharh = strDesOfAddingItems;
                            _context.FBs.Add(FB);
                            _context.SaveChanges();
                            intFBId = FB.ID;
                        }
                        else
                        {
                            intFBId = FBUser.ID;
                        }


                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);
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
                            RizMetreUserses.ForItem = ItemHasCon.FBShomareh.Substring(0, 6);
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
                            _context.SaveChanges();
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
                        string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

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
                                string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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

                                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems).FirstOrDefault();

                                    Guid intFBId = new Guid();
                                    if (FBUser == null)
                                    {
                                        clsFB FBSave = new clsFB();
                                        FBSave.BarAvordId = ItemHasCon.BarAvordId;
                                        FBSave.Shomareh = strAddedItems.Trim();
                                        FBSave.BahayeVahedZarib = 0;
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
                                        _context.SaveChanges();
                                        //RizMetreUsers.Save();
                                    }

                                }
                            }

                        }

                        break;
                    }
                case 13:
                    {
                        decimal Meghdar = ItemHasCon.Meghdar;
                        decimal Meghdar2 = ItemHasCon.Meghdar2;
                        string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        Guid BarAvordId = ItemHasCon.BarAvordId;
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


                                clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == AddedItems).FirstOrDefault();

                                Guid intFBId = new Guid();
                                if (FBUser == null)
                                {
                                    clsFB FBSave = new clsFB();
                                    FBSave.BarAvordId = ItemHasCon.BarAvordId;
                                    FBSave.Shomareh = AddedItems.Trim();
                                    FBSave.BahayeVahedZarib = 0;
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

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

                            clsFB varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
                            Guid intFBId = new Guid();
                            if (varFBUser == null)
                            {
                                clsFB FB = new clsFB();
                                FB.BarAvordId = ItemHasCon.BarAvordId; //Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                FB.Shomareh = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                                FB.BahayeVahedZarib = 0;
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

                                RizMetreUsers.Vazn = decimal.Parse("0.9");

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
                                _context.SaveChanges();
                                //RizMetreUsers.Save();
                            }
                        }
                        break;
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

                        Guid BarAvordId = ItemHasCon.BarAvordId;

                        GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                        {
                            ShomarehFB = ItemHasCon.FBShomareh,
                            BarAvordUserId = BarAvordId,
                            NoeFB = NoeFB,
                            Year = Year,
                            LevelNumber = LevelNumber,
                            ConditionGroupId = lngConditionGroupId
                        };
                        List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);


                        if (lstRM.Count != 0)
                        {
                            string strCurrentFBShomareh = ItemHasCon.FBShomareh; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                            long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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
                                var varFBUser = _context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strItemShomareh).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                Guid intFBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
                                    FB.Shomareh = AddedItems;
                                    FB.BahayeVahedZarib = 0;
                                    FB.BahayeVahedSharh = "";
                                    _context.FBs.Add(FB);
                                    _context.SaveChanges();
                                    intFBId = FB.ID;
                                }
                                else
                                {
                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                                }


                                string[] FieldsAddingSplit = strFieldsAdding;

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
        public void fnCheckOperationConditionForDelete(ApplicationDbContext _context, ItemsAddingToFBForCheckOperationDto itemsAddingToFBForCheckOperation,
            List<ItemsFieldsDto> ItemsFields, ItemsHasConditionConditionContextForCheckOperationDto ItemHasCon, Guid FBId, clsRizMetreUsers RizMetre)
        {
            bool blnCheckAgain = true;
            string Condition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition.Trim() : "";
            string FinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking.Trim() : "";
            string AddedItems = itemsAddingToFBForCheckOperation.AddedItems != null ? itemsAddingToFBForCheckOperation.AddedItems.Substring(0, 6).Trim() : "";
            string DesOfAddingItems = itemsAddingToFBForCheckOperation.DesOfAddingItems != null ? itemsAddingToFBForCheckOperation.DesOfAddingItems.Trim() : "";
            string UseItemForAdd = itemsAddingToFBForCheckOperation.UseItemForAdd != null ? itemsAddingToFBForCheckOperation.UseItemForAdd.Trim() : "";

            Guid guBAId = ItemHasCon.BarAvordId;
            switch (itemsAddingToFBForCheckOperation.ConditionType)
            {
                case 1:
                    {
                        string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                        string strCondition = Condition;
                        string strFinalWorking = FinalWorking;
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                            decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                            string strAddedItems = AddedItems;
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strCharacterPlus);

                            Guid intFBId = new Guid();

                            if (varFBUsersAdded != null)
                                intFBId = varFBUsersAdded.ID;

                            string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();

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
                            }

                        }
                        break;
                    }
                case 2:
                    {
                        string strShomarehAdd = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);//.ToList();
                        Guid intFBId = new Guid();

                        if (varFBUser != null)
                            intFBId = varFBUser.ID;


                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6); //DtFB.Rows[0]["Shomareh"].ToString().Trim();

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
                        }
                        break;
                    }
                case 3:
                    {
                        string FinalWorking1 = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "0";
                        decimal dPercent = decimal.Parse(FinalWorking1);
                        string strAddedItems = AddedItems;
                        string strStatus = dPercent > 0 ? "B" : "e";
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strStatus);

                        Guid intFBId = new Guid();

                        if (varFBUser != null)
                            intFBId = varFBUser.ID;



                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);// DtFB.Rows[0]["Shomareh"].ToString().Trim();

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
                            string strItemFBShomareh = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();

                            List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strForItem && x.UseItem == strUseItem && x.Type == "2").ToList();
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
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            string strAddedItems = AddedItems;
                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
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
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        string strCondition = Condition;
                        string strAddedItems = AddedItems;
                        string strFinalWorking = FinalWorking;
                        DataTable DtRizMetreUserses = new DataTable();
                        string strForItem = "";
                        string strUseItem = "";
                        string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        Guid guFBId = FBId;
                        strUseItem = strItemFBShomareh;
                        strForItem = UseItemForAdd;

                        string strConditionOp = strCondition.Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();

                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                            long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());
                            string strItemShomareh = AddedItems;
                            strAddedItems = strAddedItems.Trim();
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

                            Guid intFBId = new Guid();
                            if (varFBUsersAdded != null)
                                intFBId = varFBUsersAdded.ID;

                            long lngShomareh = RizMetre.Shomareh;

                            List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh).OrderBy(x => x.Shomareh).ToList();
                            clsRizMetreUsers? RizMetreUsers = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == lngShomareh);

                            List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

                            if (RizMetreUsers != null)
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
                                _context.RizMetreUserses.RemoveRange(RizMetreUsers);
                            }

                        }
                        break;
                    }
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
                                intFBId = varFBUsersAdded.ID;

                            long lngShomareh = RizMetre.Shomareh; //long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());

                            List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh).ToList();
                            clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == lngShomareh);

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
                            }
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
                                }
                                else
                                {

                                    dZarib = dZarib < 0 ? dZarib * -1 : dZarib;
                                    var varBA = _context.BaravordUsers.Where(x => x.ID == ItemHasCon.BarAvordId).ToList();
                                    DataTable DtBA = clsConvert.ToDataTable(varBA);

                                    string strItemShomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

                                    Guid intFBId = new Guid();
                                    if (FBUser != null)
                                        intFBId = FBUser.ID;



                                    string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
                                    List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                                        _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2" && x.Shomareh == RizMetre.Shomareh).ToList();

                                    clsRizMetreUsers? RizMetreUsersCurrent =
                                        lstRizMetreUsersCurrent.FirstOrDefault(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2");

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
                        string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                        string strItemShomareh = ItemHasCon.FBShomareh.Substring(0, 6) + strCharacterPlus;
                        string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                        string strDesOfAddingItems = DesOfAddingItems;
                        decimal dPercent = decimal.Parse(strFinalWorking);

                        clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

                        Guid intFBId = new Guid();
                        if (FBUser != null)
                            intFBId = FBUser.ID;

                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);

                        List<clsRizMetreUsers> lstRizMetreUsersCurrent =
                                        _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
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
                        }
                        break;
                    }
                case 12:
                    {
                        //string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding!=null? itemsAddingToFBForCheckOperation.FieldsAdding.Split(','):new string[0];
                        string strCondition = Condition;
                        string[] strConditionSplit = strCondition.Split("_");
                        string strAddedItems = AddedItems;
                        string strFinalWorking = FinalWorking;
                        DataTable DtRizMetreUserses = new DataTable();
                        string strForItem = "";
                        string strUseItem = "";
                        string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        Guid guFBId = FBId;
                        strUseItem = strItemFBShomareh;
                        strForItem = UseItemForAdd;

                        decimal? dErtefa1 = null;
                        string strConditionOp = strConditionSplit[0].Replace("x", RizMetre.Tool == null ? "0" : RizMetre.Tool.Value.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck2 = true;
                        if (strConditionSplit.Length == 2)
                        {
                            string strConditionOp2 = strConditionSplit[1].Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
                            blnCheck2 = StringToFormula.RelationalExpression2(strConditionOp2);
                            ///در صورتی که کمتر از 3 سانت باشد عدد یک درج میشود
                            ///در صورتی که بیشتر از 3 سانت باشد مازاد بر 3 درج میشود
                            ///Ertefa-3
                            ///
                            strFinalWorking = strFinalWorking.Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
                            dErtefa1 = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString("0.##"));
                        }
                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            if (blnCheck2)
                            {
                                string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                                long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());

                                if (strFinalWorking != "")
                                {
                                    strFinalWorking = strFinalWorking.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");
                                    string strItemShomareh = AddedItems;
                                    strAddedItems = strAddedItems.Trim();
                                    clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);
                                    //DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                                    Guid intFBId = new Guid();
                                    if (varFBUsersAdded != null)
                                        intFBId = varFBUsersAdded.ID;

                                    long lngShomareh = RizMetre.Shomareh;

                                    List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh).OrderBy(x => x.Shomareh).ToList();
                                    clsRizMetreUsers? RizMetreUsers = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == lngShomareh);

                                    List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

                                    if (RizMetreUsers != null)
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
                                        _context.RizMetreUserses.RemoveRange(RizMetreUsers);
                                    }
                                }
                            }
                        }
                        break;
                    }
                case 13:
                    {
                        decimal Meghdar = ItemHasCon.Meghdar;
                        decimal Meghdar2 = ItemHasCon.Meghdar2;
                        decimal dZaribVazn = ((Meghdar * Meghdar2) / 10000);

                        Guid BarAvordId = ItemHasCon.BarAvordId;

                        string[] strCondition = Condition.Trim().Split("_");
                        string strCondition1 = strCondition[0];
                        string strCondition2 = strCondition[1];
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
                                string strItemShomareh = AddedItems;
                                clsFB? FBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemShomareh);

                                Guid intFBId = new Guid();
                                if (FBUser != null)
                                    intFBId = FBUser.ID;

                                string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6);
                                List<clsRizMetreUsers> lstRizMetreUsersCurrent = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == strShomareh1 && x.Type == "2").ToList();
                                clsRizMetreUsers? RizMetreUsersCurrent = lstRizMetreUsersCurrent.FirstOrDefault(x => x.Shomareh == RizMetre.Shomareh);

                                List<clsRizMetreUsers> lstRizMetreBase = _context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();

                                if (RizMetreUsersCurrent != null)
                                {
                                    if (lstRizMetreBase.Count == 1)
                                    {
                                        clsItemsHasConditionAddedToFB? ItemsHasConditionAddedToFB =
                                            _context.ItemsHasConditionAddedToFBs.FirstOrDefault(x => x.BarAvordId == BarAvordId
                                                 && x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id && x.ConditionGroupId == ItemHasCon.ConditionGroupId);

                                        if (ItemsHasConditionAddedToFB != null)
                                        {
                                            _context.ItemsHasConditionAddedToFBs.RemoveRange(ItemsHasConditionAddedToFB);
                                        }
                                    }
                                    _context.RizMetreUserses.RemoveRange(RizMetreUsersCurrent);
                                }

                                break;
                            }
                        }
                        break;
                    }
                case 14:
                    {
                        string strCondition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition : "";
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            string strAddedItems = AddedItems;
                            clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
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
                            }
                        }
                        break;
                    }
                case 16:
                    {
                        string strAddedItems = AddedItems;
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
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
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        public void fnCheckOperationConditionForUpdate(ApplicationDbContext _context, ItemsAddingToFBForCheckOperationDto itemsAddingToFBForCheckOperation,
            List<ItemsFieldsDto> ItemsFields, ItemsHasConditionConditionContextForCheckOperationDto ItemHasCon, Guid FBId, clsRizMetreUsers RizMetre, RizMetreInputDto OldRizMetre
            , int LevelNumber, int Year, NoeFehrestBaha NoeFB)
        {
            bool blnCheckAgain = true;
            string Condition = itemsAddingToFBForCheckOperation.Condition != null ? itemsAddingToFBForCheckOperation.Condition.Trim() : "";
            string FinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking.Trim() : "";
            string AddedItems = itemsAddingToFBForCheckOperation.AddedItems != null ? itemsAddingToFBForCheckOperation.AddedItems.Substring(0, 6).Trim() : "";
            string DesOfAddingItems = itemsAddingToFBForCheckOperation.DesOfAddingItems != null ? itemsAddingToFBForCheckOperation.DesOfAddingItems.Trim() : "";
            string UseItemForAdd = itemsAddingToFBForCheckOperation.UseItemForAdd != null ? itemsAddingToFBForCheckOperation.UseItemForAdd.Trim() : "";
            DateTime Now = DateTime.Now;

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
                        string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                        string strCondition = Condition;
                        string strFinalWorking = FinalWorking;
                        string strConditionOp = strCondition.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();
                        bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                        if (blnCheck)
                        {
                            strFinalWorking = strFinalWorking.Replace("x", ItemHasCon.Meghdar.ToString().Trim());
                            decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                            string strAddedItems = AddedItems;
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems + strCharacterPlus);

                            Guid intFBId = new Guid();

                            if (varFBUsersAdded != null)
                            {
                                varFBUsersAdded.BahayeVahedZarib = dPercent;
                                intFBId = varFBUsersAdded.ID;
                            }

                            string strShomareh1 = ItemHasCon.FBShomareh;

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
                                _context.SaveChanges();
                            }

                        }
                        break;
                    }
                case 2:
                    {
                        string strShomarehAdd = AddedItems;// Dr[0]["AddedItems"].ToString().Trim();
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd);//.ToList();
                        Guid intFBId = new Guid();

                        if (varFBUser != null)
                            intFBId = varFBUser.ID;


                        string strShomareh1 = ItemHasCon.FBShomareh.Substring(0, 6); //DtFB.Rows[0]["Shomareh"].ToString().Trim();

                        List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strShomarehAdd).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

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

                            if (lstCurrentItemsFields[4] != true)
                                RizMetre.Vazn = null;

                            ///محاسبه مقدار جزء
                            decimal? dMeghdarJoz = null;
                            if (RizMetre.Tedad == null && RizMetre.Tool == null && RizMetre.Arz == null && RizMetre.Ertefa == null && RizMetre.Vazn == null)
                                dMeghdarJoz = null;
                            else
                                dMeghdarJoz = (RizMetre.Tedad == null ? 1 : RizMetre.Tedad.Value) * (RizMetre.Tool == null ? 1 : RizMetre.Tool.Value) *
                                (RizMetre.Arz == null ? 1 : RizMetre.Arz.Value) * (RizMetre.Ertefa == null ? 1 : RizMetre.Ertefa.Value) * (RizMetre.Vazn == null ? 1 : RizMetre.Vazn.Value);

                            RizMetre.MeghdarJoz = dMeghdarJoz;

                            _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);
                            _context.SaveChanges();
                        }
                        break;
                    }
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
                            varFBUser.BahayeVahedZarib = dPercent;
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
                            string strItemFBShomareh = ItemHasCon.FBShomareh;// DtFB.Rows[0]["Shomareh"].ToString().Trim();


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

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

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
                                _context.SaveChanges();
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];
                        string strCondition = Condition;
                        string strAddedItems = AddedItems;
                        string strFinalWorking = FinalWorking;
                        DataTable DtRizMetreUserses = new DataTable();
                        string strForItem = "";
                        string strUseItem = "";
                        string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        Guid guFBId = FBId;
                        strUseItem = strItemFBShomareh;
                        strForItem = UseItemForAdd;
                        ///ریز متره قبلی حذف میگردد

                        string strConditionOp = strCondition.Replace("z", OldRizMetre.Ertefa == null ? "0" : OldRizMetre.Ertefa.Value.ToString().Trim());
                        StringToFormula StringToFormula = new StringToFormula();

                        bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                        if (blnCheck)
                        {
                            string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                            long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());

                            string strItemShomareh = AddedItems;
                            strAddedItems = strAddedItems.Trim();
                            clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

                            //DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                            Guid intFBId = new Guid();
                            if (varFBUsersAdded != null)
                                intFBId = varFBUsersAdded.ID;

                            long lngShomareh = OldRizMetre.Shomareh;

                            List<clsRizMetreUsers> lstRizMetreUsers = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                            if (lstRizMetreUsers.Count != 0)
                            {
                                _context.RizMetreUserses.RemoveRange(lstRizMetreUsers);
                                _context.SaveChanges();
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
                        string strConditionOpNew = strCondition.Replace("z", RizMetre.Ertefa == null ? "0" : RizMetre.Ertefa.Value.ToString().Trim());
                        StringToFormula StringToFormulaNew = new StringToFormula();

                        bool blnCheckNew = StringToFormulaNew.RelationalExpression2(strConditionOpNew);
                        if (blnCheckNew)
                        {

                            string strCurrentFBShomareh = strItemFBShomareh;// Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                            long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
                            clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                 .FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.FBShomareh.Trim() == strCurrentFBShomareh.Trim() &&
                                     x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId);

                            if (currentItemsHasConditionAddedToFBs == null)
                            {
                                clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                ItemsHasConditionAddedToFB.BarAvordId = ItemHasCon.BarAvordId;
                                ItemsHasConditionAddedToFB.FBShomareh = strCurrentFBShomareh;
                                ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                ItemsHasConditionAddedToFB.Meghdar = 0;// RizMetre.Tool != null ? RizMetre.Tool.Value : 0;
                                ItemsHasConditionAddedToFB.ConditionGroupId = ItemHasCon.ConditionGroupId;
                                _context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                _context.SaveChanges();
                            }

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
                                decimal? dErtefa = null;
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
                                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                if (strCal.Count != 0)
                                {
                                    dErtefa = RizMetre.Ertefa;
                                }
                                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                if (strCal.Count != 0)
                                {
                                    dVazn = RizMetre.Vazn;
                                }

                                RizMetreUsers.Tedad = dTedad;
                                RizMetreUsers.Tool = dTool;
                                RizMetreUsers.Arz = dArz;
                                RizMetreUsers.Ertefa = dErtefa;
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
                                _context.SaveChanges();
                                //RizMetreUsers.Save();
                            }
                        }
                        break;
                    }
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
                                _context.SaveChanges();
                            }
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
                                }
                                else
                                {

                                    dZarib = dZarib < 0 ? dZarib * -1 : dZarib;

                                    string strItemShomareh = AddedItems;// Dr[idr]["AddedItems"].ToString().Trim();
                                    clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

                                    Guid intFBId = new Guid();
                                    if (FBUser != null)
                                    {
                                        intFBId = FBUser.ID;
                                        FBUser.BahayeVahedZarib = dZarib;
                                    }



                                    string strShomareh1 = ItemHasCon.FBShomareh; //DtFB.Rows[0]["Shomareh"].ToString().Trim();
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
                                        _context.SaveChanges();
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
                        string strCharacterPlus = itemsAddingToFBForCheckOperation.CharacterPlus != null ? itemsAddingToFBForCheckOperation.CharacterPlus : "";
                        string strItemShomareh = ItemHasCon.FBShomareh.Substring(0, 6) + strCharacterPlus;
                        string strFinalWorking = itemsAddingToFBForCheckOperation.FinalWorking != null ? itemsAddingToFBForCheckOperation.FinalWorking : "";
                        string strDesOfAddingItems = DesOfAddingItems;
                        decimal dPercent = decimal.Parse(strFinalWorking);

                        clsFB? FBUser = _context.FBs.Where(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strItemShomareh).FirstOrDefault();

                        Guid intFBId = new Guid();
                        if (FBUser != null)
                        {
                            intFBId = FBUser.ID;
                            FBUser.BahayeVahedZarib = dPercent;
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
                        string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

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
                                long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;// long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());

                                if (strFinalWorking != "")
                                {
                                    strFinalWorking = strFinalWorking.Replace("z", RizMetre.Ertefa != null ? RizMetre.Ertefa.Value.ToString().Trim() : "");
                                    string strItemShomareh = AddedItems;
                                    strAddedItems = strAddedItems.Trim();
                                    clsFB? varFBUsersAdded = _context.FBs.FirstOrDefault(x => x.BarAvordId == ItemHasCon.BarAvordId && x.Shomareh == strAddedItems);

                                    //DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                                    Guid intFBId = new Guid();
                                    if (varFBUsersAdded != null)
                                        intFBId = varFBUsersAdded.ID;

                                    long lngShomareh = OldRizMetre.Shomareh;

                                    List<clsRizMetreUsers> lstRizMetreUsers = _context.RizMetreUserses.Where(x => x.FBId == intFBId && x.ForItem == ItemHasCon.FBShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                                    if (lstRizMetreUsers.Count != 0)
                                    {
                                        _context.RizMetreUserses.RemoveRange(lstRizMetreUsers);
                                        _context.SaveChanges();
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
                                long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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
                                        _context.SaveChanges();
                                        //RizMetreUsers.Save();
                                    }

                                }
                            }

                        }

                        break;
                    }
                case 13:
                    {
                        decimal Meghdar = ItemHasCon.Meghdar;
                        decimal Meghdar2 = ItemHasCon.Meghdar2;
                        string strItemFBShomareh = ItemHasCon.FBShomareh.Trim();

                        Guid BarAvordId = ItemHasCon.BarAvordId;
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

                            List<bool> lstCurrentItemsFields = _context.ItemsFieldses.Where(x => x.ItemShomareh == strAddedItems).OrderBy(x => x.FieldType).Select(x => x.IsEnteringValue).ToList();

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

                                RizMetre.Ertefa = dMultiple;

                                RizMetre.Vazn = decimal.Parse("0.9");

                                //if (lstCurrentItemsFields[3] != true)
                                //    RizMetre.Ertefa = null;

                                //if (lstCurrentItemsFields[4] == true)
                                //    RizMetre.Vazn = dMultiple;
                                //else
                                //    RizMetre.Vazn = null;

                                _context.Entry(RizMetreUsersCurrent).CurrentValues.SetValues(RizMetre);
                                _context.SaveChanges();
                            }
                        }
                        break;
                    }
                case 16:
                    {
                        ////
                        ///ابتدا ریزه متره موجود را حذف میکنیم
                        ///
                        string strAddedItems = AddedItems;
                        clsFB? varFBUser = _context.FBs.FirstOrDefault(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems);
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
                            _context.SaveChanges();
                        }


                        ///بررسی پخش، آبپاشی، تسطیح و کوبیدن قشر زیراساس  
                        /////در صورت یافتن ریز متره درج شده برای این شرط بایستی به 
                        ///اضافه بها بابت سختی اجرا در شانه سازی ها به عرض تا 2 متر
                        ///اضافه گردد
                        RizMetreCommon rizMetreCommon = new RizMetreCommon();
                        long[] lngConditionGroupId = { 12 };

                        string[] strFieldsAdding = itemsAddingToFBForCheckOperation.FieldsAdding != null ? itemsAddingToFBForCheckOperation.FieldsAdding.Split(',') : new string[0];

                        Guid BarAvordId = ItemHasCon.BarAvordId;

                        GetAndShowAddItemsInputForSoubatDto request2 = new GetAndShowAddItemsInputForSoubatDto()
                        {
                            ShomarehFB = ItemHasCon.FBShomareh,
                            BarAvordUserId = BarAvordId,
                            NoeFB = NoeFB,
                            Year = Year,
                            LevelNumber = LevelNumber,
                            ConditionGroupId = lngConditionGroupId
                        };
                        List<RizMetreForGetAndShowAddItemsDto> lstRM = rizMetreCommon.GetAndShowAddItemsForSoubat(request2, _context);


                        if (lstRM.Count != 0)
                        {
                            string strCurrentFBShomareh = ItemHasCon.FBShomareh; //Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                            long lngItemsHasCondition_ConditionContextId = ItemHasCon.Id;
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
                                    _context.FBs.Add(FB);
                                    _context.SaveChanges();
                                    intFBId = FB.ID;
                                }



                                string[] FieldsAddingSplit = strFieldsAdding;

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
