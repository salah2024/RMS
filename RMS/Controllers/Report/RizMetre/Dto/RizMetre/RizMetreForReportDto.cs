using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Report.RizMetre.Dto.RizMetre;


public class RizMetreReportPageVm
{
    public int PageNo { get; set; }

    public List<RizMetreReportItemVm> Items { get; set; }
}

public class RizMetreReportGroupVm
{
    public int NoeFB { get; set; }

    public string NoeFBName { get; set; }

    public int Year { get; set; }

    public List<RizMetreReportItemVm> Items { get; set; }
}

public class RizMetreReportItemVm
{
    public string NoeFBName { get; set; }
    public string ItemShomareh { get; set; }
    public string ItemSharh { get; set; }
    public decimal? ItemBahayeVahed { get; set; }
    public decimal? ItemBahayeVahedNew { get; set; }
    public decimal? ItemBahayeKol { get; set; }
    public string ItemVahed { get; set; }
    public List<RizMetreForReportDto> Details { get; set; }
}

public class RizMetreForReportDto
{
    public string Code { get; set; }
    public string Sharh { get; set; }
    public decimal? Tedad { get; set;  }
    public decimal? Tool { get; set; }  
    public decimal? Arz { get; set; }
    public decimal? Ertefa { get; set; }
    public decimal? Vazn { get; set; }
    public decimal? JameJoz { get; set; }
    public decimal? JameKol { get; set; }
    public decimal? BahayeVahed { get; set; }
    public decimal? BahayeVahedNew { get; set; }
    public decimal? BahayeKol { get; set; }
    public int NoeFB { get; set; }
    public string NoeFBName { get; set; }
    public int Year { get; set; }
    public string ItemShomareh { get; set; }
    public string ShomarehMain { get; set; }
    public string ItemSharh { get; set; }
    public string Tozihat { get; set; }
    public string Vahed { get; set; }
}
