// UserManager/UserManager/HomeViewModel.cs
// Created by Tiran Spierer
// Created at 26/12/2022
// Class propose:

using Prism.Commands;
using UserManager.Navigation;

namespace UserManager.ViewModels;

public class HomeViewModel : ViewModelBase
{
#region Privates

    private readonly INavigationService _navigationService;

#endregion

    #region Constructors

    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }



#endregion

    #region Public Properties
    


    #endregion

    #region Public Methods



    #endregion

    #region Private Methods


    #endregion
}