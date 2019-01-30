using Dingus.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Dingus.Server.Contexts
{
    public class DingusContext : DbContext
    {
        public DingusContext(DbContextOptions<DingusContext> options) 
            : base(options)
        {
        }

        public DingusContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DashboardItem> DashboardItems { get; set; }
    }
}
