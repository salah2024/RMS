using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblKhakRiziEzafeBahaBarAvord")]
public class clsKhakRiziEzafeBahaBarAvord:clsBaseEntity
{
    [ForeignKey("BarAvordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BarAvordUser { get; set; }

    [ForeignKey("EzafeBahaKhakRizi")]
    public long EzafeBahaKhakRiziId { get; set; }
    public clsEzafeBahaKhakRizi EzafeBahaKhakRizi { get; set; }

    public ICollection<clsKhakRiziEzafeBahaBarAvordRizMetre> KhakRiziEzafeBahaBarAvordRizMetres { get; set; }

}
