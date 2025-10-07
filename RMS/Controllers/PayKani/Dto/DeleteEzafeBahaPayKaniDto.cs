namespace RMS.Controllers.PayKani.Dto;

public class DeleteEzafeBahaPayKaniDto
{
    public long NoePayKaniEzafeBahaId { get; set; }
    public Guid PayKaniInfoForBarAvordId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public long Year { get; set; }
    public int Type { get;set; }
}
