using RMS.Models.Entity;

namespace RMS.Controllers.Report.RizMetre.Dto.FB;

public class ReportDataForFBSummaryL1Dto
{
    public clsBaravordUser baravordUser { get; set; }
    public List<ReportDataForFBSummaryDto> reportDataForFBSummary { get; set; }
}

    public class ReportDataForFBSummaryDto
{
    public string NoeFBName { get; set; }

    // بدون ضرایب
    public decimal? Base { get; set; }
    public decimal? Star { get; set; }
    public decimal? Total { get; set; }

    // با ضرایب
    public decimal? BaseZ { get; set; }
    public decimal? StarZ { get; set; }
    public decimal? TotalZ { get; set; }

    public string Title { get; set; }
    public int Year { get; set; }
}

