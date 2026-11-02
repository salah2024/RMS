using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblTajmieFBItems")]
public class clsTajmieFBItems:clsBaseEntity
{
    [ForeignKey("TajmieFosoulItem")]
    public long TajmieFosoulItemId { get; set; }
    public clsTajmieFosoulItems TajmieFosoulItem { get; set; }
    public string ItemFBShomareh { get; set; }
    /// <summary>
    /// ممکن است در سال های مختلف آیتم های فهرست بهایی که به آیتم تجمیعی ست میشود متفاوت باشد
    /// </summary>
    public long Year { get;set;  }

}
