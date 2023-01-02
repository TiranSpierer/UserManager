// UserManager/UserManager/NavigationService.cs
// Created by Tiran Spierer
// Created at 02/01/2023
// Class propose:

using System;
using System.Collections.Generic;
using UserManager.ViewModels;

namespace UserManager.Navigation;

public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly Navigator  _navigator;
    private readonly Func<TViewModel> _createViewModel;

    public NavigationService(Navigator navigator, Func<TViewModel> createViewModel)
    {
        _navigator = navigator;
        _createViewModel = createViewModel;
    }

    public void Navigate()
    {
        _navigator.CurrentViewModel = _createViewModel();
    }
}