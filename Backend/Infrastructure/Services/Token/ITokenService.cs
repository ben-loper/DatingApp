using Database.Data;

namespace Infrastructure.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
