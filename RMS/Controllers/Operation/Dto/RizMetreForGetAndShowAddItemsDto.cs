using RMS.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Controllers.Operation.Dto
{
    public class RizMetreForGetAndShowAddItemsDto
    {
        public Guid Id { get; set; }
        public long Shomareh { get; set; }     
        public string ShomarehNew { get; set; }     

        public string Sharh { get; set; }  

        public decimal? Tedad { get; set; } 

        public decimal? Tool { get; set; }  

        public decimal? Arz { get; set; }   

        public decimal? Ertefa { get; set; }  

        public decimal? Vazn { get; set; }   
        public decimal? MeghdarJoz { get; set; }

        public string? Des { get; set; }
        public string ItemFBShomareh { get; set; }
        public bool HasDelButton { get; set; }
        public bool HasEditButton { get; set; }
    }
}
