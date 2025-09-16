using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblZaribRoadType")]
public class clsZaribRoadType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public decimal ZaribSheni { get; set; }               // ضریب شنی

    public decimal ZaribSakhtehNashodeh { get; set; }     // ضریب ساخته‌نشده

    public int Year { get; set; }                          // سال مربوطه

    public NoeFehrestBaha NoeFB { get; set; }
}
