using QuestPDF.Infrastructure;
using RMS.Controllers.Report.RizMetre.Dto.FB;
using RMS.Controllers.Report.RizMetre.Dto.Financial;
using RMS.Controllers.Report.RizMetre.Dto.FosoulForBarAvord;
using RMS.Controllers.Report.RizMetre.Dto.RizMetre;
using RMS.Models.Entity;

namespace RMS.Controllers.Report.RizMetre.Dto.BaravordFullPdf;

public class BaravordFullPdfDocument : IDocument
{
    private readonly List<RizMetreReportGroupVm> _rizMetre;
    private readonly List<FinancialNoeFBVm> _financial;
    private readonly List<ReportDataForFosoulForBarAvordDto> _summary;
    private readonly ReportDataForFBSummaryL1Dto _reportDataForFB;

    private readonly clsBaravordUser _baravordUser;
    private readonly string _projectName;

    public BaravordFullPdfDocument(
        List<RizMetreReportGroupVm> rizMetre,
        List<FinancialNoeFBVm> financial,
        List<ReportDataForFosoulForBarAvordDto> summary,
        ReportDataForFBSummaryL1Dto reportDataForFB,
        clsBaravordUser baravordUser,
        string projectName)
    {
        _rizMetre = rizMetre;
        _financial = financial;
        _summary = summary;
        _reportDataForFB = reportDataForFB;
        _baravordUser = baravordUser;
        _projectName = projectName;
    }

    public void Compose(IDocumentContainer container)
    {
        // بخش چهارم
        new FBSummaryPdfDocument(
            _reportDataForFB)
            .Compose(container);

        // بخش سوم
        new ReportDataForFosoulForBarAvordPdfDocument(
            _summary)
            .Compose(container);
        // بخش دوم
        new FinancialPdfDocument(
            _financial,
            _baravordUser,
            _projectName)
            .Compose(container);

        // بخش اول
        new RizMetrePdfDocument(
            _rizMetre,
            _baravordUser,
            _projectName)
            .Compose(container);

    }
}
