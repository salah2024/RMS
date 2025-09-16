using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblZarayebTabdil")]
public class clsZarayebTabdil
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public decimal Z1 { get; set; }            // مقدار Z1

    public decimal Z2 { get; set; }            // مقدار Z2

    public decimal Z3 { get; set; }            // مقدار Z3

    public decimal Z4 { get; set; }            // مقدار Z4

    public string ItemShomareh { get; set; } 

    [ForeignKey("OperationsOfHaml")]
    public long OperationsOfHamlId { get; set; } 
    public clsOperationsOfHaml OperationsOfHaml { get; set; }
    public int Year { get; set; }            
    public NoeFehrestBaha NoeFB { get; set; }
}
