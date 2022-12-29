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
    public static    IHost?         AppHost { get; private set; }
    private readonly IConfiguration _configuration;

#region Constrctor

    public App()
    {
        _configuration = ConfigureBuilder();
        AppHost        = ConfigureServices();
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
                                                                 services.AddDbContext<DataBaseContext>(options => options.UseSqlite($"Data Source={connectionString}"));
                                                                 services.AddSingleton<MainWindow>();
                                                                 services.AddDbContext<DataBaseContext>();
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
        await AppHost!.StartAsync();

        var initializer = AppHost.Services.GetRequiredService<IDatabaseInitializer>();
        initializer.Initialize();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }

#endregion
}