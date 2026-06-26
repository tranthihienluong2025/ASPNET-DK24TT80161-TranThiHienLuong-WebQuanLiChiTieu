using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuanLyChiTieu.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task EnsureSeedDataAsync(this ApplicationDbContext context)
        {
            // Check if database is empty and seed if needed
            if (!await context.DanhMucs.AnyAsync())
            {
                // Run migrations and seed data
                await context.Database.MigrateAsync();
            }
        }

        public static void DetachAllEntities(this ApplicationDbContext context)
        {
            var changedEntriesCopy = context.ChangeTracker.Entries()
                .ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}