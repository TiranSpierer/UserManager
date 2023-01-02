// UserManager/UserManager/NavigationService.cs
// Created by Tiran Spierer
// Created at 02/01/2023
// Class propose:

using System;
using UserManager.ViewModels;
using Prism.Events;
using EventAggregator;

namespace UserManager.Navigation;

public class NavigationService : INavigationService
{
    private readonly IEventAggregator _ea;
    public           ViewModelBase    CurrentViewModel { get; set; }

    public NavigationService(IEventAggregator ea)
    {
        _ea = ea;
    }

    public void NavigateTo(ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
        _ea.GetEvent<NavigationChangedEvent>().Publish();
    }

    private void Dispose()
    {
        CurrentViewModel.Dispose();
    }
}