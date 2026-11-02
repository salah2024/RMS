namespace RMS.Controllers.RolePermissions.Dto;

public class PermissionCheckboxVm
{
    public int PermissionId { get; set; }
    public string Key { get; set; } = "";
    public string Title { get; set; } = "";
    public string? Group { get; set; }
    public bool IsChecked { get; set; }
}
