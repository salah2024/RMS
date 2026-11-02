using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblTajhizKargahDarsad")]
public class clsTajhizKargahDarsad:clsBaseEntity
{
    [ForeignKey("BarAvordUser")]
    public Guid BarAvordUserId { get; set;  }
    public clsBaravordUser BarAvordUser { get; set; }   
    public NoeFehrestBaha NoeFB { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Darsad { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Meghdar { get; set;  }

}
