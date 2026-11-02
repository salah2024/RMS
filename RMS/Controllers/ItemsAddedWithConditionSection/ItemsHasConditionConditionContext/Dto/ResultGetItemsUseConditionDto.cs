namespace RMS.Controllers.ItemsAddedWithConditionSection.ItemsHasConditionConditionContext.Dto;

public class ResultGetItemsUseConditionDto
{
    public List<ItemFBShomarehUseConditionDto> lstShomarehUse { get; set; }
    public int ConditionType { get; set;  }
}

public class ItemFBShomarehUseConditionDto
{
    public long Id { get; set; }
    public string ItemFBShomareh { get; set; }
}

