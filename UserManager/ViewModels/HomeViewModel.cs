// UserManager/UserManager/HomeViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using DAL.Services.Wrapper;
using Domain.Models;
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
        Users = new ObservableCollection<User>();
        _ = InitTable();
    }

#endregion

    #region Public Properties
    
    public DelegateCommand NavigateBackCommand { get; }
    public ObservableCollection<User> Users { get; set; }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void ExecuteNavigateBack()
    {
        _navigationService.Navigate(new LoginViewModel(_dataService, _navigationService));
    }

    private async Task InitTable()
    {
        Users.Clear();
        var freshUsers = await _dataService.UserService.GetAll();
        foreach (var user in freshUsers)
        {
            Users.Add(user);
        }
    }

    #endregion
}