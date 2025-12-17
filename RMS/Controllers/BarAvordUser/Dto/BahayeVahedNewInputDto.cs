using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class BahayeVahedNewInputDto
    {
        public Guid FBId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public string BahayeVahedNew { get; set; }
        public string itemFbShomareh { get; set; }
        public int Year { get; set; }
    }
}
