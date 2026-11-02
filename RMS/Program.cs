using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;
using RMS.Controllers.RegisterInPanel.Dto;
using RMS.Interfaces.Workflow;
using RMS.Services.Workflow;
using System.Security.Claims;



QuestPDF.Settings.License = LicenseType.Community;

FontManager.RegisterFont(
    File.OpenRead("wwwroot/Scripts/Fonts/PersianFonts/BMitra.ttf"));


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity فقط یک بار
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
        options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
        options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;

        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";          // صفحه لاگین
        //options.AccessDeniedPath = "/Account/Login";   // عدم دسترسی
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddAuthorization();

// MVC Only
builder.Services.AddControllersWithViews();

/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////

//////////////کل اکشن ها را در صورتی که کاربری لاگین نشده باشد میبنند ///////////////////
//////////////برای اینکه بخواهیم اکشنی بدون لاکین کار کند بایستی روی اکشن بنویسیم ///////

///////////////////            [AllowAnonymous]          ////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddScoped<IWorkflowQueryService, WorkflowQueryService>();
builder.Services.AddScoped<IWorkflowCommandService, WorkflowCommandService>();
builder.Services.AddScoped<IWorkflowStepCommandService,WorkflowStepCommandService>();
builder.Services.AddScoped<IWorkflowStepQueryService, WorkflowStepQueryService>();
builder.Services.AddScoped<IWorkflowRuntimeService, WorkflowRuntimeService>();

////تنظیم تایم فعال بودن کاربر لاگین شده
///که اکنون 1 ساعت است
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Account/Login";

//    //options.AccessDeniedPath = "/Account/AccessDenied";

//    options.ExpireTimeSpan = TimeSpan.FromHours(1);

//    //اگر کاربر فعال باشد
//    //زمان لاگین تمدید می‌شود
//    //هر درخواست جدید زمان را ریست می‌کند
//    options.SlidingExpiration = true;

//    //برای RememberMe
//    //برابر 15 روز حفظ میشود
//    options.ExpireTimeSpan = TimeSpan.FromDays(15);

//    //Cookie فقط روی HTTPS ارسال شود
//    //در صورتی که Always باشد
//    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

//    options.Cookie.SameSite = SameSiteMode.Lax;
//});

// سرویس تولید توکن
//builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=dashboard}/{action=Index}/{id?}");

app.Run();



//// ---------- کوکی احراز هویت ----------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";

    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
});


