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

    public string FirstName { get; set; }
    public string LastName { get; set; }
    // اطلاعات تکمیلی
    //در صورتی که شرکت باشد، شناسه ملی شرکت قرار میگیرد
    public string? NationalCode { get; set; }
    public string? Address { get; set; }

    [ForeignKey("Company")]
    public Guid? CompanyId { get; set;  }
    public clsCompany Company { get; set; }
    public string? PostalCode { get; set; }
}
