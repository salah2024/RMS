using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

/// <summary>
/// بر گردش کار برآورد نظارت دارد
///یعنی در هر لحظه مشخص میکند برآورد کدام گردشش در چه جریان است
/// و در صورت به اتمام رسید 
/// isComplete
/// آن ترو میشود
/// و گردش بعدی در صورت وجود فعال میشود
/// 
/// گردش های
///برآورد اولیه
///پیشنهاد قیمت
///صورت وضعیت
/// </summary>
[Table("tblBarAvordProjectionWorkFlow")]
public class clsBarAvordProjectionWorkFlow:clsBaseEntity
{
    public Guid BarAvordUserId { get; set; }
    public ProccesType proccesType { get; set;  }
    public bool IsComplete { get; set; } = false;
}
