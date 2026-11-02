namespace RMS.Controllers.MashinAlat.Dto
{
    public class SaveDayStateDto
    {
        public Guid BarAvordUserId { get; set; }
        public string Date { get; set; }
        public string State { get; set; }
        public string ActiveDeActive { get; set; }
    }
}
