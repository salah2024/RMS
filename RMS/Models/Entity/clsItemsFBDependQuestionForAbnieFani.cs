using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemsFBDependQuestionForAbnieFani")]
public class clsItemsFBDependQuestionForAbnieFani
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("QuesForAbnieFani")]
    public long QuesForAbnieFaniId { get; set; }
    public clsQuesForAbnieFani QuesForAbnieFani { get; set; }

    public string ItemShomareh { get; set; }

    public int DefaultValue { get; set; }

    public string Vahed { get; set; }
}
