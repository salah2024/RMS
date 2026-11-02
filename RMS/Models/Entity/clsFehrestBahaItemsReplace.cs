using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

/// <summary>
/// آیتم هایی که در فهرست بها هستند و میخواهیم
/// در یک فصل دیگر بیایند را در این جدول درج میکنیم
/// با عنوان شماره آیتم اصلی و شماره آیتم جدید
/// </summary>
[Table("tblFehrestBahaItemsReplace")]
public class clsFehrestBahaItemsReplace
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string FBShomarehMain { get; set;  }
    public string FBShomarehNew { get; set; }
    public int Year { get; set;  }
    public NoeFehrestBaha NoeFB { get; set; }
    /// <summary>
    /// این آیتم ها برای کدام فصل میباشد که قرار است تغییر یابند
    /// </summary>
    public long FaslId { get; set;  }
}
