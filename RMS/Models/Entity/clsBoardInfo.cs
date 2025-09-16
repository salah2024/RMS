using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblBoardInfo")]
    public class clsBoardInfo:clsBaseEntity
    {
        public int BoardType { get; set; }
        public decimal BoardSize { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Area { get; set; }
    }
}
