using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

/// <summary>
/// وابستگی آیتم هایی که بهای واحد آنها وابسته به آیتمی دیگر میباشد را ثبت مینماید
/// مثلا آیتم 170201 بهای واحدش 10 درصد بهای واحد 170101 میباشد
/// </summary>
[Table("tblBahayeVahedItemsRelated")]
public class clsBahayeVahedItemsRelated
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    /// <summary>
    /// آیتم وابسته مثلا 170201
    /// </summary>
    public string ItemShomarehRelated { get; set; }
    /// <summary>
    /// مقدار ضریب واحد مثلا 0.1
    /// یعنی 10 درصد
    /// </summary>
    /// 
    [Column(TypeName = "decimal(18,4)")]
    public decimal Value { get; set;  }
    /// <summary>
    /// آیتم پایه مثلا 170101
    /// </summary>
    public string ItemShomareh { get; set; }

    public NoeFehrestBaha NoeFB { get; set; }
    public int Year { get;set;  }
}
