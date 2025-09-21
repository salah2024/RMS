using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblKhakRiziItem")]
public class clsKhakRiziItem
{
    [Key]
    public long Id { get; set; }
    public string Condition { get; set; }
    public string ItemFBShomareh { get; set; }
    public long Year { get; set; }
}
