// UserManager/UserManager/LoginViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

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

    private string _errorMessage;
    private string _password;
    private string _username;
    private bool   _isLoggedIn;

    #endregion

    #region Constructors

    public LoginViewModel(IDataService<User> userService, INavigationService navigationService)
    {
        _userService       = userService;
        _navigationService = navigationService;
        LoginCommand       = new DelegateCommand(OnLoginAsync);
        Password           = string.Empty;
    }

#endregion

#region Public Properties

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set => SetProperty(ref _isLoggedIn, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public DelegateCommand LoginCommand { get; }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    #endregion

    #region Private Methods

    private async void OnLoginAsync()
    {
        var user = await _userService.GetById(Username);

        if (user == null || user.Password != Password)
        {
            // Display an error message if the user doesn't exist or the password is incorrect.
            // You can use the INotifyPropertyChanged interface to notify the view when the error message should be displayed.
            ErrorMessage = "Invalid username or password.";
            return;
        }

        IsLoggedIn = true;
        _navigationService.NavigateTo(new RegisterViewModel());
    }

    #endregion
}