using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Tajmie.Dto;

public class TajmieFullReportDto
{
    public long Year { get; set; }

    public string ProjectTitle { get; set; } = "";

    public string ShomarehPeyman { get; set; } = "";

    public string TarikhPeyman { get; set; } = "";

    public List<TajmieReportGroupDto> Groups { get; set; }
        = new();


    public string NoeFBNames =>
        string.Join(
            "، ",
            Groups
                .Select(x => x.NoeFBName)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct());


    public decimal TotalBahayeKol =>
        Groups.Sum(x => x.TotalBahayeKol);


    public decimal TotalWeightPercent =>
        TotalBahayeKol > 0
            ? 100m
            : 0m;
}

public class TajmieReportGroupDto
{
    public NoeFehrestBaha NoeFB { get; set; }

    public string NoeFBName { get; set; } = "";

    public long Year { get; set; }

    public List<TajmieReportFaslDto> Fosoul { get; set; }
        = new();

    public decimal TotalBahayeKol { get; set; }

    public decimal TotalWeightPercent { get; set; }
}


public class TajmieReportDto
{
    public long Year { get; set; }

    public string NoeFBName { get; set; } = "";

    public string ProjectTitle { get; set; } = "";

    public string ShomarehPeyman { get; set; } = "";

    public string TarikhPeyman { get; set; } = "";

    public List<TajmieReportFaslDto> Fosoul { get; set; }
        = new();

    public decimal TotalBahayeKol { get; set; }

    public decimal TotalWeightPercent { get; set; }
}


public class TajmieReportFaslDto
{
    public long Id { get; set; }

    public string FaslCode { get; set; } = "";

    public string Title { get; set; } = "";

    public decimal BahayeKol { get; set; }

    public decimal WeightPercent { get; set; }

    public List<TajmieReportItemDto> Items { get; set; }
        = new();
}


public class TajmieReportItemDto
{
    public long Id { get; set; }

    public string ItemCode { get; set; } = "";

    public string Sharh { get; set; } = "";

    public string Vahed { get; set; } = "";

    public decimal Meghdar { get; set; }

    public decimal BahayeVahed { get; set; }

    public decimal BahayeKol { get; set; }

    public decimal WeightPercent { get; set; }

    public List<TajmieReportDetailDto> Details { get; set; }
        = new();
}


public class TajmieReportDetailDto
{
    public string FBShomareh { get; set; } = "";

    public string Sharh { get; set; } = "";

    public decimal Meghdar { get; set; }

    public decimal BahayeVahed { get; set; }

    public decimal BahayeKol { get; set; }

    public decimal WeightPercent { get; set; }
}