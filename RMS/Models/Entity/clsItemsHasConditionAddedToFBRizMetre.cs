using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// ریزه متره های مربوط به یک اضافه بهای یک آیتم که تیک خورده است در اینجا ثبت و لینک میشود
/// </summary>

[Table("tblItemsHasConditionAddedToFBRizMetre")]
public class clsItemsHasConditionAddedToFBRizMetre:clsBaseEntity
{
    [ForeignKey("ItemsHasConditionAddedToFB")]
    public Guid ItemsHasConditionAddedToFBId { get; set;  }
    public clsItemsHasConditionAddedToFB itemsHasConditionAddedToFB { get; set; }

    [ForeignKey("RizMetreUser")]
    public Guid RizMetreId { get; set;  }
    public clsRizMetreUsers RizMetreUser { get; set; }
    public Guid BaseRizMetreId { get; set; }

}
