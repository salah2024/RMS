using RMS.Models.Entity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class AmalyateKhakiInfoForBarAvordDetailsInsertedDto
    {
        public Guid RizMetreId { get; set; }
        public long NoeKhakBardariId { get; set; }
        public string NoeKhakBardariName { get; set; }
        public List<clsAmalyateKhakiInfoForBarAvordDetailsMore> lstAmalyateKhakiInfoForBarAvordDetailsMore { get;set; }
    }
}
