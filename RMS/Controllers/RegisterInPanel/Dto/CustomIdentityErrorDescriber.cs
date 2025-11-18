using Microsoft.AspNetCore.Identity;
namespace RMS.Controllers.RegisterInPanel.Dto;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "رمز عبور باید حداقل یک کاراکتر خاص (مانند ! @ # $ %) داشته باشد."
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "رمز عبور باید حداقل یک حرف کوچک انگلیسی (a-z) داشته باشد."
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "رمز عبور باید حداقل یک حرف بزرگ انگلیسی (A-Z) داشته باشد."
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"رمز عبور باید حداقل {length} کاراکتر باشد."
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "رمز عبور باید حداقل شامل یک عدد باشد."
        };
    }

    public override IdentityError DuplicateUserName(string UserName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = "نام کاربری تکراری میباشد"
        };
    }
}
