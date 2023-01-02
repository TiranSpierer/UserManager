// UserManager/UserManager/MainViewModel.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using System.Windows.Input;
using EventAggregator;
using Prism.Events;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class MainViewModel : ViewModelBase
{
#region Privates

    private readonly IEventAggregator   _ea;
    private readonly INavigationService _navigationService;
    private          ViewModelBase      _currentViewModel;

#endregion

#region Constructors

    public MainViewModel(INavigationService navigationService, IEventAggregator ea, LoginViewModel currentViewModel)
    {
        _navigationService = navigationService;
        _ea                = ea;
        _currentViewModel  = currentViewModel;
        _ea.GetEvent<NavigationChangedEvent>().Subscribe(OnNavigationChanged);
    }

#endregion

#region Public Properties

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

#endregion

#region Public Methods



#endregion

#region Private Methods

    private void OnNavigationChanged()
    {
        CurrentViewModel = _navigationService.CurrentViewModel;
    }

#endregion
}