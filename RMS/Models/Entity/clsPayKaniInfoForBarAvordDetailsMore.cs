using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblPayKaniInfoForBarAvordDetailsMore")]
public class clsPayKaniInfoForBarAvordDetailsMore:clsBaseEntity
{
    [ForeignKey("PayKaniInfoForBarAvordDetails")]
    public Guid PayKaniInfoForBarAvordDetailsId { get; set; }
    public clsPayKaniInfoForBarAvordDetails PayKaniInfoForBarAvordDetails { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}
