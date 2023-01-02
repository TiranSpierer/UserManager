// UserManager/UserManager/LoginViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;
using System.Threading.Tasks;
using DAL.Services;
using Domain.Models;
using Prism.Commands;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class LoginViewModel : ViewModelBase
{
#region Privates
    
    private readonly IDataService<User> _userService;
    private readonly INavigationService _navigationService;

    private string? _errorMessage;
    private string? _password;
    private string? _username;
    private bool    _isLoggedIn;
    private bool    _canExecuteLoginCommand;

#endregion

    #region Constructors

    public LoginViewModel(IDataService<User> userService, NavigationService<RegisterViewModel> navigationService)
    {
        _userService       = userService;
        _navigationService = navigationService;
        LoginCommand       = new DelegateCommand(ExecuteLoginCommandAsync).ObservesCanExecute(() => CanExecuteLoginCommand);
        RegisterCommand    = new DelegateCommand(ExecuteRegisterCommand);
        Password           = string.Empty;
    }

#endregion

    #region Public Properties

    public DelegateCommand LoginCommand    { get; }
    public DelegateCommand RegisterCommand { get; }

    public bool CanExecuteLoginCommand
    {
        get => _canExecuteLoginCommand;
        set => SetProperty(ref _canExecuteLoginCommand, value);
    }

    public string? ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set => SetProperty(ref _isLoggedIn, value);
    }

    public string? Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            Task.Run(CanExecuteLoginCommandAsync);
        }
    }

    public string? Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            Task.Run(CanExecuteLoginCommandAsync);
        }
    }

    #endregion

    #region Private Methods

    private void ExecuteLoginCommandAsync()
    {
        IsLoggedIn = true;
        _navigationService.Navigate();
    }

    private async Task CanExecuteLoginCommandAsync()
    {
        var user = await _userService.GetById(Username!);

        CanExecuteLoginCommand = user != null && user.Password == Password;
    }

    private void ExecuteRegisterCommand()
    {
        _navigationService.Navigate();
    }

    #endregion

}