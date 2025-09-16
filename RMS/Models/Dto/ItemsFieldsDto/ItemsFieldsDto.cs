using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Dto.ItemsFieldsDto
{
    public class ItemsFieldsDto
    {
        public string ItemShomareh { get;set; }
        public NoeFehrestBaha NoeFB { get;set; }
        public bool IsEnteringValue { get; set; }   
        public string Vahed { get; set; }
        public int FieldType { get; set; }
        public long OperationId { get; set; }
    }
}
