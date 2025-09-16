using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAppOperationInfoMain")]
public class clsAppOperationInfoMain:clsBaseEntity
{
    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }

    public Guid ProjectId { get; set; }

    public decimal XState { get; set; }

    public decimal YState { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public DateTime? DateSending { get; set; }

    public TimeSpan? TimeSending { get; set; }
}
