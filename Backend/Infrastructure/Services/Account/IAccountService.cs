namespace Infrastructure.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Takes in username and password and attempts to create a new app user
        /// A success user creation will return a JWT
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>JWT for the username passed in</returns>
        /// <exception cref="UserAlreadyExistsException"></exception>
        public Task<string> CreateUserAsync(string username, string password);

        /// <summary>
        /// Takes the username and password and attempts to log in as that user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>JWT for the user</returns>
        /// <exception cref="UserDoesNotExistException"></exception>
        /// <exception cref="IncorrectPasswordException"></exception>
        public Task<string> LogInAsync(string username, string password);
    }
}
