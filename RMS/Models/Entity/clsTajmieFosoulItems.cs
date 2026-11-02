using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblTajmieFosoulItems")]
public class clsTajmieFosoulItems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string ItemCode { get; set;  }

    public string Sharh { get; set; }
    public string Vahed { get; set; }


    [ForeignKey("TajmieFosoul")]
    public long TajmieFosoulId { get; set; }
    public clsTajmieFosoul TajmieFosoul { get; set; }

    /// <summary>
    /// نحوه محاسبه مقدار
    /// آیتم های تجمیعی
    /// Manual = 1,
    /// FromItem = 2,
    /// Calculate = 3,
    /// </summary>
    /// 
    public CalculateValueType calculateValueType { get; set; }
    /// <summary>
    /// آیتم اصلی
    ///در صورتی که 
    ///calculateValueType=2
    ///بایستی یکی از آیتم های فهرست بها انتخاب گردد
    ///که بهای واحد آن آیتم مبای بهای واحد آیتم تجمیعی باشد
    /// </summary>
    public string? MainItem { get; set;  }
}
