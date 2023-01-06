// UserManager/UserManager/RegisterViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using DAL.Services.Wrapper;
using Domain.Models;
using Prism.Commands;
using System.Threading.Tasks;
using UserManager.Navigation;
using DAL.Utilities;
using Microsoft.AspNet.Identity;

namespace UserManager.ViewModels;

public class RegisterViewModel : ViewModelBase
{
#region Privates

    private readonly DataServiceWrapper _dataService;
    private readonly INavigationService _navigationService;
    private readonly IPasswordHasher _passwordHasher;
    private string? _password;
    private string? _username;
    private string? _name;
    private bool    _canExecuteRegisterCommand;
    private bool    _isAddUsersSelected;
    private bool    _isDeleteUsersSelected;
    private bool    _isEditUsersSelected;

#endregion

#region Constructors

    public RegisterViewModel(DataServiceWrapper dataService, INavigationService navigationService, IPasswordHasher passwordHasher)
    {
        _dataService       = dataService;
        _navigationService = navigationService;
        _passwordHasher = passwordHasher;
        SelectedPrivileges = new HashSet<Privilege>();
        RegisterCommand    = new DelegateCommand(ExecuteRegisterCommandAsync).ObservesCanExecute(() => CanExecuteRegisterCommand);
        GoBackCommand      = new DelegateCommand(ExecuteGoBackCommand);
        Password           = string.Empty;
    }

    #endregion

    #region Public Properties


    public HashSet<Privilege> SelectedPrivileges { get; set; }

    public DelegateCommand                 RegisterCommand { get; }

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

    public bool IsAddUsersSelected
    {
        get => _isAddUsersSelected;
        set
        {
            SetProperty(ref _isAddUsersSelected, value);
            UpdatePrivilegesList(Privilege.AddUsers, value);
        }
    }

    public bool IsDeleteUsersSelected
    {
        get => _isDeleteUsersSelected;
        set
        {
            SetProperty(ref _isDeleteUsersSelected, value);
            UpdatePrivilegesList(Privilege.DeleteUsers, value);
        }
    }

    public bool IsEditUsersSelected
    {
        get => _isEditUsersSelected;
        set
        {
            SetProperty(ref _isEditUsersSelected, value);
            UpdatePrivilegesList(Privilege.EditUsers, value);
        }
    }

#endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void UpdatePrivilegesList(Privilege privilege, bool isSelected)
    {

        if (isSelected)
            SelectedPrivileges.Add(privilege);
        else
            SelectedPrivileges.Remove(privilege);
    }

    private void ExecuteGoBackCommand()
    {
        _navigationService.NavigateBack();
    }

    private async void ExecuteRegisterCommandAsync()
    {
        var user = new User()
                   {
                       Name     = Name,
                       //Password = AesEncryption.Encrypt(Password),
                       Password = _passwordHasher.HashPassword(Password),
                       Id       = Username!
                   };

        await _dataService.UserService.Create(user);
        await CreateUserPrivileges(user.Id);
        _navigationService.NavigateTo(new HomeViewModel(_dataService, _navigationService, _passwordHasher));
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

    public async Task CreateUserPrivileges(string userId)
    {
        foreach (var privilege in SelectedPrivileges)
        {
            await _dataService.UserPrivilegeService.Create(new UserPrivilege()
                                                           {
                                                               UserId    = userId,
                                                               Privilege = privilege
                                                           });
        }
    }

    #endregion
}