using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemsAddedWithConditionSection.ItemsHasConditionConditionContext.Dto;

public class ItemsHasConditionConditionContextDto
{
    public long Id { get; set; }


    public long ItemsHasConditionId { get; set; }

    public string ItemFBShomareh { get; set; }


    public long ConditionContextId { get; set; }

    public string ConditionContextText { get; set; }

    public string ConditionGroupName { get; set; }


    public bool HasEnteringValue { get; set; }

    public string? Des { get; set; }

    public string? DefaultValue { get; set; }

    public string? MinValue { get; set; }

    public string? MaxValue { get; set; }

    public string? StepChange { get; set; }


    public bool IsShow { get; set; }


    public long? ParentId { get; set; }


    public bool MoveToRel { get; set; }

    public bool ViewCheckAllRecords { get; set; }


    public int Year { get; set; }

    public NoeFehrestBaha? NoeFB { get; set; }


    public int EnteringCount { get; set; }


    public string? DesForEnteringValue { get; set; }


    public bool? EnableEditing { get; set; }

    public bool? EnableDeleting { get; set; }

    public bool? AutoSelecting { get; set; }


    public string? IHC_CC_Related { get; set; }
}
