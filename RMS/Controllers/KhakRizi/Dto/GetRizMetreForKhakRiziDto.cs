using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi.Dto;

public class GetRizMetreForKhakRiziDto
{
    public long Num { get; set; }
    public Guid BarAvordId { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public long Year { get; set; }
}
