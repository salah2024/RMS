using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.PayKani.Dto;

public class SumRizMetreWithOperationIdDto
{
    public Guid BarAvordUserId { get; set; }
    public long OpId { get; set; }
    public int Year { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
}
