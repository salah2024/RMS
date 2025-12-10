using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblBarAvordHaml")]
public class clsBarAvordHaml : clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public string FBShomareh { get; set; }
    public string FBShomarehHaml { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? Value { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? ValueBase { get; set; }
}
