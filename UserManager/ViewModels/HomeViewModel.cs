// UserManager/UserManager/HomeViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System.Diagnostics;
using System.Windows;
using DAL.Services.Wrapper;
using Prism.Commands;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class HomeViewModel : ViewModelBase
{
#region Privates

    private readonly DataServiceWrapper _dataService;
    private readonly INavigationService _navigationService;

#endregion

    #region Constructors

    public HomeViewModel(DataServiceWrapper dataService, INavigationService navigationService)
    {
        _navigationService  = navigationService;
        _dataService   = dataService;
        NavigateBackCommand = new DelegateCommand(ExecuteNavigateBack);
    }

#endregion

    #region Public Properties
    
    public DelegateCommand NavigateBackCommand { get; }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void ExecuteNavigateBack()
    {
        _navigationService.Navigate(new LoginViewModel(_dataService, _navigationService));
    }

    #endregion
}