// UserManager/DAL/DatabaseInitializer.cs
// Created by Tiran Spierer
// Created at 29/12/2022
// Class propose:

using System;
using System.Linq;

namespace DALTemp.Setup;

public class DatabaseInitializer : IDatabaseInitializer
{
    #region Privates

    private const string DEFAULT_ADMIN_NAME = "Administrator";
    private const string DEFAULT_ADMIN_PASSWORD = "ENadpass";
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
        if (_context.Users!.Find(DEFAULT_ADMIN_NAME) == null)
        {
            var user = new User
            {
                Id = DEFAULT_ADMIN_NAME,
                Name = DEFAULT_ADMIN_NAME,
                Password = DEFAULT_ADMIN_PASSWORD
            };
            var userPrivileges = Enum.GetValues(typeof(Privilege))
                                     .Cast<Privilege>()
                                     .Select(p => new UserPrivilege { UserId = user.Id, Privilege = p });

            _context.UserPrivileges!.AddRange(userPrivileges);
            _context.Users?.Add(user);
            _context.SaveChanges();
        }
    }

    #endregion
}