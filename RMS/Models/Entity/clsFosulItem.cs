using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// در صورتی که این جدول برای فصلی پر شده باشد فصل فقط شامل این آیتم ها میباشد
/// و در صورتی که برای فصل آیتمی در نظر گرفته نشده باشد
/// فصل شامل همه آیتم ها میباشد
/// </summary>
[Table("tblFosoulItem")]
public class clsFosoulItem
{
    [Key]
    public long Id { get; set; }
    [ForeignKey("Fosoul")]
    public long FosoulId { get;set; }
    public clsFosoul Fosoul { get;set; }
    public string FBShomareh { get;set;}
}
