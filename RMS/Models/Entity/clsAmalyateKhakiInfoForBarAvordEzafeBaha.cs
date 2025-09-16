using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// برای عملیات خاکی درج شده چه اضافه بهایی درج شده است
/// </summary>
[Table("tblAmalyateKhakiInfoForBarAvordEzafeBaha")]
public class clsAmalyateKhakiInfoForBarAvordEzafeBaha:clsBaseEntity
{
    [ForeignKey("AmalyateKhakiInfoForBarAvord")]
    public Guid AmalyateKhakiInfoForBarAvordId { get; set; }
    public clsAmalyateKhakiInfoForBarAvord AmalyateKhakiInfoForBarAvord { get; set; }

    [ForeignKey("NoeKhakBardariEzafeBaha")]
    public long NoeKhakBardariEzafeBahaId { get; set; }
    public   clsNoeKhakBardariEzafeBaha NoeKhakBardariEzafeBaha { get; set; }

    public ICollection<clsAmalyateKhakiInfoForBarAvordEzafeBahaRizMetre> lstAKhInfoForEBRizMetre { get; set; }
}
