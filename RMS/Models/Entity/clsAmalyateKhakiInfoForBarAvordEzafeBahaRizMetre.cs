using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre")]
public class clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre:clsBaseEntity
{
    [ForeignKey("AmalyateKhakiInfoForBarAvordEzafeBaha")]
    public Guid AmalyateKhakiInfoForBarAvordEzafeBahaId { get; set; }
    public clsAmalyateKhakiInfoForBarAvordEzafeBaha AmalyateKhakiInfoForBarAvordEzafeBaha { get; set; }

    [ForeignKey("RizMetreUser")]
    public Guid RizMetreUserId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }
}
