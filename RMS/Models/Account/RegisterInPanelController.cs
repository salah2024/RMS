using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMS.Models.Account.Dto;
using RMS.Services.JWT;

namespace RMS.Models.Account;

public class RegisterInPanelController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;

    public RegisterInPanelController(
       UserManager<IdentityUser> userManager,
       SignInManager<IdentityUser> signInManager,
       RoleManager<IdentityRole> roleManager,
       ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterInPanelDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            PhoneNumber = model.Mobile,
            FullName = model.FullName,
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

            // ساخت JWT
            var jwtToken = await _tokenService.CreateTokenAsync(user);

            // اگر API داری، بهتره JSON برگردونی
            return Json(new
            {
                token = jwtToken,
                user = new { user.UserName, user.FullName, user.PhoneNumber }
            });
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
}
