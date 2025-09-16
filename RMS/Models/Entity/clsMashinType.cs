using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblMashinType")]
public class clsMashinType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string MashinTypeName { get; set; }  

    public string Code { get; set; }            

    public string LatinName { get; set; }
}
