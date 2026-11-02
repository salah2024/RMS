namespace RMS.Controllers.RolePermissions.Dto;

public class SaveRolePermissionsRequest
{
    public string RoleId { get; set; } = "";
    public List<int> PermissionIds { get; set; } = new();
}
