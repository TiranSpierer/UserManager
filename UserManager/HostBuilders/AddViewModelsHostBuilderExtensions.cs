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
                                   services.AddTransient<LoginViewModel>();
                                   services.AddTransient<RegisterViewModel>();
                               });

        return host;
    }
}