using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AbnieFani.Dto
{
    public class GetQuestionForAbnieFaniInputDto
    {
        public Guid PolVaAbroId { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
    }
}
