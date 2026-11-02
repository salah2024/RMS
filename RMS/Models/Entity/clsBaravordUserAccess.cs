using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblBaravordUserAccess")]
public class clsBaravordUserAccess:clsBaseEntity
{
    public Guid BaravordUserId { get; set; }

    public clsBaravordUser BaravordUser { get; set; }

    public string UserId { get; set; }

    public ProjectAccessType AccessType { get; set; }
}
