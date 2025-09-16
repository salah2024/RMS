using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Board.Dto
{
    public class GetRizMetreForBoardDto
    {
        public long OperationId { get; set; }
        public Guid BaravordId { get; set; }
        public NoeFehrestBaha  NoeFB { get; set; }
        public int  Year { get; set; }
        public List<int>  lstBoardType { get; set; }
    }
}
