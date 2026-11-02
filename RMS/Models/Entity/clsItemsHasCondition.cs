using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblItemsHasCondition")]
public class clsItemsHasCondition
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string ItemFBShomareh { get; set; }
    public int Year { get; set; }
    public NoeFehrestBaha? NoeFB { get; set; }
}
