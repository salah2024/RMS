using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblKiloMetrazhOfHaml")]
public class clsKiloMetrazhOfHaml
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public decimal KM { get; set; }
    [ForeignKey("OperationsOfHaml")]
    public long OperationOfHamlId { get; set; } 
    public clsOperationsOfHaml OperationsOfHaml { get; set; }
    [ForeignKey("BarAvordUser")]
    public Guid BarAvordId { get; set; }
    public clsBaravordUser BarAvordUser { get; set; }

}
