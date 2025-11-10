namespace RMS.Controllers.Operation.Dto;

public class AllOperationDto
{
    public long ID { get; set; }
    public long order { get; set; }
    public long? ParentId { get; set; }
    public string OperationName { get; set; }
    public string FunctionCall { get; set; }
    public string Sharh { get; set; }
    public string ItemsFBShomareh { get; set; }
    public int Year { get; set; }
    public bool? CheckData { get; set; }
    public bool? HasEnteringValue { get; set; }
    public decimal? OperationDefaultValue { get; set; }
    public decimal? MaxValue { get; set; }
    public decimal? MinValue { get; set; }
    public string? MaxMinValue { get; set; }
}
