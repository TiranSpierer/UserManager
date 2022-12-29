// UserManager/UserManager/INavigator.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;
using UserManager.ViewModels;

namespace UserManager.State.Navigators;

public enum ViewType
{
    Login,
    Home,
    Portfolio,
    Buy,
    Sell
}

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    event Action  StateChanged;
}