using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// در صورتی که این ضریب پر باشد آیتم ها ازین ضریب استفاده میکنند
/// و در صورتی که برابر با نال یا صفر باشد آیتم ها از جدول ضریب بالاسری استفاده میکنند
/// </summary>

[Table("tblZarayebFosoul")]
public class clsZarayebFosoul
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [ForeignKey("Fosoul")]
    public long FaslId { get; set;  }
    public clsFosoul Fosoul { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribBalaSari { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal? ZaribManteghe { get; set; }
}