using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblRizMetreUsersHistory")]
public class clsRizMetreUsersHistory : clsBaseEntity
{
    [ForeignKey("RizMetreUsers")]
    public Guid RizMetreUsersId { get; set; }
    public clsRizMetreUsers RizMetreUsers { get; set; }
    public long Shomareh { get; set; }       
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
}
