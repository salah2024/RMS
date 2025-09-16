using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblQuesForAbnieFaniValues")]
public class clsQuesForAbnieFaniValues:clsBaseEntity
{
    [ForeignKey("QuestionForAbnieFani")]
    public long QuestionForAbnieFaniId { get; set; }  
    public clsQuesForAbnieFani QuestionForAbnieFani { get; set; }

    public decimal Value { get; set; }                

    public int ShomarehFBSelectedId { get; set; }

    [ForeignKey("PolVaAbroBarAvord")]
    public Guid PolVaAbroId { get; set; }
    public clsPolVaAbroBarAvord PolVaAbroBarAvord { get; set; }
}
