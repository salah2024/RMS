using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblOperation_ItemsFB")]
public class clsOperation_ItemsFB
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("Operation")]
    public long OperationId { get; set; }
    public clsOperation Operation { get; set; }

    public string ItemsFBShomareh { get; set; } 

    public string? NextOperation { get; set; }   

    public int Year { get; set; }
    public NoeFehrestBaha NoeFB { get; set; }
    //در صورت ترو بودن ریز متره های مربوط به این آیتم قابل ویرایش میباشند
    public bool? EnableEditing { get; set;  }
    //در صورت ترو بودن ریز متره های مربوط به این آیتم قابل حذف میباشند
    public bool? EnableDeleting { get; set;  }
}
