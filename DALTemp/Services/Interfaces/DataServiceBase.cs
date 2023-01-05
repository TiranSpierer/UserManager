// UserManager/DAL/CrudServiceBase.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Setup;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services.Interfaces;

public abstract class DataServiceBase<T> : IDataService<T> where T : class, IEntity<T>
{
    protected readonly DataBaseContext _context;
    protected readonly DbSet<T>        _dbSet;

    protected DataServiceBase(DataBaseContext context)
    {
        _context = context;
        _dbSet   = context.Set<T>();
    }

    #region Implementation of IDataService<T>

    public virtual async Task<bool> Create(T entity)
    {
        var isEntityCreated = true;
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch
        {
            isEntityCreated = false;
        }

        return isEntityCreated;
    }

    public virtual async Task<T?> GetById(params object[] id)
    {
        T?     entity = null;

        if (IsIdValid(id))
        {
            entity = await _dbSet.FindAsync(id);
        }

        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task Update(object id, T updatedEntity)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            try
            {
                updatedEntity.CopyValuesTo(entity);
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch
            {
                await Delete(entity);
                await Create(updatedEntity);
            }
        }
    }

    public virtual async Task Delete(object id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region Private Methods

    private bool IsIdValid(object id)
    {
        return id.GetType() != typeof(string) || id is string s && string.IsNullOrEmpty(s) == false;
    }

    #endregion

}