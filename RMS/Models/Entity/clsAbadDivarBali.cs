using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAbadDivarBali")]
public class clsAbadDivarBali
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public decimal h { get; set; }

    public decimal x { get; set; }

    public decimal b { get; set; }

    public decimal f { get; set; }

    public decimal m { get; set; }
}
