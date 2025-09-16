using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblAnalizBaha")]
public class clsAnalizBaha
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string FBShomareh { get; set; }
    public NoeFehrestBaha  NoeFB { get; set; }
    public int Year { get; set; }
    public long NiroMasalehMashinId { get; set; }
    public long Vahed { get; set; }
    public decimal MeghdarMeghias { get; set; }
    public decimal BahayeVahed { get; set; }
    public decimal bahayeKol { get;set; }
}
