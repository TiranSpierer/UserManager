using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAL;
using DAL.Services;
using Domain.Models;
using UserManager.ViewModels;

namespace UserManager;

public partial class MainWindow : Window
{
    private readonly IDataService<User> _dataService;
    public MainWindow(IDataService<User> dataService)
    {
        InitializeComponent();
        _dataService = dataService;
        DataContext  = new Workspace();
    }


    private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
    {
        var user = new User()
                   {
                       Id       = IdTextBox.Text,
                       Name     = NameTextBox.Text,
                       Password = PasswordTextBox.Text
                   };
        _dataService.Create(user);

        Clean();
    }

    private void ButtonUpdate_OnClick(object sender, RoutedEventArgs e)
    {
        var user = new User()
                   {
                       Id       = IdTextBox.Text,
                       Name     = NameTextBox.Text,
                       Password = PasswordTextBox.Text
                   };
        _dataService.Update(user);

        Clean();
    }

    private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
    {
        var user = new User()
                   {
                       Id       = IdTextBox.Text,
                       Name     = NameTextBox.Text,
                       Password = PasswordTextBox.Text
                   };
        _dataService.Delete(user);

        Clean();
    }

    private void ButtonShowAll_OnClick(object sender, RoutedEventArgs e)
    {
        ShowAll();
    }

    private async void ShowAll()
    {
        var x = await _dataService.GetAll();
        foreach (var y in x)
        {

            IdTextBlock.Text       = y.Id;
            NameTextBlock.Text     = y.Name;
            PasswordTextBlock.Text = y.Password;
            await Task.Delay(1500);
        }
    }

    private void Clean()
    {
        IdTextBox.Text       = string.Empty;
        NameTextBox.Text     = string.Empty;
        PasswordTextBox.Text = string.Empty;
    }
}
