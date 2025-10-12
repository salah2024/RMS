using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre")]
public class clsAmalyateKhakiInfoForBarAvordEzafeBahaHamlRizMetre : clsBaseEntity
{
    [ForeignKey("AmalyateKhakiInfoForBarAvordEzafeBaha")]
    public Guid AmalyateKhakiInfoForBarAvordEzafeBahaId { get; set; }
    public clsAmalyateKhakiInfoForBarAvordEzafeBaha AmalyateKhakiInfoForBarAvordEzafeBaha { get; set; }

    [ForeignKey("RizMetreUser")]
    public Guid RizMetreId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }
}
