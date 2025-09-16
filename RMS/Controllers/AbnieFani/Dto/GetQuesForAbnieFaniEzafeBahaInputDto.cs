using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AbnieFani.Dto
{
    public class GetQuesForAbnieFaniEzafeBahaInputDto
    {
        public Guid BarAvordUserId { get; set; }
        public Guid PolVaAbroId { get; set; }
        public int PolNum { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
    }
}
