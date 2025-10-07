using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblPayKaniInfoForBarAvordEzafeBahaRizMetre")]
public class clsPayKaniInfoForBarAvordEzafeBahaRizMetre : clsBaseEntity
{
    [ForeignKey("PayKaniInfoForBarAvordEzafeBaha")]
    public Guid PayKaniInfoForBarAvordEzafeBahaId { get; set; }
    public clsPayKaniInfoForBarAvordEzafeBaha PayKaniInfoForBarAvordEzafeBaha { get; set; }

    [ForeignKey("RizMetreUser")]
    public Guid RizMetreUserId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }
}
