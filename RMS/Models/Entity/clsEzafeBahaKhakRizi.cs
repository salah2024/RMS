using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblEzafeBahaKhakRizi")]
public class clsEzafeBahaKhakRizi
{
    [Key]
    public long Id { get;set; }
    [ForeignKey("ConditionContext")]
    public long ConditionContextId { get; set; }
    public clsConditionContext ConditionContext { get; set; }
    public long Year { get; set; }
}
