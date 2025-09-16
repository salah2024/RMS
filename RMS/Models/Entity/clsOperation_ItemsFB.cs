using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOperation_ItemsFB")]
public class clsOperation_ItemsFB
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }

    public string ItemsFBShomareh { get; set; } 

    public string? NextOperation { get; set; }   

    public int Year { get; set; }
}
