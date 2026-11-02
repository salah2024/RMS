using System.ComponentModel.DataAnnotations;

namespace RMS.Controllers.Users.Dto
{
    public class ResetPasswordAdminViewModel
    {
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
