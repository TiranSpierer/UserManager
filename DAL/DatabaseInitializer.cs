// UserManager/DAL/DatabaseInitializer.cs
// Created by Tiran Spierer
// Created at 29/12/2022
// Class propose:

using System.Collections.Generic;
using System.IO;
using Domain.Models;

namespace DAL;

public class DatabaseInitializer : IDatabaseInitializer
{
    #region Privates

    private readonly DataBaseContext _context;

    #endregion

    #region Constructors

    public DatabaseInitializer(DataBaseContext context)
    {
        _context = context;
    }

    #endregion

#region Public Methods

    public void Initialize()
    {
        _context.Database.EnsureCreated();
        CreateDefaultUser();
    }

    #endregion

#region Private Methods

    private void CreateDefaultUser()
    {
        if (_context.Users?.Find("Admin") == null)
        {
            var user = new User
                       {
                           Id       = "Admin",
                           Name     = "Tiran",
                           Password = "ENadpass"
                       };
            var privileges = new List<Privilege>
                             {
                                 Privilege.AddUsers,
                                 Privilege.DeleteUsers,
                                 Privilege.EditUsers
                             };
            foreach (var privilege in privileges)
            {
                var userPrivilege = new UserPrivilege
                                    {
                                        UserId    = user.Id,
                                        Privilege = privilege
                                    };
                _context.UserPrivileges?.Add(userPrivilege);
            }

            _context.Users?.Add(user);
            _context.SaveChanges();
        }
    }

#endregion
}