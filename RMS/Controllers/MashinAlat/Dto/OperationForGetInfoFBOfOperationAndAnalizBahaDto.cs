using RMS.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Controllers.MashinAlat.Dto;

public class OperationForGetInfoFBOfOperationAndAnalizBahaDto
{
    public long Id { get; set; }
    public string OperationName { get; set; }
    public int Year { get; set; }
    public string? LatinName { get; set; }
    public long Order { get; set; }
    public long? ParentId { get; set; }
}
