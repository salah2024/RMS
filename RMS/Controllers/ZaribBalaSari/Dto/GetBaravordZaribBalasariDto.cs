using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ZaribBalaSari.Dto;

public class GetBaravordZaribBalasariDto
{
    public Guid BaravordId { get; set; }
    public int Year { get; set; }
}
