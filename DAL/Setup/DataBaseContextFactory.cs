// UserManager/DAL/DataBaseContextFactory.cs
// Created by Tiran Spierer
// Created at 01/01/2023
// Class propose:

using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Setup;

public class DataBaseContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public DataBaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }

    public DataBaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<DataBaseContext> options = new DbContextOptionsBuilder<DataBaseContext>();

        _configureDbContext(options);

        return new DataBaseContext(options.Options);
    }
}