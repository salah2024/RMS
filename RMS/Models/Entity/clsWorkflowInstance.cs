using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblWorkflowInstance")]
public class clsWorkflowInstance:clsBaseEntity
{
    /// <summary>
    /// گردش مربوطه
    /// </summary>
    public Guid WorkflowId { get; set; }

    public clsWorkflow Workflow { get; set; }


    /// <summary>
    /// شناسه رکورد اصلی
    /// </summary>
    public Guid EntityId { get; set; }

    public string EntityType { get; set; }

    /// <summary>
    /// مرحله جاری
    /// </summary>
    public Guid CurrentStepId { get; set; }
    public clsWorkflowStep CurrentStep { get; set; }


    public WorkflowInstanceStatus Status { get; set; }
}
