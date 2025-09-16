namespace RMS.Controllers.AmalyateKhaki.Dto
{
    public class GetExistingKMAmalyateKhakiInfoWithBarAvordDto
    {
        public Guid ID { get; set; }
        public Guid BaravordUserId { get; set; }
        public int Type { get; set; }
        public string FromKM { get; set; }
        public string ToKM { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int KMNum { get; set; }
        public string FromKMSplit { get; set; }
        public string ToKMSplit { get; set; }

    }
}
