using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblShahr")]
public class clsShahr
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Ostan")]
    public long OstanId { get; set; }
    public clsOstan Ostan { get; set; }
}
