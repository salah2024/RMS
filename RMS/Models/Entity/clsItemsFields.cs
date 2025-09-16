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

    public int FieldType { get; set; }  

    public string Vahed { get; set; }      // مثلاً: "متر", "عدد", ...

    public bool IsEnteringValue { get; set; }

    public string? DefaultValue { get; set; }

    public NoeFehrestBaha NoeFB { get; set; }
}
