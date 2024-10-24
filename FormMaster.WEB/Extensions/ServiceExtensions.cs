﻿using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FormMaster.BLL.Services;
using FormMaster.BLL.Services.Contracts;
using FormMaster.BLL.Services.Implementations;
using FormMaster.DAL.DataContext;
using FormMaster.DAL.DataContext.Seeds;
using FormMaster.DAL.DataContext.Triggers;
using FormMaster.DAL.Entities;
using FormMaster.DAL.Repositories;
using FormMaster.DAL.Repositories.Contracts;
using FormMaster.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.WEB.Extensions;

public static class ServiceExtensions
{
    public static void AddFormMasterDbContext(this IServiceCollection services, 
        string? connectionStringName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringName);

        //var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        //var connectionString = configuration!.GetConnectionString(connectionStringName);
        var connectionString = Environment.GetEnvironmentVariable("POSTGRES_DB_CONNECTION");

        services.AddDbContext<FormMasterDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseTriggers(options =>
            {
                options.AddTrigger<UpdateTemplateCountBeforeSolveFormTrigger>();
            });
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

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IFormRepository, FormRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IFormService, FormService>();
    }

    public static void AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<IIdentityDataSeeder, IdentityDataSeeder>();
    }

    public static void AddElasticSearch(this IServiceCollection service)
    {
        var apiKey = Environment.GetEnvironmentVariable("ELASTIC_API_KEY");
        var url = Environment.GetEnvironmentVariable("ELASTIC_URL");
        var settings =
            new ElasticsearchClientSettings(
                    new Uri(url))
                .DefaultIndex("template")
                .Authentication(new ApiKey(apiKey))
                .EnableDebugMode()
                .PrettyJson()
                .DisableDirectStreaming();

        var client = new ElasticsearchClient(settings);

        service.AddSingleton<ITemplateSearchService>(new TemplateSearchService(client));
    }
}
