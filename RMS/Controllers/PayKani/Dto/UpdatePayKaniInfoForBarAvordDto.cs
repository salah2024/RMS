using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class UpdatePayKaniInfoForBarAvordDto
    {
        public Guid BarAvordUserId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public Guid KMPayKaniId { get; set; }
        public long FromKM { get; set; }
        public long ToKM { get; set; }
        public string HKB { get; set; }
        public long Year { get; set; }
        public int KMNum { get; set; }
        public List<PayKaniInfoForBarAvordItemsForUpdateDto> lstItems { get; set; }
    }

    public class PayKaniInfoForBarAvordItemsForUpdateDto
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
