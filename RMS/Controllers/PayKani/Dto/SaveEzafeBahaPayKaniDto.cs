using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class SaveEzafeBahaPayKaniDto
    {
        public long NoeKhakBardariEzafeBahaId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public long Year { get;set; }
    }
}
