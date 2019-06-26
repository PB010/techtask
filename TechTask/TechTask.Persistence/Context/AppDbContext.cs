using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskPriority> TaskPriorities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<LoggedActivity> LoggedActivities { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified))
            {
                entry.Property("UpdatedAt").CurrentValue = timestamp;

                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAt").CurrentValue = timestamp;
                
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified))
            {
                entry.Property("UpdatedAt").CurrentValue = timestamp;

                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAt").CurrentValue = timestamp;
            }

            return base.SaveChanges();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedAt");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("UpdatedAt");
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
