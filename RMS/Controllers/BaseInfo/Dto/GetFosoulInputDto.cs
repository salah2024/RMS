using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BaseInfo.Dto;

public class GetFosoulInputDto
{
    public NoeFehrestBaha NoeFBId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public Guid CompanyId { get; set; }
    public int Year { get; set; }
}

public class GetFosoulPishnahadGhaymatDto
{
    public NoeFehrestBaha NoeFBId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public int Year { get; set; }
}
public class GetSumAllFBPishnahadGhaymatDto
{
    //public NoeFehrestBaha NoeFBId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public Guid CompanyId { get; set; }
    public int Year { get; set; }
}
public class ResultSumAllFBPishnahadGhaymatDto
{
    public NoeFehrestBaha NoeFB { get; set; }
    public string FBTitle { get; set;  }
    public decimal SumBaravordFaslWithoutTajhiz { get; set; }
    public decimal SumBaravordFaslBaZaribWithoutTajhiz { get; set; }
    public decimal? SumPishnahadPaymanKarWithoutTajhiz { get; set; } = 0;
    public decimal? SumZaribJozWithoutTajhiz { get; set; } = 0;
}

public class ResultSumAllFBWithTajhizDto
{
    public decimal SumBaravordFaslTajhiz { get; set; }
    public decimal SumBaravordFaslBaZaribTajhiz { get; set; }
    public decimal? SumPishnahadPaymanKarTajhiz { get; set; } = 0;
    public decimal? SumZaribJozTajhiz { get; set; } = 0;
}
