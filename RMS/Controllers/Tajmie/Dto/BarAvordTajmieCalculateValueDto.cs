using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Tajmie.Dto;

public class BarAvordTajmieCalculateValueDto
{
    public NoeFehrestBaha NoeFB { get; set; }
    public Guid BarAvordId { get; set;  }
    public string FaslCode { get; set;  }
    public int Year { get; set;  }

}
