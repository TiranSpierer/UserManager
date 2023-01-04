// UserManager/DAL/DataBaseSetup.cs
// Created by Tiran Spierer
// Created at 27/12/2022
// Class propose:

using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DALTemp.Setup;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {

    }

    public DbSet<User>?          Users          { get; set; }
    public DbSet<Patient>?       Patients       { get; set; }
    public DbSet<UserPrivilege>? UserPrivileges { get; set; }
    public DbSet<Registration>?  Registrations  { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        UserBuilder(modelBuilder);
        PatientBuilder(modelBuilder);
        UserPrivilegeBuilder(modelBuilder);
        RegistrationBuilder(modelBuilder);
    }

    private static void UserBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                    .HasMany(u => u.UserPrivileges)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
    }

    private static void PatientBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

        modelBuilder.Entity<Patient>()
                    .Property(p => p.Name)
                    .IsRequired(false);

        modelBuilder.Entity<Patient>()
                    .Property(p => p.DateOfBirth)
                    .IsRequired(false);
    }

    private static void UserPrivilegeBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPrivilege>()
                    .HasKey(up => new { up.UserId, up.Privilege });
    }

    private static void RegistrationBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>()
                    .HasKey(r => r.ProcedureId);
    }
}