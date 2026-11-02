using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.AbnieFaniEnum;

namespace RMS.Models.Entity;

[Table("tblPolVaAbroBarAvordRizMetre")]
public class clsPolVaAbroBarAvordRizMetre:clsBaseEntity
{
    [ForeignKey("PolVaAbroBarAvord")]
    public Guid PolVaAbroBarAvordId { get; set; }
    public clsPolVaAbroBarAvord PolVaAbroBarAvord { get; set; }
    [ForeignKey("RizMetreUsers")]
    public Guid RizMetreId { get; set;  }
    public clsRizMetreUsers RizMetreUsers { get; set; }
    public PolDetailTypeEnum PolDetailType { get; set;  }
}
