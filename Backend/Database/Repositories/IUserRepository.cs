using Database.Data;

namespace Database.Repositories
{
    public interface IUserRepository
    {
        public Task<List<AppUser>> GetUsersAsync();
        public Task<AppUser?> GetUserByIdAsync(int id);
        public Task<AppUser?> GetUserByUsernameAsync(string username);
        public Task<AppUser?> SaveUserAsync(AppUser user);
    }
}