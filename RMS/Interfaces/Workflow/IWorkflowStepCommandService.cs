using RMS.ViewModels.WorkflowStep;

namespace RMS.Interfaces.Workflow;

public interface IWorkflowStepCommandService
{
    Task<Guid> SaveAsync(WorkflowStepVm model);

    Task<bool> DeleteAsync(Guid id);

    Task MoveUpAsync(Guid id);

    Task MoveDownAsync(Guid id);
}
