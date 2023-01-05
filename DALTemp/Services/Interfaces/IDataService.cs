// UserManager/DAL/IDataBaseService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace DAL.Services.Interfaces;

public interface IDataService<T> where T : class, IEntity<T>
{
    Task<bool>           Create(T       entity);
    Task<T?>             GetById(params object[] id);
    Task<IEnumerable<T>> GetAll();
    Task                 Update(object id, T updatedEntity);
    Task                 Delete(object id);
}