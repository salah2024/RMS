using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblZaribPishnahadiPaymanKar")]
public class clsZaribPishnahadiPaymanKar : clsBaseEntity
{
    [ForeignKey("baravordUser")]
    public Guid BarAvordUserId { get; set; }
    public clsBaravordUser baravordUser { get; set; }
    public decimal? PishnahadPaymanKarTajhiz { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribJozTajhiz { get; set; }
    public int Year { get; set; }
    [ForeignKey("company")]
    public Guid? CompanyId { get; set; }
    public clsCompany company { get; set; }
    public virtual ICollection<clsZaribPishnahadiPaymanKarFosoul> lstZaribPishnahadiPaymanKarFosoul { get; set; }
}
