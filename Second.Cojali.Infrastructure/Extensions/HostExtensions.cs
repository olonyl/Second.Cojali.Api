using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Second.Cojali.Api.Contracts.Configuration;
using Second.Cojali.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Cojali.Infrastructure.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> MigrateDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var appSettings = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>().Value;

        if (appSettings.UseDatabase)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync(); // Apply pending migrations
        }

        return host;
    }
}