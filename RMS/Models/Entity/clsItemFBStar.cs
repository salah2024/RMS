using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblItemFBStar")]
public class clsItemFBStar:clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BaravordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public string Shomareh { get; set; }
    public decimal BahayeVahed { get; set; }
    [ForeignKey("Vahed")]
    public int VahedId { get; set; }
    public clsVahed Vahed { get; set; }
    public string Sharh { get; set; }
    public bool? blnKharidTajhizat { get; set; }

    [Column(TypeName = "decimal(5,3)")]
    [Range(typeof(decimal), "0", "1", ErrorMessage = "مقدار باید بین 0 و 1 باشد.")]
    public decimal? ItemZaribStar { get; set; }
}
