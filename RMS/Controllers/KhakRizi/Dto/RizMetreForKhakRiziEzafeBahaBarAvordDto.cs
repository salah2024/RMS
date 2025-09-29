using RMS.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Controllers.KhakRizi.Dto;

public class RizMetreForKhakRiziEzafeBahaBarAvordDto
{
    public long Shomareh { get; set; }  
    public string Sharh { get; set; } 
    public decimal? Tedad { get; set; }    
    public decimal? Tool { get; set; }    
    public decimal? Arz { get; set; } 
    public decimal? Ertefa { get; set; }  
    public decimal? Vazn { get; set; }      
    public decimal? MeghdarJoz { get; set; }
    public string? Des { get; set; }
    public Guid FBId { get; set; }
    public string ItemFBShomareh { get; set; }
    public long? OperationsOfHamlId { get; set; }
    public string? ForItem { get; set; }
    public string Type { get; set; }
    public bool? hasEditButton { get; set; }
    public bool? hasDelButton { get; set; }
}
