using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class GetTreeInputDto
    {
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public Guid BarAvordUserId { get; set; }
    }
}
