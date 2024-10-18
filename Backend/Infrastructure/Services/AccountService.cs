using Database.Data;
using Database.Repositories;
using Infrastructure.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUser> CreateUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user != null) throw new UserAlreadyExistsException($"User already exists with the username [{username}]");

            using var hmac = new HMACSHA512();

            user = new AppUser()
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            return await _userRepository.SaveUserAsync(user);
        }
    }
}
