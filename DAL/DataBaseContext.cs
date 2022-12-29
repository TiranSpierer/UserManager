// UserManager/DAL/DataBaseSetup.cs
// Created by Tiran Spierer
// Created at 27/12/2022
// Class propose:

using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
        
    }

    public DbSet<User>          Users          { get; set; }
    public DbSet<Patient>       Patients       { get; set; }
    public DbSet<UserPrivilege> UserPrivileges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPrivilege>()
                    .HasKey(up => new { up.UserId, up.Privilege });
    }

}