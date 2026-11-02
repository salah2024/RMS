using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.ItemsAddedWithConditionSection.ConditionContext.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ConditionContextBase;

public class ConditionContextBaseController : Controller
{
    private readonly ApplicationDbContext _context;
    public ConditionContextBaseController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewConditionContextForm");

        return View();

    }


    [HttpGet]
    public async Task<IActionResult> GetGroups(int ConditionType, NoeFehrestBaha noeFB,int Year)
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
        //        order = x.Order
        //    })
        //    .ToListAsync();

        return Json(list);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
    int year,
    NoeFehrestBaha noeFB,
    long? conditionGroupId,
    string search = "",
    int page = 1)
    {
        const int pageSize = 20;

        var query = _context.ConditionContexts
            .Include(x => x.ConditionGroup)
            .Where(x => x.Year == year &&
                        x.NoeFB == noeFB);

        if (conditionGroupId.HasValue && conditionGroupId.Value > 0)
        {
            query = query.Where(x => x.ConditionGroupId == conditionGroupId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>

                x.Context.Contains(search)

                ||

                x.ConditionGroup.ConditionGroupName.Contains(search)

                ||

                (x.Des ?? "").Contains(search)

            );
        }

        var total = await query.CountAsync();

        var list = await query

            .OrderBy(x => x.ConditionGroup.Order)

            .ThenBy(x => x.Id)

            .Skip((page - 1) * pageSize)

            .Take(pageSize)

            .Select(x => new ConditionContextDto
            {
                Id = x.Id,

                Context = x.Context,

                ConditionGroupId = x.ConditionGroupId,

                ConditionGroupName = x.ConditionGroup.ConditionGroupName,

                Year = x.Year,

                //HasDes = x.HasDes,

                Des = x.Des,

                TitleDes = x.TitleDes,

                ConditionContextRel = x.ConditionContextRel,

                NoeFB = x.NoeFB

            })

            .ToListAsync();

        return Json(new
        {
            items = list,

            page,

            totalPages = (int)Math.Ceiling(total / (double)pageSize),

            totalCount = total
        });
    }


    [HttpGet]
    public async Task<IActionResult> Get(long id)
    {
        var item = await _context.ConditionContexts

            .Where(x => x.Id == id)

            .Select(x => new ConditionContextDto
            {
                Id = x.Id,

                Context = x.Context,

                ConditionGroupId = x.ConditionGroupId,

                Year = x.Year,

                //HasDes = x.HasDes,

                Des = x.Des,

                TitleDes = x.TitleDes,

                ConditionContextRel = x.ConditionContextRel,

                NoeFB = x.NoeFB
            })

            .FirstOrDefaultAsync();

        if (item == null)
            return NotFound();

        return Json(item);
    }


    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ConditionContextDto model)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(model.Context))
                return BadRequest("متن شرط الزامی است.");

            var entity = new clsConditionContext
            {
                Context = model.Context.Trim(),

                ConditionGroupId = model.ConditionGroupId,

                Year = model.Year,

                Des = model.Des == null ? "" : model.Des,

                TitleDes = model.TitleDes == null ? "" : model.TitleDes,

                ConditionContextRel = model.ConditionContextRel,

                NoeFB = model.NoeFB,

                HasDes = !string.IsNullOrWhiteSpace(model.Des),
            };

            _context.ConditionContexts.Add(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {

            throw;
        }
    }


    [HttpPost]
    public async Task<IActionResult> Update([FromBody] ConditionContextDto model)
    {
        var entity = await _context.ConditionContexts
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (entity == null)
            return NotFound();

        entity.Context = model.Context.Trim();

        entity.ConditionGroupId = model.ConditionGroupId;

        entity.Year = model.Year;

        entity.Des = model.Des == null ? "" : model.Des;

        entity.TitleDes = model.TitleDes == null ? "" : model.TitleDes;

        entity.ConditionContextRel = model.ConditionContextRel;

        entity.NoeFB = model.NoeFB;

        entity.HasDes = !string.IsNullOrWhiteSpace(model.Des);

        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var entity = await _context.ConditionContexts
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound();

        _context.ConditionContexts.Remove(entity);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
