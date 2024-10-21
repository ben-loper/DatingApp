using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}
