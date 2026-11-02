using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ConditionGroup.Dto;

public class ConditionGroupDto
{
    public long Id { get; set; }

    public string ConditionGroupName { get; set; }

    public int? Order { get; set; }

    public NoeFehrestBaha NoeFB { get; set; }
}
