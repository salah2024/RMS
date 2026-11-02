using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class RemoveStarValueInFBInputDto
    {
        public Guid BarAvordId { get; set;  }
        public NoeFehrestBaha NoeFB { get; set; }
        public string FBId { get;set; }
        public string itemFbShomareh { get;set; }
        public int Year { get; set; }
    }
}
