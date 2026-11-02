using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Menu.Dto;
using RMS.Controllers.RolePermissions.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.Menu;

public class MenusController : Controller
{
    private readonly ApplicationDbContext _context;
    public MenusController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Menus
    public async Task<IActionResult> Index()
    {
        var items = await _context.MenuItems
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Title)
            .ToListAsync();

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("Index", items);

        return View(items);
    }

    // GET: /Menus/Create
    public async Task<IActionResult> Create()
    {
        var vm = new MenuEditVm
        {
            Parents = await GetParentsAsync()
        };
        return View(vm);
    }

    // POST: /Menus/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MenuEditVm vm)
    {
        vm.Title = (vm.Title ?? "").Trim();
        vm.Url = string.IsNullOrWhiteSpace(vm.Url) ? null : vm.Url.Trim();
        vm.Icon = string.IsNullOrWhiteSpace(vm.Icon) ? null : vm.Icon.Trim();

        if (string.IsNullOrWhiteSpace(vm.Title))
            ModelState.AddModelError(nameof(vm.Title), "Title الزامی است.");

        if (!ModelState.IsValid)
        {
            vm.Parents = await GetParentsAsync();
            return View(vm);
        }

        _context.MenuItems.Add(new clsMenuItem
        {
            Title = vm.Title,
            Url = vm.Url,
            Icon = vm.Icon,
            ParentId = vm.ParentId,
            SortOrder = vm.SortOrder,
            IsActive = vm.IsActive
        });

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: /Menus/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var m = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
        if (m == null) return NotFound();

        var vm = new MenuEditVm
        {
            Id = m.Id,
            Title = m.Title,
            Url = m.Url,
            Icon = m.Icon,
            ParentId = m.ParentId,
            SortOrder = m.SortOrder,
            IsActive = m.IsActive,
            Parents = await GetParentsAsync(id)
        };
        return View(vm);
    }

    // POST: /Menus/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MenuEditVm vm)
    {
        vm.Title = (vm.Title ?? "").Trim();
        vm.Url = string.IsNullOrWhiteSpace(vm.Url) ? null : vm.Url.Trim();
        vm.Icon = string.IsNullOrWhiteSpace(vm.Icon) ? null : vm.Icon.Trim();

        if (string.IsNullOrWhiteSpace(vm.Title))
            ModelState.AddModelError(nameof(vm.Title), "Title الزامی است.");

        if (!ModelState.IsValid)
        {
            vm.Parents = await GetParentsAsync(vm.Id);
            return View(vm);
        }

        var m = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (m == null) return NotFound();

        m.Title = vm.Title;
        m.Url = vm.Url;
        m.Icon = vm.Icon;
        m.ParentId = vm.ParentId;
        m.SortOrder = vm.SortOrder;
        m.IsActive = vm.IsActive;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: /Menus/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var m = await _context.MenuItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (m == null) return NotFound();
        return View(m);
    }

    // POST: /Menus/Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var m = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
        if (m == null) return NotFound();

        // اگر child دارد بهتره اجازه حذف ندی (ساده)
        var hasChild = await _context.MenuItems.AnyAsync(x => x.ParentId == id);
        if (hasChild)
        {
            TempData["err"] = "این منو زیرمنو دارد؛ ابتدا زیرمنوها را حذف کنید.";
            return RedirectToAction(nameof(Index));
        }

        _context.MenuItems.Remove(m);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<List<MenuParentOption>> GetParentsAsync(int? excludeId = null)
    {
        var q = _context.MenuItems.AsNoTracking();
        if (excludeId.HasValue)
            q = q.Where(x => x.Id != excludeId.Value);

        var items = await q.OrderBy(x => x.Title).ToListAsync();

        return items.Select(x => new MenuParentOption
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();
    }


    public async Task<IActionResult> Permissions(int id)
    {
        var menu = await _context.MenuItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (menu == null) return NotFound();

        var selectedIds = await _context.MenuPermissions
            .Where(x => x.MenuItemId == id)
            .Select(x => x.PermissionId)
            .ToListAsync();

        var perms = await _context.Permissions
            .AsNoTracking()
            .OrderBy(p => p.Group)
            .ThenBy(p => p.Key)
            .ToListAsync();

        var vm = new MenuPermissionsVm
        {
            MenuId = id,
            MenuTitle = menu.Title,
            Items = perms.Select(p => new PermissionCheckboxVm
            {
                PermissionId = p.Id,
                Key = p.Key,
                Title = p.Title,
                Group = p.Group,
                IsChecked = selectedIds.Contains(p.Id)
            }).ToList()
        };

        return View(vm);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Permissions(MenuPermissionsVm vm)
    {
        var menu = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == vm.MenuId);
        if (menu == null) return NotFound();

        var existing = await _context.MenuPermissions
            .Where(x => x.MenuItemId == vm.MenuId)
            .ToListAsync();

        _context.MenuPermissions.RemoveRange(existing);

        var selected = (vm.Items ?? new())
            .Where(x => x.IsChecked)
            .Select(x => new clsMenuPermission
            {
                MenuItemId = vm.MenuId,
                PermissionId = x.PermissionId
            });

        await _context.MenuPermissions.AddRangeAsync(selected);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
