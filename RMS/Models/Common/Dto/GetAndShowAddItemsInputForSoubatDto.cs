using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Common.Dto
{
    public class GetAndShowAddItemsInputForSoubatDto
    {
        public string ShomarehFB { get; set; }
        public Guid BarAvordUserId { get; set; }
        public long[] ConditionGroupId { get; set; }
        public int LevelNumber { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
    }
}
