using Database.Data;

namespace Infrastructure.Services
{
    public interface IAccountService
    {
        public Task<AppUser> CreateUserAsync(string username, string password);
        public Task<bool> LogInAsync(string username, string password);
    }
}
