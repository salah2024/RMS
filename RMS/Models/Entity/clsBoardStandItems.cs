using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblBoardStandItems")]
    public class clsBoardStandItems : clsBaseEntity
    {
        /// <summary>
        /// 1-پایه تابلو اطلاعاتی
        /// 2-پایه تابلو دایره ای مثلثی
        /// 3-پایه تابلو مسیر نما و بال کبوتری
        /// 4-پایه تابلو اطلاعاتی با سطح کوچک
        /// </summary>
        public int BoardStandType { get; set; }
        public int Tedad { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Tool { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Arz { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Ertefa { get;set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Zarib { get; set; }
        public string? AddedFBShomareh { get; set; }
        public string Sharh { get; set; }
        public int Year { get; set; }   
    }
}
