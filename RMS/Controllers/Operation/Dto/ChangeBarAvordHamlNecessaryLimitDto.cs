using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class ChangeBarAvordHamlNecessaryLimitDto
{
    public bool blnChecked { get; set; }
    public Guid BarAvordId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
}
