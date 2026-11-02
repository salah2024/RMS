using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblBaravordUser")]
public class clsBaravordUser:clsBaseEntity
{
    public Guid UserId { get; set; }
    public long Num { get; set; }
    /// <summary>
    /// برآورد
    /// قرارداد   
    /// صورت وضعیت
    /// </summary>
    public NoeBarAvord Type { get; set; }
    public string NoeFBs { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribBalasari { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribManteghe { get; set; }

    public string? FromDate { get; set; }
    public string? ToDate { get; set; }

    ///پرنت
    ///در حالتی که صورت وضعیت باشد به یک قرار داد وابسته است 
    ///که آیدی آن در پرنت میاد
    [ForeignKey("BaravordUser")]
    public Guid? ParentId { get; set;  }
    public clsBaravordUser? BaravordUser { get; set; }

    //public bool IsCompleted { get; set; }

}
