// UserManager/UserManager/MainViewModel.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using EventAggregator;
using Prism.Events;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class MainViewModel : ViewModelBase
{
#region Privates
    
    private readonly INavigationService _navigationService;
    private readonly IEventAggregator  _ea;
    private          ViewModelBase?    _currentViewModel;
    private readonly SubscriptionToken _subscriptionToken;

#endregion

#region Constructors

    public MainViewModel(INavigationService navigationService, IEventAggregator ea)
    {
        _navigationService = navigationService;
        CurrentViewModel   = _navigationService.CurrentViewModel;
        _ea                = ea;
        _subscriptionToken = _ea.GetEvent<NavigationChangedEvent>().Subscribe(OnNavigationChanged);
    }

#endregion

#region Public Properties

    public ViewModelBase? CurrentViewModel
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
        CurrentViewModel = _navigationService.CurrentViewModel!;
    }

#endregion

#region Overrides of ViewModelBase

    public override void Dispose()
    {
        _ea.GetEvent<NavigationChangedEvent>().Unsubscribe(_subscriptionToken);
    }

#endregion

}