// UserManager/DAL/UserService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System;
using System.Collections.Generic;
using Domain.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services;

public class UserService : DataServiceBase,
                           IDataService<User>
{
#region Constructors

    public UserService(DataBaseContext context) : base(context) { }

#endregion

#region Implementation of ICrudService<User>

    public async Task Create(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task <User?> GetById(object id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task Update(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User entity)
    {
        //_context.Users.Remove(entity);
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

#endregion
}