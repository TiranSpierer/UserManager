/*
    UserManager/DAL/IDataBaseService.cs
    Created by Tiran Spierer
    Created at 28/12/2022
    Class propose:  This is the definition of an interface that provides basic CRUD (Create, Read, Update, Delete) functionality for a database entity.
                    The interface is parameterized with a generic type T that represents the entity type. 
                    The IEntity<T> constraint specifies that T must be a reference type and must implement the IEntity<T> interface.
*/

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