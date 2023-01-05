// UserManager/DAL/UserService.cs
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

public class UserService : DataServiceBase<User>
{
    #region Constructors

    public UserService(DataBaseContext context) : base(context) { }

    #endregion

    #region Implementation of ICrudService<User>

    public override async Task<User?> GetById(params object[] userId)
    {
        User? user = null;
        if (userId.Length == 1 && userId[0] is string id)
        {
            user = await _dbSet
                        .Include(u => u.UserPrivileges)
                        .FirstOrDefaultAsync(u => u.Id == id);
        }
        return user;
    }

    public override async Task<IEnumerable<User>> GetAll()
    {
        return await _dbSet
                    .Include(u => u.UserPrivileges)
                    .ToListAsync();
    }

#endregion
}