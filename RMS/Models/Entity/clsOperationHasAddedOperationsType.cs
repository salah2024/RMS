using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOperationHasAddedOperationsType")]
public class clsOperationHasAddedOperationsType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string TypeName { get; set; }  

    public string LatinName { get; set; }
}
