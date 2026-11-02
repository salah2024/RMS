using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

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
    public NoeFehrestBaha NoeFB { get; set; }
    public int Type { get; set;  }
    public bool? hasEditButton { get; set; }
    public bool? hasDelButton { get; set; }
    public bool? HasEnteringValue { get; set;  }
}
