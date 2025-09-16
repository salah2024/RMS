using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblNiroZaribKarKard")]
public class clsNiroZaribKarKard 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string CodeAmel { get; set; }           

    public int ZaribKarKard { get; set; }      

    public bool IsAmelMoaser { get; set; }     

    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }

    [ForeignKey("NoeMashinAlat")]
    public long NoeMashinAlatId { get; set; }
    public clsMashinType NoeMashinAlat { get; set; }
}
