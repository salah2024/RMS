namespace RMS.Controllers.AbnieFani.Dto
{
    public class UpdateEzafeBahaForAbnieFaniPolInputDto
    {
        public Guid BarAvordId { get; set; }
        public Guid PolVaAbroId { get; set; }
        public int PolNum { get; set; }
        public string ItemFBForUpdate { get; set; }
        public string ItemShomareh { get; set; }
        public string Hajm { get; set; }
        public string Meghdar { get; set; }

    }
}
