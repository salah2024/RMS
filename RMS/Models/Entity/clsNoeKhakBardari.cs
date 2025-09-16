using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblNoeKhakBardari")]
    public class clsNoeKhakBardari
    {
        [Key]
        public long Id { get; set; }
        public string FBItemShomareh { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }

    }
}
