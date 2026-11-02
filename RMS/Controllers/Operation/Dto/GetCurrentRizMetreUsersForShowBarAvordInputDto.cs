using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto
{
    public class GetCurrentRizMetreUsersForShowBarAvordInputDto
    {
        public NoeFehrestBaha NoeFB { get; set; }
        public long Year { get; set; }
        public string FBId { get; set; }
        public string ItemFBShomareh { get; set; }
    }
}
