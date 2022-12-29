// UserManager/UserManager/LoginViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;
using System.Threading.Tasks;
using DAL.Services;
using Domain.Models;
using Prism.Commands;

namespace UserManager.ViewModels;

public class LoginViewModel : ViewModelBase
{
#region Privates

    private readonly IDataService<User> _userService;

    private string _errorMessage;
    private bool   _isLoggedIn;
    private string _password;
    private string _username;

#endregion

#region Constructors

    public LoginViewModel(IDataService<User> userService)
    {
        _userService = userService;
        LoginCommand      = new DelegateCommand(OnLoginAsync);
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

        if (user == null || user.Password == Password)
        {
            // Display an error message if the user doesn't exist or the password is incorrect.
            // You can use the INotifyPropertyChanged interface to notify the view when the error message should be displayed.
            ErrorMessage = "Invalid username or password.";
            return;
        }

        // If the user exists and the password is correct, log in the user.
        // You can use the INotifyPropertyChanged interface to notify the view when the user is logged in.
        IsLoggedIn = true;
        
    }

    #endregion
}