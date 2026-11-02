using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// پیمانکار برآورد را ثبت مینماید
/// </summary>
[Table("tblBarAvordPaymanKar")]
public class clsBarAvordPaymanKar : clsBaseEntity
{
    [ForeignKey("Company")]
    public Guid PayManKarId { get; set; }
    public clsCompany Company { get; set; }

    [ForeignKey("BarAvordUser")]
    public Guid BarAvordUserId { get; set; }
    public clsBaravordUser BarAvordUser { get; set; }

}
