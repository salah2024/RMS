using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblSubItemsAddingToFB")]
public class clsSubItemsAddingToFB
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [ForeignKey("ItemsAddingToFB")]
    public long ItemsAddingToFBId { get; set; }   
    public clsItemsAddingToFB ItemsAddingToFB { get; set; }

    public string AddedItems { get; set; }          // شرح یا نام آیتم‌های اضافه‌شده

    public string Condition { get; set; }           // شرایط مرتبط با اضافه کردن این آیتم

    public string FinalWorking { get; set; }          // آیا این آیتم نهایی شده است؟

    public int ConditionType { get; set; }       // نوع شرایط (مثلاً: "ضروری", "اختیاری")

    public string DesOfAddingItems { get; set; }    // توضیحات اضافه کردن آیتم‌ها

    public string FieldsAdding { get; set; }
}
