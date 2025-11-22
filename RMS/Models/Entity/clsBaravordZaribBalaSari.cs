using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBaravordZaribBalaSari")]
public class clsBaravordZaribBalaSari
{
    [Key]
    public long Id { get; set; }
    [ForeignKey("BaravordUser")]
    public Guid BaravordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribBalasari { get; set; }
    public int Tarh { get; set; }   
    public int Monaghese { get; set; }
}
