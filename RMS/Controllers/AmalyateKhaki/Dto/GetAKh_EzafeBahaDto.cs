using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto;

public class GetAKh_EzafeBahaDto
{
    public Guid AmalyateKhakiInfoForBarAvordId { get; set; }
    public long NoeKhakBardariEzafeBahaId { get; set; }
    public long Year { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
}
