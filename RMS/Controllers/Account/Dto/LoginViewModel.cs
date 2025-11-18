using System.ComponentModel.DataAnnotations;

namespace RMS.Controllers.Account.Dto;

public class LoginViewModel
{
    [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
