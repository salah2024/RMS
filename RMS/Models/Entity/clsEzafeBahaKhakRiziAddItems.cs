using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblEzafeBahaKhakRiziAddItems")]
public class clsEzafeBahaKhakRiziAddItems
{
    [Key]
    public long Id {get;set;}
    public string ItemFBShomareh { get; set;}
    [ForeignKey("EzafeBahaKhakRizi")]
    public long EzafeBahaKhakRiziId { get; set;}
    public clsEzafeBahaKhakRizi EzafeBahaKhakRizi { get; set;}
    public string? Condition { get; set;}
    public string? FinalWorking { get; set; }
    public string? CharacterPlus { get; set; }
}
