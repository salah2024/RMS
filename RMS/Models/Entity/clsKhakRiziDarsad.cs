using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RMS.Controllers.KhakRizi.EnumKhakRizi;

namespace RMS.Models.Entity;

[Table("tblKhakRiziDarsad")]
public class clsKhakRiziDarsad
{
    [Key]
    public long Id { get; set; }
    public EnumRoadType NoeRah { get;set; }
    public EnumNoeDaneBandi NoeDaneBandi { get;set; }
    public EnumHajmKhakRizi NoeHajmKhakRizi { get;set; }
    public int Darsad { get; set;}
    public long Year { get; set;}
}
