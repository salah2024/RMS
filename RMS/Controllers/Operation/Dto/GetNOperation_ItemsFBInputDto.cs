using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class GetNOperation_ItemsFBInputDto
    {
        public string ItemsFBShomareh { get; set; }
        public long Operation { get; set; }
        public Guid BarAvordUserId { get; set; }
        public int Type { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int LevelNumber { get; set; }

    }
}
