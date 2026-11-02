using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblConditionGroup")]
public class clsConditionGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string ConditionGroupName { get; set; }
    public int? Order { get; set; }
    public NoeFehrestBaha NoeFB {get;set;   }
}
