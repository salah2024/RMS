using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RMS.Controllers.RegisterInPanel.Dto;
using RMS.Services.JWT;

var builder = WebApplication.CreateBuilder(args);

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
        options.AccessDeniedPath = "/Account/Login";   // عدم دسترسی
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });
builder.Services.AddAuthorization();

//.AddDefaultTokenProviders();

// خواندن تنظیمات JWT
//var jwtSettings = builder.Configuration.GetSection("Jwt");
//var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

// اینجا Authentication پیش‌فرض را دست نمی‌زنیم
// (Identity خودش کوکی Identity.Application را به عنوان default ثبت می‌کند)
//builder.Services
//    .AddAuthentication()
//    .AddJwtBearer("Jwt", options =>
//    {
//        options.RequireHttpsMetadata = false; // در محیط واقعی true
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidateLifetime = true,
//            ValidIssuer = jwtSettings["Issuer"],
//            ValidAudience = jwtSettings["Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ClockSkew = TimeSpan.Zero,
//            RoleClaimType = ClaimTypes.Role,
//            NameClaimType = ClaimTypes.Name
//        };

//        // اگر می‌خواهی JWT از کوکی access_token خوانده شود (برای APIها):
//        options.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = context =>
//            {
//                var token = context.Request.Cookies["access_token"];
//                if (!string.IsNullOrEmpty(token))
//                    context.Token = token;

//                return Task.CompletedTask;
//            }
//        };
//    });

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//    options.Cookie.HttpOnly = true; // جاوااسکریپت نتونه کوکی رو بخونه
//    //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // فقط روی HTTPS ارسال شه
//    options.Cookie.SameSite = SameSiteMode.Lax; // یا Strict، بسته به نیازت
//    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    options.SlidingExpiration = true;
//});


//builder.Services.AddAuthorization();

// MVC Only
builder.Services.AddControllersWithViews();

// سرویس تولید توکن
//builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();










//using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// ---------- DbContext ----------
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// ---------- Identity ----------
//builder.Services
//    .AddIdentity<ApplicationUser, IdentityRole>(options =>
//    {
//        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
//        options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
//        options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;

//        // اختیاری ولی بهتر:
//        options.Password.RequiredLength = 6;
//        options.Lockout.MaxFailedAccessAttempts = 5;
//    })
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//// ---------- کوکی احراز هویت ----------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

//    options.Cookie.Name = ".RMS.Auth";
//    options.Cookie.HttpOnly = true;

//    // چون روی IIS فعلاً با http تست می‌کنی:
//    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
//    // وقتی https تنظیم شد:
//    // options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

//    options.Cookie.SameSite = SameSiteMode.Lax;
//    options.Cookie.Path = "/";

//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
//    options.SlidingExpiration = true;
//});

//// ---------- MVC ----------
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// اگر روی IIS https نداری، فعلاً این خط را موقتاً کامنت کن
//// app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
