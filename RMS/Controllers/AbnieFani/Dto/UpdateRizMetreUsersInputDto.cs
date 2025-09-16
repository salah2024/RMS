using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AbnieFani.Dto
{
    public class UpdateRizMetreUsersInputDto
    {
        public Guid Id { get; set; }
        public string Sharh { get; set; }
        public decimal? Tedad { get; set; }
        public decimal? Tool { get; set; }
        public decimal? Arz { get; set; }
        public decimal? Ertefa { get; set; }
        public decimal? Vazn { get; set; }
        public string? Des { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
        public Guid BarAvordUserId { get; set; }
        public int LevelNumber { get; set; }
        public string Code { get; set; }
        public long OperationId { get; set; }
        public Guid FBId { get; set; }

    }
}
