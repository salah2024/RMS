using RMS.ViewModels.Workflow;

namespace RMS.Interfaces.Workflow;

public interface IWorkflowCommandService
{
    Task<Guid> SaveAsync(WorkflowVm model);
    Task<bool> DeleteAsync(Guid id);
}
