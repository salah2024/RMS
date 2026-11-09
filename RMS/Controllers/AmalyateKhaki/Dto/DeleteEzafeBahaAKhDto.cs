using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AmalyateKhaki.Dto;

public class DeleteEzafeBahaAKhDto
{
    public long NoeKhakBardariEzafeBahaId { get; set; }
    public Guid AmalyateKhakiInfoForBarAvordId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public long Year { get; set; }
}

public class DeleteEzafeBahaPKDto
{
    public long NoeKhakBardariEzafeBahaId { get; set; }
    public Guid PayKaniInfoForBarAvordId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public long Year { get; set; }
}
