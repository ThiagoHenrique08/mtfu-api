using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Infrastructure.Context;

namespace MoreThanFollowUp.API.Extensions
{
    public static class MigrationInitializerExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            Console.WriteLine("Applying migrations");
            using (var serviceScope = app.Services.CreateScope())
            {
                Console.WriteLine("Application...");
                var aplicationServiceDb = serviceScope.ServiceProvider
                                 .GetService<ApplicationDbContext>();
                aplicationServiceDb!.Database.Migrate();
            }
            Console.WriteLine("Done");
        }
    }
}
