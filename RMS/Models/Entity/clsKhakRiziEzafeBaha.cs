using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblKhakRiziEzafeBaha")]
public class clsKhakRiziEzafeBaha:clsBaseEntity
{
    [ForeignKey("KhakRiziBarAvord")]
    public Guid KhakRiziBarAvordId { get; set; }
    public clsKhakRiziBarAvord KhakRiziBarAvord { get; set; }

    [ForeignKey("EzafeBahaKhakRizi")]
    public long EzafeBahaKhakRiziId { get; set; }
    public clsEzafeBahaKhakRizi EzafeBahaKhakRizi { get; set; }

    public ICollection<clsKhakRiziEzafeBahaRizMetre> KhakRiziEzafeBahaRizMetres { get; set; }

}
