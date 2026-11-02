using RMS.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class ItemsAddingToFBForShowAddItemsDto
{
    public long Id { get; set; }
    public long ItemsHasCondition_ConditionContextId { get; set; }

    public string AddedItems { get; set; }

    public string? Condition { get; set; }

    public string? FinalWorking { get; set; }

    public short ConditionType { get; set; }

    public string? DesOfAddingItems { get; set; }

    public string? UseItemForAdd { get; set; }

    public string? FieldsAdding { get; set; }
    public int Year { get; set; }
    public NoeFehrestBaha? NoeFB { get; set; }
    public string? CharacterPlus { get; set; }
    public string? Sharh { get; set; }
    public string? Shomareh { get; set; }
    public string? ConditionContextRel { get; set; }
    public long ConditionContextId { get; set; }
    public string? MaxValueCheck { get; set; }
    public string? MinValueCheck { get; set; }
    public int EnteringCount { get; set; }
}
