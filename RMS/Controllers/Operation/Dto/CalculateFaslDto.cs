using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class CalculateFaslDto
{
    public Guid BarAvordId { get; set; }
    public Guid FBId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public int Year { get; set; }
    public string Code { get; set; }
}
public class CalculateBarAvordDto
{
    public Guid BarAvordId { get; set; }
    public int Year { get; set; }
}

public class CalculateFosoulDto
{
    public Guid BarAvordId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public int Year { get; set; }
}
