// UserManager/Domain/IEntity.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose: Base class for entities. Method CopyValuesTo() is for the DataBaseService to be able to update an entity. 

namespace Domain.Interfaces;

public interface IEntity<in T> where T : class
{
    void CopyValuesTo(T entity);
}