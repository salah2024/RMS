using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblPolVaAbroBarAvord")]
public class clsPolVaAbroBarAvord:clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    public int TedadDahaneh { get; set; }

    public double DahaneAbro { get; set; }

    public decimal HadAksarErtefaKoole { get; set; }

    public string Hs { get; set; }

    public int ZavieBie { get; set; }

    public decimal ToolAbro { get; set; }

    public decimal X { get; set; }

    public decimal Y { get; set; }

    public short NoeBanaii { get; set; }

    public short NahveEjraDal { get; set; }

    public long PolNum { get; set; }
}
