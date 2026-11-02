using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi.Dto;

public class CheckOnChangeKhakRiziDto
{
    public Guid BarAvordUserId { get; set; }
    public int Year  { get;set; }
    public NoeFehrestBaha NoeFB { get;set; }
}
