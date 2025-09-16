using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblHadAksarErtefaKoole")]
public class clsHadAksarErtefaKoole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public decimal HadAksarErtefaKoole { get; set; }
    public int TedadDahaneh {  get; set; }
    public int DahaneAbro { get; set; }
}
