using RMS.Controllers.PaymanKar.Common;

namespace RMS.Controllers.PaymanKar.Dto;

public class ResultGetTadilIndex
{
    public string ProjectName { get; set; }
    public string Title { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public long Year { get; set; }
    public long AllDay { get; set; }
    public List<resultTadilIndexDto> lstresultTadilIndexDtos { get; set; }

}

public class TadilReportPageVm
{
    public int PageNo { get; set; }

    public resultTadilIndexDto TadilIndex { get; set; }

    public List<TadilReportRowVm> Rows { get; set; } = new();
}

public class TadilReportRowVm
{
    public FaslTadilIndexCourseDto Fasl { get; set; }

    public ResultItem Item { get; set; }
}

public class TadilReportVm
{
    public ResultGetTadilIndex Report { get; set; }

    public List<TadilReportPageVm> Pages { get; set; } = new();
}
