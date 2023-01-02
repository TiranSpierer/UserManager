// UserManager/UserManager/INavigationService.cs
// Created by Tiran Spierer
// Created at 02/01/2023
// Class propose:

using System;
using UserManager.ViewModels;

namespace UserManager.Navigation;

public interface INavigationService
{
    ViewModelBase CurrentViewModel { get; set; }
    void          NavigateTo(ViewModelBase viewModel);
}