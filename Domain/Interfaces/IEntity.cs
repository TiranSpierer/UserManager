// UserManager/Domain/IEntity.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IEntity<in T> where T : class
{
    void CopyValuesTo(T entity);
}