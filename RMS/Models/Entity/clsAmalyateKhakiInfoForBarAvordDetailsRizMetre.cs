using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAmalyateKhakiInfoForBarAvordDetailsRizMetre")]
public class clsAmalyateKhakiInfoForBarAvordDetailsRizMetre:clsBaseEntity
{
    [ForeignKey("AmalyateKhakiInfoForBarAvordDetails")]
    public Guid AmalyateKhakiInfoForBarAvordDetailsId { get; set; }
    public clsAmalyateKhakiInfoForBarAvordDetails AmalyateKhakiInfoForBarAvordDetails { get; set; }

    [ForeignKey("RizMetreUser")]
    public Guid RizMetreUserId { get; set; }
    public clsRizMetreUsers RizMetreUser { get; set; }

}
