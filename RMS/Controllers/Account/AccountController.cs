using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    //private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    //private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly ITokenService _tokenService;

    public AccountController(
        //IConfiguration configuration,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager
        //,IHttpContextAccessor httpContextAccessor
        )
    {
        //_configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        //_httpContextAccessor = httpContextAccessor;
        //_tokenService = tokenService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // چاپ خطاها در کنسول
            }
            return View(model); // برگردوندن مدل به view
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            FullName = model.UserName,  // مقداردهی فیلد FullName
            Email = null, // چون از ایمیل استفاده نمی‌کنیم
            EmailConfirmed = true // کاربر به صورت فعال ساخته میشه
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        // اطمینان از ارسال مدل به ویو
        return View(new LoginViewModel());
    }


    //[HttpPost]
    //public async Task<IActionResult> Login(LoginViewModel model)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest("Invalid client request");

    //    var user = await _userManager.FindByNameAsync(model.UserName);
    //    if (user == null)
    //        return Unauthorized("نام کاربری یا رمز اشتباه است");

    //    var check = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
    //    if (!check.Succeeded)
    //        return Unauthorized("نام کاربری یا رمز اشتباه است");

    //    var jwtToken = await _tokenService.CreateTokenAsync(user);

    //    return Json(new
    //    {
    //        token = jwtToken,
    //        user = new { user.UserName, user.FullName, user.PhoneNumber }
    //    });

    //    //var user = await _userManager.FindByNameAsync(model.UserName);
    //    //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    //    //{
    //    //    // احراز هویت کاربر
    //    //    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

    //    //    var token = GenerateToken(user);  // توکن رو تولید کن
    //    //    SetTokenInCookie(token);          // توکن رو در کوکی ذخیره کن
    //    //    return RedirectToAction("Index", "Dashboard");
    //    //    //return Ok(new { message = "Login successful" });
    //    //}
    //    //else
    //    //{
    //    //    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور نادرست است.");
    //    //}

    //    //return Unauthorized();
    //}


    //[HttpPost]
    //public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    //{
    //    if (!ModelState.IsValid)
    //        return View(model);

    //    var user = await _userManager.FindByNameAsync(model.UserName);
    //    if (user == null)
    //    {
    //        ModelState.AddModelError(string.Empty, "نام کاربری یا رمز اشتباه است");
    //        return View(model);
    //    }

    //    //var signInResult = await _signInManager.PasswordSignInAsync(
    //    //    user, model.Password, model.RememberMe, lockoutOnFailure: false);

    //    //if (!signInResult.Succeeded)
    //    //{
    //    //    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز اشتباه است");
    //    //    return View(model);
    //    //}

    //    var result = await _signInManager.PasswordSignInAsync(
    //    user,
    //    model.Password,
    //    model.RememberMe,
    //    lockoutOnFailure: false);

    //    if (!result.Succeeded)
    //    {
    //        ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
    //        return View(model);
    //    }

    //    // ساخت JWT
    //    //var jwtToken = await _tokenService.CreateTokenAsync(user);

    //    // ذخیره توکن در کوکی (HttpOnly برای امنیت)
    //    //Response.Cookies.Append("access_token", jwtToken, new CookieOptions
    //    //{
    //    //    HttpOnly = true,
    //    //    Secure = true,          // اگر روی HTTPS هستید حتما true بماند
    //    //    SameSite = SameSiteMode.Strict,
    //    //    Expires = DateTimeOffset.UtcNow.AddHours(1)
    //    //});

    //    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
    //        return Redirect(returnUrl);

    //    // ریدایرکت به داشبورد
    //    return RedirectToAction("Index", "Dashboard");
    //}



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName,       // بهتر از خود user
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true
        );

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
            return View(model);
        }

        // اگر آدرس برگشتی معتبر بود
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        // ریدایرکت پیش‌فرض
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }



    //private string GenerateToken(ApplicationUser user)
    //{
    //    var claims = new[]
    //    {
    //        new Claim(ClaimTypes.Name, user.UserName),
    //        new Claim(ClaimTypes.NameIdentifier, user.Id),
    //        // اضافه کردن claim های بیشتر (مثل نقش‌ها) در صورت نیاز
    //    };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: _configuration["Jwt:Issuer"],
    //        audience: _configuration["Jwt:Audience"],
    //        claims: claims,
    //        expires: DateTime.Now.AddHours(1), // مدت زمان انقضا
    //        signingCredentials: creds
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}

    //private void SetTokenInCookie(string token)
    //{
    //    // ایجاد کوکی با توکن
    //    var cookieOptions = new CookieOptions
    //    {
    //        HttpOnly = false, // دسترسی به کوکی از جاوااسکریپت غیرفعال شود
    //        Secure = false,   // استفاده از کوکی تنها در اتصالات امن (HTTPS)
    //        Expires = DateTime.Now.AddDays(15), // تاریخ انقضا
    //        SameSite = SameSiteMode.Strict // جلوگیری از ارسال کوکی در درخواست‌های cross-site
    //    };

    //    // ذخیره توکن در کوکی
    //    _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", token, cookieOptions);
    //}

  
}
