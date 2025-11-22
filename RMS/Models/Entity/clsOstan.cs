using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOstan")]
public class clsOstan
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
}
