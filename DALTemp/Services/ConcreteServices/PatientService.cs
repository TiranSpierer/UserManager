// UserManager/DAL/PatientService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using DALTemp.Services.Interfaces;
using DALTemp.Setup;

namespace DALTemp.Services.ConcreteServices;

public class PatientService : DataServiceBase<Patient>,
                              IDataService<Patient>
{
    #region Constructors

    public PatientService(DataBaseContext context) : base(context) { }

    #endregion

    #region Implementation of ICrudService<Patient>

    public override async Task Create(Patient entity)
    {
        //TODO exception thrown if entity with the same id already in DB
        _context.Patients!.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient?> GetById(object id)
    {
        return await _context.Patients!.FindAsync(id);
    }

    public async Task<IEnumerable<Patient>> GetAll()
    {
        return await _context.Patients!.ToListAsync();
    }

    public async Task Update(object id, Patient updatedEntity)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            entity.Name = updatedEntity.Name;
            entity.DateOfBirth = updatedEntity.DateOfBirth;
            _context.Patients!.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(object id)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            _context.Patients!.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    #endregion
}