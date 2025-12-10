using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class GetLastNumInputDto
    {
       public int Year { get; set; }
       public NoeBarAvord Type { get; set; }
    }
}
