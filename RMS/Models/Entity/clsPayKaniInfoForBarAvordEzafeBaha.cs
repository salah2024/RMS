using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// برای عملیات خاکی درج شده چه اضافه بهایی درج شده است
/// </summary>
[Table("tblPayKaniInfoForBarAvordEzafeBaha")]
public class clsPayKaniInfoForBarAvordEzafeBaha : clsBaseEntity
{
    [ForeignKey("PayKaniInfoForBarAvord")]
    public Guid PayKaniInfoForBarAvordId { get; set; }
    public clsPayKaniInfoForBarAvord PayKaniInfoForBarAvord { get; set; }

    [ForeignKey("NoeKhakBardariEzafeBaha")]
    public long NoeKhakBardariEzafeBahaId { get; set; }
    public clsNoeKhakBardariEzafeBaha NoeKhakBardariEzafeBaha { get; set; }

    public ICollection<clsPayKaniInfoForBarAvordEzafeBahaRizMetre> lstPayKaniInfoForEBRizMetre { get; set; }
}
