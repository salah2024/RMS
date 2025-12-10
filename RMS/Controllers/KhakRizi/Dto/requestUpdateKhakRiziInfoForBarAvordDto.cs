using RMS.Controllers.KhakRizi.EnumKhakRizi;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi.Dto;

public class requestUpdateKhakRiziInfoForBarAvordDto
{
    public Guid BarAvordUserId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public int Num { get; set; }
    public long FromKM { get; set; }
    public long ToKM { get; set; }
    public EnumRoadType RoadTypeId { get; set; }
    public EnumNoeDaneBandi NoeDaneBandiId { get; set; }
    public string? HajmKhakRiziValues { get; set; }
    public long Year { get; set; }
}
