using Database.Data;
using Database.Exceptions;
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

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) throw new UserDoesNotExistException($"Could not find the user with the ID [{id}]");

            return user;
        }

        public async Task<AppUser?> GetUserByUsernameAsync(string username)
        {
            var trimmedUsername = username?.Trim();

            if (string.IsNullOrWhiteSpace(trimmedUsername)) throw new InvalidInputException(nameof(username));

            return await _context.Users.SingleOrDefaultAsync(user => user.UserName.ToLower() == trimmedUsername.ToLower());
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> SaveUserAsync(AppUser user)
        {
            var existingUser = await GetUserByUsernameAsync(user.UserName);

            if (existingUser != null) throw new UserAlreadyExistsException($"User already exists with the username [{user.UserName}]");

            user.UserName = user.UserName.Trim();

            var newUser = _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return newUser.Entity;
        }
    }
}
