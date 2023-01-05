// UserManager/DAL/UserPrivilegeService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services.Interfaces;
using DAL.Setup;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services.ConcreteServices;

public class UserPrivilegeService : DataServiceBase<UserPrivilege>
{
    #region Constructors

    public UserPrivilegeService(DataBaseContext context) : base(context)
    {
    }

    #endregion

    #region Implementation of ICrudService<UserPrivilege>

    public async Task<IEnumerable<Privilege>?> GetAllUserPrivilegesByUserId(string userId)
    {
        var privileges = await _context.UserPrivileges!
                                       .Where(up => up.UserId == userId)
                                       .Select(up => up.Privilege)
                                       .ToListAsync();

        return privileges;
    }

    public async Task<IEnumerable<User>?> GetUsersByPrivilege(Privilege privilege)
    {
        var users = await _context.Users!
                                  .Include(u => u.UserPrivileges)
                                  .Where(u => u.UserPrivileges!.Any(up => up.Privilege == privilege))
                                  .ToListAsync();

        return users;
    }

    public async Task CreateRange(IEnumerable<UserPrivilege> userPrivileges)
    {
        await _context.UserPrivileges!.AddRangeAsync(userPrivileges);
        await _context.SaveChangesAsync();
    }

    public async Task CreateRange(string userId, IEnumerable<Privilege> privileges)
    {
        var userPrivileges = new List<UserPrivilege>();

        foreach (var privilege in privileges)
        {
            userPrivileges.Add(new UserPrivilege()
                               {
                                   UserId    = userId,
                                   Privilege = privilege
                               });
        }
        await CreateRange(userPrivileges);
    }

    #endregion
}