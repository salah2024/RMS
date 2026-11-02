using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblItemsFields")]
public class clsItemsFields
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string ItemShomareh { get; set; }

    public ItemFieldType FieldType { get; set; }

    public string? Vahed { get; set; }      // مثلاً: "متر", "عدد", ...

    public bool IsEnteringValue { get; set; }
    public bool EssentialValue { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? DefaultValue { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? MaxValue { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? MinValue { get; set; }

    public NoeFehrestBaha NoeFB { get; set; }
}
