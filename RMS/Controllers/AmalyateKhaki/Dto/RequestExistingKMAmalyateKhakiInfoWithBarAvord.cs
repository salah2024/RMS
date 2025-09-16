using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class RequestExistingKMAmalyateKhakiInfoWithBarAvord
    {
        public Guid BaravordId { get; set; }
        public NoeAmalyatKhaki Type { get; set; }
    }
}
