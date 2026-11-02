namespace RMS.ViewModels.WorkflowStep;

public class WorkflowStepListVm
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public int Order { get; set; }

    public bool AllowReturn { get; set; }

    public bool IsFinal { get; set; }

    public bool NeedSelectReceiver { get; set; }

    public string Roles { get; set; }

    public string Users { get; set; }
}
