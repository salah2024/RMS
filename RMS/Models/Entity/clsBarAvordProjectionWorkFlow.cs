using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

/// <summary>
/// بر گردش کار برآورد نظارت دارد
///یعنی در هر لحظه مشخص میکند برآورد جاری گردشش در چه اقدامی است
///برآورد اولیه
///پیشنهاد قیمت
/// </summary>
[Table("tblBarAvordProjectionWorkFlow")]
public class clsBarAvordProjectionWorkFlow:clsBaseEntity
{
    public Guid BarAvordUserId { get; set; }
    public ProccesType proccesType { get; set;  }
}
