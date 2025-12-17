using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common.Dto;

public class RequestGetZarayebDto
{
    public Guid BarAvordId { get; set; }
    public int Year { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
}
