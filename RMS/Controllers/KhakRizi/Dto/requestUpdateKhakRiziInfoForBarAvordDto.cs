namespace RMS.Controllers.KhakRizi.Dto;

public class requestUpdateKhakRiziInfoForBarAvordDto
{
    public Guid BarAvordUserId { get; set; }
    public Guid KMKhakRiziId { get; set; }
    public int KMNum { get; set; }
    public long FromKM { get; set; }
    public long ToKM { get; set; }
    public short radioNoeRahKhakRizi { get; set; }
    public string DarsadKRDDaneh { get; set; }
    public string DarsadKRRDaneh { get; set; }
    public string HajmBetween0To30 { get; set; }
    public string HajmBetween30To100 { get; set; }
    public string HajmBetweenTo100 { get; set; }
    public bool EzafeBahaKRKhakMosalah { get; set; }
    public string KhakRiziInfoDetails { get; set; }
    public string KhakRiziInfoDetailsCheckBox { get; set; }
}
