using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAppOperationInfoDetails")]
public class clsAppOperationInfoDetails:clsBaseEntity
{
    [ForeignKey("AppOperationInfoMain")]
    public Guid AppOperationInfoMainId { get; set; }
    public clsAppOperationInfoMain AppOperationInfoMain { get; set; }

    [ForeignKey("AppQuestion")]
    public long QuetionId { get; set; }
    public clsAppQuestion AppQuestion { get; set; }
    public decimal Answer { get; set; }

}
