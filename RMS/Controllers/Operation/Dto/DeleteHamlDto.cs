using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class DeleteHamlDto
{
    public Guid BarAvordId { get; set; }
    public Guid RIzMetreId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public string FBShomareh { get; set; }
    public long RMShomareh { get; set; }
    public long? ConditionContextId { get; set; }

}

public class UpdateHamlDto
{
    public Guid BarAvordId { get; set; }
    public string FBShomareh { get; set; }
    public decimal? MeghdarJoz { get; set; }
    public int Year { get; set; }
    public long RMShomareh { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
}
