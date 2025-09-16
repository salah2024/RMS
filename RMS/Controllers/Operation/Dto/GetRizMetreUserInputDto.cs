using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class GetRizMetreUserInputDto
    {
        public Guid FBId { get; set; }
        public string IsFromAddedOperation { get; set; }
        public Guid BarAvordId { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
        public long OperationId { get; set; }
        public string ForItem { get; set; }
        public int LevelNumber { get; set; }
    }
}
