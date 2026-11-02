using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblCompanyManagementHistory")]
public class clsCompanyManagementHistory:clsBaseEntity
{
    public string FirstName { get;set;  }
    public string LastName { get;set; }
    public string NationalCode { get;set; }
    public string PhoneNumber { get;set; }
    public string Address { get;set; }
    public string FromDate { get;set; }
    public string ToDate { get;set; }
    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }
    public clsCompany Company { get; set; }
}
