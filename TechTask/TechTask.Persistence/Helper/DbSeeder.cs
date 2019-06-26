using Microsoft.EntityFrameworkCore;
using TechTask.Persistence.Context;

namespace TechTask.Persistence.Helper
{
    public class DbSeeder
    { 
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate(); 
        }
    }
}
