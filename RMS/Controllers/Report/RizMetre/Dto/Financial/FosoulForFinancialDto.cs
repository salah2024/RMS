using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Report.RizMetre.Dto.Financial;

public class FosoulForFinancialDto
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string LatinName { get; set; }
    public int order { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    public decimal? ZaribBalasari { get; set; }
    public decimal? ZaribMantaghe { get; set; }
    public decimal? ZaribPishnahadiPK { get; set; }
}
