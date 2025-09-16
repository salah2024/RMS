using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblQuesForAbnieFani")]
public class clsQuesForAbnieFani
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Question { get; set; }

    public bool HasGetValue { get; set; }

    public int DefaultValue { get; set; }

    public int Year { get; set; }

    public NoeFehrestBaha NoeFB { get; set; }

    public int Type { get; set; }

    public bool IsEzafeBaha { get; set; }

    public int ObjectType { get; set; }
}
