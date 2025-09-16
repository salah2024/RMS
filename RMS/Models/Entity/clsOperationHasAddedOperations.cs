using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOperationHasAddedOperations")]
public class clsOperationHasAddedOperations
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("Operation")]
    public long OperationId { get; set; }    
    public clsOperation Operation { get; set; }
    [ForeignKey("AddedOperation")]
    public long AddedOperationId { get; set; }
    public clsOperation AddedOperation { get; set; }
    public short Type { get; set; }
}
