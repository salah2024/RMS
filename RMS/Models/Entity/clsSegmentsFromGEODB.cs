using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblSegmentsFromGEODB")]
public class clsSegmentsFromGEODB : clsBaseEntity
{
    public int SegmentIdFromGEODB { get; set; }   // شناسه سگمنت از پایگاه‌داده جغرافیایی

    [ForeignKey("Operation")]
    public long OperationId { get; set; }          // شناسه عملیات مرتبط
    public clsOperation Operation { get; set; }

    [ForeignKey("Project")]
    public Guid ProjectId { get; set; }
    public clsProject Project { get; set; }
}
