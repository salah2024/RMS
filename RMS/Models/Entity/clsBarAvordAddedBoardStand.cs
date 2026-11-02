using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity
{
    /// <summary>
    /// پایه تابلو های درج شده برای برآورد جاری
    /// </summary>

    [Table("tblBarAvordAddedBoardStand")]
    public class clsBarAvordAddedBoardStand:clsBaseEntity
    {
        [ForeignKey("BaravordUser")]
        public Guid BarAvordId { get; set; }
        public clsBaravordUser BaravordUser { get; set; }
        public NoeFehrestBaha NoeFBId { get; set;  }
        [ForeignKey("boardStandItems")]
        public Guid BoardStandItemId { get; set; }
        public clsBoardStandItems boardStandItems { get; set; }
        public int Tedad { get; set; }
    }
}
