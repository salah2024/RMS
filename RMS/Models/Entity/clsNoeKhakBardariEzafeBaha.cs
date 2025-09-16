using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblNoeKhakBardariEzafeBaha")]
public class clsNoeKhakBardariEzafeBaha
{
    [Key]
    public long Id { get; set; }
    public string FBItemShomareh { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    /// <summary>
    /// آیا این اضافه بها ورودی دارد
    /// </summary>
    public bool hasEnteringValue { get; set; }
    /// <summary>
    /// تعداد ورودی ها چند تا هستن
    /// </summary>
    public int? CountForEnteringValue { get;set; }
    public string? DesForEnteringValue { get; set; }
    public string? DefaultForEnteringValue { get; set; }
    public string? Condition { get; set; }
    public bool? EnableEditing { get;set;}
    public bool? EnableDeleting { get;set;}
}
