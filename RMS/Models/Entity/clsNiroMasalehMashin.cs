using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblNiroMasalehMashin")]
public class clsNiroMasalehMashin
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string CodeAmel { get; set; } 

    public string Title { get; set; }

    [ForeignKey("Vahed")]
    public int VahedId { get; set; }
    public clsVahed Vahed { get; set;  }

    public int Year { get; set;  }
    public NoeFehrestBaha NoeFB { get; set;  }
}
