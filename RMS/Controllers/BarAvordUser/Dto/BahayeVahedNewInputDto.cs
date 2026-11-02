using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class BahayeVahedNewInputDto
    {
        public Guid FBId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public string BahayeVahedNew { get; set; }
        public string itemFbShomareh { get; set; }
        public int Year { get; set; }
    }

    public class ItemZaribInputDto
    {
        public Guid FBId { get; set; }
        public NoeFehrestBaha NoeFBId { get; set; }
        public Guid BarAvordUserId { get; set; }
        public decimal? ItemZarib { get; set; }
        public string itemFbShomareh { get; set; }
        public int Year { get; set; }
    }
    public class ItemZaribStarInputDto
    {
        public Guid ItemStarId { get; set; }
        public decimal? ItemZarib { get; set; }
    }
}
