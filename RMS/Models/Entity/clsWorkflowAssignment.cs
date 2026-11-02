using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblWorkflowAssignment")]
public class clsWorkflowAssignment:clsBaseEntity
{
    public Guid WorkflowInstanceId { get; set; }

    public clsWorkflowInstance WorkflowInstance { get; set; }

    /// <summary>
    /// مرحله ای که این Assignment مربوط به آن است
    /// </summary>
    public Guid WorkflowStepId { get; set; }

    public clsWorkflowStep WorkflowStep { get; set; }
    /// <summary>
    /// گیرنده
    /// </summary>
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }
    /// <summary>
    /// ارسال کننده
    /// </summary>
    public string AssignedByUserId { get; set; }
    public DateTime AssignDate { get; set; }
    public bool IsCompleted { get; set; }

    /// <summary>
    /// نتیجه این Assignment
    /// </summary>
    public WorkflowAssignmentResult? Result { get; set; }

    public string? Comment { get; set; }

    public DateTime? CompletedDate { get; set; }

    // شماره دور اجرای مرحله
    public int RoundNo { get; set; }

}
