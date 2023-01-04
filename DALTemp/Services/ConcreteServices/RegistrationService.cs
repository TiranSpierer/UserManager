// UserManager/DAL/RegistrationService.cs
// Created by Tiran Spierer
// Created at 04/01/2023
// Class propose:

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Automation;
using DALTemp.Services.Interfaces;
using DALTemp.Setup;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DALTemp.Services.ConcreteServices;

public class RegistrationService : DataServiceBase<Registration>
{
    #region Constructors

    public RegistrationService(DataBaseContext context) : base(context) { }

    #endregion

}