using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblRizKiloMetrazhOfHaml")]
public class clsRizKiloMetrazhOfHaml:clsBaseEntity
{
    [ForeignKey("KiloMetrazhOfHaml")]
    public long KiloMetrazhOfHamlId { get; set; }   
    public clsKiloMetrazhOfHaml KiloMetrazhOfHaml { get; set; }

    public decimal Asfalt { get; set; }        

    public decimal Sheni { get; set; }   

    public decimal SakhteNashodeh { get; set; } 

    public string ItemFB { get; set; }
}
