// UserManager/DAL/PatientService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services;

public class PatientService : DataServiceBase,
                              IDataService<Patient>
{
#region Constructors

    public PatientService(DataBaseContext context) : base(context) { }

#endregion

#region Implementation of ICrudService<Patient>

    public async Task Create(Patient entity)
    {
        _context.Patients.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient?> GetById(object id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<IEnumerable<Patient>> GetAll()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task Update(Patient entity)
    {
        _context.Patients.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Patient entity)
    {
        _context.Patients.Remove(entity);
       await _context.SaveChangesAsync();
    }

#endregion
}