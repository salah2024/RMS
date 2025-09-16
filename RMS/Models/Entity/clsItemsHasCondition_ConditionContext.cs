using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemsHasCondition_ConditionContext")]
public class clsItemsHasCondition_ConditionContext
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("ItemsHasCondition")]
    public long ItemsHasConditionId { get; set; }
    public clsItemsHasCondition ItemsHasCondition { get; set; }
    [ForeignKey("ConditionContext")]
    public long ConditionContextId { get; set; }
    public clsConditionContext ConditionContext { get; set; }

    public bool HasEnteringValue { get; set; }

    public string? Des { get; set; }

    public string? DefaultValue { get; set; }
    public string? MinValue { get; set; }
    public string? MaxValue { get; set; }
    public string? StepChange { get; set; }

    public bool IsShow { get; set; }

    [ForeignKey("ItemsHasCondition_ConditionContext")]
    public long? ParentId { get; set; }
    public clsItemsHasCondition_ConditionContext ItemsHasCondition_ConditionContext { get; set; }

    public bool MoveToRel { get; set; }

    public bool ViewCheckAllRecords { get; set; }
    public int Year { get; set; }
    public int EnteringCount { get; set; }

    /// <summary>
    /// ورود توضیحات برای بخش مقادیر وارده
    /// </summary>
    public string DesForEnteringValue { get; set; }
    public bool? EnableEditing { get; set; }
    public bool? EnableDeleting { get; set; }

}
