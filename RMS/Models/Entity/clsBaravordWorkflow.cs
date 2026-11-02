using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBaravordWorkflow")]
public class clsBaravordWorkflow : clsBaseEntity
{
    public Guid BaravordUserId { get; set; }

    public clsBaravordUser BaravordUser { get; set; }

    public Guid WorkflowId { get; set; }

    public clsWorkflow Workflow { get; set; }

    public DateTime StartDate { get; set; }

    public Guid StartUserId { get; set; }

}
