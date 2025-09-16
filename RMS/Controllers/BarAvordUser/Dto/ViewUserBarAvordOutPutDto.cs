namespace RMS.Controllers.BarAvordUser.Dto
{
    public class ViewUserBarAvordOutPutDto
    {
        public string ItemFbShomareh { get; set; }
        public string Sharh { get; set; }
        public string Vahed { get; set; }
        public string? BahayeVahed { get; set; }
        public long BahayeVahedNew { get; set; }
        public decimal Meghdar { get; set; }
        public decimal BahayeKol { get; set; }
        public Guid? FBId { get; set; }
        public List<ViewUserBarAvordOutPutRizMetreDto> RizMetre { get; set; }
    }

    public class ViewUserBarAvordOutPutRizMetreDto
    {
        public Guid Id { get; set; }
        public long Shomareh { get; set; }
        public string Sharh { get; set; }
        public decimal? Tedad { get; set; }
        public decimal? Tool { get; set; }
        public decimal? Arz { get; set; }
        public decimal? Ertefa { get; set; }
        public decimal? Vazn { get; set; }
        public decimal? MeghdarJoz { get; set; }
        public string? Des { get; set; }
        public Guid FBId { get; set; }
    }
}
