using RMS.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class BHForChangeBarAvordHamlNL
{
    public Guid ID { get; set;  }
    public Guid BarAvordId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public string FBShomareh { get; set; }
    public string FBShomarehHaml { get; set; }
    public decimal? Value { get; set; }
    public decimal? ValueBase { get; set; }
}
