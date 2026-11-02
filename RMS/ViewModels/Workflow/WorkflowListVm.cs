namespace RMS.ViewModels.Workflow;

public class WorkflowListVm
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }

    public int StepCount { get; set; }
}
