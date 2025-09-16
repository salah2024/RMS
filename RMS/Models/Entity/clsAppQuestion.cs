using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure;

namespace RMS.Models.Entity;

[Table("tblAppQuestion")]
public class clsAppQuestion 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }

    public string Title { get; set; }

    public string LatinTitle { get; set; }
}
