using Microsoft.EntityFrameworkCore;
using Delta.Domain.Entities.Utilities;

namespace Delta.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets here
        public DbSet<User> Users { get; set; }
    }
}
