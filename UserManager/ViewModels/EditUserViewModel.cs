// UserManager/UserManager/EditUserViewModel.cs
// Created by Tiran Spierer
// Created at 03/01/2023
// Class propose:

using DALTemp.Services.Wrapper;
using Domain.Models;
using Prism.Commands;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class EditUserViewModel : ViewModelBase
{
    #region Privates

    private readonly DataServiceWrapper _dataService;
    private readonly INavigationService _navigationService;

    private readonly string  _originalUsername;
    private          string? _password;
    private          string? _username;
    private          string? _name;
    private          bool    _canExecuteSaveCommand;

    #endregion

    #region Constructors

    public EditUserViewModel(DataServiceWrapper dataService, INavigationService navigationService, string username)
    {
        _dataService       = dataService;
        _navigationService = navigationService;
        _originalUsername  = username;

        SaveCommand        = new DelegateCommand(ExecuteSaveCommandAsync).ObservesCanExecute(() => CanExecuteSaveCommand);
        GoBackCommand      = new DelegateCommand(ExecuteGoBackCommand);

        InitFields();
    }

    private async void InitFields()
    {
        var user = await _dataService.UserService.GetById(_originalUsername);

        if (user != null)
        {
            Username = user.Id;
            Name = user.Name;
        }
    }

#endregion

    #region Public Properties

    public DelegateCommand SaveCommand { get; }

    public DelegateCommand GoBackCommand { get; }

    public bool CanExecuteSaveCommand
    {
        get => _canExecuteSaveCommand;
        set => SetProperty(ref _canExecuteSaveCommand, value);
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
            CanExecuteSaveCommandAsync();
        }
    }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void ExecuteGoBackCommand()
    {
        _navigationService.NavigateTo(new HomeViewModel(_dataService, _navigationService));
    }

    private async void ExecuteSaveCommandAsync()
    {
        var user = new User()
        {
            Name = Name,
            Password = Password,
            Id = Username!
        };

        await _dataService.UserService.Update(_originalUsername, user);
        GoBackCommand.Execute();
    }

    private async void CanExecuteSaveCommandAsync()
    {
        if (string.IsNullOrEmpty(Username) == false)
        {
            if (Username != _originalUsername)
                CanExecuteSaveCommand = await _dataService.UserService.GetById(Username) == null;
            else
                CanExecuteSaveCommand = true;
        }
        else
            CanExecuteSaveCommand = false;
    }

    #endregion
}