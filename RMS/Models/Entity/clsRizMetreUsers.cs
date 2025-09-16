using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblRizMetreUsers")]
public class clsRizMetreUsers : clsBaseEntity
{
    public long Shomareh { get; set; }            // شماره آیتم
    /// <summary>
    /// شماره ای که برای اضافه/کسر بها ایجاد میشود
    /// </summary>
    public string? ShomarehNew { get; set; }      

    public string Sharh { get; set; }               // شرح آیتم

    [Column(TypeName = "decimal(18,4)")]
    public decimal? Tedad { get; set; }                  // تعداد
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Tool { get; set; }              // طول (متر)
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Arz { get; set; }                // عرض (متر)
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Ertefa { get; set; }            // ارتفاع (متر)
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Vazn { get; set; }             // وزن (کیلوگرم یا تن)
    [Column(TypeName = "decimal(18,4)")]
    public decimal? MeghdarJoz { get; set; }          
    public string? Des { get; set; }
    [ForeignKey("FB")]
    public Guid FBId { get; set; }
    public clsFB FB { get; set; }
    [ForeignKey("OperationsOfHaml")]
    public long? OperationsOfHamlId { get; set; }    
    public clsOperationsOfHaml OperationsOfHaml { get; set; }
    public string? ForItem { get; set; }           
    public string Type { get; set; }            

    public string? UseItem { get; set; }

    /// <summary>
    /// در صورتی که این فیلد دارای مقدار باشد
    /// یعنی دارای وابستگی با یک آیتم دیگر است
    /// آیتم یا آیتم های مربوطه در فیلد 
    /// ConditionContextRel
    /// میباشد
    ///
    /// در نتیجه وقتی یکی از آنها حذف گردد بایستی دنبال فیلدهایی گشت که 
    /// ConditionContextId
    /// آنها در 
    /// ConditionContextRel
    /// قرار دارد و حذف نمود
    /// </summary>
    public long? ConditionContextId { get; set; }
    public string? ConditionContextRel { get; set; }

    /// <summary>
    /// جهت بررسی اینکه ریز متره از کدام سطح فراخوانی شده است
    /// سطح دوم سطوحیست که به آیتم های مربوط میشود که خودشان اضافه بهای سطح اول هستند
    /// </summary>
    public int? LevelNumber { get; set; }
}
