using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.AbnieFani.Dto
{
    public class SaveEzafeBahaAndLakeGiriRizMetreInputDto
    {
        public string Tool { get; set; }
        public string Ertefa { get; set; }
        public string Arz { get; set; }
        public string ItemShomareh { get; set; }
        public Guid RizMetreId { get; set; }
        public Guid BarAvordId { get; set; }
        public int LevelNumber { get; set; }
        public int Year { get; set; }   
        public NoeFehrestBaha NoeFB { get; set; }
    }
}
