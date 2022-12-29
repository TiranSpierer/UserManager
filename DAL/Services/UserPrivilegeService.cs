// UserManager/DAL/UserPrivilegeService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services;

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
        await _context.UserPrivileges.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<UserPrivilege?> GetById(object id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserPrivilege?> GetById(string id, Privilege privilege)
    {
        return await _context.UserPrivileges.FindAsync(id, privilege);
    }

    public async Task<IEnumerable<Privilege>?> GetAllById(string id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        return user?.UserPrivileges.Select(up => up.Privilege);
    }

    public async Task<IEnumerable<UserPrivilege>> GetAll()
    {
        return await _context.UserPrivileges.ToListAsync();
    }

    public async Task Update(UserPrivilege entity)
    {
        _context.UserPrivileges.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(UserPrivilege entity)
    {
        _context.UserPrivileges.Remove(entity);
        await _context.SaveChangesAsync();
    }

#endregion
}