using RMS.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Controllers.BarAvordUser.Dto;

public class ItemFBStarForShowBaravordDto
{
    public Guid Id { get; set; }
    public Guid BaravordId { get; set; }
    public string Shomareh { get; set; }
    public decimal BahayeVahed { get; set; }
    public int VahedId { get; set; }
    public string VahedName { get; set; }
    public string Sharh { get; set; }
    public List<ViewBarAvordItemStarOutPutRizMetreDto> RizMetre { get; set; }
    public List<ItemsFieldForUserBarAvordOutPutDto> ItemsFields { get; set; }

}
