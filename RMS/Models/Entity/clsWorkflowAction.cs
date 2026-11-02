using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

/// <summary>
/// آیدی داده های این جدول با اینام زیر تطابق دارد
/// public enum ProccesType
/// </summary>
[Table("tblWorkflowAction")]
public class clsWorkflowAction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    /// <summary>
    /// عنوان عملیات
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Controller مقصد
    /// </summary>
    public string Controller { get; set; }

    /// <summary>
    /// Action مقصد
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// فعال
    /// </summary>
    public bool IsActive { get; set; }
}