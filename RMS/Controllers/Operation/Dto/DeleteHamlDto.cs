using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class DeleteHamlDto
{
    public Guid BarAvordId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public string FBShomareh { get; set; }
    public long RMShomareh { get; set; }
}
