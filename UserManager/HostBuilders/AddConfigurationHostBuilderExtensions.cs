// UserManager/UserManager/AddConfigurationHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace UserManager.HostBuilders;

public static class AddConfigurationHostBuilderExtensions
{
    public static IHostBuilder AddConfiguration(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration(c =>
                                       {
                                           c.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                                           c.AddJsonFile("appsettings.json", true, true);
                                       });

        return host;
    }
}