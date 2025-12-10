using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblBaravordUser")]
public class clsBaravordUser:clsBaseEntity
{
    public Guid UserId { get; set; }
    public long Num { get; set; }
    public NoeBarAvord Type { get; set; }
    public string NoeFBs { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribBalasari { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribManteghe { get; set; }

}
