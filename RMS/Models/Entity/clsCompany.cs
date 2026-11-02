using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblCompany")]
public class clsCompany:clsBaseEntity
{
    public string? CompanyNationalCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? Address { get; set; }
    public string? CompanyName { get; set; }
    public string? PostalCode { get; set; }
    public string? FaxNumber { get; set; }
    public string? Email { get; set; }
    public string? ManagementFirstName { get; set; }
    public string? ManagementLastName { get; set; }
    public CompanyType CompanyType { get; set; }
    public virtual ICollection<clsCompanyManagementHistory> lstCompanyManagementHistory { get; set;  }
}
