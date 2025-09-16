using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblFehrestBaha")]
public class clsFehrestBaha
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string? BahayeVahed { get; set; }

    public string Vahed { get; set; }

    public string Sharh { get; set; }

    public string Shomareh { get; set; }

    public long Sal { get; set; }

    public NoeFehrestBaha? NoeFB { get; set; }
}
