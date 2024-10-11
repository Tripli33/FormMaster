using FormMaster.DAL.DataContext;
using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;
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

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
        });
    }
}
