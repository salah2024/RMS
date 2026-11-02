using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.ItemsAddedWithConditionSection.ItemsHasConditionConditionContext.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ItemsHasConditionConditionContext;

public class ItemsHasConditionConditionContextController : Controller
{
    private readonly ApplicationDbContext _context;


    public ItemsHasConditionConditionContextController(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewItemsHasConditionConditionContextForm");

        return View();

    }


    [HttpGet]
    public async Task<IActionResult> GetItems(
    int year,
    NoeFehrestBaha noeFB)
    {

        var list = await _context.ItemsHasConditions

            .Where(x =>

                x.Year == year

                &&

                x.NoeFB == noeFB

            )

            .Select(x => new
            {
                id = x.Id,

                text = x.ItemFBShomareh

            })

            .OrderBy(x => x.text)

            .ToListAsync();



        return Json(list);

    }


    [HttpGet]
    public async Task<IActionResult> GetItemsUseCondition(
    long ConditionContextId)
    {

        List<ItemFBShomarehUseConditionDto> list = await _context.ItemsHasCondition_ConditionContexts.Include(x => x.ItemsHasCondition)
             .Where(x => x.ConditionContextId == ConditionContextId).Select(x => new ItemFBShomarehUseConditionDto
             {
                 Id = x.Id,
                 ItemFBShomareh = x.ItemsHasCondition.ItemFBShomareh

             }).ToListAsync();

        int ConditionType = 0;
        if (list.Count != 0)
        {
            clsItemsAddingToFB? currentItemsAddingToFBs = await _context.ItemsAddingToFBs.FirstOrDefaultAsync(x => x.ItemsHasCondition_ConditionContextId == list.First().Id);
            if (currentItemsAddingToFBs != null)
            {
                ConditionType = currentItemsAddingToFBs.ConditionType;
            }
        }

        var result = new ResultGetItemsUseConditionDto
        {
            lstShomarehUse = list,
            ConditionType = ConditionType

        };

        return Json(result);

    }


    [HttpGet]
    public async Task<IActionResult> GetGroups(
    int ConditionType, NoeFehrestBaha noeFB, int Year)
    {


        var conditionGroupIds = await _context.ItemsAddingToFBs
           .Where(x =>
           x.ConditionType == ConditionType &&
           x.NoeFB == noeFB &&
           x.Year == Year)
               .Join(
       _context.ItemsHasCondition_ConditionContexts,
           item => item.ItemsHasCondition_ConditionContextId,
           context => context.Id,
           (item, context) => context.ConditionContextId)
           .Join(
       _context.ConditionContexts,
       conditionContextId => conditionContextId,
       conditionContext => conditionContext.Id,
       (conditionContextId, conditionContext) => conditionContext.ConditionGroupId
           )
           .Distinct()
           .ToListAsync();


        var query = _context.ConditionGroups
            .Where(x => x.NoeFB == noeFB);

        if (conditionGroupIds.Any())
        {
            query = query.Where(x => conditionGroupIds.Contains(x.Id));
        }

        var list = await query
            .OrderBy(x => x.Order)
            .Select(x => new
            {
                id = x.Id,
                text = x.ConditionGroupName,
                order = x.Order
            })
            .ToListAsync();

        //var list = await _context.ConditionGroups

        //    .Where(x => x.NoeFB == noeFB)

        //    .OrderBy(x => x.Order)

        //    .Select(x => new
        //    {

        //        id = x.Id,

        //        text = x.ConditionGroupName,
        //        order=x.Order

        //    })

        //    .ToListAsync();



        return Json(list);

    }


    [HttpGet]
    public async Task<IActionResult> GetConditions(
    long groupId,
    int year,
    NoeFehrestBaha noeFB)
    {


        var list = await _context.ConditionContexts

        .Where(x =>
            x.ConditionGroupId == groupId &&
            x.Year == year &&
            x.NoeFB == noeFB)

        .OrderBy(x => x.Context)

        .Select(x => new
        {
            id = x.Id,
            text = x.Context
        })


        .ToListAsync();

        return Json(list);

    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
    int year,
    NoeFehrestBaha noeFB,
    string itemFBShomareh,
    string search = "",
    int page = 1)
    {

        int pageSize = 20;


        var query =
      from ihcc in _context.ItemsHasCondition_ConditionContexts

      join item in _context.ItemsHasConditions
          on ihcc.ItemsHasConditionId equals item.Id

      join condition1 in _context.ConditionContexts
          on ihcc.ConditionContextId equals condition1.Id

      join conditionGroup in _context.ConditionGroups
          on condition1.ConditionGroupId equals conditionGroup.Id

      where ihcc.Year == year
            && ihcc.NoeFB == noeFB

      select new
      {
          ihcc.Id,

          // آیتم
          ItemsHasConditionId = item.Id,
          ItemFBShomareh = item.ItemFBShomareh,

          // گروه شرط
          ConditionGroupId = conditionGroup.Id,
          ConditionGroupName = conditionGroup.ConditionGroupName,

          // شرط
          ConditionContextId = condition1.Id,
          ConditionContextText = condition1.Context,

          // توضیحات
          ihcc.Des,

          // شرط مرتبط
          ihcc.IHC_CC_Related,

          // تعداد ورودی
          ihcc.EnteringCount,

          // توضیح مقادیر ورودی
          ihcc.DesForEnteringValue,

          // مقادیر
          ihcc.DefaultValue,
          ihcc.MinValue,
          ihcc.MaxValue,
          ihcc.StepChange,

          // وضعیت‌ها
          ihcc.HasEnteringValue,
          ihcc.IsShow,
          ihcc.AutoSelecting,
          ihcc.EnableEditing,
          ihcc.EnableDeleting,
          ihcc.MoveToRel,
          ihcc.ViewCheckAllRecords,

          // مشخصات
          ihcc.Year,
          ihcc.NoeFB,

      };



        //if (itemId.HasValue && itemId.Value > 0)
        //{
        //    query =
        //        query.Where(x =>
        //        x.ItemsHasConditionId == itemId);
        //}

        if (!string.IsNullOrWhiteSpace(itemFBShomareh))
        {
            query = query.Where(x =>
                x.ItemFBShomareh == itemFBShomareh);
        }


        if (!string.IsNullOrWhiteSpace(search))
        {

            search = search.Trim();


            query =
                query.Where(x =>

                    x.ItemFBShomareh.Contains(search)

                    ||

                    x.ConditionContextText.Contains(search)

                    ||

                    x.ConditionGroupName.Contains(search)

                );

        }


        var total =
            await query.CountAsync();



        var data =
            await query

            .OrderBy(x => x.ItemFBShomareh)

            .Skip((page - 1) * pageSize)

            .Take(pageSize)

            .ToListAsync();



        return Json(new
        {

            items = data,

            page,

            totalPages =
            (int)Math.Ceiling(
                total / (double)pageSize)

        });


    }


