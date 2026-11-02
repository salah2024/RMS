using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BoardStand.Dto
{
    public class ReturnBoardStandItemsDto
    {
        public Guid BaravordId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
    }
}
