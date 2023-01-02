// UserManager/UserManager/AddViewModelsHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using System;
using DAL.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using UserManager.Navigation;
using UserManager.ViewModels;

namespace UserManager.HostBuilders;

public static class AddViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
                               {
                                   services.AddTransient<MainViewModel>();
                                   services.AddTransient<RegisterViewModel>();
                                   services.AddTransient<HomeViewModel>();
                                   services.AddTransient<LoginViewModel>();
                                   services.AddSingleton<CreateViewModel<RegisterViewModel>>(s => () => CreateRegisterViewModel(s));
                                   services.AddSingleton<CreateViewModel<HomeViewModel>>(s => () => CreateHomeViewModel(s));
                                   services.AddSingleton<CreateViewModel<LoginViewModel>>(s => () => CreateLoginViewModel(s));
                               });

        return host;
    }

    private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
    {
        return new RegisterViewModel(
                                     services.GetRequiredService<IDataService<User>>(),
                                     services.GetRequiredService<NavigationService<LoginViewModel>>());
    }

    private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
    {
        return new LoginViewModel(
                                 services.GetRequiredService<IDataService<User>>(),
                                 services.GetRequiredService<NavigationService<RegisterViewModel>>());
    }

    private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
    {
        return new HomeViewModel(services.GetRequiredService<NavigationService<LoginViewModel>>());
    }
}