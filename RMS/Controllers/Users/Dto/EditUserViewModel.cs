namespace RMS.Controllers.Users.Dto;

public class EditUserViewModel
{
    public string Id { get; set; }

    public string UserName { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }

    public string Role { get; set; }

    public Guid? CompanyId { get; set; }
}
