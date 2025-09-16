using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BaseInfo.Dto
{
    public class GetFosoulInputDto
    {
        public long NoeFaslId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
    }
}
