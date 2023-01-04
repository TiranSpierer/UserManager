// UserManager/DAL/CrudServiceBase.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using DALTemp.Setup;

namespace DALTemp.Services.Interfaces;

public abstract class DataServiceBase<T> : IDataService<T> where T : class
{
    protected readonly DataBaseContext _context;

    protected DataServiceBase(DataBaseContext context)
    {
        _context        = context;
    }

    #region Implementation of ICrudService<T>

    public virtual async Task Create(T entity)
    {
        if (await IsEntityInDataBase(entity) == false)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        else
        {
            // Todo - return boolean value or throw an exception?
        }
    }

    public virtual async Task<T?> GetById(object id)
    {
        T?     entity = null;

        if (IsIdValid(id))
        {
            entity = await _context.Set<T>().FindAsync(id);
        }

        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task Update(object id, T updatedEntity)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            _context.Set<T>().Update(updatedEntity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task Delete(object id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    #endregion

    protected bool IsIdValid(object id)
    {
        return id is not string || id is string s && string.IsNullOrEmpty(s) == false;
    }
}