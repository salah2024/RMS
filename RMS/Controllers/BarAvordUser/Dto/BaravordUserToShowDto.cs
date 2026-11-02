using RMS.Controllers.Operation.Dto;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto;

public class BaravordUserToShowDto
{
    public Guid BUId { get; set; }
    public long BUNum { get; set; }
    public string BUName { get; set; }
    public string? FromDate { get; set; }
    public string? ToDate { get; set; }
    public DateTime? BUInsertDate { get; set; }
    public int BUYear { get; set; }
    public string BUNoeFBs { get; set; }
    public string NoeFBName { get; set; }
    public string BUInsertDateSolar { get; set; }
    public NoeBarAvord BUType { get; set; }
    public bool IsOwner { get; set; }

    public bool hasWorkflow { get; set; }

    public bool CanOpen { get; set; }

    public bool CanApprove { get; set; }

    public bool CanReturn { get; set; }

    public string? WorkflowStatusTitle { get; set; }
    public bool IsCompleted { get; set; }


}
public class SVToShowDto
{
    public Guid BUId { get; set; }
    public long BUNum { get; set; }
    public string BUName { get; set; }
    public string? FromDate { get; set; }
    public string? ToDate { get; set; }
    public DateTime? BUInsertDate { get; set; }
    public int BUYear { get; set; }
    public string BUNoeFBs { get; set; }
    public string NoeFBName { get; set; }
    public string BUInsertDateSolar { get; set; }
    public NoeBarAvord BUType { get; set; }
    public ResultCalculateBarAvordDto CalculateBarAvord { get; set;  }

}
