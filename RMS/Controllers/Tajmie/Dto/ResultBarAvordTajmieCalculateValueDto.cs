namespace RMS.Controllers.Tajmie.Dto;

public class ResultBarAvordTajmieCalculateValueDto
{
    public long TajmieFosoulId { get; set; }
    public string ItemCode { get; set;  }
    public decimal Value { get; set; }
    public string Sharh { get; set; }
    public string Vahed { get; set; }
    public decimal BahayehKol { get; set; }
    public decimal BahayeVahed { get; set; }
    public decimal Meghdar { get; set; }
    public decimal WeightPercent { get; set; }
    public List<FBShomarehValueDto> Items { get; set; }
}


public class FBShomarehValueDto
{
    public string FBShomareh { get; set; }
    public string FBSharh { get; set; }
    public decimal Value { get; set; }
    public string BahayeVahed { get; set; }
    public string BahayeKolInMeghdar { get; set; }
    public decimal WeightPercent { get; set; }
}

