using RMS.Controllers.PaymanKar.Common;

namespace RMS.Controllers.PaymanKar.Dto
{
    public class TadilReportDto
    {
    }


    public class TadilPdfVm
    {
        public long AllDay { get; set; }

        public string ProjectName { get; set; }
        public string Title { get; set; }

        public string NoeFBName { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string MabnaIndexTitle { get; set; }

        public List<TadilPdfRowVm> Rows { get; set; }
    }

    public class TadilPdfRowVm
    {
        public string Fasl { get; set; }

        public ResultItem Item { get; set; }

        public decimal MablaghCurrent { get; set; }

        public decimal MablaghPrev { get; set; }

        public decimal Deff { get; set; }
    }
}
