using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Company;


public class ViewCompanyController(ApplicationDbContext context) : Controller
{
    ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var companies = await _context.UserDetails
            .Include(x => x.User)
            .Where(x => x.userType == UserType.Legal)
            .ToListAsync();
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ViewCompanyForm", companies);

        return PartialView(companies);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var detail = await _context.UserDetails
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.ID == id);

        if (detail == null)
            return NotFound();
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ViewEditForm", detail);

        return PartialView(detail);
    }

    // POST: ویرایش اطلاعات شرکت
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(clsUserDetail model)
    {
        if (!ModelState.IsValid)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_ViewEditForm", model);

            return PartialView(model); 
        }

        var detail = await _context.UserDetails
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.ID == model.ID);

        if (detail == null)
            return NotFound();

        // به‌روزرسانی فیلدهای اطلاعات تکمیلی
        detail.CompanyName = model.CompanyName;
        detail.CEOName = model.CEOName;
        detail.Address = model.Address;
        detail.PostalCode = model.PostalCode;
        detail.NationalCode = model.NationalCode;
        detail.userType = model.userType;

        // اگر بخوای اطلاعات کاربر مرتبط هم آپدیت بشه:
        if (detail.User != null)
        {
            detail.User.FullName = model.User?.FullName ?? detail.User.FullName;
            detail.User.PhoneNumber = model.User?.PhoneNumber ?? detail.User.PhoneNumber;
        }

        await _context.SaveChangesAsync();

        TempData["Success"] = "اطلاعات شرکت با موفقیت ویرایش شد.";
        return RedirectToAction(nameof(Index));
    }

    // حذف شرکت + کاربر مربوطه (به خاطر Cascade delete روی FK)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id)
    {
        var detail = await _context.UserDetails
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.ID == id);

        if (detail == null)
            return NotFound();

        _context.UserDetails.Remove(detail);
        await _context.SaveChangesAsync();

        TempData["Success"] = "شرکت با موفقیت حذف شد.";
        return RedirectToAction(nameof(Index));
    }
}
