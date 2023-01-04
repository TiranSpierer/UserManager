// UserManager/DAL/IDataBaseService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DALTemp.Services.Interfaces;

public interface IDataService<T> where T : class
{
    Task Create(T entity);
    Task<T?> GetById(object id);
    Task<IEnumerable<T>> GetAll();
    Task Update(object id, T updatedEntity);
    Task Delete(object id);
}