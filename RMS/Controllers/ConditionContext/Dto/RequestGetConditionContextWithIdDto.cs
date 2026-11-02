using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ConditionContext.Dto;

public class RequestGetConditionContextWithIdDto
{
    public long Id { get; set; }    
}

public class RequestCheckItemHasConditionContextDto
{
    public long Id { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
}

