using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblGeneralProjectTiming")]
public class clsGeneralProjectTiming:clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BaravordUserId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    public DateTime StartDate { get; set; }

    public int CountDaies { get; set; }

    public int CourseCountDaies { get; set; }

    public int HolyCourseCountDaies { get; set; }
}
