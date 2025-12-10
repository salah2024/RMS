using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

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
        public int order { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public NoeFehrestBaha NoeFB { get; set; }

        /// <summary>
        /// در صورتی که این ضریب پر باشد آیتم ها ازین ضریب استفاده میکنند
        /// و در صورتی که برابر با نال یا صفر باشد آیتم ها از جدول ضریب بالاسری استفاده میکنند
        /// </summary>
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Zarib { get; set; }
    }
}
