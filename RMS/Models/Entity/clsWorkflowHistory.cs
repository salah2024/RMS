using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblWorkflowHistory")]
public class clsWorkflowHistory : clsBaseEntity
{

    /// <summary>
    /// گردش اجرایی پروژه
    /// </summary>
    public Guid WorkflowInstanceId { get; set; }

    public clsWorkflowInstance WorkflowInstance { get; set; }



    /// <summary>
    /// مرحله ای که عملیات روی آن انجام شده
    /// </summary>
    public Guid WorkflowStepId { get; set; }



    /// <summary>
    /// کاربری که اقدام کرده
    /// </summary>
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }



    /// <summary>
    /// نوع عملیات
    /// </summary>
    public WorkflowAction Action { get; set; }



    /// <summary>
    /// توضیحات کاربر
    /// </summary>
    public string? Comment { get; set; }



    /// <summary>
    /// تاریخ اقدام
    /// </summary>
    public DateTime ActionDate { get; set; }

    public int RoundNo { get; set; }

}