using Database.Data;

namespace Database.Repositories
{
    public interface IUserRepository
    {
        public Task<List<AppUser>> GetUsersAsync();
    }
}