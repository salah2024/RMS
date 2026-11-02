namespace RMS.Controllers.PaymanKar.Dto;

public class RequestSetMablaghPishnahadiDto
{
    public Guid ItemId { get; set;  }
    public decimal txtValue { get; set; }
}

public class RequestSetMablaghPishnahadiTajhizDto
{
    public Guid CompanyId { get; set;  }
    public Guid BarAvordUserId { get; set;  }
    public decimal txtValue { get; set; }
}
