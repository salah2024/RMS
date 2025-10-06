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
        /// <summary>
        /// 1- خاکبرداری
        /// 2- پی کنی
        /// 3- کانال کنی
        /// </summary>
        public int Type { get; set; }
    }
}
