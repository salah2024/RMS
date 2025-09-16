using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemsAddingToFB")]
public class clsItemsAddingToFB
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("ItemsHasCondition_ConditionContext")]
    public long ItemsHasCondition_ConditionContextId { get; set; }
    public clsItemsHasCondition_ConditionContext ItemsHasCondition_ConditionContext { get; set; }

    public string AddedItems { get; set; }

    public string? Condition { get; set; }

    public string? FinalWorking { get; set; }

    public short ConditionType { get; set; }

    public string? DesOfAddingItems { get; set; }

    public string? UseItemForAdd { get; set; }

    public string? FieldsAdding { get; set; }
    public int Year { get; set; }
    public string? CharacterPlus { get; set; }
}
