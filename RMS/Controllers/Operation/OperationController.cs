using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Dto.ItemsFieldsDto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation
{
    public class OperationController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ShowTree()
        {
            return PartialView();
        }


        public JsonResult GetTree([FromBody] GetTreeInputDto request)
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
                                 }).Where(x => x.Year == request.Year && x.NoeFB == request.NoeFB);

            var operation = (from op in _context.Operations
                             join Sh_Items in ShomarehItems
                             on op.Id equals Sh_Items.OperationId
                             into joinTable
                             from jT in joinTable.DefaultIfEmpty()
                             where op.Year == request.Year
                             select new
                             {
                                 ID = op.Id,
                                 order = op.Order,
                                 ParentId = op.ParentId,
                                 OperationName = op.OperationName,
                                 FunctionCall = op.FunctionCall == null ? "" : op.FunctionCall,
                                 Sharh = jT.Sharh,
                                 ItemsFBShomareh = jT.ItemsFBShomareh == null ? "" : jT.ItemsFBShomareh,
                                 Year = op.Year,
                             })
                             .OrderBy(x => x.order)
                             .ThenBy(x => x.ID)
                             .ToList();
            return new JsonResult(operation);
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
}
