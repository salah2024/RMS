using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBarAvordHamlRizMetre")]
public class clsBarAvordHamlRizMetre:clsBaseEntity
{
    [ForeignKey("BarAvordHaml")]
    public Guid BarAvordHamlId { get; set; }
    public clsBarAvordHaml BarAvordHaml { get; set; }
    [ForeignKey("RizMetreUser")]
    public Guid RizMetreId { get;set; }
    public clsRizMetreUsers RizMetreUser { get; set; }
}
