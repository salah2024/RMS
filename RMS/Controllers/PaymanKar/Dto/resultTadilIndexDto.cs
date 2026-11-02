using RMS.Controllers.PaymanKar.Common;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.PaymanKar.Dto;

public class resultTadilIndexDto
{
    public NoeFehrestBaha NoeFB { get; set;  }
    public string NoeFBName { get; set;  }
    public string MabnaIndexTitle { get; set;  }

    public decimal SumBahayeKolInZarib { get; set; }
    public decimal SumBahayeKolInZaribPrev { get; set; }
    public decimal SumDeff { get; set; }

    //public decimal SumNesbat { get; set; }
    public decimal SumMablaghKarkardDoreh { get; set; }
    public decimal SumMablaghTadil { get; set; }

    //public decimal AvgZaribTadil { get; set; }

    public List<FaslTadilIndexCourseDto> lstFaslTadilIndex { get; set; }
}

public class FaslTadilIndexCourseDto
{
    public string FaslCode { get; set; }
    public decimal BahayeKolInZarib { get;set;  }
    public decimal BahayeKolInZaribPrev { get;set;  }
    public decimal Deff { get;set;  }
    public decimal nesbat { get;set;  }
    public decimal MablaghKarkardDoreh { get;set;  }
    public decimal ZaribTadil { get;set;  }
    public decimal MablaghTadil { get;set;  }
    public decimal JamMablaghTadil { get; set; }
    public List<ResultItem>  lstTadilIndex { get; set;  }

}
