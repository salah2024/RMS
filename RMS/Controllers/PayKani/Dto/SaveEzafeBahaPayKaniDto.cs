using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class SaveEzafeBahaPayKaniDto
    {
        public long NoeKhakBardariEzafeBahaId { get; set; }
        public Guid PayKaniInfoForBarAvordId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public long Year { get;set; }
    }

    public class DeletePayKaniInfoForBarAvordDto
    {
        public Guid BarAvordUserId { get; set; }
        public Guid KMPayKaniId { get; set; }
        public long Year { get; set; }
        public int KMNum { get; set; }
    }
}
