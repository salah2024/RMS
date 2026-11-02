using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// کارفرما برآورد را ثبت مینماید
/// </summary>
[Table("tblBarAvordKarfarma")]
public class clsBarAvordKarfarma : clsBaseEntity
{
    [ForeignKey("Company")]
    public Guid KarFarmaId { get; set; }
    public clsCompany Company { get; set; }

    [ForeignKey("BarAvordUser")]
    public Guid BarAvordUserId { get; set; }
    public clsBaravordUser BarAvordUser { get; set; }
}
