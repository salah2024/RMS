using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.ConditionContext.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;

namespace RMS.Controllers.ConditionContext;

public class ConditionContextController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public ActionResult GetConditionContextWithId([FromBody] RequestGetConditionContextWithIdDto request)
    {
        clsItemsHasCondition_ConditionContext? itemsHasCondition_ConditionContext = _context.ItemsHasCondition_ConditionContexts.FirstOrDefault(x => x.Id == request.Id);

        List<ResultCC_RelatedDto> CC_Related = new List<ResultCC_RelatedDto>();
        if (itemsHasCondition_ConditionContext != null)
        {
            if (itemsHasCondition_ConditionContext.IHC_CC_Related != null)
            {
                string[] CC_RelatedSplit = itemsHasCondition_ConditionContext.IHC_CC_Related.Split("_");
                if (CC_RelatedSplit.Length > 1)
                {
                    foreach (var item in CC_RelatedSplit)
                    {
                        ResultItemsHasCondition_ConditionContextDto? CC =
                            _context.ItemsHasCondition_ConditionContexts.Include(x => x.ConditionContext)
                                .Where(x => x.Id == long.Parse(item)).Select(x => new ResultItemsHasCondition_ConditionContextDto
                                {
                                    ConditionGroupId = x.ConditionContext.ConditionGroupId,
                                }).First();

                        ResultCC_RelatedDto resultCC = new ResultCC_RelatedDto
                        {
                            RelatedConditionGroupId = CC.ConditionGroupId,
                            Related = long.Parse(item)
                        };

                        CC_Related.Add(resultCC);
                    }
                }
            }

            return new JsonResult(CC_Related);
        }
        return new JsonResult(null);
    }

    [HttpPost]
    public ActionResult CheckItemHasConditionContext([FromBody] RequestCheckItemHasConditionContextDto request)
    {

        long CurrentId = request.Id;

        string? IHC_CC_Related = "";

        List<clsItemsHasCondition_ConditionContext> lstitemsHasCondition_ConditionContext = _context.ItemsHasCondition_ConditionContexts.Where(x => x.NoeFB == request.NoeFB && x.IHC_CC_Related != null).ToList();

        clsItemsHasCondition_ConditionContext? ItemsHasCondition_ConditionContext = _context.ItemsHasCondition_ConditionContexts.FirstOrDefault(x => x.Id == CurrentId);

        if (ItemsHasCondition_ConditionContext!=null)
        {
            IHC_CC_Related = ItemsHasCondition_ConditionContext.IHC_CC_Related;
        }

        //پیدا نمودن آیتم هایی که به آیدی ارسالی مرتبط هستن
        //تا چک شوند که حتما آن آیتم ها موقع تیک خوردن آیتم جاری تیک خورده باشند
        //در صورتی که هیچ آیتمی یافت نشود در نتیجه این آیتم هیچ پیش انتخابی نداشته و میتواند تیک بخورد


        List<string> ItemRelated = new List<string>();
        if (lstitemsHasCondition_ConditionContext.Count != 0)
        {
            foreach (var item in lstitemsHasCondition_ConditionContext)
            {
                //string[] IHCSplit = item.IHC_CC_Related.Split('_');
                //foreach (var IHC in IHCSplit)
                //{
                //    if (IHC.Trim() != "")
                //    {

                //    }
                //}

                if (item.IHC_CC_Related.Contains(CurrentId.ToString()))
                {
                    if (item.IHC_CC_Related.ToString().Contains('&'))
                    {
                        string strItemId = item.Id + "&";
                        ItemRelated.Add(strItemId);
                    }
                    else
                        ItemRelated.Add(item.Id.ToString());
                }
            }

            ResultCheckItemHasConditionContextDto resultCheckItemHasConditionContext = new ResultCheckItemHasConditionContextDto
            {
                IHC_CC_Related = IHC_CC_Related,
                ItemRelated = ItemRelated
            };

            return new JsonResult(resultCheckItemHasConditionContext);
        }
        else
            return new JsonResult(null);
    }

    public IActionResult Index()
    {
        return View();
    }
}
