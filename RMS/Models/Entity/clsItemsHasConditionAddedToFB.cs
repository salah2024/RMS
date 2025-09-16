using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemsHasConditionAddedToFB")]
public class clsItemsHasConditionAddedToFB:clsBaseEntity
{
    public string FBShomareh { get; set; }

    [ForeignKey("BarAvordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BarAvordUser { get; set; }

    public decimal Meghdar { get; set; }
    /// <summary>
    /// در صورتی که از ورودی دو مقدار بگیریم مقدار دوم را اینجا بایستی زخیره کرد
    /// </summary>
    public decimal Meghdar2 { get; set; }

    [ForeignKey("ItemsHasCondition_ConditionContext")]
    public long ItemsHasCondition_ConditionContextId { get; set; }
    public clsItemsHasCondition_ConditionContext ItemsHasCondition_ConditionContext { get; set; }

    [ForeignKey("ConditionGroup")]
    public long ConditionGroupId { get; set; }
    public clsConditionGroup ConditionGroup { get; set; }
}
