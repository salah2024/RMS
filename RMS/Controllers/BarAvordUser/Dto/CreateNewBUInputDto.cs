using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class CreateNewBUInputDto
    {
        public NoeBarAvord Type { get; set; }
        public string Title { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
        public string UserName { get; set; }
    }
}
