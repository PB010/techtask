using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Logs;
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
        public DbSet<UpdateLog> UpdateLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
