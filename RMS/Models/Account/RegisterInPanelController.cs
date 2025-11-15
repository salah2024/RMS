using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Models.Entity;
using RMS.Services.JWT;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Account;

[AllowAnonymous]
public class RegisterInPanelController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;

    public RegisterInPanelController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterInPanelDto model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // درخواست از طریق ajax-link → فقط فرم
                return PartialView("_RegisterForm", model);
            }
            return View("Index", model);
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            PhoneNumber = model.Mobile,
            FullName = "",
            RegisterDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            clsUserDetail userDetail = new clsUserDetail
            {
                NationalCode = model.NationalCode,
                UserId = user.Id,
                userType = UserType.Legal,
            };
            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            // ساخت JWT
            var jwtToken = await _tokenService.CreateTokenAsync(user);

            return RedirectToAction("Index", "ViewCompany");

        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        ViewBag.Roles = _roleManager.Roles.ToList();

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

    public IActionResult Index()
    {
        ViewBag.Roles = _roleManager.Roles.ToList();
        var model = new RegisterInPanelDto();

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_RegisterForm", model);

        return PartialView(model);
    }
}
