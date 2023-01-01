// UserManager/UserManager/AddServicesHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using DAL;
using DAL.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UserManager.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {

        host.ConfigureServices(services =>
                               {
                                   services.AddTransient<IDataService<User>, UserService>();
                                   services.AddTransient<IDataService<Patient>, PatientService>();
                                   services.AddTransient<IDataService<UserPrivilege>, UserPrivilegeService>();
                                   services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
                               });

        return host;
    }
}