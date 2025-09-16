namespace RMS.Controllers.AbnieFani.Dto
{
    public class DeleteEzafeBahaForAbnieFaniPolInputDto
    {
        public Guid BarAvordUserID { get; set; }
        public Guid PolVaAbroId { get; set; }
        public long QuestionId { get; set; }
        public string ItemFBForDel { get; set; }

    }
}
