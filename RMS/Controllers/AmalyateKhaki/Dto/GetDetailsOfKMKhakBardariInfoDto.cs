using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class GetDetailsOfKMKhakBardariInfoDto
    {
        public Guid AmalyateKhakiInfoForBarAvordId { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
    }
}
