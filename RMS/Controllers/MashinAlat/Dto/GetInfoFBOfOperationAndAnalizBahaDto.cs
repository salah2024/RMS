using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.MashinAlat.Dto;

public class GetInfoFBOfOperationAndAnalizBahaDto
{
    public int Year { get; set; }
    public NoeFehrestBaha NoeFB { get; set;  }
    public Guid BarAvordId { get; set; }
}
