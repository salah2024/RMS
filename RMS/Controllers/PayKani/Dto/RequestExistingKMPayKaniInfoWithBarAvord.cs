using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class RequestExistingKMPayKaniInfoWithBarAvord
    {
        public Guid BaravordId { get; set; }
        /// <summary>
        /// 1-پی کنی
        /// 2-کانال کنی
        /// </summary>
        public int Type { get; set; }
    }
}
