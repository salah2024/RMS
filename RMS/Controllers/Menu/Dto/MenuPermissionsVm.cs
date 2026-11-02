using RMS.Controllers.RolePermissions.Dto;

namespace RMS.Controllers.Menu.Dto;

public class MenuPermissionsVm
{
    public int MenuId { get; set; }
    public string MenuTitle { get; set; } = "";
    public List<PermissionCheckboxVm> Items { get; set; } = new();
}
