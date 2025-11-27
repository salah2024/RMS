using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblRizMetreStar")]
public class clsRizMetreStar : clsBaseEntity
{
    public long Shomareh { get; set; }            // شماره آیتم
    /// <summary>
    /// شماره ای که برای اضافه/کسر بها ایجاد میشود
    /// </summary>
    public string? ShomarehNew { get; set; }      

    public string Sharh { get; set; }               

    [Column(TypeName = "decimal(18,4)")]
    public decimal? Tedad { get; set; }                 
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Tool { get; set; }             
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Arz { get; set; }               
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Ertefa { get; set; }       
    [Column(TypeName = "decimal(18,4)")]
    public decimal? Vazn { get; set; }             
    [Column(TypeName = "decimal(18,4)")]
    public decimal? MeghdarJoz { get; set; }          
    public string? Des { get; set; }
    [ForeignKey("ItemFBStar")]
    public Guid ItemFBStarId { get; set; }
    public clsItemFBStar ItemFBStar { get; set; }
    
}
