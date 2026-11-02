using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Users.Dto;

namespace RMS.Controllers.Users;

public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UserList(Guid companyId, int page = 1, int pageSize = 10)
    {
        var query = from u in _context.Users
                    join ud in _context.UserDetails on u.Id equals ud.UserId
                    join c in _context.Companies on ud.CompanyId equals c.ID
                    where ud.CompanyId == companyId  
                    select new UserListViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        FullName = u.FullName,
                        PhoneNumber = u.PhoneNumber,
                        CompanyName = c.CompanyName
                    };

        var totalItems = await query.CountAsync();

        var users = await query
            .OrderBy(x => x.UserName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PagedResult<UserListViewModel>
        {
            Items = users,
            TotalItems = totalItems,
            PageNumber = page,
            PageSize = pageSize
        };


        var Company = _context.Companies.First(x => x.ID == companyId);

        ViewBag.CompanyName = Company.CompanyName; 
        ViewBag.CompanyId = companyId; 

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_UserList", result);

        return PartialView(result);
    }


    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            var userDetail = await _context.UserDetails
                .FirstOrDefaultAsync(x => x.UserId == id);

            if (userDetail != null)
                _context.UserDetails.Remove(userDetail);

            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(UserList));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        var detail = await _context.UserDetails
            .FirstOrDefaultAsync(x => x.UserId == id);

        var model = new EditUserViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            FullName = user.FullName,
            CompanyId = detail?.CompanyId
        };

        ViewBag.Companies = _context.Companies.ToList();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.Id);

        user.UserName = model.UserName;
        user.PhoneNumber = model.PhoneNumber;
        user.FullName = model.FullName;

        await _userManager.UpdateAsync(user);

        var detail = await _context.UserDetails
            .FirstOrDefaultAsync(x => x.UserId == model.Id);

        if (detail != null)
            detail.CompanyId = model.CompanyId;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(UserList));
    }

    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> ResetPassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(new ResetPasswordAdminViewModel { UserId = id });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> ResetPassword(ResetPasswordAdminViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
            return NotFound();

        // 🔥 تولید توکن
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // 🔥 ریست امن پسورد
        var result = await _userManager.ResetPasswordAsync(
            user,
            token,
            model.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        return RedirectToAction(nameof(UserList));
    }


    //[Authorize(Roles = "Admin")]
    //public async Task<IActionResult> Edit(string id)
    //{
    //    var user = await _userManager.FindByIdAsync(id);
    //    if (user == null)
    //        return NotFound();

    //    var userRoles = await _userManager.GetRolesAsync(user);
    //    var detail = await _context.UserDetails
    //        .FirstOrDefaultAsync(x => x.UserId == id);

    //    var model = new EditUserViewModel
    //    {
    //        Id = user.Id,
    //        UserName = user.UserName,
    //        FullName = user.FullName,
    //        PhoneNumber = user.PhoneNumber,
    //        Role = userRoles.FirstOrDefault(),
    //        CompanyId = detail?.CompanyId
    //    };

    //    ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
    //    ViewBag.Companies = _context.Companies.ToList();

    //    return View(model);
    //}
}
