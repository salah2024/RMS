using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ItemFBStar.Dto
{
    public class DeleteRizMetreInputStarFromShowBarAvordDto
    {
        public Guid Id { get; set; }
        public Guid BarAvordId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public int Year { get; set; }
        public string FBShomareh { get; set; }
    }
}
