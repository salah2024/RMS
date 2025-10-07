using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace RMS.Models.Entity;

[Table("tblPayKaniInfoForBarAvordMore")]
public class clsPayKaniInfoForBarAvordMore : clsBaseEntity
{
    [ForeignKey("PayKaniInfoForBarAvord")]
    public Guid PayKaniInfoForBarAvordId { get; set; }
    public clsPayKaniInfoForBarAvord PayKaniInfoForBarAvord { get; set; }
    public  string Name { get; set; }
    public  decimal Value { get; set; }
}
