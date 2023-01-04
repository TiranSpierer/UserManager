// UserManager/DAL/CrudServiceBase.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using DAL.Setup;
using Domain.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services.Interfaces;

public abstract class DataServiceBase
{
    protected readonly DataBaseContext _context;
    //protected readonly DbSet<T> _dbSet;

    protected DataServiceBase(DataBaseContext context)
    {
        _context = context;
        //_dbSet = context.Set<T>();
    }
}