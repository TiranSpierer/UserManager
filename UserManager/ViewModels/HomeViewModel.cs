// UserManager/UserManager/HomeViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DAL.Services.Wrapper;
using Domain.Models;
using Microsoft.AspNet.Identity;
using Prism.Commands;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class HomeViewModel : ViewModelBase
{
#region Privates

    private readonly DataServiceWrapper _dataService;
    private readonly INavigationService _navigationService;
    private          User?              _selectedUser;
    private          bool               _canExecuteEditCommand;
    private          bool               _canExecuteRemoveCommand;
    private IPasswordHasher _passwordHasher;

    #endregion

    #region Constructors

    public HomeViewModel(DataServiceWrapper dataService, INavigationService navigationService, IPasswordHasher passwordHasher)
    {
        _navigationService  = navigationService;
        _dataService        = dataService;
        _passwordHasher = passwordHasher;
        NavigateBackCommand = new DelegateCommand(ExecuteNavigateBack);
        EditUserCommand     = new DelegateCommand(ExecuteEditUser).ObservesCanExecute(() => CanExecuteEditCommand);
        RemoveUsersCommand  = new DelegateCommand(ExecuteRemoveUsers).ObservesCanExecute(() => CanExecuteRemoveCommand);
        AddUserCommand      = new DelegateCommand(ExecuteAddUser);

        Users               = new ObservableCollection<User>();
        _                   = InitTable();
    }



#endregion

    #region Public Properties
    
    public DelegateCommand            EditUserCommand     { get; }
    public DelegateCommand            RemoveUsersCommand  { get; }
    public DelegateCommand            AddUserCommand      { get; }
    public DelegateCommand            NavigateBackCommand { get; }
    public ObservableCollection<User> Users               { get; set; }

    public User? SelectedUser
    {
        get => _selectedUser;
        set
        {
            SetProperty(ref _selectedUser, value);
            CanExecuteRemoveCommand = CanExecuteEditCommand = value != null;
        }
    }

    public bool CanExecuteRemoveCommand
    {
        get => _canExecuteRemoveCommand;
        set => SetProperty(ref _canExecuteRemoveCommand, value);
    }

    public bool CanExecuteEditCommand
    {
        get => _canExecuteEditCommand;
        set => SetProperty(ref _canExecuteEditCommand, value);
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void ExecuteNavigateBack()
    {
        _navigationService.NavigateBack();
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

    private void ExecuteAddUser()
    {
        _navigationService.NavigateTo(new RegisterViewModel(_dataService, _navigationService, _passwordHasher));
    }

    private async void ExecuteRemoveUsers()
    {
        await _dataService.UserService.Delete(SelectedUser!.Id);
        await InitTable();
    }

    private void ExecuteEditUser()
    {
        _navigationService.NavigateTo(new EditUserViewModel(_dataService, _navigationService, username: SelectedUser!.Id, _passwordHasher));
    }

    #endregion
}