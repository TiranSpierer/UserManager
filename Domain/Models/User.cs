// UserManager/Domain/User.cs
// Created by Tiran Spierer
// Created at 27/12/2022
// Class propose:

using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models;

public class User : IEntity<User>
{

#region Public Properties

    public         string                      Id             { get; set; }
    public         string?                     Name           { get; set; }
    public         string?                     Password       { get; set; }

    public         ICollection<UserPrivilege>? UserPrivileges { get; set; }
    public virtual ICollection<Procedure>      Procedures     { get; set; }

    #endregion

    #region Implementation of IEntity<in User>
    public void CopyValuesTo(User entity)
    {
        entity.Id = Id;
        entity.Name = Name;
        entity.Password = Password;
        entity.UserPrivileges = UserPrivileges;
    }

#endregion
}