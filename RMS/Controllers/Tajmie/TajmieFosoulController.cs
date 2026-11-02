using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Tajmie;

public class TajmieFosoulController : Controller
{
    private readonly ApplicationDbContext _context;
    public TajmieFosoulController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewTajmieFosoulForm");

        return View();

    }

    [HttpGet]
    public async Task<IActionResult> GetAllTajmieFosoul(NoeFehrestBaha? noeFBId)
    {
        var query = _context.TajmieFosouls.AsQueryable();

        if (noeFBId.HasValue)
        {
            query = query.Where(x => x.NoeFBId == noeFBId.Value);
        }
        var list = await query
            .Select(x => new
            {
                x.Id,
                x.Title,
                NoeFBId = (int)x.NoeFBId,
                NoeFB = x.NoeFBId.ToString(),
                x.FaslCode
            }).OrderBy(x => x.FaslCode)
            .ToListAsync();

        return Json(list);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(long Year,Guid BarAvordId, NoeFehrestBaha? noeFBId)
    {

        TajmieCommon _tajmieCommon = new TajmieCommon(_context);

        var result =
        await _tajmieCommon.TajmieFosoulAndSub(
            Year,
            BarAvordId,
            noeFBId);

        // ============================================================
        // دریافت مقادیر ذخیره شده آیتم‌های تجمیعی
        // ============================================================

        var lstBarAvordUserTajmie =
            await _context.BarAvordUserTajmies
                .Where(x =>
                    x.BarAvordUserId == BarAvordId)
                .ToListAsync();

        var calculatedBahayeKolDictionary = new Dictionary<long, decimal>();

     

        // ============================================================
        // 1. دریافت تمام فصل‌ها
        // ============================================================

        var lstFosoul = await _context.TajmieFosouls
            .Where(x => x.NoeFBId == noeFBId)
            .Select(x => new
            {
                x.Id,
                x.FaslCode,
                x.Title
            })
            .OrderBy(x => x.FaslCode)
            .ToListAsync();


        // ============================================================
        // 2. دریافت آیتم‌های تمام فصل‌ها
        // ============================================================

        var lstTFI = await _context.TajmieFosoulItemses
            .Where(x =>
                x.TajmieFosoul.NoeFBId == noeFBId)
            .ToListAsync();


        // ============================================================
        // 3. دریافت مقادیر ذخیره شده
        // ============================================================

        var lstSavedTajmie = await _context.BarAvordUserTajmies
            .Where(x =>
                x.BarAvordUserId == BarAvordId)
            .ToListAsync();


        // ============================================================
        // 4. دریافت فهرست بها
        // ============================================================

        var lstFB = await _context.FehrestBahas
            .Where(x =>
                x.Sal == Year &&
                x.NoeFB == noeFBId)
            .ToListAsync();


        // ============================================================
        // Dictionary فهرست بها
        // ============================================================

        var fbDictionary = lstFB
            .Where(x =>
                !string.IsNullOrWhiteSpace(x.Shomareh))
            .GroupBy(x =>
                x.Shomareh.Trim())
            .ToDictionary(
                g => g.Key,
                g => g.First());


        // ============================================================
        // 5. ضرایب عمومی
        // ============================================================

        RequestGetZarayebDto requestGetZarayeb =
            new RequestGetZarayebDto
            {
                BarAvordId = BarAvordId,
                NoeFBId = (NoeFehrestBaha) noeFBId,
                Year = (int) Year
            };

        ResultGetZarayebDto resultZarayeb =
            RizMetreCommon.GetZarayeb(
                requestGetZarayeb,
                _context);

        decimal zaribBalaSari =
            resultZarayeb.zaribBalaSari ?? 1;

        decimal zaribManteghe =
            resultZarayeb.zaribManteghe ?? 1;


        // ============================================================
        // 6. دریافت ریزمتره‌های معمولی
        // ============================================================

        var lstRM = await _context.RizMetreUserses
            .Include(x => x.FB)
            .Where(x =>
                x.FB.BarAvordId == BarAvordId &&
                x.FB.NoeFBId == noeFBId &&
                x.TajmieItem != null)
            .Select(x => new
            {
                x.TajmieItem,
                x.MeghdarJoz,

                FBShomareh =
                    x.FB.Shomareh,

                BahayeVahed =
                    x.FB.BahayeVahedNew,

                IsStar = false
            })
            .ToListAsync();


        // ============================================================
        // 7. دریافت ریزمتره‌های Star
        // ============================================================

        var lstRMStar = await _context.RizMetreStars
            .Include(x => x.ItemFBStar)
            .Where(x =>
                x.ItemFBStar.BaravordId == BarAvordId &&
                x.ItemFBStar.NoeFBId == noeFBId &&
                x.TajmieItem != null)
            .Select(x => new
            {
                x.TajmieItem,

                x.MeghdarJoz,

                FBShomareh =
                    x.ItemFBStar.Shomareh,

                BahayeVahed =
                    x.ItemFBStar.BahayeVahed,

                ItemZaribStar =
                    x.ItemFBStar.ItemZaribStar,

                blnKharidTajhizat =
                    x.ItemFBStar.blnKharidTajhizat,

                IsStar = true
            })
            .ToListAsync();


        

        // ============================================================
        // 9. آیتم‌های معمولی
        // ============================================================

        foreach (var rm in lstRM)
        {
            if (string.IsNullOrWhiteSpace(rm.TajmieItem))
                continue;


            var tajmieItemCode =
                rm.TajmieItem.Trim();


            var tajmieItem =
                lstTFI.FirstOrDefault(x =>
                    x.ItemCode == tajmieItemCode);


            if (tajmieItem == null)
                continue;


            var fbKey =
                rm.FBShomareh?.Trim();


            if (string.IsNullOrWhiteSpace(fbKey))
                continue;


            fbDictionary.TryGetValue(
                fbKey,
                out var fbItem);


            decimal bahayeVahed =
                0;


            if (fbItem != null &&
                !string.IsNullOrWhiteSpace(
                    fbItem.BahayeVahed))
            {
                decimal.TryParse(
                    fbItem.BahayeVahed,
                    out bahayeVahed);
            }


            // -----------------------------------------
            // بهای واحد نهایی
            // -----------------------------------------

            decimal bahayeVahedFinal =
                bahayeVahed
                * zaribBalaSari
                * zaribManteghe;


            // -----------------------------------------
            // بهای کل
            // -----------------------------------------

            decimal bahayeKol =
                (rm.MeghdarJoz ?? 0)
                * bahayeVahedFinal;


            // -----------------------------------------
            // جمع برای آیتم تجمیعی
            // -----------------------------------------

            if (calculatedBahayeKolDictionary
                .ContainsKey(tajmieItem.Id))
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] += bahayeKol;
            }
            else
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] = bahayeKol;
            }
        }


        // ============================================================
        // 10. آیتم‌های Star
        // ============================================================

        foreach (var rm in lstRMStar)
        {
            if (string.IsNullOrWhiteSpace(rm.TajmieItem))
                continue;


            var tajmieItemCode =
                rm.TajmieItem.Trim();


            var tajmieItem =
                lstTFI.FirstOrDefault(x =>
                    x.ItemCode == tajmieItemCode);


            if (tajmieItem == null)
                continue;


            // -----------------------------------------
            // ضریب Star
            // -----------------------------------------

            decimal zaribStar =
                rm.ItemZaribStar ?? 1;


            decimal zaribKharid =
                rm.blnKharidTajhizat == true
                    ? 1.14m
                    : zaribBalaSari * zaribManteghe;


            // -----------------------------------------
            // بهای واحد نهایی Star
            // -----------------------------------------

            decimal bahayeVahedFinal =
                rm.BahayeVahed
                * zaribStar
                * zaribKharid;


            // -----------------------------------------
            // بهای کل Star
            // -----------------------------------------

            decimal bahayeKol =
                (rm.MeghdarJoz ?? 0)
                * bahayeVahedFinal;


            // -----------------------------------------
            // جمع برای آیتم تجمیعی
            // -----------------------------------------

            if (calculatedBahayeKolDictionary
                .ContainsKey(tajmieItem.Id))
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] += bahayeKol;
            }
            else
            {
                calculatedBahayeKolDictionary[
                    tajmieItem.Id] = bahayeKol;
            }
        }

        // ============================================================
        // 11. محاسبه جمع هر فصل
        // ============================================================

        var resultFosoul = lstFosoul
            .Select(fosoul =>
            {
                decimal totalBahayeKol = 0;

                // -----------------------------------------
                // آیتم‌های این فصل
                // -----------------------------------------

                var items = lstTFI
                    .Where(x =>
                        x.TajmieFosoulId == fosoul.Id)
                    .ToList();


                foreach (var item in items)
                {
                    // -------------------------------------
                    // اگر مقدار ذخیره شده وجود دارد
                    // -------------------------------------

                    var savedItem =
                        lstSavedTajmie.FirstOrDefault(x =>
                            x.TajmieFosoulItemId == item.Id);

                    if (savedItem != null)
                    {
                        totalBahayeKol += savedItem.BahayeKol;

                        continue;
                    }


                    // -------------------------------------
                    // اگر ذخیره نشده:
                    // مقدار محاسبه شده از ریزمتره
                    // -------------------------------------

                    if (calculatedBahayeKolDictionary.TryGetValue(
                            item.Id,
                            out decimal calculatedBahayeKol))
                    {
                        totalBahayeKol += calculatedBahayeKol;
                    }
                }


                return new
                {
                    TajmieFosoulId = fosoul.Id,

                    FaslCode = fosoul.FaslCode,

                    Title = fosoul.Title,

                    TotalBahayehKol = totalBahayeKol
                };
            })
            .OrderBy(x => x.FaslCode)
            .ToList();


        // ============================================================
        // 12. محاسبه جمع کل پروژه
        // ============================================================

        decimal totalAllFosoul =
            resultFosoul.Sum(x =>
                x.TotalBahayehKol);


        // ============================================================
        // 13. اضافه کردن درصد وزنی به هر فصل
        // ============================================================

        var resultFosoulWithWeight =
            resultFosoul
                .Select(x => new
                {
                    x.TajmieFosoulId,

                    x.FaslCode,

                    x.Title,

                    x.TotalBahayehKol,

                    // درصد وزنی فصل نسبت به کل پروژه
                    WeightPercent =
                        totalAllFosoul != 0
                            ? Math.Round(
                                (x.TotalBahayehKol / totalAllFosoul) * 100,
                                2,
                                MidpointRounding.AwayFromZero)
                            : 0
                })
                .ToList();


        // ============================================================
        // 14. اضافه کردن رکورد جمع کل
        // ============================================================

        resultFosoulWithWeight.Add(new
        {
            TajmieFosoulId = 0L,

            FaslCode = "",

            Title = "جمع کل",

            TotalBahayehKol = totalAllFosoul,

            WeightPercent =
                totalAllFosoul != 0
                    ? 100m
                    : 0m
        });


        // ============================================================
        // 15. خروجی
        // ============================================================

        return new JsonResult(resultFosoulWithWeight);
    }




    [HttpGet]
    public async Task<IActionResult> Get(long id)
    {
        var item = await _context.TajmieFosouls

            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return NotFound();

        return Json(new
        {
            item.Id,
            item.Title,
            item.NoeFBId
        });
    }

    [HttpGet]
    public async Task<IActionResult> CheckItemInTajmie(string ItemTajmieValue,NoeFehrestBaha NoeFB)
    {
        if (ItemTajmieValue.Trim() != "")
        {
            clsTajmieFosoulItems? item = await _context.TajmieFosoulItemses.Include(x => x.TajmieFosoul).FirstOrDefaultAsync(x => x.ItemCode.Trim() == ItemTajmieValue.Trim() && x.TajmieFosoul.NoeFBId == NoeFB);

            if (item == null)
                return NotFound();

            return new JsonResult("OK");
        }
        return new JsonResult("OK");
    }

    [HttpGet]
    public IActionResult GetTajmieTree(NoeFehrestBaha noeFB)
    {
        var data = _context.TajmieFosouls
            .Where(x => x.NoeFBId == noeFB)
            .Select(x => new
            {
                x.Id,
                x.Title,
                Items = x.TajmieFosoulItems.Select(i => new
                {
                    i.Id,
                    i.ItemCode,
                    i.Sharh
                })
            })
            .ToList();

        var tree = new List<object>();

        foreach (var group in data)
        {
            tree.Add(new
            {
                id = "F_" + group.Id,
                parent = "#",
                text = group.Title,
                icon = "fa fa-folder",
                state = new { opened = false }
            });

            foreach (var item in group.Items)
            {
                tree.Add(new
                {
                    id = item.Id.ToString(),
                    parent = "F_" + group.Id,
                    text = $"{item.ItemCode} - {item.Sharh}",

                    data = new
                    {
                        itemCode = item.ItemCode,
                        sharh = item.Sharh,
                        tajmieId = item.Id
                    },
                    icon = "fa fa-file"
                });
            }
        }

        return Json(tree);
    }



    [HttpPost]
    public async Task<IActionResult> Save(
        [FromBody] clsTajmieFosoul model)
    {

        bool exists =
            await _context.TajmieFosouls

            .AnyAsync(x =>

                x.NoeFBId ==
                model.NoeFBId
                && x.Title.Trim() == model.Title
            );

        if (exists)
        {
            return BadRequest(new
            {
                message = "برای این فهرست بها قبلاً ثبت شده است."
            });
        }

        _context.TajmieFosouls.Add(model);

        await _context.SaveChangesAsync();

        return Ok();
    }



    [HttpPost]
    public async Task<IActionResult> Update(
    [FromBody] clsTajmieFosoul model)
    {

        var entity =
            await _context.TajmieFosouls

            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (entity == null)
            return NotFound();

        model.Title = model.Title?.Trim();

        bool exists = await _context.TajmieFosouls.AnyAsync(x =>

            x.Id != model.Id
            &&
            x.NoeFBId == model.NoeFBId
            &&
            x.Title.Trim() == model.Title
        );

        if (exists)
        {
            return BadRequest(new
            {
                message = "فصلی با این عنوان برای این فهرست بها قبلاً ثبت شده است."
            });
        }

        entity.NoeFBId = model.NoeFBId;
        entity.FaslCode = model.FaslCode;
        entity.Title = model.Title;

        await _context.SaveChangesAsync();

        return Ok();
    }



    [HttpPost]
    public async Task<IActionResult> Delete(long id)
    {

        var entity =
            await _context.TajmieFosouls

            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound();

        bool hasItems =
            await _context.TajmieFosoulItemses

            .AnyAsync(x =>
                x.TajmieFosoulId == id);

        if (hasItems)
        {
            return BadRequest(new
            {
                message = "ابتدا آیتم های این فصل را حذف نمایید."
            });
        }

        _context.TajmieFosouls.Remove(entity);

        await _context.SaveChangesAsync();

        return Ok();
    }


    [HttpGet]
    public async Task<IActionResult> GetItems(long tajmieId)
    {
        var list = await _context.TajmieFosoulItemses

            .Where(x => x.TajmieFosoulId == tajmieId)

            .OrderBy(x => x.Id)

            .Select(x => new
            {
                x.Id,
                x.ItemCode,
                x.Sharh,
                x.Vahed,
                x.MainItem,

                CalculateValueType = (int)x.calculateValueType,

                CalculateValueTypeText =

                    x.calculateValueType == CalculateValueType.Manual ?

                    "دستی"

                    :

                    x.calculateValueType == CalculateValueType.FromItem ?

                    "از آیتم"

                    :

                    "محاسباتی"

            })

            .ToListAsync();

        return Json(list);
    }



    [HttpPost]
    public async Task<IActionResult> SaveItem(
            [FromBody] clsTajmieFosoulItems model)
    {

        bool exists =
            await _context.TajmieFosoulItemses

            .AnyAsync(x =>

                x.TajmieFosoulId ==
                model.TajmieFosoulId

                &&

                x.ItemCode ==
                model.ItemCode

            );

        if (exists)
        {
            return BadRequest(new
            {
                message = "این آیتم قبلاً ثبت شده است."
            });
        }

        //long newId = 1;

        //var lastId =
        //    await _context.TajmieFosoulItemses

        //    .OrderByDescending(x => x.Id)

        //    .Select(x => (long?)x.Id)

        //    .FirstOrDefaultAsync();

        //if (lastId.HasValue)
        //    newId = lastId.Value + 1;

        //model.Id = newId;

        _context.TajmieFosoulItemses.Add(model);

        await _context.SaveChangesAsync();

        return Ok();

    }


    [HttpGet]
    public async Task<IActionResult> GetItem(long id)
    {

        var item =
            await _context.TajmieFosoulItemses

            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return NotFound();

        return Json(item);

    }


    [HttpPost]
    public async Task<IActionResult> UpdateItem(
        [FromBody] clsTajmieFosoulItems model)
    {

        var entity =
            await _context.TajmieFosoulItemses

            .FirstOrDefaultAsync(x => x.Id == model.Id);

        if (entity == null)
            return NotFound();

        entity.ItemCode = model.ItemCode;

        entity.Sharh = model.Sharh;

        entity.Vahed = model.Vahed;

        entity.MainItem = model.MainItem;

        entity.calculateValueType = model.calculateValueType;

        await _context.SaveChangesAsync();

        return Ok();

    }


    [HttpPost]
    public async Task<IActionResult> DeleteItem(long id)
    {

        var entity =
            await _context.TajmieFosoulItemses

            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound();

        _context.TajmieFosoulItemses.Remove(entity);

        await _context.SaveChangesAsync();

        return Ok();

    }

    /// <summary>
    /// TajmieFBItems
    /// </summary>
    /// <param name="tajmieItemId"></param>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> GetFBItems(long tajmieItemId)
    {
        var list = await _context.TajmieFBItemses.Include(x=>x.TajmieFosoulItem).ThenInclude(x=>x.TajmieFosoul)
            .Where(x => x.TajmieFosoulItemId == tajmieItemId)
            .OrderBy(x => x.Year)
            .ThenBy(x => x.ItemFBShomareh)
            .Select(x => new
            {
                x.ID,
                x.Year,
                x.ItemFBShomareh,
                x.TajmieFosoulItem.TajmieFosoul.NoeFBId
            })
            .ToListAsync();



        return Json(list);
    }


    [HttpPost]
    public async Task<IActionResult> SaveFBItem(
    [FromBody] clsTajmieFBItems model)
    {
        model.ItemFBShomareh =
            model.ItemFBShomareh?.Trim();

        bool exists = await _context.TajmieFBItemses.AnyAsync(x =>

            x.TajmieFosoulItemId == model.TajmieFosoulItemId

            &&

            x.Year == model.Year

            &&

            x.ItemFBShomareh == model.ItemFBShomareh
        );

        if (exists)
        {
            return BadRequest(new
            {
                message = "این آیتم قبلاً ثبت شده است."
            });
        }

        _context.TajmieFBItemses.Add(model);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateFBItem(
    [FromBody] clsTajmieFBItems model)
    {
        var entity = await _context.TajmieFBItemses

            .FirstOrDefaultAsync(x => x.ID == model.ID);

        if (entity == null)
            return NotFound();

        bool exists = await _context.TajmieFBItemses.AnyAsync(x =>

            x.ID != model.ID

            &&

            x.TajmieFosoulItemId == model.TajmieFosoulItemId

            &&

            x.Year == model.Year

            &&

            x.ItemFBShomareh == model.ItemFBShomareh
        );

        if (exists)
        {
            return BadRequest(new
            {
                message = "این آیتم قبلاً ثبت شده است."
            });
        }

        entity.ItemFBShomareh = model.ItemFBShomareh;

        entity.Year = model.Year;

        await _context.SaveChangesAsync();

        return Ok();
    }



    [HttpGet]
    public async Task<IActionResult> GetFBItem(Guid id)
    {
        var item = await _context.TajmieFBItemses

            .FirstOrDefaultAsync(x => x.ID == id);

        if (item == null)
            return NotFound();

        return Json(item);
    }



    [HttpPost]
    public async Task<IActionResult> DeleteFBItem(Guid id)
    {
        var item = await _context.TajmieFBItemses

            .FirstOrDefaultAsync(x => x.ID == id);

        if (item == null)
            return NotFound();

        _context.TajmieFBItemses.Remove(item);

        await _context.SaveChangesAsync();

        return Ok();
    }






}
