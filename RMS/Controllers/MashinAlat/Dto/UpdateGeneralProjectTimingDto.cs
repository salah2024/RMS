namespace RMS.Controllers.MashinAlat.Dto
{
    public class UpdateGeneralProjectTimingDto
    {
        public Guid Id { get; set; }
        public string StartDate { get; set; }
        public int CountDaies { get; set; }
        public int CourseCountDaies { get; set; }
        public int HolyCourseCountDaies { get; set; }
    }
}
