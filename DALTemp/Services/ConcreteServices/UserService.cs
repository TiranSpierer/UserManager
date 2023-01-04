// UserManager/DAL/UserService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using DALTemp.Services.Interfaces;
using DALTemp.Setup;

namespace DALTemp.Services.ConcreteServices;

public class UserService : DataServiceBase<User>,
                           IDataService<User>
{
    #region Constructors

    public UserService(DataBaseContext context) : base(context) { }

    #endregion

    #region Implementation of ICrudService<User>

    public async Task Create(User entity)
    {
        if (string.IsNullOrEmpty(entity.Id) == false)
        {
            await _context.Users!.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

    public override async Task<User?> GetById(object userId)
    {
        User? user = null;
        if (userId is string id)
        {
            user = await _context.Users!
                                 .Include(u => u.UserPrivileges)
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users!
                             .Include(u => u.UserPrivileges)
                             .ToListAsync();
    }

    public async Task Update(object id, User updatedEntity)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            if (entity.Id == updatedEntity.Id)
            {
                entity.Name     = updatedEntity.Name;
                entity.Password = updatedEntity.Password;
                _context.Users!.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                await Delete(entity.Id);
                await Create(updatedEntity);
            }
        }
    }

    public async Task Delete(object id)
    {
        var entity = await GetById(id);
        var isIndelible = entity?.UserPrivileges!.Any(up => up.Privilege == Privilege.Indelible);

        if (entity != null && isIndelible == false)
        {
            _context.Users!.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    #endregion
}