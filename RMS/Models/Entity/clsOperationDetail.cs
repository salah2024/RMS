using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOperationDetail")]
public class clsOperationDetail
{
    [Key]
    public long Id { get; set; }
    public bool? CheckData { get; set; }
    public bool? HasEnteringValue { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    ///در صورتی که ترو باشد یعنی حداقل نوشته شده بایستی در نظر گرفته شود
    ///برای پرداخت
    ///مثلا اگر حداقل 150 باشد یعنی در صورتی که اعداد کوچکتر از 150 نیز وارد شود 150 را حتما بایستی ثبت کنیم
    ///ولی در صورتی که فالس باشد یعنی هر عددی وارد شد از حداقل کسر میشود و باقیماده ثبت میگردد
    ///
    public bool UseMinForInsert { get; set; }
    public string? Description { get; set; }

    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }

}