    [HttpGet]
    public async Task<IActionResult> Get(long id)
    {
        var data = await _context
            .Set<clsItemsHasCondition_ConditionContext>()
            .Include(x => x.ItemsHasCondition)
            .Include(x => x.ConditionContext)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (data == null)
            return NotFound();

        return Json(new
        {
            data.Id,

            // آیتم
            data.ItemsHasConditionId,
            ItemFBShomareh = data.ItemsHasCondition.ItemFBShomareh,

            // گروه و شرط
            data.ConditionContextId,
            ConditionGroupId = data.ConditionContext.ConditionGroupId,

            // توضیحات
            data.Des,

            // شرط مرتبط
            data.IHC_CC_Related,

            // تعداد ورودی
            data.EnteringCount,

            // توضیح مقادیر ورودی
            data.DesForEnteringValue,

            // مقادیر
            data.DefaultValue,
            data.MinValue,
            data.MaxValue,
            data.StepChange,

            // وضعیت‌ها
            data.HasEnteringValue,
            data.IsShow,
            data.MoveToRel,
            data.AutoSelecting,
            data.EnableEditing,
            data.EnableDeleting,
            data.ViewCheckAllRecords,

            // سایر
            data.Year,
            data.NoeFB
        });
    }

    [HttpPost]
    public async Task<IActionResult> Save(
    [FromBody] clsItemsHasCondition_ConditionContext model)
    {


        bool exists =
            await _context
            .Set<clsItemsHasCondition_ConditionContext>()

            .AnyAsync(x =>

                x.ItemsHasConditionId ==
                model.ItemsHasConditionId

                &&

                x.ConditionContextId ==
                model.ConditionContextId

                &&

                x.Year ==
                model.Year

                &&

                x.NoeFB ==
                model.NoeFB
            );



        if (exists)
        {

            return BadRequest(
            "این شرط قبلاً برای این آیتم ثبت شده است.");

        }

        model.HasEnteringValue =
            model.EnteringCount > 0;


        long newId = 1;

        var lastId = await _context.ItemsHasCondition_ConditionContexts
            .OrderByDescending(x => x.Id)
            .Select(x => (long?)x.Id)
            .FirstOrDefaultAsync();

        if (lastId.HasValue)
            newId = lastId.Value + 1;

        model.Id = newId;



        _context
        .Add(model);



        await _context.SaveChangesAsync();



        return Ok();

    }




    [HttpPost]
    public async Task<IActionResult> Update(
    [FromBody] clsItemsHasCondition_ConditionContext model)
    {

        var entity =
            await _context
            .Set<clsItemsHasCondition_ConditionContext>()

            .FirstOrDefaultAsync(x => x.Id == model.Id);



        if (entity == null)

            return NotFound();



        entity.ItemsHasConditionId =
            model.ItemsHasConditionId;


        entity.ConditionContextId =
            model.ConditionContextId;


        entity.HasEnteringValue =
            model.HasEnteringValue;


        entity.DefaultValue =
            model.DefaultValue;


        entity.MinValue =
            model.MinValue;


        entity.MaxValue =
            model.MaxValue;


        entity.StepChange =
            model.StepChange;


        entity.IsShow =
            model.IsShow;


        entity.MoveToRel =
            model.MoveToRel;


        entity.ViewCheckAllRecords =
            model.ViewCheckAllRecords;


        entity.EnteringCount =
            model.EnteringCount;


        entity.DesForEnteringValue =
            model.DesForEnteringValue;


        entity.AutoSelecting =
             model.AutoSelecting;


        entity.IHC_CC_Related =
            model.IHC_CC_Related;

        entity.HasEnteringValue =
             model.EnteringCount > 0;


        entity.Des =
                 model.Des;


        entity.EnableEditing =
            model.EnableEditing;


        entity.EnableDeleting =
            model.EnableDeleting;


        entity.IHC_CC_Related =
            model.IHC_CC_Related;

        await _context.SaveChangesAsync();


        return Ok();

    }





    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {


        var entity =
            await _context
            .Set<clsItemsHasCondition_ConditionContext>()

            .FindAsync(id);



        if (entity == null)

            return NotFound();



        _context.Remove(entity);



        await _context.SaveChangesAsync();



        return Ok();

    }







}
