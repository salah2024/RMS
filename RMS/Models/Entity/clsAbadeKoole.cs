using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAbadeKoole")]
public class clsAbadeKoole 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [ForeignKey("HadAksarErtefaKoole")]
    public long HadAksarErtefaKooleId { get; set; }
    public clsHadAksarErtefaKoole HadAksarErtefaKoole { get; set; }
    public string Hs { get; set; }
    public decimal a1 { get; set; }
    public decimal a2 { get; set; }
    public decimal b1 { get; set; }
    public decimal b2 { get; set; }
    public decimal c1 { get; set; }
    public decimal c2 { get; set; }
    public decimal f { get; set; }
    public decimal m { get; set; }
    public decimal t { get; set; }
    public int HadAghalZavieEstekak { get; set; }
    public decimal DerzEnbesat { get; set; }
    public decimal p1 { get; set; }
    public decimal p2 { get; set; }
    public decimal e { get; set; }
    public decimal n { get; set; }
    public decimal k { get; set; }
}
