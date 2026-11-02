using Microsoft.AspNetCore.Identity;

namespace RMS.Controllers.RolePermissions.Dto;

public class RolePermissionsIndexVm
{
    public string? SelectedRoleId { get; set; }
    public List<IdentityRole> Roles { get; set; } = new();
    public List<PermissionCheckboxVm> Items { get; set; } = new();
}
