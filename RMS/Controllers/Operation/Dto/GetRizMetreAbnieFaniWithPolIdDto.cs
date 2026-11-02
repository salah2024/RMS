using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class GetRizMetreAbnieFaniWithPolIdDto
{
    public Guid PolId { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public int Year { get; set;  }
}
