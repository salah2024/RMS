using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    // نام کاربری
    [Required(ErrorMessage = "نام کاربری الزامی است")]
    [StringLength(100, ErrorMessage = "نام کاربری باید حداقل 3 کاراکتر باشد", MinimumLength = 3)]
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; }

    // رمز عبور
    [Required(ErrorMessage = "رمز عبور الزامی است")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد", MinimumLength = 6)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+={}\[\]|:;,.<>?/-]).{6,}$", ErrorMessage = "رمز عبور باید شامل حداقل یک حرف بزرگ، یک حرف کوچک، یک عدد و یک کاراکتر خاص باشد.")]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; }

    // تکرار رمز عبور
    [Required(ErrorMessage = "تکرار رمز عبور الزامی است")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن باید یکسان باشد")]
    [Display(Name = "تکرار رمز عبور")]
    public string ConfirmPassword { get; set; }
}
