using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblUserNotification")]
public class clsUserNotification:clsBaseEntity
{
    public Guid UserId { get; set; }
    [ForeignKey("baravordUser")]
    public Guid? BarAvordId { get; set; }
    public clsBaravordUser? baravordUser { get; set; }
    public NotifyType notifyType { get; set;  }
    public string Title { get; set;  }
    public string ShortDes { get; set;  }
    public string Description { get; set; }
    public string RelateLink { get; set;  }
    public bool IsRead { get; set; }

}
