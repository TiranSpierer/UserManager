﻿// UserManager/DAL/UserService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System;
using System.Collections.Generic;
using System.Linq;
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
        await _context.Users!.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task <User?> GetById(object id)
    {
        return await _context.Users!
                             .Include(u => u.UserPrivileges)
                             .FirstOrDefaultAsync(u => u.Id == id as string);

        //return await _context.Users!.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users!.ToListAsync();
    }

    public async Task Update(object id, User updatedEntity)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            entity.Name     = updatedEntity.Name;
            entity.Password = updatedEntity.Password;
            _context.Users!.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(User entity)
    {
        //_context.Users.Remove(entity);
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();

    }

    public async Task Delete(object id)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            _context.Users!.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    #endregion
}