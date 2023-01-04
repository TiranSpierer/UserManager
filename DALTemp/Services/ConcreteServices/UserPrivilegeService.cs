// UserManager/DAL/UserPrivilegeService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using DALTemp.Services.Interfaces;
using DALTemp.Setup;

namespace DALTemp.Services.ConcreteServices;

public class UserPrivilegeService : DataServiceBase,
                                    IDataService<UserPrivilege>
{
    #region Constructors

    public UserPrivilegeService(DataBaseContext context) : base(context)
    {
    }

    #endregion

    #region Implementation of ICrudService<UserPrivilege>

    public async Task Create(UserPrivilege entity)
    {
        await _context.UserPrivileges!.AddAsync(entity);
        await _context.SaveChangesAsync();
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
                UserId = userId,
                Privilege = privilege
                               });
        }
        await CreateRange(userPrivileges);
    }

    public async Task<UserPrivilege?> GetById(object compositeId)
    {
        UserPrivilege? userPrivilege = null;

        if (compositeId is UserPrivilege id)
        {
            userPrivilege = await _context.UserPrivileges!.FindAsync(id.UserId, id.Privilege);
        }

        return userPrivilege;
    }

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

    public async Task<IEnumerable<UserPrivilege>> GetAll()
    {
        return await _context.UserPrivileges!.ToListAsync();
    }

    public async Task Update(object id, UserPrivilege updatedEntity)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            _context.UserPrivileges!.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(object id)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            _context.UserPrivileges!.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    #endregion
}