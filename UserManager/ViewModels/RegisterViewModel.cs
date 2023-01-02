// UserManager/UserManager/RegisterViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using DAL.Services;
using Domain.Models;
using Prism.Commands;
using System.Threading.Tasks;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class RegisterViewModel : ViewModelBase
{
#region Privates

    private readonly IDataService<User> _userService;
    private readonly INavigationService _navigationService;

    private string? _password;
    private string? _username;
    private string? _name;

#endregion

#region Constructors

    public RegisterViewModel(IDataService<User> userService, NavigationService<LoginViewModel> navigationService)
    {
        _userService       = userService;
        _navigationService = navigationService;
        RegisterCommand    = new DelegateCommand(ExecuteRegisterCommandAsync).ObservesCanExecute(() => CanExecuteRegisterCommand);
        Password           = string.Empty;
    }



    #endregion

    #region Public Properties

    public DelegateCommand RegisterCommand           { get; }

    public bool            CanExecuteRegisterCommand { get; set; }

    public string? Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string? Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            CanExecuteRegisterCommandAsync(); 
        }
    }

#endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private async void ExecuteRegisterCommandAsync()
    {
        var user = new User()
                   {
                       Name     = Name,
                       Password = Password,
                       Id       = Username!
                   };

        await _userService.Create(user);
        _navigationService.Navigate();
    }

    private async void CanExecuteRegisterCommandAsync()
    {
        if (string.IsNullOrEmpty(Username) == false)
        {
            CanExecuteRegisterCommand = await _userService.GetById(Username) == null;
        }
        else
        {
            CanExecuteRegisterCommand = false;
        }
    }

    #endregion
}