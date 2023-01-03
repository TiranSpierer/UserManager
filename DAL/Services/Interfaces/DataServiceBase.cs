// UserManager/DAL/CrudServiceBase.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

namespace DAL.Services.Interfaces;

public abstract class DataServiceBase
{
    protected readonly DataBaseContext _context;

    protected DataServiceBase(DataBaseContext context) => _context = context;
}