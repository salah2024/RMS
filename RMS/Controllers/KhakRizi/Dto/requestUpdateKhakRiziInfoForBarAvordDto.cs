using RMS.Controllers.KhakRizi.EnumKhakRizi;

namespace RMS.Controllers.KhakRizi.Dto;

public class requestUpdateKhakRiziInfoForBarAvordDto
{
    public Guid KhakRiziId { get;set; }
    public Guid BarAvordUserId { get; set; }
    public long FromKM { get; set; }
    public long ToKM { get; set; }
    public EnumRoadType RoadTypeId { get; set; }
    public EnumNoeDaneBandi NoeDaneBandiId { get; set; }
    public string? HajmKhakRiziValues { get; set; }
    public long Year { get; set; }
}
