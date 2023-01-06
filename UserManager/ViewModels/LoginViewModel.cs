// UserManager/UserManager/LoginViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System.Threading.Tasks;
using DAL.Services.Wrapper;
using Prism.Commands;
using UserManager.Navigation;
using DAL.Utilities;
using Microsoft.AspNet.Identity;

namespace UserManager.ViewModels;

public class LoginViewModel : ViewModelBase
{
#region Privates
    
    private readonly DataServiceWrapper _dataService;
    private readonly INavigationService _navigationService;
    private readonly IPasswordHasher _passwordHasher;
    private string? _errorMessage;
    private string? _password;
    private string? _username;
    private bool    _isLoggedIn;
    private bool    _canExecuteLoginCommand;

#endregion

    #region Constructors

    public LoginViewModel(DataServiceWrapper dataService, INavigationService navigationService, IPasswordHasher passwordHasher)
    {
        _dataService       = dataService;
        _navigationService = navigationService;
        _passwordHasher = passwordHasher;
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
        _navigationService.NavigateTo(new HomeViewModel(_dataService, _navigationService, _passwordHasher));
    }

    private async Task CanExecuteLoginCommandAsync()
    {
        var user = await _dataService.UserService.GetById(Username!);

        if(user != null)
        {
            if(string.IsNullOrEmpty(user.Password) && string.IsNullOrEmpty(Password))
            {
                CanExecuteLoginCommand = true;
            }
            else if (string.IsNullOrEmpty(user.Password) == false)
            {
                //CanExecuteLoginCommand = AesEncryption.Decrypt(user.Password) == Password;
                CanExecuteLoginCommand = _passwordHasher.VerifyHashedPassword(user.Password, Password) == PasswordVerificationResult.Success;
            }
        }
        else
        {
            CanExecuteLoginCommand = false;
        }
    }

    private void ExecuteRegisterCommand()
    {
        _navigationService.NavigateTo(new RegisterViewModel(_dataService, _navigationService, _passwordHasher));
    }

    #endregion

}