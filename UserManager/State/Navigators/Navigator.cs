// UserManager/UserManager/Navigator.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;
using UserManager.ViewModels;

namespace UserManager.State.Navigators;

public class Navigator
{
    private ViewModelBase _currentViewModel;

    public event Action StateChanged;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel?.Dispose();
            _currentViewModel = value;
            StateChanged?.Invoke();
        }
    }

}