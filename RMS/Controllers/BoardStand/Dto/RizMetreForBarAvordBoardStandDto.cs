namespace RMS.Controllers.BoardStand.Dto
{
    public class RizMetreForBarAvordBoardStandDto
    {
        public Guid Id { get; set; }
        public Guid RizMetreId { get; set; }
        public Guid BarAvordAddedBoardStandId { get; set; }
        public string ItemFBShomareh { get; set; }
        public long Shomareh { get; set; }
        public string Sharh { get; set; }
        public string FBSharh { get; set; }
        public string? Des { get; set; }
        public decimal? Tedad { get; set; }
        public decimal? Tool { get; set; }
        public decimal? Arz { get; set; }
        public decimal? Ertefa { get; set; }
        public decimal? Vazn { get; set; }
        public decimal? MeghdarJoz { get; set; }
    }
}
