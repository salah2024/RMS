namespace RMS.Services.ViewComponents;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Menu.Dto;

public class SidebarMenuViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SidebarMenuViewComponent(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // 1) اگر لاگین نیست: فقط منوهای public (منوهایی که MenuPermission ندارند)
        if (User?.Identity?.IsAuthenticated != true)
        {
            var publicTree = await BuildTreeAsync(permissionIds: null);
            return View("Default", publicTree);
        }

        // 2) PermissionId های کاربر را از RolePermissions بگیر
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
        {
            var publicTree = await BuildTreeAsync(permissionIds: null);
            return View("Default", publicTree);
        }

        var roleNames = await _userManager.GetRolesAsync(user);
        var roleIds = await _roleManager.Roles
            .Where(r => roleNames.Contains(r.Name!))
            .Select(r => r.Id)
            .ToListAsync();

        //var roleIds = await _context.UserRoles
        //    .Where(x => x.UserId == user.Id)
        //    .Select(x => x.RoleId)
        //    .ToListAsync();

        var permissionIds = await _context.RolePermissions
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.PermissionId)
            .Distinct()
            .ToListAsync();

        var tree = await BuildTreeAsync(permissionIds);
        return View("Default", tree);
    }

    private async Task<List<SidebarMenuItemVm>> BuildTreeAsync(List<int>? permissionIds)
    {
        // همه منوهای فعال
        var allMenus = await _context.MenuItems
            .AsNoTracking()
            .Where(m => m.IsActive)
            .Select(m => new { m.Id, m.Title, m.Url, m.Icon, m.SortOrder, m.ParentId })
            .ToListAsync();

        var publicMenuIds = await _context.MenuItems
            .AsNoTracking()
            .Where(m => m.IsActive)
            .Where(m => m.Url != null)
            .Where(m => !_context.MenuPermissions.Any(mp => mp.MenuItemId == m.Id))
            .Select(m => m.Id)
            .ToListAsync();

        // منوهای public (هیچ MenuPermission ندارند)
        //var publicMenuIds = await _context.MenuItems
        //    .AsNoTracking()
        //    .Where(m => m.IsActive)
        //    .Where(m => !_context.MenuPermissions.Any(mp => mp.MenuItemId == m.Id))
        //    .Select(m => m.Id)
        //    .ToListAsync();
//publicMenuIds
        var allowed = new HashSet<int>();

        // اگر permissionIds داریم، منوهایی که یکی از Permissionها را دارند هم مجاز هستند
        if (permissionIds != null && permissionIds.Count > 0)
        {
            var permittedMenuIds = await _context.MenuPermissions
                .AsNoTracking()
                .Where(mp => permissionIds.Contains(mp.PermissionId))
                .Select(mp => mp.MenuItemId)
                .Distinct()
                .ToListAsync();

            foreach (var id in permittedMenuIds)
                allowed.Add(id);
        }

        // برای اینکه درخت خراب نشه، parentهای لازم را هم اضافه می‌کنیم
        bool added;
        do
        {
            added = false;
            foreach (var m in allMenus)
            {
                if (m.ParentId.HasValue && allowed.Contains(m.Id) && !allowed.Contains(m.ParentId.Value))
                {
                    allowed.Add(m.ParentId.Value);
                    added = true;
                }
            }
        } while (added);

        // ساخت VM
        var vms = allMenus
            .Where(m => allowed.Contains(m.Id))
            .Select(m => new SidebarMenuItemVm
            {
                Id = m.Id,
                Title = m.Title,
                Url = m.Url,
                Icon = m.Icon,
                SortOrder = m.SortOrder,
                ParentId = m.ParentId
            })
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Title)
            .ToList();

        // ساخت درخت
        var dict = vms.ToDictionary(x => x.Id);
        var roots = new List<SidebarMenuItemVm>();

        foreach (var item in vms)
        {
            if (item.ParentId.HasValue && dict.ContainsKey(item.ParentId.Value))
                dict[item.ParentId.Value].Children.Add(item);
            else
                roots.Add(item);
        }

        // مرتب‌سازی children
        void SortTree(List<SidebarMenuItemVm> nodes)
        {
            nodes.Sort((a, b) =>
            {
                var c = a.SortOrder.CompareTo(b.SortOrder);
                return c != 0 ? c : string.Compare(a.Title, b.Title, StringComparison.Ordinal);
            });
            foreach (var n in nodes) SortTree(n.Children);
        }
        SortTree(roots);

        return roots;
    }
}