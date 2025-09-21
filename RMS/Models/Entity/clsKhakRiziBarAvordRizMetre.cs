using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblKhakRiziBarAvordRizMetre")]
public class clsKhakRiziBarAvordRizMetre:clsBaseEntity
{
    [ForeignKey("KhakRiziBarAvord")]
    public Guid KhakRiziBarAvordId { get; set; }
    public clsKhakRiziBarAvord KhakRiziBarAvord { get; set; }
    [ForeignKey("RizMetreUser")]
    public Guid RizMetreUserId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }
}
