using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblPayKaniInfoForBarAvordDetailsRizMetre")]
public class clsPayKaniInfoForBarAvordDetailsRizMetre : clsBaseEntity
{
    [ForeignKey("PayKaniInfoForBarAvordDetails")]
    public Guid PayKaniInfoForBarAvordDetailsId { get; set; }
    public clsPayKaniInfoForBarAvordDetails PayKaniInfoForBarAvordDetails { get; set; }

    [ForeignKey("RizMetreUser")]
    public Guid RizMetreUserId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }

}
