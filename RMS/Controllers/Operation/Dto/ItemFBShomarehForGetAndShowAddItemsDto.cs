using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class ItemFBShomarehForGetAndShowAddItemsDto
    {
        public string ItemFBShomareh { get;set; }
        public string Des { get;set; }
        public List<ItemFBShomarehForGetAndShowAddItemsFieldsDto> ItemFields { get; set; }

    }
    public class ItemFBShomarehForGetAndShowAddItemsFieldsDto
    {
        public string Shomareh { get; set; }
        public ItemFieldType FieldType { get; set; }
        public string Vahed { get; set; }
        public bool IsEnteringValue { get; set; }
        public bool EssentialValue { get; set; }
    }
}
