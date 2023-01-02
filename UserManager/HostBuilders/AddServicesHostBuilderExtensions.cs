// UserManager/UserManager/AddServicesHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using System;
using System.Windows.Navigation;
using DAL;
using DAL.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using UserManager.Navigation;
using UserManager.ViewModels;

namespace UserManager.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {

        host.ConfigureServices(services =>
                               {
                                   services.AddSingleton<IDataService<User>, UserService>();
                                   services.AddSingleton<IDataService<Patient>, PatientService>();
                                   services.AddSingleton<IDataService<UserPrivilege>, UserPrivilegeService>();
                                   services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();

                                   services.AddSingleton<Navigator>();
                                   services.AddSingleton(CreateLoginNavigationService);
                                   //services.AddSingleton(CreateRegisterNavigationService);
                                   //services.AddSingleton(CreateHomeNavigationService);

                                   services.AddSingleton<IEventAggregator, Prism.Events.EventAggregator>();
                               });

        return host;
    }

    private static INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<HomeViewModel>(serviceProvider.GetRequiredService<Navigator>(), serviceProvider.GetRequiredService<HomeViewModel>);
    }

    private static INavigationService CreateRegisterNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<RegisterViewModel>(serviceProvider.GetRequiredService<Navigator>(), serviceProvider.GetRequiredService<RegisterViewModel>);
    }

    private static INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
    {
        return new NavigationService<LoginViewModel>(serviceProvider.GetRequiredService<Navigator>(), serviceProvider.GetRequiredService<LoginViewModel>);
    }
}