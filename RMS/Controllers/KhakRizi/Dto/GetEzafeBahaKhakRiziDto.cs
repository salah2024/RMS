namespace RMS.Controllers.KhakRizi.Dto;

public class GetEzafeBahaKhakRiziDto
{
    public long Id { get; set; }
    public string GroupContext { get; set; }
    public string Context { get; set; }
    public long ConditionGroupId { get; set; }
    public bool? HasEnteringValue { get; set; }
    public int Type { get; set;  }
}

public class GetEzafeBahaKhakRiziNewDto
{
    public int Year { get; set; }
    public List<string> Shomareh { get; set; }
    public Guid BarAvordId { get; set; }

}
