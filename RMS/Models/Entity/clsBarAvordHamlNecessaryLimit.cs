using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;
/// <summary>
/// در صورتی که برای براوردی در اینجا رکوردی درج گردد
/// بدین معناست که این برآورد حمل هایش نیازی به محدودیت سقف کیلومتراژ
/// حمل ندارند، همانند استان های شمالی کشور
/// </summary>

[Table("tblBarAvordHamlNecessaryLimit")]
public class clsBarAvordHamlNecessaryLimit:clsBaseEntity
{
    [ForeignKey("BarAvord")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BarAvord { get; set; }
    public NoeFehrestBaha NoeFBId { get;set; }
}
