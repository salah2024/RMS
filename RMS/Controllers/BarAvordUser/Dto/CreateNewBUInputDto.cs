using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser.Dto
{
    public class CreateNewBUInputDto
    {
        public NoeBarAvord Type { get; set; }
        public string Title { get; set; }
        public string NoeFBs { get; set; }
        public int Year { get; set; }
        public string UserName { get; set; }
    }
    public class CreateNewSooratVaziatDto
    {
        public NoeBarAvord Type { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string UserName { get; set; }
        public Guid buId { get; set; }
        public string? FromDate { get; set;  }
        public string? ToDate { get; set; }
    }
}
