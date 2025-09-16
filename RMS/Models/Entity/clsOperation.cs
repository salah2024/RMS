using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblOperation")]
public class clsOperation
{
    [Key]
    public long Id { get; set; }
    public string OperationName { get; set; }
    public int Year { get; set; }
    public string? LatinName { get; set; }
    [ForeignKey("Parent")]
    public long? ParentId { get; set; }
    public clsOperation? Parent { get; set; }
    public string? FunctionCall { get; set; }
    public long Order { get; set; }
    public List<clsOperation>? Children { get; set; }
    public clsOperation_ItemsFB Operation_ItemsFBs { get; set; }
}
