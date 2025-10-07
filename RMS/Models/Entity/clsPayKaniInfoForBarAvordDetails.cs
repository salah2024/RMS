using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblPayKaniInfoForBarAvordDetails")]
public class clsPayKaniInfoForBarAvordDetails : clsBaseEntity
{
    [ForeignKey("PayKaniInfoForBarAvord")]
    public Guid PayKaniInfoForBarAvordId;
    public clsPayKaniInfoForBarAvord PayKaniInfoForBarAvord { get; set; }

    [ForeignKey("NoeKhakBardari")]
    public long NoeKhakBardariId { get; set; }
    public clsNoeKhakBardari NoeKhakBardari { get; set; }
    public decimal Value { get; set; }
    public string Name { get; set; }
    public bool boolValue { get; set; }
    public ICollection<clsPayKaniInfoForBarAvordDetailsMore> lstPayKaniInfoForBarAvordDetailsMore { get; set; }
}