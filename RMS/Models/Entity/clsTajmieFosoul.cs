using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblTajmieFosoul")]
public class clsTajmieFosoul
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string FaslCode { get; set; }
    public NoeFehrestBaha NoeFBId { get; set;  }
    public string Title { get; set; }
    public virtual ICollection<clsTajmieFosoulItems> TajmieFosoulItems { get; set; }

}
