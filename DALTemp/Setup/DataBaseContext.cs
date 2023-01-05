/*
    UserManager/DAL/DataBaseSetup.cs
    Created by Tiran Spierer
    Created at 27/12/2022
    Class propose:  Database context class for an Entity Framework Core (EF Core) application. 
                    This context class is used to manage the entities in the application, 
                    which are represented by the DbSet properties (e.g. Users, Patients, etc.).

                    The DbContextOptions<DataBaseContext> parameter passed to the base class in the constructor is used to configure the context.

                    The OnModelCreating method is called when the model for the context is being created. 
                    You can use this method to configure the model (e.g. set up relationships, define keys, etc.). 
                    The ModelBuilder parameter passed to this method provides a way to build the model.

                    Each of the private methods (e.g. UserBuilder, PatientBuilder, etc.) 
                    defines configuration for the corresponding entity type.

                    To add a new database using this context class, you will need to:
                        1. Create a new entity class to represent the entity in your database (e.g. Product, Order, etc.). 
                           This class should be defined in the Models.
                        2. Add a DbSet property to the DataBaseContext class for the new entity.
                        3. In the OnModelCreating method, add a call to a private method that will be used to configure the entity.
                        4. In the private method, use the ModelBuilder parameter to specify any additional configuration for the entity 
                           (e.g. keys, relationships, etc.).
                        5. Create an EntityService class that inherits from DataServiceBase, add the reference to the services and add it to the wrapper class.
*/

using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Setup;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {

    }

    public DbSet<User>?          Users          { get; set; }
    public DbSet<Patient>?       Patients       { get; set; }
    public DbSet<UserPrivilege>? UserPrivileges { get; set; }
    public DbSet<Registration>?  Registrations  { get; set; }
    public DbSet<Procedure>?     Procedures     { get; set; }
    public DbSet<Frame>?         Frames         { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        UserBuilder(modelBuilder);
        PatientBuilder(modelBuilder);
        UserPrivilegeBuilder(modelBuilder);
        RegistrationBuilder(modelBuilder);
        ProcedureBuilder(modelBuilder);
        FrameBuilder(modelBuilder);
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

    private void ProcedureBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Procedure>()
                    .HasOne(p => p.Patient)
                    .WithMany(p => p.Procedures)
                    .HasForeignKey(p => p.PatientId);

        modelBuilder.Entity<Procedure>()
                    .HasOne(p => p.User)
                    .WithMany(u => u.Procedures)
                    .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Procedure>()
                    .HasMany(p => p.Frames)
                    .WithOne(f => f.Procedure)
                    .HasForeignKey(f => f.ProcedureId);
    }

    private void FrameBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Frame>()
                    .HasOne(f => f.Procedure)
                    .WithMany(p => p.Frames)
                    .HasForeignKey(f => f.ProcedureId);
    }

}