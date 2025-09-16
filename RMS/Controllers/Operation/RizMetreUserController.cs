using System;
using System.Data;
using System.Reflection.PortableExecutable;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using RMS.Models.StoredProceduresData;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation;

public class RizMetreUserController(ApplicationDbContext _context) : Controller
{
    private readonly ApplicationDbContext context = _context;
    // GET: RizMetreUser
    public ActionResult Index()
    {
        var RizMetreUser = context.RizMetreUserses.ToList();
        return View(RizMetreUser);
    }

    [HttpPost]
    public JsonResult ConfirmRizMetreUsers([FromBody] RizMetreInputDto Request)
    {
        string Sharh = Request.Sharh;
        decimal? Tedad = Request.Tedad;
        decimal? Tool = Request.Tool;
        decimal? Arz = Request.Arz;
        decimal? Ertefa = Request.Ertefa;
        decimal? Vazn = Request.Vazn;
        string Des = Request.Des;
        Guid FBId = Request.FBId;
        Guid BarAvordId = Request.BarAvordUserId;
        int OperationId = Request.OperationId;
        string ForItem = Request.ForItem;
        int Year = Request.Year;
        NoeFehrestBaha NoeFB = Request.NoeFB;
        int LevelNumber = Request.LevelNumber;
        DateTime Now = DateTime.Now;

        //try
        //{
        long LastShomareh = 0;
        clsRizMetreUsers? RizMetreUserAllBarAvord = context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        if (RizMetreUserAllBarAvord != null)
            LastShomareh = RizMetreUserAllBarAvord.Shomareh + 1;
        else
            LastShomareh = 1;

        //clsRizMetreUsers? RizMetreUser = context.RizMetreUserses.Where(x => x.FBId == FBId && x.LevelNumber == LevelNumber).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        //long Shomareh = 0;
        //if (RizMetreUser != null)
        //    Shomareh = RizMetreUser.Shomareh + 1;
        //else
        //    Shomareh = 1;


        //DataTable DtLastRizMetreUsersShomareh = clsRizMetreUserss.GetLastRizMetreUsersShomareh("FBId=" + FBId);
        //int Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.ID = Guid.NewGuid();
        RizMetre.Shomareh = LastShomareh;
        RizMetre.Sharh = Sharh.Trim();
        RizMetre.Tedad = Tedad;
        RizMetre.Tool = Tool;
        RizMetre.Arz = Arz;
        RizMetre.Ertefa = Ertefa;
        RizMetre.Vazn = Vazn;
        RizMetre.Des = Des.Trim() == "" ? ForItem.Trim() == "" ? "" : " به آیتم شماره " + ForItem : Des.Trim();
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "1";
        RizMetre.ForItem = ForItem.Trim();
        RizMetre.UseItem = "";
        RizMetre.LevelNumber = LevelNumber;
        RizMetre.InsertDateTime = Now;


        ///محاسبه مقدار جزء
        decimal dMeghdarJoz = 0;
        if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
            dMeghdarJoz = 0;
        else
            dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
        RizMetre.MeghdarJoz = dMeghdarJoz;

        context.RizMetreUserses.Add(RizMetre);
        context.SaveChanges();



        /////////////
        //////////////
        //////////////
        ///


        clsOperation_ItemsFB operation_ItemsFB = context.Operation_ItemsFBs.First(x => x.OperationId == OperationId);


        List<ItemsFieldsDto> ItemsField = (from ItemF in context.ItemsFieldses
                                           join OpItemFB in context.Operation_ItemsFBs
                                           on ItemF.ItemShomareh equals OpItemFB.ItemsFBShomareh
                                           select new ItemsFieldsDto
                                           {
                                               ItemShomareh = ItemF.ItemShomareh,
                                               NoeFB = ItemF.NoeFB,
                                               IsEnteringValue = ItemF.IsEnteringValue,
                                               Vahed = ItemF.Vahed,
                                               FieldType = ItemF.FieldType,
                                               OperationId = OpItemFB.OperationId
                                           }).Where(x => x.OperationId == OperationId && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();

        List<ItemsHasConditionConditionContextForCheckOperationDto> lstItemsHasCondition = _context.ItemsHasCondition_ConditionContexts
            .Where(cc => cc.Year == Year)
            .Join(_context.ItemsHasConditionAddedToFBs,
            cc => cc.Id,
            fb => fb.ItemsHasCondition_ConditionContextId,
    (cc, fb) => new { cc, fb })
        .Join(_context.ItemsHasConditions,
    temp => temp.cc.ItemsHasConditionId,
    ihc => ihc.Id,
    (temp, ihc) => new { temp.cc, temp.fb, ihc })
        .Join(_context.Operation_ItemsFBs,
    temp => temp.ihc.ItemFBShomareh,
    ofb => ofb.ItemsFBShomareh,
    (temp, ofb) => new ItemsHasConditionConditionContextForCheckOperationDto
    {
        Id = temp.cc.Id,
        ItemsHasConditionId = temp.cc.ItemsHasConditionId,
        ConditionContextId = temp.cc.ConditionContextId,
        HasEnteringValue = temp.cc.HasEnteringValue,
        Des = temp.cc.Des,
        DefaultValue = temp.cc.DefaultValue,
        IsShow = temp.cc.IsShow,
        ParentId = temp.cc.ParentId,
        MoveToRel = temp.cc.MoveToRel,
        ViewCheckAllRecords = temp.cc.ViewCheckAllRecords,
        StepChange = temp.cc.StepChange,
        Meghdar = temp.fb.Meghdar,
        Meghdar2 = temp.fb.Meghdar2,
        FBShomareh = temp.fb.FBShomareh,
        ConditionGroupId = temp.fb.ConditionGroupId,
        BarAvordId = temp.fb.BarAvordId,
        OperationId = ofb.OperationId
    }).Where(x => x.OperationId == OperationId && x.BarAvordId == BarAvordId).ToList();


        var ItemsAddingToFBs = _context.ItemsAddingToFBs.Include(x => x.ItemsHasCondition_ConditionContext).ThenInclude(x => x.ConditionContext).Where(x => x.Year == Year).Select(x => new
        {
            x.ItemsHasCondition_ConditionContextId,
            x.AddedItems,
            x.Condition,
            x.FinalWorking,
            x.ConditionType,
            x.DesOfAddingItems,
            x.UseItemForAdd,
            x.FieldsAdding,
            x.CharacterPlus,
            x.ItemsHasCondition_ConditionContext.ConditionContext.ConditionContextRel,
            x.ItemsHasCondition_ConditionContext.ConditionContextId
        }).ToList();

        foreach (var ItemHasCon in lstItemsHasCondition)
        {
            List<ItemsAddingToFBForCheckOperationDto> lstItemsAddingToFBForCheckOperation = ItemsAddingToFBs.Where(x => x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id).Select(x => new
            ItemsAddingToFBForCheckOperationDto
            {
                ItemsHasCondition_ConditionContextId = x.ItemsHasCondition_ConditionContextId,
                AddedItems = x.AddedItems,
                Condition = x.Condition,
                FinalWorking = x.FinalWorking,
                ConditionType = x.ConditionType,
                DesOfAddingItems = x.DesOfAddingItems,
                UseItemForAdd = x.UseItemForAdd,
                FieldsAdding = x.FieldsAdding,
                CharacterPlus = x.CharacterPlus,
                ConditionContextId = x.ConditionContextId,
                ConditionContextRel = x.ConditionContextRel
            }).ToList();

            CheckOperationConditions checkOperationConditions = new CheckOperationConditions();
            foreach (var itemsAddingToFBForCheckOperation in lstItemsAddingToFBForCheckOperation)
            {
                checkOperationConditions.fnCheckOperationCondition(_context, itemsAddingToFBForCheckOperation, ItemsField, ItemHasCon, FBId, RizMetre, 1, Year, NoeFB);
            }
        }

        //////////
        ///////////////
        //////////////




        return new JsonResult("OK_" + operation_ItemsFB.ItemsFBShomareh);











        //DataTable DtFB = clsOperationItemsFB.FBListWithParameter("Id=" + FBId);
        //var varFB = context.FBs.Where(x => x.ID == FBId).ToList();
        //DataTable DtFB = clsConvert.ToDataTable(varFB);
        //string strShomareh = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //var varItemsHasConditionAddedToFB = (from clsItemsHasConditionAddedToFB in context.ItemsHasConditionAddedToFBs
        //                                     join tblItemsHasConditionConditionContext in context.ItemsHasCondition_ConditionContexts
        //                                     on clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasConditionConditionContext.Id
        //                                     join tblItemsHasCondition in context.ItemsHasConditions on tblItemsHasConditionConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
        //                                     select new
        //                                     {
        //                                         clsItemsHasConditionAddedToFB.ID,
        //                                         clsItemsHasConditionAddedToFB.FBShomareh,
        //                                         clsItemsHasConditionAddedToFB.BarAvordId,
        //                                         clsItemsHasConditionAddedToFB.Meghdar,
        //                                         clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
        //                                         clsItemsHasConditionAddedToFB.ConditionGroupId,
        //                                         ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh
        //                                     }).Where(x => x.BarAvordId == BarAvordId && x.FBShomareh == strShomareh).ToList();
        //DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
        ////DataTable DtItemsHasConditionAddedToFB = clsItemsHasConditionAddedToFB.ListWithParameterSimple("BarAvordId=" + BarAvordId + " and FBShomareh='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "'");
        ////DataTable DtItemsFBShomarehValueShomareh = clsItemsFBShomarehValueShomareh.ListWithParameter("FBShomareh='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and BarAvordId=" + BarAvordId);
        //var varItemsFBShomarehValueShomareh = context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == strShomareh && x.BarAvordId == BarAvordId).ToList();
        //DataTable DtItemsFBShomarehValueShomareh = clsConvert.ToDataTable(varItemsFBShomarehValueShomareh);
        //if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
        //{
        //    string strGetValuesShomareh = DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim();
        //    var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strGetValuesShomareh).ToList();
        //    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //    //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim() + "'");


        //    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //    //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim() + "'");
        //    string strFBUser = DtFBUser.Rows[0]["Shomareh"].ToString().Trim();
        //    var varItemsForGetValues = context.ItemsForGetValuess.Where(x => x.ItemShomareh == strFBUser && x.Year == Year).ToList();
        //    DataTable DtItemsForGetValues = clsConvert.ToDataTable(varItemsForGetValues);
        //    //DataTable DtItemsForGetValues = clsOperationItemsFB.ItemsForGetValuesListWithParameter("ItemShomareh='" + DtFBUser.Rows[0]["Shomareh"].ToString().Trim() + "' and Year=1397");// and ItemShomarehForGetValue='" + ItemsFBShomareh + "'");

        //    if (DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString() != "")
        //        if (DtItemsForGetValues.Rows.Count != 0)
        //        {
        //            //if (strFBShomareh.Trim() != "")
        //            //{
        //            DataRow[] DrItemsForGetValues = DtItemsForGetValues.Select("ItemShomarehForGetValue='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "'");

        //            //clsItemsFBShomarehValueShomareh ItemsFBShomarehValueShomareh = new clsItemsFBShomarehValueShomareh();

        //            string[] strRizMetreFieldsRequire = DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString().Split(',');
        //            List<string> lst = new List<string>();
        //            for (int j = 0; j < strRizMetreFieldsRequire.Length; j++)
        //            {
        //                lst.Add(strRizMetreFieldsRequire[j]);
        //            }

        //            decimal dTedad = 0;
        //            decimal dTool = 0;
        //            decimal dArz = 0;
        //            decimal dErtefa = 0;
        //            decimal dVazn = 0;
        //            var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
        //            if (strCal.Count != 0)
        //            {
        //                string[] s = strCal[0].ToString().Split('+');
        //                if (s.Length > 1)
        //                    dTedad = (Tedad != 0 ? 1 : Tedad) * decimal.Parse(s[1]) + Tedad;
        //                else
        //                    dTedad = Tedad;
        //            }
        //            strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
        //            if (strCal.Count != 0)
        //            {
        //                string[] s = strCal[0].ToString().Split('+');
        //                if (s.Length > 1)
        //                    dTool = (Tool != 0 ? 1 : Tool) * decimal.Parse(s[1]) + Tool;
        //                else

        //                    dTool = Tool;
        //            }
        //            strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
        //            if (strCal.Count != 0)
        //            {
        //                string[] s = strCal[0].ToString().Split('+');
        //                if (s.Length > 1)
        //                    dArz = (Arz != 0 ? 1 : Arz) * decimal.Parse(s[1]) + Arz;
        //                else

        //                    dArz = Arz;
        //            }
        //            strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
        //            if (strCal.Count != 0)
        //            {
        //                string[] s = strCal[0].ToString().Split('+');
        //                if (s.Length > 1)
        //                    dErtefa = (Ertefa != 0 ? 1 : Ertefa) * decimal.Parse(s[1]) + Ertefa;
        //                else
        //                    dErtefa = Ertefa;
        //            }
        //            strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
        //            if (strCal.Count != 0)
        //            {
        //                string[] s = strCal[0].ToString().Split('+');
        //                if (s.Length > 1)
        //                    dVazn = (Vazn != 0 ? 1 : Vazn) * decimal.Parse(s[1]) + Vazn;
        //                else
        //                    dVazn = Vazn;
        //            }
        //            RizMetre = new clsRizMetreUsers();
        //            RizMetre.Shomareh = Shomareh;
        //            RizMetre.Sharh = Sharh.Trim();
        //            RizMetre.Tedad = dTedad;
        //            RizMetre.Tool = dTool;
        //            RizMetre.Arz = dArz;
        //            RizMetre.Ertefa = dErtefa;
        //            RizMetre.Vazn = dVazn;
        //            RizMetre.FBId = FBId;
        //            RizMetre.Type = "4";
        //            RizMetre.Des = " به آیتم شماره " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //            RizMetre.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //            RizMetre.UseItem = "";
        //            RizMetre.OperationsOfHamlId = 1;
        //            RizMetre.LevelNumber = LevelNumber;

        //            ///محاسبه مقدار جزء
        //            dMeghdarJoz = 0;
        //            if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                dMeghdarJoz = 0;
        //            else
        //                dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //            RizMetre.MeghdarJoz = dMeghdarJoz;


        //            context.RizMetreUserses.Add(RizMetre);
        //            context.SaveChanges();
        //            //RizMetre.Save();
        //            //}
        //        }
        //}

        //var varItemsFields = (from ItemsFields in context.ItemsFieldses
        //                      join OperationItemsFB in context.Operation_ItemsFBs
        //                      on ItemsFields.ItemShomareh equals OperationItemsFB.ItemsFBShomareh
        //                      select new
        //                      {
        //                          ID = ItemsFields.Id,
        //                          ItemShomareh = ItemsFields.ItemShomareh,
        //                          FieldType = ItemsFields.FieldType,
        //                          Vahed = ItemsFields.Vahed,
        //                          IsEnteringValue = ItemsFields.IsEnteringValue,
        //                          DefaultValue = ItemsFields.DefaultValue,
        //                          NoeFB = ItemsFields.NoeFB,
        //                          OperationId = OperationItemsFB.OperationId
        //                      }).Where(x => x.OperationId == OperationId && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();
        //DataTable DtItemsFields = clsConvert.ToDataTable(varItemsFields);
        ////DataTable DtItemsFields = clsItemsFields.ItemsFieldsListWithParameter("OperationId='" + OperationId + "' and NoeFB=234");
        //if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
        //{
        //    List<long> strItemsHasConditionConditionContext = new List<long>();
        //    if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
        //    {
        //        //strItemsHasConditionConditionContext += "ItemsHasCondition_ConditionContextId in(";
        //        for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
        //        {
        //            //if ((i + 1) < DtItemsHasConditionAddedToFB.Rows.Count)
        //            strItemsHasConditionConditionContext.Add(long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString()));
        //            //else
        //            //    strItemsHasConditionConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString();
        //        }
        //        //strItemsHasConditionConditionContext += ")";
        //    }
        //    var varItemsAddingToFB = context.ItemsAddingToFBs.Where(x => strItemsHasConditionConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
        //    DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);
        //    //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasConditionConditionContext);
        //    for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
        //    {
        //        decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
        //        string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();
        //        DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
        //        if (Dr.Length != 0)
        //        {
        //            for (int idr = 0; idr < Dr.Length; idr++)
        //            {
        //                switch (Dr[idr]["ConditionType"].ToString())
        //                {
        //                    case "1":
        //                        {
        //                            string strCondition = Dr[idr]["Condition"].ToString().Trim();
        //                            string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
        //                            string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
        //                            StringToFormula StringToFormula = new StringToFormula();
        //                            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                            if (blnCheck)
        //                            {
        //                                strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
        //                                decimal dPercent = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
        //                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //                                var varBA = context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
        //                                DataTable DtBA = clsConvert.ToDataTable(varBA);
        //                                //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
        //                                Guid guidBarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                string strShomarehAdd = Dr[idr]["AddedItems"].ToString().Trim() + "A";
        //                                var varFBUser = context.FBs.Where(x => x.BarAvordId == guidBarAvordId && x.Shomareh == strShomarehAdd).ToList();
        //                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "A'");
        //                                Guid intFBId = new Guid();
        //                                if (DtFBUser.Rows.Count == 0)
        //                                {
        //                                    clsFB FBSave = new clsFB();
        //                                    FBSave.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                    FBSave.Shomareh = Dr[idr]["AddedItems"].ToString().Trim() + "A";
        //                                    FBSave.BahayeVahedZarib = dPercent;
        //                                    context.FBs.Add(FBSave);
        //                                    context.SaveChanges();
        //                                    intFBId = FBSave.ID;
        //                                    //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim() + "A", dPercent);
        //                                }
        //                                else
        //                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //                                Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
        //                                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                        select new
        //                                                        {
        //                                                            ID = RizMetreUsers.ID,
        //                                                            Shomareh = RizMetreUsers.Shomareh,
        //                                                            Tedad = RizMetreUsers.Tedad,
        //                                                            Tool = RizMetreUsers.Tool,
        //                                                            Arz = RizMetreUsers.Arz,
        //                                                            Ertefa = RizMetreUsers.Ertefa,
        //                                                            Vazn = RizMetreUsers.Vazn,
        //                                                            Des = RizMetreUsers.Des,
        //                                                            FBId = RizMetreUsers.FBId,
        //                                                            OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                            ForItem = RizMetreUsers.ForItem,
        //                                                            Type = RizMetreUsers.Type,
        //                                                            UseItem = RizMetreUsers.UseItem,
        //                                                            BarAvordId = FB.BarAvordId
        //                                                        }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
        //                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
        //                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");

        //                                var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
        //                                                               join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                               select new
        //                                                               {
        //                                                                   ID = RizMetreUsers.ID,
        //                                                                   Shomareh = RizMetreUsers.Shomareh,
        //                                                                   Tedad = RizMetreUsers.Tedad,
        //                                                                   Tool = RizMetreUsers.Tool,
        //                                                                   Arz = RizMetreUsers.Arz,
        //                                                                   Ertefa = RizMetreUsers.Ertefa,
        //                                                                   Vazn = RizMetreUsers.Vazn,
        //                                                                   Des = RizMetreUsers.Des,
        //                                                                   FBId = RizMetreUsers.FBId,
        //                                                                   OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                                   ForItem = RizMetreUsers.ForItem,
        //                                                                   Type = RizMetreUsers.Type,
        //                                                                   UseItem = RizMetreUsers.UseItem,
        //                                                                   BarAvordId = FB.BarAvordId
        //                                                               }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh && x.Type == "2").OrderBy(x => x.Shomareh).ToList();
        //                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsers);
        //                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
        //                                for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
        //                                {
        //                                    DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
        //                                    if (DrRizMetreUsersCurrent.Length == 0)
        //                                    {
        //                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
        //                                        RizMetreUsers.Shomareh = int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
        //                                        RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

        //                                        if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                        else
        //                                            RizMetreUsers.Tedad = 0;

        //                                        if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                        else
        //                                            RizMetreUsers.Tool = 0;

        //                                        if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                        else
        //                                            RizMetreUsers.Arz = 0;

        //                                        if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                        else
        //                                            RizMetreUsers.Ertefa = 0;

        //                                        if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                        else
        //                                            RizMetreUsers.Vazn = 0;

        //                                        RizMetreUsers.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.FBId = intFBId;
        //                                        RizMetreUsers.OperationsOfHamlId = 1;
        //                                        RizMetreUsers.Type = "2";
        //                                        RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.UseItem = "";
        //                                        RizMetre.LevelNumber = LevelNumber;

        //                                        ///محاسبه مقدار جزء
        //                                        dMeghdarJoz = 0;
        //                                        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                                            dMeghdarJoz = 0;
        //                                        else
        //                                            dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                                            (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //                                        RizMetre.MeghdarJoz = dMeghdarJoz;


        //                                        context.RizMetreUserses.Add(RizMetreUsers);
        //                                        context.SaveChanges();
        //                                        //RizMetreUsers.Save();
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    case "2":
        //                        {
        //                            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();

        //                            var varBA = context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
        //                            DataTable DtBA = clsConvert.ToDataTable(varBA);
        //                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
        //                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                            string strShomarehAdd = Dr[0]["AddedItems"].ToString().Trim();
        //                            var varFBUser = context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdd).ToList();
        //                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //                            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + "'");
        //                            Guid intFBId = new Guid();
        //                            if (DtFBUser.Rows.Count == 0)
        //                            {
        //                                clsFB Fb = new clsFB();
        //                                Fb.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                Fb.Shomareh = Dr[0]["AddedItems"].ToString().Trim();
        //                                Fb.BahayeVahedZarib = 0;
        //                                context.FBs.Add(Fb);
        //                                context.SaveChanges();
        //                                intFBId = Fb.ID;
        //                                //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[0]["AddedItems"].ToString().Trim(), 0);
        //                            }
        //                            else
        //                                intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
        //                            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                    select new
        //                                                    {
        //                                                        ID = RizMetreUsers.ID,
        //                                                        Shomareh = RizMetreUsers.Shomareh,
        //                                                        Tedad = RizMetreUsers.Tedad,
        //                                                        Tool = RizMetreUsers.Tool,
        //                                                        Arz = RizMetreUsers.Arz,
        //                                                        Ertefa = RizMetreUsers.Ertefa,
        //                                                        Vazn = RizMetreUsers.Vazn,
        //                                                        Des = RizMetreUsers.Des,
        //                                                        FBId = RizMetreUsers.FBId,
        //                                                        OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                        ForItem = RizMetreUsers.ForItem,
        //                                                        Type = RizMetreUsers.Type,
        //                                                        UseItem = RizMetreUsers.UseItem,
        //                                                        BarAvordId = FB.BarAvordId
        //                                                    }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
        //                            DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
        //                            //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");

        //                            var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
        //                                                           join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                           select new
        //                                                           {
        //                                                               ID = RizMetreUsers.ID,
        //                                                               Shomareh = RizMetreUsers.Shomareh,
        //                                                               Tedad = RizMetreUsers.Tedad,
        //                                                               Tool = RizMetreUsers.Tool,
        //                                                               Arz = RizMetreUsers.Arz,
        //                                                               Ertefa = RizMetreUsers.Ertefa,
        //                                                               Vazn = RizMetreUsers.Vazn,
        //                                                               Des = RizMetreUsers.Des,
        //                                                               FBId = RizMetreUsers.FBId,
        //                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                               ForItem = RizMetreUsers.ForItem,
        //                                                               Type = RizMetreUsers.Type,
        //                                                               UseItem = RizMetreUsers.UseItem,
        //                                                               BarAvordId = FB.BarAvordId
        //                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh && x.Type == "2").OrderBy(x => x.Shomareh).ToList();
        //                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsers);

        //                            //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
        //                            //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
        //                            for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
        //                            {
        //                                DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
        //                                if (DrRizMetreUsersCurrent.Length == 0)
        //                                {
        //                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
        //                                    RizMetreUsers.Shomareh = int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
        //                                    RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();
        //                                    //RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                    //RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                    //RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                    //RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                    //RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());

        //                                    if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                    else
        //                                        RizMetreUsers.Tedad = 0;

        //                                    if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                    else
        //                                        RizMetreUsers.Tool = 0;

        //                                    if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                    else
        //                                        RizMetreUsers.Arz = 0;

        //                                    if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                    else
        //                                        RizMetreUsers.Ertefa = 0;

        //                                    if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                    else
        //                                        RizMetreUsers.Vazn = 0;

        //                                    RizMetreUsers.Des = Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                    RizMetreUsers.FBId = intFBId;
        //                                    RizMetreUsers.OperationsOfHamlId = 1;
        //                                    RizMetreUsers.Type = "2";
        //                                    RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                    RizMetreUsers.UseItem = "";
        //                                    RizMetreUsers.LevelNumber = LevelNumber;


        //                                    ///محاسبه مقدار جزء
        //                                    dMeghdarJoz = 0;
        //                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                                        dMeghdarJoz = 0;
        //                                    else
        //                                        dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                                        (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //                                    RizMetre.MeghdarJoz = dMeghdarJoz;


        //                                    context.RizMetreUserses.Add(RizMetreUsers);
        //                                    context.SaveChanges();
        //                                    //RizMetreUsers.Save();
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    case "3":
        //                        {
        //                            decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
        //                            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //                            var varBA = context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
        //                            DataTable DtBA = clsConvert.ToDataTable(varBA);
        //                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
        //                            string strStatus = dPercent > 0 ? "B" : "e";
        //                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                            string strAddedItems = Dr[0]["AddedItems"].ToString().Trim() + strStatus;
        //                            var varFBUser = context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems).ToList();
        //                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //                            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[0]["AddedItems"].ToString().Trim() + strStatus + "'");
        //                            Guid intFBId = new Guid();
        //                            if (DtFBUser.Rows.Count == 0)
        //                            {
        //                                //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[0]["AddedItems"].ToString().Trim() + strStatus, dPercent);
        //                                clsFB FB = new clsFB();
        //                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                FB.Shomareh = Dr[0]["AddedItems"].ToString().Trim() + strStatus;
        //                                FB.BahayeVahedZarib = dPercent;
        //                                context.FBs.Add(FB);
        //                                context.SaveChanges();
        //                                intFBId = FB.ID;
        //                            }
        //                            else
        //                                intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
        //                            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                    select new
        //                                                    {
        //                                                        ID = RizMetreUsers.ID,
        //                                                        Shomareh = RizMetreUsers.Shomareh,
        //                                                        RizMetreUsers.Sharh,
        //                                                        Tedad = RizMetreUsers.Tedad,
        //                                                        Tool = RizMetreUsers.Tool,
        //                                                        Arz = RizMetreUsers.Arz,
        //                                                        Ertefa = RizMetreUsers.Ertefa,
        //                                                        Vazn = RizMetreUsers.Vazn,
        //                                                        Des = RizMetreUsers.Des,
        //                                                        FBId = RizMetreUsers.FBId,
        //                                                        OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                        ForItem = RizMetreUsers.ForItem,
        //                                                        Type = RizMetreUsers.Type,
        //                                                        UseItem = RizMetreUsers.UseItem,
        //                                                        BarAvordId = FB.BarAvordId
        //                                                    }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
        //                            DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

        //                            var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
        //                                                           join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                           select new
        //                                                           {
        //                                                               ID = RizMetreUsers.ID,
        //                                                               Shomareh = RizMetreUsers.Shomareh,
        //                                                               RizMetreUsers.Sharh,
        //                                                               Tedad = RizMetreUsers.Tedad,
        //                                                               Tool = RizMetreUsers.Tool,
        //                                                               Arz = RizMetreUsers.Arz,
        //                                                               Ertefa = RizMetreUsers.Ertefa,
        //                                                               Vazn = RizMetreUsers.Vazn,
        //                                                               Des = RizMetreUsers.Des,
        //                                                               FBId = RizMetreUsers.FBId,
        //                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                               ForItem = RizMetreUsers.ForItem,
        //                                                               Type = RizMetreUsers.Type,
        //                                                               UseItem = RizMetreUsers.UseItem,
        //                                                               BarAvordId = FB.BarAvordId
        //                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh && x.Type == "2").ToList();
        //                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
        //                            //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
        //                            //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
        //                            for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
        //                            {
        //                                DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
        //                                if (DrRizMetreUsersCurrent.Length == 0)
        //                                {
        //                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
        //                                    RizMetreUsers.Shomareh = int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
        //                                    RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();
        //                                    //RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                    //RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                    //RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                    //RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                    //RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                    if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                    else
        //                                        RizMetreUsers.Tedad = 0;

        //                                    if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                    else
        //                                        RizMetreUsers.Tool = 0;

        //                                    if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                    else
        //                                        RizMetreUsers.Arz = 0;

        //                                    if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                    else
        //                                        RizMetreUsers.Ertefa = 0;

        //                                    if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
        //                                        RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                    else
        //                                        RizMetreUsers.Vazn = 0;

        //                                    RizMetreUsers.Des = Dr[0]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                    RizMetreUsers.FBId = intFBId;
        //                                    RizMetreUsers.OperationsOfHamlId = 1;
        //                                    RizMetreUsers.Type = "2";
        //                                    RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                    RizMetreUsers.UseItem = "";
        //                                    RizMetreUsers.LevelNumber = LevelNumber;


        //                                    ///محاسبه مقدار جزء
        //                                    dMeghdarJoz = 0;
        //                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                                        dMeghdarJoz = 0;
        //                                    else
        //                                        dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                                        (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //                                    RizMetre.MeghdarJoz = dMeghdarJoz;


        //                                    context.RizMetreUserses.Add(RizMetreUsers);
        //                                    context.SaveChanges();
        //                                    //RizMetreUsers.Save();
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    case "4":
        //                        {
        //                            string strCondition = Dr[idr]["Condition"].ToString().Trim();
        //                            string strFinalWorking = Dr[idr]["FinalWorking"].ToString().Trim();
        //                            string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
        //                            StringToFormula StringToFormula = new StringToFormula();
        //                            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                            if (blnCheck)
        //                            {
        //                                strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
        //                                if (strFinalWorking != "")
        //                                {
        //                                    decimal dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
        //                                    //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //                                    var varBA = context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
        //                                    DataTable DtBA = clsConvert.ToDataTable(varBA);
        //                                    //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
        //                                    Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                    string strAddedItems = Dr[idr]["AddedItems"].ToString().Trim();
        //                                    var varFBUser = context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems).ToList();
        //                                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //                                    //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
        //                                    Guid intFBId = new Guid();
        //                                    if (DtFBUser.Rows.Count == 0)
        //                                    {
        //                                        //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
        //                                        clsFB FB = new clsFB();
        //                                        FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                        FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
        //                                        FB.BahayeVahedZarib = 0;
        //                                        context.FBs.Add(FB);
        //                                        context.SaveChanges();
        //                                        intFBId = FB.ID;
        //                                    }
        //                                    else
        //                                        intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //                                    DataTable DtRizMetreUsers = new DataTable();
        //                                    string strForItem = "";
        //                                    string strUseItem = "";
        //                                    string strItemFBShomareh = DtFB.Rows[0]["Shomareh"].ToString().Trim();

        //                                    if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
        //                                    {
        //                                        strForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
        //                                        var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                                join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                                select new
        //                                                                {
        //                                                                    ID = RizMetreUsers.ID,
        //                                                                    Shomareh = RizMetreUsers.Shomareh,
        //                                                                    RizMetreUsers.Sharh,
        //                                                                    Tedad = RizMetreUsers.Tedad,
        //                                                                    Tool = RizMetreUsers.Tool,
        //                                                                    Arz = RizMetreUsers.Arz,
        //                                                                    Ertefa = RizMetreUsers.Ertefa,
        //                                                                    Vazn = RizMetreUsers.Vazn,
        //                                                                    Des = RizMetreUsers.Des,
        //                                                                    FBId = RizMetreUsers.FBId,
        //                                                                    OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                                    ForItem = RizMetreUsers.ForItem,
        //                                                                    Type = RizMetreUsers.Type,
        //                                                                    UseItem = RizMetreUsers.UseItem,
        //                                                                    BarAvordId = FB.BarAvordId
        //                                                                }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
        //                                        DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
        //                                        //DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
        //                                    }
        //                                    else
        //                                    {
        //                                        strUseItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
        //                                        var varCurrentFB = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strForItem).ToList();
        //                                        DataTable DtCurrentFB = clsConvert.ToDataTable(varCurrentFB);
        //                                        //DataTable DtCurrentFB = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strForItem + "'");
        //                                        if (DtCurrentFB.Rows.Count != 0)
        //                                        // DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtCurrentFB.Rows[0]["Id"].ToString() + " and ForItem='" + strItemFBShomareh + "' and Type=4");
        //                                        {
        //                                            Guid guFBId = Guid.Parse(DtCurrentFB.Rows[0]["ID"].ToString());
        //                                            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                                    select new
        //                                                                    {
        //                                                                        ID = RizMetreUsers.ID,
        //                                                                        Shomareh = RizMetreUsers.Shomareh,
        //                                                                        RizMetreUsers.Sharh,
        //                                                                        Tedad = RizMetreUsers.Tedad,
        //                                                                        Tool = RizMetreUsers.Tool,
        //                                                                        Arz = RizMetreUsers.Arz,
        //                                                                        Ertefa = RizMetreUsers.Ertefa,
        //                                                                        Vazn = RizMetreUsers.Vazn,
        //                                                                        Des = RizMetreUsers.Des,
        //                                                                        FBId = RizMetreUsers.FBId,
        //                                                                        OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                                        ForItem = RizMetreUsers.ForItem,
        //                                                                        Type = RizMetreUsers.Type,
        //                                                                        UseItem = RizMetreUsers.UseItem,
        //                                                                        BarAvordId = FB.BarAvordId
        //                                                                    }).Where(x => x.FBId == guFBId && x.ForItem == strItemFBShomareh && x.Type == "4").OrderBy(x => x.Shomareh).ToList();
        //                                            DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
        //                                        }
        //                                    }
        //                                    var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
        //                                                                   join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                                   select new
        //                                                                   {
        //                                                                       ID = RizMetreUsers.ID,
        //                                                                       Shomareh = RizMetreUsers.Shomareh,
        //                                                                       RizMetreUsers.Sharh,
        //                                                                       Tedad = RizMetreUsers.Tedad,
        //                                                                       Tool = RizMetreUsers.Tool,
        //                                                                       Arz = RizMetreUsers.Arz,
        //                                                                       Ertefa = RizMetreUsers.Ertefa,
        //                                                                       Vazn = RizMetreUsers.Vazn,
        //                                                                       Des = RizMetreUsers.Des,
        //                                                                       FBId = RizMetreUsers.FBId,
        //                                                                       OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                                       ForItem = RizMetreUsers.ForItem,
        //                                                                       Type = RizMetreUsers.Type,
        //                                                                       UseItem = RizMetreUsers.UseItem,
        //                                                                       BarAvordId = FB.BarAvordId
        //                                                                   }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh && x.UseItem == strUseItem && x.Type == "2").ToList();
        //                                    DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
        //                                    //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + strForItem + "' and UseItem='" + strUseItem + "' and Type=2");
        //                                    for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
        //                                    {
        //                                        DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
        //                                        if (DrRizMetreUsersCurrent.Length == 0)
        //                                        {
        //                                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
        //                                            RizMetreUsers.Shomareh = int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
        //                                            RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();
        //                                            //RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString()) * dMultiple;
        //                                            //RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                            //RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                            //RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                            //RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());

        //                                            if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
        //                                                RizMetreUsers.Tedad = (decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString()) == 0 ? 1 : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString())) * dMultiple;
        //                                            else
        //                                                RizMetreUsers.Tedad = 0;

        //                                            if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
        //                                                RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                            else
        //                                                RizMetreUsers.Tool = 0;

        //                                            if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
        //                                                RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                            else
        //                                                RizMetreUsers.Arz = 0;

        //                                            if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
        //                                                RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                            else
        //                                                RizMetreUsers.Ertefa = 0;

        //                                            if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
        //                                                RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                            else
        //                                                RizMetreUsers.Vazn = 0;

        //                                            RizMetreUsers.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + strForItem;
        //                                            RizMetreUsers.FBId = intFBId;
        //                                            RizMetreUsers.OperationsOfHamlId = 1;
        //                                            RizMetreUsers.Type = "2";
        //                                            RizMetreUsers.ForItem = strForItem;
        //                                            RizMetreUsers.UseItem = strUseItem;
        //                                            RizMetreUsers.LevelNumber = LevelNumber;


        //                                            ///محاسبه مقدار جزء
        //                                            dMeghdarJoz = 0;
        //                                            if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                                                dMeghdarJoz = 0;
        //                                            else
        //                                                dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                                                (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //                                            RizMetre.MeghdarJoz = dMeghdarJoz;


        //                                            context.RizMetreUserses.Add(RizMetreUsers);
        //                                            context.SaveChanges();
        //                                            //RizMetreUsers.Save();
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    case "5":
        //                        {
        //                            string strCondition = Dr[idr]["Condition"].ToString().Trim();
        //                            string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
        //                            StringToFormula StringToFormula = new StringToFormula();
        //                            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                            if (blnCheck)
        //                            {
        //                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //                                var varBA = context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
        //                                DataTable DtBA = clsConvert.ToDataTable(varBA);
        //                                //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
        //                                Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                string strAddedItems = Dr[idr]["AddedItems"].ToString().Trim();
        //                                var varFBUser = context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItems).ToList();
        //                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
        //                                Guid intFBId = new Guid();
        //                                if (DtFBUser.Rows.Count == 0)
        //                                {
        //                                    clsFB FB = new clsFB();
        //                                    FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                    FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
        //                                    FB.BahayeVahedZarib = 0;
        //                                    context.FBs.Add(FB);
        //                                    context.SaveChanges();
        //                                    intFBId = FB.ID;
        //                                    //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
        //                                }
        //                                else
        //                                    intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //                                Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
        //                                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                        select new
        //                                                        {
        //                                                            ID = RizMetreUsers.ID,
        //                                                            Shomareh = RizMetreUsers.Shomareh,
        //                                                            RizMetreUsers.Sharh,
        //                                                            Tedad = RizMetreUsers.Tedad,
        //                                                            Tool = RizMetreUsers.Tool,
        //                                                            Arz = RizMetreUsers.Arz,
        //                                                            Ertefa = RizMetreUsers.Ertefa,
        //                                                            Vazn = RizMetreUsers.Vazn,
        //                                                            Des = RizMetreUsers.Des,
        //                                                            FBId = RizMetreUsers.FBId,
        //                                                            OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                            ForItem = RizMetreUsers.ForItem,
        //                                                            Type = RizMetreUsers.Type,
        //                                                            UseItem = RizMetreUsers.UseItem,
        //                                                            BarAvordId = FB.BarAvordId
        //                                                        }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
        //                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

        //                                var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
        //                                                               join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                               select new
        //                                                               {
        //                                                                   ID = RizMetreUsers.ID,
        //                                                                   Shomareh = RizMetreUsers.Shomareh,
        //                                                                   RizMetreUsers.Sharh,
        //                                                                   Tedad = RizMetreUsers.Tedad,
        //                                                                   Tool = RizMetreUsers.Tool,
        //                                                                   Arz = RizMetreUsers.Arz,
        //                                                                   Ertefa = RizMetreUsers.Ertefa,
        //                                                                   Vazn = RizMetreUsers.Vazn,
        //                                                                   Des = RizMetreUsers.Des,
        //                                                                   FBId = RizMetreUsers.FBId,
        //                                                                   OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                                   ForItem = RizMetreUsers.ForItem,
        //                                                                   Type = RizMetreUsers.Type,
        //                                                                   UseItem = RizMetreUsers.UseItem,
        //                                                                   BarAvordId = FB.BarAvordId
        //                                                               }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh && x.Type == "2").ToList();
        //                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
        //                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
        //                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
        //                                for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
        //                                {
        //                                    DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
        //                                    if (DrRizMetreUsersCurrent.Length == 0)
        //                                    {
        //                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
        //                                        RizMetreUsers.Shomareh = int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
        //                                        RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();
        //                                        //RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                        //RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                        //RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                        //RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                        //RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());

        //                                        if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                        else
        //                                            RizMetreUsers.Tedad = 0;

        //                                        if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                        else
        //                                            RizMetreUsers.Tool = 0;

        //                                        if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                        else
        //                                            RizMetreUsers.Arz = 0;

        //                                        if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                        else
        //                                            RizMetreUsers.Ertefa = 0;

        //                                        if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                        else
        //                                            RizMetreUsers.Vazn = 0;

        //                                        RizMetreUsers.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.FBId = intFBId;
        //                                        RizMetreUsers.OperationsOfHamlId = 1;
        //                                        RizMetreUsers.Type = "2";
        //                                        RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.UseItem = "";
        //                                        RizMetreUsers.LevelNumber = LevelNumber;


        //                                        ///محاسبه مقدار جزء
        //                                        dMeghdarJoz = 0;
        //                                        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                                            dMeghdarJoz = 0;
        //                                        else
        //                                            dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                                            (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //                                        RizMetre.MeghdarJoz = dMeghdarJoz;


        //                                        context.RizMetreUserses.Add(RizMetreUsers);
        //                                        context.SaveChanges();
        //                                        //RizMetreUsers.Save();
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    case "6":
        //                        {
        //                            string strCondition = Dr[idr]["Condition"].ToString().Trim();
        //                            StringToFormula StringToFormula = new StringToFormula();
        //                            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //                            var varBA = context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
        //                            DataTable DtBA = clsConvert.ToDataTable(varBA);
        //                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
        //                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                            string strShomarehAdded = Dr[idr]["AddedItems"].ToString().Trim();
        //                            var varFBUser = context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strShomarehAdded).ToList();
        //                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //                            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
        //                            Guid intFBId = new Guid();
        //                            if (DtFBUser.Rows.Count == 0)
        //                            {
        //                                //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
        //                                clsFB FB = new clsFB();
        //                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
        //                                FB.BahayeVahedZarib = 0;
        //                                context.FBs.Add(FB);
        //                                context.SaveChanges();
        //                                intFBId = FB.ID;
        //                            }
        //                            else
        //                                intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
        //                            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                    select new
        //                                                    {
        //                                                        ID = RizMetreUsers.ID,
        //                                                        Shomareh = RizMetreUsers.Shomareh,
        //                                                        RizMetreUsers.Sharh,
        //                                                        Tedad = RizMetreUsers.Tedad,
        //                                                        Tool = RizMetreUsers.Tool,
        //                                                        Arz = RizMetreUsers.Arz,
        //                                                        Ertefa = RizMetreUsers.Ertefa,
        //                                                        Vazn = RizMetreUsers.Vazn,
        //                                                        Des = RizMetreUsers.Des,
        //                                                        FBId = RizMetreUsers.FBId,
        //                                                        OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                        ForItem = RizMetreUsers.ForItem,
        //                                                        Type = RizMetreUsers.Type,
        //                                                        UseItem = RizMetreUsers.UseItem,
        //                                                        BarAvordId = FB.BarAvordId
        //                                                    }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
        //                            DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

        //                            var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
        //                                                           join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                           select new
        //                                                           {
        //                                                               ID = RizMetreUsers.ID,
        //                                                               Shomareh = RizMetreUsers.Shomareh,
        //                                                               RizMetreUsers.Sharh,
        //                                                               Tedad = RizMetreUsers.Tedad,
        //                                                               Tool = RizMetreUsers.Tool,
        //                                                               Arz = RizMetreUsers.Arz,
        //                                                               Ertefa = RizMetreUsers.Ertefa,
        //                                                               Vazn = RizMetreUsers.Vazn,
        //                                                               Des = RizMetreUsers.Des,
        //                                                               FBId = RizMetreUsers.FBId,
        //                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                               ForItem = RizMetreUsers.ForItem,
        //                                                               Type = RizMetreUsers.Type,
        //                                                               UseItem = RizMetreUsers.UseItem,
        //                                                               BarAvordId = FB.BarAvordId
        //                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh && x.Type == "2").ToList();
        //                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
        //                            //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
        //                            //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
        //                            for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
        //                            {
        //                                string strConditionOp = strCondition.Replace("x", DtRizMetreUsers.Rows[i]["Ertefa"].ToString().Trim());
        //                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                                if (blnCheck)
        //                                {
        //                                    DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
        //                                    if (DrRizMetreUsersCurrent.Length == 0)
        //                                    {
        //                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
        //                                        RizMetreUsers.Shomareh = int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
        //                                        RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

        //                                        if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                        else
        //                                            RizMetreUsers.Tedad = 0;

        //                                        if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                        else
        //                                            RizMetreUsers.Tool = 0;

        //                                        if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                        else
        //                                            RizMetreUsers.Arz = 0;

        //                                        if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                        else
        //                                            RizMetreUsers.Ertefa = 0;

        //                                        if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                        else
        //                                            RizMetreUsers.Vazn = 0;

        //                                        //RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
        //                                        //RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                        //RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                        //RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                        //RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                        RizMetreUsers.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.FBId = intFBId;
        //                                        RizMetreUsers.OperationsOfHamlId = 1;
        //                                        RizMetreUsers.Type = "2";
        //                                        RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.UseItem = "";
        //                                        RizMetreUsers.LevelNumber = LevelNumber;


        //                                        ///محاسبه مقدار جزء
        //                                        dMeghdarJoz = 0;
        //                                        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                                            dMeghdarJoz = 0;
        //                                        else
        //                                            dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                                            (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //                                        RizMetre.MeghdarJoz = dMeghdarJoz;


        //                                        context.RizMetreUserses.Add(RizMetreUsers);
        //                                        context.SaveChanges();
        //                                        //RizMetreUsers.Save();
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    case "8":
        //                        {
        //                            string strCondition = Dr[idr]["Condition"].ToString().Trim();
        //                            StringToFormula StringToFormula = new StringToFormula();
        //                            //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //                            var varBA = context.BaravordUsers.Where(x => x.ID == BarAvordId).ToList();
        //                            DataTable DtBA = clsConvert.ToDataTable(varBA);
        //                            //DataTable DtBA = clsBaravordUser.ListWithParametr("Id=" + BarAvordId);
        //                            string strAddedItem = Dr[idr]["AddedItems"].ToString().Trim();
        //                            Guid guBAId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                            var varFBUser = context.FBs.Where(x => x.BarAvordId == guBAId && x.Shomareh == strAddedItem).ToList();
        //                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
        //                            //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + DtBA.Rows[0]["Id"].ToString() + " and Shomareh='" + Dr[idr]["AddedItems"].ToString().Trim() + "'");
        //                            Guid intFBId = new Guid();
        //                            if (DtFBUser.Rows.Count == 0)
        //                            {
        //                                clsFB FB = new clsFB();
        //                                FB.BarAvordId = Guid.Parse(DtBA.Rows[0]["ID"].ToString());
        //                                FB.Shomareh = Dr[idr]["AddedItems"].ToString().Trim();
        //                                FB.BahayeVahedZarib = 0;
        //                                context.FBs.Add(FB);
        //                                context.SaveChanges();
        //                                intFBId = FB.ID;
        //                                //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim(), 0);
        //                            }
        //                            else
        //                                intFBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

        //                            Guid guFBId = Guid.Parse(DtFB.Rows[0]["ID"].ToString());
        //                            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
        //                                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                    select new
        //                                                    {
        //                                                        ID = RizMetreUsers.ID,
        //                                                        Shomareh = RizMetreUsers.Shomareh,
        //                                                        Tedad = RizMetreUsers.Tedad,
        //                                                        Tool = RizMetreUsers.Tool,
        //                                                        Arz = RizMetreUsers.Arz,
        //                                                        Ertefa = RizMetreUsers.Ertefa,
        //                                                        Vazn = RizMetreUsers.Vazn,
        //                                                        Des = RizMetreUsers.Des,
        //                                                        FBId = RizMetreUsers.FBId,
        //                                                        OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                        ForItem = RizMetreUsers.ForItem,
        //                                                        Type = RizMetreUsers.Type,
        //                                                        UseItem = RizMetreUsers.UseItem,
        //                                                        BarAvordId = FB.BarAvordId
        //                                                    }).Where(x => x.FBId == guFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();
        //                            DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

        //                            var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
        //                                                           join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
        //                                                           select new
        //                                                           {
        //                                                               ID = RizMetreUsers.ID,
        //                                                               Shomareh = RizMetreUsers.Shomareh,
        //                                                               Tedad = RizMetreUsers.Tedad,
        //                                                               Tool = RizMetreUsers.Tool,
        //                                                               Arz = RizMetreUsers.Arz,
        //                                                               Ertefa = RizMetreUsers.Ertefa,
        //                                                               Vazn = RizMetreUsers.Vazn,
        //                                                               Des = RizMetreUsers.Des,
        //                                                               FBId = RizMetreUsers.FBId,
        //                                                               OperationsOfHamlId = RizMetreUsers.OperationsOfHamlId,
        //                                                               ForItem = RizMetreUsers.ForItem,
        //                                                               Type = RizMetreUsers.Type,
        //                                                               UseItem = RizMetreUsers.UseItem,
        //                                                               BarAvordId = FB.BarAvordId
        //                                                           }).Where(x => x.FBId == intFBId && x.ForItem == strShomareh && x.Type == "2").ToList();
        //                            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
        //                            //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + DtFB.Rows[0]["Id"].ToString() + " and Type=1");
        //                            //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + intFBId + " and ForItem='" + DtFB.Rows[0]["Shomareh"].ToString().Trim() + "' and Type=2");
        //                            for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
        //                            {
        //                                string strConditionOp = strCondition.Replace("x", DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim());
        //                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
        //                                if (blnCheck)
        //                                {
        //                                    decimal ArzEzafi = 1;
        //                                    string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
        //                                    if (strFinalWorking != "")
        //                                    {
        //                                        strFinalWorking = strFinalWorking.Replace("x", DtRizMetreUsers.Rows[i]["Arz"].ToString().Trim());
        //                                        ArzEzafi = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
        //                                    }

        //                                    DataRow[] DrRizMetreUsersCurrent = DtRizMetreUsersCurrent.Select("shomareh=" + int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString()));
        //                                    if (DrRizMetreUsersCurrent.Length == 0)
        //                                    {
        //                                        clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
        //                                        RizMetreUsers.Shomareh = int.Parse(DtRizMetreUsers.Rows[i]["Shomareh"].ToString());
        //                                        RizMetreUsers.Sharh = DtRizMetreUsers.Rows[i]["Sharh"].ToString().Trim();

        //                                        if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString()) * ArzEzafi;
        //                                        else
        //                                            RizMetreUsers.Tedad = 0;

        //                                        if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Tool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
        //                                        else
        //                                            RizMetreUsers.Tool = 0;

        //                                        if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Arz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
        //                                        else
        //                                            RizMetreUsers.Arz = 0;

        //                                        if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Ertefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
        //                                        else
        //                                            RizMetreUsers.Ertefa = 0;

        //                                        if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
        //                                            RizMetreUsers.Vazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
        //                                        else
        //                                            RizMetreUsers.Vazn = 0;

        //                                        RizMetreUsers.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.FBId = intFBId;
        //                                        RizMetreUsers.OperationsOfHamlId = 1;
        //                                        RizMetreUsers.Type = "2";
        //                                        RizMetreUsers.ForItem = DtFB.Rows[0]["Shomareh"].ToString().Trim();
        //                                        RizMetreUsers.UseItem = "";
        //                                        RizMetreUsers.LevelNumber = LevelNumber;


        //                                        ///محاسبه مقدار جزء
        //                                        dMeghdarJoz = 0;
        //                                        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
        //                                            dMeghdarJoz = 0;
        //                                        else
        //                                            dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
        //                                            (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        //                                        RizMetre.MeghdarJoz = dMeghdarJoz;


        //                                        context.RizMetreUserses.Add(RizMetreUsers);
        //                                        context.SaveChanges();
        //                                        //RizMetreUsers.Save();
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //}
        //return new JsonResult("OK");
        //}
        //catch (Exception)
        //{
        //    return "NOK";
        //}
    }


    [HttpPost]
    public JsonResult GetCurrentRizMetreUsersForShowBarAvord([FromBody] GetCurrentRizMetreUsersForShowBarAvordInputDto request)
    {
        Guid FBId = Guid.Parse(request.FBId);
        List<clsRizMetreUsers>? rizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId).OrderBy(x => x.Shomareh).ToList();
        return new JsonResult(rizMetreUsers);
    }

