// UserManager/UserManager/ViewModelBase.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using System;
using Prism.Mvvm;

namespace UserManager.ViewModels;

public delegate TViewModel CreateViewModel<out TViewModel>() where TViewModel : ViewModelBase;

public abstract class ViewModelBase : BindableBase, IDisposable
{
    public virtual void Dispose()
    {

    }
}