using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBaravordZaribManteghe")]
public class clsBaravordZaribManteghe
{
    [Key]
    public long Id { get; set; }
    [ForeignKey("BaravordUser")]
    public Guid BaravordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribManteghe { get; set; }
    public long OstanId { get; set; }
    public long ShahrId { get; set; }
    public long BakhshId { get; set; }
}
