using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.RegisterInPanel;

public class RegisterInPanelController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    //private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;

    public RegisterInPanelController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        //ITokenService tokenService,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        //_tokenService = tokenService;
        _context = context;
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterInPanelDto model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            ViewBag.Companies = _context.Companies.ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_RegisterForm", model);

            return View("Index", model);
        }

        // بررسی وجود Role با RoleId
        var roleExists = await _roleManager.Roles
            .AnyAsync(r => r.Id == model.RoleId);

        if (!roleExists)
        {
            ModelState.AddModelError("", "نقش انتخاب شده معتبر نیست.");

            ViewBag.Roles = _roleManager.Roles.ToList();
            ViewBag.Companies = _context.Companies.ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_RegisterForm", model);

            return View("Index", model);
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            PhoneNumber = model.Mobile,
            FullName = model.FirstName + " " + model.LastName,
            RegisterDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // ثبت مستقیم نقش با RoleId
            var userRole = new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = model.RoleId
            };

            _context.UserRoles.Add(userRole);

            // ثبت اطلاعات تکمیلی
            clsUserDetail userDetail = new clsUserDetail
            {
                NationalCode = model.NationalCode,
                UserId = user.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PostalCode = model.PostalCode,
                CompanyId = model.CompanyId,
            };

            _context.UserDetails.Add(userDetail);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "UserList",
                "Users",
                new { companyId = model.CompanyId });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        ViewBag.Roles = _roleManager.Roles.ToList();
        ViewBag.Companies = _context.Companies.ToList();

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_RegisterForm", model);

        return View("Index", model);
    }


    [HttpGet]
    public async Task<IActionResult> CheckUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return Json(new { exists = false });

        var user = await _userManager.FindByNameAsync(userName);

        return Json(new { exists = user != null });
    }

    public IActionResult Index(Guid? companyId)
    {

        ViewBag.Roles = _roleManager.Roles.ToList();
        ViewBag.Companies = _context.Companies.ToList();

        var model = new RegisterInPanelDto();

        if (companyId.HasValue)
            model.CompanyId = companyId.Value;   // 🔥 ست کردن خودکار شرکت

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_RegisterForm", model);

        return PartialView(model);
    }
}
