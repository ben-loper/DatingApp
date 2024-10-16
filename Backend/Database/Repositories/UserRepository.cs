using Database.Data;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;    
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
