using Database.Data;
using Microsoft.AspNetCore.Mvc;

namespace Database.Repositories
{
    public interface IUserRepository
    {
        public Task<List<AppUser>> GetUsersAsync();
        public Task<AppUser?> GetUserByIdAsync(int id);
    }
}