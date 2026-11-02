using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// مشاور برآورد را ثبت مینماید
/// </summary>
[Table("tblBarAvordMoshaver")]
public class clsBarAvordMoshaver:clsBaseEntity
{
    [ForeignKey("Company")]
    public Guid MoshaverId { get; set; }
    public clsCompany Company { get; set;  }

    [ForeignKey("BarAvordUser")]
    public Guid BarAvordUserId { get; set; }
    public clsBaravordUser BarAvordUser { get; set; }

}






