using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOperationAmelMoaser")]
public class clsOperationAmelMoaser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }
    public string CodeAmel { get; set; }
}
