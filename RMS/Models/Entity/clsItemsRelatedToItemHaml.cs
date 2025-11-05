using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemsRelatedToItemHaml")]
public class clsItemsRelatedToItemHaml
{
    [Key]
    public long Id { get; set; }
    public string ItemFB { get; set; }
    public string ItemHamlFB { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Zarib1 { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Zarib2 { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Zarib3 { get; set; }
    public long Year { get; set; }
}
