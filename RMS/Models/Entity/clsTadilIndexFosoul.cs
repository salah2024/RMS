using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblTadilIndexFosoul")]
public class clsTadilIndexFosoul
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("Fosoul")]
    public long FosoulId { get; set; }
    public clsFosoul Fosoul { get; set; }
    /// <summary>
    /// در صورتی که 1 باشد بدین معناس که دوره فصلی میباشد
    /// و در صورتی که 2 باشد بدین معناس که دوره ماهانه میباشد
    /// </summary>
    public int CourseType { get; set; }
    public int CourseCode { get; set; }
    [Column(TypeName = "decimal(18,3)")]
    public decimal IndexValue { get; set; }
}
