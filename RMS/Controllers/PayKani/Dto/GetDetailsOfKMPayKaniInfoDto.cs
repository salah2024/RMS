using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class GetDetailsOfKMPayKaniInfoDto
    {
        public Guid PayKaniInfoForBarAvordId { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
        public int Type { get; set; }
    }
}
