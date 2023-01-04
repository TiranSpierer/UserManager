// UserManager/DALTemp/DataBaseContextFactory.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using Microsoft.EntityFrameworkCore;
using System;

namespace DALTemp.Setup;

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