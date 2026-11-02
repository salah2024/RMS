using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBarAvordUserTajmie")]
public class clsBarAvordUserTajmie:clsBaseEntity
{
    [ForeignKey("BaravordUser")]
    public Guid BarAvordUserId { get; set;  }
    public clsBaravordUser BaravordUser { get; set;  }

    [ForeignKey("TajmieFosoulItem")]
    public long TajmieFosoulItemId { get; set;  }
    public clsTajmieFosoulItems TajmieFosoulItem { get; set;  }

    public decimal BahayeVahed { get; set; }
    public decimal Meghdar { get; set; }
    public decimal BahayeKol { get; set; }

}
