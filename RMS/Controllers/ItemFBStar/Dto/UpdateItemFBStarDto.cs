namespace RMS.Controllers.ItemFBStar.Dto;
public class UpdateItemFBStarDto
{
    public Guid Id { get;set; }
    public bool KharidTajhizat { get;set; }
    public int Year { get;set; }
}

public class DeleteItemFBStarDto
{
    public Guid Id { get;set; }
    public int Year { get; set; }
}
