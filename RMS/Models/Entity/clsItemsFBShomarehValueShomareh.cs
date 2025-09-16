using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemsFBShomarehValueShomareh")]
public class clsItemsFBShomarehValueShomareh
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string FBShomareh { get; set; }

    [ForeignKey("BaravordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }

    public string GetValuesShomareh { get; set; }

    public int Type { get; set; }
}
