using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class GetAndShowAddItemsInputDto
    {
        public string strRBCode { get; set; }
        public long OperationId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public long ConditionGroupId { get; set; }
        public int LevelNumber { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }

    }
}
