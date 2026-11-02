using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Company.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMS.Controllers.Company;

public class RegisterCompanyInPanelController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterCompanyInPanelController(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                ApplicationDbContext context)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterCompanyInPanelDto model)
    {

        if (!ModelState.IsValid)
        {
            ViewBag.CompanyTypes = Enum.GetValues(typeof(CompanyType))
                            .Cast<CompanyType>()
                            .Select(e => new { Id = (int)e, Name = e.ToString() })
                            .ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Index", model);
            }
            return View("Index", model);
        }

        // بررسی تکراری نبودن شناسه ملی
        bool nationalCodeExists = await _context.Companies
            .AnyAsync(x => x.CompanyNationalCode == model.CompanyNationalCode);

        if (nationalCodeExists)
        {
            ModelState.AddModelError(nameof(model.CompanyNationalCode),
                "شرکتی با این شناسه ملی قبلاً ثبت شده است.");

            ViewBag.CompanyTypes = Enum.GetValues(typeof(CompanyType))
                            .Cast<CompanyType>()
                            .Select(e => new { Id = (int)e, Name = e.ToString() })
                            .ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Index", model);
            }

            return View("Index", model);
        }

        clsCompany Company = new clsCompany
        {
            CompanyName = model.CompanyName,
            CompanyType = model.CompanyType,
            CompanyNationalCode = model.CompanyNationalCode,
            PostalCode = model.PostalCode,
            Address = model.Address,
            Email = model.Email,
            FaxNumber = model.FaxNumber,
            ManagementFirstName = model.ManagementFirstName,
            ManagementLastName = model.ManagementLastName,
            PhoneNumber = model.PhoneNumber,
            MobileNumber = model.MobileNumber,
        };

        await _context.Companies.AddAsync(Company);

        string strRoleName = "";
        switch (model.CompanyType)
        {
            case CompanyType.KarFarma:
                {
                    strRoleName = "karfarma";
                    break;
                }
            case CompanyType.Moshaver:
                {
                    strRoleName = "moshaver";
                    break;
                }
            case CompanyType.PayManKar:
                {
                    strRoleName = "peymankar";
                    break;
                }
            default:
                break;
        }

        var user = new ApplicationUser
        {
            UserName = model.CompanyNationalCode, //model.UserName,
            PhoneNumber = model.MobileNumber,
            FullName = model.ManagementFirstName + " " + model.ManagementLastName,
            RegisterDate = DateTime.UtcNow
        };

        string strPass = "Aa12345@";

        var result = await _userManager.CreateAsync(user, strPass);

        if (result.Succeeded)
        {
            var roleExists = await _roleManager.Roles
                    .FirstOrDefaultAsync(r => r.NormalizedName.ToLower() == strRoleName);

            // ثبت مستقیم نقش با RoleId
            var userRole = new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = roleExists.Id
            };

            _context.UserRoles.Add(userRole);

            // ثبت اطلاعات تکمیلی
            clsUserDetail userDetail = new clsUserDetail
            {
                NationalCode = "",
                UserId = user.Id,
                FirstName = model.ManagementFirstName,
                LastName = model.ManagementLastName,
                Address = model.Address,
                PostalCode = model.PostalCode,
                CompanyId = Company.ID,
            };

            _context.UserDetails.Add(userDetail);
        }

        await _context.SaveChangesAsync();


        ViewBag.CompanyTypes = Enum.GetValues(typeof(CompanyType))
                                .Cast<CompanyType>()
                                .Select(e => new { Id = (int)e, Name = e.ToString() })
                                .ToList();

        return RedirectToAction("Index", "ViewCompany");

        //if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        //    return PartialView("_RegisterCompanyForm", model);

        //return PartialView("Index", model);
    }

    public IActionResult Index()
    {
        ViewBag.CompanyTypes = Enum.GetValues(typeof(CompanyType))
                        .Cast<CompanyType>()
                        .Select(e => new { Id = (int)e, Name = e.ToString() })
                        .ToList();

        var model = new RegisterCompanyInPanelDto();

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_RegisterCompanyForm", model);

        return PartialView(model);
    }
}
