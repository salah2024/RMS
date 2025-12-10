using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

/// <summary>
/// ضریب بالاسری
/// بنا به نوع طرحها ضریب مربوطه انتخاب میگردد
/// 1=طرح عمرانی
/// 2=طرح غیر عمرانی
/// 3=مناقصه عمومی
/// 4=عدم الزام له برگزاری مناقصه
/// 5=ترک تشریفات
/// 
/// که بصورت 1و3 ضریب برابر با 1.4
/// انتخاب میشود
/// 
/// </summary>
[Table("tblZaribBalaSari")]
public class clsZaribBalaSari
{
    [Key]
    public long Id { get; set; }
    public int? Noe1 { get; set; }
    public int? Noe2 { get; set; }
    public int? Noe3 { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Zarib { get;set; }
    public int Year { get;set; }
    public NoeFehrestBaha NoeFBId { get; set; }
}
