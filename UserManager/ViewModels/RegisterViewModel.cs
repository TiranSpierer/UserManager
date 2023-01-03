// UserManager/UserManager/RegisterViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using DAL.Services.Interfaces;
using DAL.Services.Wrapper;
using Domain.Models;
using Prism.Commands;
using System.Threading.Tasks;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class RegisterViewModel : ViewModelBase
{
#region Privates

    private readonly DataServiceWrapper _dataService;
    private readonly INavigationService _navigationService;

    private string? _password;
    private string? _username;
    private string? _name;
    private bool    z;
    private bool    _canExecuteRegisterCommand;

#endregion

#region Constructors

    public RegisterViewModel(DataServiceWrapper dataService, INavigationService navigationService)
    {
        _dataService       = dataService;
        _navigationService = navigationService;
        RegisterCommand    = new DelegateCommand(ExecuteRegisterCommandAsync).ObservesCanExecute(() => CanExecuteRegisterCommand);
        GoBackCommand      = new DelegateCommand(ExecuteGoBackCommand);
        Password           = string.Empty;
    }

#endregion

    #region Public Properties

    public DelegateCommand RegisterCommand { get; }

    public DelegateCommand GoBackCommand { get; }

    public bool CanExecuteRegisterCommand
    {
        get => _canExecuteRegisterCommand;
        set => SetProperty(ref _canExecuteRegisterCommand, value);
    }

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

    private void ExecuteGoBackCommand()
    {
        _navigationService.Navigate(new LoginViewModel(_dataService, _navigationService));
    }

    private async void ExecuteRegisterCommandAsync()
    {
        var user = new User()
                   {
                       Name     = Name,
                       Password = Password,
                       Id       = Username!
                   };

        await _dataService.UserService.Create(user);
        _navigationService.Navigate(new LoginViewModel(_dataService, _navigationService));
    }

    private async void CanExecuteRegisterCommandAsync()
    {
        if (string.IsNullOrEmpty(Username) == false)
        {
            CanExecuteRegisterCommand = await _dataService.UserService.GetById(Username) == null;
        }
        else
        {
            CanExecuteRegisterCommand = false;
        }
    }

    #endregion
}