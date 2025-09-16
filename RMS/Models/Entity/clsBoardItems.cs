using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblBoardItems")]
    public class clsBoardItems : clsBaseEntity
    {
        /// <summary>
        /// آرایه ای از اشکال
        /// که به , از هم جدا میشوند
        /// 1- 8 ضلعی
        /// 2- دایره
        /// 3- مثلث
        /// 4- مربع
        /// 5- تابلو اطلاعاتی
        /// </summary>
        public string? Shape { get; set; }
        public int? Tip { get; set; }
        public int? Material { get; set; }
        public int? PrintType { get; set; }

        /// <summary>
        /// اگر pop بجای 
        /// EGP
        /// یا
        /// HIP
        /// استفاده شود آیتمهایی که درج میشوند متفاوت هستند
        /// </summary>
        public int? PrintTypeBase { get; set; }
        public decimal? Thikness { get; set; }
        /// <summary>
        /// نوع آیتم اضافه میشود
        /// ترو آیتم اصلی
        /// فالس آیتم اضافه
        /// </summary>
        public bool AddedItemType { get; set; }
        public string AddedItem { get; set; }
        public int Year { get; set; }

    }
}
