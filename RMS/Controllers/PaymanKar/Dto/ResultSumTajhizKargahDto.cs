using RMS.Models.Entity;

namespace RMS.Controllers.PaymanKar.Dto;

public class ResultSumTajhizKargahDto
{
    public List<ZaribPishnahadiPaymanKarDto> zaribPishnahadiPaymanKars { get; set; }
    public clsZaribPishnahadiPaymanKar? zaribPishnahadiPaymanKar { get; set; }
}
