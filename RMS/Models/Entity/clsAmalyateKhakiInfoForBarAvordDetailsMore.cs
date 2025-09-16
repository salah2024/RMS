using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAmalyateKhakiInfoForBarAvordDetailsMore")]
public class clsAmalyateKhakiInfoForBarAvordDetailsMore:clsBaseEntity
{
    [ForeignKey("AmalyateKhakiInfoForBarAvordDetails")]
    public Guid AmalyateKhakiInfoForBarAvordDetailsId { get; set; }
    public clsAmalyateKhakiInfoForBarAvordDetails AmalyateKhakiInfoForBarAvordDetails { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
}
