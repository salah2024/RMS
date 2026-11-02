using RMS.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.TajhizKargah.Dto;

public class TajhizKargahDarsadDto
{
    public Guid BarAvordUserId { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public decimal Darsad { get; set; }
    public decimal Meghdar { get; set; }
    public int Year { get; set; }
}

public class TajhizKargahDarsadForDeleteDto
{
    public Guid BarAvordUserId { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public int Year { get; set; }
}

public class RequestTajhizKargahDarsadDto
{
    public int Year { get; set; }
    public Guid BarAvordUserId { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
}
