using RMS.ViewModels.WorkflowStep;

namespace RMS.Interfaces.Workflow;

public interface IWorkflowStepQueryService
{
    Task<List<WorkflowStepListVm>> GetByWorkflowAsync(Guid workflowId);

    Task<List<WorkflowStepVm>> GetAsync(Guid id);
    Task<WorkflowStepVm?> GetForEditAsync(Guid id);
}
