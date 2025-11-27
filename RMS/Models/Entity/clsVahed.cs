using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblVahed")]
public class clsVahed
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
