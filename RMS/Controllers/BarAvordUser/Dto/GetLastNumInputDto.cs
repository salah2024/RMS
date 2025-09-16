using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class GetLastNumInputDto
    {
       public NoeFehrestBaha NoeFB { get; set; }
       public NoeBarAvord Type { get; set; }
    }
}
