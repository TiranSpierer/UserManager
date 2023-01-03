using System;
using System.Windows;
using DAL.Setup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManager.HostBuilders;
using UserManager.Navigation;
using UserManager.ViewModels;

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
                   .AddViews()
                   .Build();
    }

#endregion

#region Overrides

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _appHost!.StartAsync();

        _appHost.Services.GetRequiredService<IDatabaseInitializer>().Initialize();

        _appHost.Services.GetRequiredService<INavigationService>().NavigateTo(_appHost.Services.GetRequiredService<LoginViewModel>());

        _appHost.Services.GetRequiredService<MainWindow>().Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _appHost!.StopAsync();
        base.OnExit(e);
    }

#endregion
}