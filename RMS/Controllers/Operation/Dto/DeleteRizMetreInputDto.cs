using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class DeleteRizMetreInputDto
    {
        public Guid Id { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
        public Guid BarAvordUserId { get; set; }
        public Guid FBId { get; set; }
        public long OperationId { get; set; }
    }
    
    public class DeleteRelRizMetreInputDto
    {
        public Guid Id { get; set; }
        public Guid BarAvordUserId { get; set; }
        public long OperationId { get; set; }
    }
}
