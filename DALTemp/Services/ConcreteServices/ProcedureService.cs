// UserManager/DAL/ProcedureService.cs
// Created by Tiran Spierer
// Created at 05/01/2023
// Class propose:

using DAL.Services.Interfaces;
using DAL.Setup;
using Domain.Models;

namespace DAL.Services.ConcreteServices;

public class ProcedureService : DataServiceBase<Procedure>
{

#region Constructors

    public ProcedureService(DataBaseContext context) : base(context)
    {
    }

    #endregion

}