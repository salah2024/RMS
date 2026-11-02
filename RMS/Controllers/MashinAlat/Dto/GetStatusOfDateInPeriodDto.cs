namespace RMS.Controllers.MashinAlat.Dto
{
    public class GetStatusOfDateInPeriodDto
    {
        public Guid BarAvordUserId { get; set; }
        public string StartDate { get; set; }
        public int ProjectLen { get; set; }
        public int CourseCountDaies { get; set; }
        public int HolyCourseCountDaies { get; set; }
    }
}
