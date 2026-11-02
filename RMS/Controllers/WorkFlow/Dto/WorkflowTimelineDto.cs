using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.WorkFlow.Dto;

public class WorkflowTimelineDto
{
    public Guid WorkflowInstanceId { get; set; }

    public Guid WorkflowId { get; set; }

    public string EntityType { get; set; }

    public Guid EntityId { get; set; }

    public Guid CurrentStepId { get; set; }

    public WorkflowInstanceStatus Status { get; set; }

    public string ProjectName { get; set; }

    public List<WorkflowStepTimelineDto> Steps { get; set; }
        = new();
}

public class WorkflowStepTimelineDto
{
    public Guid WorkflowStepId { get; set; }

    public string StepTitle { get; set; }

    public int Order { get; set; }

    public int? RoundNo { get; set; }

    public bool IsCurrent { get; set; }

    public bool IsFinal { get; set; }

    public string StatusTitle { get; set; }

    public List<WorkflowAssignmentTimelineDto> Assignments { get; set; }
        = new();

    public List<WorkflowHistoryTimelineDto> Histories { get; set; }
        = new();
}


public class WorkflowAssignmentTimelineDto
{
    public Guid AssignmentId { get; set; }

    public string UserId { get; set; }

    public string FullName { get; set; }

    public string CompanyName { get; set; }

    public bool IsCompleted { get; set; }

    public WorkflowAssignmentResult? Result { get; set; }

    public string ResultTitle { get; set; }

    public string Comment { get; set; }

    public DateTime AssignDate { get; set; }

    public DateTime? CompletedDate { get; set; }
}


public class WorkflowHistoryTimelineDto
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public string FullName { get; set; }

    public string CompanyName { get; set; }

    public WorkflowAction Action { get; set; }

    public string ActionTitle { get; set; }

    public string Comment { get; set; }

    public DateTime ActionDate { get; set; }

    public string ActionDateShamsi { get; set; }
}
