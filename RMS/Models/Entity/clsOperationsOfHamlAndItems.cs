using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblOperationsOfHamlAndItems")]
public class clsOperationsOfHamlAndItems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("OperationsOfHaml")]
    public long OperationsOfHamlId { get; set; }
    public clsOperationsOfHaml OperationsOfHaml { get; set; }

    public string ItemsFB { get; set; }       

    public int Year { get; set; }        

    public NoeFehrestBaha NoeFB { get; set; }    

    public string KiloMetrazh { get; set; }
}
