using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsFields;

public class ItemsFieldsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ItemsFieldsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // =========================================================
    // Index
    // =========================================================

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewItemsFieldsForm");

        return View();
    }


    [HttpGet]
    public IActionResult GetNoeFehrestBaha()
    {
        var result = Enum.GetValues<NoeFehrestBaha>()
            .Select(x => new
            {
                Id = (int)x,
                Name = x switch
                {
                    NoeFehrestBaha.RahDari => "راهداری",
                    NoeFehrestBaha.Abnie => "ابنیه",
                    NoeFehrestBaha.RahoBand => "راه، راه‌آهن و باند فرودگاه",
                    NoeFehrestBaha.Barghi => "تأسیسات برقی",
                    NoeFehrestBaha.Mekaniki => "تأسیسات مکانیکی",
                    NoeFehrestBaha.MareMat => "مرمت",
                    _ => x.ToString()
                }
            })
            .ToList();

        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        string? itemShomareh,
        int? noeFB,
        int page = 1,
        int pageSize = 20)
    {
        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 20;

        if (pageSize > 100)
            pageSize = 100;

        var query = _context.ItemsFieldses
            .AsNoTracking()
            .AsQueryable();


        // جستجو فقط روی شماره فهرست بها
        if (!string.IsNullOrWhiteSpace(itemShomareh))
        {
            itemShomareh = itemShomareh.Trim();

            query = query.Where(x =>
                x.ItemShomareh.Contains(itemShomareh));
        }


        // نوع فهرست بها
        if (noeFB.HasValue)
        {
            query = query.Where(x =>
                (int)x.NoeFB == noeFB.Value);
        }


        // تعداد کل
        var totalCount = await query.CountAsync();


        // تعداد صفحات
        var totalPages = (int)Math.Ceiling(
            totalCount / (double)pageSize);


        if (totalPages > 0 && page > totalPages)
            page = totalPages;


        // اطلاعات صفحه جاری
        var data = await query

            .OrderBy(x => x.ItemShomareh)
            .ThenBy(x => x.Id)

            .Skip((page - 1) * pageSize)
            .Take(pageSize)

            .Select(x => new
            {
                x.Id,
                x.ItemShomareh,
                x.Vahed,
                x.IsEnteringValue,
                x.EssentialValue,
                x.DefaultValue,
                x.MaxValue,
                x.MinValue,

                NoeFBName =
                    x.NoeFB == NoeFehrestBaha.RahDari
                        ? "راهداری" :

                    x.NoeFB == NoeFehrestBaha.Abnie
                        ? "ابنیه" :

                    x.NoeFB == NoeFehrestBaha.RahoBand
                        ? "راه، راه‌آهن و باند فرودگاه" :

                    x.NoeFB == NoeFehrestBaha.Barghi
                        ? "تأسیسات برقی" :

                    x.NoeFB == NoeFehrestBaha.Mekaniki
                        ? "تأسیسات مکانیکی" :

                    x.NoeFB == NoeFehrestBaha.MareMat
                        ? "مرمت" :

                    "",

                FieldTypeName =
                    x.FieldType == ItemFieldType.Tedad
                        ? "تعداد" :

                    x.FieldType == ItemFieldType.Tool
                        ? "طول" :

                    x.FieldType == ItemFieldType.Arz
                        ? "عرض" :

                    x.FieldType == ItemFieldType.Ertefa
                        ? "ارتفاع" :

                    x.FieldType == ItemFieldType.Vazn
                        ? "وزن" :

                    x.FieldType == ItemFieldType.MeghdarJoz
                        ? "مقدار جزء" :

                    ""
            })

            .ToListAsync();


        return Json(new
        {
            data,
            page,
            pageSize,
            totalCount,
            totalPages
        });
    }
    // =========================================================
    // Get One
    // =========================================================

    [HttpGet]
    public async Task<IActionResult> Get(long id)
    {
        var item = await _context.ItemsFieldses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return NotFound();

        return Json(new
        {
            item.Id,
            item.ItemShomareh,
            item.FieldType,
            item.Vahed,
            item.IsEnteringValue,
            item.EssentialValue,
            item.DefaultValue,
            item.MaxValue,
            item.MinValue,
            NoeFB = (int)item.NoeFB
        });
    }


    // =========================================================
    // Create
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(clsItemsFields model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                .Select(x => new
                {
                    Field = x.Key,
                    Errors = x.Value!.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList()
                })
                .ToList();

            return BadRequest(new
            {
                success = false,
                message = "اطلاعات ارسال شده نامعتبر است.",
                errors
            });
        }


        try
        {
            model.ItemShomareh = model.ItemShomareh?.Trim();


            _context.ItemsFieldses.Add(model);

            await _context.SaveChangesAsync();


            return Json(new
            {
                success = true,
                message = "رکورد با موفقیت ثبت شد.",
                id = model.Id
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = ex.InnerException?.Message ?? ex.Message
            });
        }
    }

    // =========================================================
    // Update
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(clsItemsFields model)
    {
        if (model.Id <= 0)
        {
            return Json(new
            {
                success = false,
                message = "شناسه رکورد نامعتبر است."
            });
        }

        if (string.IsNullOrWhiteSpace(model.ItemShomareh))
        {
            return Json(new
            {
                success = false,
                message = "شماره آیتم را وارد کنید."
            });
        }

        var item = await _context.ItemsFieldses
            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (item == null)
        {
            return Json(new
            {
                success = false,
                message = "رکورد مورد نظر پیدا نشد."
            });
        }

        //bool exists = await _context.ItemsFieldses.AnyAsync(x =>
        //    x.Id != model.Id &&
        //    x.ItemShomareh == model.ItemShomareh.Trim() &&
        //    x.NoeFB == model.NoeFB);

        //if (exists)
        //{
        //    return Json(new
        //    {
        //        success = false,
        //        message = "رکورد مشابهی وجود دارد."
        //    });
        //}

        item.ItemShomareh = model.ItemShomareh.Trim();
        item.FieldType = model.FieldType;
        item.Vahed = model.Vahed;
        item.IsEnteringValue = model.IsEnteringValue;
        item.EssentialValue = model.EssentialValue;
        item.DefaultValue = model.DefaultValue;
        item.MaxValue = model.MaxValue;
        item.MinValue = model.MinValue;
        item.NoeFB = model.NoeFB;

        await _context.SaveChangesAsync();

        return Json(new
        {
            success = true,
            message = "رکورد با موفقیت ویرایش شد."
        });
    }


    // =========================================================
    // Delete
    // =========================================================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        var item = await _context.ItemsFieldses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            return Json(new
            {
                success = false,
                message = "رکورد مورد نظر پیدا نشد."
            });
        }

        _context.ItemsFieldses.Remove(item);

        await _context.SaveChangesAsync();

        return Json(new
        {
            success = true,
            message = "رکورد با موفقیت حذف شد."
        });
    }


    // =========================================================
    // Enum Name
    // =========================================================

    private static string GetNoeFBName(NoeFehrestBaha noe)
    {
        return noe switch
        {
            NoeFehrestBaha.RahDari => "راه‌داری",
            NoeFehrestBaha.Abnie => "ابنیه",
            NoeFehrestBaha.RahoBand => "راه، راه آهن و باند فرودگاه",
            NoeFehrestBaha.Barghi => "برقی",
            NoeFehrestBaha.Mekaniki => "مکانیکی",
            NoeFehrestBaha.MareMat => "مرمت",
            _ => noe.ToString()
        };
    }
}

