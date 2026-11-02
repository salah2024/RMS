using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblFosoul")]
public class clsFosoul
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string LatinName  { get; set; }
    public int order { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    /// <summary>
    /// در صورتی که ترو باشد یعنی آیتم های آن بدون نیاز به تغییر نمایش داده میشود
    /// در صورتی که فالس باشد یعنی آیتم های فصل بایستی تغییر یابند و در جدول 
    /// tblFehrestBahaItemsReplace
    /// آیتم های لازمه درج میگردند 
    /// </summary>
    public bool? FaslAsli { get; set;  }
}
