using RMS.Controllers.Tajmie.Dto;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Report.Tajmie.Dto;

public class TajmieSingleLevelReportDto
{
    public string FaslCode { get; set; } = "";

    public string Title { get; set; } = "";

    public decimal BahayeKol { get; set; }

    public decimal WeightPercent { get; set; }
}

public class TajmieSingleLevelReportGroupDto
{
    public NoeFehrestBaha NoeFB { get; set; }

    public string NoeFBName { get; set; } = "";

    public long Year { get; set; }

    public List<TajmieReportFaslDto> Fosoul { get; set; }
        = new();

    public decimal TotalBahayeKol { get; set; }

    public decimal TotalWeightPercent { get; set; }
}

public class TajmieSingleLevelFullReportDto
{
    public string ProjectName { get; set; } = "";

    public string Peymankar { get; set; } = "";

    public string Moshaver { get; set; } = "";

    public string DastgahEjraei { get; set; } = "";

    public string ShomarehPeyman { get; set; } = "";

    public string TarikhPeyman { get; set; } = "";

    public long Year { get; set; }

    public List<TajmieSingleLevelReportGroupDto> Groups { get; set; }
        = new();
}
