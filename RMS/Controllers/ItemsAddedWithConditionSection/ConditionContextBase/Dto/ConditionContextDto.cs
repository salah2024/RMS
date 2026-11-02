using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ConditionContext.Dto;

public class ConditionContextDto
{
    public long Id { get; set; }

    public string Context { get; set; }

    public long ConditionGroupId { get; set; }

    public string ConditionGroupName { get; set; }

    public int Year { get; set; }

    public string Des { get; set; }

    public string TitleDes { get; set; }

    public string ConditionContextRel { get; set; }

    public NoeFehrestBaha NoeFB { get; set; }
}