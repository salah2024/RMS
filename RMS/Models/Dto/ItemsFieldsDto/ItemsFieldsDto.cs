using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Dto.ItemsFieldsDto
{
    public class ItemsFieldsDto
    {
        public string ItemShomareh { get;set; }
        public NoeFehrestBaha NoeFB { get;set; }
        public bool IsEnteringValue { get; set; }   
        public string Vahed { get; set; }
        public ItemFieldType FieldType { get; set; }
        public long OperationId { get; set; }
        public decimal? DefaultValue { get; set; }
        public decimal? MaxValue { get; set; }
        public decimal? MinValue { get; set; }

    }
}
