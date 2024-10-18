using FormMaster.BLL.Services;
using FormMaster.DAL.DataContext;
using FormMaster.DAL.DataContext.Seeds;
using FormMaster.DAL.Entities;
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
        services.AddIdentity<User, Role>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
        })
            .AddEntityFrameworkStores<FormMasterDbContext>();
    }

    public static void ConfigureApplicationCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/Login";
        });
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BLL.AssemblyReference).Assembly);
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAdminService, AdminService>();
    }

    public static void AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<IIdentityDataSeeder, IdentityDataSeeder>();
    }
}
