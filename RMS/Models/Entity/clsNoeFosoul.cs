using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblNoeFosoul")]
    public class clsNoeFosoul
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string NoeFaslName { get; set; }
        public int Order { get; set; }
        public int Year { get; set; }
    }
}
