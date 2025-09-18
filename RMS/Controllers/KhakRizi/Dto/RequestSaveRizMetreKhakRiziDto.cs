namespace RMS.Controllers.KhakRizi.Dto;

public class RequestSaveRizMetreKhakRiziDto
{
    public Guid BarAvordUserID { get; set; }
    public Guid KMId { get; set; }
    public int KMNum { get; set; }
    public string KMS { get; set; }
    public string KME { get; set; }
    public short radioNoeRahKhakRizi { get; set; }
    public decimal DarsadKRDDaneh { get; set; }
    public decimal DarsadKRRDaneh { get; set; }
    public decimal HajmBetween0To30 { get; set; }
    public decimal HajmBetween30To100 { get; set; }
    public decimal HajmBetweenTo100 { get; set; }
    public bool EzafeBahaKRKhakMosalah { get; set; }
}
