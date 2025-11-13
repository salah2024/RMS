namespace RMS.Services.JWT;

public interface ITokenService
{
    Task<string> CreateTokenAsync(ApplicationUser user);
}
