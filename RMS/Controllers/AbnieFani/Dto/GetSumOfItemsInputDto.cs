using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AbnieFani.Dto
{
    public class GetSumOfItemsInputDto
    {
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public Guid BarAvordId { get; set; }

    }
}
