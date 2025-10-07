using RMS.Models.Entity;

namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class PayKaniInfoForBarAvordDetailsInsertedDto
    {
        public Guid RizMetreId { get; set; }
        public long NoeKhakBardariId { get; set; }
        public string NoeKhakBardariName { get; set; }
        public List<clsPayKaniInfoForBarAvordDetailsMore> lstPayKaniInfoForBarAvordDetailsMore { get;set; }
    }
}
