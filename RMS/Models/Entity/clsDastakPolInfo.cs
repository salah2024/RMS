using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblDastakPolInfo")]
public class clsDastakPolInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("polVaAbroBarAvord")]
    public Guid PolVaAbroId { get; set; }
    public clsPolVaAbroBarAvord polVaAbroBarAvord { get; set; }
    public long Shomareh { get; set; }

    public decimal ToolW { get; set; }

    public int Zavie { get; set; }

    public decimal hMin { get; set; }
}
