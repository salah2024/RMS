using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RMS.Models.Entity;

[Table("tblShomarehFBForQuesForAbnieFani")]
public class clsShomarehFBForQuesForAbnieFani
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("QuesForAbnieFani")]
    public long QuesForAbnieFaniId { get; set; }  
    public clsQuesForAbnieFani QuesForAbnieFani { get; set; }
    public string Shomareh { get; set; }
}
