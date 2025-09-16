namespace RMS.Models.StoredProceduresData
{
    public class QuesForAbnieFaniValuesDto
    {
        public Guid ID { get; set; }
        public long QuestionForAbnieFaniId { get; set; }
        public decimal Value { get; set; }
        public int Type { get; set; }
        public int ShomarehFBSelectedId { get; set; }
        public Guid PolVaAbroId { get; set; }
        public string Shomareh { get; set; }

    }
}
