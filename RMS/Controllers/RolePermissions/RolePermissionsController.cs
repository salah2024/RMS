using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.RolePermissions.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.RolePermissions;

//[Authorize]
public class RolePermissionsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolePermissionsController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }

    // صفحه اصلی فقط dropdown را می‌دهد
    public async Task<IActionResult> Index()
    {
        var vm = new RolePermissionsIndexVm
        {
            Roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync()
        };
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("Index", vm);

        return View(vm);
    }

    // Ajax: گرفتن لیست Permissionهای یک Role
    [HttpGet]
    public async Task<IActionResult> GetPermissions(string roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            return BadRequest("roleId is required");

        var rolePermissionIds = await _context.RolePermissions
            .Where(x => x.RoleId == roleId)
            .Select(x => x.PermissionId)
            .ToListAsync();

        var permissions = await _context.Permissions
            .OrderBy(p => p.Group)
            .ThenBy(p => p.Key)
            .ToListAsync();

        var items = permissions.Select(p => new PermissionCheckboxVm
        {
            PermissionId = p.Id,
            Key = p.Key,
            Title = p.Title,
            Group = p.Group,
            IsChecked = rolePermissionIds.Contains(p.Id)
        }).ToList();

        return PartialView("_PermissionsTable", items);
    }

    // Ajax: ذخیره Permissionهای انتخاب شده
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveAjax([FromBody] SaveRolePermissionsRequest req)
    {
        if (req == null || string.IsNullOrWhiteSpace(req.RoleId))
            return BadRequest("RoleId is required");

        // پاک کردن قبلی‌ها
        var existing = await _context.RolePermissions
            .Where(x => x.RoleId == req.RoleId)
            .ToListAsync();

        _context.RolePermissions.RemoveRange(existing);

        // افزودن جدیدها
        if (req.PermissionIds != null && req.PermissionIds.Count > 0)
        {
            var toAdd = req.PermissionIds.Distinct().Select(pid => new clsRolePermission
            {
                RoleId = req.RoleId,
                PermissionId = pid
            });

            await _context.RolePermissions.AddRangeAsync(toAdd);
        }

        await _context.SaveChangesAsync();
        return Ok(new { ok = true });
    }
}