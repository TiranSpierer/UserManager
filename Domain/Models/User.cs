// UserManager/Domain/User.cs
// Created by Tiran Spierer
// Created at 27/12/2022
// Class propose:

using System.Collections.Generic;

namespace Domain.Models;

public class User
{

#region Public Properties

    public string                     Id             { get; set; }
    public string                     Name           { get; set; }
    public string                     Password       { get; set; }
    public ICollection<UserPrivilege> UserPrivileges { get; set; }

    #endregion

}