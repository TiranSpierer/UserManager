// UserManager/UserManager/NavigationService.cs
// Created by Tiran Spierer
// Created at 02/01/2023
// Class propose:

using System;
using System.Collections.Generic;
using UserManager.ViewModels;
using Prism.Events;
using EventAggregator;

namespace UserManager.Navigation;

public class NavigationService : INavigationService
{
    private readonly IEventAggregator      _ea;
    public           ViewModelBase?        CurrentViewModel  { get; set; }
    public           Stack<ViewModelBase?> PreviousViewModel { get; set; }

    public NavigationService(IEventAggregator ea)
    {
        PreviousViewModel = new Stack<ViewModelBase?>();
        _ea               = ea;
    }

    public void NavigateTo(ViewModelBase viewModel)
    {
        PreviousViewModel.Push(CurrentViewModel);
        CurrentViewModel = viewModel;
        _ea.GetEvent<NavigationChangedEvent>().Publish();
    }

    public void NavigateBack()
    {
        CurrentViewModel = PreviousViewModel.Pop();
        _ea.GetEvent<NavigationChangedEvent>().Publish();
    }
}