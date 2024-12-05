using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Second.Cojali.Api.Contracts.Configuration;
using Second.Cojali.Domain.Ports;
using Second.Cojali.Infrastructure.Adapters;
using Second.Cojali.Infrastructure.Data;
using Second.Cojali.Infrastructure.Utilities;

namespace Second.Cojali.Infrastructure.Extensions;

public static class RepositoryConfigurationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository>(provider =>
        {
            // Retrieve AppSettings
            var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;

            if (appSettings.UseDatabase)
            {
                // Ensure DbContext is registered
                var dbContext = provider.GetRequiredService<AppDbContext>();
                return new MySqlUserRepository(dbContext);
            }
            else
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Resolve the absolute path to the JSON file
                var absolutePath = PathResolver.ResolveAndValidatePath(
                    currentDirectory,
                    appSettings.UserJsonFilePath,
                    "The User JSON file was not found."
                );

                return new FileUserRepository(absolutePath);
            }
        });

        // Conditionally register AppDbContext
        services.AddDbContextIfEnabled();

        return services;
    }

    private static IServiceCollection AddDbContextIfEnabled(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;

        if (appSettings.UseDatabase)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(appSettings.ConnectionString, ServerVersion.AutoDetect(appSettings.ConnectionString));
            });
        }

        return services;
    }
}
