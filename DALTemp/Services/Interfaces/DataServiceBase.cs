/*
    UserManager/DAL/Interfaces/DataServiceBase.cs
    Created by Tiran Spierer
    Created at 27/12/2022
    Class propose:  Abstract base class that provides basic CRUD (Create, Read, Update, Delete) functionality for a database entity. 
                    It uses Entity Framework Core (EF Core) to interact with the database.
   
                    The base class is parameterized with a generic type T that represents the entity type. 
                    The class implements the IDataService<T> interface, which defines methods for creating, retrieving, updating, and deleting entities.
   
                    The base class has a protected field _context of type DataBaseContext, which is used to access the database. 
                    It also has a protected field _dbSet of type DbSet<T>, which represents the specific entity in the database for the inheritors.
   
                    The base class has a protected constructor that takes a DataBaseContext parameter and sets the _context and _dbSet fields.
   
                    The base class provides an implementation of the IDataService<T> methods using the _context and _dbSet fields. 
                    For example, the Create method adds a new entity to the database and saves the changes, the GetById method retrieves an entity by its ID, and so on.
   
                    The base class also has a private method IsIdValid that is used to check if an ID is valid.
*/

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