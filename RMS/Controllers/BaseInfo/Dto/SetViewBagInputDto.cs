using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BaseInfo.Dto
{
    public class SetViewBagInputDto
    {
        public Guid BAId { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
    }
}
