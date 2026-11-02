using System.ComponentModel.DataAnnotations;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Company.Dto;

public class RegisterCompanyInPanelDto
{
    [Required(ErrorMessage = "انتخاب نوع شرکت الزامی است.")]
    public CompanyType CompanyType { get; set; }
    [Required(ErrorMessage = "نام شرکت الزامی است.")]
    public string? CompanyName { get; set; }

    [Required(ErrorMessage = "شناسه ملی الزامی است.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "شناسه ملی باید 11 رقم باشد.")]
    public string? CompanyNationalCode { get; set; }

    [Required(ErrorMessage = "شماره تلفن الزامی است.")]
    [Phone(ErrorMessage = "شماره تلفن نامعتبر است.")]
    [RegularExpression(@"^0\d{10}$", ErrorMessage = "شماره تلفن باید 11 رقم باشد.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "شماره موبایل الزامی است.")]
    [Phone(ErrorMessage = "شماره موبایل نامعتبر است.")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل باید 11 رقم باشد.")]
    public string MobileNumber { get; set; }

    public string? ManagementFirstName { get; set; }
    public string? ManagementLastName { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }

    [Phone(ErrorMessage = "شماره فاکس نامعتبر است.")]
    [RegularExpression(@"^0\d{10}$", ErrorMessage = "شماره فاکس باید 11 رقم باشد.")]
    public string? FaxNumber { get; set; }

    [EmailAddress(ErrorMessage = "آدرس ایمیل نامعتبر است.")]
    public string? Email { get; set; }
}
