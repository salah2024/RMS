using Microsoft.AspNetCore.Identity;
using RMS.Models.Entity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    public clsUserDetail UserDetail { get; set; }
}
