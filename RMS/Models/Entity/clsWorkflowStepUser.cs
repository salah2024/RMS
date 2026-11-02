using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblWorkflowStepUser")]

public class clsWorkflowStepUser:clsBaseEntity
{
    public Guid WorkflowStepId { get; set; }

    public clsWorkflowStep WorkflowStep { get; set; }

    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}