    [HttpPost]
    public JsonResult ConfirmRizMetreUsersFromShowBarAvord([FromBody] RizMetreFromShowBarAvordInputDto Request)
    {
        string Sharh = Request.Sharh;
        decimal Tedad = Request.Tedad;
        decimal Tool = Request.Tool;
        decimal Arz = Request.Arz;
        decimal Ertefa = Request.Ertefa;
        decimal Vazn = Request.Vazn;
        string Des = Request.Des;
        string FBShomareh = Request.Shomareh;
        Guid BarAvordId = Request.BarAvordUserId;
        string Code = Request.Shomareh.Substring(0, 2);
        DateTime Now = DateTime.Now;

        //try
        //{
        clsFB? FB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == FBShomareh).FirstOrDefault();
        clsBaravordUser? UB = _context.BaravordUsers.Where(x => x.ID == Request.BarAvordUserId).FirstOrDefault();

        Guid FBId = new Guid();
        if (FB == null)
        {
            clsFB newFB = new clsFB();
            newFB.BarAvordId = BarAvordId;
            newFB.Shomareh = FBShomareh;
            newFB.BahayeVahedZarib = 0;
            _context.FBs.Add(newFB);
            _context.SaveChanges();
            FBId = newFB.ID;
        }
        else
            FBId = FB.ID;

        clsRizMetreUsers? RizMetreUser = context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
        long Shomareh = 0;
        if (RizMetreUser != null)
            Shomareh = RizMetreUser.Shomareh + 1;
        else
            Shomareh = 1;

        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.ID = Guid.NewGuid();
        RizMetre.Shomareh = Shomareh;
        RizMetre.Sharh = Sharh.Trim();
        RizMetre.Tedad = Tedad == 0 ? 1 : Tedad;
        RizMetre.Tool = Tool == 0 ? 1 : Tool;
        RizMetre.Arz = Arz == 0 ? 1 : Arz;
        RizMetre.Ertefa = Ertefa == 0 ? 1 : Ertefa;
        RizMetre.Vazn = Vazn == 0 ? 1 : Vazn;
        RizMetre.Des = Des.Trim();
        RizMetre.FBId = FBId;
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "1";
        RizMetre.ForItem = null;
        RizMetre.UseItem = "";
        RizMetre.LevelNumber = 1;
        RizMetre.InsertDateTime = Now;


