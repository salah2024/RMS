using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto;

public class BaravordUserToShowDto
{
    public Guid BUId { get; set; }
    public long BUNum { get; set; }
    public string BUName { get; set; }
    public DateTime? BUInsertDate { get; set; }
    public int BUYear { get; set; }
    public string BUNoeFBs { get; set; }
    public string NoeFBName { get; set; }
    public string BUInsertDateSolar { get; set; }

}
