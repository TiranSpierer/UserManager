using System.Windows;
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

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        ButtonBase_OnClickAsync();
    }

    private async void ButtonBase_OnClickAsync()
    {
        var temp = await _dataService.GetById("Admin");
        TextBloock.Text = temp?.Name;

    }
}
