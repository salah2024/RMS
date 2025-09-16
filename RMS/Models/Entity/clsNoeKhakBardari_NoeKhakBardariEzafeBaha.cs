using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// این اضافه بها برای کدام آیتم ها درج میشوند
/// </summary>
[Table("tblNoeKhakBardari_NoeKhakBardariEzafeBaha")]
public class clsNoeKhakBardari_NoeKhakBardariEzafeBaha
{
    [Key]
    public long Id { get; set; }
    [ForeignKey("NoeKhakBardari")]
    public long NoeKhakBardariId { get; set; }
    public clsNoeKhakBardari NoeKhakBardari { get; set; }
    [ForeignKey("NoeKhakBardariEzafeBaha")]
    public long NoeKhakBardariEzafeBahaId { get; set; }
    public clsNoeKhakBardariEzafeBaha NoeKhakBardariEzafeBaha { get; set; }

}
