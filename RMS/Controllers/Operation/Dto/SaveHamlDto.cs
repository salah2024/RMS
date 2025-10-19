namespace RMS.Controllers.Operation.Dto;

public class SaveHamlDto
{
    public Guid BarAvordUserId { get; set; }
    public long Year { get;set; }
    public string ItemFBShomareh { get; set; }
    public Guid BarAvordHamlId { get;set; }
    public decimal? MeghdarJoz { get; set; }
}
