using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    /// <summary>
    /// آیتمی که دارای آیتم های مرتبط باشد
    /// به وسیله این جدول مشخص میگردد که در کدام سطح آیتم مرتبط بهش نمایش داده شود، ممکن است آیتمی در سطح اول آیتم مرتبط داشته باشد ولی در سطح دوم آیتم مرتبط نداشته باشد
    /// </summary>
    [Table("tblOperationHasAddedOperationsLevelNumber")]
    public class clsOperationHasAddedOperationsLevelNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("OperationHasAddedOperations")]
        public long OperationHasAddedOperationsId { get; set; }
        public clsOperationHasAddedOperations OperationHasAddedOperations { get; set; }
        public int LevelNumber { get; set; }
    }
}
