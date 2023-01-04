using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DALTemp;
using DALTemp.Services;
using Domain.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using UserManager.ViewModels;
namespace UserManager;

public partial class MainWindow : Window
{
    //private readonly IDataService<User> _userDataService;
    //private readonly IDataService<UserPrivilege> _userPrivilegeDataService;

    //public MainWindow(IDataService<User> userDataService, IDataService<UserPrivilege> userPrivilegeDataService, object dataContext)
    //{
    //    InitializeComponent();
    //    _userDataService          = userDataService;
    //    _userPrivilegeDataService = userPrivilegeDataService;
    //    DataContext               = dataContext;
    //}

    public MainWindow(object dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }


    //private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
    //{
    //    var user = new User()
    //               {
    //                   Id       = IdTextBox.Text,
    //                   Name     = NameTextBox.Text,
    //                   Password = PasswordTextBox.Text
    //               };
    //    _userDataService.Create(user);

    //    foreach (var privilege in Enum.GetValues(typeof(Privilege)))
    //    {
    //        if ((Privilege) privilege != Privilege.Indelible && new Random().NextDouble() < 0.5)
    //        {
    //            _userPrivilegeDataService.Create(new UserPrivilege
    //                                             {
    //                                                 UserId    = IdTextBox.Text,
    //                                                 Privilege = (Privilege)privilege
    //                                             });
    //        }
    //    }

    //    Clean();
    //}

    //private void ButtonUpdate_OnClick(object sender, RoutedEventArgs e)
    //{
    //    var user = new User()
    //               {
    //                   Id       = IdTextBox.Text,
    //                   Name     = NameTextBox.Text,
    //                   Password = PasswordTextBox.Text
    //               };
    //    _userDataService.Update(IdTextBox.Text, user);

    //    Clean();
    //}

    //private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
    //{
    //    _userDataService.Delete(IdTextBox.Text);

    //    Clean();
    //}

    //private void ButtonShowAll_OnClick(object sender, RoutedEventArgs e)
    //{
    //    ShowAll();
    //}

    //private async void ShowAll()
    //{
    //    var x = await _userDataService.GetAll();
    //    foreach (var y in x)
    //    {

    //        IdTextBlock.Text       = y.Id;
    //        NameTextBlock.Text     = y.Name;
    //        PasswordTextBlock.Text = y.Password;
    //        await Task.Delay(1500);
    //    }
    //}

    //private void Clean()
    //{
    //    IdTextBox.Text       = string.Empty;
    //    NameTextBox.Text     = string.Empty;
    //    PasswordTextBox.Text = string.Empty;
    //}

    //private async void ButtonShowSpecific_OnClick(object sender, RoutedEventArgs e)
    //{
    //    var user = await _userDataService.GetById(IdTextBox.Text);

    //    IdTextBlock.Text       = user?.Id;
    //    NameTextBlock.Text     = user?.Name;
    //    PasswordTextBlock.Text = user?.Password;

    //    Clean();
    //}
}
