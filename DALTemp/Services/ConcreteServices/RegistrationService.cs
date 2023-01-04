// UserManager/DAL/RegistrationService.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using DALTemp.Services.Interfaces;
using DALTemp.Setup;

namespace DALTemp.Services.ConcreteServices;

public class RegistrationService : DataServiceBase<Registration>, IDataService<Registration>
{
    #region Constructors

    public RegistrationService(DataBaseContext context) : base(context) { }

    #endregion

    #region Implementation of ICrudService<Registration>

    public override async Task Create(Registration entity)
    {
        if (string.IsNullOrEmpty(entity.ProcedureId) == false)
        {
            await _context.Registrations!.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

    public override async Task<Registration?> GetById(object procedureId)
    {
        Registration? registration = null;
        if (procedureId is string id)
        {
            registration = await _context.Registrations!
                                        .FirstOrDefaultAsync(r => r.ProcedureId == id);
        }

        return registration;
    }

    public override async Task<IEnumerable<Registration>> GetAll()
    {
        return await _context.Registrations!.ToListAsync();
    }

    public override async Task Update(object id, Registration updatedEntity)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            if (entity.ProcedureId == updatedEntity.ProcedureId)
            {
                entity.XiphoidProcessPosition = updatedEntity.XiphoidProcessPosition;
                entity.ReferenceSensorPosition = updatedEntity.ReferenceSensorPosition;
                _context.Registrations!.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                await Delete(entity.ProcedureId);
                await Create(updatedEntity);
            }
        }

        var x = _context.Registrations;
    }

    public override async Task Delete(object id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _context.Registrations!.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    #endregion
}