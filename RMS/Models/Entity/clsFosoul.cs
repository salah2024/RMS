using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblFosoul")]
    public class clsFosoul
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string LatinName  { get; set; }
        [ForeignKey("NoeFosoul")]
        public long NoeFosoulId { get; set; }
        public clsNoeFosoul NoeFosoul { get;set; }
        public int order { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
    }
}
