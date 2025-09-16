using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOperationsOfHaml_ItemsHasHaml")]
public class clsOperationsOfHaml_ItemsHasHaml
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("OperationsOfHaml")]
    public long OperationsOfHamlId { get; set; }
    public clsOperationsOfHaml OperationsOfHaml { get; set; }
    [ForeignKey("ItemsHasHaml")]
    public long ItemsHasHamlId { get; set; }
    public clsItemsHasHaml ItemsHasHaml { get; set; }
}
