using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class SavePayKaniInfoForBarAvordDto
    {
        public Guid BarAvordUserId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        /// <summary>
        /// 2-پی کنی
        /// 3- کانال کنی
        /// </summary>
        public int Type { get; set; }
        public long FromKM { get; set; }
        public long ToKM { get; set; }
        public string HKB { get; set; }
        public List<PayKaniInfoForBarAvordItemsDto> lstItems { get; set; }
        
    }


    public class PayKaniInfoForBarAvordItemsDto
    {
        public string DetailValue { get; set; }
        public string DarsadValue { get; set; }
        public string DetailValueOfReCycle { get; set; }
        public string DarsadValueOfReCycle { get; set; }
        public string DetailValueOfVarize { get; set; }
        public string DarsadValueOfVarize { get; set; }
        public string DetailValueOfHaml { get; set; }
        public string DarsadValueOfHaml { get; set; }
        public long NoeKhakBardari { get; set; }
    }
}
