namespace RMS.Controllers.AbnieFani.Dto
{
    public class SaveEzafeBahaForAbnieFaniPolInputDto
    {
        public Guid PolVaAbroId { get; set; }
        public int PolNum { get; set; }
        public Guid BarAvordId { get; set; }
        public long QuestionForAbnieFaniId { get; set; }
        public string ItemsForAdd { get; set; }
        public string ItemFBForAdd { get; set; }
        public int LevelNumber { get; set; }
    }
}
