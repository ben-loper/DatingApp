using Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Utils
{
    public static class ContextServiceExtensions
    {

        public static void AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DB Context
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
