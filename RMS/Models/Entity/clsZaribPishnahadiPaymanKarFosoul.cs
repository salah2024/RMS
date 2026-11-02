using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblZaribPishnahadiPaymanKarFosoul")]
public class clsZaribPishnahadiPaymanKarFosoul:clsBaseEntity
{
    [ForeignKey("ZaribPishnahadiPaymanKar")]
    public Guid ZaribPishnahadiPaymanKarId { get; set; }
    public clsZaribPishnahadiPaymanKar ZaribPishnahadiPaymanKar { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public decimal BaravordFasl { get; set; }
    public decimal BaravordFaslBaZarib { get; set; }
    public decimal? PishnahadPaymanKar { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribJoz { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }

}
