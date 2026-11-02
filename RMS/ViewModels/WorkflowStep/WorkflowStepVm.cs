namespace RMS.ViewModels.WorkflowStep;

public class WorkflowStepVm
{
    public Guid Id { get; set; }

    public int Order { get; set; }

    public Guid WorkflowId { get; set; }


    public string Title { get; set; }


    public bool AllowReturn { get; set; }


    public bool IsFinal { get; set; }


    public bool NeedSelectReceiver { get; set; }

    /// <summary>
    /// نقش های مقصد
    /// </summary>
    public List<string> RoleIds { get; set; }
        = new();



    /// <summary>
    /// کاربران خاص مقصد
    /// </summary>
    public List<string> UserIds { get; set; }
        = new();


    /// <summary>
    /// نقش های مقصد
    /// </summary>
    public List<string> RoleNames { get; set; }
        = new();



    /// <summary>
    /// کاربران خاص مقصد
    /// </summary>
    public List<string> UserNames { get; set; }
        = new();
}