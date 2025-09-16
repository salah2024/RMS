using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace RMS.Models.Entity;

[Table("tblAmalyateKhakiInfoForBarAvordMore")]
public class clsAmalyateKhakiInfoForBarAvordMore:clsBaseEntity
{
    [ForeignKey("AmalyateKhakiInfoForBarAvord")]
    public Guid AmalyateKhakiInfoForBarAvordId { get; set; }
    public clsAmalyateKhakiInfoForBarAvord AmalyateKhakiInfoForBarAvord { get; set; }
    public  string Name { get; set; }
    public  decimal Value { get; set; }
}
