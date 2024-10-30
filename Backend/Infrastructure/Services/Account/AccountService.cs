using Database.Data;
using Database.Repositories;
using Infrastructure.Exceptions;
using Infrastructure.Services.Token;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AccountService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> CreateUserAsync(string username, string password)
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

            await _userRepository.SaveUserAsync(user);

            // TODO: Maybe throw exception is null is returned for the user

            return _tokenService.CreateToken(user);
        }

        public async Task<string> LogInAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username) ?? throw new UserDoesNotExistException($"The user [{username}] does not exist");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) throw new IncorrectPasswordException("The password entered does not match");
            }

            return _tokenService.CreateToken(user);
        }
    }
}
