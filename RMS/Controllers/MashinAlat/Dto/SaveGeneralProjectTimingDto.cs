namespace RMS.Controllers.MashinAlat.Dto
{
    public class SaveGeneralProjectTimingDto
    {
        public Guid BarAvordId { get; set; }
        public string StartDate { get; set; }
        public int CountDaies { get; set; }
        public int CourseCountDaies { get; set; }
        public int HolyCourseCountDaies { get; set; }
    }
}
