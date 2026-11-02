using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.ItemsAddedWithConditionSection.ItemHasCondition.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ItemHasCondition;

public class ItemsHasConditionController : Controller
{
    private readonly ApplicationDbContext _context;
    public ItemsHasConditionController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewItemsHasConditionForm");

        return View();

    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int year,
    NoeFehrestBaha noeFB,
    string search = "",
    int page = 1)
    {
        const int pageSize = 20;

        var query = _context.ItemsHasConditions
            .Where(x => x.Year == year &&
                        x.NoeFB == noeFB);


        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>
                x.ItemFBShomareh.Contains(search));
        }


        int total = await query.CountAsync();

        var list = await query
            .OrderBy(x => x.ItemFBShomareh)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Json(new
        {
            items = list,
            page = page,
            totalPages = (int)Math.Ceiling(total / (double)pageSize),
            totalCount = total
        });
    }

    [HttpGet]
    public async Task<IActionResult> Get(long id)
    {
        var item = await _context.ItemsHasConditions
            .Where(x => x.Id == id)
            .Select(x => new ItemsHasConditionDto
            {
                Id = x.Id,
                ItemFBShomareh = x.ItemFBShomareh,
                Year = x.Year,
                NoeFB = x.NoeFB
            })
            .FirstOrDefaultAsync();

        if (item == null)
            return NotFound();

        return Json(item);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ItemsHasConditionDto model)
    {
        if (string.IsNullOrWhiteSpace(model.ItemFBShomareh))
            return BadRequest("شماره آیتم الزامی است.");

        bool exist = await _context.ItemsHasConditions.AnyAsync(x =>
                x.ItemFBShomareh == model.ItemFBShomareh &&
                x.Year == model.Year &&
                x.NoeFB == model.NoeFB);

        if (exist)
            return BadRequest("این آیتم قبلاً ثبت شده است.");

        long newId = 1;

        var lastId = await _context.ItemsHasConditions
            .OrderByDescending(x => x.Id)
            .Select(x => (long?)x.Id)
            .FirstOrDefaultAsync();

        if (lastId.HasValue)
            newId = lastId.Value + 1;

        clsItemsHasCondition entity = new()
        {
            Id = newId,
            ItemFBShomareh = model.ItemFBShomareh,
            Year = model.Year,
            NoeFB = model.NoeFB
        };

        _context.ItemsHasConditions.Add(entity);

        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Update([FromBody] ItemsHasConditionDto model)
    {
        var entity = await _context.ItemsHasConditions
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (entity == null)
            return NotFound();

        entity.ItemFBShomareh = model.ItemFBShomareh;
        entity.Year = model.Year;
        entity.NoeFB = model.NoeFB;

        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {
        var entity = await _context.ItemsHasConditions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound();

        _context.ItemsHasConditions.Remove(entity);

        await _context.SaveChangesAsync();

        return Ok();
    }




}