        ///محاسبه مقدار جزء
        decimal dMeghdarJoz = 0;
        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
            dMeghdarJoz = 0;
        else
            dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
            (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
        RizMetre.MeghdarJoz = dMeghdarJoz;

        context.RizMetreUserses.Add(RizMetre);
        context.SaveChanges();

        decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == FBId).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);
        /////
        //////
        //////محاسبه کل فصل
        ///
        decimal SumMeghdarFasl = 0;
        var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId)
                .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
                {
                    Id = riz.ID,
                    Sharh = riz.Sharh,
                    Shomareh = riz.Shomareh,
                    Arz = riz.Arz,
                    Des = riz.Des,
                    Ertefa = riz.Ertefa,
                    Tedad = riz.Tedad,
                    Tool = riz.Tool,
                    Vazn = riz.Vazn,
                    FBId = riz.FBId,
                    MeghdarJoz = riz.MeghdarJoz
                }).OrderBy(x => x.Shomareh).ToList();


        // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        var fbItems = _context.FBs
            .Where(f => f.BarAvordId == BarAvordId)
            .GroupBy(f => f.Shomareh)
            .Select(g => g.FirstOrDefault())
            .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        var userBarAvordOutPut = _context.FehrestBahas
            .Where(fehrest => fehrest.Sal == UB.Year && fehrest.Shomareh.StartsWith(Code))
            .ToList() // انتقال به حافظه
            .GroupJoin(fbItems,
                fehrest => fehrest.Shomareh,
                fb => fb.Shomareh,
                (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
            .OrderBy(x => x.fehrest.Shomareh)
            .Select(x => new ViewUserBarAvordOutPutDto
            {
                ItemFbShomareh = x.fehrest.Shomareh,
                Sharh = x.fehrest.Sharh,
                BahayeVahed = x.fehrest.BahayeVahed,
                Vahed = x.fehrest.Vahed,
                FBId = x.fbItem != null ? x.fbItem.ID : null,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
            }).ToList();


        decimal JameFasl = 0;
        foreach (var item in userBarAvordOutPut)
        {
            decimal Meghdar = 0;
            foreach (var RM in RizMetreAll)
            {
                if (item.FBId == RM.FBId)
                {
                    item.RizMetre.Add(RM);
                    Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                }
            }
            item.Meghdar = Meghdar;
            decimal dBahayeKol = Meghdar * (item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed));
            item.BahayeKol = dBahayeKol;
            JameFasl += dBahayeKol;
        }


        return new JsonResult("OK_" + FBId + "_" + dMeghdarJoz + "_" + SumMeghdarJoz + "_" + JameFasl);
    }

    public JsonResult UpdateRizMetreUsersFrmShowBarAvord([FromBody] UpdateRizMetreUsersInputDto request)
    {
        Guid Id = request.Id;
        string Sharh = request.Sharh;
        decimal? Tedad = request.Tedad;
        decimal? Tool = request.Tool;
        decimal? Arz = request.Arz;
        decimal? Ertefa = request.Ertefa;
        decimal? Vazn = request.Vazn;
        string Des = request.Des;
        int LevelNumber = request.LevelNumber;

        int Year = request.Year;
        Guid BarAvordId = request.BarAvordUserId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        string Code = request.Code;

        clsRizMetreUsers? entity = context.RizMetreUserses.FirstOrDefault(x => x.ID == Id);
        if (entity == null)
            return new JsonResult("NOK");

        entity.Sharh = Sharh;
        entity.Tedad = Tedad;
        entity.Tool = Tool;
        entity.Arz = Arz;
        entity.Ertefa = Ertefa;
        entity.Vazn = Vazn;
        entity.Des = Des;
        entity.LevelNumber = request.LevelNumber;

        decimal dMeghdarJoz = 0;
        if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
            dMeghdarJoz = 0;
        else
            dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
        entity.MeghdarJoz = dMeghdarJoz;

        context.SaveChanges();

        decimal SumMeghdarJoz = context.RizMetreUserses.Where(x => x.FBId == entity.FBId).Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);


        /////
        //////
        //////محاسبه کل فصل
        ///
        decimal SumMeghdarFasl = 0;
        var RizMetreAll = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId)
                .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
                {
                    Id = riz.ID,
                    Sharh = riz.Sharh,
                    Shomareh = riz.Shomareh,
                    Arz = riz.Arz,
                    Des = riz.Des,
                    Ertefa = riz.Ertefa,
                    Tedad = riz.Tedad,
                    Tool = riz.Tool,
                    Vazn = riz.Vazn,
                    FBId = riz.FBId,
                    MeghdarJoz = riz.MeghdarJoz
                }).OrderBy(x => x.Shomareh).ToList();


        // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        var fbItems = _context.FBs
            .Where(f => f.BarAvordId == BarAvordId)
            .GroupBy(f => f.Shomareh)
            .Select(g => g.FirstOrDefault())
            .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        var userBarAvordOutPut = _context.FehrestBahas
            .Where(fehrest => fehrest.Sal == Year && fehrest.Shomareh.StartsWith(Code))
            .ToList() // انتقال به حافظه
            .GroupJoin(fbItems,
                fehrest => fehrest.Shomareh,
                fb => fb.Shomareh,
                (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
            .OrderBy(x => x.fehrest.Shomareh)
            .Select(x => new ViewUserBarAvordOutPutDto
            {
                ItemFbShomareh = x.fehrest.Shomareh,
                Sharh = x.fehrest.Sharh,
                BahayeVahed = x.fehrest.BahayeVahed,
                Vahed = x.fehrest.Vahed,
                FBId = x.fbItem != null ? x.fbItem.ID : null,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
            }).ToList();


        decimal JameFasl = 0;
        foreach (var item in userBarAvordOutPut)
        {
            decimal Meghdar = 0;
            foreach (var RM in RizMetreAll)
            {
                if (item.FBId == RM.FBId)
                {
                    item.RizMetre.Add(RM);
                    Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                }
            }
            item.Meghdar = Meghdar;
            decimal dBahayeKol = Meghdar * (item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed));
            item.BahayeKol = dBahayeKol;
            JameFasl += dBahayeKol;
        }


        return new JsonResult("OK_" + dMeghdarJoz + "_" + SumMeghdarJoz + "_" + JameFasl);
    }

    [HttpPost]
    public JsonResult UpdateRizMetreUsers([FromBody] UpdateRizMetreUsersInputDto request)
    {
        try
        {

            Guid Id = request.Id;

            clsRizMetreUsers? clsRizMetreUsers1 = context.RizMetreUserses.Where(x => x.ID == Id).FirstOrDefault();


            RizMetreInputDto OldRizMetre = new RizMetreInputDto
            {
                Sharh = clsRizMetreUsers1.Sharh,
                Tedad = clsRizMetreUsers1.Tedad,
                Tool = clsRizMetreUsers1.Tool,
                Arz = clsRizMetreUsers1.Arz,
                Ertefa = clsRizMetreUsers1.Ertefa,
                Vazn = clsRizMetreUsers1.Vazn,
                Des = clsRizMetreUsers1.Des,
                FBId = clsRizMetreUsers1.FBId,
                ForItem = clsRizMetreUsers1.ForItem,
                LevelNumber = clsRizMetreUsers1.LevelNumber.Value,
                Shomareh = clsRizMetreUsers1.Shomareh
            };

            string Sharh = request.Sharh;
            decimal? Tedad = request.Tedad;
            decimal? Tool = request.Tool;
            decimal? Arz = request.Arz;
            decimal? Ertefa = request.Ertefa;
            decimal? Vazn = request.Vazn;
            string Des = request.Des;
            NoeFehrestBaha NoeFB = request.NoeFB;
            int Year = request.Year;
            int LevelNumber = request.LevelNumber == 0 ? 1 : request.LevelNumber;
            DastyarCommon DastyarCommon = new DastyarCommon(context);

            DataTable DtRizMetreUsers = new DataTable();
            if (request.LevelNumber == 0)
            {
                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                            BarAvordId = FB.BarAvordId
                                        }).Where(x => x.ID == Id).OrderBy(x => x.Shomareh).ToList();
                DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
            }
            else
            {
                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                            BarAvordId = FB.BarAvordId
                                        }).Where(x => x.ID == Id).OrderBy(x => x.Shomareh).ToList();
                DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
            }

            // DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + Id);
            int intShomareh = int.Parse(DtRizMetreUsers.Rows[0]["Shomareh"].ToString().Trim());
            string Type = DtRizMetreUsers.Rows[0]["Type"].ToString().Trim();
            Guid intFBId = Guid.Parse(DtRizMetreUsers.Rows[0]["FBId"].ToString().Trim());
            var varFBUser = context.FBs.Where(x => x.ID == intFBId).ToList();
            DataTable DtFBUsers = clsConvert.ToDataTable(varFBUser);
            //DataTable DtFBUsers = clsOperationItemsFB.FBListWithParameter("Id=" + intFBId);
            Guid intBarAvordId = Guid.Parse(DtFBUsers.Rows[0]["BarAvordId"].ToString().Trim());
            string strFBShomareh = DtFBUsers.Rows[0]["Shomareh"].ToString().Trim();
            string strForItemCurrent = DtRizMetreUsers.Rows[0]["ForItem"].ToString().Trim();


            //var varItemsHasConditionAddedToFB = (from clsItemsHasConditionAddedToFB in context.ItemsHasConditionAddedToFBs
            //                                     join tblItemsHasConditionConditionContext in context.ItemsHasCondition_ConditionContexts
            //                                     on clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals tblItemsHasConditionConditionContext.Id
            //                                     join tblItemsHasCondition in context.ItemsHasConditions on tblItemsHasConditionConditionContext.ItemsHasConditionId equals tblItemsHasCondition.Id
            //                                     select new
            //                                     {
            //                                         clsItemsHasConditionAddedToFB.ID,
            //                                         clsItemsHasConditionAddedToFB.FBShomareh,
            //                                         clsItemsHasConditionAddedToFB.BarAvordId,
            //                                         clsItemsHasConditionAddedToFB.Meghdar,
            //                                         clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
            //                                         clsItemsHasConditionAddedToFB.ConditionGroupId,
            //                                         ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh
            //                                     }).Where(x => x.BarAvordId == intBarAvordId && x.FBShomareh == strFBShomareh).ToList();
            //DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
            ////DataTable DtItemsHasConditionAddedToFB = clsItemsHasConditionAddedToFB.ListWithParameterSimple("BarAvordId=" + intBarAvordId + " and FBShomareh='" + strFBShomareh + "'");
            //var varItemsFBShomarehValueShomareh = context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == strFBShomareh && x.BarAvordId == intBarAvordId).ToList();
            //DataTable DtItemsFBShomarehValueShomareh = clsConvert.ToDataTable(varItemsFBShomarehValueShomareh);
            ////DataTable DtItemsFBShomarehValueShomareh = clsItemsFBShomarehValueShomareh.ListWithParameter("FBShomareh='" + strFBShomareh + "' and BarAvordId=" + intBarAvordId);
            var varItemsFields = (from ItemF in context.ItemsFieldses
                                  join OpItemFB in context.Operation_ItemsFBs
                                  on ItemF.ItemShomareh equals OpItemFB.ItemsFBShomareh
                                  select new
                                  {
                                      ItemShomareh = ItemF.ItemShomareh,
                                      NoeFB = ItemF.NoeFB,
                                      IsEnteringValue = ItemF.IsEnteringValue,
                                      Vahed = ItemF.Vahed,
                                      FieldType = ItemF.FieldType,
                                      OperationId = OpItemFB.OperationId
                                  }).Where(x => x.ItemShomareh == strFBShomareh && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();
            DataTable DtItemsFields = clsConvert.ToDataTable(varItemsFields);
            //DataTable DtItemsFields = clsItemsFields.ItemsFieldsListWithParameter("ItemShomareh='" + strFBShomareh + "' and NoeFB=234");

            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                RizMetre.Tedad = Tedad;
            else
                RizMetre.Tedad = null;

            if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                RizMetre.Tool = Tool;
            else
                RizMetre.Tool = null;

            if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                RizMetre.Arz = Arz;
            else
                RizMetre.Arz = null;

            if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                RizMetre.Ertefa = Ertefa;
            else
                RizMetre.Ertefa = null;

            if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                RizMetre.Vazn = Vazn;
            else
                RizMetre.Vazn = null;

            RizMetre.ID = Id;
            RizMetre.Sharh = Sharh.Trim();
            //RizMetre.Tedad = Tedad;
            //RizMetre.Tool = Tool;
            //RizMetre.Arz = Arz;
            //RizMetre.Ertefa = Ertefa;
            //RizMetre.Vazn = Vazn;
            RizMetre.Des = Des.Trim();
            RizMetre.LevelNumber = LevelNumber;
            RizMetre.Type = Type;
            RizMetre.ForItem = strForItemCurrent.Trim();


            decimal dMeghdarJoz = 0;
            if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);

            RizMetre.MeghdarJoz = dMeghdarJoz;

            bool blnCheckUpdate1 = false;



            if (clsRizMetreUsers1 != null)
            {
                RizMetre.FBId = clsRizMetreUsers1.FBId;
                RizMetre.Shomareh = clsRizMetreUsers1.Shomareh;
                RizMetre.ShomarehNew = clsRizMetreUsers1.ShomarehNew;
                RizMetre.OperationsOfHamlId = 1;
                try
                {
                    context.Entry(clsRizMetreUsers1).CurrentValues.SetValues(RizMetre);
                    context.SaveChanges();
                    blnCheckUpdate1 = true;
                }
                catch (Exception e)
                {
                    throw e;
                    //blnCheckUpdate1 = false;
                }
            }
            //if (RizMetre.Update())
            if (blnCheckUpdate1)
            {


                /////////////
                //////////////
                //////////////
                ///
                long OperationId = request.OperationId;
                Guid FBId = request.FBId;

                clsOperation_ItemsFB operation_ItemsFB = context.Operation_ItemsFBs.First(x => x.OperationId == OperationId);


                List<ItemsFieldsDto> ItemsField = (from ItemF in context.ItemsFieldses
                                                   join OpItemFB in context.Operation_ItemsFBs
                                                   on ItemF.ItemShomareh equals OpItemFB.ItemsFBShomareh
                                                   select new ItemsFieldsDto
                                                   {
                                                       ItemShomareh = ItemF.ItemShomareh,
                                                       NoeFB = ItemF.NoeFB,
                                                       IsEnteringValue = ItemF.IsEnteringValue,
                                                       Vahed = ItemF.Vahed,
                                                       FieldType = ItemF.FieldType,
                                                       OperationId = OpItemFB.OperationId
                                                   }).Where(x => x.OperationId == OperationId && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();

                List<ItemsHasConditionConditionContextForCheckOperationDto> lstItemsHasCondition = _context.ItemsHasCondition_ConditionContexts
                    .Where(cc => cc.Year == Year)
                    .Join(_context.ItemsHasConditionAddedToFBs,
                    cc => cc.Id,
                    fb => fb.ItemsHasCondition_ConditionContextId,
            (cc, fb) => new { cc, fb })
                .Join(_context.ItemsHasConditions,
            temp => temp.cc.ItemsHasConditionId,
            ihc => ihc.Id,
            (temp, ihc) => new { temp.cc, temp.fb, ihc })
                .Join(_context.Operation_ItemsFBs,
            temp => temp.ihc.ItemFBShomareh,
            ofb => ofb.ItemsFBShomareh,
            (temp, ofb) => new ItemsHasConditionConditionContextForCheckOperationDto
            {
                Id = temp.cc.Id,
                ItemsHasConditionId = temp.cc.ItemsHasConditionId,
                ConditionContextId = temp.cc.ConditionContextId,
                HasEnteringValue = temp.cc.HasEnteringValue,
                Des = temp.cc.Des,
                DefaultValue = temp.cc.DefaultValue,
                IsShow = temp.cc.IsShow,
                ParentId = temp.cc.ParentId,
                MoveToRel = temp.cc.MoveToRel,
                ViewCheckAllRecords = temp.cc.ViewCheckAllRecords,
                StepChange = temp.cc.StepChange,
                Meghdar = temp.fb.Meghdar,
                Meghdar2 = temp.fb.Meghdar2,
                FBShomareh = temp.fb.FBShomareh,
                ConditionGroupId = temp.fb.ConditionGroupId,
                BarAvordId = temp.fb.BarAvordId,
                OperationId = ofb.OperationId
            }).Where(x => x.OperationId == OperationId && x.BarAvordId == intBarAvordId).ToList();


                var ItemsAddingToFBs = _context.ItemsAddingToFBs.Where(x => x.Year == Year).Select(x => new
                {
                    x.ItemsHasCondition_ConditionContextId,
                    x.AddedItems,
                    x.Condition,
                    x.FinalWorking,
                    x.ConditionType,
                    x.DesOfAddingItems,
                    x.UseItemForAdd,
                    x.FieldsAdding,
                    x.CharacterPlus
                }).ToList();

                foreach (var ItemHasCon in lstItemsHasCondition)
                {
                    List<ItemsAddingToFBForCheckOperationDto> lstItemsAddingToFBForCheckOperation = ItemsAddingToFBs.Where(x => x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id).Select(x => new
                    ItemsAddingToFBForCheckOperationDto
                    {
                        ItemsHasCondition_ConditionContextId = x.ItemsHasCondition_ConditionContextId,
                        AddedItems = x.AddedItems,
                        Condition = x.Condition,
                        FinalWorking = x.FinalWorking,
                        ConditionType = x.ConditionType,
                        DesOfAddingItems = x.DesOfAddingItems,
                        UseItemForAdd = x.UseItemForAdd,
                        FieldsAdding = x.FieldsAdding,
                        CharacterPlus = x.CharacterPlus
                    }).ToList();

                    CheckOperationConditions checkOperationConditions = new CheckOperationConditions();
                    foreach (var itemsAddingToFBForCheckOperation in lstItemsAddingToFBForCheckOperation)
                    {
                        checkOperationConditions.fnCheckOperationConditionForUpdate(_context, itemsAddingToFBForCheckOperation,
                            ItemsField, ItemHasCon, FBId, RizMetre, OldRizMetre, LevelNumber, Year, NoeFB);
                    }
                }

                //////////
                ///////////////
                //////////////
                ///


                return new JsonResult("OK_" + operation_ItemsFB.ItemsFBShomareh);

                //if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                //{
                //    //string strItemsHasConditionConditionContext = "";
                //    //if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                //    //{
                //    //    strItemsHasConditionConditionContext += "ItemsHasCondition_ConditionContextId in(";
                //    //    for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                //    //    {
                //    //        if ((i + 1) < DtItemsHasConditionAddedToFB.Rows.Count)
                //    //            strItemsHasConditionConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString() + ",";
                //    //        else
                //    //            strItemsHasConditionConditionContext += DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString();
                //    //    }
                //    //    strItemsHasConditionConditionContext += ")";
                //    //}

                //    List<long> strItemsHasConditionConditionContext = new List<long>();
                //    if (DtItemsHasConditionAddedToFB.Rows.Count != 0)
                //    {
                //        for (int i = 0; i < DtItemsHasConditionAddedToFB.Rows.Count; i++)
                //        {
                //            strItemsHasConditionConditionContext.Add(long.Parse(DtItemsHasConditionAddedToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString()));
                //        }
                //    }

                //    var varItemsAddingToFB = context.ItemsAddingToFBs.Where(x => strItemsHasConditionConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
                //    DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);


                //    //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasConditionConditionContext);
                //    for (int Counter = 0; Counter < DtItemsHasConditionAddedToFB.Rows.Count; Counter++)
                //    {
                //        decimal Meghdar = decimal.Parse(DtItemsHasConditionAddedToFB.Rows[Counter]["Meghdar"].ToString());
                //        string RBCode = DtItemsHasConditionAddedToFB.Rows[Counter]["ItemsHasCondition_ConditionContextId"].ToString().Trim();
                //        DataRow[] Dr = DtItemsAddingToFB.Select("ItemsHasCondition_ConditionContextId=" + RBCode);
                //        if (Dr.Length != 0)
                //        {
                //            for (int idr = 0; idr < Dr.Length; idr++)
                //            {
                //                string strFBShomarehAdded = Dr[idr]["AddedItems"].ToString().Trim();
                //                switch (Dr[idr]["ConditionType"].ToString())
                //                {
                //                    case "1":
                //                        {

                //                            var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strFBShomarehAdded + "A").ToList();
                //                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                //                            //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "A'");
                //                            if (DtFBUsersAdded.Rows.Count != 0)
                //                            {
                //                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();

                //                                if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tedad = Tedad;
                //                                else
                //                                    RizMetreUsers.Tedad = 0;

                //                                if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tool = Tool;
                //                                else
                //                                    RizMetreUsers.Tool = 0;

                //                                if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Arz = Arz;
                //                                else
                //                                    RizMetreUsers.Arz = 0;

                //                                if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Ertefa = Ertefa;
                //                                else
                //                                    RizMetreUsers.Ertefa = 0;

                //                                if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Vazn = Vazn;
                //                                else
                //                                    RizMetreUsers.Vazn = 0;

                //                                RizMetreUsers.Shomareh = intShomareh;
                //                                RizMetreUsers.Sharh = Sharh;
                //                                //RizMetreUsers.Tedad = Tedad;
                //                                //RizMetreUsers.Tool = Tool;
                //                                //RizMetreUsers.Arz = Arz;
                //                                //RizMetreUsers.Ertefa = Ertefa;
                //                                //RizMetreUsers.Vazn = Vazn;
                //                                RizMetreUsers.ForItem = strFBShomareh.Trim();
                //                                RizMetreUsers.UseItem = "";
                //                                RizMetreUsers.LevelNumber = LevelNumber;
                //                                RizMetreUsers.FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                RizMetreUsers.FBId = guFBId;

                //                                dMeghdarJoz = 0;
                //                                if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                    dMeghdarJoz = 0;
                //                                else
                //                                    dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                    (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                //                                RizMetreUsers.OperationsOfHamlId = 1;

                //                                string strForItem = strFBShomareh.Trim().Substring(0, 6);
                //                                string strUseItem = "";
                //                                clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                && x.ForItem == strForItem && x.UseItem == strUseItem).FirstOrDefault();
                //                                if (clsRizMetreUsers != null)
                //                                {
                //                                    try
                //                                    {
                //                                        var entry = context.Entry(clsRizMetreUsers);
                //                                        var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                        foreach (var property in entry.OriginalValues.Properties)
                //                                        {
                //                                            if (!excludedProperties.Contains(property.Name))
                //                                            {
                //                                                var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                entry.CurrentValues[property] = newValue;
                //                                            }
                //                                        }
                //                                        //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                        context.SaveChanges();
                //                                    }
                //                                    catch (Exception)
                //                                    {
                //                                    }
                //                                }
                //                                //RizMetreUsers.UpdateWithFBIdAndShomareh();
                //                            }
                //                            break;
                //                        }
                //                    case "2":
                //                        {
                //                            var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                //                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                //                            //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                //                            if (DtFBUsersAdded.Rows.Count != 0)
                //                            {
                //                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();

                //                                if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tedad = Tedad;
                //                                else
                //                                    RizMetreUsers.Tedad = 0;

                //                                if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tool = Tool;
                //                                else
                //                                    RizMetreUsers.Tool = 0;

                //                                if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Arz = Arz;
                //                                else
                //                                    RizMetreUsers.Arz = 0;

                //                                if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Ertefa = Ertefa;
                //                                else
                //                                    RizMetreUsers.Ertefa = 0;

                //                                if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Vazn = Vazn;
                //                                else
                //                                    RizMetreUsers.Vazn = 0;

                //                                RizMetreUsers.Shomareh = intShomareh;
                //                                RizMetreUsers.Sharh = Sharh;
                //                                //RizMetreUsers.Tedad = Tedad;
                //                                //RizMetreUsers.Tool = Tool;
                //                                //RizMetreUsers.Arz = Arz;
                //                                //RizMetreUsers.Ertefa = Ertefa;
                //                                //RizMetreUsers.Vazn = Vazn;
                //                                RizMetreUsers.ForItem = strFBShomareh.Trim();
                //                                RizMetreUsers.UseItem = "";
                //                                RizMetreUsers.LevelNumber = LevelNumber;
                //                                RizMetreUsers.FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                RizMetreUsers.FBId = guFBId;

                //                                dMeghdarJoz = 0;
                //                                if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                    dMeghdarJoz = 0;
                //                                else
                //                                    dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                    (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                //                                RizMetreUsers.OperationsOfHamlId = 1;


                //                                string strForItem = strFBShomareh.Trim().Substring(0, 6);
                //                                string strUseItem = "";
                //                                clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                && x.ForItem == strForItem && x.UseItem == strUseItem).FirstOrDefault();
                //                                if (clsRizMetreUsers != null)
                //                                {
                //                                    try
                //                                    {
                //                                        var entry = context.Entry(clsRizMetreUsers);
                //                                        var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                        foreach (var property in entry.OriginalValues.Properties)
                //                                        {
                //                                            if (!excludedProperties.Contains(property.Name))
                //                                            {
                //                                                var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                entry.CurrentValues[property] = newValue;
                //                                            }
                //                                        }
                //                                        //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                        context.SaveChanges();
                //                                    }
                //                                    catch (Exception)
                //                                    {
                //                                    }
                //                                }
                //                                //RizMetreUsers.UpdateWithFBIdAndShomareh();
                //                            }
                //                            break;
                //                        }
                //                    case "3":
                //                        {
                //                            decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
                //                            string strStatus = dPercent > 0 ? "B" : "e";
                //                            var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strFBShomarehAdded + strStatus).ToList();
                //                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                //                            //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + strStatus + "'");
                //                            if (DtFBUsersAdded.Rows.Count != 0)
                //                            {
                //                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();

                //                                if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tedad = Tedad;
                //                                else
                //                                    RizMetreUsers.Tedad = 0;

                //                                if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tool = Tool;
                //                                else
                //                                    RizMetreUsers.Tool = 0;

                //                                if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Arz = Arz;
                //                                else
                //                                    RizMetreUsers.Arz = 0;

                //                                if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Ertefa = Ertefa;
                //                                else
                //                                    RizMetreUsers.Ertefa = 0;

                //                                if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Vazn = Vazn;
                //                                else
                //                                    RizMetreUsers.Vazn = 0;

                //                                RizMetreUsers.Shomareh = intShomareh;
                //                                RizMetreUsers.Sharh = Sharh;
                //                                //RizMetreUsers.Tedad = Tedad;
                //                                //RizMetreUsers.Tool = Tool;
                //                                //RizMetreUsers.Arz = Arz;
                //                                //RizMetreUsers.Ertefa = Ertefa;
                //                                //RizMetreUsers.Vazn = Vazn;
                //                                RizMetreUsers.ForItem = strFBShomareh.Trim();
                //                                RizMetreUsers.UseItem = "";
                //                                RizMetreUsers.LevelNumber = LevelNumber;
                //                                //RizMetreUsers.FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                RizMetreUsers.FBId = guFBId;

                //                                dMeghdarJoz = 0;
                //                                if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                    dMeghdarJoz = 0;
                //                                else
                //                                    dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                    (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                //                                RizMetreUsers.OperationsOfHamlId = 1;

                //                                string strForItem = strFBShomareh.Trim().Substring(0, 6);
                //                                string strUseItem = "";
                //                                clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                && x.ForItem == strForItem && x.UseItem == strUseItem).FirstOrDefault();
                //                                if (clsRizMetreUsers != null)
                //                                {
                //                                    try
                //                                    {
                //                                        var entry = context.Entry(clsRizMetreUsers);
                //                                        var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                        foreach (var property in entry.OriginalValues.Properties)
                //                                        {
                //                                            if (!excludedProperties.Contains(property.Name))
                //                                            {
                //                                                var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                entry.CurrentValues[property] = newValue;
                //                                            }
                //                                        }

                //                                        //var Original = clsRizMetreUsers;
                //                                        //clsRizMetreUsers.ID = Original.ID;
                //                                        //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                        context.SaveChanges();
                //                                    }
                //                                    catch (Exception)
                //                                    {
                //                                    }
                //                                }
                //                                //RizMetreUsers.UpdateWithFBIdAndShomareh();
                //                            }
                //                            break;
                //                        }
                //                    case "4":
                //                        {
                //                            var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                //                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                //                            //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                //                            if (DtFBUsersAdded.Rows.Count != 0)
                //                            {
                //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
                //                                string strFinalWorking = Dr[idr]["FinalWorking"].ToString().Trim();
                //                                if (strFinalWorking != "")
                //                                {
                //                                    string strConditionOp = strCondition.Replace("x", Meghdar.ToString().Trim());
                //                                    StringToFormula StringToFormula = new StringToFormula();
                //                                    bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                //                                    if (blnCheck)
                //                                    {
                //                                        string strForItem = "";
                //                                        string strUseItem = "";
                //                                        if (Dr[idr]["UseItemForAdd"].ToString().Trim() == "")
                //                                        {
                //                                            strForItem = strFBShomareh.Trim();
                //                                            strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                //                                            decimal dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                //                                            clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                //                                            RizMetreUsers.Shomareh = intShomareh;
                //                                            RizMetreUsers.Sharh = Sharh;


                //                                            if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                                RizMetreUsers.Tedad = ((Tedad == 0) ? 1 : Tedad) * dMultiple;
                //                                            else
                //                                                RizMetreUsers.Tedad = 0;

                //                                            if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                                RizMetreUsers.Tool = Tool;
                //                                            else
                //                                                RizMetreUsers.Tool = 0;

                //                                            if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                                RizMetreUsers.Arz = Arz;
                //                                            else
                //                                                RizMetreUsers.Arz = 0;

                //                                            if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                                RizMetreUsers.Ertefa = Ertefa;
                //                                            else
                //                                                RizMetreUsers.Ertefa = 0;

                //                                            if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                                RizMetreUsers.Vazn = Vazn;
                //                                            else
                //                                                RizMetreUsers.Vazn = 0;

                //                                            //RizMetreUsers.Tedad = ((Tedad == 0) ? 1 : Tedad) * dMultiple;
                //                                            //RizMetreUsers.Tool = Tool;
                //                                            //RizMetreUsers.Arz = Arz;
                //                                            //RizMetreUsers.Ertefa = Ertefa;
                //                                            //RizMetreUsers.Vazn = Vazn;
                //                                            RizMetreUsers.ForItem = strForItem;
                //                                            RizMetreUsers.UseItem = strUseItem;
                //                                            RizMetreUsers.LevelNumber = LevelNumber;
                //                                            Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                            RizMetreUsers.FBId = guFBId;

                //                                            dMeghdarJoz = 0;
                //                                            if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                                dMeghdarJoz = 0;
                //                                            else
                //                                                dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                                (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                //                                            RizMetreUsers.OperationsOfHamlId = 1;

                //                                            string strForItem1 = strFBShomareh.Trim().Substring(0, 6);
                //                                            clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                            && x.ForItem == strForItem1 && x.UseItem == strUseItem).FirstOrDefault();
                //                                            if (clsRizMetreUsers != null)
                //                                            {
                //                                                try
                //                                                {
                //                                                    var entry = context.Entry(clsRizMetreUsers);
                //                                                    var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                                    foreach (var property in entry.OriginalValues.Properties)
                //                                                    {
                //                                                        if (!excludedProperties.Contains(property.Name))
                //                                                        {
                //                                                            var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                            entry.CurrentValues[property] = newValue;
                //                                                        }
                //                                                    }

                //                                                    //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                                    context.SaveChanges();
                //                                                }
                //                                                catch (Exception)
                //                                                {
                //                                                }
                //                                            }
                //                                            //RizMetreUsers.UpdateWithFBIdAndShomareh();
                //                                        }
                //                                        else
                //                                        {
                //                                            strForItem = Dr[idr]["UseItemForAdd"].ToString().Trim();
                //                                            strUseItem = strFBShomareh.Trim();
                //                                            if (strForItem == strUseItem)
                //                                            {
                //                                                strUseItem = DtRizMetreUsers.Rows[0]["ForItem"].ToString().Trim();

                //                                                strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                //                                                decimal dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                //                                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();



                //                                                if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                                    RizMetreUsers.Tedad = ((Tedad == 0) ? 1 : Tedad) * dMultiple;
                //                                                else
                //                                                    RizMetreUsers.Tedad = 0;

                //                                                if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                                    RizMetreUsers.Tool = Tool;
                //                                                else
                //                                                    RizMetreUsers.Tool = 0;

                //                                                if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                                    RizMetreUsers.Arz = Arz;
                //                                                else
                //                                                    RizMetreUsers.Arz = 0;

                //                                                if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                                    RizMetreUsers.Ertefa = Ertefa;
                //                                                else
                //                                                    RizMetreUsers.Ertefa = 0;

                //                                                if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                                    RizMetreUsers.Vazn = Vazn;
                //                                                else
                //                                                    RizMetreUsers.Vazn = 0;

                //                                                RizMetreUsers.Shomareh = intShomareh;
                //                                                RizMetreUsers.Sharh = Sharh;
                //                                                //RizMetreUsers.Tedad = ((Tedad == 0) ? 1 : Tedad) * dMultiple;
                //                                                //RizMetreUsers.Tool = Tool;
                //                                                //RizMetreUsers.Arz = Arz;
                //                                                //RizMetreUsers.Ertefa = Ertefa;
                //                                                //RizMetreUsers.Vazn = Vazn;
                //                                                RizMetreUsers.ForItem = strForItem;
                //                                                RizMetreUsers.UseItem = strUseItem;
                //                                                Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                                RizMetreUsers.FBId = guFBId;
                //                                                RizMetreUsers.LevelNumber = LevelNumber;

                //                                                dMeghdarJoz = 0;
                //                                                if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                                    dMeghdarJoz = 0;
                //                                                else
                //                                                    dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                                    (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                //                                                RizMetreUsers.OperationsOfHamlId = 1;

                //                                                string strForItem1 = strFBShomareh.Trim().Substring(0, 6);
                //                                                clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                                && x.ForItem == strForItem1 && x.UseItem == strUseItem).FirstOrDefault();
                //                                                if (clsRizMetreUsers != null)
                //                                                {
                //                                                    try
                //                                                    {
                //                                                        var entry = context.Entry(clsRizMetreUsers);
                //                                                        var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                                        foreach (var property in entry.OriginalValues.Properties)
                //                                                        {
                //                                                            if (!excludedProperties.Contains(property.Name))
                //                                                            {
                //                                                                var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                                entry.CurrentValues[property] = newValue;
                //                                                            }
                //                                                        }

                //                                                        //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                                        context.SaveChanges();
                //                                                    }
                //                                                    catch (Exception)
                //                                                    {
                //                                                    }
                //                                                }
                //                                                //RizMetreUsers.UpdateWithFBIdAndShomareh();
                //                                            }
                //                                            else
                //                                            {
                //                                                strFinalWorking = strFinalWorking.Replace("x", Meghdar.ToString().Trim());
                //                                                decimal dMultiple = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                //                                                if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
                //                                                {
                //                                                    string strUseItemForAdd = Dr[idr]["UseItemForAdd"].ToString().Trim();
                //                                                    var varItemsForGetValues = context.ItemsForGetValuess.Where(x => x.ItemShomareh == strUseItemForAdd && x.Year == Year).ToList();
                //                                                    DataTable DtItemsForGetValues = clsConvert.ToDataTable(varItemsForGetValues);
                //                                                    //DataTable DtItemsForGetValues = clsOperationItemsFB.ItemsForGetValuesListWithParameter("ItemShomareh='" + Dr[idr]["UseItemForAdd"].ToString().Trim() + "' and Year=1397");
                //                                                    if (DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString().Trim() != "")
                //                                                        if (DtItemsForGetValues.Rows.Count != 0)
                //                                                        {
                //                                                            if (strFBShomareh.Trim() != "")
                //                                                            {
                //                                                                DataRow[] DrItemsForGetValues = DtItemsForGetValues.Select("ItemShomarehForGetValue='" + strFBShomareh.Trim() + "'");

                //                                                                clsItemsFBShomarehValueShomareh ItemsFBShomarehValueShomareh = new clsItemsFBShomarehValueShomareh();

                //                                                                string[] strRizMetreFieldsRequire = DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString().Split(',');
                //                                                                List<string> lst = new List<string>();
                //                                                                for (int j = 0; j < strRizMetreFieldsRequire.Length; j++)
                //                                                                {
                //                                                                    lst.Add(strRizMetreFieldsRequire[j]);
                //                                                                }

                //                                                                decimal dTedad = 0;
                //                                                                decimal dTool = 0;
                //                                                                decimal dArz = 0;
                //                                                                decimal dErtefa = 0;
                //                                                                decimal dVazn = 0;
                //                                                                var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                //                                                                if (strCal.Count != 0)
                //                                                                {
                //                                                                    string[] s = strCal[0].ToString().Split('+');
                //                                                                    if (s.Length > 1)
                //                                                                        dTedad = ((Tedad != 0 ? 1 : Tedad) * dMultiple) * decimal.Parse(s[1]) + Tedad;
                //                                                                    else
                //                                                                        dTedad = Tedad * dMultiple;
                //                                                                }
                //                                                                strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                //                                                                if (strCal.Count != 0)
                //                                                                {
                //                                                                    string[] s = strCal[0].ToString().Split('+');
                //                                                                    if (s.Length > 1)
                //                                                                        dTool = (Tool != 0 ? 1 : Tool) * decimal.Parse(s[1]) + Tool;
                //                                                                    else

                //                                                                        dTool = Tool;
                //                                                                }
                //                                                                strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                //                                                                if (strCal.Count != 0)
                //                                                                {
                //                                                                    string[] s = strCal[0].ToString().Split('+');
                //                                                                    if (s.Length > 1)
                //                                                                        dArz = (Arz != 0 ? 1 : Arz) * decimal.Parse(s[1]) + Arz;
                //                                                                    else

                //                                                                        dArz = Arz;
                //                                                                }
                //                                                                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                //                                                                if (strCal.Count != 0)
                //                                                                {
                //                                                                    string[] s = strCal[0].ToString().Split('+');
                //                                                                    if (s.Length > 1)
                //                                                                        dErtefa = (Ertefa != 0 ? 1 : Ertefa) * decimal.Parse(s[1]) + Ertefa;
                //                                                                    else
                //                                                                        dErtefa = Ertefa;
                //                                                                }
                //                                                                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                //                                                                if (strCal.Count != 0)
                //                                                                {
                //                                                                    string[] s = strCal[0].ToString().Split('+');
                //                                                                    if (s.Length > 1)
                //                                                                        dVazn = (Vazn != 0 ? 1 : Vazn) * decimal.Parse(s[1]) + Vazn;
                //                                                                    else
                //                                                                        dVazn = Vazn;
                //                                                                }
                //                                                                /////////////////////
                //                                                                /////////////////////
                //                                                                if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                                                    RizMetre.Tedad = dTedad;
                //                                                                else
                //                                                                    RizMetre.Tedad = 0;

                //                                                                if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                                                    RizMetre.Tool = dTool;
                //                                                                else
                //                                                                    RizMetre.Tool = 0;

                //                                                                if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                                                    RizMetre.Arz = dArz;
                //                                                                else
                //                                                                    RizMetre.Arz = 0;

                //                                                                if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                                                    RizMetre.Ertefa = dErtefa;
                //                                                                else
                //                                                                    RizMetre.Ertefa = 0;

                //                                                                if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                                                    RizMetre.Vazn = dVazn;
                //                                                                else
                //                                                                    RizMetre.Vazn = 0;

                //                                                                RizMetre.Sharh = Sharh.Trim();
                //                                                                //RizMetre.Tedad = dTedad;
                //                                                                //RizMetre.Tool = dTool;
                //                                                                //RizMetre.Arz = dArz;
                //                                                                //RizMetre.Ertefa = dErtefa;
                //                                                                //RizMetre.Vazn = dVazn;

                //                                                                RizMetre.Shomareh = intShomareh;
                //                                                                RizMetre.ForItem = strForItem;
                //                                                                RizMetre.UseItem = strUseItem;
                //                                                                Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                                                RizMetre.FBId = guFBId;
                //                                                                RizMetre.LevelNumber = LevelNumber;
                //                                                                string strForItem1 = strFBShomareh.Trim().Substring(0, 6);


                //                                                                dMeghdarJoz = 0;
                //                                                                if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                                                    dMeghdarJoz = 0;
                //                                                                else
                //                                                                    dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                                                    (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                                                RizMetre.MeghdarJoz = dMeghdarJoz;


                //                                                                clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                                                && x.ForItem == strForItem1 && x.UseItem == strUseItem).FirstOrDefault();
                //                                                                if (clsRizMetreUsers != null)
                //                                                                {
                //                                                                    try
                //                                                                    {
                //                                                                        context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetre);
                //                                                                        context.SaveChanges();
                //                                                                    }
                //                                                                    catch (Exception)
                //                                                                    {
                //                                                                    }
                //                                                                }
                //                                                                //RizMetre.UpdateWithFBIdAndShomareh();
                //                                                            }
                //                                                        }
                //                                                }
                //                                            }
                //                                        }
                //                                    }
                //                                }
                //                            }
                //                            break;
                //                        }
                //                    case "5":
                //                        {
                //                            var varFBUser1 = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                //                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUser1);
                //                            //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                //                            if (DtFBUsersAdded.Rows.Count != 0)
                //                            {
                //                                clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();

                //                                if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tedad = Tedad;
                //                                else
                //                                    RizMetreUsers.Tedad = 0;

                //                                if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Tool = Tool;
                //                                else
                //                                    RizMetreUsers.Tool = 0;

                //                                if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Arz = Arz;
                //                                else
                //                                    RizMetreUsers.Arz = 0;

                //                                if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Ertefa = Ertefa;
                //                                else
                //                                    RizMetreUsers.Ertefa = 0;

                //                                if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                    RizMetreUsers.Vazn = Vazn;
                //                                else
                //                                    RizMetreUsers.Vazn = 0;

                //                                RizMetreUsers.Shomareh = intShomareh;
                //                                RizMetreUsers.Sharh = Sharh;
                //                                //RizMetreUsers.Tedad = Tedad;
                //                                //RizMetreUsers.Tool = Tool;
                //                                //RizMetreUsers.Arz = Arz;
                //                                //RizMetreUsers.Ertefa = Ertefa;
                //                                //RizMetreUsers.Vazn = Vazn;
                //                                RizMetreUsers.ForItem = strFBShomareh.Trim();
                //                                RizMetreUsers.UseItem = "";
                //                                RizMetreUsers.LevelNumber = LevelNumber;
                //                                Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                RizMetreUsers.FBId = guFBId;

                //                                dMeghdarJoz = 0;
                //                                if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                    dMeghdarJoz = 0;
                //                                else
                //                                    dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                    (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                //                                RizMetreUsers.OperationsOfHamlId = 1;


                //                                string strForItem = strFBShomareh.Trim().Substring(0, 6);
                //                                string strUseItem = "";
                //                                clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                && x.ForItem == strForItem && x.UseItem == strUseItem).FirstOrDefault();
                //                                if (clsRizMetreUsers != null)
                //                                {
                //                                    try
                //                                    {
                //                                        var entry = context.Entry(clsRizMetreUsers);
                //                                        var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                        foreach (var property in entry.OriginalValues.Properties)
                //                                        {
                //                                            if (!excludedProperties.Contains(property.Name))
                //                                            {
                //                                                var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                entry.CurrentValues[property] = newValue;
                //                                            }
                //                                        }
                //                                        //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                        context.SaveChanges();
                //                                    }
                //                                    catch (Exception)
                //                                    {
                //                                    }
                //                                }
                //                                //RizMetreUsers.UpdateWithFBIdAndShomareh();
                //                            }
                //                            break;
                //                        }
                //                    case "6":
                //                        {
                //                            var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                //                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);
                //                            //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                //                            if (DtFBUsersAdded.Rows.Count != 0)
                //                            {
                //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
                //                                string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                //                                string strConditionOp = strCondition.Replace("x", Ertefa.ToString().Trim());
                //                                StringToFormula StringToFormula = new StringToFormula();
                //                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                //                                if (blnCheck)
                //                                {
                //                                    strFinalWorking = strFinalWorking.Replace("x", Ertefa.ToString().Trim());
                //                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                //                                    RizMetreUsers.Shomareh = intShomareh;
                //                                    RizMetreUsers.Sharh = Sharh;


                //                                    if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Tedad = ((Tedad == 0) ? 1 : Tedad);
                //                                    else
                //                                        RizMetreUsers.Tedad = 0;

                //                                    if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Tool = Tool;
                //                                    else
                //                                        RizMetreUsers.Tool = 0;

                //                                    if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Arz = Arz;
                //                                    else
                //                                        RizMetreUsers.Arz = 0;

                //                                    if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Ertefa = Ertefa;
                //                                    else
                //                                        RizMetreUsers.Ertefa = 0;

                //                                    if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Vazn = Vazn;
                //                                    else
                //                                        RizMetreUsers.Vazn = 0;
                //                                    RizMetreUsers.ForItem = strFBShomareh.Trim();
                //                                    RizMetreUsers.UseItem = "";
                //                                    Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                    RizMetreUsers.FBId = guFBId;

                //                                    dMeghdarJoz = 0;
                //                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                        dMeghdarJoz = 0;
                //                                    else
                //                                        dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                        (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                    RizMetreUsers.MeghdarJoz = dMeghdarJoz;
                //                                    RizMetreUsers.OperationsOfHamlId = 1;


                //                                    string strForItem = strFBShomareh.Trim().Substring(0, 6);
                //                                    string strUseItem = "";
                //                                    clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                    && x.ForItem == strForItem && x.UseItem == strUseItem).FirstOrDefault();
                //                                    bool blnCheckUpdate = false;
                //                                    if (clsRizMetreUsers != null)
                //                                    {
                //                                        try
                //                                        {
                //                                            var entry = context.Entry(clsRizMetreUsers);
                //                                            var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                            foreach (var property in entry.OriginalValues.Properties)
                //                                            {
                //                                                if (!excludedProperties.Contains(property.Name))
                //                                                {
                //                                                    var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                    entry.CurrentValues[property] = newValue;
                //                                                }
                //                                            }
                //                                            //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                            context.SaveChanges();
                //                                            blnCheckUpdate = true;
                //                                        }
                //                                        catch (Exception)
                //                                        {
                //                                            blnCheckUpdate = false;
                //                                        }
                //                                    }
                //                                    //if (!RizMetreUsers.UpdateWithFBIdAndShomareh())
                //                                    if (!blnCheckUpdate)
                //                                    {
                //                                        RizMetre.ID = Guid.NewGuid();
                //                                        RizMetre.Sharh = Sharh.Trim();
                //                                        RizMetre.Tedad = Tedad;
                //                                        RizMetre.Tool = Tool;
                //                                        RizMetre.Arz = Arz;
                //                                        RizMetre.Ertefa = Ertefa;
                //                                        RizMetre.Vazn = Vazn;
                //                                        RizMetre.FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                        RizMetre.Shomareh = intShomareh;
                //                                        RizMetre.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + strFBShomareh.Trim();
                //                                        RizMetre.Type = "2";
                //                                        RizMetre.ForItem = strFBShomareh.Trim();
                //                                        RizMetre.UseItem = "";
                //                                        RizMetre.LevelNumber = LevelNumber;


                //                                        ///محاسبه مقدار جزء
                //                                        dMeghdarJoz = 0;
                //                                        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                            dMeghdarJoz = 0;
                //                                        else
                //                                            dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                            (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
                //                                        RizMetre.MeghdarJoz = dMeghdarJoz;


                //                                        context.RizMetreUserses.Add(RizMetre);
                //                                        context.SaveChanges();
                //                                        //RizMetre.Save();
                //                                    }
                //                                }
                //                                else
                //                                {
                //                                    Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.ForItem == strFBShomareh.Trim()
                //                                                                                        && x.Shomareh == intShomareh).ToList();

                //                                    if (clsRizMetreUsers != null)
                //                                    {
                //                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                //                                        context.SaveChanges();
                //                                    }
                //                                    //clsRizMetreUserss.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "' and clsRizMetreUserss.Shomareh=" + intShomareh);
                //                                }
                //                            }
                //                            break;
                //                        }
                //                    case "8":
                //                        {
                //                            var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                //                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                //                            //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + strFBShomarehAdded + "'");
                //                            if (DtFBUsersAdded.Rows.Count != 0)
                //                            {
                //                                string strCondition = Dr[idr]["Condition"].ToString().Trim();
                //                                string strFinalWorking = Dr[idr]["FinalWorking"].ToString();
                //                                string strConditionOp = strCondition.Replace("x", Arz.ToString().Trim());
                //                                StringToFormula StringToFormula = new StringToFormula();
                //                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                //                                if (blnCheck)
                //                                {
                //                                    strFinalWorking = strFinalWorking.Replace("x", Arz.ToString().Trim());
                //                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                //                                    RizMetreUsers.Shomareh = intShomareh;
                //                                    RizMetreUsers.Sharh = Sharh;

                //                                    decimal ArzEzafi = 1;
                //                                    if (strFinalWorking != "")
                //                                    {
                //                                        ArzEzafi = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());
                //                                    }

                //                                    if (DtItemsFields.Rows[0]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Tedad = ((Tedad == 0) ? 1 : Tedad) * ArzEzafi;
                //                                    else
                //                                        RizMetreUsers.Tedad = 0;

                //                                    if (DtItemsFields.Rows[1]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Tool = Tool;
                //                                    else
                //                                        RizMetreUsers.Tool = 0;

                //                                    if (DtItemsFields.Rows[2]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Arz = Arz;
                //                                    else
                //                                        RizMetreUsers.Arz = 0;

                //                                    if (DtItemsFields.Rows[3]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Ertefa = Ertefa;
                //                                    else
                //                                        RizMetreUsers.Ertefa = 0;

                //                                    if (DtItemsFields.Rows[4]["IsEnteringValue"].ToString() == "True")
                //                                        RizMetreUsers.Vazn = Vazn;
                //                                    else
                //                                        RizMetreUsers.Vazn = 0;
                //                                    RizMetreUsers.ForItem = strFBShomareh.Trim();
                //                                    RizMetreUsers.UseItem = "";
                //                                    Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                    RizMetreUsers.FBId = guFBId;

                //                                    dMeghdarJoz = 0;
                //                                    if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                        dMeghdarJoz = 0;
                //                                    else
                //                                        dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                        (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                                    RizMetre.MeghdarJoz = dMeghdarJoz;
                //                                    RizMetreUsers.OperationsOfHamlId = 1;

                //                                    bool blnCheckUpdate = false;
                //                                    string strForItem = strFBShomareh.Trim().Substring(0, 6);
                //                                    string strUseItem = "";
                //                                    clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                                    && x.ForItem == strForItem && x.UseItem == strUseItem).FirstOrDefault();
                //                                    if (clsRizMetreUsers != null)
                //                                    {
                //                                        try
                //                                        {
                //                                            var entry = context.Entry(clsRizMetreUsers);
                //                                            var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                                            foreach (var property in entry.OriginalValues.Properties)
                //                                            {
                //                                                if (!excludedProperties.Contains(property.Name))
                //                                                {
                //                                                    var newValue = RizMetreUsers.GetType().GetProperty(property.Name)?.GetValue(RizMetreUsers);
                //                                                    entry.CurrentValues[property] = newValue;
                //                                                }
                //                                            }
                //                                            //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetreUsers);
                //                                            context.SaveChanges();
                //                                            blnCheckUpdate = true;
                //                                        }
                //                                        catch (Exception)
                //                                        {
                //                                            blnCheckUpdate = false;
                //                                        }
                //                                    }
                //                                    //if (!RizMetreUsers.UpdateWithFBIdAndShomareh())
                //                                    if (!blnCheckUpdate)
                //                                    {
                //                                        RizMetre.ID = Guid.NewGuid();
                //                                        RizMetre.Sharh = Sharh.Trim();
                //                                        RizMetre.Tedad = Tedad;
                //                                        RizMetre.Tool = Tool;
                //                                        RizMetre.Arz = Arz;
                //                                        RizMetre.Ertefa = Ertefa;
                //                                        RizMetre.Vazn = Vazn;
                //                                        RizMetre.FBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                        RizMetre.Shomareh = intShomareh;
                //                                        RizMetre.Des = Dr[idr]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + strFBShomareh.Trim();
                //                                        RizMetre.Type = "2";
                //                                        RizMetre.ForItem = strFBShomareh.Trim();
                //                                        RizMetre.UseItem = "";
                //                                        RizMetre.LevelNumber = LevelNumber;


                //                                        ///محاسبه مقدار جزء
                //                                        dMeghdarJoz = 0;
                //                                        if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                                            dMeghdarJoz = 0;
                //                                        else
                //                                            dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                                            (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);
                //                                        RizMetre.MeghdarJoz = dMeghdarJoz;


                //                                        context.RizMetreUserses.Add(RizMetre);
                //                                        context.SaveChanges();
                //                                        //RizMetre.Save();
                //                                    }
                //                                }
                //                                else
                //                                {
                //                                    //clsRizMetreUserss.Delete("FBId=" + DtFBUsersAdded.Rows[0]["Id"].ToString().Trim() + " and ForItem='" + strFBShomareh.Trim() + "' and clsRizMetreUserss.Shomareh=" + intShomareh);
                //                                    Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                                    strFBShomareh = strFBShomareh.Trim();
                //                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guFBId && x.ForItem == strFBShomareh.Trim()
                //                                                                                        && x.Shomareh == intShomareh);

                //                                    if (clsRizMetreUsers != null)
                //                                    {
                //                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                //                                        context.SaveChanges();
                //                                    }
                //                                }
                //                            }
                //                            break;
                //                        }
                //                    default:
                //                        break;
                //                }
                //            }
                //        }
                //    }
                //}

                //if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
                //{
                //    string strGetValuesShomareh = DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim();
                //    var varFB1 = context.FBs.Where(x => x.BarAvordId == intBarAvordId && x.Shomareh == strGetValuesShomareh).ToList();
                //    DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFB1);

                //    //DataTable DtFBUsersAdded = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + intBarAvordId + " and Shomareh='" + DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim() + "'");
                //    string strUsersAdded = DtFBUsersAdded.Rows[0]["Shomareh"].ToString().Trim();
                //    var varItemsForGetValues = context.ItemsForGetValuess.Where(x => x.ItemShomareh == strUsersAdded && x.Year == Year).ToList();
                //    DataTable DtItemsForGetValues = clsConvert.ToDataTable(varItemsForGetValues);
                //    //DataTable DtItemsForGetValues = clsOperationItemsFB.ItemsForGetValuesListWithParameter("ItemShomareh='" + DtFBUsersAdded.Rows[0]["Shomareh"].ToString().Trim() + "' and Year=1397");// and ItemShomarehForGetValue='" + ItemsFBShomareh + "'");

                //    if (DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString().Trim() != "")
                //        if (DtItemsForGetValues.Rows.Count != 0)
                //        {
                //            if (strFBShomareh.Trim() != "")
                //            {
                //                DataRow[] DrItemsForGetValues = DtItemsForGetValues.Select("ItemShomarehForGetValue='" + strFBShomareh.Trim() + "'");

                //                clsItemsFBShomarehValueShomareh ItemsFBShomarehValueShomareh = new clsItemsFBShomarehValueShomareh();

                //                string[] strRizMetreFieldsRequire = DtItemsForGetValues.Rows[0]["RizMetreFieldsRequire"].ToString().Split(',');
                //                List<string> lst = new List<string>();
                //                for (int j = 0; j < strRizMetreFieldsRequire.Length; j++)
                //                {
                //                    lst.Add(strRizMetreFieldsRequire[j]);
                //                }

                //                decimal dTedad = 0;
                //                decimal dTool = 0;
                //                decimal dArz = 0;
                //                decimal dErtefa = 0;
                //                decimal dVazn = 0;
                //                var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                //                if (strCal.Count != 0)
                //                {
                //                    string[] s = strCal[0].ToString().Split('+');
                //                    if (s.Length > 1)
                //                        dTedad = (Tedad != 0 ? 1 : Tedad) * decimal.Parse(s[1]) + Tedad;
                //                    else
                //                        dTedad = Tedad;
                //                }
                //                strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                //                if (strCal.Count != 0)
                //                {
                //                    string[] s = strCal[0].ToString().Split('+');
                //                    if (s.Length > 1)
                //                        dTool = (Tool != 0 ? 1 : Tool) * decimal.Parse(s[1]) + Tool;
                //                    else

                //                        dTool = Tool;
                //                }
                //                strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                //                if (strCal.Count != 0)
                //                {
                //                    string[] s = strCal[0].ToString().Split('+');
                //                    if (s.Length > 1)
                //                        dArz = (Arz != 0 ? 1 : Arz) * decimal.Parse(s[1]) + Arz;
                //                    else

                //                        dArz = Arz;
                //                }
                //                strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                //                if (strCal.Count != 0)
                //                {
                //                    string[] s = strCal[0].ToString().Split('+');
                //                    if (s.Length > 1)
                //                        dErtefa = (Ertefa != 0 ? 1 : Ertefa) * decimal.Parse(s[1]) + Ertefa;
                //                    else
                //                        dErtefa = Ertefa;
                //                }
                //                strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                //                if (strCal.Count != 0)
                //                {
                //                    string[] s = strCal[0].ToString().Split('+');
                //                    if (s.Length > 1)
                //                        dVazn = (Vazn != 0 ? 1 : Vazn) * decimal.Parse(s[1]) + Vazn;
                //                    else
                //                        dVazn = Vazn;
                //                }

                //                Guid guFBId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                //                RizMetre.Sharh = Sharh.Trim();
                //                RizMetre.Tedad = dTedad;
                //                RizMetre.Tool = dTool;
                //                RizMetre.Arz = dArz;
                //                RizMetre.Ertefa = dErtefa;
                //                RizMetre.Vazn = dVazn;
                //                RizMetre.FBId = guFBId;
                //                RizMetre.Shomareh = intShomareh;
                //                RizMetre.ForItem = strFBShomareh.Trim();
                //                RizMetre.UseItem = "";

                //                dMeghdarJoz = 0;
                //                if (Tedad == 0 && Tool == 0 && Arz == 0 && Ertefa == 0 && Vazn == 0)
                //                    dMeghdarJoz = 0;
                //                else
                //                    dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Tool == 0 ? 1 : Tool) *
                //                    (Arz == 0 ? 1 : Arz) * (Ertefa == 0 ? 1 : Ertefa) * (Vazn == 0 ? 1 : Vazn);

                //                RizMetre.MeghdarJoz = dMeghdarJoz;

                //                string strForItem = strFBShomareh.Trim().Substring(0, 6);
                //                string strUseItem = "";
                //                clsRizMetreUsers? clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == guFBId && x.Shomareh == intShomareh
                //                && x.ForItem == strForItem && x.UseItem == strUseItem).FirstOrDefault();
                //                if (clsRizMetreUsers != null)
                //                {
                //                    var entry = context.Entry(clsRizMetreUsers);
                //                    var excludedProperties = new[] { "ID", "CreatedDate", "Type" };

                //                    foreach (var property in entry.OriginalValues.Properties)
                //                    {
                //                        if (!excludedProperties.Contains(property.Name))
                //                        {
                //                            var newValue = RizMetre.GetType().GetProperty(property.Name)?.GetValue(RizMetre);
                //                            entry.CurrentValues[property] = newValue;
                //                        }
                //                    }
                //                    //context.Entry(clsRizMetreUsers).CurrentValues.SetValues(RizMetre);
                //                    context.SaveChanges();
                //                }
                //            }
                //        }
                //}
            }
            return new JsonResult("OK_");
        }
        catch (Exception)
        {
            return new JsonResult("NOK_");
        }
    }

    [HttpPost]
    public JsonResult UpdateRizMetreUsersAddedItems([FromBody] UpdateRizMetreUsersInputDto request)
    {
        try
        {

            Guid Id = request.Id;

            clsRizMetreUsers? clsRizMetreUsers1 = context.RizMetreUserses.Where(x => x.ID == Id).FirstOrDefault();


            string Sharh = request.Sharh;
            decimal? Tedad = request.Tedad;
            decimal? Tool = request.Tool;
            decimal? Arz = request.Arz;
            decimal? Ertefa = request.Ertefa;
            decimal? Vazn = request.Vazn;
            string? Des = request.Des;
            NoeFehrestBaha NoeFB = request.NoeFB;
            int Year = request.Year;
            int LevelNumber = request.LevelNumber == 0 ? 1 : request.LevelNumber;
            DastyarCommon DastyarCommon = new DastyarCommon(context);

            DataTable DtRizMetreUsers = new DataTable();
            if (request.LevelNumber == 0)
            {
                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                            BarAvordId = FB.BarAvordId
                                        }).Where(x => x.ID == Id).OrderBy(x => x.Shomareh).ToList();
                DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
            }
            else
            {
                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                            BarAvordId = FB.BarAvordId
                                        }).Where(x => x.ID == Id).OrderBy(x => x.Shomareh).ToList();
                DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
            }


            long intShomareh = long.Parse(DtRizMetreUsers.Rows[0]["Shomareh"].ToString().Trim());
            string Type = DtRizMetreUsers.Rows[0]["Type"].ToString().Trim();
            Guid intFBId = Guid.Parse(DtRizMetreUsers.Rows[0]["FBId"].ToString().Trim());
            var varFBUser = context.FBs.Where(x => x.ID == intFBId).ToList();
            DataTable DtFBUsers = clsConvert.ToDataTable(varFBUser);

            Guid intBarAvordId = Guid.Parse(DtFBUsers.Rows[0]["BarAvordId"].ToString().Trim());
            string strFBShomareh = DtFBUsers.Rows[0]["Shomareh"].ToString().Trim();
            string strForItemCurrent = DtRizMetreUsers.Rows[0]["ForItem"].ToString().Trim();



            //var varItemsFields = (from ItemF in context.ItemsFieldses
            //                      join OpItemFB in context.Operation_ItemsFBs
            //                      on ItemF.ItemShomareh equals OpItemFB.ItemsFBShomareh
            //                      select new
            //                      {
            //                          ItemShomareh = ItemF.ItemShomareh,
            //                          NoeFB = ItemF.NoeFB,
            //                          IsEnteringValue = ItemF.IsEnteringValue,
            //                          Vahed = ItemF.Vahed,
            //                          FieldType = ItemF.FieldType,
            //                          OperationId = OpItemFB.OperationId
            //                      }).Where(x => x.ItemShomareh == strFBShomareh && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();
            //DataTable DtItemsFields = clsConvert.ToDataTable(varItemsFields);
            //DataTable DtItemsFields = clsItemsFields.ItemsFieldsListWithParameter("ItemShomareh='" + strFBShomareh + "' and NoeFB=234");

            List<clsItemsFields> lstItemsFields = context.ItemsFieldses.Where(x => x.ItemShomareh == strFBShomareh && x.NoeFB == NoeFB).ToList();

            clsRizMetreUsers RizMetre = new clsRizMetreUsers();
            if (lstItemsFields[0].IsEnteringValue == true)
                RizMetre.Tedad = Tedad;
            else
                RizMetre.Tedad = null;

            if (lstItemsFields[1].IsEnteringValue == true)
                RizMetre.Tool = Tool;
            else
                RizMetre.Tool = null;

            if (lstItemsFields[2].IsEnteringValue == true)
                RizMetre.Arz = Arz;
            else
                RizMetre.Arz = null;

            if (lstItemsFields[3].IsEnteringValue == true)
                RizMetre.Ertefa = Ertefa;
            else
                RizMetre.Ertefa = null;

            if (lstItemsFields[4].IsEnteringValue == true)
                RizMetre.Vazn = Vazn;
            else
                RizMetre.Vazn = null;

            RizMetre.ID = Id;
            RizMetre.Sharh = Sharh.Trim();
            //RizMetre.Tedad = Tedad;
            //RizMetre.Tool = Tool;
            //RizMetre.Arz = Arz;
            //RizMetre.Ertefa = Ertefa;
            //RizMetre.Vazn = Vazn;
            RizMetre.Des = Des.Trim();
            RizMetre.LevelNumber = LevelNumber;
            RizMetre.Type = Type;
            RizMetre.ForItem = strForItemCurrent.Trim();


            decimal dMeghdarJoz = 0;
            if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                dMeghdarJoz = 0;
            else
                dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
                (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);

            RizMetre.MeghdarJoz = dMeghdarJoz;




            if (clsRizMetreUsers1 != null)
            {
                RizMetre.FBId = clsRizMetreUsers1.FBId;
                RizMetre.Shomareh = clsRizMetreUsers1.Shomareh;
                RizMetre.ShomarehNew = clsRizMetreUsers1.ShomarehNew;
                RizMetre.OperationsOfHamlId = 1;
                try
                {
                    context.Entry(clsRizMetreUsers1).CurrentValues.SetValues(RizMetre);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return new JsonResult("OK_");
        }
        catch (Exception)
        {
            return new JsonResult("NOK_");
        }
    }


    public JsonResult UpdateRizMetreN([FromBody] UpdateRizMetreUsersInputDto request)
    {
        Guid Id = request.Id;
        string Sharh = request.Sharh;
        decimal? Tedad = request.Tedad;
        decimal? Tool = request.Tool;
        decimal? Arz = request.Arz;
        decimal? Ertefa = request.Ertefa;
        decimal? Vazn = request.Vazn;
        string Des = request.Des;
        NoeFehrestBaha NoeFB = request.NoeFB;
        int Year = request.Year;
        int LevelNumber = request.LevelNumber == 0 ? 1 : request.LevelNumber;

        clsRizMetreUsers? entity = context.RizMetreUserses.FirstOrDefault(x => x.ID == Id);
        if (entity == null)
            return new JsonResult("NOK");

        entity.Sharh = Sharh;
        entity.Tedad = Tedad;
        entity.Tool = Tool;
        entity.Arz = Arz;
        entity.Ertefa = Ertefa;
        entity.Vazn = Vazn;
        entity.Des = Des;
        entity.LevelNumber = entity.LevelNumber;

        decimal dMeghdarJoz = 0;
        if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
            dMeghdarJoz = 0;
        else
            dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
        entity.MeghdarJoz = dMeghdarJoz;

        context.SaveChanges();
        return new JsonResult("OK_");
    }

    [HttpPost]
    public JsonResult GetRizMetreNRMU([FromBody] GetRizMetreNUserInputDto request)
    {
        Guid FBId = request.FBId;
        string IsFromAddedOperation = request.IsFromAddedOperation;
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        int Year = request.Year;
        long OperationId = request.OperationId;
        string ForItem = request.ForItem;
        int LevelNumber = request.LevelNumber == 0 ? 1 : request.LevelNumber;

        var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ForItem).OrderBy(x => x.Shomareh).ToList();

        return new JsonResult(varRizMetreUsers);
    }

    [HttpPost]
    public JsonResult GetRizMetreUsers([FromBody] GetRizMetreUserInputDto request)
    {
        Guid FBId = request.FBId;
        string IsFromAddedOperation = request.IsFromAddedOperation;
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        int Year = request.Year;
        long OperationId = request.OperationId;
        string ForItem = request.ForItem;
        int LevelNumber = request.LevelNumber == 0 ? 1 : request.LevelNumber;

        DastyarCommon DastyarCommon = new DastyarCommon(context);

        var varDt = context.FBs.Where(x => x.ID == FBId).ToList();
        DataTable Dt = clsConvert.ToDataTable(varDt);
        //DataTable Dt = clsOperationItemsFB.FBListWithParameter("Id=" + FBId);
        DataTable DtRizMetreUsers = new DataTable();
        var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && (ForItem == "" ? (x.ForItem == null || x.ForItem == "") : x.ForItem == ForItem) && x.LevelNumber == LevelNumber).OrderBy(x => x.Shomareh).ToList();
        DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
        //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId);
        string str = "";
        string strShomareh = Dt.Rows[0]["Shomareh"].ToString().Trim();
        var varItemsHasCondition = (from tblItemsHasCondition in context.ItemsHasConditions
                                    join tblItemsHasConditionConditionContext in context.ItemsHasCondition_ConditionContexts
                                    on tblItemsHasCondition.Id equals tblItemsHasConditionConditionContext.ItemsHasConditionId
                                    join tblConditionContext in context.ConditionContexts on
                                    tblItemsHasConditionConditionContext.ConditionContextId equals tblConditionContext.Id
                                    join tblConditionGroup in context.ConditionGroups on
                                    tblConditionContext.ConditionGroupId equals tblConditionGroup.Id
                                    where tblItemsHasCondition.Year == Year
                                    select new
                                    {
                                        tblItemsHasConditionConditionContext.Id,
                                        ItemsHasConditionId = tblItemsHasCondition.Id,
                                        ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                        tblItemsHasConditionConditionContext.HasEnteringValue,
                                        tblConditionContext.Context,
                                        tblConditionContext.TitleDes,
                                        tblItemsHasConditionConditionContext.Des,
                                        tblConditionGroup.ConditionGroupName,
                                        ConditionGroupId = tblConditionGroup.Id,
                                        tblItemsHasConditionConditionContext.DefaultValue,
                                        tblItemsHasConditionConditionContext.StepChange,
                                        tblItemsHasConditionConditionContext.IsShow,
                                        tblItemsHasConditionConditionContext.ParentId,
                                        tblItemsHasConditionConditionContext.MoveToRel,
                                        tblItemsHasConditionConditionContext.ViewCheckAllRecords
                                    }).Where(x => x.ItemFBShomareh == strShomareh).ToList();
        DataTable DtItemsHasCondition = clsConvert.ToDataTable(varItemsHasCondition);
        //DataTable DtItemsHasCondition = clsOperationItemsFB.ItemsHasConditionListWithParameter("ItemFBShomareh=" + Dt.Rows[0]["Shomareh"].ToString().Trim());

        var varItemsHasConditionAddedToFB = (from ItemsHasConditionAddedToFB in context.ItemsHasConditionAddedToFBs
                                             join ItemsAddingToFB in context.ItemsAddingToFBs on
                                             ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId
                                             equals ItemsAddingToFB.ItemsHasCondition_ConditionContextId
                                             select new
                                             {
                                                 ID = ItemsHasConditionAddedToFB.ID,
                                                 FBShomareh = ItemsHasConditionAddedToFB.FBShomareh,
                                                 BarAvordId = ItemsHasConditionAddedToFB.BarAvordId,
                                                 Meghdar = ItemsHasConditionAddedToFB.Meghdar,
                                                 ItemsHasCondition_ConditionContextId = ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                 AddedItems = ItemsAddingToFB.AddedItems
                                             }).Where(x => x.FBShomareh == strShomareh && x.BarAvordId == BarAvordId).ToList();
        DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);
        //DataTable DtItemsHasConditionAddedToFB = clsOperationItemsFB.ItemsHasConditionAddedToFB("FBShomareh='" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "' and BarAvordId=" + BarAvordId);
        ///inja

        var varOpItems = context.Operation_ItemsFBs.Where(x => x.ItemsFBShomareh == strShomareh && x.Year == Year && x.OperationId == OperationId).ToList();
        DataTable DtOpItems = clsConvert.ToDataTable(varOpItems);
        //DataTable DtOpItems = clsOperationItemsFB.ListWithParameter("ItemsFBShomareh='" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "'");
        var varItemsForGetValuess = context.ItemsForGetValuess.Where(x => x.ItemShomarehForGetValue == strShomareh && x.Year == Year).ToList();
        DataTable DtItemsForGetValuess = clsConvert.ToDataTable(varItemsForGetValuess);
        //DataTable DtItemsForGetValuess = clsOperationItemsFB.ItemsForGetValuesListWithParameter("ItemShomarehForGetValue='" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "' and Year=1397");
        string strItemsFBShomareh = DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim();
        var varItemsFields = (
                            from ItemsFields in context.ItemsFieldses
                            join OperationItemsFB in context.Operation_ItemsFBs on
                            ItemsFields.ItemShomareh equals OperationItemsFB.ItemsFBShomareh
                            select new
                            {
                                ID = ItemsFields.Id,
                                ItemShomareh = ItemsFields.ItemShomareh,
                                FieldType = ItemsFields.FieldType,
                                Vahed = ItemsFields.Vahed,
                                IsEnteringValue = ItemsFields.IsEnteringValue,
                                DefaultValue = ItemsFields.DefaultValue,
                                NoeFB = ItemsFields.NoeFB,
                                OperationId = OperationItemsFB.OperationId
                            }).Where(x => x.ItemShomareh == strItemsFBShomareh && x.NoeFB == NoeFB && x.OperationId == OperationId).OrderBy(x => x.FieldType).ToList();
        DataTable DtItemsFields = clsConvert.ToDataTable(varItemsFields);
        //DataTable DtItemsFields = clsItemsFields.ItemsFieldsListWithParameter("ItemShomareh='" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "' and NoeFB=234");
        string lstItemsFields = "";
        string strItemsShowInRelItems = "";
        bool blnItemHasEzafeBaha = false;
        string strDes = "";
        for (int i = 0; i < DtItemsFields.Rows.Count; i++)
        {
            lstItemsFields += DtItemsFields.Rows[i]["IsEnteringValue"].ToString().Trim() + ",";
        }

        if (IsFromAddedOperation == "false")
            str += "<script type=\"text/javascript\">AddRizMetreUsers('" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "','" + lstItemsFields + "'," + LevelNumber + ")</script>";
        else
        {
            if (request.LevelNumber != 0)
                str += "<script type=\"text/javascript\">AddRizMetreUsersN('" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "','" + lstItemsFields + "'," + LevelNumber + ")</script>";

        }
        List<int> lst = new List<int>();

        if (DtRizMetreUsers.Rows.Count != 0)
        {
            bool blnIsEzafeBahaAddedItems = false;
            if (IsFromAddedOperation == "false")
            {
                str += "<div class=\"styleHeaderTable1\"><div class=\"row\"><div class=\"col-md-1\"><input id=\"btnAddedItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" style=\"border:0;\" type=\"button\" onclick=\"ShowForms('divAddedItems','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "'," + LevelNumber + ")\" value=\"اضافه بها\" class=\"spanStyleMitraSmall spanFrameNameStyle ActiveAddRelItemTab\"/></div>";
                long lngOperationId = long.Parse(DtOpItems.Rows[0]["OperationId"].ToString().Trim());
                var varOperationHasAddedOperations = (from OperationHasAddedOperations in context.OperationHasAddedOperationses
                                                      join OperationHasAddedOperationsType in context.OperationHasAddedOperationsTypes on
                                                      OperationHasAddedOperations.Type equals OperationHasAddedOperationsType.Id
                                                      select new
                                                      {
                                                          ID = OperationHasAddedOperations.Id,
                                                          OperationId = OperationHasAddedOperations.OperationId,
                                                          AddedOperationId = OperationHasAddedOperations.AddedOperationId,
                                                          Type = OperationHasAddedOperations.Type,
                                                          ButtonName = OperationHasAddedOperationsType.TypeName,
                                                          LatinName = OperationHasAddedOperationsType.LatinName
                                                      }).Where(x => x.OperationId == lngOperationId).ToList();
                DataTable DtOperationHasAddedOperations = clsConvert.ToDataTable(varOperationHasAddedOperations);
                //DataTable DtOperationHasAddedOperations = clsOperationHasAddedOperations.ListWithParameter("OperationId=" + DtOpItems.Rows[0]["OperationId"].ToString().Trim());
                str += "<div class=\"col-md-2\"><input id=\"btnRelItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" style=\"border:0;\" type=\"button\" onclick=\"ShowForms('divRelItems','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "'," + LevelNumber + ")\" value=\"آیتم های مرتبط\" class=\"spanStyleMitraSmall spanFrameNameStyle\"/></div></div>";

                if (DtItemsHasCondition.Rows.Count != 0)
                {
                    string strItemAdded = Dt.Rows[0]["Shomareh"].ToString().Trim();
                    //for (int i = 0; i < Dt.Rows.Count; i++)
                    //{
                    DataRow[] DrItemsHasCondition = DtItemsHasCondition.Select("ItemFBShomareh=" + Dt.Rows[0]["Shomareh"].ToString().Trim() + " and IsShow=1");
                    DataTable DtItemsHasConditionFiltered = new DataTable();
                    if (DrItemsHasCondition.Length != 0)
                    {
                        DtItemsHasConditionFiltered = DrItemsHasCondition.CopyToDataTable();
                    }

                    bool blnItemsHasConditionChecked = true;
                    for (int k = 0; k < DtItemsHasConditionFiltered.Rows.Count; k++)
                    {
                        long lngIsParent = long.Parse(DtItemsHasConditionFiltered.Rows[k]["ParentId"].ToString().Trim());
                        if (lngIsParent != 0)
                        {
                            var varItemsHasConditionAddedToFBWithParentId = (from ItemsHasConditionAddedToFB in context.ItemsHasConditionAddedToFBs
                                                                             join ItemsAddingToFB in context.ItemsAddingToFBs on
                                                                             ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId equals ItemsAddingToFB.ItemsHasCondition_ConditionContextId
                                                                             select new
                                                                             {
                                                                                 ID = ItemsHasConditionAddedToFB.ID,
                                                                                 FBShomareh = ItemsHasConditionAddedToFB.FBShomareh,
                                                                                 BarAvordId = ItemsHasConditionAddedToFB.BarAvordId,
                                                                                 Meghdar = ItemsHasConditionAddedToFB.Meghdar,
                                                                                 ItemsHasCondition_ConditionContextId = ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                                                 AddedItems = ItemsAddingToFB.AddedItems
                                                                             }).Where(x => x.ItemsHasCondition_ConditionContextId == lngIsParent).ToList();
                            DataTable DtItemsHasConditionAddedToFBWithParentId = clsConvert.ToDataTable(varItemsHasConditionAddedToFBWithParentId);
                            //DataTable DtItemsHasConditionAddedToFBWithParentId = clsItemsHasConditionAddedToFB.ListWithParameter("clsItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId=" + strIsParent);
                            if (DtItemsHasConditionAddedToFBWithParentId.Rows.Count == 0)
                            {
                                blnItemsHasConditionChecked = false;
                            }
                        }

                        if (blnItemsHasConditionChecked)
                        {
                            var IsContain = lst.Contains(int.Parse(DtItemsHasConditionFiltered.Rows[k]["ConditionGroupId"].ToString()));
                            if (IsContain != true)
                            {
                                lst.Add(int.Parse(DtItemsHasConditionFiltered.Rows[k]["ConditionGroupId"].ToString()));
                            }
                        }
                    }

                    str += "<div id=\"divAddedItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" class=\"col-md-12\" style=\"padding:25px 5px 2px 5px;margin-bottom:5px;margin-bottom:5px;border:1px solid #c4d4db;border-radius:4px !important;text-align:right;display:none\">";
                    for (int m = 0; m < lst.Count; m++)
                    {
                        DataRow[] DrItemsHasConditionWithItemFBShomarehFiltered = DtItemsHasConditionFiltered.Select("ConditionGroupId=" + lst[m]);

                        if (DrItemsHasConditionWithItemFBShomarehFiltered.Length != 0)
                        {
                            if (DrItemsHasConditionWithItemFBShomarehFiltered.Length > 1)
                            {
                                bool blnCheckIsSelectData = false;
                                for (int ii = 0; ii < DrItemsHasConditionWithItemFBShomarehFiltered.Length; ii++)
                                {
                                    DataRow[] Dr = DtItemsHasConditionAddedToFB.Select("ItemsHasCondition_ConditionContextId=" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString());
                                    if (Dr.Length != 0)
                                    {
                                        blnCheckIsSelectData = true;
                                    }
                                }

                                if (blnCheckIsSelectData)
                                    str += "<div class=\"col-md-4\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\"><input type=\"checkbox\" checked=\"checked\" onclick=\"OpenConditionDetails($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "')\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"OpenConditionDetailsOnly('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "<span></div>";
                                else
                                    str += "<div class=\"col-md-4\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\"><input type=\"checkbox\" onclick=\"OpenConditionDetails($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "')\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"OpenConditionDetailsOnly('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";

                                str += "<div class=\"col-md-12\" id=\"divConditionGroup" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\" style=\"display:None;background-color:#ffe7e7;border:1px solid #bfe0ed;margin-bottom:20px;border-radius:5px !important;\">";
                                for (int ii = 0; ii < DrItemsHasConditionWithItemFBShomarehFiltered.Length; ii++)
                                {
                                    string strType = "";
                                    if (DrItemsHasConditionWithItemFBShomarehFiltered.Length > 1)
                                    {
                                        strType = "radio";
                                    }
                                    else
                                    {
                                        strType = "checkbox";
                                    }
                                    DataRow[] Dr = DtItemsHasConditionAddedToFB.Select("ItemsHasCondition_ConditionContextId=" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString());

                                    if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["HasEnteringValue"].ToString() == "True")
                                    {
                                        string strMeghdar = "0";
                                        strDes = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["HasDes"].ToString().Trim() == "True" ? "<span style=\"margin-left:30px\">" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["TitleDes"].ToString().Trim() + "</span>" : "";
                                        if (Dr.Length != 0)
                                        {

                                            strMeghdar = decimal.Parse(Dr[0]["Meghdar"].ToString().Trim()).ToString("G29");
                                            str += "<div class=\"col-md-12\" style=\"padding-right:0px;margin:10px;\"><table><tr><td><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                                + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\" checked=\"checked\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span>" + strDes + "</td><td style=\"padding-right:10px;\"><input style=\"text-align:center;width:100px;\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim() + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar
                                                    + "\"/></td></tr></table></div>";
                                        }
                                        else
                                        {

                                            str += "<div class=\"col-md-12\" style=\"padding-right:0px;margin:10px;\"><table><tr><td><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                                + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\" /><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span>" + strDes + "</td><td style=\"padding-right:10px;\"><input style=\"text-align:center;width:100px;\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim() + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" class=\"form-control spanStyleMitraMedium\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim() + "\" value=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DefaultValue"].ToString().Trim()
                                                + "\"/></td></tr></table></div>";
                                        }
                                    }
                                    else
                                    {
                                        if (Dr.Length != 0)
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MoveToRel"].ToString() != "True")
                                            {
                                                if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                                {
                                                    Guid guNew = new Guid();
                                                    blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                    string strEzafeBahaIsChecked = "";
                                                    if (blnIsEzafeBahaAddedItems)
                                                    {
                                                        strEzafeBahaIsChecked = "checked=\"checked\"";
                                                    }
                                                    str += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                    blnItemHasEzafeBaha = true;
                                                }
                                                else
                                                {
                                                    str += "<div class=\"col-md-12\" style=\"padding-right:0px;margin:10px;\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" checked=\"checked\" name=\"group"
                                                        + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                         + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>";
                                                }
                                            }
                                            else
                                            {
                                                if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                                {
                                                    Guid guNew = new Guid();
                                                    blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                    string strEzafeBahaIsChecked = "";
                                                    if (blnIsEzafeBahaAddedItems)
                                                    {
                                                        strEzafeBahaIsChecked = "checked=\"checked\"";
                                                    }
                                                    strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                    blnItemHasEzafeBaha = true;
                                                }
                                                else
                                                {
                                                    strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"padding-right:0px;margin:10px;text-align: right;\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" checked=\"checked\" name=\"group"
                                                        + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                        + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MoveToRel"].ToString() != "True")
                                            {
                                                if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                                {
                                                    Guid guNew = new Guid();
                                                    blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                    string strEzafeBahaIsChecked = "";
                                                    if (blnIsEzafeBahaAddedItems)
                                                    {
                                                        strEzafeBahaIsChecked = "checked=\"checked\"";
                                                    }
                                                    str += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                    blnItemHasEzafeBaha = true;
                                                }
                                                else
                                                {
                                                    str += "<div class=\"col-md-12\" style=\"padding-right:0px;margin:10px;\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" name=\"group"
                                                          + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                          + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>";
                                                }
                                            }
                                            else
                                            {
                                                if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                                {
                                                    Guid guNew = new Guid();
                                                    blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                    string strEzafeBahaIsChecked = "";
                                                    if (blnIsEzafeBahaAddedItems)
                                                    {
                                                        strEzafeBahaIsChecked = "checked=\"checked\"";
                                                    }
                                                    strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                    blnItemHasEzafeBaha = true;
                                                }
                                                else
                                                {
                                                    strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"padding-right:0px;margin:10px;text-align: right;\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" name=\"group"
                                                        + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                        + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>";
                                                }
                                            }
                                        }
                                    }
                                }
                                str += "</div>";
                            }
                            else
                            {
                                DataRow[] Dr = DtItemsHasConditionAddedToFB.Select("ItemsHasCondition_ConditionContextId=" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString());
                                if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["HasEnteringValue"].ToString() == "True")
                                {
                                    strDes = DrItemsHasConditionWithItemFBShomarehFiltered[0]["HasDes"].ToString().Trim() == "True" ? "<div class=\"col-md-1\"><span>" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["TitleDes"].ToString().Trim() + "</span></div>" : "";
                                    if (Dr.Length != 0)
                                    {

                                        string strMeghdar = decimal.Parse(Dr[0]["Meghdar"].ToString().Trim()).ToString("G29");
                                        str += "<div class=\"col-md-12\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\">";
                                        str += "<div class=\"row\" style=\"margin: 0px;\">";
                                        str += "<div class=\"col-md-6 conditionElementStyle\"><input type=\"checkbox\" checked=\"checked\" id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";
                                        str += "<div class=\"col-md-1\"><input style=\"text-align:center;padding:3px;border: 1px solid #d5d0ff;\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["StepChange"].ToString().Trim() + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"] + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar + "\"/></div>";
                                        str += strDes;
                                        str += "</div></div>";
                                    }
                                    else
                                    {

                                        str += "<div class=\"col-md-12\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\">";
                                        str += "<div class=\"row\" style=\"margin: 0px;\">";
                                        str += "<div class=\"col-md-6 conditionElementStyle\"><input type=\"checkbox\" id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";
                                        str += "<div class=\"col-md-1\"><input style=\"text-align:center;padding:3px;border: 1px solid #d5d0ff;\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["StepChange"].ToString().Trim() + "\" type=\"number\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["StepChange"].ToString().Trim() + "\" value=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["DefaultValue"].ToString().Trim() + "\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"] + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\"/></div>";
                                        str += strDes;
                                        str += "</div></div>";
                                    }
                                }
                                else
                                {
                                    if (Dr.Length != 0)
                                    {
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["MoveToRel"].ToString() != "True")
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }
                                                str += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {
                                                str += "<div class=\"col-md-4 conditionElementStyle\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" checked=\"checked\" onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "',"
                                                    + LevelNumber + ")\"/><span onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";
                                            }
                                        }
                                        else
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }
                                                strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {
                                                strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"padding-right:0px;margin:10px;text-align: right;\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" checked=\"checked\" name=\"group"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + Dt.Rows[0]["Shomareh"].ToString().Trim()
                                                    + "\" type=\"checkbox\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["MoveToRel"].ToString() != "True")
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }
                                                str += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {
                                                str += "<div class=\"col-md-4 conditionElementStyle\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber
                                                    + ")\"/><span onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";
                                            }
                                        }
                                        else
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }
                                                strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowEzafeBahaAndLakeGiriOnly('" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {
                                                strItemsShowInRelItems += "<div class=\"col-md-4\" style=\"padding-right:0px;margin:10px;text-align: right;\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" name=\"group"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + Dt.Rows[0]["Shomareh"].ToString().Trim()
                                                    + "\" type=\"checkbox\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    str += "</div>";
                    //}
                }

                str += "<div id=\"divRelItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" class=\"col-md-12\" style=\"padding:25px 5px 4px 5px;border:1px solid #c4d4db;border-radius:4px !important;display:none\">";
                if (DtOperationHasAddedOperations.Rows.Count != 0)
                {
                    for (int i = 0; i < DtOperationHasAddedOperations.Rows.Count; i++)
                    {
                        //if (DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() == "LakeGiri")
                        //{
                        //    blnIsLakeGiriAddedItems = clsRizMetreUserss.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(),"", BarAvordId, 0);
                        //    string strLakeGiriIsChecked = "";
                        //    if (blnIsLakeGiriAddedItems)
                        //    {
                        //        strLakeGiriIsChecked = "checked=\"checked\"";
                        //    }
                        //    str += "<div class=\"col-md-2\" style=\"margin:0px 5px\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strLakeGiriIsChecked + " onclick=\"ShowAndTickAllLakeGiri($(this),'" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"ShowLakeGiriOnly('" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "')\">" + DtOperationHasAddedOperations.Rows[i]["ButtonName"].ToString().Trim() + "</span></div>";
                        //    blnItemHasLakeGigri = true;
                        //}
                        //else
                        //{
                        //str += "<div class=\"col-md-2\" style=\"margin:0px 5px\"><input type=\"button\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" value=\"" + DtOperationHasAddedOperations.Rows[i]["ButtonName"].ToString().Trim() + "\" onclick=\"CheckOperationHasAddedOperations('" + DtOperationHasAddedOperations.Rows[i]["AddedOperationId"].ToString().Trim() + "','" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["Type"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() + "')\"/></div>";
                        //}

                        int RelType = DastyarCommon.GetRelType(DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim());
                        string strChecked = "";
                        var varItemsFBShomarehValueShomareh = context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == strItemsFBShomareh && x.BarAvordId == BarAvordId && x.Type == RelType).ToList();
                        DataTable DtItemsFBShomarehValueShomareh = clsConvert.ToDataTable(varItemsFBShomarehValueShomareh);
                        //DataTable DtItemsFBShomarehValueShomareh = clsItemsFBShomarehValueShomareh.ListWithParameter("FBShomareh='" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "' and BarAvordId=" + BarAvordId + " and Type=" + RelType);
                        if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
                            strChecked = "Checked=\"Checked\"";
                        str += "<div class=\"col-md-6\" style=\"margin:0px 5px;text-align: right;\"><input type=\"checkbox\" style=\"border:0px\" " + strChecked + " class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" onclick=\"CheckOperationHasAddedOperations($(this),'" + DtOperationHasAddedOperations.Rows[i]["AddedOperationId"].ToString().Trim() + "','" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["Type"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() + "'," + LevelNumber + ")\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"CheckOperationHasAddedOperationsOnly($(this),'" + DtOperationHasAddedOperations.Rows[i]["AddedOperationId"].ToString().Trim() + "','" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["Type"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() + "'," + LevelNumber + ")\">" + DtOperationHasAddedOperations.Rows[i]["ButtonName"].ToString().Trim() + "</span></div>";
                    }
                }
                if (strItemsShowInRelItems != "")
                {
                    str += strItemsShowInRelItems;
                }
                str += "</div></div>";
            }

            str += "<div id=\"Grid" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\">";
            str += "<div class=\"row styleHeaderTable\"><div class=\"col-md-1 spanStyleMitraSmall\"><span id=\"spanFieldShomarehName\">" + (blnIsEzafeBahaAddedItems ? "انتخاب" : "شماره") + "</span></div><div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">تعداد</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[0]["Vahed"].ToString().Trim() + " </div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">طول</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[1]["Vahed"].ToString().Trim() + "</div></div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">عرض</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[2]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">ارتفاع</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[3]["Vahed"].ToString().Trim() + "</div></div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom: 1px solid #84d4e6;\">وزن</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[4]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\"><span>مقدار جزء</span></div><div class=\"VahedStyle\">" + DtItemsFields.Rows[5]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-2 spanStyleMitraSmall\">توضیحات</div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div></div>";

            decimal dSumAll = 0;
            str += "<div class=\"row styleFieldTable\">";
            str += "<div class=\"col-md-12 RMCollectStyle\">";
            for (int i = 0; i < DtRizMetreUsers.Rows.Count; i++)
            {
                decimal dMeghdarJoz = 0;
                decimal dTedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString());
                decimal dTool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString());
                decimal dArz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString());
                decimal dErtefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString());
                decimal dVazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString());
                if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                    dMeghdarJoz = 0;
                else
                    dMeghdarJoz += (dTedad == 0 ? 1 : dTedad) * (dTool == 0 ? 1 : dTool) *
                    (dArz == 0 ? 1 : dArz) * (dErtefa == 0 ? 1 : dErtefa) * (dVazn == 0 ? 1 : dVazn);
                dSumAll += dMeghdarJoz;

                //string strLakeGiri = "";
                //if (blnItemHasLakeGigri)
                //{
                //    if (clsRizMetreUserss.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(),"", BarAvordId, int.Parse(DtRizMetreUsers.Rows[i]["Id"].ToString())))
                //    {
                //        strLakeGiri = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input checked=\"checked\" id=\"CKLakeGiri" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "\" onclick=\"SelectLakeGiri('" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\" type=\"checkbox\" /></div>";
                //    }
                //    else
                //    {
                //        if (blnIsLakeGiriAddedItems)
                //            strLakeGiri = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input id=\"CKLakeGiri" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "\" onclick=\"SelectLakeGiri('" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\" type=\"checkbox\" /></div>";
                //        else
                //            strLakeGiri = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input class=\"displayNone\" id=\"CKLakeGiri" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "\" onclick=\"SelectLakeGiri('" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\" type=\"checkbox\" /></div>";
                //    }
                //}

                //string strEzafeBaha = "";
                //if (blnItemHasEzafeBaha)
                //{
                //    if (clsRizMetreUserss.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(),"", BarAvordId, int.Parse(DtRizMetreUsers.Rows[i]["Id"].ToString())))
                //    {
                //        strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input checked=\"checked\" id=\"CKEzafeBaha" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\" type=\"checkbox\" /></div>";
                //    }
                //    else
                //    {
                //        if (blnIsEzafeBahaAddedItems)
                //            strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input id=\"CKEzafeBaha" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\" type=\"checkbox\" /></div>";
                //        else
                //            strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input class=\"displayNone\" id=\"CKEzafeBaha" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + DtRizMetreUsers.Rows[i]["Id"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "')\" type=\"checkbox\" /></div>";
                //    }
                //}


                DataTable DtItemsHasConditionFiltered = new DataTable();
                if (DtItemsHasCondition.Rows.Count != 0)
                {
                    string strItemAdded = Dt.Rows[0]["Shomareh"].ToString().Trim();
                    DataRow[] DrItemsHasCondition = DtItemsHasCondition.Select("ItemFBShomareh=" + Dt.Rows[0]["Shomareh"].ToString().Trim() + " and IsShow=1");
                    if (DrItemsHasCondition.Length != 0)
                    {
                        DtItemsHasConditionFiltered = DrItemsHasCondition.CopyToDataTable();
                    }
                }

                string strEzafeBaha = "";
                for (int m = 0; m < lst.Count; m++)
                {
                    DataRow[] DrItemsHasConditionWithItemFBShomarehFiltered = DtItemsHasConditionFiltered.Select("ConditionGroupId=" + lst[m]);

                    if (DrItemsHasConditionWithItemFBShomarehFiltered.Length != 0)
                    {
                        if (blnItemHasEzafeBaha)
                        {
                            if (DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(Dt.Rows[0]["Shomareh"].ToString().Trim(), DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString().Trim(), BarAvordId, Guid.Parse(DtRizMetreUsers.Rows[i]["ID"].ToString())))
                            {
                                strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input checked=\"checked\" id=\"CKEzafeBaha" + DtRizMetreUsers.Rows[i]["ID"].ToString() + lst[m] + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"checkbox\" /></div>";
                            }
                            else
                            {
                                if (blnIsEzafeBahaAddedItems)
                                    strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input id=\"CKEzafeBaha" + DtRizMetreUsers.Rows[i]["ID"].ToString() + lst[m] + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"checkbox\" /></div>";
                                else
                                    strEzafeBaha = "<div class=\"col-md-6\" style=\"padding-right:0px;\"><input class=\"displayNone\" id=\"CKEzafeBaha" + DtRizMetreUsers.Rows[i]["ID"].ToString() + lst[m] + "\" onclick=\"SelectEzafeBahaOrLakeGiriRecords('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"checkbox\" /></div>";
                            }
                        }
                    }
                }

                string strTedad = decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString()) == 0 ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Tedad"].ToString()).ToString("G29");
                string strTool = decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString()) == 0 ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Tool"].ToString()).ToString("G29");
                string strArz = decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString()) == 0 ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Arz"].ToString()).ToString("G29");
                string strErtefa = decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString()) == 0 ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Ertefa"].ToString()).ToString("G29");
                string strVazn = decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString()) == 0 ? "" : decimal.Parse(DtRizMetreUsers.Rows[i]["Vazn"].ToString()).ToString("G29");

                str += "<div class=\"row styleRowTable\" onclick=\"RizMetreSelectClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "')\"><div class=\"col-md-1\"><div class=\"col-md-12\" style=\"padding-left:0px;\"><span>" + DtRizMetreUsers.Rows[i]["Shomareh"].ToString() + "</span></div>" + strEzafeBaha + "</div>";
                str += "<div class=\"col-md-2\"><input type=\"text\" disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\" id=\"txtSharh" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + DtRizMetreUsers.Rows[i]["Sharh"].ToString() + "\"/></div>";
                str += "<div class=\"col-md-1\"><input type=\"text\" disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\" id=\"txtTedad" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strTedad + "\"/></div>";
                str += " <div class=\"col-md-1\"><input type=\"text\" disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\" id=\"txtTool" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strTool + "\"/></div>";
                str += "<div class=\"col-md-1\"><input type=\"text\" disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\" id=\"txtArz" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strArz + "\"/></div>";
                str += "<div class=\"col-md-1\"><input type=\"text\" disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\" id=\"txtErtefa" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strErtefa + "\"/></div>";
                str += "<div class=\"col-md-1\"><input type=\"text\" disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\" id=\"txtVazn" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + strVazn + "\"/></div>";
                str += "<div class=\"col-md-1 RMMJozStyle\">" + (dMeghdarJoz == 0 ? "" : Math.Round(dMeghdarJoz, 2).ToString("G29")) + "</div>";
                str += "<div class=\"col-md-2\"><input type=\"text\" disabled=\"disabled\" class=\"form-control spanStyleMitraSmall\" id=\"txtDes" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" value=\"" + DtRizMetreUsers.Rows[i]["Des"].ToString() + "\"/></div>";
                if (IsFromAddedOperation == "true")
                    str += "<div class=\"col-md-1\"><i class=\"fa fa-edit EditRMUStyle\" id=\"iEdit" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" onclick=\"EditNRMUClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "','" + lstItemsFields + "')\"></i><i id=\"iUpdate" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" class=\"fa fa-save SaveRMUStyle displayNone\" onclick=\"UpdateNRMUClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "')\"></i><i class=\"fa fa-trash DelRMUStyle\" onclick=\"DeleteRMUNClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + FBId + "')\"></i></div></div>";
                else
                    str += "<div class=\"col-md-1\"><i class=\"fa fa-edit EditRMUStyle\" id=\"iEdit" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" onclick=\"EditRMUClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "','" + FBId + "','" + lstItemsFields + "')\"></i><i id=\"iUpdate" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "\" class=\"fa fa-save SaveRMUStyle displayNone\" onclick=\"UpdateRMUClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "')\"></i><i class=\"fa fa-trash DelRMUStyle\" onclick=\"DeleteRMUClick('" + DtRizMetreUsers.Rows[i]["ID"].ToString() + "')\"></i></div></div>";
            }
            str += "</div>";
            str += "</div>";
        }
        else
        {
            str += "<div id=\"Grid" + Dt.Rows[0]["Shomareh"].ToString().Trim() + "\">";
            str += "<div class=\"row styleHeaderTable\"><div class=\"col-md-1 spanStyleMitraSmall\"><span id=\"spanFieldShomarehName\">" + "شماره" + "</span></div><div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">تعداد</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[0]["Vahed"].ToString().Trim() + " </div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">طول</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[1]["Vahed"].ToString().Trim() + "</div></div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">عرض</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[2]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">ارتفاع</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[3]["Vahed"].ToString().Trim() + "</div></div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom: 1px solid #84d4e6;\">وزن</div><div class=\"VahedStyle\">" + DtItemsFields.Rows[4]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-1 spanStyleMitraSmall\"><div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\"><span>مقدار جزء</span></div><div class=\"VahedStyle\">" + DtItemsFields.Rows[5]["Vahed"].ToString().Trim() + "</div></div><div class=\"col-md-2 spanStyleMitraSmall\">توضیحات</div>";
            str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div></div>";

        }
        str += "</div>";
        return new JsonResult("OK_" + str);
        //return "OK" + str;
    }


    [HttpPost]
    public JsonResult GetAddedAndRelItemsForRizMetreUsers([FromBody] GetAddedAndRelItemsForRizMetreUsersInputDto request)
    {
        Guid FBId = request.FBId;
        string IsFromAddedOperation = request.IsFromAddedOperation;
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        int Year = request.Year;
        long Operation = request.OperationId;
        string ForItem = request.ForItem;
        int LevelNumber = request.LevelNumber == 0 ? 1 : request.LevelNumber;
        DastyarCommon DastyarCommon = new DastyarCommon(context);

        var varDt = context.FBs.Where(x => x.ID == FBId).ToList();
        DataTable Dt = clsConvert.ToDataTable(varDt);
        //DataTable DtRizMetreUsers = new DataTable();
        //var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && (ForItem == "" ? (x.ForItem == null || x.ForItem == "") : x.ForItem == ForItem) && x.LevelNumber == LevelNumber).OrderBy(x => x.Shomareh).ToList();
        //DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
        bool blnItemHasEzafeBaha = false;
        string strItemsShowInRelItems = "";

        string str = "";
        string strShomareh = Dt.Rows[0]["Shomareh"].ToString().Trim();

        var varItemsHasCondition = (from tblItemsHasCondition in context.ItemsHasConditions
                                    join tblItemsHasConditionConditionContext in context.ItemsHasCondition_ConditionContexts
                                    on tblItemsHasCondition.Id equals tblItemsHasConditionConditionContext.ItemsHasConditionId
                                    join tblConditionContext in context.ConditionContexts on
                                    tblItemsHasConditionConditionContext.ConditionContextId equals tblConditionContext.Id
                                    join tblConditionGroup in context.ConditionGroups on
                                    tblConditionContext.ConditionGroupId equals tblConditionGroup.Id
                                    where tblItemsHasCondition.Year == Year
                                    orderby tblConditionGroup.Order, tblConditionContext.Id
                                    select new
                                    {
                                        tblItemsHasConditionConditionContext.Id,
                                        ItemsHasConditionId = tblItemsHasCondition.Id,
                                        ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                        tblItemsHasConditionConditionContext.HasEnteringValue,
                                        tblItemsHasConditionConditionContext.EnteringCount,
                                        tblItemsHasConditionConditionContext.DesForEnteringValue,
                                        tblConditionContext.Context,
                                        ConditionContextId = tblConditionContext.Id,
                                        tblConditionContext.HasDes,
                                        tblConditionContext.TitleDes,
                                        tblConditionContext.Des,
                                        tblConditionContext.ConditionContextRel,
                                        ConditionContextDes = tblItemsHasConditionConditionContext.Des,
                                        tblConditionGroup.ConditionGroupName,
                                        ConditionGroupId = tblConditionGroup.Id,
                                        tblItemsHasConditionConditionContext.DefaultValue,
                                        tblItemsHasConditionConditionContext.MinValue,
                                        tblItemsHasConditionConditionContext.MaxValue,
                                        tblItemsHasConditionConditionContext.StepChange,
                                        tblItemsHasConditionConditionContext.IsShow,
                                        tblItemsHasConditionConditionContext.ParentId,
                                        tblItemsHasConditionConditionContext.MoveToRel,
                                        tblItemsHasConditionConditionContext.ViewCheckAllRecords
                                    }).Where(x => x.ItemFBShomareh.Substring(0, 6) == strShomareh).ToList();
        DataTable DtItemsHasCondition = clsConvert.ToDataTable(varItemsHasCondition);

        var varItemsHasConditionAddedToFB = (from ItemsHasConditionAddedToFB in context.ItemsHasConditionAddedToFBs
                                             join ItemsAddingToFB in context.ItemsAddingToFBs on
                                             ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId
                                             equals ItemsAddingToFB.ItemsHasCondition_ConditionContextId
                                             select new
                                             {
                                                 ID = ItemsHasConditionAddedToFB.ID,
                                                 FBShomareh = ItemsHasConditionAddedToFB.FBShomareh,
                                                 BarAvordId = ItemsHasConditionAddedToFB.BarAvordId,
                                                 Meghdar = ItemsHasConditionAddedToFB.Meghdar,
                                                 Meghdar2 = ItemsHasConditionAddedToFB.Meghdar2,
                                                 ItemsHasCondition_ConditionContextId = ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId,
                                                 AddedItems = ItemsAddingToFB.AddedItems
                                             }).Where(x => x.FBShomareh.Substring(0, 6) == strShomareh && x.BarAvordId == BarAvordId).ToList();
        DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);

        var varOpItems = context.Operation_ItemsFBs.Where(x => x.Year == Year && x.OperationId == Operation).ToList();
        DataTable DtOpItems = clsConvert.ToDataTable(varOpItems);
        string strItemsFBShomareh = DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim();

        List<int> lst = new List<int>();
        bool blnIsEzafeBahaAddedItems = false;
        string strDes = "";
        if (IsFromAddedOperation == "false")
        {
            str += "<div class=\"styleHeaderTable1\">" +
                "<div class=\"row\">" +
                "<div class=\"col-md-3\">" +
                "<input id=\"btnAddedItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" style=\"border:0;\" type=\"button\" onclick=\"ShowForms('divAddedItems','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "'," + LevelNumber + ")\" value=\"اضافه / کسر بها - آیتم های مرتبط\" class=\"spanStyleMitraSmall spanFrameNameStyle ActiveAddRelItemTab\"/>" +
                "</div>";
            var varOperationHasAddedOperations = (from OpHasOp in _context.OperationHasAddedOperationses
                                                  join OpHasOpType in _context.OperationHasAddedOperationsTypes
                                                  on OpHasOp.Type equals OpHasOpType.Id
                                                  join OpHasOpLevelNumber in _context.OperationHasAddedOperationsLevelNumbers
                                                  on OpHasOp.Id equals OpHasOpLevelNumber.OperationHasAddedOperationsId
                                                  where OpHasOpLevelNumber.LevelNumber == LevelNumber
                                                  select new
                                                  {
                                                      OperationId = OpHasOp.OperationId,
                                                      AddedOperationId = OpHasOp.AddedOperationId,
                                                      Type = OpHasOp.Type,
                                                      ButtonName = OpHasOpType.TypeName,
                                                      LatinName = OpHasOpType.LatinName
                                                  }).Where(x => x.OperationId == Operation).ToList();
            DataTable DtOperationHasAddedOperations = varOperationHasAddedOperations.ToDataTable();

            //str += "<div class=\"col-md-2\">" +
            //    "<input id=\"btnRelItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" style=\"border:0;\" type=\"button\" onclick=\"ShowForms('divRelItems','" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "'," + LevelNumber + ")\" class=\"spanStyleMitraSmall spanFrameNameStyle\" value=\"آیتم های مرتبط\"/>" +
            //    "</div>" +
            str += "</div>";

            if (DtItemsHasCondition.Rows.Count != 0)
            {
                string strItemAdded = strShomareh; //Dt.Rows[0]["Shomareh"].ToString().Trim();

                DataRow[] DrItemsHasCondition = DtItemsHasCondition.Select("ItemFBShomareh=" + strShomareh + " and IsShow=1");
                DataTable DtItemsHasConditionFiltered = new DataTable();
                if (DrItemsHasCondition.Length != 0)
                {
                    DtItemsHasConditionFiltered = DrItemsHasCondition.CopyToDataTable();
                }

                bool blnItemsHasConditionChecked = true;
                for (int k = 0; k < DtItemsHasConditionFiltered.Rows.Count; k++)
                {
                    string strIsParent = DtItemsHasConditionFiltered.Rows[k]["ParentId"].ToString().Trim();
                    if (strIsParent != "0")
                    {
                        long lngIsParent = long.Parse(strIsParent);
                        var varItemsHasConditionAddedToFBWithParentId = (from ItHasConAddToFB in _context.ItemsHasConditionAddedToFBs
                                                                         join ItAddToFB in _context.ItemsAddingToFBs
                                                                         on ItHasConAddToFB.ItemsHasCondition_ConditionContextId equals
                                                                         ItAddToFB.ItemsHasCondition_ConditionContextId
                                                                         select new
                                                                         {
                                                                             FBShomareh = ItHasConAddToFB.FBShomareh,
                                                                             BarAvordUserId = ItHasConAddToFB.BarAvordId,
                                                                             Meghdar = ItHasConAddToFB.Meghdar,
                                                                             ItemsHasCondition_ConditionContextId = ItHasConAddToFB.ItemsHasCondition_ConditionContextId,
                                                                             AddedItems = ItAddToFB.AddedItems
                                                                         }).Where(x => x.ItemsHasCondition_ConditionContextId == lngIsParent).ToList();
                        DataTable DtItemsHasConditionAddedToFBWithParentId = varItemsHasConditionAddedToFBWithParentId.ToDataTable();

                        if (DtItemsHasConditionAddedToFBWithParentId.Rows.Count == 0)
                        {
                            blnItemsHasConditionChecked = false;
                        }
                    }

                    if (blnItemsHasConditionChecked)
                    {
                        var IsContain = lst.Contains(int.Parse(DtItemsHasConditionFiltered.Rows[k]["ConditionGroupId"].ToString()));
                        if (IsContain != true)
                        {
                            lst.Add(int.Parse(DtItemsHasConditionFiltered.Rows[k]["ConditionGroupId"].ToString()));
                        }
                    }
                }

                str += "<div id=\"divAddedItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" class=\"col-md-12\" style=\"padding:25px 5px 2px 5px;margin-bottom:5px;margin-bottom:5px;border:1px solid #c4d4db;border-radius:4px !important;text-align:right;display:none\">";
                for (int m = 0; m < lst.Count; m++)
                {
                    DataRow[] DrItemsHasConditionWithItemFBShomarehFiltered = DtItemsHasConditionFiltered.Select("ConditionGroupId=" + lst[m]);

                    if (DrItemsHasConditionWithItemFBShomarehFiltered.Length != 0)
                    {
                        if (DrItemsHasConditionWithItemFBShomarehFiltered.Length > 1)
                        {
                            bool blnCheckIsSelectData = false;
                            for (int ii = 0; ii < DrItemsHasConditionWithItemFBShomarehFiltered.Length; ii++)
                            {
                                DataRow[] Dr = DtItemsHasConditionAddedToFB.Select("ItemsHasCondition_ConditionContextId=" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString());
                                if (Dr.Length != 0)
                                {
                                    blnCheckIsSelectData = true;
                                }
                            }

                            if (blnCheckIsSelectData)
                                str += "<div class=\"col-md-4\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\"><input type=\"checkbox\" checked=\"checked\" onclick=\"OpenConditionDetails($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "')\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"OpenConditionDetailsOnly('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + strShomareh + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";
                            else
                                str += "<div class=\"col-md-4\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\"><input type=\"checkbox\" onclick=\"OpenConditionDetails($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "')\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"OpenConditionDetailsOnly('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + strShomareh + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";

                            str += "<div class=\"col-md-12\" id=\"divConditionGroup" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + strShomareh + "\" style=\"display:None;background-color:#ffe7e7;border:1px solid #bfe0ed;margin-bottom:20px;border-radius:5px !important;\">";
                            for (int ii = 0; ii < DrItemsHasConditionWithItemFBShomarehFiltered.Length; ii++)
                            {
                                string strType = "";
                                if (DrItemsHasConditionWithItemFBShomarehFiltered.Length > 1)
                                {
                                    strType = "radio";
                                }
                                else
                                {
                                    strType = "checkbox";
                                }
                                DataRow[] Dr = DtItemsHasConditionAddedToFB.Select("ItemsHasCondition_ConditionContextId=" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString());

                                if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["HasEnteringValue"].ToString() == "True")
                                {
                                    string strMeghdar = "0";
                                    strDes = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["HasDes"].ToString().Trim() == "True" ?
                                        "<span onclick=\"ShowConditionDes(" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionContextId"].ToString().Trim() + ")\" class=\"spanConditionDes\">" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["TitleDes"].ToString().Trim() + "</span>" : "";

                                    if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["EnteringCount"].ToString() == "1")
                                    {
                                        string strDesForEnteringValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DesForEnteringValue"].ToString().Trim();
                                        if (Dr.Length != 0)
                                        {
                                            string strMinValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MinValue"].ToString().Trim();
                                            string strMaxValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MaxValue"].ToString().Trim();

                                            strMeghdar = decimal.Parse(Dr[0]["Meghdar"].ToString().Trim()).ToString("G29");
                                            str += "<div class=\"row col-md-12\" style=\"margin:10px;\">"
                                                + "<div class=\"col-5\">"
                                                + "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                                + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + strShomareh + "\" checked=\"checked\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span>" +
                                                    "</div>"
                                                    + "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                                    + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue + "</span>"
                                                    + "</div>"
                                                    + "<div class=\"col-2\" style=\"padding-right:10px;\">" +
                                                    "<input style=\"text-align:center;width:100px;\"" +
                                                    " min=\"" + strMinValue + "\" max=\"" + strMaxValue + "\" DefaultValue=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DefaultValue"].ToString().Trim() + "\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim() + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar
                                                    + "\"/>" +
                                                    "</div>" +
                                                    "<div class=\"col-2\" style=\"padding-right:10px;\">" + strDes + "</div>";
                                            str += "</div>";
                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";

                                        }
                                        else
                                        {
                                            string strMinValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MinValue"].ToString().Trim();
                                            string strMaxValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MaxValue"].ToString().Trim();

                                            str += "<div class=\"row col-md-12\" style=\"margin:10px;\">"
                                                + "<div class=\"col-5\">"
                                                + "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                                + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + strShomareh + "\" />" +
                                                "<span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span>" +
                                                "</div>"
                                                + "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                                + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue + "</span>"
                                                + "</div>"
                                                + "<div class=\"col-2\" style=\"padding-right:10px;\">" +
                                                "<input style=\"text-align:center;width:100px;\" " +
                                                " min=\"" + strMinValue + "\" max=\"" + strMaxValue + "\"  DefaultValue=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DefaultValue"].ToString().Trim() + "\" step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim() + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" class=\"form-control spanStyleMitraMedium\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\"" +
                                                " step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim() + "\" value=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DefaultValue"].ToString().Trim()
                                                + "\"/>" +
                                                "</div>" +
                                                "<div class=\"col-2\" style=\"padding-right:10px;\">" + strDes + "</div>";
                                            str += "</div>";

                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";
                                        }
                                    }
                                    else if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["EnteringCount"].ToString() == "2")
                                    {
                                        if (Dr.Length != 0)
                                        {
                                            string[] strDesForEnteringValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DesForEnteringValue"].ToString().Trim().Split('_');
                                            string[] strDefaultValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DefaultValue"].ToString().Trim().Split('_');
                                            string[] strMinValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MinValue"].ToString().Trim().Split('_');
                                            string[] strMaxValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MaxValue"].ToString().Trim().Split('_');
                                            string[] strStepChange = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim().Split('_');

                                            string strDV1 = (strDefaultValue.Length >= 1 ? decimal.Parse(strDefaultValue[0]).ToString("G29") : "");
                                            string strDV2 = (strDefaultValue.Length == 2 ? decimal.Parse(strDefaultValue[1]).ToString("G29") : "");

                                            string strMin1 = (strMinValue.Length >= 1 ? decimal.Parse(strMinValue[0]).ToString("G29") : "");
                                            string strMin2 = (strMinValue.Length == 2 ? decimal.Parse(strMinValue[1]).ToString("G29") : "");

                                            string strMax1 = (strMaxValue.Length >= 1 ? decimal.Parse(strMaxValue[0]).ToString("G29") : "");
                                            string strMax2 = (strMaxValue.Length == 2 ? decimal.Parse(strMaxValue[1]).ToString("G29") : "");

                                            string strMeghdar1 = decimal.Parse(Dr[0]["Meghdar"].ToString().Trim()).ToString("G29");
                                            string strMeghdar2 = decimal.Parse(Dr[0]["Meghdar2"].ToString().Trim()).ToString("G29");

                                            // strMeghdar1 = (strDefaultValue.Length >= 1 ? decimal.Parse(strDefaultValue[0]).ToString("G29") : "");
                                            //string strMeghdar2 = (strDefaultValue.Length == 2 ? decimal.Parse(strDefaultValue[1]).ToString("G29") : "");
                                            str += "<div class=\"row col-md-12\" style=\"margin:10px;\">"
                                                + "<div class=\"row col-3\">"
                                                + "<div class=\"col-1\">"
                                                + "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                                + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + strShomareh + "\" checked=\"checked\"/>"
                                                + "</div>"
                                                + "<div class=\"col-10\">"
                                                + "<span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim()
                                                    + "</span>"
                                                    + "</div>"
                                                    + "</div>"
                                                    + "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                                    + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue[0] + "</span>"
                                                    + "</div>"
                                                    + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                                    + "<input style=\"text-align:center;width:100px;\" max=\"" + strMax2 + "\" min=\"" + strMin2 + "\" defaultvalue=\"" + strDV1 + "\" step=\"" + strStepChange[0]
                                                    + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "_1\" onchange=\"textMeghdarOnChange($(this),'"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar1 + "\"/>"
                                                    + "</div>"
                                                    + "<div class=\"col-1\" style=\"padding-right:10px;text-align: left;\">"
                                                    + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue[1] + "</span>"
                                                    + "</div>"
                                                    + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                                    + "<input style=\"text-align:center;width:100px;\" max=\"" + strMax2 + "\" min=\"" + strMin2 + "\" defaultvalue=\"" + strDV2 + "\" step=\"" + strStepChange[1]
                                                    + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "_2\" onchange=\"textMeghdarOnChange($(this),'"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar2 + "\"/>"
                                                    + "</div>"
                                                    + "<div class=\"col-2\" style=\"padding-right:10px;\">" + strDes + "</div>"
                                                    + "</div>";
                                            //str += "</div>";
                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";
                                        }
                                        else
                                        {
                                            string[] strDesForEnteringValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DesForEnteringValue"].ToString().Trim().Split('_');
                                            string[] strDefaultValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["DefaultValue"].ToString().Trim().Split('_');
                                            string[] strStepChange = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["StepChange"].ToString().Trim().Split('_');
                                            string[] strMinValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MinValue"].ToString().Trim().Split('_');
                                            string[] strMaxValue = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MaxValue"].ToString().Trim().Split('_');


                                            string strMin1 = (strMinValue.Length >= 1 ? decimal.Parse(strMinValue[0]).ToString("G29") : "");
                                            string strMin2 = (strMinValue.Length == 2 ? decimal.Parse(strMinValue[1]).ToString("G29") : "");

                                            string strMax1 = (strMaxValue.Length >= 1 ? decimal.Parse(strMaxValue[0]).ToString("G29") : "");
                                            string strMax2 = (strMaxValue.Length == 2 ? decimal.Parse(strMaxValue[1]).ToString("G29") : "");


                                            string strMeghdar1 = (strDefaultValue.Length >= 1 ? decimal.Parse(strDefaultValue[0]).ToString("G29") : "");
                                            string strMeghdar2 = (strDefaultValue.Length == 2 ? decimal.Parse(strDefaultValue[1]).ToString("G29") : "");

                                            str += "<div class=\"row col-md-12\" style=\"margin:10px;\">"
                                                + "<div class=\"row col-3\">"
                                                + "<div class=\"col-1\">"
                                                + "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                                + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + strShomareh + "\" />"
                                                + "</div>"
                                                + "<div class=\"col-10\">"
                                                + "<span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim()
                                                + "</span>"
                                                + "</div>"
                                                + "</div>"
                                                + "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                                + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue[0] + "</span>"
                                                + "</div>"
                                                + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                                + "<input style=\"text-align:center;width:100px;\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "_1\" class=\"form-control spanStyleMitraMedium\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\""
                                                + " min=\"" + strMin1 + "\" max=\"" + strMax1 + "\" defaultvalue=\"" + strMeghdar1 + "\" step=\"" + strStepChange[0] + "\" value=\"\"/>"
                                                + "</div>"
                                                + "<div class=\"col-1\" style=\"padding-right:10px;text-align: left;\">"
                                                + "<span class=\"spanStyleEnteringValue\">" + (strDesForEnteringValue.Length > 1 ? strDesForEnteringValue[1] : "") + "</span>"
                                                + "</div>"
                                                + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                                + "<input style=\"text-align:center;width:100px;\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "_2\" class=\"form-control spanStyleMitraMedium\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\""
                                                + " min=\"" + strMin2 + "\" max=\"" + strMax2 + "\" defaultvalue=\"" + strMeghdar2 + "\" step=\"" + (strStepChange.Length > 1 ? strStepChange[1] : "") + "\" value=\"\"/>"
                                                + "</div>"
                                                + "<div class=\"col-1\" style=\"padding-right:10px;\">" + strDes + "</div>"
                                                + "</div>";
                                            // + "</div>";

                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Dr.Length != 0)
                                    {
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MoveToRel"].ToString() != "True")
                                        {
                                            strDes = DrItemsHasConditionWithItemFBShomarehFiltered[0]["HasDes"].ToString().Trim() == "True" ? "<div class=\"col-4\"><span onclick=\"ShowConditionDes("
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionContextId"].ToString().Trim() + ")\" class=\"spanConditionDes\">" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["TitleDes"].ToString().Trim() + "</span></div>" : "";
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[ii]["AddedItems"].ToString().Trim(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }
                                                str += "<div class=\"row col-md-12\" style=\"padding-left: 0px;padding-right: 0px;text-align:right\"><div class=\"col-7\"><input type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" "
                                                    + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + "',"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\""
                                                    + " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh
                                                    + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "');\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                                str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";

                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {
                                                str += "<div class=\"row col-md-12\" style=\"padding-left:0px;margin:10px;\"><div class=\"col-7\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" checked=\"checked\" name=\"group"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + strShomareh + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                     + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";

                                                str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";

                                                //str += "<script>GetAndShowAddItems('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "')</script>";
                                            }
                                        }
                                        else
                                        {
                                            strDes = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["HasDes"].ToString().Trim() == "True" ? "<div class=\"col-4\"><span onclick=\"ShowConditionDes(" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionContextId"].ToString().Trim()
                                                + ")\" class=\"spanConditionDes\">" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["TitleDes"].ToString().Trim() + "</span></div>" : "";
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[ii]["AddedItems"].ToString(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }
                                                strItemsShowInRelItems += "<div class=\"row col-md-12\" style=\"padding-left:0px;padding-right:0px;text-align:right\"><div class=\"col-7\">" +
                                                    "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" "
                                                    + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\""
                                                    + " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "');\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                                str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";

                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {
                                                strItemsShowInRelItems += "<div class=\"row col-md-12\" style=\"padding-right:0px;margin:10px;text-align: right;\"><div class=\"col-7\">" +
                                                    "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" checked=\"checked\" name=\"group"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + strShomareh + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";

                                                str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";

                                                //str += "<script>GetAndShowAddItems('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "')</script>";

                                            }
                                        }
                                    }
                                    else
                                    {
                                        strDes = DrItemsHasConditionWithItemFBShomarehFiltered[ii]["HasDes"].ToString().Trim() == "True" ? "<div class=\"col-4\"><span onclick=\"ShowConditionDes(" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionContextId"].ToString().Trim()
                                            + ")\" class=\"spanConditionDes\">" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["TitleDes"].ToString().Trim() + "</span></div>" : "";
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["MoveToRel"].ToString() != "True")
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }

                                                str += "<div class=\"row col-md-12\" style=\"text-align:right\"><div class=\"col-7\">" +
                                                    "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\"" +
                                                    " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "');\">" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {

                                                str += "<div class=\"row col-md-12\" style=\"padding-right:0px;margin:10px;\"><div class=\"col-7\">" +
                                                    "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" name=\"group"
                                                     + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + strShomareh + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                     + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";

                                                str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";
                                            }
                                        }
                                        else
                                        {
                                            if (DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ViewCheckAllRecords"].ToString() == "True")
                                            {
                                                clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                                Guid guNew = new Guid();
                                                blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                                string strEzafeBahaIsChecked = "";
                                                if (blnIsEzafeBahaAddedItems)
                                                {
                                                    strEzafeBahaIsChecked = "checked=\"checked\"";
                                                }

                                                strItemsShowInRelItems += "<div class=\"col-md-12\" style=\"padding-left:0px;padding-right:0px;text-align:right\"><div class=\"col-7\">" +
                                                    "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\"" +
                                                    " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "');\">" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                                blnItemHasEzafeBaha = true;
                                            }
                                            else
                                            {

                                                strItemsShowInRelItems += "<div class=\"row col-md-12\" style=\"padding-right:0px;margin:10px;text-align: right;\"><div class=\"col-7\">" +
                                                    "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" name=\"group"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + strShomareh + "\" type=\"" + strType + "\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                    + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";

                                                str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[ii]["ID"].ToString() + "\"></div>";
                                            }
                                        }
                                    }
                                }
                            }
                            str += "</div>";
                        }
                        else
                        {
                            DataRow[] Dr = DtItemsHasConditionAddedToFB.Select("ItemsHasCondition_ConditionContextId=" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString());

                            strDes = DrItemsHasConditionWithItemFBShomarehFiltered[0]["HasDes"].ToString().Trim() == "True" ? "<div class=\"col-md-4\"><span onclick=\"ShowConditionDes(" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionContextId"].ToString().Trim()
                                + ")\" class=\"spanConditionDes\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["TitleDes"].ToString().Trim() + "</span></div>" : "";
                            if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["HasEnteringValue"].ToString() == "True")
                            {
                                if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["EnteringCount"].ToString() == "1")
                                {
                                    if (Dr.Length != 0)
                                    {
                                        string strDesForEnteringValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["DesForEnteringValue"].ToString().Trim();

                                        string strMeghdar = decimal.Parse(Dr[0]["Meghdar"].ToString().Trim()).ToString("G29");
                                        str += "<div class=\"col-md-12\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\">";
                                        str += "<div class=\"row\" style=\"margin: 0px;\">";
                                        str += "<div class=\"col-md-5 conditionElementStyle\">"
                                            + "<input type=\"checkbox\" checked=\"checked\" id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\""
                                            + " onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber
                                            + ")\"/>" +
                                            "<span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span>"
                                            + "</div>";

                                        str += "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                            + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue + "</span>"
                                            + "</div>";

                                        str += "<div class=\"col-md-1\">"
                                            + " <input style=\"text-align:center;padding:3px;border: 1px solid #d5d0ff;\""
                                            + " max =\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["MaxValue"].ToString().Trim() + "\""
                                            + " min =\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["MinValue"].ToString().Trim() + "\""
                                            + " DefaultValue =\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["DefaultValue"].ToString().Trim() + "\""
                                            + " step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["StepChange"].ToString().Trim() + "\" type=\"number\" id=\"txtMeghdar"
                                            + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"textMeghdarOnChange($(this),'"
                                            + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"] + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar + "\"/></div>";
                                        str += strDes;

                                        str += "</div></div>";

                                        str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";

                                    }
                                    else
                                    {
                                        /////////
                                        //////////ghjghj
                                        //////////////
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                        {
                                            clsRizMetreUsers clsRizMetreUsers = new clsRizMetreUsers();
                                            Guid guNew = new Guid();
                                            blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                            string strEzafeBahaIsChecked = "";
                                            if (blnIsEzafeBahaAddedItems)
                                            {
                                                strEzafeBahaIsChecked = "checked=\"checked\"";
                                            }

                                            str += "<div class=\"col-md-6\" style=\"padding-left:0px;padding-right:0px;text-align:right\">" +
                                                "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\"" +
                                                " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "');\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span>" + strDes + "</div>";
                                            blnItemHasEzafeBaha = true;
                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";

                                        }
                                        else
                                        {
                                            string strDesForEnteringValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["DesForEnteringValue"].ToString().Trim();

                                            str += "<div class=\"col-md-12\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\">";
                                            str += "<div class=\"row\" style=\"margin: 0px;\">";
                                            str += "<div class=\"col-md-5 conditionElementStyle\">"
                                                + "<input type=\"checkbox\" id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\""
                                                + " onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber
                                                + ")\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";

                                            str += "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                          + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue + "</span>"
                                          + "</div>";

                                            str += "<div class=\"col-md-1\">"
                                                + "<input style=\"text-align:center;padding:3px;border: 1px solid #d5d0ff;\" type=\"number\""
                                                + " max =\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["MaxValue"].ToString().Trim() + "\""
                                                + " min =\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["MinValue"].ToString().Trim() + "\""
                                                + " DefaultValue =\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["DefaultValue"].ToString().Trim() + "\""
                                                + " step=\"" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["StepChange"].ToString().Trim() + "\" value=\""
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["DefaultValue"].ToString().Trim() + "\" id=\"txtMeghdar"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"textMeghdarOnChange($(this),'"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"] + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\"/></div>";
                                            str += strDes;
                                            str += "</div></div>";
                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";

                                        }
                                    }
                                }
                                else if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["EnteringCount"].ToString() == "2")
                                {
                                    string strType = "checkbox";

                                    if (Dr.Length != 0)
                                    {
                                        string[] strDesForEnteringValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["DesForEnteringValue"].ToString().Trim().Split('_');
                                        string[] strDefaultValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["DefaultValue"].ToString().Trim().Split('_');
                                        string[] strMinValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["MinValue"].ToString().Trim().Split('_');
                                        string[] strMaxValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["MaxValue"].ToString().Trim().Split('_');
                                        string[] strStepChange = DrItemsHasConditionWithItemFBShomarehFiltered[0]["StepChange"].ToString().Trim().Split('_');

                                        string strDV1 = (strDefaultValue.Length >= 1 ? decimal.Parse(strDefaultValue[0]).ToString("G29") : "");
                                        string strDV2 = (strDefaultValue.Length == 2 ? decimal.Parse(strDefaultValue[1]).ToString("G29") : "");

                                        string strMin1 = (strMinValue.Length >= 1 ? decimal.Parse(strMinValue[0]).ToString("G29") : "");
                                        string strMin2 = (strMinValue.Length == 2 ? decimal.Parse(strMinValue[1]).ToString("G29") : "");

                                        string strMax1 = (strMaxValue.Length >= 1 ? decimal.Parse(strMaxValue[0]).ToString("G29") : "");
                                        string strMax2 = (strMaxValue.Length == 2 ? decimal.Parse(strMaxValue[1]).ToString("G29") : "");

                                        string strMeghdar1 = decimal.Parse(Dr[0]["Meghdar"].ToString().Trim()).ToString("G29");
                                        string strMeghdar2 = decimal.Parse(Dr[0]["Meghdar2"].ToString().Trim()).ToString("G29");

                                        // strMeghdar1 = (strDefaultValue.Length >= 1 ? decimal.Parse(strDefaultValue[0]).ToString("G29") : "");
                                        //string strMeghdar2 = (strDefaultValue.Length == 2 ? decimal.Parse(strDefaultValue[1]).ToString("G29") : "");
                                        str += "<div class=\"row col-md-12\" style=\"margin:10px;\">"
                                            + "<div class=\"row col-4\">"
                                            + "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                            + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + strShomareh + "\" checked=\"checked\"/>"

                                            + "<span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim()
                                                + "</span>"
                                                + "</div>"
                                                + "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                                + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue[0] + "</span>"
                                                + "</div>"
                                                + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                                + "<input style=\"text-align:center;width:100px;border: 1px solid #d5d0ff;\" max=\"" + strMax2 + "\" min=\"" + strMin2 + "\" defaultvalue=\"" + strDV1 + "\" step=\"" + strStepChange[0]
                                                + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "_1\" onchange=\"textMeghdarOnChange($(this),'"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar1 + "\"/>"
                                                + "</div>"
                                                + "<div class=\"col-1\" style=\"padding-right:10px;text-align: left;\">"
                                                + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue[1] + "</span>"
                                                + "</div>"
                                                + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                                + "<input style=\"text-align:center;width:100px;border: 1px solid #d5d0ff;\" max=\"" + strMax2 + "\" min=\"" + strMin2 + "\" defaultvalue=\"" + strDV2 + "\" step=\"" + strStepChange[1]
                                                + "\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "_2\" onchange=\"textMeghdarOnChange($(this),'"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\" class=\"form-control spanStyleMitraMedium\" value=\"" + strMeghdar2 + "\"/>"
                                                + "</div>"
                                                + "<div class=\"col-2\" style=\"padding-right:10px;\">" + strDes + "</div>"
                                                + "</div>";
                                        //str += "</div>";
                                        str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";
                                    }
                                    else
                                    {
                                        string[] strDesForEnteringValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["DesForEnteringValue"].ToString().Trim().Split('_');
                                        string[] strDefaultValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["DefaultValue"].ToString().Trim().Split('_');
                                        string[] strStepChange = DrItemsHasConditionWithItemFBShomarehFiltered[0]["StepChange"].ToString().Trim().Split('_');
                                        string[] strMinValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["MinValue"].ToString().Trim().Split('_');
                                        string[] strMaxValue = DrItemsHasConditionWithItemFBShomarehFiltered[0]["MaxValue"].ToString().Trim().Split('_');


                                        string strMin1 = (strMinValue.Length >= 1 ? decimal.Parse(strMinValue[0]).ToString("G29") : "");
                                        string strMin2 = (strMinValue.Length == 2 ? decimal.Parse(strMinValue[1]).ToString("G29") : "");

                                        string strMax1 = (strMaxValue.Length >= 1 ? decimal.Parse(strMaxValue[0]).ToString("G29") : "");
                                        string strMax2 = (strMaxValue.Length == 2 ? decimal.Parse(strMaxValue[1]).ToString("G29") : "");


                                        string strMeghdar1 = (strDefaultValue.Length >= 1 ? decimal.Parse(strDefaultValue[0]).ToString("G29") : "");
                                        string strMeghdar2 = (strDefaultValue.Length == 2 ? decimal.Parse(strDefaultValue[1]).ToString("G29") : "");

                                        str += "<div class=\"row col-md-12\" style=\"margin:10px;\">"
                                            + "<div class=\"row col-4\">"
                                            + "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" type=\"" + strType
                                            + "\" name=\"group" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + strShomareh + "\" />"
                                            + "<span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                            + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim()
                                            + "</span>"
                                            + "</div>"
                                            + "<div class=\"col-2\" style=\"padding-right:10px;text-align: left;\">"
                                            + "<span class=\"spanStyleEnteringValue\">" + strDesForEnteringValue[0] + "</span>"
                                            + "</div>"
                                            + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                            + "<input style=\"text-align:center;width:100px;border: 1px solid #d5d0ff;\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "_1\" class=\"form-control spanStyleMitraMedium\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\""
                                            + " min=\"" + strMin1 + "\" max=\"" + strMax1 + "\" defaultvalue=\"" + strMeghdar1 + "\" step=\"" + strStepChange[0] + "\" value=\"\"/>"
                                            + "</div>"
                                            + "<div class=\"col-1\" style=\"padding-right:10px;text-align: left;\">"
                                            + "<span class=\"spanStyleEnteringValue\">" + (strDesForEnteringValue.Length > 1 ? strDesForEnteringValue[1] : "") + "</span>"
                                            + "</div>"
                                            + "<div class=\"col-1\" style=\"padding-right:10px;\">"
                                            + "<input style=\"text-align:center;width:100px;border: 1px solid #d5d0ff;\" type=\"number\" id=\"txtMeghdar" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "_2\" class=\"form-control spanStyleMitraMedium\" onchange=\"textMeghdarOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "'," + LevelNumber + ")\""
                                            + " min=\"" + strMin2 + "\" max=\"" + strMax2 + "\" defaultvalue=\"" + strMeghdar2 + "\" step=\"" + (strStepChange.Length > 1 ? strStepChange[1] : "") + "\" value=\"\"/>"
                                            + "</div>"
                                            + "<div class=\"col-1\" style=\"padding-right:10px;\">" + strDes + "</div>"
                                            + "</div>";
                                        // + "</div>";

                                        str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";
                                    }

                                }
                            }
                            else
                            {

                                if (Dr.Length != 0)
                                {
                                    strDes = DrItemsHasConditionWithItemFBShomarehFiltered[0]["HasDes"].ToString().Trim() == "True" ? "<div class=\"col-4\"><span onclick=\"ShowConditionDes(" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionContextId"].ToString().Trim()
                                        + ")\" class=\"spanConditionDes\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["TitleDes"].ToString().Trim() + "</span></div>" : "";

                                    if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["MoveToRel"].ToString() != "True")
                                    {
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                        {
                                            clsRizMetreUsers rizMetreUser = new clsRizMetreUsers();
                                            Guid guNew = new Guid();
                                            blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                            string strEzafeBahaIsChecked = "";
                                            if (blnIsEzafeBahaAddedItems)
                                            {
                                                strEzafeBahaIsChecked = "checked=\"checked\"";
                                            }

                                            str += "<div class=\"row col-md-12\" style=\"text-align:right\"><div class=\"col-7\">" +
                                                "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\"" +
                                                " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "');\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";

                                            //str += "<script>GetAndShowAddItems('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "')</script>";

                                            blnItemHasEzafeBaha = true;
                                        }
                                        else
                                        {
                                            //str += "<div class=\"col-md-4 conditionElementStyle\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" checked=\"checked\" onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\"/>"
                                            //    + "<span onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>";

                                            str += "<div class=\"row col-md-12 conditionElementStyle\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\">" +
                                                "<div class=\"col-7\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString()
                                                + "\" type=\"checkbox\" checked=\"checked\" onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim()
                                                + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\"/><span onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>"
                                                + strDes
                                                + "</div>";

                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";

                                        }
                                    }
                                    else
                                    {
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                        {
                                            clsRizMetreUsers rizMetreUser = new clsRizMetreUsers();
                                            Guid guNew = new Guid();
                                            blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                            string strEzafeBahaIsChecked = "";
                                            if (blnIsEzafeBahaAddedItems)
                                            {
                                                strEzafeBahaIsChecked = "checked=\"checked\"";
                                            }

                                            str += "<div class=\"col-md-6\" style=\"padding-left:0px;padding-right:0px;text-align:right\">" +
                                                "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\"" +
                                                " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "');\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span>" + strDes + "</div>";
                                            blnItemHasEzafeBaha = true;
                                        }
                                        else
                                        {
                                            str += "<div class=\"col-md-12\" style=\"padding-right:0px;margin:10px;text-align: right;\"><div class=\"col-md-5\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" checked=\"checked\" name=\"group"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + strShomareh + "\" type=\"checkbox\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                        }
                                    }
                                }
                                else
                                {
                                    strDes = DrItemsHasConditionWithItemFBShomarehFiltered[0]["HasDes"].ToString().Trim() == "True" ? "<div class=\"col-4\"><span onclick=\"ShowConditionDes(" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionContextId"].ToString().Trim()
                                        + ")\" class=\"spanConditionDes\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["TitleDes"].ToString().Trim() + "</span></div>" : "";
                                    if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["MoveToRel"].ToString() != "True")
                                    {
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                        {
                                            clsRizMetreUsers rizMetreUser = new clsRizMetreUsers();
                                            Guid guNew = new Guid();
                                            blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guNew);
                                            string strEzafeBahaIsChecked = "";
                                            if (blnIsEzafeBahaAddedItems)
                                            {
                                                strEzafeBahaIsChecked = "checked=\"checked\"";
                                            }

                                            str += "<div class=\"row col-md-12\" style=\"text-align:right\"><div class=\"col-7\">" +
                                                "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\"" +
                                                " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "');\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";

                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";

                                            blnItemHasEzafeBaha = true;
                                        }
                                        else
                                        {
                                            str += "<div class=\"row col-md-12 conditionElementStyle\" style=\"color:#0002b1;padding-left:0px;padding-right:0px\">" +
                                                "<div class=\"col-7\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString()
                                                + "\" type=\"checkbox\" onclick=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim()
                                                + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\"/><span onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupName"].ToString().Trim() + "</span></div>"
                                                + strDes
                                                + "</div>";

                                            str += "<div class=\"col-12\" style=\"margin-top: 20px;margin-bottom: 40px;border: 1px solid #f7caf6;display:none\" id=\"divShowRizMetre" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\"></div>";
                                        }
                                    }
                                    else
                                    {
                                        if (DrItemsHasConditionWithItemFBShomarehFiltered[0]["ViewCheckAllRecords"].ToString() == "True")
                                        {
                                            clsRizMetreUsers rizMetreUser = new clsRizMetreUsers();
                                            Guid guID = new Guid();

                                            blnIsEzafeBahaAddedItems = DastyarCommon.CheckLakeGiriIsAddingRizMetreUsers1(strShomareh, DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString(), BarAvordId, guID);
                                            string strEzafeBahaIsChecked = "";
                                            if (blnIsEzafeBahaAddedItems)
                                            {
                                                strEzafeBahaIsChecked = "checked=\"checked\"";
                                            }

                                            str += "<div class=\"col-md-12\" style=\"padding-left: 0px;padding-right: 0px;text-align: right;\"><div class=\"col-md-5\">" +
                                                "<input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" type=\"checkbox\" style=\"border:0px\" class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" " + strEzafeBahaIsChecked + " onclick=\"ShowAndTickAllEzafeBahaAndLakeGiri($(this),'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "'," + LevelNumber + ",'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "')\"/><span class=\"spanStyleMitraSmall spanLakeGiriStyle\"" +
                                                " onclick=\"ShowEzafeBahaAndLakeGiriOnly(this,'" + strShomareh + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "');\">" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                            blnItemHasEzafeBaha = true;
                                        }
                                        else
                                        {

                                            str += "<div class=\"col-md-12\" style=\"padding-right:0px;margin:10px;text-align: right;\"><div class=\"col-7\"><input id=\"CK" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "\" onchange=\"CheckedOnChange($(this),'" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString().Trim() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ItemFBShomareh"].ToString().Trim() + "'," + LevelNumber + ")\" name=\"group"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + strShomareh + "\" type=\"checkbox\"/><span class=\"spanStyleMitraSmall spanStyleItemHasConditionDetails\" onclick=\"spanOnClick('" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ID"].ToString() + "','" + DrItemsHasConditionWithItemFBShomarehFiltered[0]["ConditionGroupId"].ToString() + "')\" style=\"color:#000296;\">"
                                                + DrItemsHasConditionWithItemFBShomarehFiltered[0]["Context"].ToString().Trim() + "</span></div>" + strDes + "</div>";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //if (DtOperationHasAddedOperations.Rows.Count != 0)
                //{
                //    for (int i = 0; i < DtOperationHasAddedOperations.Rows.Count; i++)
                //    {

                //        int RelType = DastyarCommon.GetRelType(DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim());
                //        string strChecked = "";
                //        //string strItemsFBShomareh = DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim();
                //        var varItemsFBShomarehValueShomareh = _context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == strItemsFBShomareh && x.BarAvordId == BarAvordId && x.Type == RelType).ToList();
                //        DataTable DtItemsFBShomarehValueShomareh = varItemsFBShomarehValueShomareh.ToDataTable();

                //        if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
                //            strChecked = "Checked=\"Checked\"";
                //        str += "<div class=\"col-md-6\" style=\"padding-left: 0px;padding-right: 0px;text-align: right;\">" +
                //            "<input type=\"checkbox\" style=\"border:0px\" " + strChecked + " class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" onclick=\"CheckOperationHasAddedOperations($(this),'" + DtOperationHasAddedOperations.Rows[i]["AddedOperationId"].ToString().Trim() + "','" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["Type"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() + "'," + LevelNumber + ")\"/>" +
                //            "<span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"CheckOperationHasAddedOperationsOnly($(this),'" + DtOperationHasAddedOperations.Rows[i]["AddedOperationId"].ToString().Trim() + "','" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["Type"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() + "'," + LevelNumber + ")\">" + DtOperationHasAddedOperations.Rows[i]["ButtonName"].ToString().Trim()
                //            + "</span></div>";
                //    }
                //}

                str += "</div>";


                //}
            }
            str += "</div>";

            //str += "<div id=\"divRelItems" + DtOpItems.Rows[0]["OperationId"].ToString().Trim() + "\" class=\"col-md-12\" style=\"padding:25px 5px 4px 5px;border:1px solid #c4d4db;border-radius:4px !important;display:none\">";
            //if (DtOperationHasAddedOperations.Rows.Count != 0)
            //{
            //    for (int i = 0; i < DtOperationHasAddedOperations.Rows.Count; i++)
            //    {

            //        int RelType = DastyarCommon.GetRelType(DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim());
            //        string strChecked = "";
            //        //string strItemsFBShomareh = DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim();
            //        var varItemsFBShomarehValueShomareh = _context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == strItemsFBShomareh && x.BarAvordId == BarAvordId && x.Type == RelType).ToList();
            //        DataTable DtItemsFBShomarehValueShomareh = varItemsFBShomarehValueShomareh.ToDataTable();

            //        if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
            //            strChecked = "Checked=\"Checked\"";
            //        str += "<div class=\"col-md-6\" style=\"padding-left: 0px;padding-right: 0px;text-align: right;\">" +
            //            "<input type=\"checkbox\" style=\"border:0px\" " + strChecked + " class=\"RizMetreUsersAddStyle spanStyleMitraSmall\" onclick=\"CheckOperationHasAddedOperations($(this),'" + DtOperationHasAddedOperations.Rows[i]["AddedOperationId"].ToString().Trim() + "','" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["Type"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() + "'," + LevelNumber + ")\"/>" +
            //            "<span class=\"spanStyleMitraSmall spanLakeGiriStyle\" onclick=\"CheckOperationHasAddedOperationsOnly($(this),'" + DtOperationHasAddedOperations.Rows[i]["AddedOperationId"].ToString().Trim() + "','" + DtOpItems.Rows[0]["ItemsFBShomareh"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["Type"].ToString().Trim() + "','" + DtOperationHasAddedOperations.Rows[i]["LatinName"].ToString().Trim() + "'," + LevelNumber + ")\">" + DtOperationHasAddedOperations.Rows[i]["ButtonName"].ToString().Trim() 
            //            + "</span></div>";
            //    }
            //}
            //if (strItemsShowInRelItems != "")
            //{
            //    str += strItemsShowInRelItems;
            //}
            //str += "</div></div>";
        }

        return new JsonResult(str);
    }
    public JsonResult GetLastShomarehRizMetreUsers([FromBody] GetLastShomarehRizMetreUsersInputDto request)
    {
        try
        {
            Guid FBId = request.FBId;
            //string strParam = "";
            //strParam = "FBId=" + FBId;
            int intLastShomareh = 1;
            var varDt = context.RizMetreUserses.Where(x => x.FBId == FBId).ToList();
            DataTable Dt = clsConvert.ToDataTable(varDt);
            if (Dt.Rows.Count != 0)
            {
                intLastShomareh = int.Parse(Dt.Rows[0]["Shomareh"].ToString().Trim()) + 1;
            }
            //DataTable Dt = clsRizMetreUserss.GetLastRizMetreUsersShomareh(strParam);
            return new JsonResult("OK" + intLastShomareh);
            //return "OK" + Dt.Rows[0]["lastShomareh"].ToString().Trim();
        }
        catch (Exception)
        {
            return new JsonResult("NOK");
            //return "NOK";
        }
    }
    public JsonResult GetSumOfItems([FromBody] GetSumOfItemsInputDto request)
    {
        try
        {
            int Year = request.Year;
            NoeFehrestBaha NoeFB = request.NoeFB;
            Guid BarAvordId = request.BarAvordId;

            var vYear = new SqlParameter("@Year", Year);
            var vNoeFB = new SqlParameter("@NoeFB", NoeFB);
            var vBarAvordUserId = new SqlParameter("@BarAvordId", BarAvordId);

            var OperationWithSumMeghdarAndRial = _context.Set<QuesForAbnieFaniValuesDto>()
                .FromSqlRaw("EXEC OperationWithSumMeghdarAndRial @Year,@NoeFB,@BarAvordId", Year, NoeFB, BarAvordId)
                .ToList();

            DataTable DtOperationSumMeghdarAndRial = clsConvert.ToDataTable(OperationWithSumMeghdarAndRial);

            //DataTable DtOperationSumMeghdarAndRial = clsOperation_ItemsFB.OperationWithSumMeghdarAndRialAll(int.Parse(Year), NoeFB, BarAvordId);
            //DtOperationSumMeghdarAndRial.TableName = "tblOperationSumMeghdarAndRial";
            DataSet Ds = new DataSet();
            Ds.Tables.Add(DtOperationSumMeghdarAndRial);
            //return Ds.GetXml();
            return new JsonResult(Ds.GetXml());
        }
        catch (Exception)
        {
            return new JsonResult("");
        }
    }

    public JsonResult ConfirmRizMetreUsersForAbnieFani([FromBody] ConfirmRizMetreUsersForAbnieFaniInputDto request)
    {

        Guid PolVaAbroId = request.PolVaAbroId;
        int PolNum = request.PolNum;
        Guid BarAvordId = request.BarAvordId;
        string LAbro = request.LAbro;
        string w1 = request.w1;
        string w2 = request.w2;
        string w3 = request.w3;
        string w4 = request.w4;
        string f = request.f;
        string m = request.m;
        string n = request.n;
        string k = request.k;
        string h = request.h;
        string t = request.t;
        string b1 = request.b1;
        string b2 = request.b2;
        string c1 = request.c1;
        string c2 = request.c2;
        string j = request.j;
        string p1 = request.p1;
        string p2 = request.p2;
        string D = request.D;
        string Hs = request.Hs;
        string TedadDahaneh = request.TedadDahaneh;
        string NahveEjraDal = request.NahveEjraDal;
        string LPayeMoarab = request.LPayeMoarab;
        string LKooleMoarab = request.LKooleMoarab;
        string NoeBanaii = request.NoeBanaii;
        string LW1j = request.LW1j;
        string LW1p = request.LW1p;
        string LB1W1 = request.LB1W1;
        string LB2W1 = request.LB2W1;
        string LW2j = request.LW2j;
        string LW2p = request.LW2p;
        string LB1W2 = request.LB1W2;
        string LB2W2 = request.LB2W2;
        string LW3j = request.LW3j;
        string LW3p = request.LW3p;
        string LB1W3 = request.LB1W3;
        string LB2W3 = request.LB2W3;
        string LW4j = request.LW4j;
        string LW4p = request.LW4p;
        string LB1W4 = request.LB1W4;
        string LB2W4 = request.LB2W4;
        string hMinw1 = request.hMinw1;
        string hMinw2 = request.hMinw2;
        string hMinw3 = request.hMinw3;
        string hMinw4 = request.hMinw4;



        //try
        //{
        string strPolNum = PolNum.ToString("D3");
        List<string> lstStr = new List<string>();
        lstStr.Add("3"); lstStr.Add("5");
        var RizMetreUsers1 = context.RizMetreUserses.Where(x => x.FB.BarAvordId == BarAvordId && lstStr.Contains(x.Type.Trim().Substring(1, 1))
                             && x.Type.Trim().Substring(4, 3) == strPolNum).ToList();

        if (RizMetreUsers1 != null)
        {
            context.RizMetreUserses.RemoveRange(RizMetreUsers1);
            context.SaveChanges();
        }
        //clsRizMetreUserss.Delete("BarAvordId=" + BarAvordID + " and (SUBSTRING(ltrim(rtrim(Type)),1,1) in('3','5')) and SUBSTRING(ltrim(rtrim(Type)),4,3)='" + PolNum.ToString("D3") + "'");
        AbnieFaniCommon AbnieFani = new AbnieFaniCommon(_context);
        if (NoeBanaii.Trim() == "1")
        {
            AbnieFani.GhalebBandi(BarAvordId, PolVaAbroId, PolNum, LAbro, w1, w2, w3, w4, f, m, n, k, h, D, TedadDahaneh, LPayeMoarab, LKooleMoarab);
            AbnieFani.GhalebBandiChenaj(BarAvordId, PolVaAbroId, PolNum, LAbro, w1, w2, w3, w4, f, m, n, k, h, t, b2, c2, p2, D, TedadDahaneh);
            if (NahveEjraDal.Trim() == "1")
                AbnieFani.GhalebBandiDalDarja(BarAvordId, PolVaAbroId, PolNum, LAbro, w1, w2, w3, w4, t, j, c1, D, TedadDahaneh);
            else if (NahveEjraDal.Trim() == "2")
                AbnieFani.GhalebBandiDalPishSakhteh(BarAvordId, PolVaAbroId, PolNum, LAbro, w1, w2, w3, w4, t, j, c1, D, TedadDahaneh);
            _context.SaveChanges();

            AbnieFani.Boton(PolVaAbroId, PolNum, BarAvordId, LAbro, w1, w2, w3, w4, f, m, n, k, h, t, b1, b2, j, c1, c2, p1, p2, D, TedadDahaneh, NahveEjraDal);

            AbnieFani.GhalebBandiFendasionDastak(PolVaAbroId, PolNum, D, TedadDahaneh, BarAvordId,
                LW1j, LW1p, LB1W1, LB2W1, LW2j, LW2p, LB1W2, LB2W2, LW3j, LW3p, LB1W3, LB2W3, LW4j, LW4p, LB1W4, LB2W4, h);

            AbnieFani.GhalebBandiDivarVaSotoonDastak(PolVaAbroId, PolNum, D, TedadDahaneh, BarAvordId, h, t, hMinw1, hMinw2, hMinw3, hMinw4, w1, w2, w3, w4);
            AbnieFani.GhalebBandiSarKalaDastak(PolVaAbroId, PolNum, D, TedadDahaneh, BarAvordId, t, hMinw1, hMinw2, hMinw3, hMinw4, w1, w2, w3, w4);

            AbnieFani.BotonFendasionDastak(PolVaAbroId, PolNum, D, TedadDahaneh, BarAvordId, h, t, hMinw1, hMinw2, hMinw3, hMinw4, w1, w2, w3, w4);

            AbnieFani.Armator(PolVaAbroId, PolNum, D, TedadDahaneh, BarAvordId, LAbro, Hs);

        }
        return new JsonResult("OK");
        //return "OK";
        //}
        //catch (Exception)
        //{
        //    return "NOK";
        //}
    }

    public JsonResult SaveEzafeBahaForAbnieFaniPol([FromBody] SaveEzafeBahaForAbnieFaniPolInputDto request)
    {
        Guid PolVaAbroId = request.PolVaAbroId;
        int PolNum = request.PolNum;
        Guid BarAvordId = request.BarAvordId;
        long QuestionForAbnieFaniId = request.QuestionForAbnieFaniId;
        string ItemsForAdd = request.ItemsForAdd;
        string ItemFBForAdd = request.ItemFBForAdd.Trim();
        DateTime Now = DateTime.Now;

        int LevelNumber = request.LevelNumber;

        string[] strShomareh = { "120701", "120702" };

        //bool blnCheck = false;
        //clsAbnieFaniQueries AbnieFaniQueries = new clsAbnieFaniQueries();
        //DataTable DtQuesForAbnieFaniValues = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFaniValues("PolVaAbroId=" + PolVaAbroId + " and QuestionForAbnieFaniId=" + QuestionForAbnieFaniId);
        //if (DtQuesForAbnieFaniValues.Rows.Count == 0)
        //{
        //    if (AbnieFaniQueries.Save(QuestionForAbnieFaniId, 1, 0, PolVaAbroId))
        //        blnCheck = true;
        //}
        //else
        //    blnCheck = true;

        //if (blnCheck)
        //{

        string strOtherShomareh = "";
        for (int i = 0; i < strShomareh.Length; i++)
        {
            if (strShomareh[i] != ItemFBForAdd.Trim())
                strOtherShomareh = strShomareh[i];
        }
        var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == ItemFBForAdd).ToList();
        DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

        var varFBUserOtherShomareh = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strOtherShomareh).ToList();
        DataTable DtFBUserOtherShomareh = clsConvert.ToDataTable(varFBUserOtherShomareh);

        //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + ItemFBForAdd.Trim() + "'");
        //DataTable DtFBUserOtherShomareh = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordID + " and Shomareh='" + strOtherShomareh + "'");
        //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
        //int FBId = 0;
        //if (DtFBUser.Rows.Count == 0)
        //{

        //    FBId = OperationItemsFB.SaveFB(BarAvordID, ItemFBForAdd.Trim(), 0);
        //}
        //else
        //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

        ItemFBForAdd = ItemFBForAdd.Trim();
        Guid FBId = new Guid();
        if (DtFBUser.Rows.Count == 0)
        {
            clsFB FBSave = new clsFB();
            FBSave.BarAvordId = BarAvordId;
            FBSave.Shomareh = ItemFBForAdd;
            FBSave.BahayeVahedZarib = 0;
            context.FBs.Add(FBSave);
            context.SaveChanges();
            FBId = FBSave.ID;
        }
        else
            FBId = Guid.Parse(DtFBUser.Rows[0]["Id"].ToString());


        bool blnCheckIsOtherShomarehRizMetreDelete = true;
        if (DtFBUserOtherShomareh.Rows.Count != 0)
        {
            //DataTable DtQuesForAbnieFani = clsQuesForAbnieFani.ListWithParameterQuesForAbnieFaniValues("isezafebaha=1 and ID<>" + QuestionForAbnieFaniId);

            string strParam = "isezafebaha=1 and Id<>" + QuestionForAbnieFaniId;
            var QuesForAbnieFaniParam = new SqlParameter("@Parameter", strParam);

            var QuesForAbnieFani = _context.Set<QuesForAbnieFaniValuesDto>()
                .FromSqlRaw("EXEC QuesForAbnieFaniValuesListWithParameter @Parameter", QuesForAbnieFaniParam)
                .ToList();

            DataTable DtQuesForAbnieFani = clsConvert.ToDataTable(QuesForAbnieFani);

            //DataTable DtQuesForAbnieFani = clsAbnieFaniQueries.ListWithParameterQuesForAbnieFani("isezafebaha=1 and id<>" + QuestionForAbnieFaniId);
            Guid FBIdOtherShomareh = Guid.Parse(DtFBUserOtherShomareh.Rows[0]["Id"].ToString());
            long lngQuesForAbnieFaniId = long.Parse(DtQuesForAbnieFani.Rows[0]["Id"].ToString());
            var varAbnieFaniQueries = context.QuesForAbnieFaniValuess.Where(x => x.PolVaAbroId == PolVaAbroId && x.QuestionForAbnieFaniId == lngQuesForAbnieFaniId).ToList();
            if (varAbnieFaniQueries != null)
            {
                context.QuesForAbnieFaniValuess.RemoveRange(varAbnieFaniQueries);
                context.SaveChanges();
            }
            //clsAbnieFaniQueries.DeleteQuesForAbnieFaniValuesWithParameter("PolVaAbroId=" + PolVaAbroId + " and QuestionForAbnieFaniId=" + DtQuesForAbnieFani.Rows[0]["Id"].ToString());

            var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                    join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                        BarAvordId = FB.BarAvordId
                                    }).Where(x => x.FBId == FBIdOtherShomareh && x.Type == "300" + PolNum.ToString("D3") + "60").OrderBy(x => x.Shomareh).ToList();
            DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
            // DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBIdOtherShomareh + " and type=300" + PolNum.ToString("D3") + "60");
            if (DtRizMetreUsers.Rows.Count != 0)
            {
                try
                {
                    var varRizMetreUsers1 = context.RizMetreUserses.Where(x => x.FBId == FBIdOtherShomareh && x.Type == "300" + PolNum.ToString("D3") + "60").ToList();
                    context.RizMetreUserses.RemoveRange(varRizMetreUsers1);
                    context.SaveChanges();
                    blnCheckIsOtherShomarehRizMetreDelete = true;
                }
                catch (Exception)
                {
                    blnCheckIsOtherShomarehRizMetreDelete = false;
                }

                //blnCheckIsOtherShomarehRizMetreDelete = clsRizMetreUserss.Delete("FBId=" + FBIdOtherShomareh + " and type=300" + PolNum.ToString("D3") + "60");
            }
        }

        if (blnCheckIsOtherShomarehRizMetreDelete)
        {
            bool blnAbnieFaniQueriesCheck = false;
            try
            {
                clsQuesForAbnieFaniValues QuesForAbnieFaniValue = new clsQuesForAbnieFaniValues();
                QuesForAbnieFaniValue.QuestionForAbnieFaniId = QuestionForAbnieFaniId;
                QuesForAbnieFaniValue.Value = 1;
                QuesForAbnieFaniValue.ShomarehFBSelectedId = 0;
                QuesForAbnieFaniValue.PolVaAbroId = PolVaAbroId;
                context.QuesForAbnieFaniValuess.Add(QuesForAbnieFaniValue);
                context.SaveChanges();
                blnAbnieFaniQueriesCheck = true;
            }
            catch (Exception)
            {
                blnAbnieFaniQueriesCheck = false;
            }
            //if (AbnieFaniQueries.Save(QuestionForAbnieFaniId, 1, 0, PolVaAbroId))
            if (blnAbnieFaniQueriesCheck)
            {
                //DataTable DtLastRizMetreUsersShomareh = clsRizMetreUserss.GetLastRizMetreUsersShomareh("FBId=" + FBId + " and type=300" + PolNum.ToString("D3") + "60");
                //int Shomareh = int.Parse(DtLastRizMetreUsersShomareh.Rows[0]["lastShomareh"].ToString().Trim());
                long Shomareh = 1;
                clsRizMetreUsers RizMetreUser = context.RizMetreUserses.Where(x => x.FBId == FBId).OrderByDescending(x => x.Shomareh).FirstOrDefault();
                if (RizMetreUser != null)
                    Shomareh = RizMetreUser.Shomareh + 1;
                else
                    Shomareh = 1;

                string[] ItemsForAddSplit = ItemsForAdd.Split(',');
                for (int i = 0; i < ItemsForAddSplit.Length - 1; i++)
                {
                    clsRizMetreUsers RizMetre = new clsRizMetreUsers();
                    string[] ItemsForAddSplitSplit = ItemsForAddSplit[i].Split("");
                    decimal Tedad = decimal.Parse(ItemsForAddSplitSplit[1].Trim());
                    decimal Vazn = decimal.Parse(ItemsForAddSplitSplit[2].Trim());
                    RizMetre.Shomareh = Shomareh++;
                    RizMetre.Sharh = "اضافه بها بابت مصرف سیمان";
                    RizMetre.Tedad = Tedad;
                    RizMetre.Tool = 0;
                    RizMetre.Arz = 0;
                    RizMetre.Ertefa = 0;
                    RizMetre.Vazn = Vazn;
                    RizMetre.Des = "";
                    RizMetre.FBId = FBId;
                    RizMetre.OperationsOfHamlId = 1;
                    RizMetre.Type = "300" + PolNum.ToString("D3") + "60";///اضافه بها بابت مصرف سیمان
                    RizMetre.ForItem = ItemsForAddSplitSplit[0].Trim();
                    RizMetre.UseItem = "";
                    RizMetre.LevelNumber = LevelNumber;
                    RizMetre.InsertDateTime = Now;

                    ///محاسبه مقدار جزء
                    decimal dMeghdarJoz = 0;
                    if (Tedad == 0 && Vazn == 0)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (Tedad == 0 ? 1 : Tedad) * (Vazn == 0 ? 1 : Vazn);
                    RizMetre.MeghdarJoz = dMeghdarJoz;


                    context.RizMetreUserses.Add(RizMetre);
                    context.SaveChanges();
                    //RizMetre.Save();
                }
                return new JsonResult("OK");

                //return "OK";
            }
            else
                return new JsonResult("NOK");

            //return "NOK";
        }
        else
            return new JsonResult("NOK");

        //return "NOK";
        //}
        //else
        //    return "NOK";
        /////////////////
    }

    public JsonResult UpdateEzafeBahaForAbnieFaniPol([FromBody] UpdateEzafeBahaForAbnieFaniPolInputDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        Guid PolVaAbroId = request.PolVaAbroId;
        int PolNum = request.PolNum;
        string ItemFBForUpdate = request.ItemFBForUpdate;
        string ItemShomareh = request.ItemShomareh;
        string Hajm = request.Hajm;
        string Meghdar = request.Meghdar;
        DateTime Now = DateTime.Now;


        clsRizMetreUsers RizMetre = new clsRizMetreUsers();
        RizMetre.Tedad = decimal.Parse(Hajm.Trim());
        RizMetre.Tool = 0;
        RizMetre.Arz = 0;
        RizMetre.Ertefa = 0;
        RizMetre.Vazn = decimal.Parse(Meghdar.Trim());
        RizMetre.Des = "";
        RizMetre.OperationsOfHamlId = 1;
        RizMetre.Type = "300" + PolNum.ToString("D3") + "60";///اضافه بها بابت مصرف سیمان
        RizMetre.ForItem = ItemShomareh.Trim();
        RizMetre.UseItem = "";
        //RizMetre.UpdateWithShomarehAndForItemAndType(BarAvordId, ItemFBForUpdate);
        return new JsonResult("OK");

        //return "OK";
        /////////////////
    }

    public JsonResult SaveEzafeBahaAndLakeGiriRizMetreAll([FromBody] SaveEzafeBahaAndLakeGiriRizMetreAllInputDto request)
    {
        //string Param = request.Param;
        string ItemShomareh = request.ItemShomareh.Trim();
        Guid BarAvordId = request.BarAvordId;
        int LevelNumber = request.LevelNumber;
        int Year = request.Year;
        int ConditionGroupId = request.ConditionGroupId;
        Guid currentFBId = request.FBId;
        long RBCode = long.Parse(request.RBCode.Trim());
        DateTime Now = DateTime.Now;

        //var varRizMetreUserses = (from RUsers in _context.RizMetreUserses
        //                          join fb in _context.FBs on RUsers.FBId equals fb.ID
        //                          where RUsers.LevelNumber == LevelNumber
        //                          select new
        //                          {
        //                              RUsers.ID,
        //                              RUsers.Shomareh,
        //                              RUsers.Sharh,
        //                              RUsers.Tedad,
        //                              RUsers.Tool,
        //                              RUsers.Arz,
        //                              RUsers.Ertefa,
        //                              RUsers.Vazn,
        //                              RUsers.Des,
        //                              RUsers.ForItem,
        //                              RUsers.Type,
        //                              RUsers.UseItem,
        //                              fb.BarAvordId,
        //                              FBShomareh = fb.Shomareh.Trim(),
        //                              RUsers.FBId
        //                          }).Where(x => x.FBId == currentFBId && x.Type == "1" && FBShomareh == ItemShomareh).OrderBy(x => x.Shomareh).ToList();

        var varRizMetreUserses = context.RizMetreUserses.Include(x => x.FB).Where(x => x.FBId == currentFBId && x.Type == "1" && x.FB.Shomareh == ItemShomareh).Select(x => new
        {
            x.ID,
            x.Shomareh,
            x.Sharh,
            x.Tedad,
            x.Tool,
            x.Arz,
            x.Ertefa,
            x.Vazn,
            x.Des,
            x.ForItem,
            x.Type,
            x.UseItem,
            x.FBId
        }).OrderBy(x => x.Shomareh).ToList();
        //var varRizMetreUsersesCurrent = (from RizMetreUserses in _context.RizMetreUserses
        //                                 join FB in _context.FBs on RizMetreUserses.FBId equals FB.ID
        //                                 where RizMetreUserses.LevelNumber == LevelNumber
        //                                 select new
        //                                 {
        //                                     ID = RizMetreUserses.ID,
        //                                     Shomareh = RizMetreUserses.Shomareh,
        //                                     Tedad = RizMetreUserses.Tedad,
        //                                     Tool = RizMetreUserses.Tool,
        //                                     Arz = RizMetreUserses.Arz,
        //                                     Ertefa = RizMetreUserses.Ertefa,
        //                                     Vazn = RizMetreUserses.Vazn,
        //                                     Des = RizMetreUserses.Des,
        //                                     FBId = RizMetreUserses.FBId,
        //                                     OperationsOfHamlId = RizMetreUserses.OperationsOfHamlId,
        //                                     ForItem = RizMetreUserses.ForItem,
        //                                     Type = RizMetreUserses.Type,
        //                                     UseItem = RizMetreUserses.UseItem,
        //                                     BarAvordId = FB.BarAvordId
        //                                 }).Where(x => x.FBId == currentFBId && x.ForItem == ItemShomareh && x.Type == "2").ToList();


        //var varItemsHasCondition = (from tblItemsHasCondition in context.ItemsHasConditions
        //                            join tblItemsHasConditionConditionContext in context.ItemsHasCondition_ConditionContexts
        //                            on tblItemsHasCondition.Id equals tblItemsHasConditionConditionContext.ItemsHasConditionId
        //                            join tblConditionContext in context.ConditionContexts on
        //                            tblItemsHasConditionConditionContext.ConditionContextId equals tblConditionContext.Id
        //                            join tblConditionGroup in context.ConditionGroups on
        //                            tblConditionContext.ConditionGroupId equals tblConditionGroup.Id
        //                            where tblItemsHasCondition.Year == Year && tblItemsHasConditionConditionContext.Id == RBCode
        //                            select new
        //                            {
        //                                tblItemsHasConditionConditionContext.Id,
        //                                ItemsHasConditionId = tblItemsHasCondition.Id,
        //                                ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
        //                                tblItemsHasConditionConditionContext.HasEnteringValue,
        //                                tblConditionContext.Context,
        //                                tblItemsHasConditionConditionContext.Des,
        //                                tblConditionGroup.ConditionGroupName,
        //                                ConditionGroupId = tblConditionGroup.Id,
        //                                tblItemsHasConditionConditionContext.DefaultValue,
        //                                tblItemsHasConditionConditionContext.IsShow,
        //                                tblItemsHasConditionConditionContext.ParentId,
        //                                tblItemsHasConditionConditionContext.MoveToRel,
        //                                tblItemsHasConditionConditionContext.ViewCheckAllRecords
        //                            }).Where(x => x.ItemFBShomareh == ItemShomareh).ToList();
        //DataTable DtItemsHasCondition = clsConvert.ToDataTable(varItemsHasCondition);

        List<ItemsAddingToFBEzafeBahaAndLakeGiriDto> lstAddingToFB = context.ItemsAddingToFBs
            .Include(x => x.ItemsHasCondition_ConditionContext).ThenInclude(x => x.ConditionContext)
            .Where(x => x.ItemsHasCondition_ConditionContext.Year == Year && x.ItemsHasCondition_ConditionContextId == RBCode)
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

        //string[] strParam = Param.Split("_");
        //for (int ii = 0; ii < strParam.Length - 1; ii++)
        //{

        //List<long> strItemsHasConditionConditionContext = new List<long>();
        //if (DtItemsHasCondition.Rows.Count != 0)
        //{
        //    for (int i = 0; i < DtItemsHasCondition.Rows.Count; i++)
        //    {
        //        strItemsHasConditionConditionContext.Add(long.Parse(DtItemsHasCondition.Rows[i]["ID"].ToString()));
        //    }
        //}

        long ShomareNew = 1;
        clsRizMetreUsers? RizMetreUser = context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.InsertDateTime).ThenByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordId);
        if (RizMetreUser != null)
        {
            long currentShomareNew = RizMetreUser.ShomarehNew == null || RizMetreUser.ShomarehNew.Trim() == "" ? 1 : long.Parse(RizMetreUser.ShomarehNew);
            if (currentShomareNew > RizMetreUser.Shomareh)
            {
                ShomareNew = currentShomareNew;
            }
            else
                ShomareNew = RizMetreUser.Shomareh;
        }

        foreach (var RM in varRizMetreUserses)
        {
            //string[] strParamSplit = strParam[ii].Split(',');
            Guid RizMetreId = RM.ID; //Guid.Parse(strParamSplit[0].ToString());
            string Tool = RM.Tool == null ? "0" : RM.Tool.Value.ToString(); //strParamSplit[1].ToString();
            string Arz = RM.Arz == null ? "0" : RM.Arz.Value.ToString();// strParamSplit[3].ToString();
            string Ertefa = RM.Ertefa == null ? "0" : RM.Ertefa.Value.ToString();// strParamSplit[2].ToString();

            //var varItemsAddingToFB = context.ItemsAddingToFBs.Where(x => strItemsHasConditionConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
            //DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);

            //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasConditionConditionContext);
            //for (int i = 0; i < DtItemsAddingToFB.Rows.Count; i++)

            foreach (var AddingToFB in lstAddingToFB)
            {
                switch (AddingToFB.ConditionType)
                {
                    case 4:
                        {
                            string[] strFieldsAdding = AddingToFB.FieldsAdding != null ? AddingToFB.FieldsAdding.Split(',') : new string[0];
                            string strCondition = AddingToFB.Condition != null ? AddingToFB.Condition : "";
                            string strAddedItems = AddingToFB.AddedItems != null ? AddingToFB.AddedItems : "";
                            string strConditionOp = strCondition.Replace("x", Tool.Trim());
                            StringToFormula StringToFormula = new StringToFormula();
                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                            if (blnCheck)
                            {
                                long lngItemsHasCondition_ConditionContextId = AddingToFB.ItemsHasCondition_ConditionContextId;
                                List<clsItemsHasConditionAddedToFB> lstItemsHasConditionAddedToFB = context.ItemsHasConditionAddedToFBs.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh.Trim() == ItemShomareh.Trim()
                                                && x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId && x.ConditionGroupId == ConditionGroupId).ToList();
                                if (lstItemsHasConditionAddedToFB.Count == 0)
                                {
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = ItemShomareh;
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                    ItemsHasConditionAddedToFB.Meghdar = decimal.Parse(Tool);
                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                    context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    context.SaveChanges();
                                }
                                //ItemsHasConditionAddedToFB.Save();

                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                strAddedItems = strAddedItems.Trim();
                                var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FBSave = new clsFB();
                                    FBSave.BarAvordId = BarAvordId;
                                    FBSave.Shomareh = strAddedItems.Trim();
                                    FBSave.BahayeVahedZarib = 0;
                                    context.FBs.Add(FBSave);
                                    context.SaveChanges();
                                    FBId = FBSave.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                //var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
                                //                               join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
                                //                               where RizMetreUsers.LevelNumber == LevelNumber
                                //                               select new
                                //                               {
                                //                                   RizMetreUsers.ID,
                                //                                   RizMetreUsers.Shomareh,
                                //                                   RizMetreUsers.Tedad,
                                //                                   RizMetreUsers.Tool,
                                //                                   RizMetreUsers.Arz,
                                //                                   RizMetreUsers.Ertefa,
                                //                                   RizMetreUsers.Vazn,
                                //                                   RizMetreUsers.Sharh,
                                //                                   RizMetreUsers.Des,
                                //                                   RizMetreUsers.FBId,
                                //                                   RizMetreUsers.OperationsOfHamlId,
                                //                                   RizMetreUsers.ForItem,
                                //                                   RizMetreUsers.Type,
                                //                                   RizMetreUsers.UseItem,
                                //                                   FB.BarAvordId
                                //                               }).Where(x => x.ID == RizMetreId).OrderBy(x => x.Shomareh).ToList();
                                //DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                long lngShomareh = RM.Shomareh; //long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                                        }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                                if (DtRizMetreUsers.Rows.Count == 0)
                                {
                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                    RizMetreUsers.Shomareh = RM.Shomareh;
                                    ShomareNew++;
                                    RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUsers.Sharh = RM.Sharh; //" آیتم " + strAddedItems; //DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();

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

                                    RizMetreUsers.Tedad = dTedad;
                                    RizMetreUsers.Tool = dTool;
                                    RizMetreUsers.Arz = dArz;
                                    RizMetreUsers.Ertefa = dErtefa;
                                    RizMetreUsers.Vazn = dVazn;
                                    RizMetreUsers.Des = RM.Des; //DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh + " - ریز متره " + lngShomareh;
                                    RizMetreUsers.FBId = FBId;
                                    RizMetreUsers.OperationsOfHamlId = 1;
                                    RizMetreUsers.Type = "2";
                                    RizMetreUsers.ForItem = ItemShomareh;
                                    RizMetreUsers.UseItem = "";
                                    RizMetreUsers.LevelNumber = LevelNumber;
                                    RizMetreUsers.ConditionContextId = AddingToFB.ConditionContextId;
                                    RizMetreUsers.ConditionContextRel = AddingToFB.ConditionContextRel;
                                    RizMetreUsers.InsertDateTime = Now;


                                    ///محاسبه مقدار جزء
                                    decimal dMeghdarJoz = 0;
                                    if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                        dMeghdarJoz = 0;
                                    else
                                        dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                    RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                    context.RizMetreUserses.Add(RizMetreUsers);
                                    context.SaveChanges();
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
                                string strResult = SubItemsAddingToFB(AddingToFB.ID, Ertefa, RizMetreId, BarAvordId, strAddedItems,ShomareNew);
                            }
                            //else
                            //{
                            //    //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                            //    strAddedItems = strAddedItems.Trim();
                            //    var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                            //    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                            //    //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                            //    //int FBId = 0;
                            //    //if (DtFBUser.Rows.Count == 0)
                            //    //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                            //    //else
                            //    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                            //    Guid FBId = new Guid();
                            //    if (DtFBUser.Rows.Count == 0)
                            //    {
                            //        clsFB FB = new clsFB();
                            //        FB.BarAvordId = BarAvordId;
                            //        FB.Shomareh = strAddedItems.Trim();
                            //        FB.BahayeVahedZarib = 0;
                            //        context.FBs.Add(FB);
                            //        context.SaveChanges();
                            //        FBId = FB.ID;
                            //    }
                            //    else
                            //    {
                            //        FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                            //    }

                            //    var varRizMetreUsersCurrent = (from RUsers in context.RizMetreUserses
                            //                                   join fb in context.FBs on RUsers.FBId equals fb.ID
                            //                                   where RUsers.LevelNumber == LevelNumber
                            //                                   select new
                            //                                   {
                            //                                       RUsers.ID,
                            //                                       RUsers.Shomareh,
                            //                                       RUsers.Sharh,
                            //                                       RUsers.Tedad,
                            //                                       RUsers.Tool,
                            //                                       RUsers.Arz,
                            //                                       RUsers.Ertefa,
                            //                                       RUsers.Vazn,
                            //                                       RUsers.Des,
                            //                                       RUsers.ForItem,
                            //                                       RUsers.Type,
                            //                                       RUsers.UseItem,
                            //                                       fb.BarAvordId,
                            //                                       RUsers.FBId
                            //                                   }).Where(x => x.ID == RizMetreId).OrderBy(x => x.Shomareh).ToList();
                            //    DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                            //    //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                            //    int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                            //    var varRizMetreUsers = (from RUsers in context.RizMetreUserses
                            //                            join fb in context.FBs on RUsers.FBId equals fb.ID
                            //                            where RUsers.LevelNumber == LevelNumber
                            //                            select new
                            //                            {
                            //                                RUsers.ID,
                            //                                RUsers.Shomareh,
                            //                                RUsers.Sharh,
                            //                                RUsers.Tedad,
                            //                                RUsers.Tool,
                            //                                RUsers.Arz,
                            //                                RUsers.Ertefa,
                            //                                RUsers.Vazn,
                            //                                RUsers.Des,
                            //                                RUsers.ForItem,
                            //                                RUsers.Type,
                            //                                RUsers.UseItem,
                            //                                fb.BarAvordId,
                            //                                RUsers.FBId
                            //                            }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).OrderBy(x => x.Shomareh).ToList();
                            //    DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                            //    //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                            //    if (DtRizMetreUsers.Rows.Count != 0)
                            //    {
                            //        Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                            //        var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                            //        if (clsRizMetreUsers != null)
                            //        {
                            //            context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                            //            context.SaveChanges();
                            //        }
                            //        //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                            //        string strResult = SubItemsAddingToFB(int.Parse(DtItemsAddingToFB.Rows[i]["ID"].ToString()), Ertefa, RizMetreId, BarAvordId, strAddedItems);
                            //    }
                            //}
                            break;
                        }
                    case 7:
                        {
                            string[] strFieldsAdding = AddingToFB.FieldsAdding != null ? AddingToFB.FieldsAdding.Split(',') : new string[0];
                            string strCondition = AddingToFB.Condition != null ? AddingToFB.Condition : "";
                            string strAddedItems = AddingToFB.AddedItems != null ? AddingToFB.AddedItems : "";

                            string strConditionOp = strCondition.Replace("x", Arz.Trim());
                            StringToFormula StringToFormula = new StringToFormula();
                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                            if (blnCheck)
                            {
                                long lngItemsHasCondition_ConditionContextId = AddingToFB.ItemsHasCondition_ConditionContextId;
                                List<clsItemsHasConditionAddedToFB> lstItemsHasConditionAddedToFB = context.ItemsHasConditionAddedToFBs.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh.Trim() == ItemShomareh.Trim()
                                                && x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId && x.ConditionGroupId == ConditionGroupId).ToList();
                                if (lstItemsHasConditionAddedToFB.Count == 0)
                                {
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = ItemShomareh;
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                    ItemsHasConditionAddedToFB.Meghdar = 0;
                                    ItemsHasConditionAddedToFB.ConditionGroupId = 0;
                                    context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    context.SaveChanges();
                                }
                                //ItemsHasConditionAddedToFB.Save();

                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                strAddedItems = strAddedItems.Trim();
                                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);
                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId;
                                    FB.Shomareh = strAddedItems.Trim();
                                    FB.BahayeVahedZarib = 0;
                                    context.FBs.Add(FB);
                                    context.SaveChanges();
                                    FBId = FB.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                var varRizMetreUsersCurrent = context.RizMetreUserses.Where(x => x.ID == RizMetreId).ToList();
                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                                if (DtRizMetreUsers.Rows.Count == 0)
                                {
                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                    RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                    ShomareNew++;
                                    RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUsers.Sharh = DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();

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
                                        dTedad = DtRizMetreUsersCurrent.Rows[0]["Tedad"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tedad"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dTool = DtRizMetreUsersCurrent.Rows[0]["Tool"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tool"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dArz = DtRizMetreUsersCurrent.Rows[0]["Arz"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Arz"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dErtefa = DtRizMetreUsersCurrent.Rows[0]["Ertefa"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Ertefa"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dVazn = DtRizMetreUsersCurrent.Rows[0]["Vazn"].ToString().Trim() == "" ? null : decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Vazn"].ToString());
                                    }

                                    RizMetreUsers.Tedad = dTedad;
                                    RizMetreUsers.Tool = dTool;
                                    RizMetreUsers.Arz = dArz;
                                    RizMetreUsers.Ertefa = dErtefa;
                                    RizMetreUsers.Vazn = dVazn;
                                    RizMetreUsers.Des = DtRizMetreUsersCurrent.Rows[0]["Des"].ToString().Trim();// DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh;
                                    RizMetreUsers.FBId = FBId;
                                    RizMetreUsers.OperationsOfHamlId = 1;
                                    RizMetreUsers.Type = "2";
                                    RizMetreUsers.ForItem = ItemShomareh;
                                    RizMetreUsers.UseItem = "";
                                    RizMetreUsers.LevelNumber = LevelNumber;
                                    RizMetreUsers.ConditionContextId = AddingToFB.ConditionContextId;
                                    RizMetreUsers.ConditionContextRel = AddingToFB.ConditionContextRel;
                                    RizMetreUsers.InsertDateTime = Now;


                                    ///محاسبه مقدار جزء
                                    decimal dMeghdarJoz = 0;
                                    if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                        dMeghdarJoz = 0;
                                    else
                                        dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                    RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                    context.RizMetreUserses.Add(RizMetreUsers);
                                    context.SaveChanges();
                                    //RizMetreUsers.Save();
                                }
                                else
                                {
                                    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                                    if (clsRizMetreUsers != null)
                                    {
                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                                        context.SaveChanges();
                                    }
                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                }
                            }
                            else
                            {
                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                strAddedItems = strAddedItems.Trim();
                                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId;
                                    FB.Shomareh = strAddedItems.Trim();
                                    FB.BahayeVahedZarib = 0;
                                    context.FBs.Add(FB);
                                    context.SaveChanges();
                                    FBId = FB.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                                var varRizMetreUsersCurrent = context.RizMetreUserses.Where(x => x.ID == RizMetreId).ToList();
                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                                if (DtRizMetreUsers.Rows.Count != 0)
                                {
                                    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                                    if (clsRizMetreUsers != null)
                                    {
                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                                        context.SaveChanges();
                                    }
                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                }
                            }
                            break;
                        }
                    case 9:
                        {
                            string[] strFieldsAdding = AddingToFB.FieldsAdding != null ? AddingToFB.FieldsAdding.Split(',') : new string[0];
                            string strCondition = AddingToFB.Condition != null ? AddingToFB.Condition : "";
                            string strAddedItems = AddingToFB.AddedItems != null ? AddingToFB.AddedItems : "";

                            string strConditionOp = strCondition.Replace("x", Tool.Trim()).Replace("y", Arz.Trim());
                            StringToFormula StringToFormula = new StringToFormula();
                            bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                            if (blnCheck)
                            {
                                long lngItemsHasCondition_ConditionContextId = AddingToFB.ItemsHasCondition_ConditionContextId;
                                List<clsItemsHasConditionAddedToFB> lstItemsHasConditionAddedToFB = context.ItemsHasConditionAddedToFBs.Where(x => x.BarAvordId == BarAvordId && x.FBShomareh.Trim() == ItemShomareh.Trim()
                                                && x.ItemsHasCondition_ConditionContextId == lngItemsHasCondition_ConditionContextId && x.ConditionGroupId == ConditionGroupId).ToList();
                                if (lstItemsHasConditionAddedToFB.Count == 0)
                                {
                                    clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                    ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                    ItemsHasConditionAddedToFB.FBShomareh = ItemShomareh;
                                    ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = lngItemsHasCondition_ConditionContextId;
                                    ItemsHasConditionAddedToFB.Meghdar = 0;
                                    ItemsHasConditionAddedToFB.ConditionGroupId = ConditionGroupId;
                                    context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                    context.SaveChanges();
                                }
                                //ItemsHasConditionAddedToFB.Save();

                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                strAddedItems = strAddedItems.Trim();
                                var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FBSave = new clsFB();
                                    FBSave.BarAvordId = BarAvordId;
                                    FBSave.Shomareh = strAddedItems.Trim();
                                    FBSave.BahayeVahedZarib = 0;
                                    context.FBs.Add(FBSave);
                                    context.SaveChanges();
                                    FBId = FBSave.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                //var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
                                //                               join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
                                //                               where RizMetreUsers.LevelNumber == LevelNumber
                                //                               select new
                                //                               {
                                //                                   RizMetreUsers.ID,
                                //                                   RizMetreUsers.Shomareh,
                                //                                   RizMetreUsers.Tedad,
                                //                                   RizMetreUsers.Tool,
                                //                                   RizMetreUsers.Arz,
                                //                                   RizMetreUsers.Ertefa,
                                //                                   RizMetreUsers.Vazn,
                                //                                   RizMetreUsers.Sharh,
                                //                                   RizMetreUsers.Des,
                                //                                   RizMetreUsers.FBId,
                                //                                   RizMetreUsers.OperationsOfHamlId,
                                //                                   RizMetreUsers.ForItem,
                                //                                   RizMetreUsers.Type,
                                //                                   RizMetreUsers.UseItem,
                                //                                   FB.BarAvordId
                                //                               }).Where(x => x.ID == RizMetreId).OrderBy(x => x.Shomareh).ToList();
                                //DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                long lngShomareh = RM.Shomareh; //long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                                        }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                                if (DtRizMetreUsers.Rows.Count == 0)
                                {
                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                    RizMetreUsers.Shomareh = RM.Shomareh;
                                    ShomareNew++;
                                    RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUsers.Sharh = RM.Sharh; //" آیتم " + strAddedItems; 

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

                                    RizMetreUsers.Tedad = dTedad;
                                    RizMetreUsers.Tool = dTool;
                                    RizMetreUsers.Arz = dArz;
                                    RizMetreUsers.Ertefa = dErtefa;
                                    RizMetreUsers.Vazn = dVazn;
                                    RizMetreUsers.Des = RM.Des; //DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh + " - ریز متره " + lngShomareh;
                                    RizMetreUsers.FBId = FBId;
                                    RizMetreUsers.OperationsOfHamlId = 1;
                                    RizMetreUsers.Type = "2";
                                    RizMetreUsers.ForItem = ItemShomareh;
                                    RizMetreUsers.UseItem = "";
                                    RizMetreUsers.LevelNumber = LevelNumber;
                                    RizMetreUsers.ConditionContextId = AddingToFB.ConditionContextId;
                                    RizMetreUsers.ConditionContextRel = AddingToFB.ConditionContextRel;
                                    RizMetreUsers.InsertDateTime = Now;


                                    ///محاسبه مقدار جزء
                                    decimal dMeghdarJoz = 0;
                                    if (dTedad == null && dTool == null && dArz == null && dErtefa == null && dVazn == null)
                                        dMeghdarJoz = 0;
                                    else
                                        dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                        (dArz == null ? 1 : dArz.Value) * (dErtefa == null ? 1 : dErtefa.Value) * (dVazn == null ? 1 : dVazn.Value);
                                    RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                    context.RizMetreUserses.Add(RizMetreUsers);
                                    context.SaveChanges();
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
                                string strResult = SubItemsAddingToFB(AddingToFB.ID, Ertefa, RizMetreId, BarAvordId, strAddedItems,ShomareNew);
                            }
                            break;
                        }
                    case 12:
                        {
                            string[] strFieldsAdding = AddingToFB.FieldsAdding != null ? AddingToFB.FieldsAdding.Split(',') : new string[0];
                            string strCondition = AddingToFB.Condition != null ? AddingToFB.Condition : "";
                            string strAddedItems = AddingToFB.AddedItems != null ? AddingToFB.AddedItems : "";
                            string strFinalWorking = AddingToFB.FinalWorking != null ? AddingToFB.FinalWorking : "";

                            string[] strConditionSplit = strCondition.Split("_");
                            DataTable DtRizMetreUserses = new DataTable();
                            string strForItem = "";
                            string strUseItem = "";
                            string strItemFBShomareh = ItemShomareh.Trim();

                            Guid guFBId = currentFBId;
                            strUseItem = ItemShomareh.Trim();
                            strForItem = AddingToFB.UseItemForAdd != null ? AddingToFB.UseItemForAdd : "";

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
                                    long lngItemsHasCondition_ConditionContextId = AddingToFB.ItemsHasCondition_ConditionContextId;
                                    clsItemsHasConditionAddedToFB? currentItemsHasConditionAddedToFBs = _context.ItemsHasConditionAddedToFBs
                                         .FirstOrDefault(x => x.BarAvordId == BarAvordId && x.FBShomareh.Trim() == strCurrentFBShomareh.Trim() &&
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

                                    if (strFinalWorking != "")
                                    {
                                        strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");
                                        string strItemShomareh = AddingToFB.AddedItems != null ? AddingToFB.AddedItems : "";
                                        strAddedItems = strAddedItems.Trim();
                                        var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                        DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                                        Guid FBId = new Guid();
                                        if (DtFBUser.Rows.Count == 0)
                                        {
                                            clsFB FBSave = new clsFB();
                                            FBSave.BarAvordId = BarAvordId;
                                            FBSave.Shomareh = strAddedItems.Trim();
                                            FBSave.BahayeVahedZarib = 0;
                                            context.FBs.Add(FBSave);
                                            context.SaveChanges();
                                            FBId = FBSave.ID;
                                        }
                                        else
                                            FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                        long lngShomareh = RM.Shomareh;
                                        var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                                                join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                                                }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();
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
                                            RizMetreUsers.FBId = FBId;
                                            RizMetreUsers.OperationsOfHamlId = 1;
                                            RizMetreUsers.Type = "2";
                                            RizMetreUsers.ForItem = ItemShomareh;
                                            RizMetreUsers.UseItem = "";
                                            RizMetreUsers.LevelNumber = LevelNumber;
                                            RizMetreUsers.ConditionContextId = AddingToFB.ConditionContextId;
                                            RizMetreUsers.ConditionContextRel = AddingToFB.ConditionContextRel;
                                            RizMetreUsers.InsertDateTime = Now;


                                            ///محاسبه مقدار جزء
                                            decimal dMeghdarJoz = 0;
                                            if (dTedad == null && dTool == null && dArz == null && dVazn == null)
                                                dMeghdarJoz = 0;
                                            else
                                                dMeghdarJoz += (dTedad == null ? 1 : dTedad.Value) * (dTool == null ? 1 : dTool.Value) *
                                                (dArz == null ? 1 : dArz.Value) * (dErtefa1 == null ? 1 : dErtefa1.Value) * (dVazn == null ? 1 : dVazn.Value);
                                            RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                            context.RizMetreUserses.Add(RizMetreUsers);
                                            context.SaveChanges();
                                            //RizMetreUsers.Save();
                                        }

                                    }
                                }

                            }

                            break;
                        }

                }
            }
        }
        return new JsonResult("OK_درج بدرستی صورت گرفت");

        // return "OKدرج بدرستی صورت گرفت";
        //}
        //catch (Exception)
        //{
        //    return Json("NOK", JsonRequestBehavior.AllowGet);

        //    //return "NOK";
        //}
    }

    public JsonResult SaveEzafeBahaAndLakeGiriRizMetre([FromBody] SaveEzafeBahaAndLakeGiriRizMetreInputDto request)
    {
        try
        {
            string Tool = request.Tool;
            string Ertefa = request.Ertefa;
            string Arz = request.Arz;
            string ItemShomareh = request.ItemShomareh;
            Guid RizMetreId = request.RizMetreId;
            Guid BarAvordId = request.BarAvordId;
            int LevelNumber = request.LevelNumber;
            DateTime Now = DateTime.Now;

            //string strItemsHasConditionConditionContext = "";
            string strShomareh = ItemShomareh.Trim();
            var varItemsHasCondition = (from tblItemsHasCondition in context.ItemsHasConditions
                                        join tblItemsHasConditionConditionContext in context.ItemsHasCondition_ConditionContexts
                                        on tblItemsHasCondition.Id equals tblItemsHasConditionConditionContext.ItemsHasConditionId
                                        join tblConditionContext in context.ConditionContexts on
                                        tblItemsHasConditionConditionContext.ConditionContextId equals tblConditionContext.Id
                                        join tblConditionGroup in context.ConditionGroups on
                                        tblConditionContext.ConditionGroupId equals tblConditionGroup.Id
                                        where tblItemsHasCondition.Year == request.Year
                                        select new
                                        {
                                            tblItemsHasConditionConditionContext.Id,
                                            ItemsHasConditionId = tblItemsHasCondition.Id,
                                            ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                            tblItemsHasConditionConditionContext.HasEnteringValue,
                                            tblConditionContext.Context,
                                            tblItemsHasConditionConditionContext.Des,
                                            tblConditionGroup.ConditionGroupName,
                                            ConditionGroupId = tblConditionGroup.Id,
                                            tblItemsHasConditionConditionContext.DefaultValue,
                                            tblItemsHasConditionConditionContext.IsShow,
                                            tblItemsHasConditionConditionContext.ParentId,
                                            tblItemsHasConditionConditionContext.MoveToRel,
                                            tblItemsHasConditionConditionContext.ViewCheckAllRecords
                                        }).Where(x => x.ItemFBShomareh == strShomareh).ToList();
            DataTable DtItemsHasCondition = clsConvert.ToDataTable(varItemsHasCondition);
            //DataTable DtItemsHasCondition = clsOperationItemsFB.ItemsHasConditionListWithParameter("ItemFBShomareh='" + ItemShomareh.Trim() + "'");
            //if (DtItemsHasCondition.Rows.Count != 0)
            //{
            //    strItemsHasConditionConditionContext += "ItemsHasCondition_ConditionContextId in(";
            //    for (int i = 0; i < DtItemsHasCondition.Rows.Count; i++)
            //    {
            //        if ((i + 1) < DtItemsHasCondition.Rows.Count)
            //            strItemsHasConditionConditionContext += DtItemsHasCondition.Rows[i]["Id"].ToString() + ",";
            //        else
            //            strItemsHasConditionConditionContext += DtItemsHasCondition.Rows[i]["Id"].ToString();
            //    }
            //    strItemsHasConditionConditionContext += ")";
            //}

            List<long> strItemsHasConditionConditionContext = new List<long>();
            if (DtItemsHasCondition.Rows.Count != 0)
            {
                for (int i = 0; i < DtItemsHasCondition.Rows.Count; i++)
                {
                    strItemsHasConditionConditionContext.Add(long.Parse(DtItemsHasCondition.Rows[i]["ID"].ToString()));
                }
            }

            clsRizMetreUsers? RizMetre = context.RizMetreUserses.Include(x => x.FB).OrderByDescending(x => x.InsertDateTime).ThenByDescending(x => x.Shomareh).FirstOrDefault(x => x.FB.BarAvordId == BarAvordId);
            long ShomareNew = 1;
            if (RizMetre!=null)
            {
                long currentShomareNew = RizMetre.ShomarehNew == null || RizMetre.ShomarehNew.Trim() == "" ? 1 : long.Parse(RizMetre.ShomarehNew);
                if (currentShomareNew > RizMetre.Shomareh)
                {
                    ShomareNew = currentShomareNew;
                }
                else
                    ShomareNew = RizMetre.Shomareh;
            }

            var varItemsAddingToFB1 = context.ItemsAddingToFBs.Where(x => strItemsHasConditionConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
            DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB1);

            string strTypeOp = "";
            //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasConditionConditionContext);
            for (int i = 0; i < DtItemsAddingToFB.Rows.Count; i++)
            {
                switch (DtItemsAddingToFB.Rows[i]["ConditionType"].ToString())
                {
                    case "4":
                        {
                            string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');
                            string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
                            string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
                            string strConditionOp = strCondition.Replace("x", Tool.Trim());
                            StringToFormula StringToFormula = new StringToFormula();
                            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                            if (blnCheck)
                            {
                                clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                ItemsHasConditionAddedToFB.FBShomareh = ItemShomareh;
                                ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());
                                ItemsHasConditionAddedToFB.Meghdar = 0;
                                ItemsHasConditionAddedToFB.ConditionGroupId = 0;
                                context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                context.SaveChanges();
                                //ItemsHasConditionAddedToFB.Save();

                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                strAddedItems = strAddedItems.Trim();
                                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId;
                                    FB.Shomareh = strAddedItems.Trim();
                                    FB.BahayeVahedZarib = 0;
                                    context.FBs.Add(FB);
                                    context.SaveChanges();
                                    FBId = FB.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                var varRizMetreUsersCurrent = context.RizMetreUserses.Where(x => x.ID == RizMetreId).ToList();
                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);

                                int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                                if (DtRizMetreUsers.Rows.Count == 0)
                                {
                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                    RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                    ShomareNew++;
                                    RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                                    RizMetreUsers.Sharh = DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();

                                    List<string> lst = new List<string>();
                                    for (int j = 0; j < strFieldsAdding.Length; j++)
                                    {
                                        lst.Add(strFieldsAdding[j]);
                                    }

                                    decimal dTedad = 0;
                                    decimal dTool = 0;
                                    decimal dArz = 0;
                                    decimal dErtefa = 0;
                                    decimal dVazn = 0;

                                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dTedad = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tedad"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dTool = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tool"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dArz = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Arz"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dErtefa = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Ertefa"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dVazn = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Vazn"].ToString());
                                    }

                                    RizMetreUsers.Tedad = dTedad;
                                    RizMetreUsers.Tool = dTool;
                                    RizMetreUsers.Arz = dArz;
                                    RizMetreUsers.Ertefa = dErtefa;
                                    RizMetreUsers.Vazn = dVazn;
                                    RizMetreUsers.Des = DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh;
                                    RizMetreUsers.FBId = FBId;
                                    RizMetreUsers.OperationsOfHamlId = 1;
                                    RizMetreUsers.Type = "2";
                                    RizMetreUsers.ForItem = ItemShomareh;
                                    RizMetreUsers.UseItem = "";
                                    RizMetreUsers.LevelNumber = LevelNumber;
                                    RizMetreUsers.InsertDateTime = Now;

                                    ///محاسبه مقدار جزء
                                    decimal dMeghdarJoz = 0;
                                    if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                                        dMeghdarJoz = 0;
                                    else
                                        dMeghdarJoz += (dTedad == 0 ? 1 : dTedad) * (dTool == 0 ? 1 : dTool) *
                                        (dArz == 0 ? 1 : dArz) * (dErtefa == 0 ? 1 : dErtefa) * (dVazn == 0 ? 1 : dVazn);
                                    RizMetreUsers.MeghdarJoz = dMeghdarJoz;


                                    context.RizMetreUserses.Add(RizMetreUsers);
                                    context.SaveChanges();
                                    //RizMetreUsers.Save();
                                    strTypeOp = "Add";
                                }
                                else
                                {
                                    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                                    if (clsRizMetreUsers != null)
                                    {
                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                                        context.SaveChanges();
                                    }
                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                    strTypeOp = "Del";
                                }
                                string strResult = SubItemsAddingToFB(long.Parse(DtItemsAddingToFB.Rows[i]["ID"].ToString()), Ertefa, RizMetreId, BarAvordId, strAddedItems,ShomareNew);
                            }
                            else
                            {
                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                strAddedItems = strAddedItems.Trim();
                                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId;
                                    FB.Shomareh = strAddedItems.Trim();
                                    FB.BahayeVahedZarib = 0;
                                    context.FBs.Add(FB);
                                    context.SaveChanges();
                                    FBId = FB.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                var varRizMetreUsersCurrent = context.RizMetreUserses.Where(x => x.ID == RizMetreId).ToList();
                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);


                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                                if (DtRizMetreUsers.Rows.Count != 0)
                                {
                                    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                                    if (clsRizMetreUsers != null)
                                    {
                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                                        context.SaveChanges();
                                    }
                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                    string strResult = SubItemsAddingToFB(long.Parse(DtItemsAddingToFB.Rows[i]["ID"].ToString()), Ertefa, RizMetreId, BarAvordId, strAddedItems,ShomareNew);
                                }
                            }
                            break;
                        }
                    case "7":
                        {
                            string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');
                            string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
                            string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
                            string strConditionOp = strCondition.Replace("x", Arz.Trim());
                            StringToFormula StringToFormula = new StringToFormula();
                            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                            if (blnCheck)
                            {

                                clsItemsHasConditionAddedToFB ItemsHasConditionAddedToFB = new clsItemsHasConditionAddedToFB();
                                ItemsHasConditionAddedToFB.BarAvordId = BarAvordId;
                                ItemsHasConditionAddedToFB.FBShomareh = ItemShomareh;
                                ItemsHasConditionAddedToFB.ItemsHasCondition_ConditionContextId = int.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());
                                ItemsHasConditionAddedToFB.Meghdar = 0;
                                ItemsHasConditionAddedToFB.ConditionGroupId = 0;
                                context.ItemsHasConditionAddedToFBs.Add(ItemsHasConditionAddedToFB);
                                context.SaveChanges();
                                //ItemsHasConditionAddedToFB.Save();

                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                strAddedItems = strAddedItems.Trim();
                                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId;
                                    FB.Shomareh = strAddedItems.Trim();
                                    FB.BahayeVahedZarib = 0;
                                    context.FBs.Add(FB);
                                    context.SaveChanges();
                                    FBId = FB.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                var varRizMetreUsersCurrent = context.RizMetreUserses.Where(x => x.ID == RizMetreId).ToList();
                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                                if (DtRizMetreUsers.Rows.Count == 0)
                                {
                                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                                    RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                    ShomareNew++;

                                    RizMetreUsers.ShomarehNew = ShomareNew.ToString();
                                    RizMetreUsers.Sharh = DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();

                                    List<string> lst = new List<string>();
                                    for (int j = 0; j < strFieldsAdding.Length; j++)
                                    {
                                        lst.Add(strFieldsAdding[j]);
                                    }

                                    decimal dTedad = 0;
                                    decimal dTool = 0;
                                    decimal dArz = 0;
                                    decimal dErtefa = 0;
                                    decimal dVazn = 0;

                                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dTedad = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tedad"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dTool = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tool"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dArz = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Arz"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dErtefa = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Ertefa"].ToString());
                                    }
                                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                                    if (strCal.Count != 0)
                                    {
                                        dVazn = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Vazn"].ToString());
                                    }

                                    RizMetreUsers.Tedad = dTedad;
                                    RizMetreUsers.Tool = dTool;
                                    RizMetreUsers.Arz = dArz;
                                    RizMetreUsers.Ertefa = dErtefa;
                                    RizMetreUsers.Vazn = dVazn;
                                    RizMetreUsers.Des = DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + " به آیتم " + ItemShomareh;
                                    RizMetreUsers.FBId = FBId;
                                    RizMetreUsers.OperationsOfHamlId = 1;
                                    RizMetreUsers.Type = "2";
                                    RizMetreUsers.ForItem = ItemShomareh;
                                    RizMetreUsers.UseItem = "";
                                    RizMetreUsers.LevelNumber = LevelNumber;
                                    RizMetreUsers.InsertDateTime = Now;


                                    ///محاسبه مقدار جزء
                                    decimal dMeghdarJoz = 0;
                                    if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                                        dMeghdarJoz = 0;
                                    else
                                        dMeghdarJoz += (dTedad == 0 ? 1 : dTedad) * (dTool == 0 ? 1 : dTool) *
                                        (dArz == 0 ? 1 : dArz) * (dErtefa == 0 ? 1 : dErtefa) * (dVazn == 0 ? 1 : dVazn);
                                    RizMetreUsers.MeghdarJoz = dMeghdarJoz;

                                    context.RizMetreUserses.Add(RizMetreUsers);
                                    context.SaveChanges();
                                    //RizMetreUsers.Save();
                                    strTypeOp = "Add";
                                }
                                else
                                {
                                    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                                    if (clsRizMetreUsers != null)
                                    {
                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                                        context.SaveChanges();
                                    }
                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                    strTypeOp = "Del";
                                }
                            }
                            else
                            {
                                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                strAddedItems = strAddedItems.Trim();
                                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                //int FBId = 0;
                                //if (DtFBUser.Rows.Count == 0)
                                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                //else
                                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                Guid FBId = new Guid();
                                if (DtFBUser.Rows.Count == 0)
                                {
                                    clsFB FB = new clsFB();
                                    FB.BarAvordId = BarAvordId;
                                    FB.Shomareh = strAddedItems.Trim();
                                    FB.BahayeVahedZarib = 0;
                                    context.FBs.Add(FB);
                                    context.SaveChanges();
                                    FBId = FB.ID;
                                }
                                else
                                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                var varRizMetreUsersCurrent = context.RizMetreUserses.Where(x => x.ID == RizMetreId).ToList();
                                DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).ToList();
                                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);

                                //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
                                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                                if (DtRizMetreUsers.Rows.Count != 0)
                                {
                                    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                                    if (clsRizMetreUsers != null)
                                    {
                                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                                        context.SaveChanges();
                                    }
                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                }
                            }
                            break;
                        }
                }
            }
            if (strTypeOp == "Add")
                return new JsonResult("OK_" + "ثبت بدرستی صورت گرفت");
            // return "OK" + "ثبت بدرستی صورت گرفت";
            else
                return new JsonResult("OK_" + "حذف بدرستی صورت گرفت");
            //return "OK" + "حذف بدرستی صورت گرفت";
        }
        catch (Exception)
        {
            return new JsonResult("NOK");
            //return "NOK";
        }
    }

    public string SubItemsAddingToFB(long Id, string Ertefa, Guid RizMetreId, Guid BarAvordId, string ItemShomareh,long ShomareNew)
    {
        var varItemsAddingToFB = context.SubItemsAddingToFBs.Where(x => x.ItemsAddingToFBId == Id).ToList();
        DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);
        int LevelNumber = 1;
        DateTime Now = DateTime.Now;

        //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListSubItemsAddingToFBWithParameter("ItemsAddingToFBId=" + Id);
        for (int i = 0; i < DtItemsAddingToFB.Rows.Count; i++)
        {
            string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
            string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
            string strFinalWorking = DtItemsAddingToFB.Rows[i]["FinalWorking"].ToString();
            string strConditionOp = strCondition.Replace("x", Ertefa.Trim());
            string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');

            var varRizMetreUsersCurrent = (from RUsers in context.RizMetreUserses
                                           join fb in context.FBs on RUsers.FBId equals fb.ID
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
                                           }).Where(x => x.ID == RizMetreId).OrderBy(x => x.Shomareh).ToList();
            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
            //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);

            StringToFormula StringToFormula = new StringToFormula();
            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
            if (blnCheck)
            {
                strFinalWorking = strFinalWorking.Replace("x", Ertefa.Trim());
                decimal dFinalWorking = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                strAddedItems = strAddedItems.Trim();
                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                //int FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                Guid FBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FB = new clsFB();
                    FB.BarAvordId = BarAvordId;
                    FB.Shomareh = strAddedItems.Trim();
                    FB.BahayeVahedZarib = 0;
                    context.FBs.Add(FB);
                    context.SaveChanges();
                    FBId = FB.ID;
                }
                else
                {
                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());
                }

                int intShomareh = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                var varRizMetreUsers = (from RUsers in context.RizMetreUserses
                                        join fb in context.FBs on RUsers.FBId equals fb.ID
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
                                        }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == intShomareh).OrderBy(x => x.Shomareh).ToList();
                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                if (DtRizMetreUsers.Rows.Count == 0)
                {
                    clsRizMetreUsers RizMetreUsers = new clsRizMetreUsers();
                    RizMetreUsers.Shomareh = long.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                    ShomareNew++;
                    RizMetreUsers.ShomarehNew = ShomareNew.ToString();

                    RizMetreUsers.Sharh = DtRizMetreUsersCurrent.Rows[0]["Sharh"].ToString().Trim();
                    List<string> lst = new List<string>();
                    for (int j = 0; j < strFieldsAdding.Length; j++)
                    {
                        lst.Add(strFieldsAdding[j]);
                    }

                    decimal dTedad = 0;
                    decimal dTool = 0;
                    decimal dArz = 0;
                    decimal dErtefa = 0;
                    decimal dVazn = 0;

                    var strCal = lst.Where(a => a.Substring(0, 1) == "1").ToList();
                    if (strCal.Count != 0)
                    {
                        dTedad = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tedad"].ToString());
                    }
                    strCal = lst.Where(a => a.Substring(0, 1) == "2").ToList();
                    if (strCal.Count != 0)
                    {
                        dTool = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Tool"].ToString());
                    }
                    strCal = lst.Where(a => a.Substring(0, 1) == "3").ToList();
                    if (strCal.Count != 0)
                    {
                        dArz = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Arz"].ToString());
                    }
                    strCal = lst.Where(a => a.Substring(0, 1) == "4").ToList();
                    if (strCal.Count != 0)
                    {
                        dErtefa = dFinalWorking;
                    }
                    strCal = lst.Where(a => a.Substring(0, 1) == "5").ToList();
                    if (strCal.Count != 0)
                    {
                        dVazn = decimal.Parse(DtRizMetreUsersCurrent.Rows[0]["Vazn"].ToString());
                    }

                    RizMetreUsers.Tedad = dTedad;
                    RizMetreUsers.Tool = dTool;
                    RizMetreUsers.Arz = dArz;
                    RizMetreUsers.Ertefa = dErtefa;
                    RizMetreUsers.Vazn = dVazn;
                    RizMetreUsers.Des = DtItemsAddingToFB.Rows[i]["DesOfAddingItems"].ToString().Trim() + ItemShomareh;
                    RizMetreUsers.FBId = FBId;
                    RizMetreUsers.OperationsOfHamlId = 1;
                    RizMetreUsers.Type = "2";
                    RizMetreUsers.ForItem = ItemShomareh;
                    RizMetreUsers.UseItem = "";
                    RizMetreUsers.LevelNumber = LevelNumber;
                    RizMetreUsers.InsertDateTime = Now;


                    ///محاسبه مقدار جزء
                    decimal dMeghdarJoz = 0;
                    if (dTedad == 0 && dTool == 0 && dArz == 0 && dErtefa == 0 && dVazn == 0)
                        dMeghdarJoz = 0;
                    else
                        dMeghdarJoz += (dTedad == 0 ? 1 : dTedad) * (dTool == 0 ? 1 : dTool) *
                        (dArz == 0 ? 1 : dArz) * (dErtefa == 0 ? 1 : dErtefa) * (dVazn == 0 ? 1 : dVazn);
                    RizMetreUsers.MeghdarJoz = dMeghdarJoz;

                    context.RizMetreUserses.Add(RizMetreUsers);
                    context.SaveChanges();
                    //RizMetreUsers.Save();
                }
                else
                {
                    Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                    var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID);
                    if (clsRizMetreUsers != null)
                    {
                        context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                        context.SaveChanges();
                    }
                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                }
            }
            //else
            //{
            //    clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
            //    DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
            //    int FBId = 0;
            //    if (DtFBUser.Rows.Count == 0)
            //        FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
            //    else
            //        FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

            //    DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);
            //    DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
            //    if (DtRizMetreUsers.Rows.Count != 0)
            //        clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
            //}
        }
        return "OK";
    }

    public JsonResult DeleteEzafeBahaAndLakeGiriRizMetreAll([FromBody] DeleteEzafeBahaAndLakeGiriRizMetreAllInputDto request)
    {
        try
        {
            //string Param = request.Param;
            //string ItemShomareh = request.ItemShomareh;
            //Guid BarAvordId = request.BarAvordId;
            //int LevelNumber = request.LevelNumber;
            //int Year = request.Year;



            string ItemShomareh = request.ItemShomareh.Trim();
            Guid BarAvordId = request.BarAvordId;
            int LevelNumber = request.LevelNumber;
            int Year = request.Year;
            int ConditionGroupId = request.ConditionGroupId;
            Guid currentFBId = request.FBId;
            long RBCode = long.Parse(request.RBCode.Trim());

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
                                      }).Where(x => x.FBId == currentFBId && x.Type == "1").OrderBy(x => x.Shomareh).ToList();



            //string strItemsHasConditionConditionContext = "";
            string strShomareh = ItemShomareh.Trim();
            var varItemsHasCondition = (from tblItemsHasCondition in context.ItemsHasConditions
                                        join tblItemsHasConditionConditionContext in context.ItemsHasCondition_ConditionContexts
                                        on tblItemsHasCondition.Id equals tblItemsHasConditionConditionContext.ItemsHasConditionId
                                        join tblConditionContext in context.ConditionContexts on
                                        tblItemsHasConditionConditionContext.ConditionContextId equals tblConditionContext.Id
                                        join tblConditionGroup in context.ConditionGroups on
                                        tblConditionContext.ConditionGroupId equals tblConditionGroup.Id
                                        where tblItemsHasCondition.Year == Year && tblItemsHasConditionConditionContext.Id == RBCode
                                        select new
                                        {
                                            tblItemsHasConditionConditionContext.Id,
                                            ItemsHasConditionId = tblItemsHasCondition.Id,
                                            ItemFBShomareh = tblItemsHasCondition.ItemFBShomareh,
                                            tblItemsHasConditionConditionContext.HasEnteringValue,
                                            tblConditionContext.Context,
                                            tblItemsHasConditionConditionContext.Des,
                                            tblConditionGroup.ConditionGroupName,
                                            ConditionGroupId = tblConditionGroup.Id,
                                            tblItemsHasConditionConditionContext.DefaultValue,
                                            tblItemsHasConditionConditionContext.IsShow,
                                            tblItemsHasConditionConditionContext.ParentId,
                                            tblItemsHasConditionConditionContext.MoveToRel,
                                            tblItemsHasConditionConditionContext.ViewCheckAllRecords
                                        }).Where(x => x.ItemFBShomareh == strShomareh).ToList();
            DataTable DtItemsHasCondition = clsConvert.ToDataTable(varItemsHasCondition);
            ////DataTable DtItemsHasCondition = clsOperationItemsFB.ItemsHasConditionListWithParameter("ItemFBShomareh='" + ItemShomareh.Trim() + "'");
            //if (DtItemsHasCondition.Rows.Count != 0)
            //{
            //    strItemsHasConditionConditionContext += "ItemsHasCondition_ConditionContextId in(";
            //    for (int i = 0; i < DtItemsHasCondition.Rows.Count; i++)
            //    {
            //        if ((i + 1) < DtItemsHasCondition.Rows.Count)
            //            strItemsHasConditionConditionContext += DtItemsHasCondition.Rows[i]["Id"].ToString() + ",";
            //        else
            //            strItemsHasConditionConditionContext += DtItemsHasCondition.Rows[i]["Id"].ToString();
            //    }
            //    strItemsHasConditionConditionContext += ")";
            //}

            List<long> lngItemsHasConditionConditionContext = new List<long>();
            if (DtItemsHasCondition.Rows.Count != 0)
            {
                for (int i = 0; i < DtItemsHasCondition.Rows.Count; i++)
                {
                    lngItemsHasConditionConditionContext.Add(long.Parse(DtItemsHasCondition.Rows[i]["ID"].ToString()));
                }
            }


            //string[] strParam = Param.Split("");
            //for (int ii = 0; ii < strParam.Length - 1; ii++)
            //{
            List<clsItemsHasConditionAddedToFB> lstItemsHasConditionAddedToFBs = context.ItemsHasConditionAddedToFBs.Where(x => lngItemsHasConditionConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)
                    && x.BarAvordId == BarAvordId).ToList();
            if (lstItemsHasConditionAddedToFBs.Count != 0)
            {
                context.ItemsHasConditionAddedToFBs.RemoveRange(lstItemsHasConditionAddedToFBs);
                context.SaveChanges();
            }

            foreach (var RM in varRizMetreUserses)
            {
                //string[] strParamSplit = strParam[ii].Split(',');
                Guid RizMetreId = RM.ID;
                string Tool = RM.Tool.ToString(); //strParamSplit[1].ToString();
                string Ertefa = RM.Ertefa.ToString(); //strParamSplit[2].ToString();
                string Arz = RM.Arz.ToString(); //strParamSplit[3].ToString();

                var varItemsAddingToFB = context.ItemsAddingToFBs.Where(x => lngItemsHasConditionConditionContext.Contains(x.ItemsHasCondition_ConditionContextId)).ToList();
                DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);
                // DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListWithParameter(strItemsHasConditionConditionContext);
                for (int i = 0; i < DtItemsAddingToFB.Rows.Count; i++)
                {
                    switch (DtItemsAddingToFB.Rows[i]["ConditionType"].ToString())
                    {
                        case "4":
                            {
                                string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');
                                string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
                                string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
                                string strConditionOp = strCondition.Replace("x", Tool);
                                StringToFormula StringToFormula = new StringToFormula();
                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                                if (blnCheck)
                                {
                                    strAddedItems = strAddedItems.Trim();
                                    //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                    var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                    //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                                    //int FBId = 0;
                                    //if (DtFBUser.Rows.Count == 0)
                                    //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                                    //else
                                    //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                                    Guid FBId = new Guid();
                                    if (DtFBUser.Rows.Count == 0)
                                    {
                                        clsFB Fb = new clsFB();
                                        Fb.BarAvordId = BarAvordId;
                                        Fb.Shomareh = strAddedItems.Trim();
                                        Fb.BahayeVahedZarib = 0;
                                        context.FBs.Add(Fb);
                                        context.SaveChanges();
                                        FBId = Fb.ID;
                                    }
                                    else
                                        FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                    //var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
                                    //                               join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
                                    //                               where RizMetreUsers.LevelNumber == LevelNumber
                                    //                               select new
                                    //                               {
                                    //                                   RizMetreUsers.ID,
                                    //                                   RizMetreUsers.Shomareh,
                                    //                                   RizMetreUsers.Tedad,
                                    //                                   RizMetreUsers.Tool,
                                    //                                   RizMetreUsers.Arz,
                                    //                                   RizMetreUsers.Ertefa,
                                    //                                   RizMetreUsers.Vazn,
                                    //                                   RizMetreUsers.Des,
                                    //                                   RizMetreUsers.FBId,
                                    //                                   RizMetreUsers.OperationsOfHamlId,
                                    //                                   RizMetreUsers.ForItem,
                                    //                                   RizMetreUsers.Type,
                                    //                                   RizMetreUsers.UseItem,
                                    //                                   FB.BarAvordId
                                    //                               }).Where(x => x.ID == RizMetreId).OrderBy(x => x.Shomareh).ToList();
                                    //DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
                                    //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);

                                    long lngShomareh = RM.Shomareh; //int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                                    //var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                    //                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
                                    //                        where RizMetreUsers.LevelNumber == LevelNumber
                                    //                        select new
                                    //                        {
                                    //                            RizMetreUsers.ID,
                                    //                            RizMetreUsers.Shomareh,
                                    //                            RizMetreUsers.Tedad,
                                    //                            RizMetreUsers.Tool,
                                    //                            RizMetreUsers.Arz,
                                    //                            RizMetreUsers.Ertefa,
                                    //                            RizMetreUsers.Vazn,
                                    //                            RizMetreUsers.Des,
                                    //                            RizMetreUsers.FBId,
                                    //                            RizMetreUsers.OperationsOfHamlId,
                                    //                            RizMetreUsers.ForItem,
                                    //                            RizMetreUsers.Type,
                                    //                            RizMetreUsers.UseItem,
                                    //                            FB.BarAvordId
                                    //                        }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == strShomareh1).OrderBy(x => x.Shomareh).ToList();
                                    //DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                                    //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");

                                    List<clsRizMetreUsers> lstRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                                    if (lstRizMetreUsers.Count != 0)
                                    {
                                        context.RizMetreUserses.RemoveRange(lstRizMetreUsers);
                                        context.SaveChanges();
                                    }

                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                    string strResult = DeleteSubItemsAddingToFB(long.Parse(DtItemsAddingToFB.Rows[i]["ID"].ToString()), Ertefa, RizMetreId, BarAvordId, strAddedItems);
                                }
                                break;
                            }
                        case "7":
                            {
                                string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');
                                string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
                                string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
                                string strConditionOp = strCondition.Replace("x", Arz);
                                StringToFormula StringToFormula = new StringToFormula();
                                bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
                                if (blnCheck)
                                {
                                    //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                                    strAddedItems = strAddedItems.Trim();
                                    var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                                    //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");

                                    Guid FBId = new Guid();
                                    if (DtFBUser.Rows.Count == 0)
                                    {
                                        clsFB FBSave = new clsFB();
                                        FBSave.BarAvordId = BarAvordId;
                                        FBSave.Shomareh = strAddedItems.Trim();
                                        FBSave.BahayeVahedZarib = 0;
                                        context.FBs.Add(FBSave);
                                        context.SaveChanges();
                                        FBId = FBSave.ID;
                                    }
                                    else
                                        FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());



                                    long lngShomareh = RM.Shomareh;
                                    //var varRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == strShomareh1).OrderBy(x => x.Shomareh).ToList();
                                    //DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                                    //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");

                                    //Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                                    //var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID).ToList();

                                    List<clsRizMetreUsers> lstRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                                    if (lstRizMetreUsers.Count != 0)
                                    {
                                        context.RizMetreUserses.RemoveRange(lstRizMetreUsers);
                                        context.SaveChanges();
                                    }

                                    //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
                                }
                                break;
                            }
                        case "9":
                            {
                                string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');
                                string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
                                string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
                                string strConditionOp = strCondition.Replace("x", Tool.Trim()).Replace("y", Arz.Trim());
                                StringToFormula StringToFormula = new StringToFormula();
                                bool blnCheck = StringToFormula.RelationalExpression2(strConditionOp);
                                if (blnCheck)
                                {
                                    strAddedItems = strAddedItems.Trim();
                                    var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                    DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);


                                    Guid FBId = new Guid();
                                    if (DtFBUser.Rows.Count == 0)
                                    {
                                        clsFB FBSave = new clsFB();
                                        FBSave.BarAvordId = BarAvordId;
                                        FBSave.Shomareh = strAddedItems.Trim();
                                        FBSave.BahayeVahedZarib = 0;
                                        context.FBs.Add(FBSave);
                                        context.SaveChanges();
                                        FBId = FBSave.ID;
                                    }
                                    else
                                        FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());


                                    long lngShomareh = RM.Shomareh;
                                    List<clsRizMetreUsers> lstRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                                    if (lstRizMetreUsers.Count != 0)
                                    {
                                        context.RizMetreUserses.RemoveRange(lstRizMetreUsers);
                                        context.SaveChanges();
                                    }
                                    //string strResult = DeleteSubItemsAddingToFB(long.Parse(DtItemsAddingToFB.Rows[i]["ID"].ToString()), Ertefa, RizMetreId, BarAvordId, strAddedItems);
                                }
                                break;
                            }
                        case "12":
                            {
                                string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');
                                string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
                                string[] strConditionSplit = strCondition.Split("_");
                                string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
                                string strFinalWorking = DtItemsAddingToFB.Rows[i]["FinalWorking"].ToString();
                                DataTable DtRizMetreUserses = new DataTable();
                                string strForItem = "";
                                string strUseItem = "";
                                string strItemFBShomareh = ItemShomareh.Trim();

                                Guid guFBId = currentFBId;
                                strUseItem = ItemShomareh.Trim();
                                strForItem = DtItemsAddingToFB.Rows[i]["UseItemForAdd"].ToString().Trim();

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
                                        long lngItemsHasCondition_ConditionContextId = long.Parse(DtItemsAddingToFB.Rows[i]["ItemsHasCondition_ConditionContextId"].ToString());

                                        if (strFinalWorking != "")
                                        {
                                            strFinalWorking = strFinalWorking.Replace("z", RM.Ertefa != null ? RM.Ertefa.Value.ToString().Trim() : "");
                                            string strItemShomareh = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
                                            strAddedItems = strAddedItems.Trim();
                                            var varFBUsersAdded = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                                            DataTable DtFBUser = clsConvert.ToDataTable(varFBUsersAdded);

                                            Guid FBId = new Guid();
                                            if (DtFBUser.Rows.Count != 0)
                                                FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                                            long lngShomareh = RM.Shomareh;

                                            List<clsRizMetreUsers> lstRizMetreUsers = context.RizMetreUserses.Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == lngShomareh).OrderBy(x => x.Shomareh).ToList();

                                            if (lstRizMetreUsers.Count != 0)
                                            {
                                                context.RizMetreUserses.RemoveRange(lstRizMetreUsers);
                                                context.SaveChanges();
                                            }
                                        }
                                    }

                                }

                                break;
                            }


                    }
                }
            }
            return new JsonResult("OK_آیتم های انتخابی بدرستی حذف گردیدند");
            //return "OKآیتم های انتخابی بدرستی حذف گردیدند";
        }
        catch (Exception)
        {
            return new JsonResult("NOK");
            //return "NOK";
        }
    }

    public string DeleteSubItemsAddingToFB(long Id, string Ertefa, Guid RizMetreId, Guid BarAvordId, string ItemShomareh)
    {
        int LevelNumber = 1;
        var varItemsAddingToFB = context.SubItemsAddingToFBs.Where(x => x.ItemsAddingToFBId == Id).ToList();
        DataTable DtItemsAddingToFB = clsConvert.ToDataTable(varItemsAddingToFB);
        //DataTable DtItemsAddingToFB = clsItemsAddingToFB.ListSubItemsAddingToFBWithParameter("ItemsAddingToFBId=" + Id);
        for (int i = 0; i < DtItemsAddingToFB.Rows.Count; i++)
        {
            string strCondition = DtItemsAddingToFB.Rows[i]["Condition"].ToString().Trim();
            string strAddedItems = DtItemsAddingToFB.Rows[i]["AddedItems"].ToString().Trim();
            string strFinalWorking = DtItemsAddingToFB.Rows[i]["FinalWorking"].ToString();
            string strConditionOp = strCondition.Replace("x", Ertefa.Trim());
            string[] strFieldsAdding = DtItemsAddingToFB.Rows[i]["FieldsAdding"].ToString().Split(',');

            var varRizMetreUsersCurrent = (from RizMetreUsers in context.RizMetreUserses
                                           join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                               RizMetreUsers.Des,
                                               RizMetreUsers.FBId,
                                               RizMetreUsers.OperationsOfHamlId,
                                               RizMetreUsers.ForItem,
                                               RizMetreUsers.Type,
                                               RizMetreUsers.UseItem,
                                               FB.BarAvordId
                                           }).Where(x => x.ID == RizMetreId).OrderBy(x => x.Shomareh).ToList();
            DataTable DtRizMetreUsersCurrent = clsConvert.ToDataTable(varRizMetreUsersCurrent);
            //DataTable DtRizMetreUsersCurrent = clsRizMetreUserss.RizMetreUsersesListWithParameter("clsRizMetreUserss.Id=" + RizMetreId);

            StringToFormula StringToFormula = new StringToFormula();
            bool blnCheck = StringToFormula.RelationalExpression(strConditionOp);
            if (blnCheck)
            {
                strFinalWorking = strFinalWorking.Replace("x", Ertefa.Trim());
                decimal dFinalWorking = decimal.Parse(StringToFormula.Eval(strFinalWorking).ToString());

                //clsOperationItemsFB OperationItemsFB = new clsOperationItemsFB();
                strAddedItems = strAddedItems.Trim();
                var varFBUser = context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strAddedItems).ToList();
                DataTable DtFBUser = clsConvert.ToDataTable(varFBUser);

                //DataTable DtFBUser = clsOperationItemsFB.FBListWithParameter("BarAvordId=" + BarAvordId + " and Shomareh='" + strAddedItems.Trim() + "'");
                //int FBId = 0;
                //if (DtFBUser.Rows.Count == 0)
                //    FBId = OperationItemsFB.SaveFB(BarAvordId, strAddedItems.Trim(), 0);
                //else
                //    FBId = int.Parse(DtFBUser.Rows[0]["Id"].ToString());

                Guid FBId = new Guid();
                if (DtFBUser.Rows.Count == 0)
                {
                    clsFB FBSave = new clsFB();
                    FBSave.BarAvordId = BarAvordId;
                    FBSave.Shomareh = strAddedItems.Trim();
                    FBSave.BahayeVahedZarib = 0;
                    context.FBs.Add(FBSave);
                    context.SaveChanges();
                    FBId = FBSave.ID;
                    //intFBId = OperationItemsFB.SaveFB(int.Parse(DtBA.Rows[0]["Id"].ToString()), Dr[idr]["AddedItems"].ToString().Trim() + "A", dPercent);
                }
                else
                    FBId = Guid.Parse(DtFBUser.Rows[0]["ID"].ToString());

                int strShomareh1 = int.Parse(DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString());
                var varRizMetreUsers = (from RizMetreUsers in context.RizMetreUserses
                                        join FB in context.FBs on RizMetreUsers.FBId equals FB.ID
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
                                            RizMetreUsers.Des,
                                            RizMetreUsers.FBId,
                                            RizMetreUsers.OperationsOfHamlId,
                                            RizMetreUsers.ForItem,
                                            RizMetreUsers.Type,
                                            RizMetreUsers.UseItem,
                                            FB.BarAvordId
                                        }).Where(x => x.FBId == FBId && x.ForItem == ItemShomareh && x.Shomareh == strShomareh1).OrderBy(x => x.Shomareh).ToList();
                DataTable DtRizMetreUsers = clsConvert.ToDataTable(varRizMetreUsers);
                //DataTable DtRizMetreUsers = clsRizMetreUserss.RizMetreUsersesListWithParameter("FBId=" + FBId + " and ForItem='" + ItemShomareh + "' and clsRizMetreUserss.Shomareh='" + DtRizMetreUsersCurrent.Rows[0]["Shomareh"].ToString() + "'");
                Guid guID = Guid.Parse(DtRizMetreUsers.Rows[0]["ID"].ToString());
                var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.ID == guID).ToList();
                if (clsRizMetreUsers != null)
                {
                    context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                    context.SaveChanges();
                }
                //clsRizMetreUserss.Delete("clsRizMetreUserss.Id=" + DtRizMetreUsers.Rows[0]["Id"].ToString());
            }
        }
        return "OK";
    }

    public JsonResult DeleteItemsFBShomarehValueShomareh([FromBody] DeleteItemsFBShomarehValueShomarehInputDto request)
    {
        try
        {
            int LevelNumber = request.LevelNumber;
            Guid BarAvordId = request.BarAvordId;
            string ItemsFBShomareh = request.ItemsFBShomareh.Trim();
            int Type = request.Type;

            var varItemsFBShomarehValueShomareh = context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == ItemsFBShomareh && x.BarAvordId == BarAvordId && x.Type == Type).ToList();
            DataTable DtItemsFBShomarehValueShomareh = clsConvert.ToDataTable(varItemsFBShomarehValueShomareh);


            //DataTable DtItemsFBShomarehValueShomareh = clsItemsFBShomarehValueShomareh.ListWithParameter("FBShomareh='" + ItemsFBShomareh.Trim() + "' and BarAvordId=" + BarAvordId + " and Type=" + Type);
            if (DtItemsFBShomarehValueShomareh.Rows.Count != 0)
            {
                string strShomareh = DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim();
                var clsRizMetreUsers = context.RizMetreUserses.Where(x => x.FB.Shomareh == strShomareh && x.FB.BarAvordId == BarAvordId
                && x.ForItem == ItemsFBShomareh && x.LevelNumber == LevelNumber).ToList();

                if (clsRizMetreUsers.Count != 0)
                {
                    context.RizMetreUserses.RemoveRange(clsRizMetreUsers);
                    context.SaveChanges();
                }
                //clsRizMetreUserss.Delete("clsFB.Shomareh='" + DtItemsFBShomarehValueShomareh.Rows[0]["GetValuesShomareh"].ToString().Trim() + "' and BarAvordId=" + BarAvordId + " and ForItem='" + ItemsFBShomareh.Trim() + "'");
                var varItemsFBShomarehValueShomarehs = context.ItemsFBShomarehValueShomarehs.Where(x => x.FBShomareh == ItemsFBShomareh && x.BarAvordId == BarAvordId && x.Type == Type).ToList();
                if (varItemsFBShomarehValueShomarehs.Count != 0)
                {
                    context.ItemsFBShomarehValueShomarehs.RemoveRange(varItemsFBShomarehValueShomarehs);
                    context.SaveChanges();
                }

                //clsItemsFBShomarehValueShomareh.Delete("FBShomareh='" + ItemsFBShomareh.Trim() + "' and BarAvordId=" + BarAvordId + " and Type=" + Type);
            }
            return new JsonResult("OK");
            //return "OK";
        }
        catch (Exception)
        {
            return new JsonResult("NOK");
            //return "NOK";
        }
    }


    // GET: RizMetreUser/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: RizMetreUser/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: RizMetreUser/Create
    [HttpPost]
    public ActionResult Create(FormCollection collection)
    {
        try
        {
            // TODO: Add insert logic here

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    // GET: RizMetreUser/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: RizMetreUser/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection)
    {
        try
        {
            // TODO: Add update logic here

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    public ActionResult DeleteRizMetre([FromBody] DeleteRizMetreInputDto request)
    {
        clsRizMetreUsers? entity = context.RizMetreUserses.Find(request.Id);
        if (entity != null)
        {
            context.RizMetreUserses.Remove(entity);
            // context.SaveChanges();


            /////////////
            //////////////
            //////////////
            ///
            long OperationId = request.OperationId;
            NoeFehrestBaha NoeFB = request.NoeFB;
            int Year = request.Year;
            Guid BarAvordId = request.BarAvordUserId;
            Guid FBId = request.FBId;

            clsOperation_ItemsFB operation_ItemsFB = context.Operation_ItemsFBs.First(x => x.OperationId == OperationId);


            List<ItemsFieldsDto> ItemsField = (from ItemF in context.ItemsFieldses
                                               join OpItemFB in context.Operation_ItemsFBs
                                               on ItemF.ItemShomareh equals OpItemFB.ItemsFBShomareh
                                               select new ItemsFieldsDto
                                               {
                                                   ItemShomareh = ItemF.ItemShomareh,
                                                   NoeFB = ItemF.NoeFB,
                                                   IsEnteringValue = ItemF.IsEnteringValue,
                                                   Vahed = ItemF.Vahed,
                                                   FieldType = ItemF.FieldType,
                                                   OperationId = OpItemFB.OperationId
                                               }).Where(x => x.OperationId == OperationId && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();

            List<ItemsHasConditionConditionContextForCheckOperationDto> lstItemsHasCondition = _context.ItemsHasCondition_ConditionContexts
                .Where(cc => cc.Year == Year)
                .Join(_context.ItemsHasConditionAddedToFBs,
                cc => cc.Id,
                fb => fb.ItemsHasCondition_ConditionContextId,
        (cc, fb) => new { cc, fb })
            .Join(_context.ItemsHasConditions,
        temp => temp.cc.ItemsHasConditionId,
        ihc => ihc.Id,
        (temp, ihc) => new { temp.cc, temp.fb, ihc })
            .Join(_context.Operation_ItemsFBs,
        temp => temp.ihc.ItemFBShomareh,
        ofb => ofb.ItemsFBShomareh,
        (temp, ofb) => new ItemsHasConditionConditionContextForCheckOperationDto
        {
            Id = temp.cc.Id,
            ItemsHasConditionId = temp.cc.ItemsHasConditionId,
            ConditionContextId = temp.cc.ConditionContextId,
            HasEnteringValue = temp.cc.HasEnteringValue,
            Des = temp.cc.Des,
            DefaultValue = temp.cc.DefaultValue,
            IsShow = temp.cc.IsShow,
            ParentId = temp.cc.ParentId,
            MoveToRel = temp.cc.MoveToRel,
            ViewCheckAllRecords = temp.cc.ViewCheckAllRecords,
            StepChange = temp.cc.StepChange,
            Meghdar = temp.fb.Meghdar,
            Meghdar2 = temp.fb.Meghdar2,
            FBShomareh = temp.fb.FBShomareh,
            ConditionGroupId = temp.fb.ConditionGroupId,
            BarAvordId = temp.fb.BarAvordId,
            OperationId = ofb.OperationId
        }).Where(x => x.OperationId == OperationId && x.BarAvordId == BarAvordId).ToList();


            var ItemsAddingToFBs = _context.ItemsAddingToFBs.Where(x => x.Year == Year).Select(x => new
            {
                x.ItemsHasCondition_ConditionContextId,
                x.AddedItems,
                x.Condition,
                x.FinalWorking,
                x.ConditionType,
                x.DesOfAddingItems,
                x.UseItemForAdd,
                x.FieldsAdding,
                x.CharacterPlus
            }).ToList();

            foreach (var ItemHasCon in lstItemsHasCondition)
            {
                List<ItemsAddingToFBForCheckOperationDto> lstItemsAddingToFBForCheckOperation = ItemsAddingToFBs.Where(x => x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id).Select(x => new
                ItemsAddingToFBForCheckOperationDto
                {
                    ItemsHasCondition_ConditionContextId = x.ItemsHasCondition_ConditionContextId,
                    AddedItems = x.AddedItems,
                    Condition = x.Condition,
                    FinalWorking = x.FinalWorking,
                    ConditionType = x.ConditionType,
                    DesOfAddingItems = x.DesOfAddingItems,
                    UseItemForAdd = x.UseItemForAdd,
                    FieldsAdding = x.FieldsAdding,
                    CharacterPlus = x.CharacterPlus
                }).ToList();

                CheckOperationConditions checkOperationConditions = new CheckOperationConditions();
                foreach (var itemsAddingToFBForCheckOperation in lstItemsAddingToFBForCheckOperation)
                {
                    checkOperationConditions.fnCheckOperationConditionForDelete(_context, itemsAddingToFBForCheckOperation, ItemsField, ItemHasCon, FBId, entity);
                }
            }

            context.SaveChanges();

            //////////
            ///////////////
            //////////////

            return new JsonResult("OK_" + operation_ItemsFB.ItemsFBShomareh);
        }
        else
            return new JsonResult("NOK");
    }
    public ActionResult DeleteRelRizMetre([FromBody] DeleteRelRizMetreInputDto request)
    {
        Guid BarAvordId = request.BarAvordUserId;
        long OperationId = request.OperationId;

        string ItemFBShomareh = "";
        clsOperation_ItemsFB? Operation_ItemsFB = context.Operation_ItemsFBs.FirstOrDefault(x => x.OperationId == OperationId);
        if (Operation_ItemsFB != null)
        {
            ItemFBShomareh = Operation_ItemsFB.ItemsFBShomareh;
        }

        List<clsRizMetreUsers> lstAllRizMetre = context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId && x.ForItem.Substring(0, 6) == ItemFBShomareh).ToList();
        clsRizMetreUsers? entity = context.RizMetreUserses.Find(request.Id);
        if (entity != null)
        {
            context.RizMetreUserses.Remove(entity);
            // context.SaveChanges();

            if (entity.ConditionContextRel != null)
            {
                foreach (var RM in lstAllRizMetre)
                {
                    string[] ConditionContextRel = entity.ConditionContextRel.Split(",");
                    foreach (var conditionContextRel in ConditionContextRel)
                    {
                        if (conditionContextRel != "")
                        {
                            long? lngconditionContextRel = long.Parse(conditionContextRel);
                            if (RM.ConditionContextId == lngconditionContextRel && RM.Shomareh == entity.Shomareh)
                            {
                                context.RizMetreUserses.Remove(RM);
                            }
                        }

                    }
                }
            }
            /////////////
            //////////////
            //////////////
            ///


            context.SaveChanges();

            //////////
            ///////////////
            //////////////

            return new JsonResult("OK");
        }
        else
            return new JsonResult("NOK");
    }

    // POST: RizMetreUser/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
        try
        {
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }
}
