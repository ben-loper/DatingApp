using Database.Repositories;
using Infrastructure.Services.Account;
using Infrastructure.Services.Token;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API.Utils
{
    public static class AppServiceExtensions
    {
        public static bool IsLocal(this WebApplication app)
        {
            return app?.Environment.EnvironmentName == "Local";
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
