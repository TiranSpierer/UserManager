using System;
using System.IO;
using System.Windows;
using DAL;
using DAL.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UserManager;

public partial class App : Application
{
    private static   IHost?         _appHost;
    private readonly IConfiguration _configuration;

#region Constrctor

    public App()
    {
        _configuration = ConfigureBuilder();
        _appHost        = ConfigureServices();
    }

#endregion

#region Private Methods

    private IConfigurationRoot ConfigureBuilder()
    {
        return new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json", true, true)
              .Build();
    }

    private IHost ConfigureServices()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        Directory.CreateDirectory(Path.GetDirectoryName(connectionString)!);

        return Host.CreateDefaultBuilder()
                   .ConfigureServices((hostContext, services) =>
                                                             {
                                                                 services.AddSingleton<MainWindow>();

                                                                 services.AddDbContext<DataBaseContext>(options => options.UseSqlite($"Data Source={connectionString}"));
                                                                 services.AddTransient<IDataService<User>, UserService>();
                                                                 services.AddTransient<IDataService<Patient>, PatientService>();
                                                                 services.AddTransient<IDataService<UserPrivilege>, UserPrivilegeService>();
                                                                 services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
                                                             }).Build();
    }

#endregion

#region Overrides

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _appHost!.StartAsync();

        var initializer = _appHost.Services.GetRequiredService<IDatabaseInitializer>();
        initializer.Initialize();

        var startupForm = _appHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _appHost!.StopAsync();
        base.OnExit(e);
    }

#endregion
}