using System.Numerics;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation;


public class OperationController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        return View();
    }
    public ActionResult ShowTree()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ViewShowTreeForm");

        return View();
    }


    public JsonResult GetTree([FromBody] GetTreeInputDto request)
    {
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        var ShomarehItems = (from op_Item in _context.Operation_ItemsFBs
                             join FB in _context.FehrestBahas
                             on op_Item.ItemsFBShomareh.Substring(0, 6) equals FB.Shomareh.Trim()
                             select new
                             {
                                 ItemsFBShomareh = op_Item.ItemsFBShomareh,
                                 OperationId = op_Item.OperationId,
                                 Sharh = FB.Sharh,
                                 Year = FB.Sal,
                                 NoeFB = FB.NoeFB,
                                 Vahed = FB.Vahed,
                                 BahayeVahed = FB.BahayeVahed,
                             }).Where(x => x.Year == request.Year && x.NoeFB == NoeFBId);

        //List<AllOperationDto> operation = (from op in _context.Operations
        //                                   join OperationDetail in _context.OperationDetails
        //                                   on op.Id equals OperationDetail.OperationId
        //                                   join Sh_Items in ShomarehItems
        //                                   on op.Id equals Sh_Items.OperationId
        //                                   into joinTable
        //                                   from jT in joinTable.DefaultIfEmpty()
        //                                   where op.Year == request.Year
        //                                   select new AllOperationDto
        //                                   {
        //                                       ID = op.Id,
        //                                       order = op.Order,
        //                                       ParentId = op.ParentId,
        //                                       OperationName = op.OperationName,
        //                                       FunctionCall = op.FunctionCall == null ? "" : op.FunctionCall,
        //                                       Sharh = jT.Sharh,
        //                                       ItemsFBShomareh = jT.ItemsFBShomareh == null ? "" : jT.ItemsFBShomareh,
        //                                       Year = op.Year,
        //                                       CheckData = OperationDetail.CheckData,
        //                                       HasEnteringValue = OperationDetail.HasEnteringValue
        //                                   })
        //                 .OrderBy(x => x.order)
        //                 .ThenBy(x => x.ID)
        //                 .ToList();


        // فرض: در AllOperationDto => public bool? HasEnteringValue { get; set; }
        // نوع CheckData را هم نال‌پذیر بگیرید (مثلاً string? یا هر نوع واقعی آن به صورت ?)

        // 1) بخش دیتابیسی را بگیرید
        var baseRows = (
            from op in _context.Operations
            where op.Year == request.Year
            join od in _context.OperationDetails
                on op.Id equals od.OperationId into odGroup
            from od in odGroup.DefaultIfEmpty()
            select new { op, od }
        ).ToList();

        // 2) لاکاپ کمکی از ShomarehItems در حافظه
        var siLookup = ShomarehItems.ToLookup(s => s.OperationId);

        // 3) جوین سمت کلاینت + نگاشت به DTO
        var operation = baseRows
            .SelectMany(x => siLookup[x.op.Id].DefaultIfEmpty(),
                (x, si) => new AllOperationDto
                {
                    ID = x.op.Id,
                    order = x.op.Order,                       // در DTO nullable باشد اگر در DB nullable است
                    ParentId = x.op.ParentId,                 // ترجیحاً nullable
                    Year = x.op.Year,
                    OperationName = x.op.OperationName ?? "",
                    LatinName = x.op.LatinName ?? "",
                    FunctionCall = x.op.FunctionCall ?? "",
                    Sharh = si?.Sharh ?? "",
                    ItemsFBShomareh = si?.ItemsFBShomareh ?? "",
                    CheckData = x.od?.CheckData,
                    HasEnteringValue = x.od?.HasEnteringValue,
                    MaxValue = x.od?.MaxValue,
                    MinValue = x.od?.MinValue,
                    Description = x.od?.Description,
                    CheckNecessary = null
                })
            .OrderBy(x => x.order)
            .ThenBy(x => x.ID)
            .ToList();

        List<AllOperationDto> NewOperation = new List<AllOperationDto>();

        //decimal dOperationDefaultValue = 0;

        foreach (var item in operation)
        {
            if (item.LatinName != null)
            {
                if (item.LatinName.Trim().ToLower() == "transport")
                {
                    clsBarAvordHamlNecessaryLimit? barAvordHamlNecessaryLimit = _context.BarAvordHamlNecessaryLimits.FirstOrDefault(x => x.BarAvordId == request.BarAvordUserId && x.NoeFBId == NoeFBId);
                    if (barAvordHamlNecessaryLimit != null)
                    {
                        item.CheckNecessary = true;
                    }
                    else
                        item.CheckNecessary = false;
                }
            }
            if (item.CheckData != null)
            {
                if (item.CheckData.Value)
                {
                    clsBarAvordHaml? barAvordHaml = _context.BarAvordHamls.FirstOrDefault(x => x.BarAvordId == request.BarAvordUserId && x.NoeFBId == NoeFBId && x.FBShomarehHaml == item.ItemsFBShomareh.Trim());

                    if (barAvordHaml != null)
                    {
                        item.OperationDefaultValue = barAvordHaml?.Value;
                    }

                    long RMCount = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == request.BarAvordUserId && x.FB.Shomareh == item.ItemsFBShomareh).Count();
                    if (RMCount != 0)
                    {
                        NewOperation.Add(item);
                    }

                    item.ItemsFBShomareh = item.ItemsFBShomareh.Trim() == "" ? "" : item.ItemsFBShomareh.Substring(0, 6);
                }
            }
            else
                NewOperation.Add(item);
        }

        return new JsonResult(NewOperation);
    }


    public JsonResult GetTreeForOneOperation([FromBody] GetTreeInputDto request)
    {
        NoeFehrestBaha NoeFBId = request.NoeFBId;

        clsOperation? Operation = _context.Operations.FirstOrDefault(x => x.LatinName == request.OpName);
        if (Operation != null)
        {

            var ShomarehItems = (from op_Item in _context.Operation_ItemsFBs
                                 join FB in _context.FehrestBahas
                                 on op_Item.ItemsFBShomareh equals FB.Shomareh
                                 select new
                                 {
                                     ItemsFBShomareh = op_Item.ItemsFBShomareh,
                                     OperationId = op_Item.OperationId,
                                     Sharh = FB.Sharh,
                                     Year = FB.Sal,
                                     NoeFB = FB.NoeFB,
                                     Vahed = FB.Vahed,
                                     BahayeVahed = FB.BahayeVahed,
                                 }).Where(x => x.Year == request.Year && x.NoeFB == NoeFBId);
            // پیدا کردن parentId
            var parentId = _context.Operations
                .Where(x => x.Year == request.Year && x.LatinName == "transport")
                .Select(x => (int?)x.Id)      // ← خروجی nullable تا اگر نبود، null بدهد
                .FirstOrDefault();

            var shItemsQ = ShomarehItems.AsQueryable();

            //var operation = (from op in _context.Operations
            //                 join OpeartionDetail in _context.OperationDetails
            //                 on op.Id equals OpeartionDetail.OperationId
            //                 join sh in shItemsQ
            //                     on op.Id equals sh.OperationId into joinTable
            //                 from jT in joinTable.DefaultIfEmpty()
            //                 where op.Year == request.Year
            //                       && op.ParentId == parentId   // ← فقط فرزندان، خود والد نمی‌آید
            //                 select new AllOperationDto
            //                 {
            //                     ID = op.Id,
            //                     order = op.Order,
            //                     ParentId = op.ParentId,                // اگر DTO nullable است
            //                     OperationName = op.OperationName,
            //                     FunctionCall = op.FunctionCall == null ? "" : op.FunctionCall,
            //                     Sharh = jT.Sharh,
            //                     ItemsFBShomareh = jT.ItemsFBShomareh == null ? "" : jT.ItemsFBShomareh,
            //                     Year = op.Year,
            //                     CheckData = OpeartionDetail.CheckData,
            //                     HasEnteringValue = OpeartionDetail.HasEnteringValue
            //                 })
            //                 .OrderBy(x => x.order)
            //                 .ThenBy(x => x.ID)
            //                 .ToList();


            var baseRows = (
           from op in _context.Operations
           where op.Year == request.Year
           join od in _context.OperationDetails
               on op.Id equals od.OperationId into odGroup
           from od in odGroup.DefaultIfEmpty()
           select new { op, od }
            ).ToList();

            // 2) لاکاپ کمکی از ShomarehItems در حافظه
            var siLookup = ShomarehItems.ToLookup(s => s.OperationId);

            // 3) جوین سمت کلاینت + نگاشت به DTO
            var operation = baseRows
                .SelectMany(x => siLookup[x.op.Id].DefaultIfEmpty(),
                    (x, si) => new AllOperationDto
                    {
                        ID = x.op.Id,
                        order = x.op.Order,                       // در DTO nullable باشد اگر در DB nullable است
                        ParentId = x.op.ParentId,                 // ترجیحاً nullable
                        Year = x.op.Year,
                        OperationName = x.op.OperationName ?? "",
                        LatinName = x.op.LatinName ?? "",
                        FunctionCall = x.op.FunctionCall ?? "",
                        Sharh = si?.Sharh ?? "",
                        ItemsFBShomareh = si?.ItemsFBShomareh ?? "",
                        CheckData = x.od?.CheckData,
                        HasEnteringValue = x.od?.HasEnteringValue,
                        MaxValue = x.od?.MaxValue,
                        MinValue = x.od?.MinValue,
                        Description = x.od?.Description,
                        CheckNecessary = null
                    })
                .OrderBy(x => x.order)
                .ThenBy(x => x.ID)
                .ToList();

            List<AllOperationDto> NewOperation = new List<AllOperationDto>();

            foreach (var item in operation)
            {
                if (item.CheckData != null)
                {
                    if (item.CheckData.Value)
                    {
                        clsBarAvordHaml? barAvordHaml = _context.BarAvordHamls.FirstOrDefault(x => x.BarAvordId == request.BarAvordUserId && x.NoeFBId == NoeFBId && x.FBShomarehHaml == item.ItemsFBShomareh.Trim());

                        if (barAvordHaml != null)
                        {
                            item.OperationDefaultValue = barAvordHaml.Value;
                        }

                        long RMCount = _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == request.BarAvordUserId && x.FB.Shomareh == item.ItemsFBShomareh).Count();
                        if (RMCount != 0)
                        {
                            NewOperation.Add(item);
                        }

                        item.ItemsFBShomareh = item.ItemsFBShomareh.Trim() == "" ? "" : item.ItemsFBShomareh.Substring(0, 6);
                    }
                }
                else
                    NewOperation.Add(item);
            }

            var result = new
            {
                AllOperation = NewOperation,
                OperationId = Operation.Id
            };
            return new JsonResult(result);
        }
        else
            return null;
    }

    public ActionResult ChangeBarAvordHamlNecessaryLimit([FromBody] ChangeBarAvordHamlNecessaryLimitDto request)
    {
        //try
        //{
        List<clsBarAvordHaml> lstBarAvordHaml = _context.BarAvordHamls.Where(x => x.BarAvordId == request.BarAvordId).ToList();
        List<string> lstBarAvordHamlShomarehHamls = lstBarAvordHaml.Select(x => x.FBShomarehHaml).ToList();
        List<Guid> lstBarAvordHamlIds = lstBarAvordHaml.Select(x => x.ID).ToList();
        List<clsOperation_ItemsFB> lstOperation_ItemsFB = _context.Operation_ItemsFBs.Where(x => lstBarAvordHamlShomarehHamls.Contains(x.ItemsFBShomareh.Substring(0, 6))).ToList();
        List<long> lstOperationDetailIds = lstOperation_ItemsFB.Select(x => x.OperationId).ToList();
        List<clsOperationDetail> lstOperationDetails = _context.OperationDetails.Where(x => lstOperationDetailIds.Contains(x.OperationId)).ToList();
        List<clsBarAvordHamlRizMetre> lstBarAvordHamlRizMetre = _context.BarAvordHamlRizMetres.Where(x => lstBarAvordHamlIds.Contains(x.BarAvordHamlId)).ToList();

        NoeFehrestBaha NoeFBId = request.NoeFBId;

        bool Checked = request.blnChecked;
        Guid BarAvordId = request.BarAvordId;
        if (Checked)
        {
            _context.BarAvordHamlNecessaryLimits.Add(new clsBarAvordHamlNecessaryLimit
            {
                BarAvordId = BarAvordId,
                NoeFBId = NoeFBId
            });

            //در این صورت بایستی کیلومتراژ ها بدون محدودیت باشند
            foreach (var item in lstBarAvordHaml)
            {
                clsOperation_ItemsFB? operation_ItemsFB = lstOperation_ItemsFB.FirstOrDefault(x => x.ItemsFBShomareh == item.FBShomarehHaml);
                if (operation_ItemsFB != null)
                {

                    clsOperationDetail? operationDetail = _context.OperationDetails.FirstOrDefault(x => x.OperationId == operation_ItemsFB.OperationId);
                    if (operationDetail != null)
                    {
                        decimal? dValue = item.ValueBase;
                        decimal? dFinaValue = 0;

                        dFinaValue = dValue - (operationDetail.MinValue == null ? 0 : operationDetail.MinValue.Value);

                        item.Value = dFinaValue;
                        item.ValueBase = dValue;

                        clsBarAvordHamlRizMetre barAvordHamlRizMetre = lstBarAvordHamlRizMetre.First(x => x.BarAvordHamlId == item.ID);

                        List<clsRizMetreUsers> lstRizMetreUsers = _context.RizMetreUserses.Where(x => x.ID == barAvordHamlRizMetre.RizMetreId).ToList();
                        foreach (var RizMetreUser in lstRizMetreUsers)
                        {
                            _context.Entry(RizMetreUser).CurrentValues.SetValues(new
                            {
                                Tedad = dFinaValue,
                                MeghdarJoz = dFinaValue * RizMetreUser.Vazn
                            });
                        }
                    }
                }
            }
        }
        else
        {
            clsBarAvordHamlNecessaryLimit? barAvordHamlNecessaryLimit = _context.BarAvordHamlNecessaryLimits.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId);
            if (barAvordHamlNecessaryLimit != null)
            {

                //در این حالت بایستی کیلومتراژ ها دارای محدودیت باشند
                _context.BarAvordHamlNecessaryLimits.Remove(barAvordHamlNecessaryLimit);

                foreach (var item in lstBarAvordHaml)
                {
                    clsOperation_ItemsFB? operation_ItemsFB = lstOperation_ItemsFB.FirstOrDefault(x => x.ItemsFBShomareh == item.FBShomarehHaml);
                    if (operation_ItemsFB != null)
                    {

                        clsOperationDetail? operationDetail = _context.OperationDetails.FirstOrDefault(x => x.OperationId == operation_ItemsFB.OperationId);
                        if (operationDetail != null)
                        {
                            clsBarAvordHamlRizMetre barAvordHamlRizMetre = lstBarAvordHamlRizMetre.First(x => x.BarAvordHamlId == item.ID);

                            List<clsRizMetreUsers> lstRizMetreUsers = _context.RizMetreUserses.Where(x => x.ID == barAvordHamlRizMetre.RizMetreId).ToList();
                            foreach (var RizMetreUser in lstRizMetreUsers)
                            {
                                decimal? dValue = 0;
                                decimal? dFinaValue = 0;
                                if (item.ValueBase > operationDetail.MaxValue)
                                    dValue = operationDetail.MaxValue;
                                else
                                    dValue = item.ValueBase;

                                dFinaValue = dValue - (operationDetail.MinValue == null ? 0 : operationDetail.MinValue.Value);

                                item.Value = dFinaValue;

                                _context.Entry(RizMetreUser).CurrentValues.SetValues(new
                                {
                                    Tedad = dFinaValue,
                                    MeghdarJoz = dFinaValue * RizMetreUser.Vazn
                                });
                            }
                        }
                    }
                }
            }
        }
        _context.SaveChanges();
        return new JsonResult("OK");
        //}
        //catch (Exception)
        //{
        //    return new JsonResult("NOK");
        //}
    }
    public ActionResult SaveHamlValue([FromBody] SaveHamlValueDto request)
    {
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        clsOperation_ItemsFB? operation_ItemsFB = _context.Operation_ItemsFBs.FirstOrDefault(x => x.OperationId == request.OperationId);
        if (operation_ItemsFB != null)
        {
            string ItemsFBShomareh = operation_ItemsFB.ItemsFBShomareh;
            Guid BarAvordId = request.BarAvordId;
            List<clsBarAvordHaml> lstBarAvordHaml = _context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.FBShomarehHaml == ItemsFBShomareh).ToList();
            if (lstBarAvordHaml.Count != 0)
            {
                foreach (var barAvordHaml in lstBarAvordHaml)
                {
                    decimal dValue = 0;
                    decimal dFinaValue = 0;
                    clsOperationDetail? operationDetail = _context.OperationDetails.FirstOrDefault(x => x.OperationId == request.OperationId);
                    if (operationDetail != null)
                    {
                        if (operationDetail.UseMinForInsert)
                        {
                            if (request.Value >= operationDetail.MinValue)
                            {
                                dFinaValue = request.Value;
                            }
                            else
                                dFinaValue = (operationDetail.MinValue == null ? 0 : operationDetail.MinValue.Value);
                        }
                        else
                        {
                            if (request.Value >= operationDetail.MinValue)
                            {
                                dValue = request.Value;
                            }
                            else
                                dValue = (operationDetail.MinValue == null ? 0 : operationDetail.MinValue.Value);

                            decimal? MaxValue = operationDetail.MaxValue;
                            if (MaxValue != null)
                            {
                                clsBarAvordHamlNecessaryLimit? barAvordHamlNecessaryLimit = _context.BarAvordHamlNecessaryLimits.FirstOrDefault(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId);
                                if (barAvordHamlNecessaryLimit == null)
                                {
                                    if (request.Value > MaxValue.Value)
                                        dValue = MaxValue.Value;
                                    else
                                        dValue = request.Value;
                                }
                            }
                            dFinaValue = dValue - (operationDetail.MinValue == null ? 0 : operationDetail.MinValue.Value);
                        }
                    }

                    _context.Entry(barAvordHaml).CurrentValues
                        .SetValues(new
                        {
                            ValueBase = request.Value,
                            Value = dFinaValue
                        });


                    List<clsBarAvordHamlRizMetre> lstBaravordHaml = _context.BarAvordHamlRizMetres.Where(x => x.BarAvordHamlId == barAvordHaml.ID).ToList();
                    List<clsRizMetreUsers> lstRizMetre = _context.RizMetreUserses.Where(x => lstBaravordHaml.Select(x => x.RizMetreId).Contains(x.ID)).ToList();

                    foreach (var item in lstRizMetre)
                    {
                        decimal? dMeghdarJoz = (item.Tool != null ? item.Tool : 1) * (item.Arz != null ? item.Arz : 1) *
                                               (item.Ertefa != null ? item.Ertefa : 1) * (item.Vazn != null ? item.Vazn : 1) * dFinaValue;
                        _context.Entry(item).CurrentValues.SetValues(new
                        {
                            Tedad = dFinaValue,
                            MeghdarJoz = dMeghdarJoz
                        });
                    }
                }
                _context.SaveChanges();
            }
        }
        return new JsonResult("OK");
    }


    [HttpPost]
    public ActionResult CheckOperationHasExistActiveCondition([FromBody] CheckOperationHasExistActiveConditionInputDto request)
    {
        int Year = int.Parse(request.Year);
        Guid BarAvordId = request.BarAvordId;
        long OperationId = long.Parse(request.OperationId);
        NoeFehrestBaha NoeFB = request.NoeFB;
        Guid FBId = request.FBId;

        clsOperation_ItemsFB operation_ItemsFB = _context.Operation_ItemsFBs.First(x => x.OperationId == OperationId);

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
                                           }).Where(x => x.ItemShomareh == operation_ItemsFB.ItemsFBShomareh && x.NoeFB == NoeFB).OrderBy(x => x.FieldType).ToList();

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
            ItemsAddingToFBForCheckOperationDto itemsAddingToFBForCheckOperation = ItemsAddingToFBs.Where(x => x.ItemsHasCondition_ConditionContextId == ItemHasCon.Id).Select(x => new
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
            }).First();
            CheckOperationConditions checkOperationConditions = new CheckOperationConditions();

            //checkOperationConditions.fnCheckOperationCondition(_context, itemsAddingToFBForCheckOperation, ItemsField, ItemHasCon,FBId,1);
        }

        return new JsonResult("");
    }
}
