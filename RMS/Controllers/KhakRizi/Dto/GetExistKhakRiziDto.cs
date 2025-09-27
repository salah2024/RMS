using RMS.Controllers.KhakRizi.EnumKhakRizi;
using RMS.Models.Entity;

namespace RMS.Controllers.KhakRizi.Dto;

public class GetExistKhakRiziDto
{
    public Guid KhakRiziID { get; set; }
    public Guid BarAvordId { get; set; }
    public int KMNum { get; set; }
    public string FromKM { get; set; }
    public string ToKM { get; set; }
    public string FromKMSplit { get; set; }
    public string ToKMSplit { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    public EnumRoadType NoeRah { get; set; }
    public EnumNoeDaneBandi NoeDaneBandi { get; set; }
    public string? NoeHajmKhakRizi_Value { get; set; }

}
