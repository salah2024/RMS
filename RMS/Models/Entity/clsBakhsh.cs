using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblBakhsh")]
public class clsBakhsh
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("Shahr")]
    public long ShahrId { get; set; }
    public clsShahr Shahr { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribAbnie { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribMechanic { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribBargh { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribRah { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribRahDari { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribEnteghalAb { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribToziAb { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribEnteghalFazelAb { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribAbRostaii { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribChah { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribAbiariVaZeh { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribSad { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribGhanat { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribTahteFeshar { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ZaribAbKhizDari { get; set; }

}
