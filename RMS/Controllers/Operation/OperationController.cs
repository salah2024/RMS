using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using System.Numerics;
using static RMS.Models.Common.EnumForEntity;
using static Stimulsoft.Report.StiRecentConnections;

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


    [HttpPost]
    public JsonResult SearchTree([FromBody] SearchTreeInputDto request)
    {
        string term = (request.SearchText ?? "").Trim();
        if (term.Length < 2)
            return new JsonResult(new { NewOperation = new List<AllOperationDto>() });

        clsBaravordUser? baravordUser = _context.BaravordUsers
            .FirstOrDefault(x => x.ID == request.BarAvordUserId);
        if (baravordUser == null)
            return new JsonResult("NOK");

        var allowedNoeFBs = (baravordUser.NoeFBs ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.TryParse(x, out int value) ? (int?)value : null)
            .Where(x => x.HasValue)
            .Select(x => (NoeFehrestBaha)x!.Value)
            .ToList();

        var operations = _context.Operations
            .Where(x => x.Year == request.Year &&
                (x.NoeFB == null || allowedNoeFBs.Contains(x.NoeFB.Value)))
            .Select(x => new { x.Id, x.ParentId, x.Order, x.Year, x.OperationName, x.LatinName, x.FunctionCall, x.NoeFB })
            .ToList();

        var operationById = operations.ToDictionary(x => x.Id);
        var operationIds = operations.Select(x => x.Id).ToList();

        var itemRows = (
            from opItem in _context.Operation_ItemsFBs
            join fb in _context.FehrestBahas
                on opItem.ItemsFBShomareh.Substring(0, 6) equals fb.Shomareh.Trim()
            where operationIds.Contains(opItem.OperationId)
                  && fb.Sal == request.Year
                  && fb.NoeFB.HasValue && allowedNoeFBs.Contains(fb.NoeFB.Value)
                  && (opItem.ItemsFBShomareh.Contains(term) || fb.Sharh.Contains(term))
            select new { opItem.OperationId, opItem.ItemsFBShomareh, fb.Sharh, fb.NoeFB }
        ).ToList();

        var operationDetails = _context.OperationDetails
            .Where(x => operationIds.Contains(x.OperationId))
            .ToList()
            .GroupBy(x => x.OperationId)
            .ToDictionary(x => x.Key, x => x.First());

        var visibleHamlItems = _context.RizMetreUserses
            .Include(x => x.FB)
            .Where(x => x.FB.BarAvordId == request.BarAvordUserId)
            .Select(x => new { x.FB.Shomareh, x.FB.NoeFBId })
            .Distinct()
            .ToList();

        var result = new List<AllOperationDto>();
        var includedIds = new HashSet<long>();
        var includedItemKeys = new HashSet<string>();

        void AddAncestorChain(long? parentId)
        {
            var visited = new HashSet<long>();
            while (parentId.HasValue && parentId.Value != 0 &&
                   operationById.TryGetValue(parentId.Value, out var parent) &&
                   visited.Add(parent.Id))
            {
                if (includedIds.Add(parent.Id))
                {
                    result.Add(new AllOperationDto
                    {
                        ID = parent.Id,
                        order = parent.Order,
                        ParentId = parent.ParentId ?? 0,
                        Year = parent.Year,
                        OperationName = parent.OperationName ?? "",
                        LatinName = parent.LatinName ?? "",
                        FunctionCall = parent.FunctionCall ?? "",
                        Sharh = "",
                        ItemsFBShomareh = "",
                        ItemsFBShomarehMain = "",
                        SumVazn = 0,
                        NoeFB = parent.NoeFB
                    });
                }
                parentId = parent.ParentId;
            }
        }

        foreach (var op in operations)
        {
            bool nameMatch = (!string.IsNullOrWhiteSpace(op.OperationName) &&
                              op.OperationName.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                             (!string.IsNullOrWhiteSpace(op.LatinName) &&
                              op.LatinName.Contains(term, StringComparison.OrdinalIgnoreCase));
            if (!nameMatch) continue;

            AddAncestorChain(op.ParentId);
            if (includedIds.Add(op.Id))
            {
                result.Add(new AllOperationDto
                {
                    ID = op.Id,
                    order = op.Order,
                    ParentId = op.ParentId ?? 0,
                    Year = op.Year,
                    OperationName = op.OperationName ?? "",
                    LatinName = op.LatinName ?? "",
                    FunctionCall = op.FunctionCall ?? "",
                    Sharh = "",
                    ItemsFBShomareh = "",
                    ItemsFBShomarehMain = "",
                    SumVazn = 0,
                    NoeFB = op.NoeFB
                });
            }
        }

        foreach (var item in itemRows)
        {
            bool numberMatch = !string.IsNullOrWhiteSpace(item.ItemsFBShomareh) &&
                               item.ItemsFBShomareh.Contains(term, StringComparison.OrdinalIgnoreCase);
            bool descriptionMatch = !string.IsNullOrWhiteSpace(item.Sharh) &&
                                    item.Sharh.Contains(term, StringComparison.OrdinalIgnoreCase);
            if ((!numberMatch && !descriptionMatch) ||
                !operationById.TryGetValue(item.OperationId, out var op))
                continue;

            operationDetails.TryGetValue(op.Id, out var detail);
            if (detail?.CheckData == true &&
                !visibleHamlItems.Any(x => x.Shomareh == item.ItemsFBShomareh && x.NoeFBId == item.NoeFB))
                continue;

            AddAncestorChain(op.ParentId);
            string uniqueKey = op.Id + "|" + item.ItemsFBShomareh + "|" + (int)item.NoeFB;
            if (!includedItemKeys.Add(uniqueKey) || !includedIds.Add(op.Id)) continue;

            result.Add(new AllOperationDto
            {
                ID = op.Id,
                order = op.Order,
                ParentId = op.ParentId ?? 0,
                Year = op.Year,
                OperationName = op.OperationName ?? "",
                LatinName = op.LatinName ?? "",
                FunctionCall = op.FunctionCall ?? "",
                Sharh = item.Sharh ?? "",
                ItemsFBShomareh = item.ItemsFBShomareh ?? "",
                ItemsFBShomarehMain = item.ItemsFBShomareh ?? "",
                CheckData = detail?.CheckData,
                HasEnteringValue = detail?.HasEnteringValue,
                MaxValue = detail?.MaxValue,
                MinValue = detail?.MinValue,
                Description = detail?.Description,
                SumVazn = 0,
                NoeFB = item.NoeFB
            });
        }

        result = result.OrderBy(x => x.order).ThenBy(x => x.ID).ToList();
        return new JsonResult(new { NewOperation = result, BaravordType = baravordUser.Type });
    }

    public JsonResult GetTree([FromBody] GetTreeInputDto request)
    {
        clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == request.BarAvordUserId);
        if (baravordUser == null)
        {
            return new JsonResult("NOK");
        }
        else
        {

            List<AllOperationDto> operation1 = new List<AllOperationDto>();

            ////////////////////////////////////////////////////////
            ////ابتدا ضرایب با نوع فهرست بها برابر با نال اد میشود
            ////////////////////////////////////////////////////////

            List<AllOperationDto> operationNull = _context.Operations
                    .Where(x => x.Year == request.Year && x.NoeFB == null &&
                        (request.ParentId == null || request.ParentId == 0
                            ? (x.ParentId == null || x.ParentId == 0)
                            : x.ParentId == request.ParentId))
                    .Select(x => new AllOperationDto
                    {
                        ID = x.Id,
                        order = x.Order,
                        ParentId = x.ParentId,
                        Year = x.Year,
                        OperationName = x.OperationName ?? "",
                        LatinName = x.LatinName ?? "",
                        FunctionCall = x.FunctionCall ?? "",
                        Sharh = "",
                        ItemsFBShomareh = "",
                        CheckData = null,
                        HasEnteringValue = null,
                        MaxValue = null,
                        MinValue = null,
                        Description = null,
                        NoeFB = null,
                        CheckNecessary = null
                    }).ToList();


            operation1.AddRange(operationNull);


            ///////////////
            /////////////
            /////////////


            string[] NoeFBs = baravordUser.NoeFBs.Split(',');

            //NoeFehrestBaha NoeFBId = request.NoeFBId;

            foreach (var item in NoeFBs)
            {
                int NoeItem = int.Parse(item);
                var ShomarehItems = (from op_Item in _context.Operation_ItemsFBs
                                     join FB in _context.FehrestBahas
                                     on op_Item.ItemsFBShomareh.Substring(0, 6) equals FB.Shomareh.Trim()
                                     select new
                                     {
                                         ItemsFBShomareh = op_Item.ItemsFBShomareh,
                                         ItemsFBShomarehMain = op_Item.ItemsFBShomareh,
                                         OperationId = op_Item.OperationId,
                                         Sharh = FB.Sharh,
                                         Year = FB.Sal,
                                         NoeFB = FB.NoeFB,
                                         Vahed = FB.Vahed,
                                         BahayeVahed = FB.BahayeVahed,
                                     }).Where(x => x.Year == request.Year && x.NoeFB == (NoeFehrestBaha)NoeItem);

                var baseRows = (
                    from op in _context.Operations
                    where op.Year == request.Year && op.NoeFB == (NoeFehrestBaha)NoeItem
                          && (request.ParentId == null || request.ParentId == 0
                              ? (op.ParentId == null || op.ParentId == 0)
                              : op.ParentId == request.ParentId)
                    join od in _context.OperationDetails
                        on op.Id equals od.OperationId into odGroup
                    from od in odGroup.DefaultIfEmpty()
                    select new { op, od }
                ).ToList();

                var siLookup = ShomarehItems.ToLookup(s => s.OperationId);

                List<AllOperationDto> operation = baseRows
                    .SelectMany(x => siLookup[x.op.Id].DefaultIfEmpty(),
                        (x, si) => new AllOperationDto
                        {
                            ID = x.op.Id,
                            order = x.op.Order,
                            ParentId = x.op.ParentId,
                            Year = x.op.Year,
                            OperationName = x.op.OperationName ?? "",
                            LatinName = x.op.LatinName ?? "",
                            FunctionCall = x.op.FunctionCall ?? "",
                            Sharh = si?.Sharh ?? "",
                            ItemsFBShomareh = si?.ItemsFBShomareh ?? "",
                            ItemsFBShomarehMain = si?.ItemsFBShomarehMain ?? "",
                            CheckData = x.od?.CheckData,
                            HasEnteringValue = x.od?.HasEnteringValue,
                            MaxValue = x.od?.MaxValue,
                            MinValue = x.od?.MinValue,
                            Description = x.od?.Description,
                            NoeFB = x.op.NoeFB,
                            CheckNecessary = null,
                            SumVazn = null,
                        })
                    .OrderBy(x => x.order)
                    .ThenBy(x => x.ID)
                    .ToList();

                operation1.AddRange(operation);
            }

            List<AllOperationDto> NewOperation = new List<AllOperationDto>();


            List<string> lstShomareh = operation1.Where(x => x.CheckData == true).Select(x => x.ItemsFBShomarehMain).ToList();

            List<string> lstShomareh1 = new List<string>();
            foreach (var item in lstShomareh)
            {
                if (item.Trim() != "")
                {
                    lstShomareh1.Add(item.Trim().Substring(0, 6));
                }
            }
            List<clsItemsFields> lstItemsFields = _context.ItemsFieldses.Where(x => lstShomareh1.Contains(x.ItemShomareh)).ToList();

            foreach (var item in operation1)
            {
                if (item.LatinName != null)
                {
                    if (item.LatinName.Trim().ToLower() == "sharh")
                    {
                        clsBarAvordHamlNecessaryLimit? barAvordHamlNecessaryLimit = _context.BarAvordHamlNecessaryLimits.FirstOrDefault(x => x.BarAvordId == request.BarAvordUserId && x.NoeFBId == item.NoeFB);
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
                        clsBarAvordHaml? barAvordHaml = _context.BarAvordHamls.FirstOrDefault(x => x.BarAvordId == request.BarAvordUserId && x.NoeFBId == item.NoeFB && x.FBShomarehHaml == item.ItemsFBShomareh.Trim());

                        if (barAvordHaml != null)
                        {
                            item.OperationDefaultValue = barAvordHaml?.ValueBase == null ? 0 : barAvordHaml.ValueBase;
                            item.Value = barAvordHaml?.Value == null ? 0 : barAvordHaml.Value;
                        }

                        List<clsRizMetreUsers> lstRizMetreUsers =
                            _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == request.BarAvordUserId && x.FB.Shomareh == item.ItemsFBShomareh && x.FB.NoeFBId == item.NoeFB).ToList();


                        string strItemFBShomareh = "";
                        if (item.ItemsFBShomareh != "")
                        {
                            strItemFBShomareh = item.ItemsFBShomareh.Substring(0, 6);
                        }

                        clsItemsFields? currentItemsField = lstItemsFields.FirstOrDefault(x => x.ItemShomareh == strItemFBShomareh && x.NoeFB == item.NoeFB && x.FieldType == ItemFieldType.Vazn);

                        if (currentItemsField != null)
                        {
                            item.Vahed = currentItemsField.Vahed;
                        }

                        decimal? dSumVazn = 0;

                        foreach (var RM in lstRizMetreUsers)
                        {
                            dSumVazn += (RM.Tool != null ? RM.Tool : 1) * (RM.Arz != null ? RM.Arz : 1) * (RM.Ertefa != null ? RM.Ertefa : 1) * (RM.Vazn != null ? RM.Vazn : 1);
                        }


                        long RMCount = lstRizMetreUsers.Count();
                        if (RMCount != 0)
                        {
                            item.SumVazn = dSumVazn;
                            NewOperation.Add(item);
                        }

                        item.ItemsFBShomareh = item.ItemsFBShomareh.Trim() == "" ? "" : item.ItemsFBShomareh.Substring(0, 6);
                    }
                }
                else
                    NewOperation.Add(item);
            }

            var currentNodeIds = NewOperation
                .Where(x => string.IsNullOrWhiteSpace(x.ItemsFBShomareh))
                .Select(x => x.ID)
                .Distinct()
                .ToList();

            var parentIdsWithChildren = currentNodeIds.Count == 0
                ? new List<long>()
                : _context.Operations
                    .Where(x => x.Year == request.Year
                        && x.ParentId.HasValue
                        && currentNodeIds.Contains(x.ParentId.Value))
                    .Select(x => x.ParentId.Value)
                    .Distinct()
                    .ToList();

            foreach (var node in NewOperation)
                node.HasChildren = parentIdsWithChildren.Contains(node.ID);

            var Result = new
            {
                NewOperation,
                BaravordType = baravordUser.Type
            };

            return new JsonResult(Result);
        }
    }


    public JsonResult GetTreeForOneOperation([FromBody] GetTreeInputForOneDto request)
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
        //List<BHForChangeBarAvordHamlNL> 

        List<clsBarAvordHaml> lstBarAvordHaml = _context.BarAvordHamls.Where(x => x.BarAvordId == request.BarAvordId && x.NoeFBId == request.NoeFBId).ToList();

        //    .Select(x => new clsBarAvordHaml
        //{
        //    ID = x.ID,
        //    BarAvordId = x.BarAvordId,
        //    FBShomareh = x.FBShomareh,
        //    FBShomarehHaml = x.FBShomarehHaml,
        //    NoeFBId = x.NoeFBId,
        //    Value = x.Value,
        //    ValueBase = x.ValueBase,
        //}).ToList();

        List<string> lstBarAvordHamlShomarehHamls = lstBarAvordHaml.Select(x => x.FBShomarehHaml.Substring(0, 6)).ToList();
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

                        if (dValue < operationDetail.MinValue)
                            dValue = operationDetail.MinValue;

                        dFinaValue = dValue - (operationDetail.MinValue == null ? 0 : operationDetail.MinValue.Value);

                        item.Value = dFinaValue;
                        item.ValueBase = item.ValueBase;

                        List<clsBarAvordHamlRizMetre> BarAvordHamlRizMetres = lstBarAvordHamlRizMetre.Where(x => x.BarAvordHamlId == item.ID).ToList();

                        foreach (var barAvordHamlRizMetre in BarAvordHamlRizMetres)
                        {
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

                            decimal? dValue = item.ValueBase;
                            decimal? dFinaValue = 0;

                            if (dValue < operationDetail.MinValue)
                                dValue = operationDetail.MinValue;

                            if (dValue > operationDetail.MaxValue)
                                dValue = operationDetail.MaxValue;

                            dFinaValue = (operationDetail.MaxValue == null ? item.ValueBase : dValue) - (operationDetail.MinValue == null ? 0 : operationDetail.MinValue.Value);

                            item.Value = dFinaValue;
                            item.ValueBase = item.ValueBase;

                            List<clsBarAvordHamlRizMetre> BarAvordHamlRizMetres = lstBarAvordHamlRizMetre.Where(x => x.BarAvordHamlId == item.ID).ToList();

                            foreach (var barAvordHamlRizMetre in BarAvordHamlRizMetres)
                            {
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
            }
        }
        _context.SaveChanges();
        return new JsonResult(lstBarAvordHaml);
        //}
        //catch (Exception)
        //{
        //    return new JsonResult("NOK");
        //}
    }
    public async Task<ActionResult> SaveHamlValue([FromBody] SaveHamlValueDto request)
    {
        Guid BarAvordId = request.BarAvordId;

        clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordId);
        if (baravordUser != null)
        {
            if (baravordUser.Type != NoeBarAvord.BarAvord)
            {
                return new JsonResult("NOK");
            }
        }

        NoeFehrestBaha NoeFBId = request.NoeFBId;
        clsOperation_ItemsFB? operation_ItemsFB = await _context.Operation_ItemsFBs.FirstOrDefaultAsync(x => x.OperationId == request.OperationId);
        decimal dFinaValue = 0;
        if (operation_ItemsFB != null)
        {
            string ItemsFBShomareh = operation_ItemsFB.ItemsFBShomareh;
            List<clsBarAvordHaml> lstBarAvordHaml = await _context.BarAvordHamls.Where(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.FBShomarehHaml == ItemsFBShomareh).ToListAsync();
            if (lstBarAvordHaml.Count != 0)
            {
                foreach (var barAvordHaml in lstBarAvordHaml)
                {
                    decimal dValue = 0;
                    dFinaValue = 0;
                    clsOperationDetail? operationDetail = await _context.OperationDetails.FirstOrDefaultAsync(x => x.OperationId == request.OperationId);
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
                                clsBarAvordHamlNecessaryLimit? barAvordHamlNecessaryLimit = await _context.BarAvordHamlNecessaryLimits.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId);
                                if (barAvordHamlNecessaryLimit == null)
                                {
                                    if (dValue > MaxValue.Value)
                                        dValue = MaxValue.Value;
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


                    List<clsBarAvordHamlRizMetre> lstBaravordHaml = await _context.BarAvordHamlRizMetres.Where(x => x.BarAvordHamlId == barAvordHaml.ID).ToListAsync();
                    List<clsRizMetreUsers> lstRizMetre = await _context.RizMetreUserses.Where(x => lstBaravordHaml.Select(x => x.RizMetreId).Contains(x.ID)).ToListAsync();

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
               await _context.SaveChangesAsync();
            }
        }
        return new JsonResult("OK_" + dFinaValue);
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
