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
    ////
    ///در صورتی که یک آیتم از چند آیتم فهرست بها استفاده نماید
    ///در این صورت چند min
    ///و چند max دارد که به صورت
    ///130101_1,130301_30
    ///برای operation=200525
    ///نوشته میشود
    ///بدین معنا که در صورتی که داده ها از آیتم 130101 آمد حداقل برابر با 1 و در 
    ///صورتی که از 130301 آمد حداقل برابر با 30 میباشد
    ///
    public string MaxMinValue { get; set; }
    ///در صورتی که ترو باشد یعنی حداقل نوشته شده بایستی در نظر گرفته شود
    ///برای پرداخت
    ///مثلا اگر حداقل 150 باشد یعنی در صورتی که اعداد کوچکتر از 150 نیز وارد شود 150 را حتما بایستی ثبت کنیم
    ///ولی در صورتی که فالس باشد یعنی هر عددی وارد شد از حداقل کسر میشود و باقیماده ثبت میگردد
    ///
    public bool UseMinForInsert { get; set; }

    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }

}
