using System.ComponentModel.DataAnnotations;

namespace RMS.Models.Account.Dto;

public class RegisterInPanelDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FullName { get; set; }

    [Required]
    [Phone]
    public string Mobile { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string NationalCode { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Role { get; set; }   // نقش انتخابی کاربر
}
