using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblItemFBStar")]
public class clsItemFBStar:clsBaseEntity
{
    public Guid BaravordId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public string Shomareh { get; set; }
    public decimal BahayeVahed { get; set; }
    [ForeignKey("Vahed")]
    public int VahedId { get; set; }
    public clsVahed Vahed { get; set; }
    public string Sharh { get; set; }
    public bool? blnKharidTajhizat { get; set; }
}
