using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class SaveHamlValueDto
{
    public long OperationId { get; set; }
    public decimal Value { get; set; }
    public Guid BarAvordId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
}
