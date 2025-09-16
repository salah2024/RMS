using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBaseInfo")]
public class clsBaseInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Code {  get; set; }
    public string Name { get; set; }
    public string LatinName { get; set; }
    [ForeignKey("Type")]
    public long TypeId { get;set; }
    public clsBaseInfoType Type { get;set;}
    public long Priority { get; set; }
}
