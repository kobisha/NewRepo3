using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Accounts> Accounts => Set<Accounts>();
    }
}
