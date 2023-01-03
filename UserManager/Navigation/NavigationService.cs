// UserManager/UserManager/NavigationService.cs
// Created by Tiran Spierer
// Created at 02/01/2023
// Class propose:

using System;
using System.Collections.Generic;
using EventAggregator;
using Prism.Events;
using UserManager.ViewModels;

namespace UserManager.Navigation;

public class NavigationService : INavigationService
{
    private readonly IEventAggregator     _ea;
    private          ViewModelBase?       _currentViewModel;
    private readonly Stack<ViewModelBase> _previousViewModels;

    public NavigationService(IEventAggregator ea)
    {
        _ea                 = ea;
        _previousViewModels = new Stack<ViewModelBase>();
    }

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();
            _currentViewModel = value;
            _ea.GetEvent<NavigationChangedEvent>().Publish();
        }
    }

    public void NavigateTo(ViewModelBase viewModel)
    {
        _previousViewModels.Push(CurrentViewModel!);
        CurrentViewModel = viewModel;
    }

    public void NavigateBack()
    {
        if (_previousViewModels.Count > 0)
        {
            CurrentViewModel = _previousViewModels.Pop();
        }
    }
}