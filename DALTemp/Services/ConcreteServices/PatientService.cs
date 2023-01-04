// UserManager/DAL/PatientService.cs
// Created by Tiran Spierer
// Created at 28/12/2022
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using DALTemp.Services.Interfaces;
using DALTemp.Setup;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DALTemp.Services.ConcreteServices;

public class PatientService : DataServiceBase<Patient>
{
    #region Constructors

    public PatientService(DataBaseContext context) : base(context) { }

    #endregion
}