using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// چه نقش‌هایی مجاز هستند این مرحله را دریافت کنند.
/// </summary>
[Table("tblWorkflowStepRole")]

public class clsWorkflowStepRole:clsBaseEntity
{
    public Guid WorkflowStepId { get; set; }

    public clsWorkflowStep WorkflowStep { get; set; }

    public string RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public IdentityRole Role { get; set; }
}
