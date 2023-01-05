// UserManager/UserManager/AddServicesHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using System;
using DAL.Services.ConcreteServices;
using DAL.Services.Interfaces;
using DAL.Services.Wrapper;
using DAL.Setup;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using UserManager.Navigation;

namespace UserManager.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {

        host.ConfigureServices(services =>
                               {
                                   services.AddSingleton<IEventAggregator, Prism.Events.EventAggregator>();

                                   services.AddSingleton<IDataService<User>, UserService>();
                                   services.AddSingleton<IDataService<Patient>, PatientService>();
                                   services.AddSingleton<IDataService<UserPrivilege>, UserPrivilegeService>();
                                   services.AddSingleton<IDataService<Registration>, RegistrationService>();
                                   services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
                                   services.AddSingleton(CreateDataServiceWrapper);

                                   services.AddSingleton<INavigationService, NavigationService>();

                               });
        return host;
    }

    private static DataServiceWrapper CreateDataServiceWrapper(IServiceProvider service)
    {
        return DataServiceWrapper.Instance(
                                           service.GetRequiredService<IDataService<User>>(), 
                                           service.GetRequiredService<IDataService<Patient>>(), 
                                           service.GetRequiredService<IDataService<UserPrivilege>>(),
                                           service.GetRequiredService<IDataService<Registration>>());

    }
}