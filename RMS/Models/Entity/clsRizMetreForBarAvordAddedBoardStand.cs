using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    /// <summary>
    /// ریز متره هایی که برای پایه تابلوها اضافه شده درج شده اند
    /// </summary>

    [Table("tblRizMetreForBarAvordAddedBoardStand")]
    public class clsRizMetreForBarAvordAddedBoardStand:clsBaseEntity
    {
        [ForeignKey("BarAvordAddedBoardStand")]
        public Guid BarAvordAddedBoardStandId { get; set; }
        public clsBarAvordAddedBoardStand BarAvordAddedBoardStand { get; set; }
        [ForeignKey("RizMetreUsers")]
        public Guid RizMetreId { get; set; }
        public clsRizMetreUsers RizMetreUsers { get; set; }
    }
}
