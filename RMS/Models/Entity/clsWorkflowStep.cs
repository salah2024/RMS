using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;
/// <summary>
/// تعریف مراحل گردش
/// </summary>
[Table("tblWorkflowStep")]

public class clsWorkflowStep:clsBaseEntity
{
    public Guid WorkflowId { get; set; }

    public clsWorkflow Workflow { get; set; }

    /// <summary>
    /// عنوان مرحله
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// ترتیب مرحله
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// آیا امکان عودت وجود دارد؟
    /// </summary>
    public bool AllowReturn { get; set; }

    /// <summary>
    /// آیا این مرحله پایان گردش است؟
    /// </summary>
    public bool IsFinal { get; set; }

    /// <summary>
    /// هنگام ارسال، کاربر باید گیرنده را انتخاب کند؟
    /// </summary>
    public bool NeedSelectReceiver { get; set; }

    /// <summary>
    /// آیا این مرحله فرم عملیاتی دارد؟
    /// </summary>
    public bool RequireForm { get; set; }

    /// <summary>
    /// عملیات سیستم
    /// </summary>
    public int? WorkflowActionId { get; set; }

    public clsWorkflowAction? WorkflowAction { get; set; }

    public WorkflowReceiverMode ReceiverMode { get; set; }

    public int? MinimumRequiredResponses { get; set; }

    public Guid? NextStepId { get; set; }

    public Guid? PreviousStepId { get; set; }

    public ICollection<clsWorkflowStepRole> Roles { get; set; } = new List<clsWorkflowStepRole>();

    public ICollection<clsWorkflowStepUser> Users { get; set; } = new List<clsWorkflowStepUser>();
}
