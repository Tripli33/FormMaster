using FormMaster.DAL.DataContext.Seeds;
using FormMaster.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.WEB.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task MigrateDatabaseAndSeedDataAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<FormMasterDbContext>();
        var dataSeeder = services.GetRequiredService<IIdentityDataSeeder>();
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
            await dataSeeder.SeedRolesAsync();
            await dataSeeder.SeedAdminUserAsync();
        }
    }
}
