namespace RMS.Controllers.Operation.Dto
{
    public class RizMetreUsersForItemsAddingToFBInputDto
    {
        public Guid Id { get; set; }
        public long Shomareh { get; set; }

        public string Sharh { get; set; }

        public decimal? Tedad { get; set; }

        public decimal? Tool { get; set; }

        public decimal? Arz { get; set; }

        public decimal? Ertefa { get; set; }

        public decimal? Vazn { get; set; }

        public string? Des { get; set; }
        public Guid FBId { get; set; }
        public string Type { get; set;  }
        public string? ForItem { get; set;  }
    }
}
