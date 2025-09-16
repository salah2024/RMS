using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblBaravordUser")]
public class clsBaravordUser:clsBaseEntity
{
    public Guid UserId { get; set; }
    public long Num { get; set; }
    public NoeBarAvord Type { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}
