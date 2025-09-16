using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class DeleteItemsHasConditionAddedToFBAndRizMetreInputDto
    {
        public Guid BarAvordId { get; set; }
        public string strFBShomareh { get; set; }
        public long ConditionGroupId { get; set; }
        public int LevelNumber { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
    }
}
