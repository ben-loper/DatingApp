using Database.Data;

namespace Infrastructure.Services
{
    public interface IAccountService
    {
        public Task<AppUser> CreateUser(string username, string password);
    }
}
