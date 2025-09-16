using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblItemsForGetValues")]
public class clsItemsForGetValues
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string ItemShomareh { get; set; }
    public string ItemShomarehForGetValue { get; set; }
    public int Year { get; set; }
    public string RizMetreFieldsRequire { get; set; }
}
