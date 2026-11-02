using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.WorkFlow.Dto;

public class WorkflowHistoryDto
{
    public Guid WorkflowInstanceId { get; set; }
    public Guid WorkflowId { get; set; }
    public string EntityType { get; set; }
    public Guid EntityId { get; set; }
    public Guid CurrentStepId { get; set; }
    public WorkflowInstanceStatus Status { get; set; }

    public string ProjectName { get; set; }

    public Guid WorkflowStepId { get; set; }
    public string StepTitle { get; set; }

    public string FullName { get; set; }
    public string CompanyName { get; set; }

    public WorkflowAction? Action { get; set; }
    public DateTime? ActionDate { get; set; }
    public string? ActionDateShamsi { get; set; }
    public string? Comment { get; set; }

    public string? UserId { get; set; }

    public int RoundNo { get; set; }

    // برای نمایش Timeline
    public bool IsCompleted { get; set; }
}