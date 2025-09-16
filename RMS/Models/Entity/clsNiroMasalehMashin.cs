using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblNiroMasalehMashin")]
public class clsNiroMasalehMashin
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string CodeAmel { get; set; } 

    public string Title { get; set; }
}
