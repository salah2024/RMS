using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemFBStar.Dto;

public class GetCurrentRizMetreStarForShowBarAvordDto
{
    public Guid BarAvordUserId { get; set; }
    public string ItemFBShomareh { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
}
