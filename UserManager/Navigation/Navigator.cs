// UserManager/UserManager/NavigationStore.cs
// Created by Tiran Spierer
// Created at 02/01/2023
// Class propose:

using EventAggregator;
using Prism.Events;
using UserManager.ViewModels;

namespace UserManager.Navigation;

public class Navigator
{
    #region Privates

    private readonly IEventAggregator _ea;
    private          ViewModelBase?   _currentViewModel;

    #endregion

    #region Constructors

    public Navigator(IEventAggregator ea)
    {
        _ea = ea;
    }

    #endregion

    #region Public Properties

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

    #endregion

}