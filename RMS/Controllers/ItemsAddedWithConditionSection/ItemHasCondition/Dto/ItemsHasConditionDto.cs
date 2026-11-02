using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ItemHasCondition.Dto;

public class ItemsHasConditionDto
{
    public long Id { get; set; }

    public string ItemFBShomareh { get; set; }

    public int Year { get; set; }

    public NoeFehrestBaha? NoeFB { get; set; }
}
