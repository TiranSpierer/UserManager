// UserManager/UserManager/Workspace.cs
// Created by Tiran Spierer
// Created at 27/12/2022
// Class propose:

namespace UserManager.ViewModels;

public class Workspace
{
#region Privates



#endregion

#region Constructors

    public Workspace()
    {
        CurrentViewModel = new RegisterViewModel();
    }

    #endregion

    #region Public Properties

    public ViewModelBase CurrentViewModel { get; set; }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion
}