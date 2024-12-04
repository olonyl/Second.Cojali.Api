using Second.Cojali.Api.Contracts.Configuration;
using Second.Cojali.Api.IoC.Mappings;
using Second.Cojali.Application.Interfaces;
using Second.Cojali.Application.Services;
using Second.Cojali.Domain.Ports;
using Second.Cojali.Infrastructure.Adapters;
using Second.Cojali.Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Second.Cojali.Api.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDependencies(this IServiceCollection services)
    {
        // Register Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();

        // Register Repositories
        services.AddScoped<IUserRepository>(provider =>
        {
            var configuration = provider.GetRequiredService<IOptions<AppSettings>>().Value;
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Relative path to navigate from the executing directory to the Infrastructure directory
            var absolutePath = PathResolver.ResolveAndValidatePath(
                currentDirectory,
                configuration.UserJsonFilePath,
                "The User JSON file was not found."
            );

            return new UserRepository(absolutePath);
        });

        // Register AutoMapper profiles
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ApiMappingProfile>();
            cfg.AddProfile<ApplicationMappingProfile>();
        });

        return services;
    }
}
