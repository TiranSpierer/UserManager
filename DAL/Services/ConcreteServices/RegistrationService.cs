//// UserManager/DAL/RegistrationService.cs
//// Created by Tiran Spierer
//// Created at 04/01/2023
//// Class propose:

//using System;
//using DAL.Services.Interfaces;
//using DAL.Setup;
//using Domain.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

//namespace DAL.Services.ConcreteServices;

//public class RegistrationService : DataServiceBase, IDataService<Registration>
//{
//    #region Constructors

//    public RegistrationService(DataBaseContext context) : base(context) { }

//    #endregion

//    #region Implementation of ICrudService<Registration>

//    public async Task Create(Registration entity)
//    {
//        if (string.IsNullOrEmpty(entity.ProcedureId) == false)
//        {
//            await _context.Registrations!.AddAsync(entity);
//            await _context.SaveChangesAsync();
//        }
//    }

//    public async Task<Registration?> GetById(object procedureId)
//    {
//        Registration? registration = null;
//        if (procedureId is string id)
//        {
//            registration = await _context.Registrations!
//                                        .FirstOrDefaultAsync(r => r.ProcedureId == id);
//        }

//        return registration;
//    }

//    public async Task<IEnumerable<Registration>> GetAll()
//    {
//        return await _context.Registrations!.ToListAsync();
//    }

//    public async Task Update(object id, Registration updatedEntity)
//    {
//        var entity = await GetById(id);

//        if (entity != null)
//        {
//            if (entity.ProcedureId == updatedEntity.ProcedureId)
//            {
//                entity.XiphoidProcessPosition = updatedEntity.XiphoidProcessPosition;
//                entity.ReferenceSensorPosition = updatedEntity.ReferenceSensorPosition;
//                _context.Registrations!.Update(entity);
//                await _context.SaveChangesAsync();
//            }
//            else
//            {
//                await Delete(entity.ProcedureId);
//                await Create(updatedEntity);
//            }
//        }

//        var x = _context.Registrations;
//    }

//    public async Task Delete(object id)
//    {
//        var entity = await GetById(id);
//        if (entity != null)
//        {
//            _context.Registrations!.Remove(entity);
//            await _context.SaveChangesAsync();
//        }
//    }

//    #endregion
//}