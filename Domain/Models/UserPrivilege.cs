// UserManager/Domain/UserPrivilege.cs
// Created by Tiran Spierer
// Created at 27/12/2022
// Class propose:

namespace Domain.Models;

public class UserPrivilege
{

    #region Public Properties

    public string    UserId    { get; set; }
    public Privilege Privilege { get; set; }

    #endregion

}