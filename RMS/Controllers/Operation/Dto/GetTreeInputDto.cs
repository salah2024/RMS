using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class GetTreeInputDto
    {
        public int Year { get; set; }
        //public NoeFehrestBaha NoeFBId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public string OpName { get; set; }
        public long? ParentId { get; set; } = 0;
    }
    public class SearchTreeInputDto
    {
        public int Year { get; set; }
        public Guid BarAvordUserId { get; set; }
        public string SearchText { get; set; } = "";
    }

    public class GetTreeInputForOneDto
    {
        public int Year { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public string OpName { get; set; }
    }
}
