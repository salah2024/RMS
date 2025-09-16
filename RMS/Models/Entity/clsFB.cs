using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblFB")]
public class clsFB:clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BaravordUser { get; set; }

    public string Shomareh { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal BahayeVahedZarib { get; set; }
    /// <summary>
    /// برای آیتم هایی که اضافه یا کسر بها به خود آیتم اصلی میخورند، توضیحشان متفاوت خواهد بود نسبت به آیتم اصلی 
    /// در نتیجه در توضیح آیتم ایجاد شده توضیحی که در اینجا درج شده نمایش داده میشود
    /// مانند 
    /// 150603M ----- اعمال ضریب کاهشی 0.85 بابت عدم انطباق قیر مورد استفاده برای تولید آسفالت با قیر مشخص شده در مشخصات فنی و خصوصی 
    /// 150603A
    /// </summary>
    public string BahayeVahedSharh { get; set; } = "";
    /// <summary>
    /// آیتم هایی در فهرست بها که بهای واحد نداشته باشند در اینجا بهای واحدشان درج میگردد و در هر برآوردی ممکن است متفاوت باشد این مقدار
    /// همچنین ممکن است مقدار بهای واحد یک آیتم تغییر یابد که در این صورت آیتم مربوطه بعنوان آیتم ستاره دار در نظر گرفته میشود
    /// </summary>
    public long BahayeVahedNew { get; set; }
}
