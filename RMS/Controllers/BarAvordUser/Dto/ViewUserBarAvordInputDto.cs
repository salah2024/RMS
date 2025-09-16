using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class ViewUserBarAvordInputDto
    {
        public Guid BarAvordUserId { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public string ShomarehFasl { get; set; }
    }
}
