using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblKhakRiziEzafeBahasRizMetre")]
public class clsKhakRiziEzafeBahaRizMetre:clsBaseEntity
{
    [ForeignKey("KhakRiziEzafeBaha")]
    public Guid KhakRiziEzafeBahaId { get; set; }
    public clsKhakRiziEzafeBaha KhakRiziEzafeBaha { get; set; }
    [ForeignKey("RizMetreUser")]
    public Guid RizMetreUserId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }

}
