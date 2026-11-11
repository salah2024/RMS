using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class AllOperationDto
{
    public long ID { get; set; }
    public long order { get; set; }
    public long? ParentId { get; set; }
    public bool HasChildren { get; set; }
    public string OperationName { get; set; }
    public string? LatinName { get; set; }
    public string FunctionCall { get; set; }
    public string Sharh { get; set; }
    public string ItemsFBShomareh { get; set; }
    public string ItemsFBShomarehMain { get; set; }
    public int Year { get; set; }
    public bool? CheckData { get; set; }
    public bool? HasEnteringValue { get; set; }
    public decimal? OperationDefaultValue { get; set; }
    public decimal? Value { get; set; }
    public decimal? MaxValue { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? SumVazn { get; set; }
    public string? MaxMinValue { get; set; }
    public string? Description { get; set; }
    public bool? CheckNecessary { get; set; }
    public NoeFehrestBaha? NoeFB { get; set; }
    public string? Vahed { get; set; }
}
