namespace RMS.Controllers.BarAvordUser.Dto;

public class ItemsFieldForUserBarAvordOutPutDto
{
    public long Id { get; set; }
    public string ItemShomareh { get; set; }

    public int FieldType { get; set; }

    public string Vahed { get; set; }      // مثلاً: "متر", "عدد", ...

    public bool IsEnteringValue { get; set; }
    public bool EssentialValue { get; set; }
}
