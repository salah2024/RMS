using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblProjectCalendar")]
public class clsProjectCalendar:clsBaseEntity
{
    [ForeignKey("GeneralProjectTiming")]
    public Guid GeneralProjectTimingId { get; set; }
    public clsGeneralProjectTiming GeneralProjectTiming { get; set; }

    public DateTime Date { get; set; }

    public int State { get; set; }

    public bool ActiveDeActive { get; set; }
}
