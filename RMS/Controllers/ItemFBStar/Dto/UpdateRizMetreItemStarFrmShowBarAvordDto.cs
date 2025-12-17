using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemFBStar.Dto;

public class UpdateRizMetreItemStarFrmShowBarAvordDto
{
    public int Year { get; set; }   
    public Guid Id { get; set; }
    public string Sharh { get; set; }
    public decimal? Tedad { get; set; }
    public decimal? Tool { get; set; }
    public decimal? Arz { get; set; }
    public decimal? Ertefa { get; set; }
    public decimal? Vazn { get; set; }
    public string? Des { get; set; }
    public string Code { get; set; }
}
