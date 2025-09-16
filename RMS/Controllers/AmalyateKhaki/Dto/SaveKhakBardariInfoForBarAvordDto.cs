using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class SaveKhakBardariInfoForBarAvordDto
    {
        public Guid BarAvordUserId { get; set; }
        public NoeAmalyatKhaki Type { get; set; }
        public long FromKM { get; set; }
        public long ToKM { get; set; }
        public string HKB { get; set; }
        public List<KhakBardariInfoForBarAvordItemsDto> lstItems { get; set; }
        //public string DetailValue1 { get; set; }
        //public string DetailValueOfReCycle1 { get; set; }
        //public string DetailValueOfVarize1 { get; set; }
        //public string DetailValueOfHaml1 { get; set; }
        //public bool ckFaseleHaml1 { get; set; }
        //public string DetailValueOfFaseleHaml1 { get; set; }

        //public string DetailValue2 { get; set; }
        //public string DetailValueOfReCycle2 { get; set; }
        //public string DetailValueOfVarize2 { get; set; }
        //public string DetailValueOfHaml2 { get; set; }
        //public bool ckFaseleHaml2 { get; set; }
        //public bool ckPakhsh2 { get; set; }
        //public string DetailValueOfFaseleHaml2 { get; set; }

        //public string DetailValue3 { get; set; }
        //public string DetailValueOfReCycle3 { get; set; }
        //public string DetailValueOfVarize3 { get; set; }
        //public string DetailValueOfHaml3 { get; set; }
        //public bool ckFaseleHaml3 { get; set; }
        //public bool ckPakhsh3 { get; set; }
        //public string DetailValueOfFaseleHaml3 { get; set; }

        //public string DetailValue4 { get; set; }
        //public string DetailValueOfReCycle4 { get; set; }
        //public string DetailValueOfVarize4 { get; set; }
        //public string DetailValueOfHaml4 { get; set; }
        //public bool ckFaseleHaml4 { get; set; }
        //public bool ckPakhsh4 { get; set; }
        //public string DetailValueOfFaseleHaml4 { get; set; }

        //public string DetailValue5 { get; set; }
        //public string DetailValueOfReCycle5 { get; set; }
        //public string DetailValueOfVarize5 { get; set; }
        //public string DetailValueOfHaml5 { get; set; }
        //public bool ckFaseleHaml5 { get; set; }
        //public bool ckPakhsh5 { get; set; }
        //public string DetailValueOfFaseleHaml5 { get; set; }

        //public string DetailValue6 { get; set; }
        //public string DetailValueOfReCycle6 { get; set; }
        //public string DetailValueOfVarize6 { get; set; }
        //public string DetailValueOfHaml6 { get; set; }
        //public bool ckFaseleHaml6 { get; set; }
        //public bool ckPakhsh6 { get; set; }
        //public bool ckUseNatel6 { get; set; }
        //public bool ckUseHydrolicPic6 { get; set; }
        //public string DetailValueOfFaseleHaml6 { get; set; }

        //public string DetailValue7 { get; set; }
        //public string DetailValueOfReCycle7 { get; set; }
        //public string DetailValueOfVarize7 { get; set; }
        //public string DetailValueOfHaml7 { get; set; }
        //public bool ckFaseleHaml7 { get; set; }
        //public bool ckPakhsh7 { get; set; }
        //public bool ckUseNatel7 { get; set; }
        //public string DetailValueOfFaseleHaml7 { get; set; }
    }


    public class KhakBardariInfoForBarAvordItemsDto
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
