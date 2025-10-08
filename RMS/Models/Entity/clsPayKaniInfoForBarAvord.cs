using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblPayKaniInfoForBarAvord")]
public class clsPayKaniInfoForBarAvord : clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BaravordUserId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }
    public string FromKM { get; set; }
    public string ToKM { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public int KMNum { get; set; }
    /// <summary>
    /// 2-پی کنی
    /// 3-کانال کنی
    /// </summary>
    public int Type { get; set; }   
}
