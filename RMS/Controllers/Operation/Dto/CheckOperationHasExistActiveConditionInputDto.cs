using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class CheckOperationHasExistActiveConditionInputDto
    {
        public string OperationId { get; set; }
        public Guid BarAvordId { get; set; }
        public string Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public Guid FBId { get;set; }
    }
}
