using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblUserDetail")]
public class clsUserDetail
{
    [Key]
    public long ID { get; set; }

    // کلید خارجی به جدول AspNetUsers
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    // اطلاعات تکمیلی
    //در صورتی که شرکت باشد، شناسه ملی شرکت قرار میگیرد
    public string? NationalCode { get; set; }
    public string? Address { get; set; }
    public string? CEOName { get; set; }   // نام مدیرعامل
    public string? CompanyName { get; set; }
    /// <summary>
    /// نوع کاربر
    /// 1=حقیقی
    /// 2=حقوقی
    /// </summary>
    public UserType userType { get; set; }
    public string? PostalCode { get; set; }
}
