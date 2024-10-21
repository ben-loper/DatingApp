using Database.Data;

namespace Infrastructure.Services.Account
{
    public interface IAccountService
    {
        public Task<AppUser> CreateUserAsync(string username, string password);
        public Task<string> LogInAsync(string username, string password);
    }
}
