using System.ComponentModel.DataAnnotations.Schema;
using RMS.Controllers.KhakRizi.EnumKhakRizi;

namespace RMS.Models.Entity;

[Table("tblKhakRiziBarAvord")]
public class clsKhakRiziBarAvord:clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BarAvordId { get; set; }
    public int KMNum { get; set; }
    public string FromKM { get; set; }
    public string ToKM { get; set; }

    public clsBaravordUser BaravordUser { get; set; }
    public EnumRoadType NoeRah { get; set; }
    public EnumNoeDaneBandi NoeDaneBandi { get; set; }
    /// <summary>
    /// حجم خاکریزی بصورت 
    /// 1_25.6,2_45
    ///ثبت میگردد که اعداد 1 و 2 مربوط به نوع حجم خاکریزی میباشند که از 
    ///enum EnumHajmKhakRizi
    ///میاد و عدد بعد از _ میزان حجم خاکریزی میباشد
    ///حال میتواند به تعداد نامحدود حجم خاکریزی داشت
    /// </summary>

    public string? NoeHajmKhakRizi_Value { get; set; }
}
