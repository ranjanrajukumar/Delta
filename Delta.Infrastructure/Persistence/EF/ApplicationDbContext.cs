using Delta.Domain.Common;
using Delta.Domain.Entities.Student;
using Delta.Domain.Entities.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Delta.Infrastructure.Persistence.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menu { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<MenuFormRight> MenuFormRights { get; set; }

        // 🔥 AUDIT + SOFT DELETE HANDLING
        public override int SaveChanges()
        {
            ApplyAuditRules();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            ApplyAuditRules();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditRules()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AddOnDt = DateTime.UtcNow;
                        entry.Entity.DelStatus = 0;
                        entry.Entity.AuthAdd ??= "SYSTEM";
                        break;

                    case EntityState.Modified:
                        entry.Entity.EditOnDt = DateTime.UtcNow;
                        entry.Entity.AuthLstEdit ??= "SYSTEM";
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; // soft delete
                        entry.Entity.DelStatus = 0;
                        entry.Entity.DelOnDt = DateTime.UtcNow;
                        entry.Entity.AuthDel ??= "SYSTEM";
                        break;
                }
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
        //        {
        //            var method = typeof(ApplicationDbContext)
        //                .GetMethod(nameof(SetSoftDeleteFilter),
        //                    System.Reflection.BindingFlags.NonPublic |
        //                    System.Reflection.BindingFlags.Static)
        //                ?.MakeGenericMethod(entityType.ClrType);

        //            method?.Invoke(null, new object[] { modelBuilder });
        //        }
        //    }
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Decimal precision fix
            modelBuilder.Entity<Student>()
                .Property(x => x.Income)
                .HasPrecision(18, 2);

            // Soft delete filter
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(ApplicationDbContext)
                        .GetMethod(nameof(SetSoftDeleteFilter),
                            System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Static)
                        ?.MakeGenericMethod(entityType.ClrType);

                    method?.Invoke(null, new object[] { modelBuilder });
                }
            }
        }

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
      where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>()
                .HasQueryFilter(e => e.DelStatus == 0);
        }


    }
}
