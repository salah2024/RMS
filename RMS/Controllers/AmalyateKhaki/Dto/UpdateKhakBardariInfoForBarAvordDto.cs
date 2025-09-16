using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class UpdateKhakBardariInfoForBarAvordDto
    {
        public Guid BarAvordUserId { get; set; }
        public Guid KMKhakBardariId { get; set; }
        public long FromKM { get; set; }
        public long ToKM { get; set; }
        public string HKB { get; set; }
        public long Year { get; set; }
        public List<KhakBardariInfoForBarAvordItemsForUpdateDto> lstItems { get; set; }
    }

    public class KhakBardariInfoForBarAvordItemsForUpdateDto
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
