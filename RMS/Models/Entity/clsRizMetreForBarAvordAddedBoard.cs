using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    /// <summary>
    /// ریز متره هایی که برای تابلوهای اضافه شده درج شده اند
    /// </summary>
    [Table("tblRizMetreForBarAvordAddedBoard")]
    public class clsRizMetreForBarAvordAddedBoard:clsBaseEntity
    {
        [ForeignKey("BarAvordAddedBoard")]
        public Guid BarAvordAddedBoardId { get; set; }
        public clsBarAvordAddedBoard BarAvordAddedBoard { get; set; }
        [ForeignKey("RizMetreUsers")]
        public Guid RizMetreId { get; set; }
        public clsRizMetreUsers RizMetreUsers { get; set; }
    }
}
