using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;


/// <summary>
/// ریزمتره هایی که بنا به شرایط درج شده در جدول 
/// tblItemsRelatedToItemHaml
/// ریزه متره آیتم اصلی را حذف میکنن و ریزه متره ای برای 
/// شرط اعمال شده درج میکنن
/// در این جدول درج شده تا در صورتی که اضافه بهای مربوطه حذف شد
/// ریزه متره آیتم اصلی به خالت خود برگردد
/// </summary>
[Table("tblItemHasConditionBaseItemRemoved")]
public class clsItemHasConditionBaseItemRemoved:clsBaseEntity
{
    public Guid BarAvordId { get; set; }
    public Guid RizMetreId { get; set;  }
    public long ConditionContextId { get; set;  }

}
