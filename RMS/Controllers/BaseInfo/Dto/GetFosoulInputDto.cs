using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BaseInfo.Dto
{
    public class GetFosoulInputDto
    {
        public NoeFehrestBaha NoeFBId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public int Year { get; set; }
    }
}
