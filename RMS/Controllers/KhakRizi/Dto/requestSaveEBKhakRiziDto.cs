namespace RMS.Controllers.KhakRizi.Dto;

public class requestSaveEBKhakRiziDto
{
    public long ConditionContextId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public long Year { get; set; }
    public int Num { get; set; }


}

public class requestDeleteEBKhakRiziDto
{
    public long ConditionContextId { get; set; }
    public Guid BarAvordUserId { get; set; }
    public long Year { get; set; }
    public int Num { get; set; }
}
