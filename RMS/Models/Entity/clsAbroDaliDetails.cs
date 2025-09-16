using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAbroDaliDetails")]
public class clsAbroDaliDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [ForeignKey("AbadeKoole")]
    public long AbadeKooleId { get; set; }
    public clsAbadeKoole AbadeKoole { get; set; }
    public int Pos { get; set; }
    public int Ghotr { get; set; }
    public decimal Tedad { get; set; }
    public decimal Fasele { get; set; }
    public decimal Tool { get; set; }
    public decimal VazMilgard1M { get; set; }
    public decimal VazMilgardSE { get; set; }
}
