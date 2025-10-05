using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi.Dto;

public class RequestGetRizMetreEzafeBahaForKhakRiziDto
{
    public long ConditionContextId { get; set; }
    public Guid BarAvordId { get; set; }
    public int Num { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public long Year { get; set; }

}
