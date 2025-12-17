namespace RMS.Controllers.Operation.Dto;

public class ResultCalculateFaslDto
{
    public decimal SumMeghdarJoz { get;set; }
    public decimal JameFasl { get;set; }
    public decimal JameFaslInZarib { get;set; }

}
public class ResultCalculateBarAvordDto
{
    public decimal JameFasl { get;set; }
    public decimal JameFaslWithOutTajhiz { get;set; }
    public decimal JameFaslStar { get;set; }
    public decimal JameFaslKol { get;set; }
    public decimal JameFaslKolWithOutTajhiz { get;set; }
    public decimal JameFaslInZarib { get;set; }
    public decimal JameFaslWithOutTajhizInZarib { get;set; }
    public decimal JameFaslStarInZarib { get;set; }
    public decimal JameFaslKolInZarib { get; set; }
    public decimal JameFaslKolWithOutTajhizInZarib { get; set; }
    public CheckLimitItemStarDto Check { get; set; }
}

public class CheckLimitItemStarDto
{
    public bool Check { get; set; }
    public string Des { get; set; }
}
