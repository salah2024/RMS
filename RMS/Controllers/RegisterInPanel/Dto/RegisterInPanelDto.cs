using System.ComponentModel.DataAnnotations;

public class RegisterInPanelDto
{
    [Required(ErrorMessage = "نام الزامی است.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "نام خانوادگی الزامی است.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "انتخاب نقش الزامی است.")]
    public string RoleId { get; set; }
    [Required(ErrorMessage = "انتخاب شرکت الزامی است")]
    public Guid? CompanyId { get; set; }

    [Required(ErrorMessage = "شناسه ملی الزامی است.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "شماره ملی باید 10 رقم باشد.")]
    public string NationalCode { get; set; }

    [Required(ErrorMessage = "شماره همراه الزامی است.")]
    [Phone(ErrorMessage = "شماره همراه نامعتبر است.")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره همراه باید با 09 شروع شود و 11 رقم باشد.")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "آدرس الزامی است.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "کد پستی الزامی است.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "کد پستی باید 10 رقم باشد.")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "نام کاربری الزامی است.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "رمز عبور الزامی است.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "تکرار رمز عبور الزامی است.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
    public string ConfirmPassword { get; set; }

}
