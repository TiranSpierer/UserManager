// UserManager/UserManager/AddViewsHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManager.ViewModels;

namespace UserManager.HostBuilders;

public static class AddViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
                               {
                                   services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                               });

        return host;
    }
}