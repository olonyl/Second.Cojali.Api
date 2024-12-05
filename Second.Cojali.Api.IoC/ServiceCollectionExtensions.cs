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
using Second.Cojali.Infrastructure.Extensions;

namespace Second.Cojali.Api.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDependencies(this IServiceCollection services)
    {
        // Register Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();

        // Register Repositories using RepositoryFactory
        services.AddRepositories();

        // Register AutoMapper profiles
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
