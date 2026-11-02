namespace RMS.Controllers.Menu.Dto;

public class SidebarMenuItemVm
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
    public int? ParentId { get; set; }
    public List<SidebarMenuItemVm> Children { get; set; } = new();
}
