using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Tajmie.Dto;

public class FBForBarAvordTajmieDto
{
    public string Shomareh { get; set; }
    public decimal ZaribMantaghe { get; set; }
    public decimal ZaribBalaSari { get; set; }
    public string BahayeVahed { get;set;  }
}


public class ZaribFaslResultDto
{
    public string FBShomarehMain { get; set; }
    public string? FBShomarehNew { get; set; }
    public int Year { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public long? FaslId { get; set; }
    public decimal? ZaribBalaSari { get; set; }
    public decimal? ZaribManteghe { get; set; }
}