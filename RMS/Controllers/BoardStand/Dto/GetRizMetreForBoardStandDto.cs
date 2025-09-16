using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BoardStand.Dto
{
    public class GetRizMetreForBoardStandDto
    {
        public Guid BaravordId { get;set; }
        public NoeFehrestBaha NoeFB { get; set; }
        public int Year { get; set; }
        public List<int> lstBoardStandType { get; set; } 
    }
}
