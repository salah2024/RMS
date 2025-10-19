using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBarAvordHaml")]
public class clsBarAvordHaml:clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    public string FBShomareh { get; set; }
}
