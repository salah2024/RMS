using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblConditionContext")]
public class clsConditionContext
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Context { get; set; }

    [ForeignKey("ConditionGroup")]
    public long ConditionGroupId { get; set; }
    public clsConditionGroup ConditionGroup { get; set; }
    public int Year { get; set; }
    public bool HasDes { get; set; }
    public string Des { get; set; }
    public string TitleDes { get; set; }
    /// <summary>
    /// آیتم های وابسته به این شرط که در صورت حذف آیتمی که دارای این شرایط باشد آیتم های مربوطه اش نیز حذف گردد
    /// این فیلد بصورت رشته ای بوده و میتواند چندین وابسته داشته باشد که با ,  از هم جدا میشوند
    /// </summary>
    public string? ConditionContextRel { get; set; }
}
