namespace RMS.Controllers.KhakRizi.Dto;

public class RequestSaveRizMetreBestarKhakRiziDto
{
   public Guid BarAvordUserID { get; set; }
   public Guid KMId { get; set; }
   public int KMNum { get; set; }
   public string KMS { get; set; }
   public string KME { get; set; }
   public string KhakRiziInfoDetails { get; set; }
   public string KhakRiziInfoDetailsCheckBox { get; set; }
}
