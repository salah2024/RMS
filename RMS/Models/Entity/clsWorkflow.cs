using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblWorkflow")]
public class clsWorkflow:clsBaseEntity
{
    /// <summary>
    /// نام گردش کار
    /// </summary>
    public string Name { get; set; }
    public ProccesType proccesType { get; set; }
    public bool IsActive { get; set; }
    /// <summary>
    /// ورک فلو پیش فرض سیستم
    /// که در ابتدا برای فرآیند انتخاب میشود
    /// </summary>
    public bool IsDefault { get; set; }
    /// <summary>
    /// ورک فلو سیستمی میباشد و قابل حذف و ویرایش نیست
    /// </summary>
    public bool IsSystem { get; set; }
    /// <summary>
    /// کاربر ایجاد کننده ورک فلو
    /// </summary>
    public Guid? OwnerUserId { get; set; }
    /// <summary>
    /// در صورتی که ترو باشد یعنی گردش خودش شروع میشود
    /// در صورت فالس بودن یعنی بایستی ابتدا کاربر ایجاد کننده دکمه ای مبنی بر ارسال را بزند
    /// </summary>
    public bool? Auto { get; set; }
    public ICollection<clsWorkflowStep> WorkflowSteps { get; set; }
}
