namespace RMS.Controllers.Report.RizMetre.Dto.Financial;


public class FinancialNoeFBVm
{
    public int NoeFB { get; set; }

    public string NoeFBName { get; set; }

    public List<FinancialChapterVm> Chapters { get; set; } = new();
    public decimal? TajhizKargahAmount { get; set; }

    public decimal? TajhizKargahPercent { get; set; }

    public decimal Total => Chapters.Sum(x => x.SumAmount);
}
public class FinancialChapterVm
{
    /// <summary>
    /// نوع فهرست بها
    /// </summary>
    public int NoeFB { get; set; }

    /// <summary>
    /// کد فصل
    /// </summary>
    public string ChapterCode { get; set; }

    /// <summary>
    /// عنوان فصل
    /// </summary>
    public string ChapterName { get; set; }

    public List<FinancialItemVm> BaseItems { get; set; } = new();

    public List<FinancialItemVm> StarItems { get; set; } = new();

    public decimal? ZaribBalasari { get; set; }
    public decimal? ZaribMantaghe { get; set; }
    public decimal? ZaribPishnahadiPK { get; set; }

    public decimal BaseSum => BaseItems.Sum(x => x.BahayeKol);

    public decimal StarSum => StarItems.Sum(x => x.BahayeKol);

    public decimal SumAmount => BaseSum + StarSum;

}


public class FinancialItemVm
{

    public bool IsStarItem { get; set; }
    /// <summary>
    /// شماره آیتم
    /// </summary>
    public string ItemCode { get; set; }

    /// <summary>
    /// شرح آیتم
    /// </summary>
    public string ItemSharh { get; set; }

    /// <summary>
    /// واحد
    /// </summary>
    public string Vahed { get; set; }
    public string NoeFBName { get; set; }

    /// <summary>
    /// بهای واحد
    /// </summary>
    public decimal? BahayeVahed { get; set; }

    /// <summary>
    /// مقدار کل
    /// </summary>
    public decimal Meghdar { get; set; }

    /// <summary>
    /// مبلغ کل آیتم
    /// </summary>
    public decimal BahayeKol { get; set; }

    public bool IsTajhizKargah { get; set; }
}
