// UserManager/UserManager/AddViewModelsHostBuilderExtensions.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManager.ViewModels;

namespace UserManager.HostBuilders;

public static class AddViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
                               {
                                   services.AddSingleton<MainWindow>();

                                   //services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                                   //services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
                                   //services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));

                                   //services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();

                                   //services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                                   //services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                                   //services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
                               });

        return host;
    }

    //private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
    //{
    //    return new HomeViewModel(
    //                             );
    //}

    //private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
    //{
    //    return new LoginViewModel(
    //                              services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
    //                              services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
    //}

    //private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
    //{
    //    return new RegisterViewModel(
    //                                 services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
    //                                 services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
    //}
}