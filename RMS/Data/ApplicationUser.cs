using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
}
