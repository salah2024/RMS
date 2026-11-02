namespace RMS.Controllers.Menu.Dto;

public class MenuEditVm
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public int? ParentId { get; set; }
    public int SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    public List<MenuParentOption> Parents { get; set; } = new();
}
