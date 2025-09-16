using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class CheckRutinInputDto
    {
        public long AddedOperationId { get; set; }
        public string ItemsFBShomareh { get; set; }
        public Guid BarAvordUserId { get; set; }
        public int Type { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int LevelNumber { get; set; }
    }
}
