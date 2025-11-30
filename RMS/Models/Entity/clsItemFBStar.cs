using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemFBStar")]
public class clsItemFBStar:clsBaseEntity
{
    public Guid BaravordId { get; set; }
    public string Shomareh { get; set; }
    public decimal BahayeVahed { get; set; }
    [ForeignKey("Vahed")]
    public int VahedId { get; set; }
    public clsVahed Vahed { get; set; }
    public string Sharh { get; set; }
    public bool? blnKharidTajhizat { get; set; }
}
