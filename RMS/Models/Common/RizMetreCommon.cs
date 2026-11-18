using Microsoft.EntityFrameworkCore;
using RMS.Controllers.AbnieFani.Dto;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common.Dto;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using System.Data;
using static RMS.Models.Common.EnumForEntity;
using static Stimulsoft.Report.StiRecentConnections;

namespace RMS.Models.Common;

public class RizMetreCommon
{
    public async Task<string> UpdateRizMetreFromAddedItems(UpdateRizMetreUsersInputDto request, ApplicationDbContext context)
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
            NoeFehrestBaha NoeFB = request.NoeFBId;
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
                        await checkOperationConditions.fnCheckOperationConditionForUpdate(context, itemsAddingToFBForCheckOperation, ItemsField,
                             ItemHasCon, FBId, RizMetre, OldRizMetre, LevelNumber, Year, NoeFB,new Guid());
                    }
                }

                context.SaveChanges();

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
        string baseItemNumber = strItemsFBShomareh.Trim();
        Guid BarAvordId = request.BarAvordUserId;
        long[] lngConditionGroupId = request.ConditionGroupId ?? Array.Empty<long>();
        int LevelNumber = request.LevelNumber;
        NoeFehrestBaha NoeFB = request.NoeFB;
        int Year = request.Year;

        if (lngConditionGroupId.Length == 0)
            return new List<RizMetreForGetAndShowAddItemsDto>();

        var currentFb = _context.FBs.AsNoTracking()
            .Where(x => x.BarAvordId == BarAvordId && x.Shomareh == strItemsFBShomareh)
            .Select(x => new { x.Shomareh })
            .FirstOrDefault();

        var conditionRows = (
            from added in _context.ItemsHasConditionAddedToFBs
            join conditionContext in _context.ItemsHasCondition_ConditionContexts
                on added.ItemsHasCondition_ConditionContextId equals conditionContext.Id
            join condition in _context.ItemsHasConditions
                on conditionContext.ItemsHasConditionId equals condition.Id
            where condition.Year == Year
                && added.FBShomareh.Substring(0, 6) == strItemsFBShomareh
                && added.BarAvordId == BarAvordId
                && lngConditionGroupId.Contains(added.ConditionGroupId)
            select new
            {
                added.ID,
                added.FBShomareh,
                added.BarAvordId,
                added.Meghdar,
                added.ItemsHasCondition_ConditionContextId,
                added.ConditionGroupId,
                ItemFBShomareh = condition.ItemFBShomareh,
                conditionContext.ViewCheckAllRecords,
                conditionContext.EnableEditing,
                conditionContext.EnableDeleting
            }).ToList();

        var contextIds = conditionRows
            .Select(x => x.ItemsHasCondition_ConditionContextId)
            .Distinct()
            .ToList();

        // آیتم‌های افزوده‌شده و شرح فهرست‌بها فقط یک‌بار خوانده می‌شوند.
        var additions = (
            from adding in _context.ItemsAddingToFBs.AsNoTracking()
            join fehrest in _context.FehrestBahas.AsNoTracking()
                on adding.AddedItems.Trim() equals fehrest.Shomareh.Trim()
            where contextIds.Contains(adding.ItemsHasCondition_ConditionContextId)
                && fehrest.Sal == Year && fehrest.NoeFB == NoeFB
            select new
            {
                adding.Id,
                adding.ItemsHasCondition_ConditionContextId,
                adding.AddedItems,
                adding.Condition,
                adding.FinalWorking,
                adding.ConditionType,
                adding.DesOfAddingItems,
                adding.UseItemForAdd,
                adding.FieldsAdding,
                adding.CharacterPlus,
                fehrest.Sharh,
                fehrest.Shomareh
            }).ToList();

        // شماره تمام FBهای موردنیاز را پیش از ورود به حلقه استخراج می‌کنیم.
        var candidateNumbers = new HashSet<string>();
        foreach (var adding in additions)
        {
            string itemNumber = (adding.AddedItems ?? string.Empty).Trim();
            string suffix = (adding.CharacterPlus ?? string.Empty).Trim();
            string type = adding.ConditionType.ToString();

            switch (type)
            {
                case "1":
                case "4":
                case "5":
                case "10":
                case "14":
                case "15":
                    candidateNumbers.Add(itemNumber + suffix);
                    break;
                case "3":
                    candidateNumbers.Add(itemNumber + "B");
                    candidateNumbers.Add(itemNumber + "e");
                    break;
                case "11":
                    candidateNumbers.Add(baseItemNumber + suffix);
                    break;
                case "2":
                case "6":
                case "8":
                case "9":
                case "12":
                case "13":
                case "16":
                    candidateNumbers.Add(itemNumber);
                    break;
            }
        }

        var fbRows = candidateNumbers.Count == 0
            ? new List<clsFB>()
            : _context.FBs.AsNoTracking()
                .Where(x => x.BarAvordId == BarAvordId && candidateNumbers.Contains(x.Shomareh))
                .ToList();

        // در صورت وجود شماره تکراری، همان اولین رکوردی انتخاب می‌شود که قبلاً First/Rows[0] برمی‌گرداند.
        var fbByNumber = fbRows
            .GroupBy(x => x.Shomareh)
            .ToDictionary(g => g.Key, g => g.First());

        var fbIds = fbRows.Select(x => x.ID).Distinct().ToList();
        var allRizMetres = fbIds.Count == 0
            ? new List<clsRizMetreUsers>()
            : _context.RizMetreUserses.AsNoTracking()
                .Where(x => fbIds.Contains(x.FBId))
                .OrderBy(x => x.Shomareh)
                .ToList();

        var additionsByContext = additions
            .GroupBy(x => x.ItemsHasCondition_ConditionContextId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var result = new List<RizMetreForGetAndShowAddItemsDto>();

        List<RizMetreForGetAndShowAddItemsDto> GetRizMetres(
            Guid fbId, string forItem, bool filterLevel, int level,
            bool filterUseItem = false, string useItem = "",
            bool canDelete = false, bool canEdit = false, string displayedItemNumber = "")
        {
            return allRizMetres
                .Where(x => x.FBId == fbId
                    && x.ForItem == forItem
                    && (!filterLevel || x.LevelNumber == level)
                    && (!filterUseItem || x.UseItem == useItem))
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
                    ItemFBShomareh = displayedItemNumber,
                    HasDelButton = canDelete,
                    HasEditButton = canEdit
                }).ToList();
        }

        foreach (var groupId in lngConditionGroupId)
        {
            var rowsForGroup = conditionRows
                .Where(x => x.ConditionGroupId == groupId)
                .ToList();

            foreach (var conditionRow in rowsForGroup)
            {
                if (!additionsByContext.TryGetValue(conditionRow.ItemsHasCondition_ConditionContextId, out var contextAdditions))
                    continue;

                bool canEdit = conditionRow.EnableEditing == true;
                bool canDelete = conditionRow.EnableDeleting == true;
                decimal amount = conditionRow.Meghdar;

                foreach (var adding in contextAdditions)
                {
                    string addedItemNumber = (adding.AddedItems ?? string.Empty).Trim();
                    string characterPlus = (adding.CharacterPlus ?? string.Empty).Trim();
                    string conditionType = adding.ConditionType.ToString();

                    if (conditionType == "1" || conditionType == "11")
                    {
                        // حفظ رفتار قبلی: در این دو حالت، لیست آیتم‌های نمایشی بازنویسی می‌شود.
                        // این فهرست در خروجی نهایی استفاده نمی‌شد، اما برای حفظ مسیر اجرا نگه داشته شده است.
                    }

                    fbByNumber.TryGetValue(
                        conditionType == "11" ? baseItemNumber + characterPlus :
                        conditionType == "1" || conditionType == "4" || conditionType == "5" ||
                        conditionType == "10" || conditionType == "14" || conditionType == "15"
                            ? addedItemNumber + characterPlus
                        : conditionType == "3"
                            ? addedItemNumber + (decimal.Parse(adding.FinalWorking.ToString()) > 0 ? "B" : "e")
                            : addedItemNumber,
                        out var fb);

                    if (fb == null)
                        continue;

                    List<RizMetreForGetAndShowAddItemsDto> rizMetres;
                    switch (conditionType)
                    {
                        case "1":
                        {
                            string finalWorking = adding.FinalWorking.ToString().Replace("x", amount.ToString().Trim());
                            var formula = new StringToFormula();
                            _ = decimal.Parse(formula.Eval(finalWorking).ToString());
                            rizMetres = GetRizMetres(fb.ID, baseItemNumber, false, LevelNumber,
                                canDelete: canDelete, canEdit: canEdit, displayedItemNumber: addedItemNumber);
                            if (rizMetres.Count > 0) result = rizMetres;
                            break;
                        }
                        case "2":
                        case "13":
                            rizMetres = GetRizMetres(fb.ID, baseItemNumber, true, LevelNumber,
                                canDelete: canDelete, canEdit: canEdit, displayedItemNumber: addedItemNumber);
                            if (rizMetres.Count > 0) result = rizMetres;
                            break;
                        case "3":
                            rizMetres = GetRizMetres(fb.ID, baseItemNumber, false, LevelNumber,
                                canDelete: canDelete, canEdit: canEdit, displayedItemNumber: addedItemNumber);
                            if (rizMetres.Count > 0) result = rizMetres;
                            break;
                        case "4":
                        case "5":
                        case "14":
                        case "15":
                        case "6":
                        case "8":
                        case "16":
                            rizMetres = GetRizMetres(fb.ID, baseItemNumber, true, LevelNumber,
                                canDelete: canDelete, canEdit: canEdit, displayedItemNumber: addedItemNumber);
                            if (rizMetres.Count > 0) result = rizMetres;
                            break;
                        case "9":
                        {
                            string forItem = string.IsNullOrWhiteSpace(adding.UseItemForAdd)
                                ? (currentFb?.Shomareh ?? string.Empty).Trim()
                                : adding.UseItemForAdd.Trim();
                            string useItem = string.IsNullOrWhiteSpace(adding.UseItemForAdd)
                                ? string.Empty
                                : (currentFb?.Shomareh ?? string.Empty).Trim();
                            rizMetres = GetRizMetres(fb.ID, forItem, true, LevelNumber, true, useItem,
                                canDelete, canEdit, addedItemNumber);
                            if (rizMetres.Count > 0) result.AddRange(rizMetres);
                            break;
                        }
                        case "10":
                            rizMetres = GetRizMetres(fb.ID, baseItemNumber, false, LevelNumber,
                                canDelete: canDelete, canEdit: canEdit, displayedItemNumber: addedItemNumber);
                            if (rizMetres.Count > 0) result = rizMetres;
                            break;
                        case "11":
                            rizMetres = GetRizMetres(fb.ID, baseItemNumber, false, LevelNumber,
                                canDelete: canDelete, canEdit: canEdit, displayedItemNumber: addedItemNumber);
                            if (rizMetres.Count > 0) result = rizMetres;
                            break;
                        case "12":
                            rizMetres = GetRizMetres(fb.ID, baseItemNumber, false, LevelNumber,
                                canDelete: canDelete, canEdit: canEdit, displayedItemNumber: addedItemNumber);
                            if (rizMetres.Count > 0) result.AddRange(rizMetres);
                            break;
                    }
                }
            }
        }

        return result;
    }

    ////////
    ////////محاسبه کل فصل
    /////
    public static async Task<ResultCalculateFaslDto> fnCalculateFasl(CalculateFaslDto request, ApplicationDbContext _context)
    {
        Guid BarAvordId = request.BarAvordId;
        Guid FBId = request.FBId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;
        string Code = request.Code;

        decimal SumMeghdarJoz = await _context.FBs.AsNoTracking()
            .Where(x => x.ID == FBId)
            .Select(x => x.SumRizMetre ?? 0m)
            .FirstOrDefaultAsync();
        /////
        //////
        //////محاسبه کل فصل
        ///
        //decimal SumMeghdarFasl = 0;
        List<clsFehrestBahaItemsReplace> lstFBItemReplaces = _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToList();

        var lstFehrestBahas = (from FehrestBaha in _context.FehrestBahas
                               join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                               on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
                               from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
                               where FehrestBaha.Sal == Year && FehrestBaha.NoeFB == NoeFBId
                               select new
                               {
                                   Id = FehrestBaha.Id,
                                   BahayeVahed = FehrestBaha.BahayeVahed,
                                   Vahed = FehrestBaha.Vahed,
                                   Sharh = FehrestBaha.Sharh,
                                   ShomarehMain = FehrestBaha.Shomareh,
                                   Shomareh = FehrestBahaItemsReplace != null && FehrestBahaItemsReplace.FBShomarehNew != null
                                                             ? FehrestBahaItemsReplace.FBShomarehNew
                                                             : FehrestBaha.Shomareh,
                                   Sal = FehrestBaha.Sal,
                                   NoeFB = FehrestBaha.NoeFB,
                               }).ToList();

        // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
        //var fbItems = _context.FBs
        //    .Where(f => f.BarAvordId == BarAvordId && f.NoeFBId == NoeFBId)
        //    .GroupBy(f => f.Shomareh)
        //    .Select(g => g.FirstOrDefault())
        //    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

        var fbItems = await (from fb in _context.FBs
                             join fehrestbaha in _context.FehrestBahas
                             on fb.Shomareh.Substring(0, 6) equals fehrestbaha.Shomareh.Trim()
                             where fb.BarAvordId == BarAvordId && fb.NoeFBId == NoeFBId
                             select new FBForGetUserBarAvordDto
                             {
                                 ID = fb.ID,
                                 BahayeVahedSharh = fb.BahayeVahedSharh,
                                 Shomareh = fb.Shomareh.Substring(0, 6),
                                 BahayeVahed = fehrestbaha == null ? "0" : fehrestbaha.BahayeVahed != null ? fehrestbaha.BahayeVahed.Trim() : "0",
                                 Vahed = fehrestbaha.Vahed,
                                 //BahayeVahedZarib = fb.BahayeVahedZarib,
                                 BahayeVahedNew = fb.BahayeVahedNew,
                                 ItemZarib = fb.ItemZarib,
                                 SumRizMetre = fb.SumRizMetre
                             })
        .GroupBy(f => f.Shomareh)
        .Select(g => g.FirstOrDefault())
        .ToListAsync();


        //foreach (var item in fbItems)
        //{
        //    foreach (var fbItemReplace in lstFBItemReplaces)
        //    {
        //        if (item.Shomareh == fbItemReplace.FBShomarehMain)
        //        {
        //            item.Shomareh = fbItemReplace.FBShomarehNew;
        //        }
        //    }
        //}


        var replaceDictionary = lstFBItemReplaces
           .GroupBy(x => x.FBShomarehMain)
           .ToDictionary(
               x => x.Key,
               x => x.Last().FBShomarehNew
           );

        foreach (var item in fbItems)
        {
            if (replaceDictionary.TryGetValue(item.Shomareh, out var newShomareh))
            {
                item.Shomareh = newShomareh;
            }
        }

        // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
        var userBarAvordOutPut = lstFehrestBahas //_context.FehrestBahas
            .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId && fehrest.Shomareh.StartsWith(Code))
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
                ItemZarib = x.fbItem != null ? x.fbItem.ItemZarib : 1,
                Meghdar = x.fbItem?.SumRizMetre ?? 0m,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
            }).ToList();

        List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFilter = userBarAvordOutPut.Where(x => x.FBId != null).ToList();

        //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordId);
        //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordId);

        //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
        //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

        ////محاسبه ضرایب
        RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
        {
            BarAvordId = BarAvordId,
            NoeFBId = NoeFBId,
            Year = Year
        };
        ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
        decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
        decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

        ///
        ///

        decimal JameFaslInZarib = 0;

        decimal JameFasl = 0;

        decimal JameFaslWithoutBahaVahed = 0;//جمع آیتم های بدون بهای واحد
        decimal JameFaslWithoutBahaVahedWithZarib = 0;//جمع آیتم های بدون بهای واحد در ضریب

        List<string> lstItemFbShomarehs = userBarAvordOutPutFilter.Select(x => x.ItemFbShomareh).Distinct().ToList();

        List<clsBahayeVahedItemsRelated> lstBVItemRelated = _context.BahayeVahedItemsRelateds
            .AsNoTracking()
            .Where(x => lstItemFbShomarehs.Contains(x.ItemShomarehRelated.ToLower().Trim()) && x.NoeFB == NoeFBId && x.Year == Year)
            .ToList();

        var relatedItemsByNumber = lstBVItemRelated
            .GroupBy(x => x.ItemShomarehRelated.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var outputItemsByNumber = userBarAvordOutPut
            .GroupBy(x => x.ItemFbShomareh.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        foreach (var item in userBarAvordOutPutFilter)
        {
            decimal Meghdar = item.Meghdar;

            decimal ItemZarib = (item.ItemZarib == null || item.ItemZarib == 0) ? 1 : item.ItemZarib.Value;

            if (Meghdar != 0)
            {
                decimal dBVahed = 0;// item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                decimal dBahayeKol = 0;

                relatedItemsByNumber.TryGetValue(item.ItemFbShomareh.Trim(), out var BVItemsRelated);
                if (BVItemsRelated != null)
                {
                    outputItemsByNumber.TryGetValue(BVItemsRelated.ItemShomareh.Trim(), out var barAvordInFehrest1);
                    if (barAvordInFehrest1 != null)
                    {
                        long BahayeVahed1 = barAvordInFehrest1.BahayeVahedNew != 0 ? barAvordInFehrest1.BahayeVahedNew : barAvordInFehrest1.BahayeVahed == null ? 0 : long.Parse(barAvordInFehrest1.BahayeVahed);
                        item.BahayeVahed = (BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                    }
                }


                item.Meghdar = Meghdar;

                ///در صورتی که آیتم بهای واحدش تغییر یابد
                if (item.BahayeVahedNew != 0)
                {
                    dBVahed = item.BahayeVahedNew;
                    dBahayeKol = Meghdar * dBVahed * ItemZarib;
                    JameFaslWithoutBahaVahed += dBahayeKol;

                    dBahayeKol = Meghdar * dBVahed;
                    item.BahayeKol = dBahayeKol;
                    JameFaslWithoutBahaVahedWithZarib += dBahayeKol * zaribBalaSari * zaribManteghe;

                }
                else
                {
                    if (item.BahayeVahed == null || item.BahayeVahed == "")
                    {
                        dBVahed = 0;
                    }
                    else
                    {
                        dBVahed = decimal.Parse(item.BahayeVahed);
                        dBahayeKol = Meghdar * dBVahed * ItemZarib;
                        item.BahayeKol = dBahayeKol;
                        JameFasl += dBahayeKol;
                        JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                    }

                }
            }
        }
        ResultCalculateFaslDto result1 = new ResultCalculateFaslDto
        {
            SumMeghdarJoz = SumMeghdarJoz,
            JameFasl = JameFasl,
            JameFaslInZarib = JameFaslInZarib,
        };
        return result1;
    }

    ////////
    ////////محاسبه فصول غیر از تجهیزکارگاه
    /////
    ///
    public static async Task<ResultCalculateBarAvordDto> fnCalculateFosoulAll(CalculateFosoulDto request, ApplicationDbContext _context)
    {
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == BarAvordId);
        if (baravordUser != null)
        {

            List<clsFehrestBahaItemsReplace> lstFBItemReplaces = _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToList();


            // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
            //var fbItems = _context.FBs
            //    .Where(f => f.BarAvordId == BarAvordId && f.NoeFBId == NoeFBId)
            //    .GroupBy(f => f.Shomareh)
            //    .Select(g => g.FirstOrDefault())
            //    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF

            //    var fbItems = await (from fb in _context.FBs
            //                         join fehrestbaha in _context.FehrestBahas
            //                         on fb.Shomareh.Substring(0, 6) equals fehrestbaha.Shomareh.Trim()
            //                         where fb.BarAvordId == BarAvordId && fb.NoeFBId == NoeFBId
            //                         select new FBForGetUserBarAvordDto
            //                         {
            //                             ID = fb.ID,
            //                             BahayeVahedSharh = fb.BahayeVahedSharh,
            //                             Shomareh = fb.Shomareh.Substring(0, 6),
            //                             BahayeVahed = fehrestbaha == null ? "0" : fehrestbaha.BahayeVahed != null ? (fb.BahayeVahedZarib * decimal.Parse(fehrestbaha.BahayeVahed.Trim() == "" ? "0" :
            //                                                          fehrestbaha.BahayeVahed)).ToString() : fehrestbaha.BahayeVahed,
            //                             Vahed = fehrestbaha.Vahed,
            //                             BahayeVahedZarib = fb.BahayeVahedZarib,
            //                             BahayeVahedNew = fb.BahayeVahedNew,
            //                             ItemZarib = fb.ItemZarib
            //                         })
            //.GroupBy(f => f.Shomareh)
            //.Select(g => g.FirstOrDefault())
            //.ToListAsync();




            var fbItems = await (
   from fb in _context.FBs

   join fehrestbaha in _context.FehrestBahas
       on fb.Shomareh.Substring(0, 6)
       equals fehrestbaha.Shomareh.Trim()
       into fehrestGroup

   from fehrestbaha in fehrestGroup
       .Where(x =>
           x.NoeFB == NoeFBId &&
           x.Sal == request.Year)
       .DefaultIfEmpty()

   where fb.BarAvordId == request.BarAvordId
         && fb.NoeFBId == NoeFBId

   select new FBForGetUserBarAvordDto
   {
       ID = fb.ID,

       BahayeVahedSharh = fb.BahayeVahedSharh,

       // شماره واقعی FB حفظ شود
       Shomareh = fb.Shomareh,

       BahayeVahed =
           fehrestbaha == null
               ? "0"
               : fehrestbaha.BahayeVahed != null
                   ? fehrestbaha.BahayeVahed
                   : "0",

       Vahed = fehrestbaha != null
           ? fehrestbaha.Vahed
           : null,

       //BahayeVahedZarib = fb.BahayeVahedZarib,

       BahayeVahedNew = fb.BahayeVahedNew,

       ItemZarib = fb.ItemZarib,
       SumRizMetre = fb.SumRizMetre
   }
)
.GroupBy(x => x.Shomareh)
.Select(g => g.First())
.ToListAsync();




            //foreach (var item in fbItems)
            //{
            //    foreach (var fbItemReplace in lstFBItemReplaces)
            //    {
            //        if (item.Shomareh == fbItemReplace.FBShomarehMain)
            //        {
            //            item.Shomareh = fbItemReplace.FBShomarehNew;
            //        }
            //    }
            //}

            var replaceDictionary = lstFBItemReplaces
           .GroupBy(x => x.FBShomarehMain)
           .ToDictionary(
               x => x.Key,
               x => x.Last().FBShomarehNew
           );

            foreach (var item in fbItems)
            {
                if (replaceDictionary.TryGetValue(item.Shomareh, out var newShomareh))
                {
                    item.Shomareh = newShomareh;
                }
            }

            var lstFehrestBahas = (from FehrestBaha in _context.FehrestBahas
                                   join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                                   on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
                                   from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
                                   where FehrestBaha.Sal == request.Year && FehrestBaha.NoeFB == NoeFBId
                                   select new
                                   {
                                       Id = FehrestBaha.Id,
                                       BahayeVahed = FehrestBaha.BahayeVahed,
                                       Vahed = FehrestBaha.Vahed,
                                       Sharh = FehrestBaha.Sharh,
                                       ShomarehMain = FehrestBaha.Shomareh,
                                       Shomareh = FehrestBahaItemsReplace != null && FehrestBahaItemsReplace.FBShomarehNew != null
                                                                 ? FehrestBahaItemsReplace.FBShomarehNew
                                                                 : FehrestBaha.Shomareh,
                                       Sal = FehrestBaha.Sal,
                                       NoeFB = FehrestBaha.NoeFB,
                                   }).ToList();


            var userBarAvordOutPut = lstFehrestBahas//_context.FehrestBahas
            .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId)
            .ToList() // انتقال به حافظه
            .GroupJoin(fbItems,
                fehrest => fehrest.Shomareh,
                fb => fb.Shomareh.Substring(0, 6),
                (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
            .OrderBy(x => x.fehrest.Shomareh)
            .Select(x => new ViewUserBarAvordOutPutDto
            {
                ItemFbShomareh = x.fehrest.Shomareh,
                Sharh = x.fehrest.Sharh,
                BahayeVahed = x.fehrest.BahayeVahed,
                Vahed = x.fehrest.Vahed,
                FBId = x.fbItem != null ? x.fbItem.ID : null,
                BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
                ItemZarib = x.fbItem != null ? x.fbItem.ItemZarib : 1,
                Meghdar = x.fbItem?.SumRizMetre ?? 0m,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
            }).ToList();


            List<ViewUserBarAvordOutPutDto> lstuserBarAvordOutPut = userBarAvordOutPut.Where(x => x.FBId != null).ToList();

            var fehrestShomarehs = lstuserBarAvordOutPut.Select(x => x.ItemFbShomareh).ToHashSet();

            var barAvordOnlyInFbItems = fbItems
            .Where(fb => !fehrestShomarehs.Contains(fb.Shomareh))
            .Select(fb => new ViewUserBarAvordOutPutDto
            {
                ItemFbShomareh = fb.Shomareh,
                ItemFbShomarehMain = fb.Shomareh.Substring(0, 6),
                Sharh = fb.BahayeVahedSharh,
                BahayeVahed = fb.BahayeVahed,
                Vahed = fb.Vahed,
                FBId = fb.ID,
                ItemZarib = fb.ItemZarib,
                Meghdar = fb.SumRizMetre ?? 0m,
                RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
                ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
            }).ToList();

            lstuserBarAvordOutPut = lstuserBarAvordOutPut
            .Concat(barAvordOnlyInFbItems)
            .OrderBy(x => x.ItemFbShomareh)
            .ToList();



            //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordId);
            //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordId);

            //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

            ////محاسبه ضرایب
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = BarAvordId,
                NoeFBId = NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result1 = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
            decimal zaribBalaSari = result1.zaribBalaSari != null ? result1.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result1.zaribManteghe != null ? result1.zaribManteghe.Value : 1;

            ///
            ///

            var FaslZarib = (from FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                             join ZarayebFosoul in _context.ZarayebFosouls
                             on FehrestBahaItemsReplace.FaslId equals ZarayebFosoul.FaslId
                             where FehrestBahaItemsReplace.Year == Year && FehrestBahaItemsReplace.NoeFB == NoeFBId
                             select new
                             {
                                 FBShomareh = FehrestBahaItemsReplace.FBShomarehNew,
                                 ZarayebFosoul.ZaribBalaSari,
                                 ZarayebFosoul.ZaribManteghe
                             }).ToList();
            var faslZaribByItemNumber = FaslZarib
                .Where(x => x.FBShomareh != null)
                .GroupBy(x => x.FBShomareh)
                .ToDictionary(g => g.Key, g => g.First());


            bool blnCheckUsePercent = false;
            //در صورتی که تجهیزکارگاه بصورت درصدی حساب شود
            clsTajhizKargahDarsad? TajhizKargahDarsad = _context.TajhizKargahDarsads.FirstOrDefault(x => x.BarAvordUserId == request.BarAvordId && x.NoeFB == request.NoeFBId);
            if (TajhizKargahDarsad != null)
            {
                blnCheckUsePercent = true;
            }

            decimal JameTajhiz = 0;


            decimal JameFaslInZarib = 0;
            decimal JameFasl = 0;

            decimal JameFaslWithoutBahaVahed = 0;//جمع آیتم های بدون بهای واحد
            decimal JameFaslWithoutBahaVahedWithZarib = 0;//جمع آیتم های بدون بهای واحد در ضریب

            decimal JameFaslWithOutTajhizInZarib = 0;
            decimal JameFaslWithOutTajhiz = 0;

            List<string> lstItemFbShomarehs = lstuserBarAvordOutPut.Select(x => x.ItemFbShomareh).Distinct().ToList();

            List<clsBahayeVahedItemsRelated> lstBVItemRelated = _context.BahayeVahedItemsRelateds
            .AsNoTracking()
            .Where(x => lstItemFbShomarehs.Contains(x.ItemShomarehRelated.ToLower().Trim()) && x.NoeFB == NoeFBId && x.Year == Year)
            .ToList();

        var relatedItemsByNumber = lstBVItemRelated
            .GroupBy(x => x.ItemShomarehRelated.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var outputItemsByNumber = userBarAvordOutPut
            .GroupBy(x => x.ItemFbShomareh.Trim())
            .ToDictionary(g => g.Key, g => g.First());


            foreach (var item in lstuserBarAvordOutPut)
            {
                decimal Meghdar = blnCheckUsePercent && item.ItemFbShomareh.Substring(0, 2) == "99"
                    ? 0m
                    : item.Meghdar;
                if (Meghdar != 0)
                {
                    relatedItemsByNumber.TryGetValue(item.ItemFbShomareh.Trim(), out var BVItemsRelated);
                    if (BVItemsRelated != null)
                    {
                        outputItemsByNumber.TryGetValue(BVItemsRelated.ItemShomareh.Trim(), out var barAvordInFehrest1);
                        if (barAvordInFehrest1 != null)
                        {
                            long BahayeVahed1 = barAvordInFehrest1.BahayeVahedNew != 0 ? barAvordInFehrest1.BahayeVahedNew : (barAvordInFehrest1.BahayeVahed == null || barAvordInFehrest1.BahayeVahed == "") ? 0 : long.Parse(barAvordInFehrest1.BahayeVahed);
                            item.BahayeVahed = (BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                        }
                    }
                }
                decimal dBVahed = 0;// item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                decimal dBahayeKol = 0;

                decimal ItemZarib = (item.ItemZarib == null || item.ItemZarib == 0) ? 1 : item.ItemZarib.Value;

                ///در صورتی که آیتم بهای واحدش تغییر یابد
                if (item.BahayeVahedNew != 0)
                {
                    dBVahed = item.BahayeVahedNew;
                    dBahayeKol = Meghdar * dBVahed * ItemZarib;
                    if (item.ItemFbShomareh.Substring(0, 2) != "99")
                    {
                        JameFaslWithoutBahaVahed += dBahayeKol;
                    }

                    if (faslZaribByItemNumber.TryGetValue(item.ItemFbShomareh, out var currentFaslZarib))
                    {
                        

                        decimal newZaribBalaSari = currentFaslZarib.ZaribBalaSari == null ? zaribBalaSari : currentFaslZarib.ZaribBalaSari.Value;
                        decimal newZaribManteghe = currentFaslZarib.ZaribManteghe == null ? zaribManteghe : currentFaslZarib.ZaribManteghe.Value;

                        if (item.ItemFbShomareh.Substring(0, 2) != "99")
                        {
                            JameFaslWithoutBahaVahedWithZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                            //JameFaslWithOutTajhiz += dBahayeKol;
                            //JameFaslWithOutTajhizInZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                        }
                        else
                            JameTajhiz += dBahayeKol;  //JameFaslWithoutBahaVahedWithZarib += dBahayeKol;

                        //if (item.ItemFbShomareh.Substring(0, 2) != "99")
                        //{
                        //}
                    }
                    else
                    {
                        if (item.ItemFbShomareh.Substring(0, 2) != "99")
                        {
                            JameFaslWithoutBahaVahedWithZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                            //JameFaslWithOutTajhiz += dBahayeKol;
                            //JameFaslWithOutTajhizInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                        }
                        else
                            JameTajhiz += dBahayeKol;
                        //else
                        //    JameFaslWithoutBahaVahedWithZarib += dBahayeKol;

                        //if (item.ItemFbShomareh.Substring(0, 2) != "99")
                        //{
                        //}
                    }

                }
                else
                {
                    if (item.BahayeVahed == null || item.BahayeVahed == "")
                    {
                        dBVahed = 0;
                    }
                    else
                    {
                        dBVahed = decimal.Parse(item.BahayeVahed);
                        dBahayeKol = Meghdar * dBVahed * ItemZarib;

                        JameFasl += dBahayeKol;

                        if (faslZaribByItemNumber.TryGetValue(item.ItemFbShomareh, out var currentFaslZarib))
                        {
                            

                            decimal newZaribBalaSari = currentFaslZarib.ZaribBalaSari == null ? zaribBalaSari : currentFaslZarib.ZaribBalaSari.Value;
                            decimal newZaribManteghe = currentFaslZarib.ZaribManteghe == null ? zaribManteghe : currentFaslZarib.ZaribManteghe.Value;

                            if (item.ItemFbShomareh.Substring(0, 2) != "99")
                            {
                                JameFaslInZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                                JameFaslWithOutTajhiz += dBahayeKol;
                                JameFaslWithOutTajhizInZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                            }
                            else
                                JameFaslInZarib += dBahayeKol;

                            //if (item.ItemFbShomareh.Substring(0, 2) != "99")
                            //{
                            //}


                        }
                        else
                        {
                            if (item.ItemFbShomareh.Substring(0, 2) != "99")
                            {
                                JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                                JameFaslWithOutTajhiz += dBahayeKol;
                                JameFaslWithOutTajhizInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                            }
                            else
                                JameFaslInZarib += dBahayeKol;

                            //if (item.ItemFbShomareh.Substring(0, 2) != "99")
                            //{
                            //}
                        }
                    }
                }


                //if (item.ItemFbShomareh.Substring(0, 2) != "99")
                //{
                //    JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                //}
                //else
                //    JameFaslInZarib += dBahayeKol;

            }


            List<clsItemFBStar> lstItemFBStars = await _context.ItemFBStars
                .AsNoTracking()
                .Where(x => x.BaravordId == BarAvordId && x.NoeFBId == NoeFBId)
                .ToListAsync();

            // جمع تمام ریزمتره‌های ستاره‌دار با یک Query گروهی؛ جلوگیری از N+1
            var rizMetreStarSums = await _context.RizMetreStars
                .AsNoTracking()
                .Where(x => x.ItemFBStar.BaravordId == BarAvordId &&
                            x.ItemFBStar.NoeFBId == NoeFBId)
                .GroupBy(x => x.ItemFBStarId)
                .Select(g => new { ItemFBStarId = g.Key, SumMeghdar = g.Sum(x => x.MeghdarJoz ?? 0m) })
                .ToDictionaryAsync(x => x.ItemFBStarId, x => x.SumMeghdar);

            decimal JameFaslStar = 0;
            decimal JameFaslStarWithZarib = 0;

            foreach (var itemFBStarCurrent in lstItemFBStars)
            {
                decimal ItemZaribStar = itemFBStarCurrent.ItemZaribStar == null ? 1 : itemFBStarCurrent.ItemZaribStar.Value;

                decimal BahayeVahedStar = itemFBStarCurrent.BahayeVahed * ItemZaribStar;

                decimal JameItemFBStar = rizMetreStarSums.TryGetValue(itemFBStarCurrent.ID, out var starSum)
                    ? starSum
                    : 0m;

                JameFaslStar += JameItemFBStar * BahayeVahedStar;
                bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;
                decimal ZaribStar2 = zaribBalaSari;
                if (blnKharidTajhizat)
                {
                    zaribManteghe = 1;
                    ZaribStar2 = 1.14m;
                }
                JameFaslStarWithZarib += (JameItemFBStar * BahayeVahedStar * zaribManteghe * ZaribStar2);
            }

            clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordId);
            int Monaghese = 0;
            if (baravordZaribBalaSari != null)
            {
                Monaghese = baravordZaribBalaSari.Monaghese;
            }

            CheckLimitItemStarDto checkLimitItemStar = new CheckLimitItemStarDto()
            {
                Check = false,
                Des = ""
            };

            string str = "";

            decimal d10Darsad = JameFaslInZarib * 0.1m;
            decimal d15Darsad = JameFaslInZarib * 0.15m;
            decimal d30Darsad = JameFaslInZarib * 0.3m;
            if (Monaghese == 5)
            {
                if (JameFaslStarWithZarib > d10Darsad)
                {
                    str = "مبلغ اقلام ستاره دار بیش از 10 درصد برآورد میباشد";
                    checkLimitItemStar.Check = true;
                }
            }
            else if (Monaghese == 4)
            {
                if (JameFaslStarWithZarib > d15Darsad)
                {
                    str = "مبلغ اقلام ستاره دار بیش از 15 درصد برآورد میباشد";
                    checkLimitItemStar.Check = true;
                }
            }
            else if (Monaghese == 3)
            {
                if (JameFaslStarWithZarib > d30Darsad)
                {
                    str = "مبلغ اقلام ستاره دار بیش از 30 درصد برآورد میباشد";
                    checkLimitItemStar.Check = true;

                }
            }

            //decimal JameTajhizInZarib = 0;

            //در صورتی که تجهیزکارگاه بصورت درصدی حساب شود
            if (TajhizKargahDarsad != null)
            {
                JameTajhiz = (JameFaslWithOutTajhizInZarib + JameFaslStarWithZarib) * (TajhizKargahDarsad.Darsad / 100);
                //JameTajhizInZarib = JameTajhiz * zaribBalaSari * zaribManteghe;
            }
            checkLimitItemStar.Des = str;

            ResultCalculateBarAvordDto result = new ResultCalculateBarAvordDto
            {
                JameFasl = JameFasl,// + JameTajhiz,
                JameFaslStar = JameFaslStar + JameFaslWithoutBahaVahed,
                JameFaslKol = JameFasl + JameFaslWithoutBahaVahed + JameFaslStar + JameTajhiz,

                JameFaslInZarib = JameFaslInZarib,// + JameTajhiz,
                JameFaslStarInZarib = JameFaslStarWithZarib + JameFaslWithoutBahaVahedWithZarib,
                JameFaslKolInZarib = JameFaslInZarib + JameFaslWithoutBahaVahedWithZarib + JameFaslStarWithZarib + JameTajhiz,

                JameFaslWithOutTajhiz = JameFaslWithOutTajhiz,
                JameFaslKolWithOutTajhiz = JameFaslWithOutTajhiz + JameFaslWithoutBahaVahed + JameFaslStar,

                JameFaslWithOutTajhizInZarib = JameFaslWithOutTajhizInZarib,
                JameFaslKolWithOutTajhizInZarib = JameFaslWithOutTajhizInZarib + JameFaslWithoutBahaVahedWithZarib + JameFaslStarWithZarib,
                Check = checkLimitItemStar
            };

            return result;
        }
        else
        {
            return new ResultCalculateBarAvordDto
            {

            };

        }
    }


    ////////
    ////////محاسبه کل برآورد
    /////
    ///
    public static async Task<ResultCalculateBarAvordDto> fnCalculateBarAvord(CalculateBarAvordDto request, ApplicationDbContext _context)
    {
        Guid BarAvordId = request.BarAvordId;
        int Year = request.Year;

        clsBaravordUser? baravordUser = await _context.BaravordUsers.AsNoTracking().FirstOrDefaultAsync(x => x.ID == BarAvordId);

        ////////////////////////////////
        //در صورتی که صورت وضعیت باشد، بایستی ضریب پیشنهادی پیمانکار را نیز در ضرایب منطقه و بالاسری برای هر فصل پیدا کرده و ضرب کنیم
        ////////////////////////////////
        NoeBarAvord NoeBAType = NoeBarAvord.BarAvord;

        List<clsZaribPishnahadiPaymanKarFosoul> lstZaribPishnahadiPaymanKarFosoul = new List<clsZaribPishnahadiPaymanKarFosoul>();

        if (baravordUser != null)
        {

            NoeBAType = baravordUser.Type;

            if (NoeBAType != NoeBarAvord.BarAvord)
            {
                clsBaravordUser? baravordUserMain = await _context.BaravordUsers.AsNoTracking().FirstOrDefaultAsync(x => x.ID == baravordUser.ParentId);
                if (baravordUserMain != null)
                {
                    //یافتن ضرایب پیشنهادی پیمانکار برای هر فصل
                    lstZaribPishnahadiPaymanKarFosoul =
                          _context.ZaribPishnahadiPaymanKarFosouls.AsNoTracking().Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == baravordUserMain.ID).ToList();
                }
            }
        }
        ////////////////////////
        ////////////////////////
        ////////////////////////
        if (baravordUser != null)
        {
            string[] NoeFB = baravordUser.NoeFBs.Split(",");
            // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
            decimal JameFaslAll = 0;
            decimal JameFaslInZaribAll = 0;

            decimal AllJameFaslStar = 0;
            decimal AllJameFaslStarWithZarib = 0;

            decimal JameTajhizAll = 0;
            //decimal JameTajhizInZaribAll = 0;

            List<clsFehrestBahaItemsReplace> lstFehrestBahaItemsReplace = await _context.FehrestBahaItemsReplaces.AsNoTracking().Where(x => x.Year == Year).ToListAsync();
            //List<clsZarayebFosoul> lstZarayebFosoul = _context.ZarayebFosouls.ToList();

            var FaslZarib = (from FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                             join ZarayebFosoul in _context.ZarayebFosouls
                             on FehrestBahaItemsReplace.FaslId equals ZarayebFosoul.FaslId
                             where FehrestBahaItemsReplace.Year == Year
                             select new
                             {
                                 NoeFB = FehrestBahaItemsReplace.NoeFB,
                                 FBShomareh = FehrestBahaItemsReplace.FBShomarehNew,
                                 ZarayebFosoul.ZaribBalaSari,
                                 ZarayebFosoul.ZaribManteghe
                             }).ToList();

            // اقلام ستاره‌دار و جمع ریزمتره آن‌ها را یک‌بار برای کل برآورد دریافت می‌کنیم.
            // قبلاً برای هر NoeFB دو Query جداگانه اجرا می‌شد.
            var noeFBIds = NoeFB
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => (NoeFehrestBaha)int.Parse(x))
                .Distinct()
                .ToList();

            var allItemFBStars = await _context.ItemFBStars
                .AsNoTracking()
                .Where(x => x.BaravordId == BarAvordId && noeFBIds.Contains(x.NoeFBId))
                .ToListAsync();

            var rizMetreStarSums = await _context.RizMetreStars
                .AsNoTracking()
                .Where(x => x.ItemFBStar.BaravordId == BarAvordId &&
                            noeFBIds.Contains(x.ItemFBStar.NoeFBId))
                .GroupBy(x => x.ItemFBStarId)
                .Select(g => new { ItemFBStarId = g.Key, SumMeghdar = g.Sum(x => x.MeghdarJoz ?? 0m) })
                .ToDictionaryAsync(x => x.ItemFBStarId, x => x.SumMeghdar);

            var itemFBStarsByNoeFB = allItemFBStars
                .GroupBy(x => x.NoeFBId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var noeFB in NoeFB)
            {
                if (noeFB != "")
                {
                    //decimal SumMeghdarFasl = 0;
                    NoeFehrestBaha NoeFBId = (NoeFehrestBaha)int.Parse(noeFB);

                    List<clsFehrestBahaItemsReplace> lstFehrestBahaItemsReplaceFilter = lstFehrestBahaItemsReplace.Where(x => x.NoeFB == NoeFBId).ToList();

                    var FaslZaribFilter = FaslZarib.Where(x => x.NoeFB == NoeFBId).ToList();
                    var faslZaribByItemNumber = FaslZaribFilter
                        .Where(x => x.FBShomareh != null)
                        .GroupBy(x => x.FBShomareh)
                        .ToDictionary(g => g.Key, g => g.First());

                    // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
                    //var fbItems = _context.FBs
                    //    .Where(f => f.BarAvordId == BarAvordId && f.NoeFBId == NoeFBId)
                    //    .GroupBy(f => f.Shomareh)
                    //    .Select(g => g.FirstOrDefault())
                    //    .ToList(); // انتقال به حافظه، جلوگیری از خطای EF


                    //var fbItems = await (from fb in _context.FBs
                    //                     join fehrestbaha in _context.FehrestBahas
                    //                     on fb.Shomareh.Substring(0, 6) equals fehrestbaha.Shomareh.Trim()
                    //                     where fb.BarAvordId == BarAvordId && fb.NoeFBId == NoeFBId
                    //                            && fehrestbaha.NoeFB == NoeFBId && fehrestbaha.Sal == request.Year
                    //                     select new FBForGetUserBarAvordDto
                    //                     {
                    //                         ID = fb.ID,
                    //                         BahayeVahedSharh = fb.BahayeVahedSharh,
                    //                         Shomareh = fb.Shomareh.Substring(0, 6),
                    //                         BahayeVahed = fehrestbaha == null ? "0" : fehrestbaha.BahayeVahed != null ? (fb.BahayeVahedZarib * decimal.Parse(fehrestbaha.BahayeVahed.Trim() == "" ? "0" :
                    //                                                      fehrestbaha.BahayeVahed)).ToString() : fehrestbaha.BahayeVahed,
                    //                         Vahed = fehrestbaha.Vahed,
                    //                         BahayeVahedZarib = fb.BahayeVahedZarib,
                    //                         BahayeVahedNew = fb.BahayeVahedNew,
                    //                         ItemZarib = fb.ItemZarib
                    //                     })
                    //    .GroupBy(f => f.Shomareh)
                    //    .Select(g => g.FirstOrDefault())
                    //    .ToListAsync();


                    var fbItems = await (
                    from fb in _context.FBs

                    join fehrestbaha in _context.FehrestBahas
                       on fb.Shomareh.Substring(0, 6)
                       equals fehrestbaha.Shomareh.Trim()
                       into fehrestGroup

                    from fehrestbaha in fehrestGroup
                       .Where(x =>
                           x.NoeFB == NoeFBId &&
                           x.Sal == request.Year)
                       .DefaultIfEmpty()

                    where fb.BarAvordId == request.BarAvordId
                         && fb.NoeFBId == NoeFBId

                    select new FBForGetUserBarAvordDto
                    {
                        ID = fb.ID,

                        BahayeVahedSharh = fb.BahayeVahedSharh,

                        // شماره واقعی FB حفظ شود
                        Shomareh = fb.Shomareh,

                        BahayeVahed =
                           fehrestbaha == null
                               ? "0"
                               : fehrestbaha.BahayeVahed != null
                                   ? fehrestbaha.BahayeVahed.Trim()
                                   : "0",

                        Vahed = fehrestbaha != null
                           ? fehrestbaha.Vahed
                           : null,

                        //BahayeVahedZarib = fb.BahayeVahedZarib,

                        BahayeVahedNew = fb.BahayeVahedNew,

                        ItemZarib = fb.ItemZarib,
                        SumRizMetre = fb.SumRizMetre
                    }
                    )
                    .GroupBy(x => x.Shomareh)
                    .Select(g => g.First())
                    .ToListAsync();

                    //foreach (var item in fbItems)
                    //{
                    //    foreach (var fbItemsReplace in lstFehrestBahaItemsReplaceFilter)
                    //    {
                    //        if (item.Shomareh == fbItemsReplace.FBShomarehMain)
                    //        {
                    //            item.Shomareh = fbItemsReplace.FBShomarehNew;
                    //        }
                    //    }
                    //}

                    var replaceDictionary = lstFehrestBahaItemsReplaceFilter
                        .GroupBy(x => x.FBShomarehMain)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Last().FBShomarehNew
                        );

                    foreach (var item in fbItems)
                    {
                        if (replaceDictionary.TryGetValue(item.Shomareh, out var newShomareh))
                        {
                            item.Shomareh = newShomareh;
                        }
                    }


                    var lstFehrestBahas = await (
             from fb in _context.FehrestBahas.AsNoTracking()
             join replace in _context.FehrestBahaItemsReplaces.AsNoTracking()
             on fb.Shomareh equals replace.FBShomarehMain into replaces
             from replace in replaces.DefaultIfEmpty()
             where fb.Sal == Year
                 && fb.NoeFB == NoeFBId
             select new
             {
                 Id = fb.Id,
                 BahayeVahed = fb.BahayeVahed,
                 Vahed = fb.Vahed,
                 Sharh = fb.Sharh,

                 ShomarehMain = fb.Shomareh,

                 Shomareh = replace != null && replace.FBShomarehNew != null
                 ? replace.FBShomarehNew
                 : fb.Shomareh,

                 Sal = fb.Sal,
                 NoeFB = fb.NoeFB
             }).ToListAsync();

                    List<ViewUserBarAvordOutPutDto> userBarAvordOutPut = lstFehrestBahas //_context.FehrestBahas
                    .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId)
                    .ToList() // انتقال به حافظه
                    .GroupJoin(fbItems,
                        fehrest => fehrest.Shomareh,
                        fb => fb.Shomareh.Substring(0, 6),
                        (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
                    .OrderBy(x => x.fehrest.Shomareh)
                    .Select(x => new ViewUserBarAvordOutPutDto
                    {
                        ItemFbShomareh = x.fehrest.Shomareh,
                        Sharh = x.fehrest.Sharh,
                        BahayeVahed = x.fehrest.BahayeVahed,
                        Vahed = x.fehrest.Vahed,
                        FBId = x.fbItem != null ? x.fbItem.ID : null,
                        BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
                        ItemZarib = x.fbItem != null ? x.fbItem.ItemZarib : 1,
                        Meghdar = x.fbItem?.SumRizMetre ?? 0m,
                        RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                    }).ToList();


                    List<ViewUserBarAvordOutPutDto> userBarAvordOutPutFiltered = userBarAvordOutPut.Where(x => x.FBId != null).ToList();


                    //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordId);
                    //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordId);

                    //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
                    //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

                    ////محاسبه ضرایب
                    RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
                    {
                        BarAvordId = BarAvordId,
                        NoeFBId = NoeFBId,
                        Year = Year
                    };
                    ResultGetZarayebDto result1 = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
                    decimal zaribBalaSari = result1.zaribBalaSari != null ? result1.zaribBalaSari.Value : 1;
                    decimal zaribManteghe = result1.zaribManteghe != null ? result1.zaribManteghe.Value : 1;

                    decimal AllZarib = zaribManteghe * zaribBalaSari;

                    ///
                    ///
                    //List<long> FaslIds = _context.ZarayebFosouls.Include(x => x.Fosoul).Where(x => x.Fosoul.NoeFB == NoeFBId && x.Fosoul.Year == Year).Select(x => x.FaslId).ToList();
                    //List<string> _context.FosoulItems.Where(x => FaslIds.Contains(x.FosoulId)).ToList();




                    bool blnCheckUsePercent = false;

                    clsTajhizKargahDarsad? TajhizKargahDarsad = _context.TajhizKargahDarsads.FirstOrDefault(x => x.BarAvordUserId == request.BarAvordId && x.NoeFB == NoeFBId);
                    if (TajhizKargahDarsad != null)
                    {
                        blnCheckUsePercent = true;
                    }

                    decimal JameFaslInZarib = 0;
                    decimal JameFasl = 0;

                    decimal JameFaslWithoutBahaVahed = 0;//جمع آیتم های بدون بهای واحد
                    decimal JameFaslWithoutBahaVahedWithZarib = 0;//جمع آیتم های بدون بهای واحد در ضریب


                    var fehrestShomarehs = userBarAvordOutPutFiltered.Select(x => x.ItemFbShomareh).ToList();

                    var barAvordOnlyInFbItems = fbItems
                        .Where(fb => !fehrestShomarehs.Contains(fb.Shomareh))
                        .Select(fb => new ViewUserBarAvordOutPutDto
                        {
                            ItemFbShomareh = fb.Shomareh,
                            ItemFbShomarehMain = fb.Shomareh.Substring(0, 6),
                            Sharh = fb.BahayeVahedSharh,
                            BahayeVahed = fb.BahayeVahed,
                            Vahed = fb.Vahed,
                            FBId = fb.ID,
                            ItemZarib = fb.ItemZarib,
                            Meghdar = fb.SumRizMetre ?? 0m,
                            RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>(),
                            ItemsFields = new List<ItemsFieldForUserBarAvordOutPutDto>()
                        }).ToList();

                    userBarAvordOutPutFiltered = userBarAvordOutPutFiltered
                        .Concat(barAvordOnlyInFbItems)
                        .OrderBy(x => x.ItemFbShomareh)
                        .DistinctBy(x=>x.FBId)
                        .ToList();
                    


                    List<string> lstItemFbShomarehs = userBarAvordOutPutFiltered.Select(x => x.ItemFbShomareh).Distinct().ToList();

                    List<clsBahayeVahedItemsRelated> lstBVItemRelated = _context.BahayeVahedItemsRelateds
            .AsNoTracking()
            .Where(x => lstItemFbShomarehs.Contains(x.ItemShomarehRelated.ToLower().Trim()) && x.NoeFB == NoeFBId && x.Year == Year)
            .ToList();

        var relatedItemsByNumber = lstBVItemRelated
            .GroupBy(x => x.ItemShomarehRelated.Trim())
            .ToDictionary(g => g.Key, g => g.First());

        var outputItemsByNumber = userBarAvordOutPut
            .GroupBy(x => x.ItemFbShomareh.Trim())
            .ToDictionary(g => g.Key, g => g.First());

                    foreach (var item in userBarAvordOutPutFiltered)
                    {
                        decimal ItemZarib = (item.ItemZarib == null || item.ItemZarib == 0) ? 1 : item.ItemZarib.Value;
                        decimal Meghdar = blnCheckUsePercent && item.ItemFbShomareh.Substring(0, 2) == "99"
                            ? 0m
                            : item.Meghdar;

                        if (Meghdar != 0)
                        {
                            decimal dBVahed = 0;// item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                            decimal dBahayeKol = 0;


                            relatedItemsByNumber.TryGetValue(item.ItemFbShomareh.Trim(), out var BVItemsRelated);
                            if (BVItemsRelated != null)
                            {
                                outputItemsByNumber.TryGetValue(BVItemsRelated.ItemShomareh.Trim(), out var barAvordInFehrest1);
                                if (barAvordInFehrest1 != null)
                                {
                                    long BahayeVahed1 = barAvordInFehrest1.BahayeVahedNew != 0 ? barAvordInFehrest1.BahayeVahedNew :
                                        (barAvordInFehrest1.BahayeVahed == null || barAvordInFehrest1.BahayeVahed == "") ? 0 : long.Parse(barAvordInFehrest1.BahayeVahed);
                                    item.BahayeVahed = (BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                                }
                            }


                            if (item.BahayeVahedNew != 0)
                            {
                                dBVahed = item.BahayeVahedNew;
                                dBahayeKol = Meghdar * dBVahed * ItemZarib;
                                JameFaslWithoutBahaVahed += dBahayeKol;

                                if (zaribBalaSari == 0 && zaribBalaSari == 0)
                                {

                                }
                                else
                                {
                                    if (faslZaribByItemNumber.TryGetValue(item.ItemFbShomareh, out var currentFaslZarib))
                                    {
                                        

                                        decimal newZaribBalaSari = currentFaslZarib.ZaribBalaSari == null ? zaribBalaSari : currentFaslZarib.ZaribBalaSari.Value;
                                        decimal newZaribManteghe = currentFaslZarib.ZaribManteghe == null ? zaribManteghe : currentFaslZarib.ZaribManteghe.Value;

                                        if (item.ItemFbShomareh.Substring(0, 2) != "99")
                                        {
                                            JameFaslWithoutBahaVahedWithZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                                        }
                                        else
                                            JameFaslWithoutBahaVahedWithZarib += dBahayeKol;

                                    }
                                    else
                                    {
                                        if (item.ItemFbShomareh.Substring(0, 2) != "99")
                                        {
                                            JameFaslWithoutBahaVahedWithZarib += dBahayeKol * AllZarib; //zaribBalaSari * zaribManteghe;
                                        }
                                        else
                                            JameFaslWithoutBahaVahedWithZarib += dBahayeKol;

                                    }
                                }
                            }
                            else
                            {
                                if (item.BahayeVahed == null || item.BahayeVahed == "")
                                {
                                    dBVahed = 0;
                                }
                                else
                                {
                                    dBVahed = decimal.Parse(item.BahayeVahed);

                                    //item.Meghdar = Meghdar;
                                    dBahayeKol = Meghdar * dBVahed * ItemZarib;
                                    //item.BahayeKol = dBahayeKol;
                                    JameFasl += dBahayeKol;

                                    if (zaribBalaSari == 0 && zaribBalaSari == 0)
                                    {

                                    }
                                    else
                                    {
                                        if (faslZaribByItemNumber.TryGetValue(item.ItemFbShomareh, out var currentFaslZarib))
                                        {
                                            

                                            decimal newZaribBalaSari = currentFaslZarib.ZaribBalaSari == null ? zaribBalaSari : currentFaslZarib.ZaribBalaSari.Value;
                                            decimal newZaribManteghe = currentFaslZarib.ZaribManteghe == null ? zaribManteghe : currentFaslZarib.ZaribManteghe.Value;

                                            if (item.ItemFbShomareh.Substring(0, 2) != "99")
                                            {
                                                JameFaslInZarib += dBahayeKol * newZaribBalaSari * newZaribManteghe;
                                            }
                                            else
                                                JameFaslInZarib += dBahayeKol;

                                        }
                                        else
                                        {
                                            if (item.ItemFbShomareh.Substring(0, 2) != "99")
                                            {
                                                JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
                                            }
                                            else
                                                JameFaslInZarib += dBahayeKol;

                                        }
                                    }
                                }
                            }
                        }
                    }
                    JameFaslAll += JameFasl;
                    JameFaslInZaribAll += JameFaslInZarib;


                    //JameTajhizInZaribAll += JameTajhizInZarib;


                    List<clsItemFBStar> lstItemFBStars = itemFBStarsByNoeFB.TryGetValue(NoeFBId, out var starsForNoeFB)
                        ? starsForNoeFB
                        : new List<clsItemFBStar>();

                    decimal JameFaslStar = 0;
                    decimal JameFaslStarWithZarib = 0;

                    foreach (var itemFBStarCurrent in lstItemFBStars)
                    {
                        decimal ItemZaribStar = (itemFBStarCurrent.ItemZaribStar == null || itemFBStarCurrent.ItemZaribStar == 0) ? 1 : itemFBStarCurrent.ItemZaribStar.Value;

                        //clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.Trim() == itemFBStarCurrent.Shomareh);

                        //BahayeVahedStar = 0;
                        //if (itemFBStar != null)
                        //{
                        decimal BahayeVahedStar = itemFBStarCurrent.BahayeVahed * ItemZaribStar;
                        //}
                        decimal JameItemFBStar = rizMetreStarSums.TryGetValue(itemFBStarCurrent.ID, out var starSum)
                            ? starSum
                            : 0m;

                        JameFaslStar += JameItemFBStar * BahayeVahedStar;
                        bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;
                        decimal ZaribStar2 = zaribBalaSari;
                        if (blnKharidTajhizat)
                        {
                            zaribManteghe = 1;
                            ZaribStar2 = 1.14m;
                        }
                        JameFaslStarWithZarib += (JameItemFBStar * BahayeVahedStar * zaribManteghe * ZaribStar2);
                    }

                    decimal JameTajhiz = 0;
                    //decimal JameTajhizInZarib = 0;

                    //در صورتی که تجهیزکارگاه بصورت درصدی حساب شود
                    if (TajhizKargahDarsad != null)
                    {
                        JameTajhiz = (JameFaslInZarib + JameFaslStarWithZarib) * (TajhizKargahDarsad.Darsad / 100);
                        //JameTajhizInZarib = JameTajhiz * zaribBalaSari * zaribManteghe;
                    }

                    JameTajhizAll += JameTajhiz;


                    AllJameFaslStar += JameFaslStar + JameFaslWithoutBahaVahed;
                    AllJameFaslStarWithZarib += JameFaslStarWithZarib + JameFaslWithoutBahaVahedWithZarib;

                }
            }

            ResultCalculateBarAvordDto result = new ResultCalculateBarAvordDto
            {
                JameFasl = JameFaslAll,
                JameFaslStar = AllJameFaslStar + JameTajhizAll,
                JameFaslKol = JameFaslAll + JameTajhizAll + AllJameFaslStar,

                JameFaslInZarib = JameFaslInZaribAll,
                JameFaslStarInZarib = AllJameFaslStarWithZarib + JameTajhizAll,
                JameFaslKolInZarib = JameFaslInZaribAll + AllJameFaslStarWithZarib + JameTajhizAll,

            };
            return result;

        }
        else
        {
            return new ResultCalculateBarAvordDto
            {

            };
        }
    }


    ///
    //دریافت ضرایب
    ///
    public static ResultGetZarayebDto GetZarayeb(RequestGetZarayebDto request, ApplicationDbContext _context)
    {
        Guid BarAvordId = request.BarAvordId;
        int Year = request.Year;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordId);
        clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordId);

        decimal? zaribBalaSari = 0;
        decimal zaribManteghe = 0;
        if (baravordZaribBalaSari != null)
        {
            clsZaribBalaSari? currentZaribBalaSari = _context.ZaribBalaSaris
                .FirstOrDefault(x => x.Year == Year && x.NoeFBId == NoeFBId && x.Noe1 == baravordZaribBalaSari.Tarh && x.Noe2 == baravordZaribBalaSari.Monaghese);
            if (currentZaribBalaSari != null)
            {
                zaribBalaSari = currentZaribBalaSari.Zarib;
            }
        }
        if (baravordZaribManteghe != null)
        {
            clsBakhsh? currentBakhsh = _context.Bakhshs.FirstOrDefault(x => x.Id == baravordZaribManteghe.BakhshId);
            if (currentBakhsh != null)
            {
                switch (NoeFBId)
                {
                    case NoeFehrestBaha.RahDari:
                        {
                            zaribManteghe = currentBakhsh.ZaribRahDari;
                            break;
                        }
                    case NoeFehrestBaha.Abnie:
                        {
                            zaribManteghe = currentBakhsh.ZaribAbnie;
                            break;
                        }
                    case NoeFehrestBaha.RahoBand:
                        {
                            zaribManteghe = currentBakhsh.ZaribRah;
                            break;
                        }
                    case NoeFehrestBaha.Barghi:
                        {
                            zaribManteghe = currentBakhsh.ZaribBargh;
                            break;
                        }
                    case NoeFehrestBaha.Mekaniki:
                        {
                            zaribManteghe = currentBakhsh.ZaribMechanic;
                            break;
                        }
                    case NoeFehrestBaha.MareMat:
                        {
                            zaribManteghe = currentBakhsh.ZaribAbnie;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        ResultGetZarayebDto result = new ResultGetZarayebDto
        {
            zaribBalaSari = zaribBalaSari,
            zaribManteghe = zaribManteghe
        };

        return result;
    }

    public static decimal? SumRizMetreCurrentFB(Guid FBId, ApplicationDbContext context)
    {
        decimal? SumRizMetreCurrentFB = context.FBs.AsNoTracking()
            .Where(x => x.ID == FBId)
            .Select(x => x.SumRizMetre)
            .FirstOrDefault();
        return SumRizMetreCurrentFB;
    }
}
