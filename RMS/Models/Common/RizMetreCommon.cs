using Microsoft.EntityFrameworkCore;
using System.Data;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RMS.Models.Common.Dto;

namespace RMS.Models.Common
{
    public class RizMetreCommon
    {
        public string UpdateRizMetreFromAddedItems(UpdateRizMetreUsersInputDto request, ApplicationDbContext context)
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
                    }
                }
                if (blnCheckUpdate1)
                {
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

                    List<ItemsHasConditionConditionContextForCheckOperationDto> lstItemsHasCondition = context.ItemsHasCondition_ConditionContexts
                        .Where(cc => cc.Year == Year)
                    .Join(context.ItemsHasConditionAddedToFBs,
                        cc => cc.Id,
                        fb => fb.ItemsHasCondition_ConditionContextId,
                (cc, fb) => new { cc, fb })
                    .Join(context.ItemsHasConditions,
                temp => temp.cc.ItemsHasConditionId,
                ihc => ihc.Id,
                (temp, ihc) => new { temp.cc, temp.fb, ihc })
                    .Join(context.Operation_ItemsFBs,
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


                    var ItemsAddingToFBs = context.ItemsAddingToFBs.Where(x => x.Year == Year).Select(x => new
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
                            checkOperationConditions.fnCheckOperationConditionForUpdate(context, itemsAddingToFBForCheckOperation, ItemsField,
                                ItemHasCon, FBId, RizMetre, OldRizMetre,LevelNumber,Year,NoeFB);
                        }
                    }

                    //////////
                    ///

                    return "Ok";
                }
                return "OK";
            }
            catch (Exception)
            {
                return ("NOK");
            }
        }

        //دریافت آیتم ها برای اضافه بهای صعوبت
        public List<RizMetreForGetAndShowAddItemsDto> GetAndShowAddItemsForSoubat(GetAndShowAddItemsInputForSoubatDto request, ApplicationDbContext _context)
        {
            string strItemsFBShomareh = request.ShomarehFB;
            Guid BarAvordId = request.BarAvordUserId;
            long[] lngConditionGroupId = request.ConditionGroupId;
            int LevelNumber = request.LevelNumber;
            NoeFehrestBaha NoeFB = request.NoeFB;
            int Year = request.Year;

            var varFB = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemsFBShomareh).ToList();
            DataTable DtFB = clsConvert.ToDataTable(varFB);
            DataTable DtItemsAddingToFB = new DataTable();

            List<RizMetreForGetAndShowAddItemsDto> lstFbs = new List<RizMetreForGetAndShowAddItemsDto>();

            foreach (var ConditionGroupId in lngConditionGroupId)
            {
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
                                                     }).Where(x => x.FBShomareh.Substring(0, 6) == strItemsFBShomareh && x.BarAvordId == BarAvordId && x.ConditionGroupId == ConditionGroupId).ToList();
                DataTable DtItemsHasConditionAddedToFB = clsConvert.ToDataTable(varItemsHasConditionAddedToFB);

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

                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh)
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
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                var RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim()
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
                                            decimal dPercent = decimal.Parse(Dr[0]["FinalWorking"].ToString());
                                            string strStatus = dPercent > 0 ? "B" : "e";
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strStatus).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                clsFB? Fb = _context.FBs.Where(x => x.ID == guFBUsersAddedId).FirstOrDefault();
                                                if (Fb != null)
                                                {
                                                    List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
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
                                    case "15":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded + strCharacterPlus).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim() && x.LevelNumber == LevelNumber)
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
                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim() && x.LevelNumber == LevelNumber)
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
                                    case "8":
                                    case "16":
                                        {
                                            var varFBUsersAdded = _context.FBs.Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strFBShomarehAdded).ToList();
                                            DataTable DtFBUsersAdded = clsConvert.ToDataTable(varFBUsersAdded);

                                            if (DtFBUsersAdded.Rows.Count != 0)
                                            {
                                                Guid guFBUsersAddedId = Guid.Parse(DtFBUsersAdded.Rows[0]["ID"].ToString().Trim());
                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim() && x.LevelNumber == LevelNumber)
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
                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
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
                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == Fb.ID && x.ForItem == strItemsFBShomareh.Trim()).OrderBy(x => x.Shomareh).OrderBy(x => x.Shomareh).Select(x => new RizMetreForGetAndShowAddItemsDto
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
                                                List<RizMetreForGetAndShowAddItemsDto> RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim()).OrderBy(x => x.Shomareh)
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
                                                var RizMetre = _context.RizMetreUserses.Where(x => x.FBId == guFBUsersAddedId && x.ForItem == strItemsFBShomareh.Trim()
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
            }
            lstFbs.OrderBy(x => x.Shomareh);
            //var Result = new
            //{
            //    lst,
            //    //lstItemFBShomarehForGet
            //};

            return lstFbs;
        }

    }
}
