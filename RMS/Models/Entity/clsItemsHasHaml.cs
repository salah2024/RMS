using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblItemsHasHaml")]
public class clsItemsHasHaml
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string ItemsFB { get; set; }    

    public int Year { get; set; }         

    public NoeFehrestBaha NoeFB { get; set; }
}
