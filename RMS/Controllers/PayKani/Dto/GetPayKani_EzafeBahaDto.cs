using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto;

public class GetPayKani_EzafeBahaDto
{
    public Guid PayKaniInfoForBarAvordId { get; set; }
    public long NoePayKaniEzafeBahaId { get; set; }
    public long Year { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
}
