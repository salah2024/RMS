using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblAmalyateKhakiInfoForBarAvord")]
public class clsAmalyateKhakiInfoForBarAvord : clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BaravordUserId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    ///<summary>
    //1- خاکبرداری
    //2- خاکریزی
    //3- پی کنی
    ///</summary>
    public NoeAmalyatKhaki Type { get; set; }
    public string FromKM { get; set; }
    public string ToKM { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public int KMNum { get; set; }
}
