// UserManager/UserManager/AddDbContextHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using System;
using System.IO;
using DALTemp.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UserManager.HostBuilders;

public static class AddDbContextHostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
                                   var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                                   Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite($"Data Source={connectionString}");
                                   Directory.CreateDirectory(Path.GetDirectoryName(connectionString)!);

                                   services.AddDbContext<DataBaseContext>(configureDbContext);
                                   services.AddSingleton(new DataBaseContextFactory(configureDbContext));
                               });

        return host;
    }
}