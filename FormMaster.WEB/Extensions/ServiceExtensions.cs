using FormMaster.DAL.DataContext;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.WEB.Extensions;

public static class ServiceExtensions
{
    public static void AddFormMasterDbContext(this IServiceCollection services, 
        string? connectionStringName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringName);

        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var connectionString = configuration!.GetConnectionString(connectionStringName);

        services.AddDbContext<FormMasterDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}
