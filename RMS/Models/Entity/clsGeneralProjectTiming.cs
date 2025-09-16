using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblGeneralProjectTiming")]
public class clsGeneralProjectTiming:clsBaseEntity
{
    [ForeignKey("Project")]
    public Guid ProjectId { get; set; }
    public clsProject Project { get; set; }
    public DateTime StartDate { get; set; }

    public int CountDaies { get; set; }

    public int CourseCountDaies { get; set; }

    public int HolyCourseCountDaies { get; set; }
}
