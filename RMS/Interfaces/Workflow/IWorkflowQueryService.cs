using RMS.ViewModels.Workflow;

namespace RMS.Interfaces.Workflow;

public interface IWorkflowQueryService
{
    Task<List<WorkflowListVm>> GetAllAsync();

    Task<WorkflowVm?> GetAsync(Guid id);
}
