using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class DeleteRizMetreInputFromShowBarAvordDto
{
    public Guid Id { get; set; }
    public Guid BarAvordUserId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
}
