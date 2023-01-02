// UserManager/UserManager/AddServicesHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using DAL;
using DAL.Services;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using UserManager.Navigation;
using UserManager.ViewModels;
using UserManager.Views;

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

                                   services.AddSingleton<INavigationService, NavigationService>();

                                   services.AddSingleton<IEventAggregator, Prism.Events.EventAggregator>();
                               });

        return host;
    }
}