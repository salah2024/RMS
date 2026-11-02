using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Controllers.PaymanKar.Dto;

public class ZaribPishnahadiPaymanKarDto
{
    public int Radif { get; set; }
    public Guid ID { get; set;  }
    public string Code { get; set; }
    public string Title { get; set; }
    public decimal BaravordFasl { get; set; }
    public decimal BaravordFaslBaZarib { get; set; }
    public decimal? PishnahadPaymanKar { get; set; }
    public decimal? ZaribJoz { get; set; }
}
