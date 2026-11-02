using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RMS.Controllers.KhakRizi.EnumKhakRizi;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblKhakRiziCondition")]
public class clsKhakRiziCondition
{
    [Key]
    public long Id { get; set; }
    public EnumRoadType NoeRah { get;set; }
    public EnumNoeDaneBandi NoeDaneBandi { get;set; }
    public EnumHajmKhakRizi NoeHajmKhakRizi { get;set; }
    public string FBShomareh { get; set;}
    public string FBShomarehCondition2 { get; set;}
    public string Condition { get;set; }
    public string FinalWork { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public long Year { get; set;}
}
