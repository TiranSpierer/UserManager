using System.Windows;
using DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManager.HostBuilders;

namespace UserManager;

public partial class App : Application
{
    private static   IHost?         _appHost;

#region Constrctor

    public App()
    {
        _appHost = CreateHost();
    }

#endregion

#region Private Methods

    private IHost CreateHost()
    {
        return Host.CreateDefaultBuilder()
                   .AddConfiguration()
                   .AddDbContext()
                   .AddServices()
                   .AddViewModels()
                   .Build();
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