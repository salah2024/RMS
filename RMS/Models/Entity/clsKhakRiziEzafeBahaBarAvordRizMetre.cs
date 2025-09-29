using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

public class clsKhakRiziEzafeBahaBarAvordRizMetre:clsBaseEntity
{
    [ForeignKey("KhakRiziEzafeBahaBarAvord")]
    public Guid KhakRiziEzafeBahaBarAvordId { get; set; }
    public clsKhakRiziEzafeBahaBarAvord KhakRiziEzafeBahaBarAvord { get; set; }
    [ForeignKey("RizMetreUser")]
    public Guid RizMetreUserId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }

}
