using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.KhakRizi.Dto;

public class RequestSaveKhakRiziInfoForBarAvordDto
{
    public Guid BarAvordUserId { get; set; }
    public long FromKM { get; set; }
    public long ToKM { get; set; }
    public int RoadTypeId { get; set; }
    public int NoeDaneBandiId { get; set; }
    public int HajmKhakRiziId { get; set; }
    public decimal HajmKhakRiziValue { get; set; }

}
