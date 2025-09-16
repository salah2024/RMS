using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    /// <summary>
    /// تابلو های درج شده برای برآورد جاری
    /// </summary>
    [Table("tblBarAvordAddedBoard")]
    public class clsBarAvordAddedBoard:clsBaseEntity
    {
        [ForeignKey("BaravordUser")]
        public Guid BarAvordId { get; set; }
        public clsBaravordUser BaravordUser { get; set; }
        /// <summary>
        /// نوع تابلو
        /// 1- 8 ضلعی
        /// 2- دایره
        /// 3- مثلث
        /// </summary>
        public int BoardType { get; set; }  
        /// <summary>
        /// اندازه تابلو
        /// </summary>
        public int? Size { get; set; }
        /// <summary>
        /// جنس ورق
        /// 1 روغنی
        /// 2 گالوانیزه
        /// </summary>
        public int? Material { get; set; }
        
        /// <summary>
        /// ضخامت ورق
        /// 1.25
        /// 1.5
        /// 2
        /// 3
        /// </summary>
        public decimal? Thickness { get; set; }
        /// <summary>
        /// تیپ تابلو
        /// 1-ساده
        /// 2-لبه دار
        /// 3- رخ دار
        /// </summary>
        public int? BoardTip { get; set; }
        /// <summary>
        /// نوع شبرنگ
        /// 1 EPG
        /// 2 HIP
        /// 3 POP
        /// 4 DIG
        /// </summary>
        public int? PrintType { get; set; }
        /// <summary>
        /// مقدار نوع چاپ
        /// بعضی نوع چاپ ها دارای مقدار میباشند
        /// </summary>
        public decimal? MeghdarPrintType { get; set; }
        /// <summary>
        /// نوع تابلو مربع 
        /// دارای ارتفاع و عرض میباشد
        /// </summary>
        public decimal? Ertefa { get; set; }
        public decimal? Arz { get; set; }

        public string? Sharh { get; set; }
        public long? Tedad { get; set; }
        public bool? UsePOP { get; set; }
        public decimal? PercentPrintPOP { get; set; }
    }
}
