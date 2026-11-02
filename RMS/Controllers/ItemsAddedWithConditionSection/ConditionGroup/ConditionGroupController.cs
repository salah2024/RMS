using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.ItemsAddedWithConditionSection.ConditionGroup.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ConditionGroup;

public class ConditionGroupController : Controller
{
    private readonly ApplicationDbContext _context;
    public ConditionGroupController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewConditionGroupForm");

        return View();

    }



    [HttpGet]
    public async Task<IActionResult> GetAll(
    NoeFehrestBaha noeFB,
    string search = "",
    int page = 1)
    {
        const int pageSize = 20;

        var query = _context.ConditionGroups
            .Where(x => x.NoeFB == noeFB);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>
                x.ConditionGroupName.Contains(search));
        }

        var total = await query.CountAsync();

        var list = await query
            .OrderBy(x => x.Order)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ConditionGroupDto
            {
                Id = x.Id,
                ConditionGroupName = x.ConditionGroupName,
                Order = x.Order,
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
        var item = await _context.ConditionGroups
            .Where(x => x.Id == id)
            .Select(x => new ConditionGroupDto
            {
                Id = x.Id,
                ConditionGroupName = x.ConditionGroupName,
                Order = x.Order,
                NoeFB = x.NoeFB
            })
            .FirstOrDefaultAsync();

        if (item == null)
            return NotFound();

        return Json(item);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ConditionGroupDto model)
    {
        if (string.IsNullOrWhiteSpace(model.ConditionGroupName))
            return BadRequest("نام گروه الزامی است.");

        bool exist = await _context.ConditionGroups.AnyAsync(x =>
            x.ConditionGroupName == model.ConditionGroupName &&
            x.NoeFB == model.NoeFB);

        if (exist)
            return BadRequest("این گروه قبلاً ثبت شده است.");

        int order = model.Order ?? 0;

        if (order == 0)
        {
            order = (await _context.ConditionGroups
                        .Where(x => x.NoeFB == model.NoeFB)
                        .MaxAsync(x => (int?)x.Order) ?? 0) + 1;
        }

        long newId = 1;

        var lastId = await _context.ConditionGroups
            .OrderByDescending(x => x.Id)
            .Select(x => (long?)x.Id)
            .FirstOrDefaultAsync();

        if (lastId.HasValue)
            newId = lastId.Value + 1;

        var entity = new clsConditionGroup
        {
            Id = newId,
            ConditionGroupName = model.ConditionGroupName.Trim(),
            Order = order,
            NoeFB = model.NoeFB
        };

        _context.ConditionGroups.Add(entity);

        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Update([FromBody] ConditionGroupDto model)
    {
        var entity = await _context.ConditionGroups
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (entity == null)
            return NotFound();

        bool exist = await _context.ConditionGroups.AnyAsync(x =>
            x.Id != model.Id &&
            x.NoeFB == model.NoeFB &&
            x.ConditionGroupName == model.ConditionGroupName);

        if (exist)
            return BadRequest("این گروه قبلاً ثبت شده است.");

        entity.ConditionGroupName = model.ConditionGroupName.Trim();
        entity.Order = model.Order;
        entity.NoeFB = model.NoeFB;

        await _context.SaveChangesAsync();

        return Ok();
    }


    //[HttpPost]
    //public async Task<IActionResult> Delete(long id)
    //{
    //    var entity = await _context.ConditionGroups
    //        .FirstOrDefaultAsync(x => x.Id == id);

    //    if (entity == null)
    //        return NotFound();

    //    _context.ConditionGroups.Remove(entity);

    //    await _context.SaveChangesAsync();

    //    return Ok();
    //}


    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var entity = await _context.ConditionGroups
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound();

        // بررسی وجود شرط برای این گروه
        bool hasCondition = await _context.ConditionContexts
            .AnyAsync(x => x.ConditionGroupId == id);

        if (hasCondition)
        {
            return BadRequest(new
            {
                message = "این گروه دارای شرط می‌باشد و قابل حذف نیست."
            });
        }

        _context.ConditionGroups.Remove(entity);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
